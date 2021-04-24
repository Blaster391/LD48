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
    private float m_shotgunDuration = 5.0f;
    [SerializeField]
    private float m_shotgunAttackSpeed = 0.5f;
    [SerializeField]
    private float m_shotgunAttackInaccuracy = 0.5f;
    [SerializeField]
    private int m_shotgunAttackCountMin = 5;
    [SerializeField]
    private int m_shotgunAttackCountMax = 7;
    [SerializeField]
    private float m_summonDuration = 5.0f;
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
    private BossState m_state = BossState.Roar;
    private int m_stage = 1;
    private Vector2 m_pounceDestination;
    private GameObject m_player;

    private bool m_stateChanged = false;
    private float m_timeInState = 0.0f;
    private float m_randomisedTotalTimeInState = 0.0f;
    private bool m_pounceComplete = false;
    public void Die()
    {
        ChangeState(BossState.Dying);
    }

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_health = GetComponent<AIDamage>();
        m_movement = GetComponent<AIMovementHelper>();
        m_player = GameMaster.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggerableAI.Active())
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

        if(m_shotgunChance > Random.value)
        {
            ChangeState(BossState.Shotgun);
            return;
        }

        if (m_state != BossState.Rest && m_restChance > Random.value)
        {
            ChangeState(BossState.Rest);
            return;
        }

        ChangeState(BossState.PounceCharging);
    }

    private void RoarState()
    {
        if (m_timeInState > m_roarTime * 0.5f && m_stage == 3)
        {
            m_renderer.color = m_stage3Colour;
        }

        if (m_timeInState > m_roarTime)
        {
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
            m_movement.SetSpeed(m_pounceSpeed);
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
            if(Mathf.Abs(transform.eulerAngles.z) < 1)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                readyToEnd = true;
            }
        }

        if (m_timeInState > m_spinAttackWindup)
        {

        }

        if (readyToEnd)
        {
            ChooseNextState();
        }
    }

    private void ShotgunState()
    {

        if (m_timeInState > m_shotgunDuration)
        {
            ChooseNextState();
        }
    }

    private void SummonState()
    {

        if(m_timeInState > m_summonDuration)
        {
            ChooseNextState();
        }
    }

    private void DyingState()
    {
        if (m_timeInState == 0.0f)
        {
            m_triggerableAI.GetTriggerArea().GetComponent<AITriggerSpawner>().KillAllAI();
        }

        if (m_timeInState > m_deathDuration)
        {
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
