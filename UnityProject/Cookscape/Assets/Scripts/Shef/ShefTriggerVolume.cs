using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProject.Cookscape
{
    public class ShefTriggerVolume : MonoBehaviour
    {
        public bool CanImprisonCatchee = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Jail"))
            {
                CanImprisonCatchee = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Jail"))
            {
                CanImprisonCatchee = false;
            }
        }
    }
}
