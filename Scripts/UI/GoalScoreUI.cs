using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalScoreUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI goalScoreCountText;

    public static int currentScoreCount;



    private void Start() {
        currentScoreCount = 0;
        UpdateScoreCard();

        Projectile.OnGoalHit += Projectile_OnGoalHit;
    }

    private void Projectile_OnGoalHit(object sender, EventArgs e) {
        currentScoreCount++;
        UpdateScoreCard();
    }

    private void UpdateScoreCard() {
        goalScoreCountText.text = currentScoreCount.ToString();
    }

    public static int GetGoalScore() {
        return currentScoreCount;
    }

    private void OnDestroy() {
        Projectile.OnGoalHit -= Projectile_OnGoalHit;
    }
} 
