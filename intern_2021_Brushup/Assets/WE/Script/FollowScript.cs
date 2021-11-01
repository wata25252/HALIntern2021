using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WE
{
	public class FollowScript : MonoBehaviour
	{
		[SerializeField] GameObject objTarget;
		[SerializeField] Vector3 offset;

		// Start is called before the first frame update
		private void Start()
		{

		}

		// Update is called once per frame
		private void Update()
		{
			Vector3 pos = objTarget.transform.localPosition;
			transform.localPosition = pos + offset;
		}
	}
}
