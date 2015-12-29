using Assets.Scripts.Character.Stats;
using System;
using UnityEngine;

public class PlayerHealthTracker : MonoBehaviour
{
    public CStats ivPlayer;
    public UnityEngine.UI.Image ivImage;

    void Start()
    {
        ReadObjects();
    }

    private void ReadObjects()
    {
        try
        {
            ivPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().stats;
            ivImage = GetComponent<UnityEngine.UI.Image>();
        }
        catch (Exception e)
        {
            Assets.Scripts.Utility.Logger.Log("Failed to load objects");
            Assets.Scripts.Utility.Logger.Log(e.Message);
        }
    }

    void Update()
    {

        if (ivPlayer == null || ivImage == null)
            ReadObjects();

        ivImage.fillAmount = ivPlayer.Resources.HealthPercentage;
    }
}
