using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace ARImages
{
    public class ARKitController : MonoBehaviour
    {
        public ARImage[] ImageCollection;



        //[SerializeField]
        //private ARReferenceImage referenceImage;

        //[SerializeField]
        //private GameObject prefabToGenerate;

        //private GameObject imageAnchorGO;

        void Start()
        {
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
            UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
            UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;

        }

        void OnDestroy()
        {
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
            UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
            UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

        }

        void AddImageAnchor(ARImageAnchor arImageAnchor)
        {
            Debug.LogFormat("image anchor added[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
            for (int i = 0; i < ImageCollection.Length; i++)
            {
                if (arImageAnchor.referenceImageName == ImageCollection[i].ArImageName)
                {
                    ARAnchor a = new ARAnchor();
                    a.CreateAnchorARKit(arImageAnchor);
                    ImageCollection[i].Add(a);
                    break;
                }
            }
        }

        void UpdateImageAnchor(ARImageAnchor arImageAnchor)
        {
            Debug.LogFormat("image anchor updated[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
            for (int i = 0; i < ImageCollection.Length; i++)
            {
                if (arImageAnchor.referenceImageName == ImageCollection[i].ArImageName)
                {
                    //TODO: support turning on and off based on tracking, and add similar support to arcore
                    //if (arImageAnchor.isTracked)
                    //{
                    //if (!imageAnchorGO.activeSelf)
                    //{
                    //    imageAnchorGO.SetActive(true);
                    //}
                    ImageCollection[i].ProcessTracking();
                    //}
                    //else if (imageAnchorGO.activeSelf)
                    //{
                    //    imageAnchorGO.SetActive(false);
                    //}
                }
            }
        }

        void RemoveImageAnchor(ARImageAnchor arImageAnchor)
        {
            Debug.LogFormat("image anchor removed[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
            for (int i = 0; i < ImageCollection.Length; i++)
            {
                if (arImageAnchor.referenceImageName == ImageCollection[i].ArImageName)
                {
                    ImageCollection[i].Remove();
                    break;
                }
            }
        }
    }
}
