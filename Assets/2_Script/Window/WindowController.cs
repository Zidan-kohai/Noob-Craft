using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class WindowController : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private ChunkSpawner chunkSpawner;
    [SerializeField] private GameObject mobileController;
    [SerializeField] private GameObject pcController;
    [SerializeField] private GameObject settingsController;

    private void Start()
    {
        bool isMobile = YandexGame.EnvironmentData.isMobile;

        mobileController.SetActive(isMobile);
        Cursor.lockState = isMobile ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMobile;
    }

    private void Update()
    {
        if(inputHandler.Settings)
        {
            OpenSettingsWindow();
        }
    }

    public void OpenSettingsWindow()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        chunkSpawner.enabled = false;
        settingsController.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        chunkSpawner.enabled = true;
        settingsController.SetActive(false);
    }
}
