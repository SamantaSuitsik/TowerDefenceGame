using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public TextMeshProUGUI CostText;

    public TextMeshProUGUI ShortCutText;

    public Image IconImage;

    public TowerData TowerData;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Pressed);
        }

        if (TowerData != null)
        {
            CostText.text = TowerData.Cost.ToString() + "$";
            ShortCutText.text = TowerData.ShortCut;
            IconImage.sprite = TowerData.Icon;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(TowerData.ShortCut.ToLower()))
        {
            Events.TowerSelected(TowerData);
        }
    }

    public void Pressed()
    {
        Events.TowerSelected(TowerData);
    }

}
