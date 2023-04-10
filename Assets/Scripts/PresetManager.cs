using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetManager : MonoBehaviour
{
    public Preset[] presets = new Preset[3];
    public int currentPresetIndex;
    public float totalAttributeValue;

    public GameObject presetMenu;
    
    public Slider sightSlider;
    public Slider hearingSlider;
    public Slider movementSlider;
    public Slider attackSlider;
    public Slider defenseSlider;

    public TextMeshProUGUI totalAttributeValueText;
    public TextMeshProUGUI sightValueText;
    public TextMeshProUGUI hearingValueText;
    public TextMeshProUGUI movementValueText;
    public TextMeshProUGUI attackValueText;
    public TextMeshProUGUI defenseValueText;

    [SerializeField]
    private Light playerLight;
    
    // Start is called before the first frame update
    void Awake()
    {
        Preset preset1 = new Preset();
        Preset preset2 = new Preset();
        Preset preset3 = new Preset();

        presets[0] = preset1;
        presets[1] = preset2;
        presets[2] = preset3;
        currentPresetIndex = 0;

        playerLight = GameObject.FindGameObjectWithTag("PlayerLight").GetComponent<Light>();

        sightSlider.value = presets[currentPresetIndex].sightAttribute;
        hearingSlider.value = presets[currentPresetIndex].hearingAttribute;
        movementSlider.value = presets[currentPresetIndex].movementAttribute;
        attackSlider.value = presets[currentPresetIndex].attackAttribute;
        defenseSlider.value = presets[currentPresetIndex].defenseAttribute;

        presetMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggling Preset Menu on and off
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!presetMenu.activeInHierarchy)
            {
                Time.timeScale = 0;
                presetMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                presetMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
        
        totalAttributeValue = presets[currentPresetIndex].sightAttribute + presets[currentPresetIndex].hearingAttribute +
            presets[currentPresetIndex].movementAttribute + presets[currentPresetIndex].attackAttribute +
            presets[currentPresetIndex].defenseAttribute;

        // Limiting the slider values so they sum up maximum of 100
        defenseSlider.value = Mathf.Clamp(defenseSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + movementSlider.value + attackSlider.value));
        attackSlider.value = Mathf.Clamp(attackSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + movementSlider.value + defenseSlider.value));
        movementSlider.value = Mathf.Clamp(movementSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + attackSlider.value + defenseSlider.value));
        hearingSlider.value = Mathf.Clamp(hearingSlider.value, 0f, 100 - (sightSlider.value + movementSlider.value + attackSlider.value + defenseSlider.value));
        sightSlider.value = Mathf.Clamp(sightSlider.value, 0f, 100 - (hearingSlider.value + movementSlider.value + attackSlider.value + defenseSlider.value));

        // Updating the values to the UI texts
        totalAttributeValueText.text = totalAttributeValue.ToString() + " %";
        sightValueText.text = presets[currentPresetIndex].sightAttribute.ToString() + " %";
        hearingValueText.text = presets[currentPresetIndex].hearingAttribute.ToString() + " %";
        movementValueText.text = presets[currentPresetIndex].movementAttribute.ToString() + " %";
        attackValueText.text = presets[currentPresetIndex].attackAttribute.ToString() + " %";
        defenseValueText.text = presets[currentPresetIndex].defenseAttribute.ToString() + " %";
    }

    public void OnValueChangedSight(float newValue)
    {
        presets[currentPresetIndex].sightAttribute = newValue;
        ChangeSight();
    }

    public void OnValueChangedHearing(float newValue)
    {
        presets[currentPresetIndex].hearingAttribute = newValue;
    }

    public void OnValueChangedMovement(float newValue)
    {
        presets[currentPresetIndex].movementAttribute = newValue;
    }

    public void OnValueChangedAttack(float newValue)
    {
        presets[currentPresetIndex].attackAttribute = newValue;
    }

    public void OnValueChangedDefense(float newValue)
    {
        presets[currentPresetIndex].defenseAttribute = newValue;
    }

    private void ChangeSight()
    {
        playerLight.range = 2 * presets[currentPresetIndex].sightAttribute;
    }
}
