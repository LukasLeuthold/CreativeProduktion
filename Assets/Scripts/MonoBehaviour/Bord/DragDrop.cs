using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace AutoDefense
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>Field of the Unit</summary>
        [HideInInspector] public UnitSlot LastSlot;
        
        /// <summary>tells if the Unit is dead</summary>
        [HideInInspector]public bool isDead;
        /// <summary></summary>
        [HideInInspector]public bool CantDragDrop;

        /// <summary>Damage Number that shows when the Unit gets Damage</summary>
        public TMP_Text damageText;
       
        /// <summary>Current HP of the Unit</summary>
        private int currHP;

        /// <summary>All Unit relevant Data</summary>
        [SerializeField] private HeroData heroData;

        [Header("StarSprites")]
        /// <summary> Sarte Image für the hover Boarder</summary>
        [SerializeField] private Image starImage;
        /// <summary>One Star sprite for the hover Boarder</summary>
        [SerializeField] Sprite oneStarSprite;
        /// <summary>two Star sprite for the hover Boarder</summary>
        [SerializeField] Sprite twoStarSprite;
        /// <summary>three Star sprite for the hover Boarder</summary>
        [SerializeField] Sprite threeStarSprite;

        [Header("CurrStats")]
        /// <summary>true when the Unit gets draged over an Field</summary>
        [HideInInspector] public bool haveSlot;

        /// <summary>Hover Boarder</summary>
        [SerializeField] private GameObject currStats;
        /// <summary>Hp text from the hover Boarder</summary>
        [SerializeField] private Text hP;
        /// <summary>speed Text from the hover Boarder</summary>
        [SerializeField] private Text speed;
        /// <summary>Attack Text from the hover Boarder</summary>
        [SerializeField] private Text aT;

        [Header("Details")]
        /// <summary>Details Boarder</summary>
        [SerializeField] private GameObject details;
        /// <summary>mele Unit text from the  Details Boarder</summary>
        [SerializeField] private GameObject meleUnitText;
        /// <summary>range Unit text from the Details Boarder/</summary>
        [SerializeField] private GameObject rangeUnitText;
        /// <summary>HPtext from the Details Boarder/</summary>
        [SerializeField] private Text _HP;
        /// <summary>Speed text from the Details Boarder/</summary>
        [SerializeField] private Text _Speed;
        /// <summary>Attacktext from the Details Boarder/</summary>
        [SerializeField] private Text _AT;
        /// <summary>range text from the Details Boarder/</summary>
        [SerializeField] private Text _Range;
        /// <summary>Name text from the Details Boarder/</summary>
        [SerializeField] private Text _Name;
        /// <summary>Cost text from the Details Boarder/</summary>
        [SerializeField] private Text _Cost;
        /// <summary>Alliance text from the Details Boarder/</summary>
        [SerializeField] private Text _Alliance;
        /// <summary>Class text from the Details Boarder/</summary>
        [SerializeField] private Text _Class;
        /// <summary>Boarder Image from the Details Boarder/</summary>
        [SerializeField] private Image _border;
        /// <summary>Hero Image from the Details Boarder</summary>
        [SerializeField] private Image heroImage;
        /// <summary>Highlight Image Attribute</summary>
        [SerializeField] private Image highlightImage;
        
        /// <summary>Toggle</summary>
        private bool isHiddenCardToggle = true;
        /// <summary>Unit Transfomr</summary>
        private RectTransform unitRectTransform;
        /// <summary>Last Unit Transform</summary>
        private Vector2 lastRectTranform;
        /// <summary>Game Canvas</summary>
        private CanvasGroup canvasGroup;

        /// <summary>Unit Animator</summary>
        [SerializeField]private Animator animator;
        /// <summary>Audio Event</summary>
        [SerializeField]private AUDIOScriptableEvent OnUnitDeath;
        /// <summary>Game Canvas</summary>
        private Canvas canvas;
        /// <summary>Unit start Drag Event</summary>
        [SerializeField] private VOIDScriptableEvent OnUnitDragStart;
        /// <summary>Unit end Drag Event</summary>
        [SerializeField] private VOIDScriptableEvent OnUnitDragEnd;

        /// <summary>All the Unit Data</summary>
        public HeroData HData
        {
            get => heroData;
            set
            {
                if (heroData != null)
                {
                    heroData.OnModifierChanged -= UpdateUnitCard;
                    heroData.OnCurrStatBlockChanged -= UpdateUnitCard;
                }
                heroData = value;
                UpdateUnitCard();
                heroData.OnModifierChanged += UpdateUnitCard;
                    heroData.OnCurrStatBlockChanged += UpdateUnitCard;
            }
        }

        /// <summary>The current HP of the Unit</summary>
        public int CurrHP
        {
            get
            {
                return currHP;
            }
            set
            {
                if (value >= heroData.CurrStatBlock.MaxHP)
                {
                    currHP = heroData.CurrStatBlock.MaxHP;
                    return;
                }
                currHP = value;
                if (currHP <=0)
                {
                    currHP = 0;
                }
            }
        }

        /// <summary>default values</summary>
        private void Start()
        {
            canvas = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
            unitRectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            currStats.SetActive(false);
            details.SetActive(false);
            highlightImage.gameObject.SetActive(false);
            CurrHP = heroData.CurrStatBlock.MaxHP;
        }
        /// <summary>default values</summary>
        private void Update()
        {
            hP.text = CurrHP.ToString();
            if (GameField.Instance.isGrabing)
            {
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.blocksRaycasts = true;
            }
           
            if (CurrHP <= 0)
            {
                isDead = true;
                OnUnitDeath.Raise();
            }
           
            if (isDead)
            {
                SwitchUnitOnOFF(isDead);
            }
            else
            {
                SwitchUnitOnOFF(isDead);
            }
        }
        /// <summary>Update unit</summary>
        private void OnValidate()
        {
            if (heroData == null)
            {
                return;
            }

            UpdateUnitCard();
        }
        /// <summary>Unit OnBeginDrag Event</summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            OnUnitDragStart?.Raise();
            if (LastSlot != null && LastSlot.isGameField)
            {
                LastSlot._HData = null;
                heroData.RemoveFromField(this);

            }
            ChangeFieldsSlectionBoarder(0.3f);
            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
            GameField.Instance.isGrabing = true;

            lastRectTranform = unitRectTransform.anchoredPosition;
            transform.SetAsLastSibling();
        }
        /// <summary>Unit</summary>
        public void OnDrag(PointerEventData eventData)
        {
            if (CantDragDrop)
            {
                return;
            }
            
            unitRectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        }
        /// <summary>Unit OnEndDrag Event</summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            OnUnitDragEnd?.Raise();
            ChangeFieldsSlectionBoarder(0);
            canvasGroup.alpha = 1f;

            GameField.Instance.isGrabing = false;
            canvasGroup.blocksRaycasts = true;
            if (!haveSlot)
            {
                if (LastSlot.isGameField)
                {
                heroData.PlaceOnField(this);
                }
                unitRectTransform.anchoredPosition = lastRectTranform;
                LastSlot._HData = HData;
            }
            haveSlot = false;
        }
        /// <summary>Unit OnPointerEnterEvent</summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            currStats.SetActive(true);
            transform.SetAsLastSibling();
            if (LastSlot.isGameField)
            {
                PrintRangeOnField(Color.green, 0.5f);
            }
        }
        /// <summary>Unit On PointerExit Event</summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            currStats.SetActive(false);
            details.SetActive(false);
            if (LastSlot.isGameField)
            {
                PrintRangeOnField(Color.white, 0);
            }
            isHiddenCardToggle = true;

        }
        /// <summary>Unit ONPointerClick Event</summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!isHiddenCardToggle)
                {
                    isHiddenCardToggle = true;
                    details.SetActive(false);
                    currStats.SetActive(true);
                }
                else if (isHiddenCardToggle)
                {
                    isHiddenCardToggle = false;
                    details.SetActive(true);
                    currStats.SetActive(false);
                }
            }
        }

       /// <summary>
       /// Turns units on off when they die 
       /// </summary>
       /// <param name="_switch"></param>
        public void SwitchUnitOnOFF(bool _switch)
        {
            heroImage.gameObject.SetActive(!_switch);  
        }
        /// <summary>
        /// shows the range from the unit on the enemy Field
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        private void PrintRangeOnField(Color color, float alpha)
        {
            for (int i = 0; i < (heroData.CurrStatBlock.Range + heroData.CurrStatModifier.RangeMod); i++)
            {
                var tempColor = GameField.Instance.Slots[2 + i, (int)LastSlot.field.y].GetComponent<Image>().color;
                tempColor = color;
                tempColor.a = alpha;
                GameField.Instance.Slots[2 + i, (int)LastSlot.field.y].GetComponent<Image>().color = tempColor;
            }
        }
       /// <summary>
       /// Updates all Unit Relevant Data
       /// </summary>
        private void UpdateUnitCard()
        {
            heroData.Unit = this;
            // add this to herodata valuechanged/modchanged
            speed.text = (heroData.CurrStatBlock.Speed + heroData.CurrStatModifier.SpeedMod).ToString();
            aT.text = (heroData.CurrStatBlock.Attack + heroData.CurrStatModifier.AttackMod).ToString();

            
            _HP.text = (heroData.CurrStatBlock.MaxHP + heroData.CurrStatModifier.MaxHPMod).ToString();
            _Speed.text = (heroData.CurrStatBlock.Speed + heroData.CurrStatModifier.SpeedMod).ToString();
            _AT.text = (heroData.CurrStatBlock.Attack + heroData.CurrStatModifier.AttackMod).ToString();
            _Range.text = (heroData.CurrStatBlock.Range + heroData.CurrStatModifier.RangeMod).ToString();
            _Name.text = heroData.name;
            _Alliance.text = heroData.AllianceName;
            _Class.text = heroData.ClassName;
            if (heroData.isMele)
            {
                meleUnitText.SetActive(true);
                
            }
            else if(!heroData.isMele)
            {
                
                rangeUnitText.SetActive(true);
            }
            
            SetStarSprite(heroData.CurrLevel);
            _Cost.text = heroData.CurrCost.ToString();
            _border.color = heroData.Rarity.BorderColor;
            heroImage.sprite = heroData.unitSprite;
            heroData.Anim = animator;
        }

        /// <summary>
        /// Shows the star Level on the hover boarder
        /// </summary>
        /// <param name="_level"> Start Level</param>
        public void SetStarSprite(int _level)
        {
            if (_level<=0 || _level >= 4)
            {
                return;
            }
            switch (_level)
            {
                case 1:
                    starImage.sprite = oneStarSprite;
                    break;
                case 2:
                    starImage.sprite = twoStarSprite;
                    break;
                case 3:
                    starImage.sprite = threeStarSprite;
                    break;
            }
            starImage.SetNativeSize();
        }

        /// <summary>
        /// Highlights the Unit 
        /// </summary>
        /// <param name="_value"> on/off</param>
        /// <param name="_color">Color of the Boarder</param>
        public void ToggleHighlight(bool _value,Color _color)
        {
            highlightImage.gameObject.SetActive(_value);
            highlightImage.color = _color;
        }
        
        /// <summary>
        /// Starts Corutin
        /// </summary>
        public void AnimateFusion()
        {
            StartCoroutine(FusionAnimation());
        }

        /// <summary>
        /// Color change when Animation Level Up Unit
        /// </summary>
        /// <param name="_color">Color of the Unit</param>
        /// <param name="_alpha">Alpha of the Unit</param>
        private void ColorHeroImage(Color _color,float _alpha = 1)
        {
            Color tempColor = _color;
            tempColor.a = _alpha;
            heroImage.color = tempColor;
        }
        /// <summary>
        /// Unit Level Up animation
        /// </summary>
        private IEnumerator FusionAnimation()
        {
            ColorHeroImage(Color.blue, 1f);
            yield return new WaitForSeconds(.1f);
            ColorHeroImage(Color.white, 1);
            yield return new WaitForSeconds(.1f);
            ColorHeroImage(Color.blue, 1f);
            yield return new WaitForSeconds(.1f);
            ColorHeroImage(Color.white, 1);
        }

        /// <summary>
        /// Change the alpha of all Units relevant fields 
        /// </summary>
        /// <param name="alpha">Alpha of the Image</param>
        private void ChangeFieldsSlectionBoarder(float alpha)
        {            
            for (int i = 0; i < 2; i++)
            {
                for (int e  = 0; e < 3; e++)
                {                    
                    Image image = GameField.Instance.Slots[i,e].GetComponent<Image>();
                    GameField.Instance.SelectetField(Color.white, alpha, image);
                }
            }

            for (int i = 0; i < GameField.Instance.Reserve.Length; i++)
            {
                Image image = GameField.Instance.Reserve[i].GetComponent<Image>();
                GameField.Instance.SelectetField(Color.white, alpha, image);
            }
        }
    }
}
