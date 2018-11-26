using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcon : MonoBehaviour {

    [SerializeField]
    private Health health;

    [SerializeField]
    public Sprite myFirstImage;
    [SerializeField]
    public Sprite mySecondImage;

    private bool change = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (health.CurrentValue < 30) {
            this.GetComponent<Image>().sprite = mySecondImage;
            change = true;
        }
        else if (health.CurrentValue >= 30 && change != false) {
            this.GetComponent<Image>().sprite = myFirstImage;
            change = false;
        }
	}
}
