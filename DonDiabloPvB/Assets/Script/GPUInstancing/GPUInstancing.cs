using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjData{
    public Transform parent;
    public Vector3 localPosition;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix{
        get { return Matrix4x4.TRS(GetPos, rot, scale); }
    }

    public ObjData(Vector3 localPosition, Vector3 scale, Quaternion rot, Transform parent = null){
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

    public List<ObjData> ObjDatas;
}

public class GPUInstancing : MonoBehaviour{
    public static GPUInstancing Instance { get { return GetInstance(); } }

    #region Singleton
    private static GPUInstancing instance;

    private static GPUInstancing GetInstance(){
        if (instance == null){
            instance = FindObjectOfType<GPUInstancing>();
        }
        return instance;
    }
    #endregion

    [SerializeField]
    private Dictionary<string, Batch> batchesByName = new Dictionary<string, Batch>();

    void Update(){
        if (batchesByName.Count > 0){
            RenderBatches();
        }
    }

    public ObjData AddObjTrans(Transform trans, Transform parentTrans = null){
        //Create new ObjData with the passed data. Then check what to do with batching
        //ObjData addedObjData = (useTrans == true) ? new ObjData(trans.position, trans.localScale, trans.rotation, parentTrans) : new ObjData(pos, scale, rot, parentTrans);
        ObjData addedObjData = new ObjData(trans.position, trans.localScale, trans.rotation, (parentTrans != null) ? parentTrans : trans);
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

    public void RemoveObjByParent(Transform parentTrans){
        List<string> _keysToRemove = new List<string>();

        foreach (KeyValuePair<string, Batch> _batchByStringPair in batchesByName){
            List<ObjData> _objectDatas = _batchByStringPair.Value.ObjDatas;

            for (int i = _objectDatas.Count - 1; i >= 0; i--){
                ObjData _objData = _objectDatas[i];
                if (_objData.parent != parentTrans) { continue; }
                _objectDatas.RemoveAt(i);
            }

            if (_objectDatas.Count <= 0){
                _keysToRemove.Add(_batchByStringPair.Key);
            }
        }

        foreach (string _key in _keysToRemove){
            batchesByName.Remove(_key);
        }
    }

    /// <summary>
    /// Create a new batch with the given object data & transform
    /// </summary>
    /// <param name="initObj"></param>
    /// <param name="initTrans"></param>
    private void CreateNewBatch(ObjData initObj, Transform initTrans){
        //create the new batch with passed values
        Batch batch = new Batch{
            ObjDatas = new List<ObjData>{
                initObj,
            },
            objMesh = initTrans.GetComponent<MeshFilter>().sharedMesh,
            objMat = initTrans.gameObject.GetComponent<MeshRenderer>().sharedMaterial,
        };
        //not sure if this creates a duplicate
        batch.ObjDatas.Add(initObj);
        //add new batch the list of batches
        batchesByName.Add(initTrans.name, batch);
    }

    private void RenderBatches(){
        foreach (KeyValuePair<string, Batch> batch in batchesByName){
            Debug.Log("Drawing batch: " + batch.Value.ObjDatas);
            Graphics.DrawMeshInstanced(batch.Value.objMesh, 0, batch.Value.objMat, batch.Value.ObjDatas.Select(a => a.matrix).ToList());
        }
    }
}