using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_StartButton : MonoBehaviour
{
    public Button startButton;
    public Image blackImage;
    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(playTheGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playTheGame()
    {
        startTime = Time.time;
        StartCoroutine(endScene(blackImage.color, new Color(0, 0, 0, 1)));
    }

    float startTime;
    public float animationDuration;
    IEnumerator endScene(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey < 1.1f)
        {

            blackImage.color = Color.Lerp(start, end, journey);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;

        }
        blackImage.color = end;
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(1);

    }
}
