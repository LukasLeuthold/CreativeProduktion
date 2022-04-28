using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HeroData", menuName = "ScriptableUnitData/HeroData", order = 1)]
    public class HeroData : UnitData
    {
        public Animator Anim { get; set; }
        public DragDrop Unit;
        [SerializeField] private StatBlock[] unitStats = new StatBlock[3];
        [SerializeField, Range(1, 3)] private int currLevel = 1;
        [SerializeField] private HeroCollection activeHeroCollection;

        [Header("Attributes")]
        [SerializeField] private HeroCollection allianceAttribute;


        [SerializeField] private HeroCollection classAttribute;
        [Header("Rarity")]
        [SerializeField] private HeroRarity rarity;
        //ADDED
        [SerializeField] private int currCost;
        public int CurrCost
        {
            get
            {
                if (currLevel == 1)
                {
                    return rarity.Cost;
                }
                return rarity.Cost *  int.Parse(Mathf.Pow(3,currLevel-1).ToString());
            }
        }

        public string AllianceName
        {
            get
            {
                return allianceAttribute.name;
            }
        }
        public string ClassName
        {
            get
            {
                return classAttribute.name;
            }
        }
        public int CurrLevel
        {
            get
            {
                return currLevel;
            }
            set
            {
                if (value < 1 || 3 < value)
                {
                    return;
                }
                currLevel = value;
                CurrStatBlock = unitStats[currLevel - 1];
            }
        }
        public HeroRarity Rarity { get => rarity; private set => rarity = value; }

        private void OnValidate()
        {
            CurrStatBlock = unitStats[currLevel - 1];
        }

        public void PlaceOnField(DragDrop _heroDrag)
        {
            activeHeroCollection.AddToCollection(this, _heroDrag);
            allianceAttribute.AddToCollection(this, _heroDrag);
            classAttribute.AddToCollection(this, _heroDrag);
        }
        public void RemoveFromField(DragDrop _heroDrag)
        {
            activeHeroCollection.RemoveFromCollection(this, _heroDrag);
            allianceAttribute.RemoveFromCollection(this, _heroDrag);
            classAttribute.RemoveFromCollection(this, _heroDrag);
        }

        public override void Attack(Vector2 _enemyField)
        {
            Anim.Play("Attack");
            Debug.Log("Attack");
        }

        public HeroData GetCopy()
        {
            HeroData copy = Object.Instantiate(this);
            copy.name = this.name;
            copy.CurrLevel = 1;
            return copy;
        }

        public override void Tick()
        {
            for (int i = 0; i < (CurrStatBlock.AmountAttackActions + CurrStatModifier.AmountAttackActionsMod); i++)
            {
            }
        }

        public override string ToString()
        {
            return $"name: {name}; maxHP{this.CurrStatBlock.MaxHP}; maxHPMod{CurrStatModifier.MaxHPMod}";
        }
    }
}
