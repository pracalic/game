using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wolfik
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        [SerializeField]
        float smooth = 1f;

        bool isSet = false;

        Vector3 dist;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void SetTarget(Transform tr)
        {
            target = tr;
            dist = transform.position - target.position;
            isSet = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(isSet)
            transform.position = Vector3.Lerp(transform.position, target.position + dist, smooth * Time.deltaTime);
        }
    }
}
