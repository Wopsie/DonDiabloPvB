using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// contains all the data of the object to be added to the batch
/// </summary>
public class ObjData{
    public string name;
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix{
        get { return Matrix4x4.TRS(pos, rot, scale); }
    }

    public ObjData(Transform trans){
        this.name = trans.name;
        //this.pos = trans.position;
        this.pos = Vector3.one;
        //this.scale = trans.localScale;
        this.scale = Vector3.one * 100;
        this.rot = trans.rotation;
    }
}

/// <summary>
/// contains all the object data to be rendered that call
/// </summary>
public class Batch{
    public Mesh mesh;
    public Material mat;
    public List<ObjData> renderData = new List<ObjData>();

    public Batch(Mesh mesh, Material mat){
        this.mesh = mesh;
        this.mat = mat;
    }

    public void AddObj(ObjData obj){
        this.renderData.Add(obj);
    }
}

public class GPUInstancing : MonoBehaviour {
    private List<List<ObjData>> batches = new List<List<ObjData>>();

    private Dictionary<string, Batch> batchCollection = new Dictionary<string, Batch>();

    //make functionality for adding meshes & materials to the overall stack to batch
    /*
    private void Start(){
        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();
        for (int i = 0; i < instances; i++){
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= 1000){
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
    }

    private void Update(){
        RenderBatches();
    }
    */

    private void Update()
    {
        RenderBatches();
    }

    /// <summary>
    /// add object reference to material and mesh
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="pos"></param>
    public void AddObject(Transform trans, Vector3 pos){
        Debug.Log(batchCollection.Count);
        //trans.position = pos;
        Batch tempBatch = null;
        bool addNewBatch = false;
        //check if object already exhists somewhere in batch and if the maximum limit of objects of the batch is not exceeded
        foreach (KeyValuePair<string,Batch> b in batchCollection){
            if(b.Value.renderData[0].name == trans.name && b.Value.renderData.Count <= 999){
                //add the object to this batch
                b.Value.AddObj(new ObjData(trans));
                Debug.Log("Add to existing batch");
                return;
            }else{
                //add the object to a new batch & add that batch to the collection
                //because you cant alter a dictionairy while looping through it, we store the value we need to add, 
                //and add it to the dict after the loop is over
                tempBatch = new Batch(trans.GetComponent<MeshFilter>().sharedMesh, trans.GetComponent<Renderer>().sharedMaterial);
                addNewBatch = true;
                //Debug.Log("Add to new batch");
            }
        }

        if (addNewBatch){
            tempBatch.AddObj(new ObjData(trans));
            batchCollection.Add(tempBatch.renderData[0].name, tempBatch);
            Debug.Log("Add stored batch");
            return;
        }

        Batch firstBatch = new Batch(trans.GetComponent<MeshFilter>().sharedMesh, trans.GetComponent<Renderer>().sharedMaterial);
        firstBatch.AddObj(new ObjData(trans));
        batchCollection.Add(firstBatch.renderData[0].name, firstBatch);
        Debug.Log("very first batch");

        //check if material/transform already exhists in a batch
        //if not add to a new batch

        //else add the to the already exhisting batch
    }

    private void RenderBatches(){
        foreach (var batch in batchCollection){
            Debug.Log("Drawing batch " + batch.Value.mesh + " " + batch.Value.mat.name);
            Graphics.DrawMeshInstanced(batch.Value.mesh, 0, batch.Value.mat, batch.Value.renderData.Select((x) => x.matrix).ToList());
        }
    }

    /*
    private void AddObj(List<ObjData> currBatch, int i){
        Vector3 position = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));
        currBatch.Add(new ObjData(position, new Vector3(10, 10, 10), Quaternion.identity));
    }
    */
    /*
    private List<ObjData> BuildNewBatch(){
        return new List<ObjData>();
    }

    private void RenderBatches(){
        foreach (var batch in batches){
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
        }
    }
    */
    //Render the object data in batches
    //batch is a collection of object data with the same material and mesh
    //store each batch and render in order
    //store each objdata entry inside a batch

    //Sort batches in dictionairy depending on Key.
    //key is name of the transforms in it
    //the objdata contains a transform that holds a name

}
