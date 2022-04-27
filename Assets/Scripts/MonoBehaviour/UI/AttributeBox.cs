using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class AttributeBox : MonoBehaviour
    {
        [SerializeField] private List<HeroCollection> heroCollectionsInGame;
        Dictionary<HeroCollection, AttributeVisualManager> dicAttributeBox;
        public AttributeVisualManager[] attributeVisuals = new AttributeVisualManager[12];

        private List<HeroCollection> activeHeroCollections;

        [Header("Color")]
        [SerializeField]private Color activeAttributeColor;
        [SerializeField]private Color inActiveAttributeColor;
        //GameObject attributeVisual;

        private void Start()
        {
            dicAttributeBox = new Dictionary<HeroCollection, AttributeVisualManager>();
            activeHeroCollections = new List<HeroCollection>();
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced += SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved += DeleteAttribute;
                heroCollectionsInGame[i].OnDiversityChanged += SetAttributeCount;
                heroCollectionsInGame[i].OnBuffStatusChanged += SetAttributeStatus;
            }
            ResetAttributeVisuals();
        }
        private void OnDisable()
        {
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced -= SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved -= DeleteAttribute;
                heroCollectionsInGame[i].OnDiversityChanged -= SetAttributeCount;
                heroCollectionsInGame[i].OnBuffStatusChanged -= SetAttributeStatus;
            }
        }

        private void SpawnNewAttribute(HeroCollection _collection)
        {
            activeHeroCollections.Add(_collection);
            UpdateAttributeBox();
            //for (int i = 0; i < attributeVisuals.Length; i++)
            //{
            //    if (attributeVisuals[i].gameObject.activeSelf == false)
            //    {
            //        Debug.Log("spawn new Attribute: " + _collection.name);
            //        UpdateAttributeVisual(attributeVisuals[i],_collection);
            //        attributeBox.Add(_collection, attributeVisuals[i]);
            //        attributeVisuals[i].gameObject.SetActive(true);
            //        return;
            //    }
            //}
        }

        private void DeleteAttribute(HeroCollection _collection)
        {
            Debug.Log("delete Attribute: " + _collection.name);
            activeHeroCollections.Remove(_collection);
            UpdateAttributeBox();

            //dicAttributeBox[_collection].gameObject.SetActive(false);
            //UpdateAttributeBox();
            //dicAttributeBox.Remove(_collection);
            //TODO: shuffle them up
        }

        private void UpdateAttributeBox()
        {
            dicAttributeBox = new Dictionary<HeroCollection, AttributeVisualManager>();
            ResetAttributeVisuals();
            for (int i = 0; i < activeHeroCollections.Count; i++)
            {
                SetEmptyAttributeVisual(activeHeroCollections[i]);
                SetAttributeStatus(activeHeroCollections[i]);
            }
        }

        private void SetAttributeStatus(HeroCollection _collection)
        {
            if (_collection.CurrEffect == null)
            {
                dicAttributeBox[_collection].TextColor = inActiveAttributeColor;
            }
            else
            {
                dicAttributeBox[_collection].TextColor = activeAttributeColor;
            }

        }
        private void SetAttributeCount(HeroCollection _collection)
        {
            UpdateAttributeVisual(dicAttributeBox[_collection], _collection);
        }
        private void UpdateAttributeVisual(AttributeVisualManager _attributeVisualManager, HeroCollection _collection)
        {
            _attributeVisualManager.DiversityText = _collection.Diversity.ToString();
            _attributeVisualManager.NameText = _collection.name;
            _attributeVisualManager.ToolTipText = _collection.name+ " has " + _collection.Diversity.ToString() + " Heroes active.";
        }

        private void SetEmptyAttributeVisual(HeroCollection _collection)
        {
            for (int j = 0; j < attributeVisuals.Length; j++)
            {
                if (attributeVisuals[j].gameObject.activeSelf == false)
                {
                    dicAttributeBox.Add(_collection, attributeVisuals[j]);
                    attributeVisuals[j].gameObject.SetActive(true);
                    UpdateAttributeVisual(attributeVisuals[j], _collection);
                    return;
                }
            }
        }
        private void ResetAttributeVisuals()
        {
            for (int j = 0; j < attributeVisuals.Length; j++)
            {
                attributeVisuals[j].TextColor = inActiveAttributeColor;
                attributeVisuals[j].gameObject.SetActive(false);
            }
        }
    }
}
