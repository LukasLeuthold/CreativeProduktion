using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HeroPool", menuName = "ScriptableUnitPool/HeroPool", order = 1)]
    public class HeroPool : ScriptableObject
    {
        [SerializeField] private PoolUnit[] commonHeros;
        [SerializeField] private PoolUnit[] rareHeros;
        [SerializeField] private PoolUnit[] lordHeros;
        private Dictionary<HeroData, int> dicCommonHeros = new Dictionary<HeroData, int>();
        private Dictionary<HeroData, int> dicRareHeros = new Dictionary<HeroData, int>();
        private Dictionary<HeroData, int> dicLordHeros = new Dictionary<HeroData, int>();

        //TODO: test only delete before release
        public ProbabilityDistribution Prop;
        //
        public HeroData[] GetLineUp(int _amount, ProbabilityDistribution _probability)
        {
            HeroData[] lineUp = new HeroData[_amount];
            for (int i = 0; i < lineUp.Length; i++)
            {
                HeroData chosenHero = null;
                int value = UtilRandom.GetRandomIntFromRange(0, 100);
                if (value <= _probability.probabilityCommon)
                {
                    int number = UtilRandom.GetRandomIntFromRange(0, dicCommonHeros.Count);
                    chosenHero = (HeroData)dicCommonHeros.ElementAt(number).Key.GetCopy();
                }
                else if(value <= (_probability.probabilityCommon +_probability.probabilityRare))
                {
                    int number = UtilRandom.GetRandomIntFromRange(0, dicRareHeros.Count);
                    chosenHero = (HeroData)dicRareHeros.ElementAt(number).Key.GetCopy();
                }
                else if(value <= (_probability.probabilityCommon + _probability.probabilityRare +_probability.probabilityLord))
                {
                    int number = UtilRandom.GetRandomIntFromRange(0, dicLordHeros.Count);
                    chosenHero = (HeroData)dicLordHeros.ElementAt(number).Key.GetCopy();
                }
                lineUp[i] = chosenHero;
            }
            Debug.Log("lineUp: ");
            for (int i = 0; i < lineUp.Length; i++)
            {
                if (lineUp[i] == null)
                {
                    Debug.Log("Unit null");
                    continue;
                }
                Debug.Log("Unit: " + lineUp[i]);
            }
            return lineUp;
        }
        public void AddUnitCount(HeroData _heroData, int _amount = 1)
        {
            switch (_heroData.Rarity.name)
            {
                case "Common":
                    dicCommonHeros[_heroData]+=_amount;
                    break;
                case "Rare":
                    dicRareHeros[_heroData] += _amount;
                    break;
                case "Lord":
                    dicLordHeros[_heroData] += _amount;
                    break;
                default:
                    break;
            }
        }
        public void SubtractUnitCount(HeroData _heroData, int _amount = 1)
        {
            switch (_heroData.Rarity.name)
            {
                case "Common":
                    dicCommonHeros[_heroData] -= _amount;
                    break;
                case "Rare":
                    dicRareHeros[_heroData] -= _amount;
                    break;
                case "Lord":
                    dicLordHeros[_heroData] -= _amount;
                    break;
                default:
                    break;
            }
        }

        private void OnValidate()
        {
            for (int i = 0; i < commonHeros.Length; i++)
            {
                if (commonHeros[i].unitData != null)
                    commonHeros[i].unitData.OnUnitDataChanged += RefreshUnitData;
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                if (rareHeros[i].unitData != null)
                    rareHeros[i].unitData.OnUnitDataChanged += RefreshUnitData;
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                if (lordHeros[i].unitData != null)
                    lordHeros[i].unitData.OnUnitDataChanged += RefreshUnitData;
            }
            RefreshUnitData();
            InitDictionaries();
           
        }

        private void InitDictionaries()
        {
            dicCommonHeros.Clear();
            dicRareHeros.Clear();
            dicLordHeros.Clear();
            for (int i = 0; i < commonHeros.Length; i++)
            {
                dicCommonHeros.Add((HeroData)commonHeros[i].unitData, commonHeros[i].amount);
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                dicRareHeros.Add((HeroData)rareHeros[i].unitData, rareHeros[i].amount);
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                dicLordHeros.Add((HeroData)lordHeros[i].unitData, lordHeros[i].amount);
            }
        }

        private void RefreshUnitData()
        {
            for (int i = 0; i < commonHeros.Length; i++)
            {
                commonHeros[i].InitPoolUnit();
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                rareHeros[i].InitPoolUnit();
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                lordHeros[i].InitPoolUnit();
            }
        }

    }

    [System.Serializable]
    public struct PoolUnit
    {
        [HideInInspector] public string name;
        //TODO: refactor
        public UnitData unitData;
        public int amount;

        public void InitPoolUnit()
        {
            if (unitData != null)
            {
                this.name = unitData.name;
            }
        }
    }
}
