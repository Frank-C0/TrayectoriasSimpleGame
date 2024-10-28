using System.Collections;
using TMPro;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public EdgeCollider2D edgeCol;
    public PolygonCollider2D polyCol;
    public bool isBallInside;

    [SerializeField] public TextMeshPro countdownText;

    private float countdownTime = 3f;
    private Coroutine countdownCoroutine;

    void Start()
    {
        edgeCol = GetComponent<EdgeCollider2D>();
        polyCol = GetComponent<PolygonCollider2D>();
        print("Edge collider points: " + edgeCol.points.Length);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            isBallInside = true;
            print("Ball is inside the cup");
            StartCountdown();  // Iniciar la cuenta regresiva
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            isBallInside = true;
            print("Ball is still inside the cup");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            isBallInside = false;
            print("Ball left the cup");
            StopCountdown();  // Detener la cuenta regresiva si la bola sale
        }
    }

    void StartCountdown()
    {
        // Iniciar o reiniciar la coroutine de la cuenta regresiva
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        countdownCoroutine = StartCoroutine(Countdown());
    }

    void StopCountdown()
    {
        // Detener la coroutine de la cuenta regresiva y resetear el texto
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
            countdownText.text = "Nada";  // Limpiar el texto de la cuenta regresiva

            // ocultar texto de cuenta regresiva
            countdownText.gameObject.SetActive(false);
        }
    }

    IEnumerator Countdown()
    {
        float timeRemaining = countdownTime;
        while (timeRemaining > 0 && isBallInside)
        {

            countdownText.gameObject.SetActive(true);
            countdownText.text = timeRemaining.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            timeRemaining -= 0.1f;
        }

        // Si se completa la cuenta regresiva y la bola sigue dentro, se incrementa el puntaje
        if (isBallInside)
        {
            countdownText.gameObject.SetActive(false);
            GameManager.Instance.IncrementScore();  // Incrementa el puntaje en GameManager
            countdownText.text = ""; 
            Destroy(gameObject);  // Eliminar la copa
        }
    }
}
