using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HeroData", menuName = "Hero/HeroData", order = 1)]
    public class HeroData : UnitData
    {
        public Animator Anim { get; set; }
        public DragDrop Unit;
        public bool isMele;

        [SerializeField] private StatBlock[] unitStats = new StatBlock[3];

        [Header("Level")]
        [SerializeField, Range(1, 3)] private int currLevel = 1;
        

        [Header("HeroCollections")]
        [SerializeField] private HeroCollection allianceAttribute;
        [SerializeField] private HeroCollection classAttribute;
        [SerializeField] private HeroCollection activeHeroCollection;



        [Header("Rarity")]
        [SerializeField] private HeroRarity rarity;


        [Header("Audio")]
        [SerializeField] private AUDIOScriptableEvent onUnitAttack;


        [SerializeField] private int currCost;
        public int CurrCost
        {
            get
            {
                if (currLevel == 1)
                {
                    return rarity.Cost;
                }
                return rarity.Cost * int.Parse(Mathf.Pow(3, currLevel - 1).ToString());
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
                switch (value)
                {
                    case 1:

                        break;
                    default:
                        break;
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
            classAttribute.RemoveFromCollection(this,_heroDrag);
        }

        public override void Attack(Vector2 _enemyField)
        {
            onUnitAttack?.Raise();
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.DamageText.text = (CurrStatBlock.Attack + CurrStatModifier.AttackMod).ToString();
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.DamageText.gameObject.SetActive(true);
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.anim.Play("Damage");        

            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().enemyPrefab.currHP -= (CurrStatBlock.Attack + CurrStatModifier.AttackMod);

            Anim.Play("Attack");

        }

        public HeroData GetCopy()
        {
            HeroData copy = Object.Instantiate(this);
            copy.name = this.name;
            copy.CurrLevel = 1;
            return copy;
        }


        public override string ToString()
        {
            return $"name: {name}; maxHP{this.CurrStatBlock.MaxHP}; maxHPMod{CurrStatModifier.MaxHPMod}";
        }

       
    }
}
