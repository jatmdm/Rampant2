using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class statScreen : MonoBehaviour {

	public GameObject UI;
	public Sprite defSpr;
	public GameObject defGem;

	// Use this for initialization
	void Start () {
		//UpdateUI ();
	}

	public void UpdateUI()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		UI.transform.FindChild ("Stats").FindChild ("V").GetComponent<Text> ().text = "Vit\n" + player.GetComponent<AdventurerStats> ().dVit;
		UI.transform.FindChild ("Stats").FindChild ("W").GetComponent<Text> ().text = "Wit\n" + player.GetComponent<AdventurerStats> ().dWit;
		UI.transform.FindChild ("Stats").FindChild ("P").GetComponent<Text> ().text = "Power\n" + player.GetComponent<AdventurerStats> ().dPower;


		for(int i=0; i < 9; i++)
		{
			UI.transform.FindChild ("GemScroll").FindChild("Image "+(i+1).ToString()).GetComponent<Image>().sprite = null;//defSpr;
		}

		int count = 0;
		foreach(GameObject g in player.GetComponent<AdventurerStats>().gemInventory)
		{
			UI.transform.FindChild ("GemScroll").FindChild("Image "+(count+1).ToString()).GetComponent<Image>().sprite = g.GetComponent<Gem>().sprite;
			UI.transform.FindChild ("GemScroll").FindChild("Image "+(count+1).ToString()).GetComponent<buttonInfo>().GEM = g;
			UI.transform.FindChild ("GemScroll").FindChild("Image "+(count+1).ToString()).GetComponent<buttonInfo>().index = count;
			count++;
		}

		for(int i=0; i < 3; i++)
		{
			UI.transform.FindChild ("lace").FindChild("Image "+(i+1).ToString()).GetComponent<Image>().sprite = defSpr;
		}
		count = 0;
		foreach(GameObject g in player.GetComponent<AdventurerStats>().currentGems)
		{
			UI.transform.FindChild ("lace").FindChild("Image "+(count+1).ToString()).GetComponent<Image>().sprite = g.GetComponent<Gem>().sprite;
			UI.transform.FindChild ("lace").FindChild("Image "+(count+1).ToString()).GetComponent<buttonInfo>().GEM = g;
			UI.transform.FindChild ("lace").FindChild("Image "+(count+1).ToString()).GetComponent<buttonInfo>().index = count;
			count++;
		}
	}

	public void releaseHim(GameObject button)
	{
		button.GetComponent<Image> ().sprite = defSpr;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<AdventurerStats> ().equipGem (button.GetComponent<buttonInfo> ().GEM);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<AdventurerStats> ().removeGem (button.GetComponent<buttonInfo> ().index);
	}
	public void addHim(GameObject button)
	{
		button.GetComponent<Image> ().sprite = defSpr;
		bool poo = GameObject.FindGameObjectWithTag ("Player").GetComponent<AdventurerStats> ().equipGem2 (button.GetComponent<buttonInfo> ().GEM);
		if(poo) GameObject.FindGameObjectWithTag ("Player").GetComponent<AdventurerStats> ().removeGem2 (button.GetComponent<buttonInfo> ().index);
	}

	// Update is called once per frame
	void Update () {
		//UpdateUI ();
	}
}
