using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour 
{
	public static GameTimer Instance { get; private set; }

  [SerializeField]
	private float timeRemaining = 30;
	private float maxTimeRemaining;

	private Text Text {get; set;}
	private Image Image {get; set;}

	void Awake () 
	{
		maxTimeRemaining = timeRemaining;
		Text  = GetComponentInChildren<Text> ();
		Image = GetComponentInChildren<Image> ();
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	void Update() 
	{
		timeRemaining -= Time.deltaTime;


		if (timeRemaining >= 0) 
		{
			UpdateUI ();

		} 
		else
		{
			SceneManager.LoadSceneAsync (0);

		}
	}

	void UpdateUI () 
	{
		float remainingRatio = timeRemaining / maxTimeRemaining;
		Image.fillAmount = remainingRatio;
		Text.text = string.Format ("{0:00}", timeRemaining);
	}

}