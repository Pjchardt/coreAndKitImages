using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ARImages
{
    [System.Serializable]
    public class BeginTrackingEvent : UnityEvent<ARImageViz>
    {
    }

    public class ARImageViz : MonoBehaviour
    {
        public BeginTrackingEvent OnBeginTracking;

        [HideInInspector]
        //public AugmentedImage Image; TODO: add generic arImage class and use here

        public void BeginTracking()
        {
            Debug.Log("Begin tracking called.");
            if (OnBeginTracking != null)
            {
                OnBeginTracking.Invoke(this);
            }
        }

        public void Pause()
        {

        }

        public void Unpause()
        {

        }

        public void StopTracking()
        {

        }
    }
}
    
