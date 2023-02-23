using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform enemy;

    public float speed;
    public float[] idleTime;

    public int idWaypoint;

    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        idWaypoint = Random.Range(0, waypoints.Length);
        StartCoroutine(MoveToWaypoint());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (isMoving)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, waypoints[idWaypoint].position, speed * Time.deltaTime);

            if (enemy.position == waypoints[idWaypoint].position)
            {
                isMoving = false;
                StartCoroutine(MoveToWaypoint());
            }
        }
    }


    private IEnumerator MoveToWaypoint()
    {
        idWaypoint = Random.Range(0, waypoints.Length);
        idWaypoint++;

        if(idWaypoint >= waypoints.Length)
        {
            idWaypoint = 0;
        } 

        yield return new WaitForSeconds(Random.Range(idleTime[0], idleTime[1]));
        isMoving = true;
    }
}
