using UnityEngine;
using System.Collections;

public class TimerRun : MonoBehaviour
{

    public double TimeMaxSeconds = 4;
    public double InstructionsMaxSeconds = 2;

    public Transform InstructionsTransform;
    public AudioSource Sound;

    public double InstructionsTimeLeft;
    private double TimeLeft { get; set; }
    private float MaxScale { get; set; }

	public bool WeWon = false;

    /// <summary>
    /// The background of the scene
    /// </summary>
    public Transform Background;

	public Transform Character;

    /// <summary>
    /// Whether the player has pressed the right button or not
    /// </summary>
    public bool MissionComplete = false;

    /// <summary>
    /// Whether the player has irrevokably failed his mission
    /// </summary>
    public bool MissionFailed = false;

    // Use this for initialization
    void Start()
    {
		Time.timeScale = 10f;

        TimeLeft = TimeMaxSeconds;
        MaxScale = this.transform.localScale.x;

        InstructionsTimeLeft = InstructionsMaxSeconds;
    }

    private Color? oldColour = null;
    private double flashTimeLeft = 0.25f;

	public bool GameDone = false;

	private int totalUpdates = 0;

	void OnGUI()
	{
		if (WeWon)
		{
			GUI.color = Color.white;
			
			var leftStyle = GUI.skin.GetStyle("Label");
			leftStyle.alignment = TextAnchor.MiddleCenter;
			leftStyle.fontSize = 100;
			leftStyle.fontStyle = FontStyle.Bold;

			GUI.Label(new Rect(10, 10, Screen.width/2, Screen.height/2), "I WON!", leftStyle);
		}
	}

	void FixedUpdate()
	{
		if (WeWon)
			{
				return;
			}

		if (InstructionsTimeLeft > 0)
		{
			InstructionsTimeLeft -= Time.deltaTime;
			return;
		}
		
		if (MissionFailed && this.Background != null)
		{
			if (this.TimeLeft > 1)
			{
				this.TimeLeft = 1; //Reduce to 1 second
			}
			
			if (flashTimeLeft > 0)
			{
				flashTimeLeft -= Time.deltaTime;
				
				if (oldColour == null)
				{
					var newColour = this.Background.GetComponent<SpriteRenderer>().color;
					oldColour = new Color(newColour.r, newColour.g, newColour.b, newColour.a);
					
					//Red the background
					newColour.g = 0;
					newColour.r = 1;
					newColour.b = 0;
					
					this.Background.GetComponent<SpriteRenderer>().color = newColour;
				}
			}
			else
			{
				//back to nromal
				this.Background.GetComponent<SpriteRenderer>().color = oldColour.Value;
			}
		}
		
		//Hide the transform
		InstructionsTransform.gameObject.SetActive(false);
		
		if (Sound != null && !Sound.isPlaying)
		{
			Sound.Play();
		}
		
		TimeLeft -= Time.deltaTime;
		
		TimeLeft = TimeLeft < 0 ? 0 : TimeLeft;
		
		if (TimeMaxSeconds != 0)
		{
			//Check what %age of time is lost
			double percentage = TimeLeft / TimeMaxSeconds;
			
			Vector3 temp = this.transform.localScale;
			temp.x = (float)percentage * MaxScale;
			
			this.transform.localScale = temp;
		}
		
		if (TimeLeft <= 0)
		{
			//Time's up, Was the player successful?
			if (Character.transform.position.x >= 15) //Win!
			{
				//Load the next one
				//WIN!
				GameDone = false;

				//WE WON!
				WeWon = true;

				return;
				
			}
			else
			{
				//Failure
				//LOSE 
				GameDone = true;
				//Score is the total x
				PlayerPrefs.SetFloat("score",Character.transform.position.x); //Win
				PlayerPrefs.Save();
			}
			
			Application.LoadLevel(0);
			
		}
	}

    // Update is called once per frame
    void Update()
    {
       
    }
}
