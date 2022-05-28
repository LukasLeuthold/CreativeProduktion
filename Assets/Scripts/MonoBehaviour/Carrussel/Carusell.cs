using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// handles logic for rolling random heroes and displaying the result
    /// </summary>
    public class Carusell : MonoBehaviour
    {
        /// <summary>
        /// heropool the carousel gets the units from
        /// </summary>
        [SerializeField] private HeroPool hPool;
        /// <summary>
        /// herocard array to display and buy the shop inventory
        /// </summary>
        [SerializeField] private HeroCard[] hCard = new HeroCard[5];
        /// <summary>
        /// cost of rerolling the shop
        /// </summary>
        [SerializeField] private int rerollCost;
        /// <summary>
        /// cost to buy xp
        /// </summary>
        [SerializeField] private int xpBuyCost;
        /// <summary>
        /// amount of xp bought per buy
        /// </summary>
        [SerializeField] private int xpBuyValue;
        /// <summary>
        /// the player ressources
        /// </summary>
        [SerializeField] private PlayerRessources playerRessources;
        /// <summary>
        /// flag if player is maxlevel or not
        /// </summary>
        bool isMaxLevel = false;
        /// <summary>
        /// gameobject of the shop to turn on and off
        /// </summary>
        [SerializeField] private GameObject shop;

        /// <summary>
        /// reroll button
        /// </summary>
        [Header("UI Elements")]
        [SerializeField] private Button rerollButton;
        /// <summary>
        /// text of the reroll button
        /// </summary>
        [SerializeField] private Text rerollText;
        /// <summary>
        /// xp button
        /// </summary>
        [SerializeField] private Button xpButton;
        /// <summary>
        /// text of the xp button
        /// </summary>
        [SerializeField] private Text xpBuyText;

        /// <summary>
        /// sets default values 
        /// </summary>
        private void Start()
        {
            isMaxLevel = false;
            hPool.InitDictionaries();
            rerollText.text = "Refresh (" + rerollCost + ")";
            xpBuyText.text = "Buy Xp (" + xpBuyCost + ")";
            GetNewCarusell();
        }
        /// <summary>
        /// refreshes the shop
        /// </summary>
        private void GetNewCarusell()
        {
            HeroData[] hData = hPool.GetLineUp(hCard.Length, playerRessources.CurrProbability);
            for (int i = 0; i < hCard.Length; i++)
            {
                hCard[i].HeroData = hData[i];
            }
        }

        /// <summary>
        /// evaluates if player has enough money to reroll
        /// </summary>
        /// <param name="_value">money the player has</param>
        public void SetRerollable(int _value)
        {

            if (rerollCost <= _value)
            {
                rerollButton.interactable = true;
            }
            else if (rerollCost > _value)
            {
                rerollButton.interactable = false;
            }
        }
        /// <summary>
        /// evaluates if player has enough money to buy xp
        /// </summary>
        /// <param name="_value">money the player has</param>
        public void SetXpBuyable(int _value)
        {
            if (!isMaxLevel)
            {
                if (xpBuyCost <= _value)
                {
                    xpButton.interactable = true;
                }
                else if (xpBuyCost > _value)
                {
                    xpButton.interactable = false;
                }
            }
        }
        /// <summary>
        /// evaluates if player can buy xp or if he is maxlevel
        /// </summary>
        /// <param name="_value"></param>
        public void SetXpButtonInteractable(int _value)
        {
            if (_value < playerRessources.MaxLevel)
            {
                xpButton.interactable = true;
                isMaxLevel = false;
            }
            else
            {
                xpButton.interactable = false;
                isMaxLevel = true;
                xpBuyText.text = "MAX";
            }
        }
        /// <summary>
        /// refreshes the shop for free
        /// </summary>
        public void FreeRoll()
        {
            for (int i = 0; i < hCard.Length; i++)
            {
                if (hCard[i].card.activeSelf)
                {
                    hPool.AddUnitCount(hCard[i].HeroData.name, 1);
                }
                hCard[i].card.SetActive(true);
            }
            GetNewCarusell();
        }
        /// <summary>
        /// refreshes the shop for reroll cost
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < hCard.Length; i++)
            {
                if (hCard[i].card.activeSelf)
                {
                    hPool.AddUnitCount(hCard[i].HeroData.name, 1);
                }
                hCard[i].card.SetActive(true);
            }
            playerRessources.PlayerMoney -= rerollCost;
            GetNewCarusell();
        }
        /// <summary>
        /// buys xp from the shop
        /// </summary>
        public void BuyXp()
        {
            playerRessources.CurrXP += xpBuyValue;
            playerRessources.PlayerMoney -= xpBuyCost;
        }
       /// <summary>
       /// de/activates the shop
       /// </summary>
       /// <param name="_isOpen">if the shop is open or not</param>
        public void SetShop(bool _isOpen)
        {
            shop.SetActive(_isOpen);
        }
    }

}
