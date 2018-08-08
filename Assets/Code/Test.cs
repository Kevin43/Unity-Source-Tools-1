using System.Collections.Generic;
using System.IO;
using System.Linq;
using uSrcTools;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Test))]
public class TestEditor : Editor
{
	//string mapName;
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		//mapName = EditorGUILayout.TextField ("Map Name", "");
		if (GUILayout.Button ("Load Map"))
		{
			Test.Inst.bsp.Load (Test.Inst.mapName);
		}
	}
}

namespace uSrcTools
{
	public class Test : MonoBehaviour
	{
		public static Test Inst;

		public SourceBSPLoader bsp;
		public SourceStudioModel model;
		public Material testMaterial;
		public Texture cameraTexture;

		public string exportLocation = "D:\\uSource\\Example\\";
		public string mapName;
		public string modelName;
		public bool skinnedModel = false;
		public Transform player;
		public Transform playerCamera;
		public Transform skyCamera;
		public Light light_environment;
		public Vector3 skyCameraOrigin;
		public Vector3 startPos;

		public bool loadMap = true;
		public bool loadModel = false;
		public bool exportMap = false;
		public bool isL4D2 = false;
		public bool forceHDR = false;
		public bool skipSky = true;

		void Awake ()
		{
			Inst = this;
		}

		void Start ()
		{
			//player.transform.position = GameObject.Find ("info_player_start").transform.position;

			if (loadMap)
			{
				if (bsp == null)
					bsp = GetComponent<SourceBSPLoader> ();

				// When game starts, it goes to SourceBSPLoader and the Load function.
				bsp.Load (mapName);
				if (exportMap)
				{
					COLLADAExport.Geometry g = bsp.map.BSPToGeometry ();
					print ("Exporting map.");
					//COLLADAExport.Export(@"I:\uSource\test\"+mapName+".dae",g,false,false);
					COLLADAExport.Export (exportLocation + mapName + ".dae ", g, false, false);
				}
			}

			if (loadModel)
			{
				GameObject modelObj = new GameObject ("TestModel ");
				model.Load (@"models / " + modelName + ".mdl ");
				//model.GetInstance(modelObj,skinnedModel);
				model.GetInstance (modelObj, skinnedModel, 0);
				//modelObj.transform.localEulerAngles=new Vector3(270,0,0);
			}
		}

		void Update ()
		{
			//BSP.DrawDebugObjects (player.position);
		}

		void OnDrawGizmos ()
		{
			if (model != null)
				model.OnDrawGizmos ();
		}
	}

}