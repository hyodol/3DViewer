using UnityEngine;

public class Controller : MonoBehaviour
{
		//float var;

		void Update() 
		{
				int r = Random.Range (0, 10);
				if (r <= 3) {
						this.GetComponent<Animator> ().SetTrigger ("action");
				}
						}
}
		