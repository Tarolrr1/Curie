//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Curie
{
	public class BoundsRenderer : MonoBehaviour
	{
		Vessel v;
		bool inited = false;

		public void Init(Vessel v_){
			v = v_;
		}

		void Update(){
			if(inited) return;
			if(v.loaded){
				var lBounds = new List<Bounds>();
				foreach(Part p in v.parts){
					var meshes = new List<MeshFilter>(p.FindModelComponents<MeshFilter>());

					foreach(MeshFilter mf in meshes){
						var line = new GameObject("Line");
						var lr = line.AddComponent<LineRenderer>();
						line.transform.SetParent(v.vesselTransform);
						line.transform.localPosition = Vector3.zero;
						line.transform.localRotation = Quaternion.identity;
						lr.material = new Material( Shader.Find( "Particles/Additive" ) );
						lr.SetVertexCount(17);
						lr.useWorldSpace = false;
						lr.SetColors(Color.green,Color.green);
						lr.SetWidth(0.004f,0.004f);
						CalcPositons(mf.mesh.bounds, mf.transform);
						for(int i=0; i<17; i++)
							lr.SetPosition(i,boxVert[i]);

						Vector3 tmp1 = mf.mesh.bounds.min, tmp2 = mf.mesh.bounds.max;
						Bounds tmp3 = new Bounds();
						tmp1 = v.vesselTransform.InverseTransformPoint(mf.transform.TransformPoint(tmp1));
						tmp2 = v.vesselTransform.InverseTransformPoint(mf.transform.TransformPoint(tmp2));
						tmp3.SetMinMax(tmp1,tmp2);
						lBounds.Add(tmp3);
					}
				}
				inited = true;
				var b = new Bounds();
				foreach(Bounds b_ in lBounds)
					b.Encapsulate(b_);
				var line1 = new GameObject("Line");
				var lr1 = line1.AddComponent<LineRenderer>();
				line1.transform.SetParent(v.vesselTransform);
				line1.transform.localPosition = Vector3.zero;
				line1.transform.localRotation = Quaternion.identity;
				lr1.material = new Material( Shader.Find( "Particles/Additive" ) );
				lr1.SetVertexCount(17);
				lr1.useWorldSpace = false;
				lr1.SetColors(Color.green,Color.green);
				lr1.SetWidth(0.004f,0.004f);
				CalcPositons(b, v.vesselTransform);
				for(int i=0; i<17; i++)
					lr1.SetPosition(i,boxVert[i]);
			}
		}

		public Color color = Color.green;
		private Vector3[] boxVert = new Vector3[17];
		
		void CalcPositons(Bounds b, Transform t){

			Vector3 v3Center = b.center;
			Vector3 v3Extents = b.extents;

			Vector3 v3FTL = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);
			Vector3 v3FTR = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);
			Vector3 v3FBL = b.min;
			Vector3 v3FBR = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);
			Vector3 v3BTL = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);
			Vector3 v3BTR = b.max;
			Vector3 v3BBL = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);
			Vector3 v3BBR = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);

			boxVert[0]  = v3FTL; boxVert[1]  = v3FTR; boxVert[2]  = v3FBR; boxVert[3]  = v3FBL; boxVert[4]  = v3FTL;
			boxVert[5]  = v3BTL; boxVert[6]  = v3BTR; boxVert[7]  = v3FTR; boxVert[8]  = v3FBR; boxVert[9]  = v3BBR;
			boxVert[10] = v3BTR; boxVert[11] = v3BTL; boxVert[12] = v3BBL; boxVert[13] = v3FBL; boxVert[14] = v3FBR;
			boxVert[15] = v3BBR; boxVert[16] = v3BBL;

			for(int i=0; i<17; i++)
				boxVert[i] = v.vesselTransform.InverseTransformPoint(t.TransformPoint(boxVert[i]));
		}
	}
}

