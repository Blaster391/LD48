using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Roar,
    Rest,
    PounceCharging,
    Pouncing,
    SpinAttack,
    Shotgun,
    Summon,
    Dying
}

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float m_shotgunChance = 0.5f;
    [SerializeField]
    private float m_spinChance = 0.4f;
    [SerializeField]
    private float m_summonChance = 0.25f;
    [SerializeField]
    private float m_restChance = 0.5f;

    [SerializeField]
    private int m_stage2HealthThreshold = 75;
    [SerializeField]
    private int m_stage3HealthThreshold = 30;
    [SerializeField]
    private float m_minimumRestTime = 5.0f;
    [SerializeField]
    private float m_maximumRestTime = 10.0f;
    [SerializeField]
    private float m_roarTime = 2.0f;
    [SerializeField]
    private float m_pounceChargeTime = 2.0f;
    [SerializeField]
    private float m_pounceSpeed = 10.0f;
    [SerializeField]
    private float m_pounceTime = 2.0f;

    [SerializeField]
    private float m_spinAttackRotationSpeed = 1.0f;
    [SerializeField]
    private float m_spinAttackMovementSpeed = 10.0f;
    [SerializeField]
    private float m_spinAttackStageRotationSpeedMod = 1.5f;
    [SerializeField]
    private float m_spinAttackDuration = 5.0f;
    [SerializeField]
    private float m_spinAttackStageDurationMod = 2.0f;
    [SerializeField]
    private float m_spinAttackStageSpeedMod = 2.0f;
    [SerializeField]
    private float m_spinAttackWindup = 2.0f;
    [SerializeField]
    private float m_spinAttackSpeed = 2.0f;
    [SerializeField]
    private float m_spinAttackBulletSpeed = 10.0f;

    [SerializeField]
    private float m_shotgunDuration = 5.0f;
    [SerializeField]
    private float m_shotgunAttackSpeed = 0.5f;
    [SerializeField]
    private float m_shotgunAttackInaccuracy = 0.5f;
    [SerializeField]
    private float m_shotgunAttackVelocityVariance = 2.0f;
    [SerializeField]
    private int m_shotgunAttackCountMin = 5;
    [SerializeField]
    private int m_shotgunAttackCountMax = 7;
    [SerializeField]
    private float m_shotgunBulletSpeed = 5.0f;

    [SerializeField]
    private float m_summonDuration = 3.0f;
    [SerializeField]
    private float m_summonSpeed = 1.0f;

    [SerializeField]
    private int m_playerImpactDamage = 2;

    [SerializeField]
    private Color m_stage3Colour;
    [SerializeField]
    private float m_deathDuration = 5.0f;
    [SerializeField]
    private GameObject m_deathDrop = null;

    private SpriteRenderer m_renderer;
    private TriggerableAI m_triggerableAI;
    private AIDamage m_health = null;
    private AIMovementHelper m_movement = null;
    private ShootingHelper m_shooting = null;
    private BossState m_state = BossState.Roar;
    private int m_stage = 1;
    private Vector2 m_pounceDestination;
    private GameObject m_player;
    private PlayerCamera m_camera;

    private bool m_stateChanged = false;
    private float m_timeInState = 0.0f;
    private float m_timeSinceLastShot = 0.0f;
    private float m_timeSinceLastSpawned = 0.0f;
    private float m_randomisedTotalTimeInState = 0.0f;
    private bool m_pounceComplete = false;
    public void Die()
    {
        if(m_state != BossState.Dying)
        {
            GetComponent<Collider2D>().enabled = false;
            GameMaster.GetAudioManager().StopBGM();
            ChangeState(BossState.Dying);
        }
    }

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_health = GetComponent<AIDamage>();
        m_movement = GetComponent<AIMovementHelper>();
        m_shooting = GetComponent<ShootingHelper>();
        m_player = GameMaster.GetPlayer();
        m_camera = GameMaster.GetPlayerCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggerableAI.Active() || m_health.IsDead())
        {
            UpdateBossState();
        }
    }

    private void UpdateBossState()
    {
        Debug.Log($"Boss State: {m_state.ToString()}");

        m_stateChanged = false;

        switch (m_state)
        {
            case BossState.Roar:
                RoarState();
                break;
            case BossState.Rest:
                RestState();
                break;
            case BossState.PounceCharging:
                PounceChargingState();
                break;
            case BossState.Pouncing:
                PounceState();
                break;
            case BossState.SpinAttack:
                SpinAttackState();
                break;
            case BossState.Shotgun:
                ShotgunState();
                break;
            case BossState.Summon:
                SummonState();
                break;
            case BossState.Dying:
                DyingState();
                break;
            default:
                Debug.LogError("Unknown boss state");
                break;
        }

        if(!m_stateChanged)
        {
            m_timeInState += Time.deltaTime;
        }

    }

    private void ChangeState(BossState _state)
    {
        m_state = _state;
        m_timeInState = 0.0f;
        m_stateChanged = true;
        Debug.Log($"Change State: {m_state.ToString()}");
    }

    private void ChooseNextState()
    {
        if (m_stage < 3 && m_health.GetHealth() < m_stage3HealthThreshold)
        {
            ChangeState(BossState.Roar);
            m_stage = 3;
            return;
        }

        if (m_stage == 1 && m_health.GetHealth() < m_stage2HealthThreshold)
        {
            ChangeState(BossState.Roar);
            m_stage = 2;
            return;
        }

        if(m_stage == 3 && m_state != BossState.Summon && (m_summonChance > Random.value))
        {
            ChangeState(BossState.Summon);
            return;
        }

        if (m_stage > 1 && m_state != BossState.SpinAttack && (m_spinChance > Random.value))
        {
            ChangeState(BossState.SpinAttack);
            return;
        }

        float shotgunChance = m_state != BossState.Shotgun ? m_shotgunChance : m_shotgunChance * 0.5f;
        if (shotgunChance > Random.value)
        {
            ChangeState(BossState.Shotgun);
            return;
        }

        if (m_state != BossState.Rest && m_state != BossState.Roar && m_restChance > Random.value)
        {
            ChangeState(BossState.Rest);
            return;
        }

        ChangeState(BossState.PounceCharging);
    }

    private void RoarState()
    {
        m_camera.SetCameraShake(true);

        if (m_timeInState > m_roarTime * 0.5f && m_stage == 3)
        {
            m_renderer.color = m_stage3Colour;
        }

        if (m_timeInState > m_roarTime)
        {
            m_camera.SetCameraShake(false);
            ChooseNextState();
        }
    }

    private void RestState()
    {
        if(m_timeInState == 0)
        {
            m_randomisedTotalTimeInState = Random.Range(m_minimumRestTime, m_maximumRestTime);
        }

        if(m_timeInState > m_randomisedTotalTimeInState)
        {
            ChooseNextState();
        }
    }

    private void PounceChargingState()
    {
        m_pounceComplete = false;

        if (m_timeInState == 0.0f)
        {
            m_pounceDestination = m_movement.PlayerPosition();
        }

        if (m_timeInState > m_pounceChargeTime)
        {
            ChangeState(BossState.Pouncing);
        }
    }

    private void PounceState()
    {
        Vector2 myPosition = transform.position;
        if ((myPosition - m_pounceDestination).magnitude < 1.0f)
        {
            m_pounceComplete = true;
        }

        if(!m_pounceComplete)
        {
            m_movement.SetSpeed(m_pounceSpeed * m_stage);
            m_movement.MoveToTarget(m_pounceDestination);
        }

        if (m_timeInState > m_pounceTime)
        {
            ChooseNextState();
        }
    }

    private void SpinAttackState()
    {
        Vector2 targetPosition = m_triggerableAI.GetTriggerArea().transform.position;
        Vector2 myPosition = transform.position;
        if ((myPosition - targetPosition).magnitude > 1.0f)
        {
            m_movement.SetSpeed(m_spinAttackMovementSpeed);
            m_movement.MoveToTarget(targetPosition);
        }

        bool readyToEnd = false;

        transform.Rotate(new Vector3(0, 0, 1) * m_spinAttackRotationSpeed * m_spinAttackStageRotationSpeedMod * m_stage * Time.deltaTime);
        if (m_timeInState > m_spinAttackDuration * (m_spinAttackStageDurationMod * m_stage))
        {
            if(Mathf.Abs(transform.eulerAngles.z) < 5.0f)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                readyToEnd = true;
            }
        }

        m_timeSinceLastShot += Time.deltaTime;

        if (m_timeInState > m_spinAttackWindup)
        {
            float attackRate = 1 / (m_spinAttackSpeed * m_stage * m_spinAttackStageSpeedMod);

            if (m_timeSinceLastShot > attackRate)
            {
                m_shooting.Shoot(transform.up * m_spinAttackBulletSpeed, 1);
                m_shooting.Shoot(-transform.up * m_spinAttackBulletSpeed, 1);
                m_shooting.Shoot(transform.right * m_spinAttackBulletSpeed, 1);
                m_shooting.Shoot(-transform.right * m_spinAttackBulletSpeed, 1);

                m_timeSinceLastShot = 0.0f;
            }
        }

        if (readyToEnd)
        {
            ChooseNextState();
        }
    }

    private void ShotgunState()
    {
        m_timeSinceLastShot += Time.deltaTime;
        float attackRate = 1 / (m_shotgunAttackSpeed);

        if (m_timeSinceLastShot > attackRate)
        {
            m_timeSinceLastShot = 0.0f;
            int bulletsToFire = Random.Range(m_shotgunAttackCountMin, m_shotgunAttackCountMax);
            Vector2 playerDirection = m_movement.DirectionToPlayer();
            for(int i = 0; i < bulletsToFire; ++i)
            {
                Vector2 attackNormal = playerDirection.PerpendicularClockwise();
                Vector2 inaccuracy = attackNormal * MathsHelper.RandomWithNegative() * (m_shotgunAttackInaccuracy);
                Vector2 attackVelocity = (playerDirection + inaccuracy) * (m_shotgunBulletSpeed * m_stage + m_shotgunAttackVelocityVariance * Random.value);
                m_shooting.Shoot(attackVelocity, 1);
            }

        }

        if (m_timeInState > m_shotgunDuration)
        {
            ChooseNextState();
        }
    }

    private void SummonState()
    {
        m_camera.SetCameraShake(true);

        float spawnRate = 1 / (m_summonSpeed);
        m_timeSinceLastSpawned += Time.deltaTime;
        if (m_timeSinceLastSpawned > spawnRate)
        {
            m_timeSinceLastSpawned = 0.0f;
            m_triggerableAI.GetTriggerArea().GetComponent<AITriggerSpawner>().Spawn();
        }

        if (m_timeInState > m_summonDuration)
        {
            m_camera.SetCameraShake(false);
            ChooseNextState();
        }
    }

    private void DyingState()
    {
        m_camera.SetCameraShake(true);

        m_triggerableAI.GetTriggerArea().GetComponent<AITriggerSpawner>().KillAllAI();
  
        float fadeLerp = 1.0f - (m_timeInState / m_deathDuration);
        Color bossColour = m_renderer.color;
        bossColour.a = fadeLerp;
        m_renderer.color = bossColour;

        if (m_timeInState > m_deathDuration)
        {
            m_camera.SetCameraShake(false);
            Instantiate<GameObject>(m_deathDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == m_player)
        {
            OnHitPlayer();
        }
    }

    private void OnHitPlayer()
    {
        IDamageReceiver playerDamage = m_player.GetComponent<IDamageReceiver>();
        playerDamage.ReceiveDamage(m_playerImpactDamage);
    }
}
