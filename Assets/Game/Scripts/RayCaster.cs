using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace wolfik
{
    public class RayCaster : MonoBehaviour
    {
        Camera cam;
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, 200f);
                if (hit.collider != null)
                    if (hit.collider.CompareTag("GameController"))
                    {
                        Debug.Log(hit.collider.tag);
                        //PhotonNetwork.Destroy(hit.collider.gameObject);
                        //hit.collider.gameObject.SetActive(false);
                        //hit.collider.enabled = false;
                        hit.collider.GetComponent<CubeObserver>().SetVisible();
                        
                    }
            }
        }
    }
}
