using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CVCommon.Utility;
using Assets.Scripts.Utility;

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
