using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARImages
{
    //Based on AugmentedImageExampleController.cs by Google
    public class ARCoreController : MonoBehaviour
    {
        public ARImage[] ImageCollection;

        public GameObject FitToScanOverlay;

        private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Check that motion tracking is tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

            foreach (var image in m_TempAugmentedImages)
            {
                //sanity check, if our image is greater than our collection, skip
                if (image.DatabaseIndex >= ImageCollection.Length)
                {
                    continue;
                }

                switch (image.TrackingState)
                {
                    case TrackingState.Tracking:
                        if (!ImageCollection[image.DatabaseIndex].GetIsActive())
                        {
                            ARAnchor a = new ARAnchor();
                            a.CreateAnchorARCore(image);
                            ImageCollection[image.DatabaseIndex].Add(a);
                        }
                        ImageCollection[image.DatabaseIndex].ProcessTracking();
                        break;
                    case TrackingState.Stopped:
                        ImageCollection[image.DatabaseIndex].Remove();
                        break;
                }
            }

            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var arImage in ImageCollection)
            {
                if (arImage.GetIsActive())
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }
            }

            FitToScanOverlay.SetActive(true);
        }
    }
}




