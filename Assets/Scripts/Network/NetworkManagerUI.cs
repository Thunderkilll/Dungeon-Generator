using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;


public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private PlayerSurvival playerSurvivalScript;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private UIController uIController;
    [SerializeField] private GameStateManager gameStateManager;


    void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {

            NetworkManager.Singleton.StartServer();
            ActivateScripts();
        });
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            ActivateScripts();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            ActivateScripts();
        });
    }

    void DeactivateScripts()
    {
        cameraFollow.enabled = false;
        audioManager.enabled = false;
        musicManager.enabled = false;
        playerSurvivalScript.enabled = false;
        gameStateManager.enabled = false;
        uIController.enabled = false;

    }

    void ActivateScripts()
    {
        cameraFollow.enabled = true;
        audioManager.enabled = true;
        musicManager.enabled = true;
        playerSurvivalScript.enabled = true;
        gameStateManager.enabled = true;
        uIController.enabled = true;
    }
    private void Start()
    {
        DeactivateScripts();
    }
}
