using UnityEngine;

namespace AutoDefense
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private InitScriptObject[] initializableObjects;
        [SerializeField] private InitScriptObject[] levelInformation;
        [SerializeField] private InitScriptObject[] heroCollections;
        [SerializeField] private InitScriptObject[] enemyCollections;
        [SerializeField] private InitScriptObject[] effectCollections;
        [SerializeField] private InitScriptObject[] baseEventEffects;

        private void Awake()
        {
            InitializeCollection(initializableObjects);
            InitializeCollection(levelInformation);
            InitializeCollection(heroCollections);
            InitializeCollection(enemyCollections);
            InitializeCollection(effectCollections);
            InitializeCollection(baseEventEffects);
        }

        private void InitializeCollection(InitScriptObject[] _collection)
        {
            for (int i = 0; i < _collection.Length; i++)
            {
                _collection[i].Initialize();
            }
        }
    }
}
