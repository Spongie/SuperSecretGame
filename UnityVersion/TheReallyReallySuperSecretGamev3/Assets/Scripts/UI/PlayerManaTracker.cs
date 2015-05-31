using UnityEngine;
using System.Collections;
using CVCommon.Utility;
using UnityEngine.UI;
using Assets.Scripts.Utility;

public class PlayerManaTracker : MonoBehaviour
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
        ivImage.fillAmount = ivPlayer.ManaPercentage;
    }
}

