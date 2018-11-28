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

    private bool newMagic1 = false;
    private bool newMagic2 = false;

    private float currentFill;

    // Use this for initialization
    void Start () {
        magicTime = GetComponent<Image>();
        time = player.magicTime;
        timeE = player.magicTime;
    }
	
	// Update is called once per frame
	void Update () {
        /*
         add in player.class trigger when player actived magic, when a magic is activ
         */

        if (player.activeMagic.ToString() == "FIRE" )
        {
            newMagic1 = true;
            if (newMagic2 != false)
            {
                timeE = time;
                newMagic2 = false;
                player.iceParticles.SetActive(false);
            }

            if (player.ReFreshMagic != false)
            {
                timeE = time;
                player.ReFreshMagic = false;
            }

            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = myFirstImage;
            player.fireParticles.SetActive(true);

            if (timeFlag != false)
            {
                 StartCoroutine(duration());
                 //Invoke("duration", 1);
            }
            else if (timeE < 1)
            {

                magicTime.fillAmount = 1;
                this.GetComponent<Image>().enabled = false;
                player.beNeutral();
                timeE = time;
                newMagic1 = false;
                player.fireParticles.SetActive(false);
            }
        }
        else if (player.activeMagic.ToString() == "ICE")
        {
            newMagic2 = true;

            if (newMagic1 != false) {
                timeE = time;
                newMagic1 = false;
                player.fireParticles.SetActive(false);
            }

            if (player.ReFreshMagic != false) {
                timeE = time;
                player.ReFreshMagic = false;
            }
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = mySecondImage;
            player.iceParticles.SetActive(true);

            if (timeFlag != false)
            {
                StartCoroutine(duration());
            }
            else if (timeE < 1)
            {
                magicTime.fillAmount = 1;
                this.GetComponent<Image>().enabled = false;
                player.beNeutral();
                timeE = time;
                newMagic2 = false;
                player.iceParticles.SetActive(false);
            }
        }
        
    }

    IEnumerator duration()
    {
        magicTime.fillAmount = ((timeE - 1) / time);
        timeE -= 1;
        timeFlag = false;
        yield return new WaitForSeconds(1);
        timeFlag = true;
    }
}
