using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class AttributeBox : MonoBehaviour
    {
        [SerializeField] private List<HeroCollection> heroCollectionsInGame;
        Dictionary<HeroCollection, GameObject> attributeBox;
        GameObject attributeVisual;

        private void Start()
        {
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced += SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved += DeleteAttribute;
                heroCollectionsInGame[i].OnDiversityChanged += SetAttributeCount;
                heroCollectionsInGame[i].OnBuffChanged += SetAttributeStatus;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < heroCollectionsInGame.Count; i++)
            {
                heroCollectionsInGame[i].OnFirstUnitPlaced -= SpawnNewAttribute;
                heroCollectionsInGame[i].OnLastUnitRemoved -= DeleteAttribute;
                heroCollectionsInGame[i].OnDiversityChanged -= SetAttributeCount;
                heroCollectionsInGame[i].OnBuffChanged -= SetAttributeStatus;
            }
        }

        private void SpawnNewAttribute(HeroCollection _collection)
        {
            Debug.Log("spawn new Attribute: " + _collection.name);
            //GameObject go = Instantiate(attributeVisual);
            //attributeBox.Add(_collection,go);
        }
        private void DeleteAttribute(HeroCollection _collection)
        {
            Debug.Log("delete Attribute: " + _collection.name);
            //Destroy(attributeBox[_collection]);
            //attributeBox.Remove(_collection);
        }
        private void SetAttributeStatus(HeroCollection _collection)
        {
            Debug.Log("setAttribute status: " + _collection.name);

        }
        private void SetAttributeCount(HeroCollection _collection)
        {
            Debug.Log("changed count to: " + _collection.Diversity);
        }

    }
}
