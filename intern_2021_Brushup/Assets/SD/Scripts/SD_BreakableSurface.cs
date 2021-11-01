/**
 * Copyright 2019 Oskar Sigvardsson
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

namespace SD 
{
	public class SD_BreakableSurface : MonoBehaviour {

		public MeshFilter Filter     { get; private set; }
		public MeshRenderer Renderer { get; private set; }
		public MeshCollider Collider { get; private set; }
		public Rigidbody Rigidbody   { get; private set; }

		public List<Vector2> Polygon;
		
		public float Thickness = 1.0f;
		public float MinBreakArea = 0.01f;
		public float MinImpactToBreak = 50.0f;
		
		float _Area = -1.0f;

		int age;

		// 当たったポイントからどれくらい分割するか
		private int _divideNum;

		public float Area 
		{
			get {
				if (_Area < 0.0f) {
					_Area = Geom.Area(Polygon);
				}

				return _Area;
			}
		}

		void Start() 
		{
			// 分割する数
			_divideNum = 5;

			age = 0;
			//Reload();
			
			var audio = GetComponent<AudioSource>();
			if (audio != null)
			{
				audio.Play();
			}
        }

            public void Reload() 
		{
			var pos = transform.position;

			if (Filter == null) Filter = GetComponent<MeshFilter>();
			if (Renderer == null) Renderer = GetComponent<MeshRenderer>();
			if (Collider == null) Collider = GetComponent<MeshCollider>();
			if (Rigidbody == null) Rigidbody = GetComponent<Rigidbody>();

			if (Polygon.Count == 0) {
				// Assume it's a cube with localScale dimensions
				var scale = 0.5f * transform.localScale;

				Polygon.Add(new Vector2(-scale.x, -scale.y));
				Polygon.Add(new Vector2(scale.x, -scale.y));
				Polygon.Add(new Vector2(scale.x, scale.y));
				Polygon.Add(new Vector2(-scale.x, scale.y));

				// ココの大きさ
				Thickness = 1.2f * scale.z;

				transform.localScale = Vector3.one;
			}

			var mesh = MeshFromPolygon(Polygon, Thickness);

			Filter.sharedMesh = mesh;
			Collider.sharedMesh = mesh;
		}

		void FixedUpdate() {
			var pos = transform.position;

			age++;
			if (pos.magnitude > 1000.0f) {
				DestroyImmediate(gameObject);
			}
		}

		void OnCollisionEnter(Collision coll) 
		{			
			if (age > 5 && coll.impactForceSum.magnitude > MinImpactToBreak)
			{
				Reload();
				var pnt = coll.contacts[0].point;
				Break((Vector2)transform.InverseTransformPoint(pnt));
							
				SE se = GameObject.Find("SEManager").GetComponent<SE>();
				se.Play(1);
			}			
		}

#if false 
		// 当たっている間処理
        private void OnCollisionStay(Collision collision)
        {
			if (collision.gameObject.tag == "Player")
			{
				if (age > 5 && collision.impactForceSum.magnitude > MinImpactToBreak)
				{
					var pnt = collision.contacts[0].point;
					Break((Vector2)transform.InverseTransformPoint(pnt));
				}
			}
		}
#endif
        static float NormalizedRandom(float mean, float stddev) 
		{
			var u1 = UnityEngine.Random.value;
			var u2 = UnityEngine.Random.value;

			var randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

			return mean + stddev * randStdNormal;
		}

		public void Break(Vector2 position) 
		{
			var area = Area;
			if (area > MinBreakArea) 
			{
				var calc = new VoronoiCalculator();
				var clip = new VoronoiClipper();
				// 分割するポリゴンの数
				var sites = new Vector2[_divideNum];

				for (int i = 0; i < sites.Length; i++) 
				{
					var dist = Mathf.Abs(NormalizedRandom(0.5f, 1.0f/2.0f));
					var angle = 2.0f * Mathf.PI * Random.value;

					sites[i] = position + new Vector2(dist * Mathf.Cos(angle), dist * Mathf.Sin(angle));
				}

				var diagram = calc.CalculateDiagram(sites);

				var clipped = new List<Vector2>();

				// 分割したポリゴンの数、メッシュを再生成する
				for (int i = 0; i < sites.Length; i++) 
				{
					clip.ClipSite(diagram, Polygon, i, ref clipped);

					if (clipped.Count > 0) 
					{
						var newGo = Instantiate(gameObject, transform.parent);

						newGo.transform.localPosition = transform.localPosition;
						newGo.transform.localRotation = transform.localRotation;

						// 分割後のメッシュの大きさ
						newGo.transform.localScale = transform.localScale * 0.3f;
						//newGo.transform.localScale = new Vector3(0.375f, 0.375f, 0.375f);

						var bs = newGo.GetComponent<SD_BreakableSurface>();

						if (bs != null)
						{
							bs.Thickness = Thickness;
							bs.Polygon.Clear();
							bs.Polygon.AddRange(clipped);

							var childArea = bs.Area;

							var rb = bs.GetComponent<Rigidbody>();

							rb.mass = Rigidbody.mass * (childArea / area);

							bs.GetComponent<Meteo>().Break();

							bs.GetComponent<AudioSource>().enabled = false;
						}

					}
				}

				gameObject.active = false;
				Destroy(gameObject);
			}
		}

		static Mesh MeshFromPolygon(List<Vector2> polygon, float thickness) {
			var count = polygon.Count;
			// TODO: cache these things to avoid garbage
			var verts = new Vector3[6 * count];
			var norms = new Vector3[6 * count];
			var tris = new int[3 * (4 * count - 4)];
			// TODO: add UVs

			var vi = 0;
			var ni = 0;
			var ti = 0;

			var ext = 0.5f * thickness;

			// Top
			for (int i = 0; i < count; i++) {
				verts[vi++] = new Vector3(polygon[i].x, polygon[i].y, ext);
				norms[ni++] = Vector3.forward;
			}

			// Bottom
			for (int i = 0; i < count; i++) {
				verts[vi++] = new Vector3(polygon[i].x, polygon[i].y, -ext);
				norms[ni++] = Vector3.back;
			}

			// Sides
			for (int i = 0; i < count; i++) {
				var iNext = i == count - 1 ? 0 : i + 1;

				verts[vi++] = new Vector3(polygon[i].x, polygon[i].y, ext);
				verts[vi++] = new Vector3(polygon[i].x, polygon[i].y, -ext);
				verts[vi++] = new Vector3(polygon[iNext].x, polygon[iNext].y, -ext);
				verts[vi++] = new Vector3(polygon[iNext].x, polygon[iNext].y, ext);

				var norm = Vector3.Cross(polygon[iNext] - polygon[i], Vector3.forward).normalized;

				norms[ni++] = norm;
				norms[ni++] = norm;
				norms[ni++] = norm;
				norms[ni++] = norm;
			}


			for (int vert = 2; vert < count; vert++) {
				tris[ti++] = 0;
				tris[ti++] = vert - 1;
				tris[ti++] = vert;
			}

			for (int vert = 2; vert < count; vert++) {
				tris[ti++] = count;
				tris[ti++] = count + vert;
				tris[ti++] = count + vert - 1;
			}

			for (int vert = 0; vert < count; vert++) {
				var si = 2*count + 4*vert;

				tris[ti++] = si;
				tris[ti++] = si + 1;
				tris[ti++] = si + 2;

				tris[ti++] = si;
				tris[ti++] = si + 2;
				tris[ti++] = si + 3;
			}

			Debug.Assert(ti == tris.Length);
			Debug.Assert(vi == verts.Length);

			var mesh = new Mesh();


			mesh.vertices = verts;
			mesh.triangles = tris;
			mesh.normals = norms;

			return mesh;
		}
	}
}
