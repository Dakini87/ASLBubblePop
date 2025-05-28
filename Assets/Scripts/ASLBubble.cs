using UnityEngine;

public class ASLBubble : MonoBehaviour
{
    public char letter = 'A'; // Set in Inspector to A, B, or C
    public GameObject cageToOpen;
    public ParticleSystem successParticles;

    private ASLPoseDetector _poseDetector;
    private bool _alreadyTriggered;

    private void Start()
    {
        _poseDetector = FindObjectOfType<ASLPoseDetector>();
    }

    private void Update()
    {
        if (!_alreadyTriggered && _poseDetector != null && _poseDetector.CheckPose(letter))
        {
            _alreadyTriggered = true;
            successParticles.Play();
            if (cageToOpen != null) cageToOpen.SetActive(false);
            Destroy(gameObject, 0.5f); // Destroy after short delay
        }
    }
}