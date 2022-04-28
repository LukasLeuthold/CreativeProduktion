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

        public static Action<HeroCollection> OnTurnOnHighlight;
        public static Action<HeroCollection> OnTurnOffHighlight;

        [Header("Color")]
        [SerializeField]private Color activeAttributeColor;
        [SerializeField]private Color inActiveAttributeColor;

        private void Start()
        {
            dicAttributeBox = new Dictionary<HeroCollection, AttributeVisualManager>();
            activeHeroCollections = new List<HeroCollection>();
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced += SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved += DeleteAttribute;
                heroCollectionsInGame[i].OnCollectionChanged += RefreshAttribute;
            }
            ResetAttributeVisuals();

            OnTurnOnHighlight += TurnOnHighlight;
            OnTurnOffHighlight += TurnOffHighlight;
        }
        private void OnDisable()
        {
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced -= SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved -= DeleteAttribute;
                heroCollectionsInGame[i].OnCollectionChanged -= RefreshAttribute;
            }
            OnTurnOnHighlight -= TurnOnHighlight;
            OnTurnOffHighlight -= TurnOffHighlight;
        }

        private void SpawnNewAttribute(HeroCollection _collection)
        {
            activeHeroCollections.Add(_collection);
            UpdateAttributeBox();
        }
        private void DeleteAttribute(HeroCollection _collection)
        {
            activeHeroCollections.Remove(_collection);
            UpdateAttributeBox();
        }
        private void RefreshAttribute(HeroCollection _collection)
        {
            UpdateAttributeVisual(dicAttributeBox[_collection], _collection);
        }


        /// <summary>
        /// updates the attribute box with active attributes
        /// </summary>
        private void UpdateAttributeBox()
        {
            dicAttributeBox = new Dictionary<HeroCollection, AttributeVisualManager>();
            ResetAttributeVisuals();
            for (int i = 0; i < activeHeroCollections.Count; i++)
            {
                SetEmptyAttributeVisual(activeHeroCollections[i]);
            }
        }
        /// <summary>
        /// looks for the next empty visual and acitvates it
        /// </summary>
        /// <param name="_collection"></param>
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
        /// <summary>
        /// updates the visual representation of the collection
        /// </summary>
        /// <param name="_attributeVisualManager"></param>
        /// <param name="_collection"></param>
        private void UpdateAttributeVisual(AttributeVisualManager _attributeVisualManager, HeroCollection _collection)
        {
            _attributeVisualManager.DiversityText = _collection.Diversity.ToString();
            _attributeVisualManager.NameText = _collection.name;
            _attributeVisualManager.DisplayedCollection = _collection;
            _attributeVisualManager.ToolTipText = _collection.GetToolTip();
            if (_collection.CurrEffect == null)
            {
                _attributeVisualManager.TextColor = inActiveAttributeColor;
            }
            else
            {
                _attributeVisualManager.TextColor = activeAttributeColor;
            }
        }
        /// <summary>
        /// resets the visual representation of a collection to default
        /// </summary>
        private void ResetAttributeVisuals()
        {
            for (int j = 0; j < attributeVisuals.Length; j++)
            {
                attributeVisuals[j].TextColor = inActiveAttributeColor;
                attributeVisuals[j].DisplayedCollection = null;
                attributeVisuals[j].gameObject.SetActive(false);
                attributeVisuals[j].ToolTipText = null;
            }
        }
        private void TurnOnHighlight(HeroCollection _collection)
        {
            _collection.TurnOnHighlights();
        }
        private void TurnOffHighlight(HeroCollection _collection)
        {
            _collection.TurnOffHighlights();
        }
    }
}
