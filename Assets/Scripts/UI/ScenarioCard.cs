using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenarioCard : MonoBehaviour
{
    public ScenarioData ScenarioData;

    public TextMeshProUGUI Text;
    // Start is called before the first frame update

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(ScenarioData data)
    {
        ScenarioData = data;
        Text.text = ScenarioData.PresenterName;
    }

    public void OnClick()
    {
        if (ScenarioData != null)
        {
            Events.ScenarioSelected(ScenarioData);
        }
        else
        {
            Debug.LogWarning("ScenarioData is null. Cannot select scenario.");
        }
    }
}















