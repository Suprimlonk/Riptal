using UnityEngine;
using UnityEngine.UI;

public class Crown : MonoBehaviour
{
    public GameObject winPanel;
    public Text winText;
    public Text timeText;
    private bool hasWon = false;

    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
            Debug.Log("Crown: WinPanel encontrado y desactivado");
        }
        else
        {
            Debug.LogError("Crown: winPanel NO está asignado en el inspector");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Crown: Colisión detectada con " + other.gameObject.name + " | Tag: " + other.tag);
        
        if (hasWon)
            return;

        if (other.CompareTag("Player") || other.gameObject.name == "Player")
        {
            Debug.Log("Crown: ¡Jugador detectado! Mostrando pantalla de victoria");
            hasWon = true;
            ShowWinScreen();
        }
        else
        {
            Debug.Log("Crown: Colisión pero no es el jugador. Se espera tag 'Player'");
        }
    }

    private void ShowWinScreen()
    {
        if (winPanel == null)
        {
            Debug.LogError("Crown: Win panel no está asignado");
            return;
        }

        float elapsedSeconds = Time.timeSinceLevelLoad;
        int minutes = Mathf.FloorToInt(elapsedSeconds / 60f);
        int seconds = Mathf.FloorToInt(elapsedSeconds % 60f);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        winPanel.SetActive(true);
        
        string message = "¡GANASTE!\n\nGracias por jugar\nPostdata ya no me dio tiempo\nde poner todo lo que queria :(";
        string timeMessage = "Tiempo: " + formattedTime;

        if (winText != null)
        {
            winText.text = message;
            winText.fontSize = 40;
            winText.alignment = TextAnchor.MiddleCenter;
            winText.color = Color.yellow;
            winText.horizontalOverflow = HorizontalWrapMode.Wrap;
            winText.verticalOverflow = VerticalWrapMode.Overflow;
        }
        else
        {
            Debug.LogWarning("Crown: winText no está asignado");
        }
            
        if (timeText != null)
        {
            timeText.text = timeMessage;
            timeText.fontSize = 30;
            timeText.alignment = TextAnchor.MiddleCenter;
            timeText.color = Color.white;
            timeText.horizontalOverflow = HorizontalWrapMode.Overflow;
            timeText.verticalOverflow = VerticalWrapMode.Overflow;
        }
        else if (winText != null)
        {
            winText.text += "\n\n" + timeMessage;
        }
        else
        {
            Debug.LogWarning("Crown: timeText no está asignado");
        }

        Debug.Log("Crown: Pantalla de victoria mostrada. Tiempo: " + formattedTime);
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }
}
