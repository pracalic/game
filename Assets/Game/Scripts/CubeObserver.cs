using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace wolfik
{
    public class CubeAcumulator
    {
        public int id;
        public GameObject gob;
        public CubeObserver cO;

        public CubeAcumulator(int ident, GameObject gobek, CubeObserver cubOb)
        {
            id = ident;
            gob = gobek;
            cO = cubOb;
        }
    }

    public class CubeObserver : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField]
        GameObject gob;
        bool visible = true;
        public static List<CubeAcumulator> acumList = new List<CubeAcumulator>();

        public static void FindAndHide(int id)
        {
            for (int i = 0; i < acumList.Count; i++)
            {
                if (acumList[i].id == id)
                {
                    acumList[i].cO.SetVisible();
                    Debug.Log("znalazlem");
                }

            }
        }

        public void SetVisible(bool to = false)
        {
            visible = to;
            gob.SetActive(visible);
            Debug.Log("ukrywam");

            //this.gameObject.SetActive(visible);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                Debug.Log("wysylam wiad");
                // We own this player: send the others our data
                stream.SendNext(visible);
                
                //Debug.Log("dupa");
            }
            else
            {
                // Network player, receive data
                Debug.Log("odbieram wiad");
                gob.SetActive((bool)stream.ReceiveNext());

            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                acumList.Add(new CubeAcumulator(GetComponent<PhotonView>().ViewID, this.gameObject, this));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
