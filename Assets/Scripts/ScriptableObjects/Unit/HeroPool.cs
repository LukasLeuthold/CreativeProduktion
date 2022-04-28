using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HeroPool", menuName = "ScriptableUnitPool/HeroPool", order = 1)]
    public class HeroPool : ScriptableObject
    {
        [SerializeField] private PoolUnit[] commonHeros;
        [SerializeField] private PoolUnit[] rareHeros;
        [SerializeField] private PoolUnit[] lordHeros;
        private Dictionary<string, HeroDataCount> allHeros = new Dictionary<string, HeroDataCount>();
        private Dictionary<string, HeroDataCount> dicCommonHeros = new Dictionary<string, HeroDataCount>();
        private Dictionary<string, HeroDataCount> dicRareHeros = new Dictionary<string, HeroDataCount>();
        private Dictionary<string, HeroDataCount> dicLordHeros = new Dictionary<string, HeroDataCount>();

        List<HeroData> possibleChosenHero;

        //TODO: test only delete before release
        public ProbabilityDistribution Prop;
        //
        public HeroData[] GetLineUp(int _amount, ProbabilityDistribution _probability)
        {
            HeroData[] lineUp = new HeroData[_amount];
            bool commonUnitEmpty = false;
            bool rareUnitempty = false;
            bool lordUnitempty = false;
            for (int i = 0; i < lineUp.Length; i++)
            {

                HeroData chosenHero = null;
                possibleChosenHero = new List<HeroData>();
                int value = UtilRandom.GetRandomIntFromRange(0, 100);
                //do
                //{
                if (!commonUnitEmpty && value <= _probability.probabilityCommon)
                {
                    foreach (KeyValuePair<string, HeroDataCount> dicEntry in dicCommonHeros)
                    {
                        if (dicEntry.Value.count > 0)
                        {
                            possibleChosenHero.Add(dicEntry.Value.heroData);
                        }
                    }
                    if (possibleChosenHero.Count == 0)
                    {
                        commonUnitEmpty = true;
                    }
                }
                else if (!rareUnitempty && value <= _probability.probabilityCommon + _probability.probabilityRare)
                {
                    foreach (KeyValuePair<string, HeroDataCount> dicEntry in dicRareHeros)
                    {
                        if (dicEntry.Value.count > 0)
                        {
                            possibleChosenHero.Add(dicEntry.Value.heroData);
                        }
                    }
                    if (possibleChosenHero.Count == 0)
                    {
                        rareUnitempty = true;
                    }
                }
                else if (!lordUnitempty || value <= _probability.probabilityCommon + _probability.probabilityRare + _probability.probabilityLord)
                {
                    foreach (KeyValuePair<string, HeroDataCount> dicEntry in dicLordHeros)
                    {
                        if (dicEntry.Value.count > 0)
                        {
                            possibleChosenHero.Add(dicEntry.Value.heroData);
                        }
                    }
                    if (possibleChosenHero.Count == 0)
                    {
                        lordUnitempty = true;
                    }

                    //int number = UtilRandom.GetRandomIntFromRange(0, dicLordHeros.Count);
                    //    chosenHero = (HeroData)dicLordHeros.ElementAt(number).Value.heroData.GetCopy();
                }
                //} while (possibleChosenHero.Count <= 0);
                int number2 = UtilRandom.GetRandomIntFromRange(0, possibleChosenHero.Count);
                chosenHero = possibleChosenHero[number2].GetCopy();
                SubtractUnitCount(chosenHero.name, 1);
                lineUp[i] = chosenHero;
            }
            //Debug.Log("lineUp: ");
            //for (int i = 0; i < lineUp.Length; i++)
            //{
            //    if (lineUp[i] == null)
            //    {
            //        Debug.Log("Unit null");
            //        continue;
            //    }
            //    //Debug.Log("Unit: " + lineUp[i]);
            //}
            return lineUp;
        }
        public void AddUnitCount(string _heroName, int _amount = 1)
        {
            switch (allHeros[_heroName].heroData.Rarity.name)
            {
                case "Common":
                    dicCommonHeros[_heroName].AddCount(_amount);
                    break;
                case "Rare":
                    dicRareHeros[_heroName].AddCount(_amount);
                    break;
                case "Lord":
                    dicLordHeros[_heroName].AddCount(_amount);
                    break;
                default:
                    break;
            }
            RefreshUnitData();
        }
        public void SubtractUnitCount(string _heroName, int _amount = 1)
        {
            switch (allHeros[_heroName].heroData.Rarity.name)
            {
                case "Common":
                    dicCommonHeros[_heroName].SubCount(_amount);
                    break;
                case "Rare":
                    dicRareHeros[_heroName].SubCount(_amount);
                    break;
                case "Lord":
                    dicLordHeros[_heroName].SubCount(_amount);
                    break;
                default:
                    break;
            }
            RefreshUnitData();
        }

        private void OnValidate()
        {
            for (int i = 0; i < commonHeros.Length; i++)
            {
                if (commonHeros[i].unitData != null)
                    commonHeros[i].unitData.OnUnitDataChanged += InitUnitData;
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                if (rareHeros[i].unitData != null)
                    rareHeros[i].unitData.OnUnitDataChanged += InitUnitData;
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                if (lordHeros[i].unitData != null)
                    lordHeros[i].unitData.OnUnitDataChanged += InitUnitData;
            }
            InitUnitData();

        }

        public void InitDictionaries()
        {
            dicCommonHeros.Clear();
            dicRareHeros.Clear();
            dicLordHeros.Clear();
            InitUnitData();
            for (int i = 0; i < commonHeros.Length; i++)
            {
                dicCommonHeros.Add(commonHeros[i].name, new HeroDataCount(commonHeros[i].unitData, commonHeros[i].amount));
                allHeros.Add(commonHeros[i].name, new HeroDataCount(commonHeros[i].unitData, commonHeros[i].amount));
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                dicRareHeros.Add(rareHeros[i].name, new HeroDataCount(rareHeros[i].unitData, rareHeros[i].amount));
                allHeros.Add(rareHeros[i].name, new HeroDataCount(rareHeros[i].unitData, rareHeros[i].amount));
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                dicLordHeros.Add(lordHeros[i].name, new HeroDataCount(lordHeros[i].unitData, lordHeros[i].amount));
                allHeros.Add(lordHeros[i].name, new HeroDataCount(lordHeros[i].unitData, lordHeros[i].amount));
            }
        }

        private void InitUnitData()
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
        private void RefreshUnitData()
        {
            for (int i = 0; i < commonHeros.Length; i++)
            {
                commonHeros[i].amount = dicCommonHeros[commonHeros[i].name].count;
            }
            for (int i = 0; i < rareHeros.Length; i++)
            {
                rareHeros[i].amount = dicRareHeros[rareHeros[i].name].count;
            }
            for (int i = 0; i < lordHeros.Length; i++)
            {
                lordHeros[i].amount = dicLordHeros[lordHeros[i].name].count;
            }
        }

    }

    [System.Serializable]
    public struct PoolUnit
    {
        [HideInInspector] public string name;
        public HeroData unitData;
        [Min(1)] public int amount;
        [Min(1)] public int maxAmount;

        public void InitPoolUnit()
        {
            if (unitData != null)
            {
                this.name = unitData.name;
                amount = maxAmount;
            }
        }
    }
    public class HeroDataCount
    {
        public HeroDataCount(HeroData _heroData, int _count)
        {
            heroData = _heroData;
            count = _count;
        }
        public HeroData heroData;
        public int count;
        public void AddCount(int _add)
        {
            count += _add;
        }
        public void SubCount(int _sub)
        {
            count -= _sub;
        }
    }
}
