using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class PlayerInteractionController : MonoBehaviour {
     
	public Image crosshairDefault;
	public Image crosshairSelected;
	public GraphicRaycaster graphicRaycaster;

	void Awake ()
	{
		ToggleSelectedCursor (false);
	}

    void Update () 
    {
        PhysicsRaycasts ();
		GraphicsRaycasts ();
    }
     
	public void ToggleSelectedCursor (bool showSelectedCursor)
	{
		crosshairDefault.enabled  = !showSelectedCursor;
		crosshairSelected.enabled = showSelectedCursor;
	}


    void PhysicsRaycasts () 
    {
        Vector3 centreOfScreen = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0); 
         
        float distanceToFireRay = 20;
                 
        Ray centreOfScreenRay = Camera.main.ScreenPointToRay (centreOfScreen);
         
        RaycastHit hit;
         
        if (Physics.Raycast (centreOfScreenRay, out hit, distanceToFireRay, ~LayerMask.GetMask("SeeThrough"))) 
        {
			InteractiveObjectBase iob = hit.transform.GetComponent<InteractiveObjectBase> ();
			ToggleSelectedCursor (iob != null);
			if (iob != null
			&& Input.GetMouseButtonDown (0))
			{
				iob.OnInteraction();
			}
        }
        else
        {
			ToggleSelectedCursor (false);
        }
         
    }

	void GraphicsRaycasts() 
	{
		PointerEventData eventData = new PointerEventData (EventSystem.current);

		eventData.position = new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);

		List<RaycastResult> results = new List<RaycastResult> ();

		graphicRaycaster.Raycast (eventData, results);

		bool hitButton = false;

		if (results.Count > 0) 
		{
			for (int i = 0; i < results.Count; i++) 
			{
				Button button = results [i].gameObject.GetComponent<Button> ();

				if (button != null) 
				{
					hitButton = true;

					if (Input.GetMouseButtonDown (0)) button.onClick.Invoke ();
				} 
			}

			if (hitButton) ToggleSelectedCursor (true);
		}
	}
}