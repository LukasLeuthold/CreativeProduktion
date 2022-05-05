using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace AutoDefense
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [HideInInspector] public UnitSlot LastSlot;

        public bool isDead;
        public bool CantDragDrop;

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

        public int CurrHP;
        [SerializeField] private HeroData heroData;

        public TMP_Text damageText;

        [Header("CurrStats")]
        [HideInInspector] public bool haveSlot;
        [SerializeField] private GameObject currStats;
        [SerializeField] private Text hP;
        [SerializeField] private Text speed;
        [SerializeField] private Text aT;

        [Header("Details")]
        [SerializeField] private GameObject details;
        [SerializeField] private Text _HP;
        [SerializeField] private Text _Speed;
        [SerializeField] private Text _AT;
        [SerializeField] private Text _Range;
        [SerializeField] private Text _Name;
        [SerializeField] private Text _Cost;
        [SerializeField] private Image _border;

        [SerializeField] private Image heroImage;
        [SerializeField] private Image highlightImage;

        private bool isHiddenCard = true;
        private RectTransform rectTransform;
        private Vector2 lastRectTranform;
        private CanvasGroup canvasGroup;
        [SerializeField]private Animator animator;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            currStats.SetActive(false);
            details.SetActive(false);
            highlightImage.gameObject.SetActive(false);
            CurrHP = heroData.CurrStatBlock.MaxHP;
        }
        private void Update()
        {
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
        private void OnValidate()
        {
            if (heroData == null)
            {
                return;
            }



            UpdateUnitCard();


        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (LastSlot != null && LastSlot.isGameField)
            {
                LastSlot._HData = null;
                LastSlot._SOGameField.HDatas[LastSlot.count] = null;
                heroData.RemoveFromField(this);

            }

            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
            GameField.Instance.isGrabing = true;

            lastRectTranform = rectTransform.anchoredPosition;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CantDragDrop)
            {
                return;
            }
            
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            canvasGroup.alpha = 1f;

            GameField.Instance.isGrabing = false;
            canvasGroup.blocksRaycasts = true;
            if (!haveSlot)
            {
                if (LastSlot.isGameField)
                {
                heroData.PlaceOnField(this);
                }
                rectTransform.anchoredPosition = lastRectTranform;
            }
            haveSlot = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            currStats.SetActive(true);
            transform.SetAsLastSibling();
            if (LastSlot.isGameField)
            {
                PrintRangeOnField(Color.green);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            currStats.SetActive(false);
            details.SetActive(false);
            if (LastSlot.isGameField)
            {
                PrintRangeOnField(Color.white);
            }
            isHiddenCard = true;

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!isHiddenCard)
                {
                    isHiddenCard = true;
                    details.SetActive(false);
                    currStats.SetActive(true);
                }
                else if (isHiddenCard)
                {
                    isHiddenCard = false;
                    details.SetActive(true);
                    currStats.SetActive(false);
                }
            }
        }

        public void SwitchUnitOnOFF(bool _switch)
        {
            heroImage.gameObject.SetActive(!_switch);  
        }
        private void PrintRangeOnField(Color color)
        {
            for (int i = 0; i < (heroData.CurrStatBlock.Range + heroData.CurrStatModifier.RangeMod); i++)
            {
                var tempColor = GameField.Instance.Slots[2 + i, (int)LastSlot.field.y].GetComponent<Image>().color;
                tempColor = color;
                tempColor.a = 0.25f;
                GameField.Instance.Slots[2 + i, (int)LastSlot.field.y].GetComponent<Image>().color = tempColor;
            }
        }
        private void UpdateUnitCard()
        {
            // add this to herodata valuechanged/modchanged
            hP.text = (heroData.CurrStatBlock.MaxHP + heroData.CurrStatModifier.MaxHPMod).ToString();
            speed.text = (heroData.CurrStatBlock.Speed + heroData.CurrStatModifier.SpeedMod).ToString();
            aT.text = (heroData.CurrStatBlock.Attack + heroData.CurrStatModifier.AttackMod).ToString();

            _HP.text = (heroData.CurrStatBlock.MaxHP + heroData.CurrStatModifier.MaxHPMod).ToString();
            _Speed.text = (heroData.CurrStatBlock.Speed + heroData.CurrStatModifier.SpeedMod).ToString();
            _AT.text = (heroData.CurrStatBlock.Attack + heroData.CurrStatModifier.AttackMod).ToString();
            _Range.text = (heroData.CurrStatBlock.Range + heroData.CurrStatModifier.RangeMod).ToString();
            _Name.text = heroData.name;
            //changed cost
            _Cost.text = heroData.CurrCost.ToString();
            _border.color = heroData.Rarity.BorderColor;
            heroImage.sprite = heroData.unitSprite;
            heroData.Anim = animator;
            heroData.Unit = this;
        }

        public void ToggleHighlight(bool _value,Color _color)
        {
            highlightImage.gameObject.SetActive(_value);
            highlightImage.color = _color;
        }
        public void AnimateFusion()
        {
            StartCoroutine(FusionAnimation());
        }

        private void ColorHeroImage(Color _color,float _alpha = 1)
        {
            Color tempColor = _color;
            tempColor.a = _alpha;
            heroImage.color = tempColor;
        }

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
    }
}
