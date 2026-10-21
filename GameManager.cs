using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI strokeText;
    public GameObject victoryPanel;

    private int strokeCount = 0;

    void Awake()
    {
        // Simple Singleton pattern so any script can call GameManager.Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateStrokeUI();
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false); // Make sure victory panel starts hidden
        }
    }

    // Call this function whenever the player hits/swings the ball
    public void AddStroke()
    {
        strokeCount++;
        UpdateStrokeUI();
    }

    private void UpdateStrokeUI()
    {
        if (strokeText != null)
        {
            strokeText.text = "Strokes: " + strokeCount;
        }
    }

    // Call this when the ball sinks into the hole
    public void CompleteLevel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    // Call this function from the Restart Button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}