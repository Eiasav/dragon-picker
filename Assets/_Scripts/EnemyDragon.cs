using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] private SheetsParser parser;
    [SerializeField] private int levelInd;
    public GameObject dragonEggPrefab;
    public float speed;
    public float timeBetweenEggDrops;
    public float leftRightDistance;
    public float chanceDirection;
    void Start()
    {
        Invoke(nameof(Initialize), 1f);
        Invoke("DropEgg", 2f);

    }

    void DropEgg(){
        Vector3 myVector = new Vector3(0.0f, 5.0f, 0.0f);
        GameObject egg = Instantiate(dragonEggPrefab);
        egg.transform.position = transform.position + myVector;
        Invoke("DropEgg", timeBetweenEggDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistance)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate() 
    {
        if (Random.value < chanceDirection)
        {
            speed *= -1;
        }
    }

    private void Initialize()
    {
        speed = parser.Data[levelInd][0];
        leftRightDistance = parser.Data[levelInd][1];
        timeBetweenEggDrops = parser.Data[levelInd][2];
        chanceDirection = 0.01f;
    }
}
