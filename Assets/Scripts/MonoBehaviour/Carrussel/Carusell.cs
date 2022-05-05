using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class Carusell : MonoBehaviour
    {
        [SerializeField] private HeroPool hPool;
        [SerializeField] private HeroCard[] hCard = new HeroCard[5];
        [SerializeField] private int rerollCost;
        [SerializeField] private int xpBuyCost;
        [SerializeField] private int xpBuyValue;
        [SerializeField] private PlayerRessources playerRessources;

        [Header("UI Elements")]
        [SerializeField] private Button rerollButton;
        [SerializeField] private Text rerollText;
        [SerializeField] private Button xpButton;
        [SerializeField] private Text xpBuyText;


        private void Start()
        {
            hPool.InitDictionaries();
            rerollText.text = "Refresh (" + rerollCost + ")"; 
            xpBuyText.text = "Buy Xp (" + xpBuyCost + ")"; 
            GetNewCarusell();
        }
        private void GetNewCarusell()
        {
            HeroData[]hData = hPool.GetLineUp(hCard.Length, playerRessources.CurrProbability);
            for (int i = 0; i < hCard.Length; i++)
            {
                hCard[i].HeroData = hData[i];
            }
        }

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

        public void SetXpBuyable(int _value)
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
        public void BuyXp()
        {
            playerRessources.CurrXP+=xpBuyValue;
            playerRessources.PlayerMoney -= xpBuyCost;
        }
    }

}
