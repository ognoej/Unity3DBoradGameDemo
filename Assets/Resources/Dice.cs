using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image rend;

    public int result = 0;
    public bool isCoroutine = false;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<Image>();
        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    // If you left click over the dice then RollTheDice coroutine is started
    



    public int RollResult()
    {
        IEnumerator corutim = WaitAndPrint(1.0f);
        StartCoroutine(corutim);
        return result;
    }


    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return StartCoroutine("RollDiceMan");
        print("주사위 굴림");
    }



    public void RollDiceMan()
    {   
        //gameObject.SetActive(true);
        StartCoroutine("RollTheDice");
        //gameObject.SetActive(false);
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {

        while (rend == null)
        {
            yield return new WaitForSeconds(0.05f);
        }
            isCoroutine = true;
            // Variable to contain random dice side number.
            // It needs to be assigned. Let it be 0 initially
            int randomDiceSide = 0;

            // Final side or value that dice reads in the end of coroutine
            int finalSide = 0;

            //rend.sprite = diceSides[0];
            //yield return new WaitForSeconds(0.05f);
            // Loop to switch dice sides ramdomly
            // before final side appears. 20 itterations here.
            for (int i = 0; i <= 20; i++)
            {
                // Pick up random value from 0 to 5 (All inclusive)
                randomDiceSide = Random.Range(0, 5);

                // Set sprite to upper face of dice from array according to random value           
                rend.sprite = diceSides[randomDiceSide];

                // Pause before next itteration
                yield return new WaitForSeconds(0.05f);
            }

            // Assigning final side so you can use this value later in your game
            // for player movement for example
            finalSide = randomDiceSide + 1;

            result = finalSide;

            isCoroutine = false;

        // Show final dice value in Console
        // Debug.Log(finalSide);     
        }
    
}
