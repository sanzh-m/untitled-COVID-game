using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    Vector3 _startingPos;
    Transform _trans;
    float maxMoveDistance = 10;
    float speed = 4;
    bool down;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _startingPos = _trans.position;
        down = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = _startingPos;
        if (down)
        {

            if (_trans.position.y == _startingPos.y - maxMoveDistance)
            {
                down = false;

                _trans.position = Vector3.MoveTowards(_trans.position, new Vector3(_startingPos.x, _startingPos.y, 0), speed * Time.deltaTime);
            }
            else
            {
                _trans.position = Vector3.MoveTowards(_trans.position, new Vector3(_startingPos.x, _startingPos.y - maxMoveDistance, 0), speed * Time.deltaTime);

            }

        }
        else
        {
            if (_trans.position.y == _startingPos.y)
            {
                down = true;
                _trans.position = Vector3.MoveTowards(_trans.position, new Vector3(_startingPos.x, _startingPos.y - maxMoveDistance, 0), speed * Time.deltaTime);

            }
            else
            {
                _trans.position = Vector3.MoveTowards(_trans.position, new Vector3(_startingPos.x, _startingPos.y, 0), speed * Time.deltaTime);

            }
        }



    }
}
