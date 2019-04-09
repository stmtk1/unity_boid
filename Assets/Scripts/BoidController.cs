using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BoidController : MonoBehaviour
{
    private List<GameObject> _fishes;
    public GameObject prefab;
    // Start is called before the first frame update
    private float _boxSize;
    void Start(){
        _fishes = new List<GameObject>();
        _boxSize = 100f;
        for (int i = 0; i < 200; ++i){
            Vector3 position = new Vector3(Random.Range(0, _boxSize), Random.Range(0, _boxSize), Random.Range(0, _boxSize));
            GameObject newFish = Instantiate(prefab, position, Quaternion.identity);
            _fishes.Add(newFish);
        }
    }

    // Update is called once per frame
    void Update(){
        foreach (GameObject fish in _fishes){
            fish.GetComponent<FishController>().UpdateAcc(_fishes);
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * _boxSize);
    }
}
