using UnityEngine;

public class CannonHandler : MonoBehaviour {

    [SerializeField] private Cannon[] cannons;
    [SerializeField] private float shotInterval = 2f;
    [SerializeField] private float resetSequenceTime = 1f;

    private float timer;
    private int currentCannonIndex = 0;
    private int previousCannonIndex = -1;
    private int shotsFired;
    private bool isResting;

    private void Start() {
        timer = 0f;
        shotsFired = 0;
        isResting = false;
    }


    private void Update() {
        UpdateCannon();
    }


    private void UpdateCannon() {
        
        if (GameHandler.Instance.IsGamePlaying()) {
            timer += Time.deltaTime;
            
            // Resets sequence
            if (isResting) {
                if (timer >= resetSequenceTime) {
                    isResting = false;
                    shotsFired = 0;
                    timer = 0f;
                }
                return;
            }

            if (timer >= shotInterval) {
                FireCannon();
                timer = 0f;
            }
        }
    }


    // Fires a random cannon
    private void FireCannon() {
        currentCannonIndex = GetRandomCannonIndex();

        cannons[currentCannonIndex].Shoot();
        previousCannonIndex = currentCannonIndex;
        shotsFired++;

        if (shotsFired >= cannons.Length) {
            isResting = true;
        }
    }


    // Gives a random cannon index
    private int GetRandomCannonIndex() {
        int index = 0;

        while (true) {
            index = Random.Range(0, cannons.Length);

            // Makes sure the same cannon doesn't shoot twice in a row
            if (index != previousCannonIndex) {
                break;
            }
        }
        
        return index;
    }

    public void ResetSequence() {
        timer = 0f;
        currentCannonIndex = 0;
        isResting = false;
    }
}
