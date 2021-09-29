using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince
{

    #region Attributes
    private bool _isHuman;
    private bool _isInteracting;
    private bool _isRaven;
    private bool _isMoving;
    private bool _isAlive;
    private bool _isAttacking;
    private bool _isFlyingHigh;
    private bool _isFlyingDown;
    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _rend;
    private float _scaleValue;
    private float _walkSteps;
    private float _flySteps;
    private bool _isTalking;
    private bool _hasAlreadyTransmuted;
    #endregion

    #region Constructors
    public Prince()
    {
    }

    #endregion

    #region Setters and Getters
    public float ScaleValue { get => _scaleValue; set => _scaleValue = value; }
    public float WalkSteps { get => _walkSteps; set => _walkSteps = value; }
    public float FlySteps { get => _flySteps; set => _flySteps = value; }
    public bool IsHuman { get => _isHuman; set => _isHuman = value; }
    public bool IsRaven { get => _isRaven; set => _isRaven = value; }
    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool IsInteracting { get => _isInteracting; set => _isInteracting = value; }
    public bool IsFlyingHigh { get => _isFlyingHigh; set => _isFlyingHigh = value; }
    public bool IsFlyingDown { get => _isFlyingDown; set => _isFlyingDown = value; }
    public Animator animator { get => _animator; set => _animator = value; }
    public Rigidbody2D body { get => _body; set => _body = value; }
    public SpriteRenderer rend { get => _rend; set => _rend = value; }
    public bool HasAlreadyTransmuted { get => _hasAlreadyTransmuted; set => _hasAlreadyTransmuted = value; }
    #endregion

    #region Prince actions

    #endregion

}

public class PlayerMotor : MonoBehaviour
{

    #region Instances
    public Prince prince;
    public float _walkSteps;
    public float _flySteps;

    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    private float timeNextAttack = 0f;
    public int firstHitDamage;

    public float emotionTimer = 20;
    private float emotionBar = 20;

    private Vector3 startPosition;
    private GameObject[] obstacles;
    private ArrayList enemies;
    public GameObject energyBar;

    private bool GameIsPaused = false;
    private bool DialogueHasAlreadyOver = false;

    public EdgeCollider2D riverCollider;
    #endregion


    #region Constants
    private const string ENEMY_TAG = "enemy";
    private const string HUMAN_NOT_PASS = "human_not_pass";
    #endregion

    public GameObject strawman;

    void Start()
    {
        updateVolumeSounds();

        prince = new Prince()
        {
            IsInteracting = false,
            IsHuman = true,
            IsAlive = true,
            IsMoving = false,
            IsRaven = false,
            IsAttacking = false,
            IsFlyingDown = false,
            IsFlyingHigh = false,
            HasAlreadyTransmuted = false,
            ScaleValue = transform.localScale.x,
            WalkSteps = _walkSteps,
            FlySteps = _flySteps,
            animator = GetComponent<Animator>(),
            body = GetComponent<Rigidbody2D>(),
            rend = GetComponent<SpriteRenderer>()
        };

        startPosition = new Vector3(0, 0, 0);
        transform.position = startPosition;

        obstacles = GameObject.FindGameObjectsWithTag(HUMAN_NOT_PASS);

        GameObject[] enemiesVector = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        enemies = new ArrayList();

        foreach (GameObject enemy in enemiesVector)
        {
            enemies.Add(enemy);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("river") && prince.IsAlive && prince.IsHuman)
        {
            prince.IsAlive = false;
        }
    }

    void Update()
    {



        if (!prince.IsInteracting && !GameIsPaused)
        {
            UpdatePrinceActions();
        }

        if (prince.IsAlive)
            {
                Transform();
                Attack();
                Move();
                //Shot();
                //Hit();
                //Suffer();
                UpdateStatusBars();
            }
            else
            {
                GameOver();
        }
        

    }

