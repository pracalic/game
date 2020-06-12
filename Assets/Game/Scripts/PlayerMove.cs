using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace wolfik
{
    public class PlayerMove : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField]
        float moveBreak = 0.2f;

        float timer;
        PosibleToGoPlaces places;
        int currentPos;
        int[] table;
        PhotonView photonView;
       
        // Start is called before the first frame update
        void Start()
        {
            timer = Time.time;
            photonView = PhotonView.Get(this);
            places = GameManager.instance.GetPosiblePlacesList();
            currentPos = places.GetCurrentListPosition(transform.position);
            table = places.MoveCheck(currentPos);
        }

        public void SendMessageRay(int id)
        {
            Debug.Log("sle");
            photonView.RPC("RayMessag", RpcTarget.MasterClient, id);
        }


        [PunRPC]
        void RayMessag(int numb)
        {
            Debug.Log("odbieram");
            //Debug.Log(numb);
            CubeObserver.FindAndHide(numb);
        }

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(this.transform.position);
            }
            else
            {
                // Network player, receive data
                this.transform.position = (Vector3)stream.ReceiveNext();

            }
        }

        #endregion
        private void EasyMove()
        {
            Vector2 vec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (vec.x != 0)
            {
                if (vec.x > 0)
                    transform.Translate(1, 0, 0);
                if (vec.x < 0)
                    transform.Translate(-1, 0, 0);

                timer = Time.time + moveBreak;
                return;
            }

            if (vec.y != 0)
            {
                if (vec.y > 0)
                    transform.Translate(0, 0, 1);
                if (vec.y < 0)
                    transform.Translate(0, 0, -1);
                timer = Time.time + moveBreak;
                return;
            }
        }

        private void FillPos(int ind)
        {
            currentPos = ind;
            transform.position = places.GetCurrentWorldPosition(currentPos);
            table = places.MoveCheck(currentPos);
        }

        private void TableMove()
        {
            Vector2 vec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            //int curInd = currentPos;

            if (vec.y != 0)
            {
                if (table[0] > -1 && vec.y > 0)
                {
                    FillPos(table[0]);
                }
                if (table[1] > -1 && vec.y < 0)
                {
                    FillPos(table[1]);
                }
                timer = Time.time + moveBreak;
                return;
            }

            if (vec.x != 0)
            {
                if (table[2] > -1 && vec.x > 0)
                    FillPos(table[2]);
                if (table[3] > -1 && vec.x < 0)
                    FillPos(table[3]);

                timer = Time.time + moveBreak;
                return;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time >= timer)
            {
                TableMove();



            }
        }
    }
}
