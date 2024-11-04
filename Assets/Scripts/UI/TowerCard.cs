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

    private Button button;

    private TowerData data;

    private KeyCode shortcutKey;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetData(TowerData data)
    {
        this.data = data;
        CostText.text = data.Cost.ToString() + "€";
        ShortCutText.text = data.ShortCut.ToString();
        IconImage.sprite = data.Icon;
        if (System.Enum.TryParse(data.ShortCut, true, out shortcutKey))
        {
            Debug.Log("Shortcut set to: " + shortcutKey);
        }
        else
        {
            Debug.LogError("Invalid Shortcut: " + data.ShortCut);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shortcutKey))
        {
            Pressed();
        }
    }

    public void Pressed()
    {
        Events.TowerSelected(data);
    }

}
