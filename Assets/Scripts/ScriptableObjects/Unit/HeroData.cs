using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to hold herodata
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroData", menuName = "Hero/HeroData", order = 1)]
    public class HeroData : UnitData
    {
        /// <summary>
        /// animator of the hero prefab
        /// </summary>
        public Animator Anim { get; set; }
        /// <summary>
        /// the heroprefab
        /// </summary>
        public DragDrop Unit;
        /// <summary>
        /// flag if unit is melee or not
        /// </summary>
        public bool isMele;

        /// <summary>
        /// statblocks of the different levels of the hero
        /// </summary>
        [SerializeField] private StatBlock[] unitStats = new StatBlock[3];

        /// <summary>
        /// the current level of the hero
        /// </summary>
        [Header("Level")]
        [SerializeField, Range(1, 3)] private int currLevel = 1;
        
        /// <summary>
        /// the alliance attribute of the hero
        /// </summary>
        [Header("HeroCollections")]
        [SerializeField] private HeroCollection allianceAttribute;
        /// <summary>
        /// the class attribute of the hero
        /// </summary>
        [SerializeField] private HeroCollection classAttribute;
        /// <summary>
        /// the active hero collection all heroes get added to when in play
        /// </summary>
        [SerializeField] private HeroCollection activeHeroCollection;

        /// <summary>
        /// the rarity of the hero
        /// </summary>
        [Header("Rarity")]
        [SerializeField] private HeroRarity rarity;

        /// <summary>
        /// gets called when hero attacks
        /// </summary>
        [Header("Audio")]
        [SerializeField] private AUDIOScriptableEvent onUnitAttack;

        /// <summary>
        /// the current golld cost of the hero
        /// </summary>
        [SerializeField] private int currCost;
        /// <summary>
        /// the current golld cost of the hero
        /// </summary>
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
        /// <summary>
        /// the name of the alliance attribute
        /// </summary>
        public string AllianceName
        {
            get
            {
                return allianceAttribute.name;
            }
        }
        /// <summary>
        /// the name of the class attribute
        /// </summary>
        public string ClassName
        {
            get
            {
                return classAttribute.name;
            }
        }
        /// <summary>
        /// the current level of the hero
        /// </summary>
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
        /// <summary>
        /// the rarity of the hero
        /// </summary>
        public HeroRarity Rarity { get => rarity; private set => rarity = value; }
        /// <summary>
        /// sets the correct statblock if values change
        /// </summary>
        private void OnValidate()
        {
            CurrStatBlock = unitStats[currLevel - 1];
        }
        /// <summary>
        /// gets called when unit is placed on field; registers in all collections
        /// </summary>
        /// <param name="_heroDrag">prefab of the hero</param>
        public void PlaceOnField(DragDrop _heroDrag)
        {
            activeHeroCollection.AddToCollection(this, _heroDrag);
            allianceAttribute.AddToCollection(this, _heroDrag);
            classAttribute.AddToCollection(this, _heroDrag);
        }
        /// <summary>
        /// gets called when unit is removed from field; unregisters from all collections
        /// </summary>
        /// <param name="_heroDrag">prefab of the hero</param>
        public void RemoveFromField(DragDrop _heroDrag)
        {
            activeHeroCollection.RemoveFromCollection(this, _heroDrag);
            allianceAttribute.RemoveFromCollection(this, _heroDrag);
            classAttribute.RemoveFromCollection(this,_heroDrag);
        }
        /// <summary>
        /// attacks the target field
        /// </summary>
        /// <param name="_enemyField">targetfield</param>
        public override void Attack(Vector2 _enemyField)
        {
            onUnitAttack?.Raise();
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.DamageText.text = (CurrStatBlock.Attack + CurrStatModifier.AttackMod).ToString();
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.DamageText.gameObject.SetActive(true);
            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().EnemyOnField.anim.Play("Damage");        

            GameField.Instance.Slots[(int)_enemyField.x, (int)_enemyField.y].GetComponent<EnemyField>().enemyPrefab.currHP -= (CurrStatBlock.Attack + CurrStatModifier.AttackMod);

            Anim.Play("Attack");
        }
        /// <summary>
        /// returns a copy of the data object
        /// </summary>
        /// <returns></returns>
        public HeroData GetCopy()
        {
            HeroData copy = Object.Instantiate(this);
            copy.name = this.name;
            copy.CurrLevel = 1;
            return copy;
        }
        /// <summary>
        /// to string method of the herodata class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"name: {name}; maxHP{this.CurrStatBlock.MaxHP}; maxHPMod{CurrStatModifier.MaxHPMod}";
        }
    }
}
