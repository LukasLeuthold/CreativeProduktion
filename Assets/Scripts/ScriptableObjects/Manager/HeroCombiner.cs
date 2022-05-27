using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles logic of combining units with another to level up
    /// </summary>
    [CreateAssetMenu(fileName = "new HerCombiner", menuName = "Manager/HeroCombiner")]
    public class HeroCombiner : InitScriptObject
    {
        /// <summary>
        /// dictionary of hero prefabs currently in play and reserve
        /// </summary>
        Dictionary<int, Dictionary<string, List<DragDrop>>> heroPrefabsByLevel;
        /// <summary>
        /// amount of same heroes needed to level up
        /// </summary>
        [SerializeField,Min(2)] private int amountToCombine;
        /// <summary>
        /// maximum level the units can reach
        /// </summary>
        [SerializeField] private int maxLevel;
        /// <summary>
        /// heroprefab
        /// </summary>
        [SerializeField] private GameObject heroPrefab;
        /// <summary>
        /// gets called when a unit levels up
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent onHeroLevelUp;
        /// <summary>
        /// amount of same heroes needed to level up
        /// </summary>
        public int AmountToCombine
        {
            get
            {
                return amountToCombine;
            }
            set
            {
                amountToCombine = value;
            }
        }
        /// <summary>
        /// add a heroprefab to the dictionary or combines if there are enough
        /// </summary>
        /// <param name="_heroPrefab">heroprefab to add to dictionary</param>
        public void AddHeroPrefab(DragDrop _heroPrefab)
        {
            if (heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].ContainsKey(_heroPrefab.HData.name))
            {
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Add(_heroPrefab);
                if (heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Count >= amountToCombine)
                {
                    Transform parent;
                    UnitSlot spawnSlot = FindSpawnSlot(heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name], out parent);
                    HeroData heroData = _heroPrefab.HData.GetCopy();
                    heroData.CurrLevel = _heroPrefab.HData.CurrLevel + 1;

                    for (int i = 0; i < heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Count; i++)
                    {
                        RemoveFromGame(heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name][i]);
                    }
                    heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].Remove(_heroPrefab.HData.name);

                    DragDrop _cloneDrop = SpawnPrefab(spawnSlot, heroData, parent);
                    _cloneDrop.AnimateFusion();
                    AddHeroPrefab(_cloneDrop);
                }
            }
            else
            {
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].Add(_heroPrefab.HData.name, new List<DragDrop>());
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Add(_heroPrefab);
            }
        }
        /// <summary>
        /// remove a heroprefab from the dictionary
        /// </summary>
        /// <param name="_heroPrefab">heroprefab to remove from the dictionary</param>
        public void RemoveHeroPrefab(DragDrop _heroPrefab)
        {
            heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Remove(_heroPrefab);
            if (heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Count <= 0)
            {
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].Remove(_heroPrefab.HData.name);
            }
        }

        /// <summary>
        /// finds the best spawnslot fro the combined unit
        /// </summary>
        /// <param name="_dragDrops">list of heroes that get combined</param>
        /// <param name="_parent">transform of the unit parent gameobject</param>
        /// <returns></returns>
        private UnitSlot FindSpawnSlot(List<DragDrop> _dragDrops, out Transform _parent)
        {
            for (int i = 0; i < _dragDrops.Count; i++)
            {
                if (_dragDrops[i].LastSlot.isGameField)
                {
                    _parent = _dragDrops[i].LastSlot.Unit.transform.parent;
                    return _dragDrops[i].LastSlot;
                }
            }
            _parent = _dragDrops[0].LastSlot.Unit.transform.parent;
            return _dragDrops[0].LastSlot;
        }
        /// <summary>
        /// spawns a new prefab with the combined leveled up version of the herodata
        /// </summary>
        /// <param name="_spawnSlot">slot the unit is spawned in</param>
        /// <param name="_heroData">herodata of the hero</param>
        /// <param name="_parent">transform of the unit parent gameobject</param>
        /// <returns></returns>
        private DragDrop SpawnPrefab(UnitSlot _spawnSlot, HeroData _heroData, Transform _parent)
        {
            GameObject clone = Instantiate(heroPrefab);
            clone.transform.SetParent(_parent);
            clone.GetComponent<RectTransform>().anchoredPosition = _spawnSlot.GetComponent<RectTransform>().anchoredPosition;
            clone.GetComponent<DragDrop>().LastSlot = _spawnSlot;
            clone.GetComponent<DragDrop>().HData = _heroData;
            if (_spawnSlot.isGameField)
            {
                clone.GetComponent<DragDrop>().HData.PlaceOnField(clone.GetComponent<DragDrop>());
            }
            _spawnSlot.Unit = clone.GetComponent<DragDrop>();
            _spawnSlot._HData = _heroData;
            onHeroLevelUp.Raise();
            return clone.GetComponent<DragDrop>();
        }
        /// <summary>
        /// removes the hero object from the game
        /// </summary>
        /// <param name="_heroPrefab">hero object to remove from game</param>
        private void RemoveFromGame(DragDrop _heroPrefab)
        {
            if (_heroPrefab.LastSlot.isGameField)
            {
                _heroPrefab.HData.RemoveFromField(_heroPrefab);
            }
            _heroPrefab.LastSlot._HData = null;
            _heroPrefab.LastSlot.Unit = null;
            _heroPrefab.haveSlot = true;
            Destroy(_heroPrefab.gameObject);
        }
        /// <summary>
        /// resets to default values
        /// </summary>
        public override void Initialize()
        {
            heroPrefabsByLevel = new Dictionary<int, Dictionary<string, List<DragDrop>>>
            {
                {1,new Dictionary<string, List<DragDrop>>() },
                {2,new Dictionary<string, List<DragDrop>>() },
                {3,new Dictionary<string, List<DragDrop>>() }
            };
        }
    }
}
