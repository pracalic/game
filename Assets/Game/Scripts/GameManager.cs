using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wolfik
{
    public class GameManager
    {
        public static GameManager instance = null;

        PosibleToGoPlaces posiblePlaces;

        private void Init()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("Many instances of game manager");
        }

        public GameManager()
        {
            Init();
        }

        public GameManager(Vector3 firstPos, int x, int y)
        {
            Init();
            posiblePlaces = new PosibleToGoPlaces();
            posiblePlaces.SetListAll(firstPos, x, y);
        }

        public PosibleToGoPlaces GetPosiblePlacesList()
        {
            return posiblePlaces;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}