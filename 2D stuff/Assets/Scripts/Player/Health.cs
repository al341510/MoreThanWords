using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    private Image health;
    [SerializeField]
    private Text healthvalue;

    private float currentFill;
    [SerializeField]
    private float lerptime = 1.0f;

    /*Maxpossible Health and getter/setter*/
    [SerializeField]
    private float MaxValue { get; set; }

    /*Current Healt and getter/setter*/
    private float currentValue;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MaxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = (float)currentValue / (float)MaxValue;

            //healthvalue.text = currentValue + "/" + MaxValue;
        }
    }

    // Use this for initialization
    void Start () {
        health = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentFill != health.fillAmount) {
            health.fillAmount = Mathf.Lerp(health.fillAmount, currentFill, Time.deltaTime * lerptime);
        }
	}

    public void setHealth(float currentValue, float maxValue) {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
