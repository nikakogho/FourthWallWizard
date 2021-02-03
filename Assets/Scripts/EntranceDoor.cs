using System.Collections;
using UnityEngine;

public class EntranceDoor : Door {
    public GameObject oldOne, newOne;
    public Vector3 reachPoint;
    public GameObject wallBehind;
    Transform player;
    bool waiting = false;
    public float reachRange = 0.2f;

    protected override void Open()
    {
        base.Open();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        newOne.SetActive(true);
        waiting = true;
    }

    void FixedUpdate()
    {
        if (waiting)
        {
            if(Mathf.Abs(reachPoint.x - player.position.x) <= reachRange && Mathf.Abs(reachPoint.z - player.position.z) <= reachRange)
            {
                StartCoroutine(End());
            }
        }
    }

    IEnumerator End()
    {
        waiting = false;
        wallBehind.SetActive(true);
        yield return new WaitForSeconds(2);
        oldOne.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(reachPoint, new Vector3(reachRange, 0, reachRange));
    }
}
