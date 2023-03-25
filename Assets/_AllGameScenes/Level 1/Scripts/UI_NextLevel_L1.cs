using UnityEngine;
using UnityEngine.UI;
public class UI_NextLevel_L1 : MonoBehaviour
{
    Button button;
    public LevelManager lm;
    public Debug_CameraLerpNext camManager;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(nextLevel);
    }
    public void nextLevel()
    {

        camManager.goToNextCam();
    }
}
