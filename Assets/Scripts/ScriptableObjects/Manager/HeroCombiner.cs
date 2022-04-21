using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HerCombiner", menuName = "Manager/HeroCombiner")]
    public class HeroCombiner : InitScriptObject
    {
        Dictionary<int, Dictionary<string, List<DragDrop>>> heroPrefabsByLevel;
        [SerializeField] private int amountToCombine;
        [SerializeField] private int maxLevel;
        [SerializeField] private GameObject heroPrefab;

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
                    AddHeroPrefab(_cloneDrop);
                }
            }
            else
            {
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].Add(_heroPrefab.HData.name, new List<DragDrop>());
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Add(_heroPrefab);
            }
        }
        public void RemoveHeroPrefab(DragDrop _heroPrefab)
        {
            heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Remove(_heroPrefab);
            if (heroPrefabsByLevel[_heroPrefab.HData.CurrLevel][_heroPrefab.HData.name].Count <= 0)
            {
                heroPrefabsByLevel[_heroPrefab.HData.CurrLevel].Remove(_heroPrefab.HData.name);
            }
        }

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

        private DragDrop SpawnPrefab(UnitSlot _spawnSlot, HeroData _heroData, Transform _parent)
        {
            GameObject clone = Instantiate(heroPrefab);
            clone.transform.SetParent(_parent);
            clone.GetComponent<RectTransform>().anchoredPosition = _spawnSlot.GetComponent<RectTransform>().anchoredPosition;
            clone.GetComponent<DragDrop>().LastSlot = _spawnSlot;
            clone.GetComponent<DragDrop>().HData = _heroData;
            if (_spawnSlot.isGameField)
            {
                clone.GetComponent<DragDrop>().HData.PlaceOnField();
                GameField.Instance.sOGameField.HDatas[_spawnSlot.count] = clone.GetComponent<DragDrop>().HData;
            }
            _spawnSlot.Unit = clone.GetComponent<DragDrop>();
            _spawnSlot._HData = _heroData;
            return clone.GetComponent<DragDrop>();
        }
        private void RemoveFromGame(DragDrop _heroPrefab)
        {
            if (_heroPrefab.LastSlot.isGameField)
            {
                _heroPrefab.HData.RemoveFromField();
            }
            _heroPrefab.LastSlot._HData = null;
            _heroPrefab.LastSlot.Unit = null;
            _heroPrefab.haveSlot = true;
            GameField.Instance.sOGameField.HDatas[_heroPrefab.LastSlot.count] = null;
            Destroy(_heroPrefab.gameObject);
        }

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
