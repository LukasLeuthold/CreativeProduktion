using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// object used for initializing scriptable objects
    /// </summary>
    public class Initializer : MonoBehaviour
    {
        /// <summary>
        /// array of initializable objects
        /// </summary>
        [SerializeField] private InitScriptObject[] initializableObjects;
        /// <summary>
        /// array of level informations
        /// </summary>
        [SerializeField] private InitScriptObject[] levelInformation;
        /// <summary>
        /// array of hero collections
        /// </summary>
        [SerializeField] private InitScriptObject[] heroCollections;
        /// <summary>
        /// array of enemy collections
        /// </summary>
        [SerializeField] private InitScriptObject[] enemyCollections;
        /// <summary>
        /// array of effect collections
        /// </summary>
        [SerializeField] private InitScriptObject[] effectCollections;
        /// <summary>
        /// array of event effects
        /// </summary>
        [SerializeField] private InitScriptObject[] baseEventEffects;

        /// <summary>
        /// initializes all arrays
        /// </summary>
        private void Awake()
        {
            InitializeCollection(initializableObjects);
            InitializeCollection(levelInformation);
            InitializeCollection(heroCollections);
            InitializeCollection(enemyCollections);
            InitializeCollection(effectCollections);
            InitializeCollection(baseEventEffects);
        }

        /// <summary>
        /// initializes a collection of initializable scriptable objects
        /// </summary>
        /// <param name="_collection">collection to initialize</param>
        private void InitializeCollection(InitScriptObject[] _collection)
        {
            for (int i = 0; i < _collection.Length; i++)
            {
                _collection[i].Initialize();
            }
        }
    }
}
