using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WE
{
	public class NotRotateScript : MonoBehaviour
	{
		// Start is called before the first frame update
		private void Start()
		{

		}

		// Update is called once per frame
		private void Update()
		{
			transform.eulerAngles = new Vector3(45, 0, 0);
		}
	}
}
