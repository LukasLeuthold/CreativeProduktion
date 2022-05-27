using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles hero pool logic
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroPool", menuName = "Hero/HeroPool", order = 1)]
    public class HeroPool : ScriptableObject
    {
        /// <summary>
        /// array of common heroes
        /// </summary>
        [SerializeField] private PoolUnit[] commonHeros;
        /// <summary>
        /// array of rare heroes
        /// </summary>
        [SerializeField] private PoolUnit[] rareHeros;
        /// <summary>
        /// array of lord heroes
        /// </summary>
        [SerializeField] private PoolUnit[] lordHeros;
        /// <summary>
        /// dictionary of all heroes
        /// </summary>
        private Dictionary<string, HeroDataCount> allHeros = new Dictionary<string, HeroDataCount>();
        /// <summary>
        /// dictionary of common heroes
        /// </summary>
        private Dictionary<string, HeroDataCount> dicCommonHeros = new Dictionary<string, HeroDataCount>();
        /// <summary>
        /// dictionary of rare heroes
        /// </summary>
        private Dictionary<string, HeroDataCount> dicRareHeros = new Dictionary<string, HeroDataCount>();
        /// <summary>
        /// dictionary of lord heroes
        /// </summary>
        private Dictionary<string, HeroDataCount> dicLordHeros = new Dictionary<string, HeroDataCount>();
        /// <summary>
        /// list of possible heroes
        /// </summary>
        List<HeroData> possibleChosenHero;
        /// <summary>
        /// returns an array of random heroes using a custom probability distribution
        /// </summary>
        /// <param name="_amount">amount of units in the array</param>
        /// <param name="_probability">custom probability distribution</param>
        /// <returns></returns>
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

                }
                int number2 = UtilRandom.GetRandomIntFromRange(0, possibleChosenHero.Count);
                chosenHero = possibleChosenHero[number2].GetCopy();
                SubtractUnitCount(chosenHero.name, 1);
                lineUp[i] = chosenHero;
            }
            return lineUp;
        }
        /// <summary>
        /// adds a unit amount to the pool
        /// </summary>
        /// <param name="_heroName">hero that gets added</param>
        /// <param name="_amount">amount added</param>
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
        /// <summary>
        /// subtracts a unit amount from the pool
        /// </summary>
        /// <param name="_heroName">hero that gets removed</param>
        /// <param name="_amount">amount removed</param>
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
        /// <summary>
        /// validates entries and refreshes pool
        /// </summary>
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
        /// <summary>
        /// initializes the dictionaries from the arrays
        /// </summary>
        public void InitDictionaries()
        {
            dicCommonHeros.Clear();
            dicRareHeros.Clear();
            dicLordHeros.Clear();
            allHeros.Clear();
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
        /// <summary>
        /// initializes all the unitdatas in the arrays
        /// </summary>
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
        /// <summary>
        /// refreshes the serialization of the array
        /// </summary>
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

    /// <summary>
    /// struct to hold info about unittype and amount
    /// </summary>
    [System.Serializable]
    public struct PoolUnit
    {
        /// <summary>
        /// name of the unit
        /// </summary>
        [HideInInspector] public string name;
        /// <summary>
        /// data object of the unit
        /// </summary>
        public HeroData unitData;
        /// <summary>
        /// amount of the unit in the pool
        /// </summary>
        [Min(1)] public int amount;
        /// <summary>
        /// max amount of the unit in the pool
        /// </summary>
        [Min(1)] public int maxAmount;
        /// <summary>
        /// initializes poolunit; Sets name; and amount to starting value
        /// </summary>
        public void InitPoolUnit()
        {
            if (unitData != null)
            {
                this.name = unitData.name;
                amount = maxAmount;
            }
        }
    }

    /// <summary>
    /// class to hold info about the amount of a specific herodata
    /// </summary>
    public class HeroDataCount
    {
        /// <summary>
        /// ctor of the HeroDataCount class
        /// </summary>
        /// <param name="_heroData">herodata thats used in the class</param>
        /// <param name="_count">amount of the specific herodata</param>
        public HeroDataCount(HeroData _heroData, int _count)
        {
            heroData = _heroData;
            count = _count;
        }

        /// <summary>
        /// herodata this object is storing info about
        /// </summary>
        public HeroData heroData;
        /// <summary>
        /// current amoutn
        /// </summary>
        public int count;
        /// <summary>
        /// add amount to the current amount
        /// </summary>
        /// <param name="_add">amount to add</param>
        public void AddCount(int _add)
        {
            count += _add;
        }
        /// <summary>
        /// remove amount from the current amount
        /// </summary>
        /// <param name="_add">amount to remove</param>
        public void SubCount(int _sub)
        {
            count -= _sub;
        }
    }
}
