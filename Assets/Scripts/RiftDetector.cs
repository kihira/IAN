using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.VR;

public class RiftDetector : MonoBehaviour
{

    [SerializeField] private bool forceHeadMounted = false;
    [SerializeField] private GameObject headMounted;
    [SerializeField] private GameObject deskMounted;

    [ExecuteInEditMode]
    void Start()
    {
        if (forceHeadMounted)
        {
            deskMounted.SetActive(false);
            headMounted.SetActive(true);
            return;
        }
        deskMounted.SetActive(!VRDevice.isPresent);
        headMounted.SetActive(VRDevice.isPresent);

        GameObject.Find("/Player/Hand Mount").GetComponent<HandInfoPanel>().handController = GameObject.FindWithTag("LeapController").GetComponent<HandController>();
    }
}
