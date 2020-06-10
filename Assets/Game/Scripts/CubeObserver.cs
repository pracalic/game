using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace wolfik
{
    public class CubeObserver : MonoBehaviourPunCallbacks, IPunObservable
    {
        bool visible = true;

        public void SetVisible(bool to = false)
        {
            visible = to;
            
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(visible);
                Debug.Log("dupa");
            }
            else
            {
                // Network player, receive data
                this.gameObject.SetActive((bool)stream.ReceiveNext());

            }
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
