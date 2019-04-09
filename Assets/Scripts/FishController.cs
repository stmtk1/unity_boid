using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _velocity;
    public Vector3 velocity
    {
        get { return _velocity; }
    }
    private Vector3 _acceleration;
    private float _boxSize;
    private void Start(){
        _boxSize = 100f;
        _velocity = Random.onUnitSphere;
        _acceleration = Vector3.zero;
    }

    // Update is called once per frame
    private void FixedUpdate(){
        transform.position += _velocity;
        _velocity += _acceleration;
        _velocity = _velocity.normalized;
    }

    public void UpdateAcc(List<GameObject> fishes){
        _acceleration = Vector3.zero;
        _acceleration += Separation(fishes) * 1.5f;
        _acceleration += Alignment(fishes);
        _acceleration += Cohension(fishes);
        _acceleration += AvoidWall();
        _acceleration = _acceleration.normalized * 0.1f;
    }

    private Vector3 Separation(List<GameObject> fishes){
        Vector3 ret = Vector3.zero;
        foreach (var fish in fishes){
            Vector3 offset = fish.transform.position - transform.position;
            if (offset.magnitude > 5f){
                continue;
            }

            ret -= offset;
        }
        return ret.normalized;
    }
    
    private Vector3 Alignment(List<GameObject> fishes){
        Vector3 ret = Vector3.zero;
        foreach (var fish in fishes){
            Vector3 offset = fish.transform.position - transform.position;
            if (offset.magnitude > 10f){
                continue;
            }

            ret += fish.GetComponent<FishController>()._velocity;
        }
        return ret.normalized;
    }
    
    private Vector3 Cohension(List<GameObject> fishes){
        Vector3 ret = Vector3.zero;
        foreach (var fish in fishes){
            Vector3 offset = fish.transform.position - transform.position;
            if (offset.magnitude > 20f){
                continue;
            }

            ret += offset;
        }
        return ret.normalized;
    }

    private Vector3 AvoidWall(){
        return (AvoidWallX() + AvoidWallY() + AvoidWallZ()) / 5f;
    }
    
    private Vector3 AvoidVect(Vector3 vect){
        return vect / Mathf.Pow(vect.magnitude, 1f);
    }
    
    private Vector3 AvoidWallX(){
        Vector3 wall1 = new Vector3(0f, 0f, 0f) - transform.position;
        Vector3 wall2 = new Vector3(_boxSize, 0f, 0f) - transform.position;
        return AvoidVect(wall1) + AvoidVect(wall2);
    }

    
    private Vector3 AvoidWallY(){
        Vector3 wall1 = new Vector3(0f, 0f, 0f) - transform.position;
        Vector3 wall2 = new Vector3(0f, _boxSize, 0f) - transform.position;
        return AvoidVect(wall1) + AvoidVect(wall2);
    }
    
    private Vector3 AvoidWallZ(){
        Vector3 wall1 = new Vector3(0f, 0f, 0f) - transform.position;
        Vector3 wall2 = new Vector3(0f, 0f, _boxSize) - transform.position;
        return AvoidVect(wall1) + AvoidVect(wall2);
    }
}
