using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIconEffect : MonoBehaviour {

    [SerializeField]
    private Player player;
    [SerializeField]
    public Sprite myFirstImage;
    [SerializeField]
    public Sprite mySecondImage;
    [SerializeField]
    private float lerptime = 1.0f;

    private Image magicTime;

    private float time;

    private float timeE;
    private bool timeFlag = true;

    // Use this for initialization
    void Start () {
        magicTime = GetComponent<Image>();
        time = player.magicTime;
        timeE = player.magicTime;
    }
	
	// Update is called once per frame
	void Update () {

        if (player.activeMagic.ToString() == "FIRE")
        {

            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = myFirstImage;

            if (timeFlag != false)
            {
                StartCoroutine(Time());
            }
            else if (timeE < 1)
            {
                this.GetComponent<Image>().enabled = false;
                player.activeMagic = Player.playerMagic.NEUTRAL;
            }
        }
        else if (player.activeMagic.ToString() == "ICE")
        {
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = mySecondImage;

            if (timeFlag != false)
            {
                StartCoroutine(Time());
            }
            else if (timeE < 1)
            {
                this.GetComponent<Image>().enabled = false;
                player.activeMagic = Player.playerMagic.NEUTRAL;
            }
        }
        
    }

    IEnumerator Time()
    {
        magicTime.fillAmount = ((timeE - 1) / time);
        timeE -= 1;
        timeFlag = false;
        yield return new WaitForSeconds(1);
        timeFlag = true;
    }
}
