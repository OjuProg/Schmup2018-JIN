using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Avatar to use
    public GameObject player;
    private PlayerAvatar avatar;
    private BulletGun bulletGun;

    // UI Management
    public Slider lifeSlider;
    public Slider energySlider;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            avatar = player.GetComponent<PlayerAvatar>();
            bulletGun = player.GetComponent<BulletGun>();
        }

        lifeSlider.maxValue = avatar.maxHealth;
        lifeSlider.value = avatar.health;
        energySlider.maxValue = bulletGun.energyMax;
        energySlider.value = bulletGun.energy;
    }

    public void Update()
    {
        lifeSlider.value = avatar.health;
        energySlider.value = bulletGun.energy;
    }

    public void TogglePauseMenu()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void ToggleGameOverMenu()
    {
        gameOverPanel.SetActive(!gameOverPanel.activeSelf);
    }
}
