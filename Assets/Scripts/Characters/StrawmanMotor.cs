using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawman
{

    #region Attributes
    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _rend;
    private bool alive;
    #endregion

    #region Constructors
    public Strawman()
    {
        alive = true;
    }

    #endregion

    #region Setters and Getters
    public Animator animator { get => _animator; set => _animator = value; }
    public Rigidbody2D body { get => _body; set => _body = value; }
    public SpriteRenderer rend { get => _rend; set => _rend = value; }
    public bool IsAlive { get => alive; set => alive = value; }
    #endregion

}

public class StrawmanMotor : MonoBehaviour
{

    public int lifePoints;
    public GameObject cutHitObj;
    public GameObject director;

    private Strawman strawman;

    private bool dead = false;

    
    void Start()
    {
        strawman = new Strawman()
        {
            animator = GetComponent<Animator>(),
            body = GetComponent<Rigidbody2D>(),
            rend = GetComponent<SpriteRenderer>()
        };

    }

    void Update()
    {
        if(!strawman.IsAlive && !dead)
        {
            strawman.animator.SetBool("dead", true);
            director.SendMessage("Score", gameObject);
            dead = true;
        }

    }


    public void EnemyHit(int value)
    {
        lifePoints -= value;
        strawman.IsAlive = lifePoints > 0;
    }

    public void CutEnemy(Vector3 cutPos)
    {
        float angle = Random.Range(0f, 180f);
        Quaternion quartenion = Quaternion.Euler(0, 0, angle);
        var cutHit = Instantiate(cutHitObj, cutPos, quartenion);

    }

}
