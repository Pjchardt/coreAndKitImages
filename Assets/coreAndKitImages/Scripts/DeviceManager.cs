using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARImages
{
    public class DeviceManager : MonoBehaviour
    {
        public static DeviceManager Instance;

        //TODO do something for when in editor
        public GameObject ARCorePrefab;
        public GameObject ARKitPrefab;

        private GameObject arSessionDevice;
        public GameObject ARSessionDevice
        {
            get { return arSessionDevice; }
            private set { arSessionDevice = value; }
        }

        private void Awake()
        {
            if (Instance != null) { DestroyImmediate(gameObject); }

            Instance = this;

#if UNITY_IOS
        ARSessionDevice = GameObject.Instantiate(ARKitPrefab);
#elif UNITY_ANDROID
            ARSessionDevice = GameObject.Instantiate(ARCorePrefab);
#endif
        }
    }
}
