using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RenderObj{
    public Transform parent;
    public Vector3 localPosition;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix{
        get { return Matrix4x4.TRS(GetPos, rot, scale); }
    }

    public RenderObj(Vector3 localPosition, Vector3 scale, Quaternion rot, Transform parent = null){
        this.localPosition = localPosition;
        this.scale = scale;
        this.rot = rot;
        this.parent = parent;
    }

    private Vector3 GetPos{
        get{
            Vector3 _position = localPosition;
            if (parent != null){
                _position = parent.TransformPoint(_position);
            }
            return _position;
        }
    }
}

public class Batch{
    public Mesh objMesh;
    public Material objMat;
    public List<RenderObj> ObjDatas;
}

/// <summary>
/// GPUInstace uses GPU on mobile phone if one exists.
/// Gets you higher frames and a better experience.
/// </summary>
public class GPUInstancer : MonoBehaviour{
    public static GPUInstancer Instance { get { return GetInstance(); } }

    #region Singleton
    private static GPUInstancer instance;

    private static GPUInstancer GetInstance(){
        if (instance == null){
            instance = FindObjectOfType<GPUInstancer>();
        }
        return instance;
    }
    #endregion

    [SerializeField,HideInInspector]
    private Dictionary<string, Batch> batchesByName = new Dictionary<string, Batch>();

    void Update(){
        if (batchesByName.Count > 0){
            RenderAllBatches();
        }
    }

    public RenderObj AddObjTrans(Transform trans, Transform parentTrans = null){
        //Create new ObjData with the passed data. Then check what to do with batching
        RenderObj addedObjData = new RenderObj(trans.position, trans.localScale, trans.rotation, (parentTrans != null) ? parentTrans : trans);
        Debug.Log(trans.name);
        //check if this object has already been batched or if relevant batch is full
        if (batchesByName.ContainsKey(trans.name)){
            //object batch already exists. Check if it is not overflowing
            if (batchesByName[trans.name].ObjDatas.Count < 1000){
                batchesByName[trans.name].ObjDatas.Add(addedObjData);
            }else{
                CreateNewBatch(addedObjData, trans);
            }
        }else{//create new batch for the object
            CreateNewBatch(addedObjData, trans);
        }
        return addedObjData;
    }

    /// <summary>
    /// Create a new batch with the given object data & transform
    /// </summary>
    /// <param name="initObj"></param>
    /// <param name="initTrans"></param>
    private void CreateNewBatch(RenderObj initObj, Transform initTrans){
        //create the new batch with passed initial values
        Batch batch = new Batch{
            ObjDatas = new List<RenderObj>{
                initObj,
            },
            objMesh = initTrans.GetComponent<MeshFilter>().sharedMesh,
            objMat = initTrans.gameObject.GetComponent<MeshRenderer>().sharedMaterial,
        };
        //add new batch the list of batches
        batch.ObjDatas.Add(initObj);
        batchesByName.Add(initTrans.name, batch);
    }

    private void RenderAllBatches(){
        foreach (KeyValuePair<string, Batch> batch in batchesByName){
            Graphics.DrawMeshInstanced(batch.Value.objMesh, 0, batch.Value.objMat, batch.Value.ObjDatas.Select(a => a.matrix).ToList());
        }
    }
}