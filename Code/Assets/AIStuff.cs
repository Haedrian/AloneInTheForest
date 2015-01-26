using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using System.Linq;

public class AIStuff : MonoBehaviour {

	public int Lenght = 700;
	public int MaximumTotal = 100;

	public static int testingNumber = 0;

	// Use this for initialization
	void Start () 
	{
			PotentialSolution ps = new PotentialSolution();
			ps.GenerateRandom();

		PlayerPrefs.SetString ("Instructions",string.Join(",",ps.KeyCodes.Select(kc => kc.ToString()).ToArray()));
			PlayerPrefs.Save();

			Application.LoadLevel(1);
	}



//		if (Solutions.Count == 0)
//		{
//			for (int i=0; i < MaximumTotal; i++)
//			{
//				PotentialSolution ps = new PotentialSolution();
//				ps.GenerateRandom();
//
//				//Create a 100
//				Solutions.Add(ps);
//			}
//		}
//
//		//Put in the result
//
//		Solutions [testingNumber].Score = PlayerPrefs.GetFloat("score");
//
//		if (testingNumber >= Solutions.Count +1)
//		{
//			//Mutate and stuff
//			testingNumber = 0;
//		}
//
//		//Create 100 random ones and test them
//		if (testingNumber < Solutions.Count)
//		{
//			testingNumber++;
//
//			PlayerPrefs.SetString ("Instructions",string.Join(",",Solutions[testingNumber].KeyCodes.Select(kc => kc.ToString()).ToArray()));
//			PlayerPrefs.Save();
//
//			Application.LoadLevel(1);
//		}
//
//	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
