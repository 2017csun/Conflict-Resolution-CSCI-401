using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;

public class ProsAndConsList : MonoBehaviour {

	//-------------------------------------------
	// Pros and Cons Variables
	//-------------------------------------------
	[Header("Pros and Cons Variables")]
	public 	GameObject scrollview;
	public GameEngine gameEngine;
	public Text displayText;
	public string currentIntention1;
	public string currentIntention2;
	public Text[] proConsList;
	public Button[] buttons;
	public List<string> savedAnswers;
	public  List<string> generalProConList;
	public List<string> masterProList;
	public List<string> masterConList;
	public string[] competingList;
	public string[] accomList;
	public string[] collabList;
	public string[] avoidList;
	public string[] compromiseList;
	public GameObject chooseButton;


	public Vector2 scr = Vector2.zero;
	int numClicked = 0;
	//private string innerText = "Found me!";
	void Start() {
		//Initialize the ConflictStyle/IntentionLists
		generalProConList = new List<string> ();
		savedAnswers = new List<string> ();
		competingList = new string[6];
		accomList = new string[6];
		collabList = new string[6];
		avoidList = new string[6];
		compromiseList = new string[6];
		currentIntention1 = "avoiding";
		List<string> masterProList = new List<string> ();
		List<string> masterConList = new List<string> ();
		displayText.text = displayText.text + " " + currentIntention1;

		//competing pros
		generalProConList.Add ("Asserting your positions so ideas are taken seriously");
		generalProConList.Add ("Making quick decisions or achieving quick victory");
		generalProConList.Add ("Protecting interests from attack");
		//competing cons
		generalProConList.Add ("Straining work relationships as people develop resentment");
		generalProConList.Add ("Not exchanging information freely");
		generalProConList.Add ("Creating escalation and deadlock negotiations by using extreme tactics");
		
		//collaborative pros
		generalProConList.Add ("Seeking innovative solutions and creating synergy through the exchange of ideas");
		generalProConList.Add ("Working toward meeting both people’s concerns");
		generalProConList.Add ("Resolving problems in a relationship");
		
		//collaborative cons
		generalProConList.Add ("Involving a lot of time, full concentration, and creativity");
		generalProConList.Add ("Psychologically demanding to be open to new ideas");
		generalProConList.Add ("Working through sensitive issues and resolve hurt feelings");
		
		
		//compromising pros
		generalProConList.Add ("Getting to a deal that is good enough");
		generalProConList.Add ("Providing equal gains and losses for both people");
		generalProConList.Add ("Meeting half way to reduce relationship strain");
		
		
		//compromising cons
		generalProConList.Add ("Developing feelings of frustration if issue not fully resolved");
		generalProConList.Add ("Creating superficial understandings because  issue only partially resolved");
		generalProConList.Add ("Diminishing the quality of decision due to less innovation in the decision");
		
		//avoiding pros
		generalProConList.Add ("Reducing stress by evading unpleasant people and topics");
		generalProConList.Add ("Not stirring up problems or provoking trouble");
		generalProConList.Add ("Gaining more time to be better prepared and be less distracted");
		
		//avoiding cons
		generalProConList.Add ("Procrastinating on work because people avoid each other");
		generalProConList.Add ("Others feeling resentful as their concerns are neglected");
		generalProConList.Add ("People walking on eggshells instead of speaking candidly with each other");
		
		//accomodating pros
		generalProConList.Add ("Helping people meet their needs");
		generalProConList.Add ("Supporting people and calming people down");
		generalProConList.Add ("Building social capital by doing favors");
		
		//accomodating pros
		generalProConList.Add ("Sacrificing own interests and views");
		generalProConList.Add ("Responding with less motivation because agreeing to things that are not of interest");
		generalProConList.Add ("Losing people’s respect because constantly agreeing and not challenging");

		for (int i = 0; i < 6; i++) {
			competingList [i] = generalProConList [i];
		}

		for (int i = 0; i < 6; i++) {
			accomList [i] = generalProConList [i + 6];
		}

		for (int i = 0; i < 6; i++) {
			collabList [i] = generalProConList [i + 12];
		}
		for (int i = 0; i < 6; i++) {
			avoidList [i] = generalProConList [i + 18];
		}
		for (int i = 0; i < 6; i++) {
			compromiseList [i] = generalProConList [i + 24];
		}
		populateScrollList (currentIntention1);
	}

	void Update(){
		if (numClicked == 6) {

			chooseButton.SetActive(true);
		}

		if (numClicked > 6  || numClicked < 6) {
			
			chooseButton.SetActive(false);
		}


	}
	
	/*public void OnGUI () {
		//		scr = GUI.HorizontalScrollbar (new Rect (600, 350, 100, 30),hScrollbarValue, 1.0f, 0.0f, 10.0f );


		proConsList[0].text = GUI.Button (new Rect (0, 0, 200, 200), proConsList[0].text);
		
		
		
		
		
		
		scr = GUI.BeginScrollView(new Rect(600, 400,200,200), scr, new Rect(0,0,190,400));
		//innerText = GUI.TextArea (new Rect (0,0,200,200), innerText);
		
		
		GUI.EndScrollView ();
		
	}*/
	public void populateScrollList(string intention){
		List<string> tempList = new List<string> ();
		List<string> tempList2 = new List<string> ();



		tempList2 = generalProConList;

		if (intention.Equals ("competing")) {

			for (int i = 0; i < 6; i++) {

				tempList.Add (competingList [i]);
				tempList2.Remove (competingList[i]);
				gameEngine.sendIntention(competingList);
			}

		}

		if (intention.Equals ("accomodating")) {
			
			for (int i = 0; i < 6; i++) {
				
				tempList.Add (accomList [i]);
				tempList2.Remove (accomList[i]);
				gameEngine.sendIntention(accomList);
			}
			
		}
		if (intention.Equals ("collaborating")) {
			
			for (int i = 0; i < 6; i++) {
				
				tempList.Add (collabList [i]);
				tempList2.Remove (collabList[i]);
				gameEngine.sendIntention(collabList);
			}
			
		}
		if (intention.Equals ("avoiding")) {
			
			for (int i = 0; i < 6; i++) {
				
				tempList.Add (avoidList [i]);
				tempList2.Remove (avoidList[i]);
				gameEngine.sendIntention(avoidList);
			}
			
		}
		if (intention.Equals ("compromising")) {
			
			for (int i = 0; i < 6; i++) {
				
				tempList.Add (compromiseList [i]);
				tempList2.Remove (compromiseList[i]);
				gameEngine.sendIntention(compromiseList);
			}
			
		}

		for (int i = 0; i < 9; i++) {

				int ind = Random.Range(0, tempList2.Count - 1);

					tempList.Add (tempList2[ind]);
				tempList2.Remove(tempList2[ind]);
			}

		//print (tempList.Count - 1);
		for (int i = 0; i < proConsList.Length; i++) {

			int index = Random.Range (0, tempList.Count - 1);

			proConsList [i].text = tempList [index];
			tempList.Remove(tempList[index]);
			
		}
		
	}

	public void saveAnswer() {

		for (int i = 0; i < proConsList.Length; i++) {
			if (proConsList[i].color.Equals(Color.blue)) {

				savedAnswers.Add (proConsList [i].text);



			}
		}
		gameEngine.sendAnswers (savedAnswers);
	}
	


	public void changeTheColor(Text t) {
		if (t.color.Equals (Color.black)) {
			t.color = Color.blue;
				numClicked++;
		}
		else {

			if (t.color.Equals (Color.blue)) {
				t.color = Color.black;
					numClicked--;
			}


		}
	}



}
