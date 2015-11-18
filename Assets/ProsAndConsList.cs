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
	public Text[] proConsList1;
	public Text[] proConsList2;
	public Text intentText;
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
	int numClickedPro = 0;
	int numClickedCon = 0;
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
		//currentIntention1 = "Avoiding";
		 masterProList = new List<string> ();
		masterConList = new List<string> ();
		displayText.text = displayText.text + " " + currentIntention1;
		//intentText.text = currentIntention1;
		//competing pros
		generalProConList.Add ("Asserting your positions so ideas are taken seriously.");
		generalProConList.Add ("Making quick decisions or achieving quick victory.");
		generalProConList.Add ("Protecting interests from attack.");
		masterProList.Add ("Asserting your positions so ideas are taken seriously.");
		masterProList.Add ("Making quick decisions or achieving quick victory.");
		masterProList.Add ("Protecting interests from attack.");

		//competing cons
		generalProConList.Add ("Straining work relationships as people develop resentment.");
		generalProConList.Add ("Not exchanging information freely.");
		generalProConList.Add ("Escalation conflict and creating and deadlock negotiations by using extreme tactics.");
		masterConList.Add ("Straining work relationships as people develop resentment.");
		masterConList.Add ("Not exchanging information freely.");
		masterConList.Add ("Escalation conflict and creating and deadlock negotiations by using extreme tactics.");
		
		//collaborative pros
		generalProConList.Add ("Seeking innovative solutions and creating synergy through the exchange of ideas.");
		generalProConList.Add ("Working toward meeting both people’s concerns.");
		generalProConList.Add ("Resolving problems in a relationship.");
		masterProList.Add ("Seeking innovative solutions and creating synergy through the exchange of ideas.");
		masterProList.Add ("Working toward meeting both people’s concerns.");
		masterProList.Add ("Resolving problems in a relationship.");

		
		//collaborative cons
		generalProConList.Add ("Involving a lot of time, full concentration, and creativity.");
		generalProConList.Add ("Psychologically demanding to be open to new ideas.");
		generalProConList.Add ("Causing hurt feelings by discussing sensitive issues.");
		masterConList.Add ("Involving a lot of time, full concentration, and creativity.");
		masterConList.Add ("Psychologically demanding to be open to new ideas.");
		masterConList.Add ("Causing hurt feelings by discussing sensitive issues.");

		
		
		//compromising pros
		generalProConList.Add ("Getting to a situation that is good enough.");
		generalProConList.Add ("Providing equal gains and losses for both people.");
		generalProConList.Add ("Meeting half way to reduce relationship strain.");
		masterProList.Add ("Getting to a situation that is good enough.");
		masterProList.Add ("Providing equal gains and losses for both people.");
		masterProList.Add ("Meeting half way to reduce relationship strain.");
		
		
		//compromising cons
		generalProConList.Add ("Developing feelings of frustration if issue not fully resolved.");
		generalProConList.Add ("Creating superficial understandings because  issue only partially resolved.");
		generalProConList.Add ("Diminishing the quality of decision due to less innovation in the decision.");
		masterConList.Add("Developing feelings of frustration if issue not fully resolved.");
		masterConList.Add ("Creating superficial understandings because  issue only partially resolved.");
		masterConList.Add ("Diminishing the quality of decision due to less innovation in the decision.");
		
		//avoiding pros
		generalProConList.Add ("Reducing stress by evading unpleasant people and topics.");
		generalProConList.Add ("Not stirring up problems or provoking trouble.");
		generalProConList.Add ("Gaining more time to be better prepared and be less distracted.");
		masterProList.Add ("Reducing stress by evading unpleasant people and topics.");
		masterProList.Add ("Not stirring up problems or provoking trouble.");
		masterProList.Add ("Gaining more time to be better prepared and be less distracted.");
		
		//avoiding cons
		generalProConList.Add ("Procrastinating on work because people avoid each other.");
		generalProConList.Add ("Others feeling resentful as their concerns are neglected.");
		generalProConList.Add ("People walking on eggshells instead of speaking candidly with each other.");
		masterConList.Add ("Procrastinating on work because people avoid each other.");
		masterConList.Add ("Others feeling resentful as their concerns are neglected.");
		masterConList.Add ("People walking on eggshells instead of speaking candidly with each other.");
		
		//accomodating pros
		generalProConList.Add ("Helping people meet their needs.");
		generalProConList.Add ("Supporting people and calming people down.");
		generalProConList.Add ("Building social capital by doing favors.");
		masterProList.Add ("Helping people meet their needs.");
		masterProList.Add ("Supporting people and calming people down.");
		masterProList.Add ("Building social capital by doing favors.");
		
		//accomodating cons
		generalProConList.Add ("Sacrificing own interests and views.");
		generalProConList.Add ("Responding with less motivation because agreeing to things that are not of interest.");
		generalProConList.Add ("Losing people’s respect because constantly agreeing and not challenging.");
		masterConList.Add ("Sacrificing own interests and views.");
		masterConList.Add ("Responding with less motivation because agreeing to things that are not of interest.");
		masterConList.Add ("Losing people’s respect because constantly agreeing and not challenging.");
	
		competingList[0] = masterProList[0];
		competingList[1] = masterProList [1];
		competingList[2] = masterProList [2];
		competingList[3] = masterConList [0];
		competingList[4] = masterConList [1];
		competingList[5] = masterConList [2];
		collabList[0] = masterProList [3];
		collabList[1] = masterProList [4];
		collabList[2] = masterProList [5];
		collabList[3] = masterConList [3];
		collabList[4] = masterConList [4];
		collabList[5] = masterConList [5];
		compromiseList[0] = masterProList [6];
		compromiseList[1] = masterProList [7];
		compromiseList[2] = masterProList [8];
		compromiseList[3] = masterConList [6];
		compromiseList[4] = masterConList [7];
		compromiseList[5] = masterConList [8];
		avoidList[0] = masterProList [9];
		avoidList[1] = masterProList [10];
		avoidList[2] = masterProList [11];
		avoidList[3] = masterConList [9];
		avoidList[4] = masterConList [10];
		avoidList[5] = masterConList [11];
		accomList[0] = masterProList [12];
		accomList[1] = masterProList [13];
		accomList[2] = masterProList [14];
		accomList[3] = masterConList [12];
		accomList[4] = masterConList [13];
		accomList[5] = masterConList [14];
		for (int i = 0; i < 6; i++) {
			competingList [i] = generalProConList [i];

		}

		for (int i = 0; i < 6; i++) {
			collabList [i] = generalProConList [i + 6];
		}

		for (int i = 0; i < 6; i++) {
			compromiseList [i] = generalProConList [i + 12];
		}
		for (int i = 0; i < 6; i++) {
			avoidList [i] = generalProConList [i + 18];
		}
		for (int i = 0; i < 6; i++) {
			accomList [i] = generalProConList [i + 24];
		}

		//populateScrollList (currentIntention1);
	}

	void Update(){
		if (numClickedPro == 3 && numClickedCon == 3) {

			chooseButton.SetActive(true);
		}
		else {
		//if (numClickedPro > 3  || numClickedPro < 3 || numClickedCon > 3 || numClickedCon < 3) {
			
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


	public void resetVars() {
		 currentIntention1 = "";
		 currentIntention2= "";

		for (int i = savedAnswers.Count; i > 0; i--) {

			savedAnswers.Remove(savedAnswers[i]);
		}




	}
	public void populateScrollList(string intention){
		List<string> tempList = new List<string> ();
		List<string> tempList2 = new List<string> ();
		List<string> tempPro = new List<string> ();
		List<string> tempCon = new List<string> ();
		List<string> addTempPro = new List<string> ();
		List<string> addTempCon = new List<string> ();

		tempList2 = generalProConList;
		tempPro = masterProList;
		//print ("Gen pro is " + generalProConList.Count);
		tempCon = masterConList;

		/*check the list that contains the intetnion and att it to a list of pros. Remove it from a copy of the list of the overall pros*/
		if (intention.Equals ("Competing")) {
			intentText.text = "Competing";
			addTempPro.Add(competingList[0]);
			addTempPro.Add(competingList[1]);
			addTempPro.Add(competingList[2]);
		    addTempCon.Add(competingList[3]);
			addTempCon.Add(competingList[4]);
			addTempCon.Add(competingList[5]);


		
			for (int i = 0; i < 3; i++) {

				//tempList.Add (competingList [i]);
				//tempList2.Remove (competingList[i]);

				tempPro.Remove (competingList[i]);
				tempCon.Remove (competingList[i + 3]);


			}
			gameEngine.sendIntention(competingList);
		}

		if (intention.Equals ("Accomodating")) {
			intentText.text = "Accomodating";
			addTempPro.Add(accomList[0]);
			addTempPro.Add(accomList[1]);
			addTempPro.Add(accomList[2]);
			addTempCon.Add(accomList[3]);
			addTempCon.Add(accomList[4]);
			addTempCon.Add(accomList[5]);
			
			for (int i = 0; i < 3; i++) {
				
				tempPro.Remove (accomList[i]);
				tempCon.Remove (accomList[i + 3]);

			}
			gameEngine.sendIntention(accomList);
		}
		if (intention.Equals ("Collaborating")) {
			intentText.text = "Collaborating";
			for (int i = 0; i < 3; i++) {


				addTempPro.Add(collabList[0]);
				addTempPro.Add(collabList[1]);
				addTempPro.Add(collabList[2]);
				addTempCon.Add(collabList[3]);
				addTempCon.Add(collabList[4]);
				addTempCon.Add(collabList[5]);
				
				tempPro.Remove (collabList[i]);
				tempCon.Remove (collabList[i + 3]);

			}
			gameEngine.sendIntention(collabList);
		}
		if (intention.Equals ("Avoiding")) {
			intentText.text = "Avoiding";
			addTempPro.Add(avoidList[0]);
			addTempPro.Add(avoidList[1]);
			addTempPro.Add(avoidList[2]);
			addTempCon.Add(avoidList[3]);
			addTempCon.Add(avoidList[4]);
			addTempCon.Add(avoidList[5]);
			
			for (int i = 0; i < 3; i++) {
				
				tempPro.Remove (avoidList[i]);
				tempCon.Remove (avoidList[i + 3]);
			

			}
			gameEngine.sendIntention(avoidList);
		}
		if (intention.Equals ("Compromising")) {
			intentText.text = "Compromising";
			addTempPro.Add(compromiseList[0]);
			addTempPro.Add(compromiseList[1]);
			addTempPro.Add(compromiseList[2]);
			addTempCon.Add(compromiseList[3]);
			addTempCon.Add(compromiseList[4]);
			addTempCon.Add(compromiseList[5]);
			for (int i = 0; i < 3; i++) {


				tempPro.Remove (compromiseList [i]);
				tempCon.Remove (compromiseList[i + 3]);


			}
			gameEngine.sendIntention(compromiseList);
		}

		/*for the remaining values in the pros and cons list to possibily be displayed, scroll though and assign index to it */
		for (int i = 0; i < 6; i++) {
			print (tempPro.Count);
			/*pic a number between zero and the list of remaining pros in the list */
				int ind = Random.Range(0, tempPro.Count - 1);

					/*add that index to the list */
					addTempPro.Add (tempPro[ind]);

			/*remove the one you just entered so that it doesnt show up on the list again */
					tempPro.Remove(tempPro[ind]);
			}


		for (int i = 0; i < 6; i++) {
			
			int ind = Random.Range(0, tempCon.Count - 1);
			print (tempCon.Count + " is the current tempConcount");
			addTempCon.Add (tempCon[ind]);
			tempCon.Remove(tempCon[ind]);
			//print ("Removed " + tempCon[ind] + " from list");
		}

		//print (tempList.Count - 1);
		/*for (int i = 0; i < proConsList1.Length; i++) {

			int index = Random.Range (0, tempList.Count - 1);

			proConsList1 [i].text = tempList [index];
			tempList.Remove(tempList[index]);
			
		}*/

		/*with the new list that is added go through the list and start assigning it to list of addPros/Cons*/
		for (int i = 0; i < proConsList1.Length; i++) {


			int index = Random.Range (0, addTempPro.Count - 1);
			//print ("index was " + index);
			
			proConsList1 [i].text = addTempPro[index];
			addTempPro.Remove(addTempPro[index]);
		}

		for (int i = 0; i < proConsList2.Length; i++) {

			int index = Random.Range (0, addTempCon.Count - 1);
			
			proConsList2 [i].text = addTempCon [index];
			addTempCon.Remove(addTempCon[index]);
		}
	}

	public void saveAnswer() {

		for (int i = 0; i < proConsList1.Length; i++) {
			if (proConsList1[i].color.Equals(Color.blue)) {

				savedAnswers.Add (proConsList1[i].text);
				print (savedAnswers.Count +  " saved Answers Count");


			}
		}

		for (int i = 0; i < proConsList2.Length; i++) {
			if (proConsList2[i].color.Equals(Color.blue)) {
				
				savedAnswers.Add (proConsList2[i].text);
				
				print (savedAnswers.Count +  " saved Answers Count");
				
			}
		}
	
		gameEngine.sendAnswers (savedAnswers);
	}
	


	public void changeTheColor(Text t) {
		if (t.color.Equals (Color.black)) {
			t.color = Color.blue;

			if(proConsList1.Contains(t)) {
				numClickedPro++;
			}
			else {
				numClickedCon++;
			}
		}
		else {

			if (t.color.Equals (Color.blue)) {
				t.color = Color.black;
				if(proConsList1.Contains(t)) {
					numClickedPro--;
				}
				else {
					numClickedCon--;
				}
			}


		}
	}



}