    private void GameOver()
    {
        prince.animator.SetBool("walk", false);
        prince.animator.SetBool("walk", false);

        audios[AudioWalking].Pause();
        audios[AudioAttacking].Pause();

    }

    private bool transformable = true;
    private bool loadingTransformation = false;

    private void UpdatePrinceActions()
    {
        if (prince.IsAlive)
        {
            _movX = Input.GetAxisRaw("Horizontal");
            _movY = Input.GetAxisRaw("Vertical");
            prince.IsMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
            prince.HasAlreadyTransmuted = Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift);

            if(!loadingTransformation && emotionTimer > 0)
            {
                prince.IsRaven = Input.GetKey(KeyCode.LeftShift);
                prince.IsHuman = !prince.IsRaven;
            }

            if (loadingTransformation || emotionTimer < 0)
            {
                prince.IsRaven = false;
                prince.IsHuman = true;
                loadingTransformation = true;
            }

            if(loadingTransformation && emotionTimer > 2f)
            {
                loadingTransformation = false;
            }

            prince.IsAttacking = false;
            if (timeNextAttack <= 0 && Input.GetKeyDown(KeyCode.Z))
            {
                prince.IsAttacking = Input.GetKeyDown(KeyCode.Z);
                timeNextAttack = 0.75f;
            } else
            {
                timeNextAttack -= Time.deltaTime;
            }


            UpdatePrinceCollisions();
        }
    }

    private void UpdateStatusBars()
    {

        if (prince.IsRaven)
        {
            emotionTimer -= Time.deltaTime * 4; // 10 segundos

        } else if (prince.IsHuman &&  emotionTimer < emotionBar)
        {
            emotionTimer += Time.deltaTime; // 20 segundos
        }

        float limitBar = -0.7f;
        float passoEmotion = limitBar - ((limitBar * emotionTimer) / emotionBar);

        Vector3 energyScale = energyBar.transform.localScale;
        energyScale.x = passoEmotion;
        energyBar.transform.localScale = energyScale;

    }

    private void Transform()
    {
        
        prince.animator.SetBool("raven", prince.IsRaven && !prince.IsHuman && !prince.IsInteracting && !GameIsPaused);
        prince.animator.SetBool("flying_high", prince.IsFlyingHigh && !prince.IsInteracting && !GameIsPaused);
        prince.animator.SetBool("flying_low", prince.IsFlyingDown && !prince.IsInteracting && !GameIsPaused);

        RunAudio(AudioFlying, prince.IsRaven && !prince.IsInteracting);
    }

    private void Attack()
    {

        prince.animator.SetBool("attack", prince.IsAttacking && !GameIsPaused);
        
            if (prince.IsAttacking)
            {
                if (!audios[AudioAttacking].isPlaying && !prince.IsRaven)
                    audios[AudioAttacking].Play();
            }

        if (prince.animator.GetBool("attack") && prince.IsAttacking && prince.IsHuman)
        {
            PlayerAttack();
        }

        
    }

    private float _movX;
    private float _movY;

    private void Move()
    {

        if (!prince.IsInteracting && !GameIsPaused)
        {
            Vector3 moveDirection = new Vector3(_movX, _movY, 0);
            float velocity = prince.IsRaven ? prince.FlySteps : prince.WalkSteps;

            float posX = transform.position.x + moveDirection.x * Time.deltaTime * velocity;
            float posY = transform.position.y + moveDirection.y * Time.deltaTime * velocity;
            Vector2 move = new Vector2(posX, posY);

            prince.body.MovePosition(move);
            Turn(_movX, _movY);
        }

        prince.animator.SetBool("walk", prince.IsMoving && !prince.IsInteracting && !GameIsPaused);
        RunAudio(AudioWalking, prince.IsHuman && prince.IsMoving && !prince.IsInteracting);

    }

    private void Turn(float _movX, float _movY)
    {
        Vector3 characterScale = transform.localScale;

        bool left = _movX < 0;
        bool down = _movY < 0;
        bool right = _movX > 0;
        bool up = _movY > 0;

        prince.IsFlyingHigh = up && right || up && left;
        prince.IsFlyingDown = down && right || down && left;

        if ((left && down) || (left && up))
        {
            characterScale.x = prince.ScaleValue;
        }
        else if ((right && down) || (right && up))
        {
            characterScale.x = -prince.ScaleValue;
        }
        else if (left || down)
        {
            characterScale.x = prince.ScaleValue;
        }
        else if (right || up)
        {
            characterScale.x = -prince.ScaleValue;
        }

        transform.localScale = characterScale;
    }

    private void UpdatePrinceCollisions()
    {
        riverCollider.enabled = prince.IsHuman;

        if (prince.HasAlreadyTransmuted || DialogueHasAlreadyOver)
        {
            foreach (GameObject element in obstacles)
            {
                element.GetComponent<Collider2D>().enabled = prince.IsHuman;

            }

            foreach (GameObject element in obstacles)
            {
                if (element != null && element.GetComponent<SpriteRenderer>() != null)
                {
                    if (prince.IsRaven || prince.IsAttacking)
                    {
                        element.GetComponent<SpriteRenderer>().sortingOrder = prince.rend.sortingOrder - 1;
                    }
                    else
                    {
                        element.GetComponent<SpriteRenderer>().sortingOrder = prince.rend.sortingOrder;
                    }

                    element.GetComponent<Collider2D>().isTrigger = prince.IsRaven;
                }
            }

            foreach (GameObject element in enemies)
            {
                if(element != null) 
                { 
                if (prince.IsRaven || prince.IsAttacking)
                {
                    element.GetComponent<SpriteRenderer>().sortingOrder = prince.rend.sortingOrder - 1;
                } else
                {
                    element.GetComponent<SpriteRenderer>().sortingOrder = prince.rend.sortingOrder;
                }

                element.GetComponent<Collider2D>().isTrigger = prince.IsRaven;
                }
            }

            DialogueHasAlreadyOver = false;
        }
    }

    void PlayerAttack()
    {

        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy);

        foreach (Collider2D enemy in enemiesAttack)
        {
                enemy.SendMessage("EnemyHit", firstHitDamage);
                enemy.SendMessage("CutEnemy", enemy.transform.position);
        }

    }

    public void PlayerIsTalking(bool IsTalking)
    {
        prince.IsInteracting = IsTalking;
        prince.animator.SetBool("walk", false);
        prince.animator.SetBool("raven", false);
        prince.animator.SetBool("attack", false);

        if (IsTalking && prince.IsRaven)
        {
            prince.IsRaven = false;
            prince.IsHuman = true;
            DialogueHasAlreadyOver = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

    private void RunAudio(int id, bool turnOn)
    {
        if (turnOn && !GameIsPaused)
        {
            if(!audios[id].isPlaying)
                audios[id].Play();

        } else
        {
            audios[id].Pause();
        }
    }

    public void PauseGame(bool value)
    {

        GameIsPaused = value;
        
        if (GameIsPaused)
        {
            DialogueHasAlreadyOver = true;

        } else
        {
            updateVolumeSounds();
        }

    }

    public AudioSource[] audios;
    private const int AudioWalking = 0;
    private const int AudioFlying = 1;
    private const int AudioAttacking = 2;
    private const float AudioWalkingVolumeMax = 0.149f;
    private const float AudioFlyingVolumeMax = 1;
    private const float AudioAttackingVolumeMax = 1;

    private void updateVolumeSounds()
    {
        audios[AudioWalking].volume = AudioWalkingVolumeMax * GameManager.AudioVolumePerc / 100;
        audios[AudioFlying].volume = AudioFlyingVolumeMax * GameManager.AudioVolumePerc / 100;
        audios[AudioAttacking].volume = AudioAttackingVolumeMax * GameManager.AudioVolumePerc / 100;

    }



}
