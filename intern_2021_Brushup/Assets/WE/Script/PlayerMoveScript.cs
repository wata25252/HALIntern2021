using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WE
{
	public class PlayerMoveScript : MonoBehaviour
	{
		private int _moveSpeed = 10;

		// Start is called before the first frame update
		private void Start()
		{

		}

		// Update is called once per frame
		private void Update()
		{
			if (Input.GetKey(KeyCode.W))
			{
				transform.position += new Vector3(0, 0, _moveSpeed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.position += new Vector3(0, 0, -_moveSpeed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.position += new Vector3(-_moveSpeed * Time.deltaTime, 0, 0);
			}
		}
	}
}
