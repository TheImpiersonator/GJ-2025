using UnityEditor; //Allows for accessing editor specifics, needed because this is an editor script
using UnityEngine;

/*======| NOTE |=====
    - Did utilize ChatGPT to help learn Editor-specific methods and how to properly destroy objects for OnValidate()
 */



public class PrefabPlacementTool : MonoBehaviour
{
    /*=====| TOOL SETTINGS |=====*/
    [SerializeField] GameObject meshPrefab; // The mesh prefab to place

    //___Placement Area/Count
    [SerializeField] bool useVolume = true; // Toggle between volume-based or count-based placement
    [SerializeField] Vector3 placementVolume = new Vector3(10f, 0f, 10f); // Size of the placement area
    [SerializeField] int meshCount = 10; // Number of meshes for count-based placement

    //___Spacing
    [SerializeField] Vector3 meshSpacing = new Vector3(1f, 0f, 1f); // Spacing between meshes

    //___Rotation
    [SerializeField] Vector3 meshRotation = Vector3.zero; // Default rotation for each mesh
    [SerializeField] bool randomizeRotation = false; // Toggle for random rotation
    [SerializeField] Vector2 randomRotationRange = new Vector2(0f, 359f); // Range for random rotation

    bool isProcessing = false;

    /*=====| SCHEDULE FUNCTIONS |=====*/
    //When the widgets in the editor is drawns
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, placementVolume);

    }

    private void OnValidate()
    {
        if (isProcessing) { return; } //Leave if we're still processing the edits made in the tool 

        if (meshPrefab == null)
        {
            Debug.Log("No Prefab Set for PlacementTool");
            return;
        }
        
        EditorApplication.delayCall += () =>
        {
            if (this == null) { return; }// Returns if somehow the tool got destroyed mid generation
            ClearChildren();    //Clear the Current Children to make way for new generation
            GenerateChildren(); //Genereate new meshes that met the settings of the tool

            //Finished Processing
            isProcessing = false;
        };
    }

    void ClearChildren()
    {
        //LOOP: reverse iterate from the last counted child to destroy each children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

    }

    void GenerateChildren()
    {
        /*____VOLUME INSTANTIATION____*/
        //CHECKL: Using Volume to place gameobjects
        if (useVolume)
        {
            Vector3 start = transform.position - placementVolume / 2;

            int xCount = Mathf.FloorToInt(placementVolume.x / meshSpacing.x);
            int zCount = Mathf.FloorToInt(placementVolume.z / meshSpacing.z);

            for (int x = 0; x < xCount; x++)
            {
                for (int z = 0; z < zCount; z++)
                {
                    Vector3 position = start + new Vector3(x * meshSpacing.x, 0, z * meshSpacing.z);
                    Quaternion rotation = GetRotation();
                    InstantiateChild(position, rotation);
                }
            }
        }
        else
        {
            for (int i = 0; i < meshCount; i++)
            {
                Vector3 position = transform.position + new Vector3(i * meshSpacing.x, 0, 0);
                Quaternion rotation = GetRotation();
                InstantiateChild(position, rotation);
            }
        }
    }

    Quaternion GetRotation()
    {
        if (randomizeRotation)
        {
            float randomY = Random.Range(randomRotationRange.x, randomRotationRange.y);
            return Quaternion.Euler(new Vector3(meshRotation.x, randomY, meshRotation.z));
        }
        return Quaternion.Euler(meshRotation);
    }

    void InstantiateChild(Vector3 position, Quaternion rotation)
    {
        if (!meshPrefab) return;

        GameObject obj = Instantiate(meshPrefab, position, rotation, transform);

        // Ensure the object has a collider
        if (!obj.GetComponent<Collider>())
        {
            obj.AddComponent<BoxCollider>();
        }
    }

}