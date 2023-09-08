using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 taregtCoord;
    public RessourcesEnum type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, taregtCoord, step);
    }

    public void SetTarget(Vector3 targetToSet)
        => taregtCoord = targetToSet;

    public void SetType(RessourcesEnum randType)
    {
        type = randType;

        if (type == RessourcesEnum.BlackMunition)
            GetComponent<SpriteRenderer>().color = Color.black;
        else
            GetComponent<SpriteRenderer>().color = Color.red;
    }
}
