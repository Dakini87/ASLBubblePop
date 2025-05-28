using UnityEngine;
using Meta.XR.BuildingBlocks;

public class ASLPoseDetector : MonoBehaviour
{
    private OVRHand _rightHand;
    private OVRSkeleton _rightSkeleton;

    private void Start()
    {
        // Get hand tracking components from Building Blocks
        var handTrackingBlock = FindObjectOfType<HandTrackingBlock>();
        if (handTrackingBlock == null)
        {
            Debug.LogError("Add HandTrackingBlock from Building Blocks!");
            return;
        }

        _rightHand = handTrackingBlock.RightHand;
        _rightSkeleton = _rightHand.GetComponent<OVRSkeleton>();

        Debug.Log(_rightHand != null ? "Hand tracking ready!" : "Hand NOT found");
    }

    public bool CheckPose(char letter)
    {
        if (_rightHand == null || !_rightHand.IsTracked) return false;

        return letter switch
        {
            'A' => IsPoseA(),
            'B' => IsPoseB(),
            'C' => IsPoseC(),
            _ => false
        };
    }

    // Updated pose detection for OpenXR
    private bool IsPoseA() // Thumb extended, others curled
    {
        return _rightHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb) == false &&
               _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.7f;
    }

    private bool IsPoseB() // All fingers extended, thumb folded
    {
        return _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) > 0.7f &&
               _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) < 0.3f &&
               _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) < 0.3f;
    }

    private bool IsPoseC() // Hand curved like a "C"
    {
        return _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) > 0.4f &&
               _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.4f &&
               _rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) > 0.4f;
    }
}