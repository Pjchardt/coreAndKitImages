using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace ARImages
{
    public class ARAnchor
    {
        Anchor arCoreAnchor;
        ARImageAnchor arKitAnchor;

        Transform t;

        public void CreateAnchorARCore(AugmentedImage image)
        {
            arCoreAnchor = image.CreateAnchor(image.CenterPose);
            CreateTransform();
        }

        public void CreateAnchorARKit(ARImageAnchor arImageAnchor)
        {
            arKitAnchor = arImageAnchor;
            CreateTransform();
        }

        public void UpdateAnchor(Pose p)
        {
            arCoreAnchor.transform.position = p.position;
            arCoreAnchor.transform.rotation = p.rotation;
        }

        void CreateTransform()
        {
            t = new GameObject("arAnchor").transform;
        }

        public void UpdateTransform()
        {
#if UNITY_IOS
            t.position = UnityARMatrixOps.GetPosition(arKitAnchor.transform);
            t.rotation = UnityARMatrixOps.GetRotation(arKitAnchor.transform);
#elif UNITY_ANDROID
            t.position = arCoreAnchor.transform.position;
            t.rotation = arCoreAnchor.transform.rotation;
#endif
        }

        public Transform GetTransform()
        {
#if UNITY_IOS
            t.position = UnityARMatrixOps.GetPosition(arKitAnchor.transform);
            t.rotation = UnityARMatrixOps.GetRotation(arKitAnchor.transform);
            return t;
#elif UNITY_ANDROID
            t.position = arCoreAnchor.transform.position;
            t.rotation = arCoreAnchor.transform.rotation;
            return t;
#else
            return null;
#endif
        }

        public Matrix4x4 GetMatrixTransform()
        {
#if UNITY_IOS
            return arKitAnchor.transform;
#elif UNITY_ANDROID
            //TODO: Implement
            return Matrix4x4.identity;
#else
            return Matrix4x4.identity; 
#endif
        }
    }
}

