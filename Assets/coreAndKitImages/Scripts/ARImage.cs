﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace ARImages
{
    [Serializable]
    public class ARImage
    {
        public string  ArImageName;
        public ARImageViz Prefab;
        private ARImageViz rootObject;
        ARAnchor anchor;

        public bool GetIsActive()
        {
            return rootObject != null;
        }

        public void ProcessTracking()
        {
            anchor.UpdateTransform();
            rootObject.transform.position = anchor.GetTransform().position;
            rootObject.transform.rotation = anchor.GetTransform().rotation;
        }

        public void ProcessTracking(Pose p)
        {
            anchor.UpdateAnchor(p);
            anchor.UpdateTransform();
        }

        public void Add(ARAnchor a)
        {
            Debug.Log("Initializing image");
            anchor = a;
            rootObject = (ARImageViz)GameObject.Instantiate(Prefab, anchor.GetTransform());
            rootObject.BeginTracking();
        }

        public void Remove()
        {
            if (rootObject != null)
            {
                rootObject.StopTracking();
            } 
        }
    }
}
