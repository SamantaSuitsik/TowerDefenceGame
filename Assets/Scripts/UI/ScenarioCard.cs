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
    void Start()
    {
        Text.text = ScenarioData.PresenterName;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        Events.ScenarioSelected(ScenarioData);
    }
}
