using System.Collections;
using System.Collections.Generic;
using dook.tools;
using TMPro;
using UnityEngine;

public class ProgressMan : MonoBehaviour
{
    public TextMeshProUGUI progressText;
    private float timer;
    public Goal? currentGoal = null;

    public static ProgressMan instance;
    
    public static class GoalStrings
    {
        public const string seatGoal = "FIND A SEAT";
        public const string underSeat = "FIND ANOTHER SEAT";
    };
    
    // Start is called before the first frame update
    void Start()
    {
        SetGoal(GoalStrings.seatGoal, "why is that guy standing there's so many seats lol", 15);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGoal != null)
        {
            timer -= Time.deltaTime;
            
            progressText.text = $"{currentGoal.Value.text} {timer.RoundTo(1)}s";

            if (timer <= 0)
            {
                GetComponent<GameMan>().Setdeath(currentGoal.Value.reason);
                currentGoal = null;
            }
        }
    }

    public void SetGoal(string goal, string deathReason, float timer)
    {
        currentGoal = (new Goal { text = goal, reason = deathReason, timer = timer });
        this.timer = timer;
    }

    public void ClearGoal(string goal)
    {
        if (currentGoal != null && currentGoal.Value.text == goal)
        {
            currentGoal = null;
            progressText.text = "";
        }
    }
    public void ForceClearGoal()
    {
        currentGoal = null;
            progressText.text = "";
    }
    
    public struct Goal
    {
        public string text;
        public float timer;
        public string reason;
    }
}
