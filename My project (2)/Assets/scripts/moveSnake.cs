//Authors
//Cian O'Toole <B00143633>
//Conor Donovan <B00134690>
//
//IMM
//
//Snake Game 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSnake : MonoBehaviour {

    public float speed = 5;
    public float rotSpeed = 180;
    public int gap = 30;
    
    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionsHistory = new List<Vector3>();

    public GameObject bodyPrefab;
    public GameObject food;
    
    // Start is called before the first frame update
    void Start() {
        ChangeFoodPos();            
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    // Update is called once per frame
    void Update() {
        //Move Snake forward
        transform.position += transform.forward * speed * Time.deltaTime;

        //Turn Snake Left & Right
        float rotDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotDirection * rotSpeed * Time.deltaTime);

        //Make body follow head
        //Store position history
        positionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in bodyParts) {
            Vector3 pos = positionsHistory[Mathf.Clamp(index * gap, 0, positionsHistory.Count - 1)];

            //move body parts
            Vector3 dir = pos - body.transform.position;
            body.transform.position += dir * speed * Time.deltaTime;

            //rotate body parts
            body.transform.LookAt(pos);

            index++;
        }
    }

    public void GrowSnake() {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(bodyPrefab);
        bodyParts.Add(body);
    }
        
    public void ChangeFoodPos(){
        Vector3 randSpawn = new Vector3(Random.Range(-9, 10), 12, Random.Range(-9, 10));   
        food.transform.position = randSpawn;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("food")) {
            GrowSnake();
            ChangeFoodPos();
        } 
    }


    //ran into an error with the body gab variable
    //conor found this solution online
    //limits framerate
    //without code, body parts appear with no gap
    void Awake(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 45;
     }
}