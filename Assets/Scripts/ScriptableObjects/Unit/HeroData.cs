using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HeroData", menuName = "ScriptableUnitData/HeroData", order = 1)]
    public class HeroData : UnitData
    {
        [SerializeField] private StatBlock[] unitStats = new StatBlock[3];
        [SerializeField, Range(1, 3)] private int currLevel = 1;
        [SerializeField] private HeroCollection activeHeroCollection;

        [Header("Attributes")]
        [SerializeField] private HeroCollection allianceAttribute;
        [SerializeField] private HeroCollection classAttribute;
        [Header("Rarity")]
        [SerializeField] private HeroRarity rarity;
        public Animator anim { get; set; }
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

        public void PlaceOnField()
        {
            //TODO: test copy thingy
            activeHeroCollection.AddToCollection(GetCopy());
            allianceAttribute.AddToCollection(this);
            classAttribute.AddToCollection(this);
        }
        public void RemoveFromField()
        {
            activeHeroCollection.RemoveFromCollection(GetCopy());
            allianceAttribute.RemoveFromCollection(this);
            classAttribute.RemoveFromCollection(this);
        }

        public override void Attack()
        {
            anim.Play("Attack");
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
                Attack();
            }
        }

        public override string ToString()
        {
            return $"name: {name}; maxHP{this.CurrStatBlock.MaxHP}; maxHPMod{CurrStatModifier.MaxHPMod}";
        }
    }
}
