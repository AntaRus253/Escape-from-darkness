using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mapName;
    [SerializeField] private TMP_Text mapDescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockImage;

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapDescription.text = map.mapDescription;
        mapImage.sprite = map.mapImage;
        
        lockImage.SetActive(map.mapLocked);
        playButton.interactable = !map.mapLocked;

        if (map.mapLocked)
        {
            mapImage.color = Color.grey;
        }
        else
        {
            mapImage.color = Color.white;
        }
        
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(()=> SceneManager.LoadScene(map.mapDescription));
    }
}
