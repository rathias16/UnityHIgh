using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class StageManager : MonoBehaviour {
    public GameObject[] JewelPrefab;
    public  Terrain terrain;

    [SerializeField] private LayerMask fieldLayer;
    private LayerMask defaultFieldLayer;
    public bool removeExistingColliders = true;

    private float range ;
    private float radian ;
    private int Item;
    private int ItemNum = 100;

    // Use this for initialization
    void Start () {
       CreateInvertedMeshCollider();
        RandomItem(ItemNum);
        fieldLayer = defaultFieldLayer;
    }

    public void RandomItem(int ItemNum)
    {
        Vector3 terrainPos = terrain.GetPosition();
        TerrainData Data = terrain.terrainData;
        
        int i;
        for(i = 0;i < ItemNum; i++)
        {
            Item = Random.Range(0,5);
            range = Random.Range(-112.5f,112.5f);
            radian = (Random.Range(0, 360f) * Mathf.Deg2Rad);

            float px = Mathf.Cos(radian) * range;
            float pz = Mathf.Sin(radian) * range;
            Vector3 pos = new Vector3(px + transform.position.x, terrainPos.y + Data.size.y, pz + transform.position.z);

            RaycastHit hit;
            if(Physics.Raycast(pos,Vector3.down,out hit, terrainPos.y + Data.size.y + 100f, fieldLayer))
            {
                pos.y = hit.point.y;
              
            }
            Instantiate(JewelPrefab[Item], pos, transform.rotation);


        }
    }

    public void CreateInvertedMeshCollider()
    {
        if (removeExistingColliders)
            RemoveExistingColliders();

        InvertMesh();

        gameObject.AddComponent<MeshCollider>();
    }

    private void RemoveExistingColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            DestroyImmediate(colliders[i]);
    }

    private void InvertMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
