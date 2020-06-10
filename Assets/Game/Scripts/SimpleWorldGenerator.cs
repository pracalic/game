using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace wolfik
{
    public class SimpleWorldGenerator : MonoBehaviour
    {
        [SerializeField]
        int worldXSize = 15, worldYSize = 15;

        [SerializeField]
        GameObject basicCubePrefab = null;

        [SerializeField]
        float playerYPos = 1;

        
        
        // Start is called before the first frame update
        void Awake()
        {
            if (basicCubePrefab == null)
            {
                Debug.LogError("Set prefab for basic cube");
                return;
            }

            //Generate();
        }

        public void Generate()
        {
            int firstXPos = -worldXSize / 2;
            int firstYPos = -worldYSize / 2;

            if(PhotonNetwork.IsMasterClient)
            for (int i = 0; i < worldXSize; i++)
                for (int j = 0; j < worldYSize; j++)
                {
                   GameObject gob = PhotonNetwork.Instantiate(basicCubePrefab.name, new Vector3(firstXPos + i, 0, firstYPos + j), Quaternion.identity);
                    gob.transform.SetParent(this.transform);
                }

           new GameManager(new Vector3(firstXPos, playerYPos, firstYPos), worldXSize, worldYSize);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
