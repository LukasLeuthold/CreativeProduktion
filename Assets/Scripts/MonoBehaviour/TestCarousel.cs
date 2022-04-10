using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class TestCarousel : MonoBehaviour
    {
        public HeroPool heropool;
        public ProbabilityDistribution prop;
        public GameObject unitcardprefab;
        public int numberofunits;

        // Start is called before the first frame update
        void Start()
        {
            HeroData[] lineup =heropool.GetLineUp(numberofunits, prop);
            for (int i = 0; i < lineup.Length; i++)
            {
                GameObject Clone =Instantiate(unitcardprefab, Vector3.right * i *1.5f, Quaternion.identity);
                Clone.GetComponent<HeroCard>().HeroData = lineup[i];
                Clone.GetComponent<HeroCard>().HeroData.PlaceOnField();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
