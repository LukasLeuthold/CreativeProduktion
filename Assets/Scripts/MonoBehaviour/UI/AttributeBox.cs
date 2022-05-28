using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles attribute box logic
    /// </summary>
    public class AttributeBox : MonoBehaviour
    {
        /// <summary>
        /// list of all herocollections in the game
        /// </summary>
        [SerializeField] private List<HeroCollection> heroCollectionsInGame;
        /// <summary>
        /// dictionary of collecitons and corresponding visualmanagers
        /// </summary>
        Dictionary<HeroCollection, AttributeVisualManager> dicAttributeBox;
        /// <summary>
        /// array of possible attribute spots
        /// </summary>
        public AttributeVisualManager[] attributeVisuals = new AttributeVisualManager[12];
        /// <summary>
        /// list of the active attributes in play
        /// </summary>
        private List<HeroCollection> activeHeroCollections;
        /// <summary>
        /// gets called when highlights get turned on
        /// </summary>
        public static Action<HeroCollection> OnTurnOnHighlight;
        /// <summary>
        /// gets called when highlights get turned of
        /// </summary>
        public static Action<HeroCollection> OnTurnOffHighlight;

        /// <summary>
        /// sprite used for active attributes
        /// </summary>
        [Header("Sprites")]
        [SerializeField] private Sprite activeAttributeSprite;
        /// <summary>
        /// sprote used for inactive attributes
        /// </summary>
        [SerializeField]private Sprite inActiveAttributeSprite;

        /// <summary>
        /// sets default values; subscribes to action
        /// </summary>
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
        /// <summary>
        /// unsubscribes from action
        /// </summary>
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

        /// <summary>
        /// adds a new attribute to the box
        /// </summary>
        /// <param name="_collection">attribute to add</param>
        private void SpawnNewAttribute(HeroCollection _collection)
        {
            activeHeroCollections.Add(_collection);
            UpdateAttributeBox();
        }
        /// <summary>
        /// removes an attribute from the box
        /// </summary>
        /// <param name="_collection">attribute to remove</param>
        private void DeleteAttribute(HeroCollection _collection)
        {
            activeHeroCollections.Remove(_collection);
            UpdateAttributeBox();
        }
        /// <summary>
        /// refreshes the complet attribute box
        /// </summary>
        /// <param name="_collection">collection which triggered the refreshing</param>
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
            if (_collection.CurrActiveEffect == null)
            {
                _attributeVisualManager.Attributesprite = inActiveAttributeSprite;
            }
            else
            {
                _attributeVisualManager.Attributesprite = activeAttributeSprite;
            }
        }
        /// <summary>
        /// resets the visual representation of a collection to default
        /// </summary>
        private void ResetAttributeVisuals()
        {
            for (int j = 0; j < attributeVisuals.Length; j++)
            {
                attributeVisuals[j].Attributesprite = inActiveAttributeSprite;
                attributeVisuals[j].DisplayedCollection = null;
                attributeVisuals[j].gameObject.SetActive(false);
                attributeVisuals[j].ToolTipText = null;
            }
        }
        /// <summary>
        /// turns on the highlights
        /// </summary>
        /// <param name="_collection">colleciton to highlight</param>
        private void TurnOnHighlight(HeroCollection _collection)
        {
            _collection.TurnOnHighlights();
        }
        /// <summary>
        /// turns of the highlights
        /// </summary>
        /// <param name="_collection">colleciton to highlight</param>
        private void TurnOffHighlight(HeroCollection _collection)
        {
            _collection.TurnOffHighlights();
        }
    }
}
