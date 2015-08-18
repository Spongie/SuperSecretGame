﻿using Assets.Scripts.Character.Stat;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXpTracker : MonoBehaviour
{
    private CStats ivPlayer;
    private Image ivImage;

    void Start()
    {
        ivPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().stats;
        ivImage = GetComponent<Image>();
    }

    void Update()
    {
        ivImage.fillAmount = ivPlayer.ExpPercentage;
    }
}
