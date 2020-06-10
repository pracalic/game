using System.Collections.Generic;
using UnityEngine;

namespace wolfik
{

    public  class Place
    {
        public Vector3 pos;
        public bool moveReady;

        public Place(Vector3 p, bool ready = true)
        {
            pos = p;
            moveReady = ready;
        }
    }

    public class PosibleToGoPlaces
    {
        List<Place[]> places;
        List<Place> allPlaces;
        int sizeX, sizeY;
        int listLenght;

        public void SetList(Vector3 firstPos, int x, int y)
        {
            places = new List<Place[]>();

            for (int i = 0; i < x; i++)
            {
                places.Add(new Place[y]);
                for (int j = 0; j < y; j++)
                {
                    Debug.Log(i + "  " + j);
                    // places[i] = new list<place>();
                    Place place = new Place(firstPos + new Vector3(i, 0, j));
                    places[i][j] = place;

                }
            }
        }

        public void SetListAll(Vector3 firstPos, int x, int y)
        {
            allPlaces = new List<Place>();
            sizeX = x;
            sizeY = y;
            listLenght = x * y;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    //Debug.Log(i + "  " + j);
                    // places[i] = new list<place>();
                    Place place = new Place(firstPos + new Vector3(i, 0, j));
                    allPlaces.Add(place);

                }
            }
        }

        public int GetCurrentListPosition(Vector3 vec)
        {
            for (int i = 0; i < allPlaces.Count; i++)
            {
                if (allPlaces[i].moveReady && allPlaces[i].pos == vec)
                    return i;
            }

            Debug.LogError("Player stay on bad position. Please correct it or make script for correct this.");
            return -1;
        }

        public Vector3 GetCurrentWorldPosition(int placeIndex)
        {
            return allPlaces[placeIndex].pos;
        }

        private void Fill(ref int pos, int index)
        {
            pos = -1;
            if (index >= 0 && index < listLenght)
            {
                if (allPlaces[index].moveReady)
                    pos = index;
            }
        }

        public int[] MoveCheck(int currentPos)
        {
            int[] table = new int[4];
            int toGo = 0;
            //Debug.Log(currentPos);
            //first north direction
            toGo = currentPos + 1;
            if (toGo % sizeX == 0)
            {
                toGo = -1;
            }
            Fill(ref table[0], toGo);
            //second south direction
            toGo = currentPos - 1;
            if (toGo % sizeX == sizeX-1)
            {
                toGo = -1;
            }

            Fill(ref table[1], toGo);
            //third east direction
            toGo = currentPos + sizeX;
            Fill(ref table[2], toGo);
            //second weast direction
            toGo = currentPos - sizeX;
            Fill(ref table[3], toGo);

            return table;
        }
    }
}
