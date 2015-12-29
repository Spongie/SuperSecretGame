using Assets.Scripts.Character.Stats;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaTracker : MonoBehaviour
{
    private CStats ivPlayer;
    private Image ivImage;

    void Start()
    {
        ivPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().stats;
        ivImage = GetComponent<Image>();
    }

    void Update()
    {
        ivImage.fillAmount = ivPlayer.Resources.ManaPercentage;
    }
}

