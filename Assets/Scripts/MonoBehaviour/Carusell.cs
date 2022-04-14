using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class Carusell : MonoBehaviour
    {
        [SerializeField] private HeroPool hPool;
        [SerializeField] private HeroCard[] hCard = new HeroCard[5];

        private void Start()
        {
            hPool.InitDictionaries();
            GetNewCarusell();
        }
        private void GetNewCarusell()
        {
            HeroData[]hData = hPool.GetLineUp(hCard.Length, hPool.Prop);

            for (int i = 0; i < hCard.Length; i++)
            {
                hCard[i].HeroData = hData[i];
            }
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
            GetNewCarusell();
        }
    }

}
