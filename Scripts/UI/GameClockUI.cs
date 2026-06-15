using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour {

    [SerializeField] private Image GameClockImage;


    private void Update() {
        UpdateClockVisual();
    }

    private void UpdateClockVisual() {
        GameClockImage.fillAmount = GameHandler.Instance.GetGamePlayingTimeNormalized();
    }
}
