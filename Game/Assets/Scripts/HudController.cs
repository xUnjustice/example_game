using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HudController : MonoBehaviour
{
    [Header("Interface Elements")]
    [SerializeField] private TextMeshProUGUI resourcesText;
    [SerializeField] private Text resourcesRequirmentText;
   // [SerializeField] private Text WeaponNameText;
    //[SerializeField] private Text WeaponAmmunitionText;
    /*[SerializeField] private RectTransform weaponReloadBar;


    [Header("Tool Selector")]
    [SerializeField] private GameObject toolFocus;
    [SerializeField] private GameObject toolContainer;
    [SerializeField] private float focusSmoothness;*/

    private float targetFocusX = 0;

    public int Resources
    {
        set
        {
            resourcesText.text = "Resources: " + value;
        }
    }

    

    private void Start()
    {
       // targetFocusX = toolContainer.transform.GetChild(1).transform.position.x;
       // toolFocus.transform.position = new Vector3(targetFocusX, toolFocus.transform.position.y);
      //  resourcesRequirmentText.enabled = true;

    }

    private void Update()
    {
       /* toolFocus.transform.position = new Vector3(
            Mathf.Lerp(toolFocus.transform.position.x, targetFocusX, Time.deltaTime * focusSmoothness),
            toolFocus.transform.position.y
        );*/
    }

    public void UpdateResourcesRequirement(int cost, int balance)
    {
        resourcesRequirmentText.text = "Requires: " + cost;
        if (balance < cost)
        {
            resourcesRequirmentText.color = Color.red;
        }
        else
        {
            resourcesRequirmentText.color = Color.white;
        }
    }

  
}
