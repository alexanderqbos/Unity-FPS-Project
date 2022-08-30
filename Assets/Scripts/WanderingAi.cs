using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { alive, dead };

public class WanderingAi : MonoBehaviour
{
    private float enemySpeed = 3.0f;
    private float baseSpeed = 0.25f;
    private float difficultySpeed = 0.3f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

    private EnemyStates state;

    [SerializeField]
    private GameObject laserPrefab;

    private GameObject laserbeam;

    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    private void Awake() {
        Messenger<int>.AddListener(GameEvents.DIFFICULTY_CHANGED, onDifficultyChanged);
    }

    void Start() {
        state = EnemyStates.alive;
        int baseDifficulty = PlayerPrefs.GetInt("difficulty");
        adjustEnemySpeed(baseDifficulty);
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        Debug.DrawLine(transform.position, rangeTest);
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == EnemyStates.alive)
        {
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
        }

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if(Physics.SphereCast(ray, sphereRadius, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.GetComponent<PlayerCharacter>())
            {
                if(laserbeam == null && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    laserbeam = Instantiate(laserPrefab) as GameObject;
                    laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                    laserbeam.transform.rotation = transform.rotation;
                }
            }
            else if(hit.distance < obstacleRange)
            {
                float turnAngle = Random.Range(-110, 110);
                transform.Rotate(Vector3.up * turnAngle);
            }
        }
    }

    private void OnDestroy() {
        Messenger<int>.RemoveListener(GameEvents.DIFFICULTY_CHANGED, onDifficultyChanged);
    }

    void onDifficultyChanged(int difficulty)
    {
        enemySpeed = baseSpeed + (difficulty * difficultySpeed);
    }

    void adjustEnemySpeed(int difficulty)
    {
        enemySpeed = baseSpeed + (difficulty * difficultySpeed);
    }
}
