using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play sink sound
            GolfBallSFX sfx = other.GetComponent<GolfBallSFX>();
            if (sfx != null)
            {
                sfx.PlaySinkSound();
            }

            Debug.Log("Hole in One! Level Complete!");

            // THIS IS THE NEW PART: Pop up the Victory Panel!
            if (GameManager.Instance != null)
            {
                GameManager.Instance.CompleteLevel();
            }
        }
    }
}