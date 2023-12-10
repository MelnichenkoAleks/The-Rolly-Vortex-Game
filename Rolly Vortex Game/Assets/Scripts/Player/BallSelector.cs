using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallSelector : MonoBehaviour
{
    public GameObject[] ballSkins;
    int selectedBall = 0;

    public Ball[] balls;
    public Button[] buttons;

    public Button unlockButton;

    private void Awake()
    {
        int counter = 0;
        foreach(Ball b in balls)
        {
            b.index = counter;
            if (counter == 0)
                b.isLocked = false;
            else
            {
                if(PlayerPrefs.GetInt(b.index.ToString(),1) == 1)
                    b.isLocked=true;
                else
                    b.isLocked=false;
                buttons[b.index].interactable = !b.isLocked;
            }
            counter++;
        }
    }
    void Start()
    {   
        selectedBall = PlayerPrefs.GetInt("SelectedBall", 0);
        foreach (GameObject skin in ballSkins)
        {
            skin.SetActive(false);
        }
        ballSkins[selectedBall].SetActive(true);
    }

    
    void Update()
    {
        if (PlayerPrefs.GetInt("TotalGems", 0) < 50)
            unlockButton.interactable = false;
        else
            unlockButton.interactable = true;
    }

    public void ChangeBall(int index)
    {
        ballSkins[selectedBall].SetActive(false);
        selectedBall = index;
        ballSkins[selectedBall].SetActive(true);
        PlayerPrefs.SetInt("SelectedBall", index);
    }

    public void Unlock()
    {
        List<Ball> lockedBalls = new List<Ball>();
        foreach (Ball b in balls)
        {
            if(b.isLocked)
                lockedBalls.Add(b);
        }
        if (lockedBalls.Count == 0)
            return;

        int randomBall = Random.Range(0, lockedBalls.Count);

        int ballIndex = lockedBalls[randomBall].index;
        balls[ballIndex].isLocked = false;
        buttons[ballIndex].interactable = true;
        PlayerPrefs.SetInt(ballIndex.ToString(), 0);
        PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems") - 50);

        buttons[ballIndex].onClick.Invoke();
    }
}
