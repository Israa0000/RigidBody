using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PresetChanger : MonoBehaviour
{
    [SerializeField] List<Stats> playerStats = new List<Stats>();
    private Movement playerMovement;
    private int currentPreset = 0;
    public void AddElement(Stats newStat)
    {
        playerStats.Add(newStat);
    }

    void Start()
    {
        playerMovement = GetComponent<Movement>(); 

        if (playerStats.Count > 0)
        {
            ChangePreset(currentPreset);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentPreset = (currentPreset + 1) % playerStats.Count;
            ChangePreset(currentPreset);
        }
    }

    public void ChangePreset(int index)
    {
        if (index >= 0 && index < playerStats.Count)
        {
            Stats selectedPreset = playerStats[index];
            playerMovement.SetMovementProfile(selectedPreset); 
        }
    }
}
