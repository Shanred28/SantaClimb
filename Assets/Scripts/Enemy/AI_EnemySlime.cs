using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_EnemySlime : MonoBehaviour
{
    public enum AIBehaviour
    {
        Null,
        Idle,
        PatrolRandom,
        CirclePatrol,
        PursuetTarget,
        SeekTarget,
        Attack
    }

    public enum AlarmType
    {
        Detected,
        Midle,
        Non
    }


    [SerializeField] private AIBehaviour _aiBehaviour;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyCharacterMove _characterMovement;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private float _attackDistance;

    [SerializeField] private int _patrolPatchNodeIndex = 0;

    [SerializeField] private float _radiusDetectPlayer;

    [SerializeField] private EntityAction _actionAttack;


    //private PatrolPath _patrolPath;
    private NavMeshPath _navMeshPath;
    //private PatrolPathNode _currentPathNode;

    private GameObject _potentionalTarget;
    private Transform _pursuetTarget;
    private Vector3 _seekTarget;
    [SerializeField] private float timeDetectingPeripheral;
    private Timer timer;

    private void Start()
    {
        _characterMovement.UpdatePosition = true;

       // FindPatrolPath();
        FindPotentionalTarget();
        _navMeshPath = new NavMeshPath();
        StartBehaviour(_aiBehaviour);
        _enemy.OnGetDamage += OnGetDamage;
    }

    private void Update()
    {
        SyncAgentAndCharacterMovement();
        UpdateAI();
    }

    private void OnDestroy()
    {
        _enemy.OnGetDamage -= OnGetDamage;
    }

    // Handler
    private void OnGetDamage(Destructible other)
    {
        if (other.TeamId != _enemy.TeamId)
        {
/*            if (_colliderViewer.IsObjectVisible(_potentionalTarget) == true)
            {
                ActionAssignTargetAllTeamMember(other.transform);
            }
            else
            {
                _seekTarget = other.transform.position;
                ActionAssignTargetAllTeamMember(other.transform);
                StartBehaviour(AIBehaviour.SeekTarget);
            }*/
        }
    }

    private void UpdateAI()
    {
        ActionUpdateTarget();

        if (_aiBehaviour == AIBehaviour.Idle)
        {
            return;
        }

        if (_aiBehaviour == AIBehaviour.PursuetTarget)
        {
            _agent.CalculatePath(_pursuetTarget.position, _navMeshPath);
            _agent.SetPath(_navMeshPath);

            if (Vector3.Distance(transform.position, _pursuetTarget.position) <= _attackDistance)
            {
                _actionAttack.StartAction();
            }
            else
                return;
        }

        if (_aiBehaviour == AIBehaviour.SeekTarget)
        {
            _agent.CalculatePath(_seekTarget, _navMeshPath);
            _agent.SetPath(_navMeshPath);

            if (AgentReachedDistination() == true)
            {
                //StartBehaviour(AIBehaviour.PatrolRandom);
                _pointCentreAreaSeek = _seekTarget;
                //StartCoroutine(SeekAreaTarget());
            }
        }

/*        if (_aiBehaviour == AIBehaviour.PatrolRandom)
        {
            if (AgentReachedDistination() == true)
            {
                StartCoroutine(SetBehaviourOnTime(AIBehaviour.Idle, _currentPathNode.IdleTime));
            }
        }*/

/*        if (_aiBehaviour == AIBehaviour.CirclePatrol)
        {
            if (AgentReachedDistination() == true)
            {
                StartCoroutine(SetBehaviourOnTime(AIBehaviour.Idle, _currentPathNode.IdleTime));
            }
        }*/
    }

    // Action
    private void FindPotentionalTarget()
    {
        _potentionalTarget = Player.Instance.gameObject;
    }

    private void ActionUpdateTarget()
    {
        if (_potentionalTarget == null)
        {
            _potentionalTarget = Player.Instance.gameObject;
            timer.Restart(timeDetectingPeripheral);
            return;
        }

        if (Vector3.Distance(_potentionalTarget.transform.position, transform.position) <= _radiusDetectPlayer)
        {
            _pursuetTarget = _potentionalTarget.transform;
            ActionAssignTargetAllTeamMember(_pursuetTarget);
            //timer.Stop();
        }

/*        if ((Vector3.Distance(_potentionalTarget.transform.position, transform.position) <= _radiusDetectPlayer)
        {
            
        }*/

/*        if (_colliderViewer.IsObjectPeripheralVisible(_potentionalTarget) == true)
        {
            if (_colliderViewer.IsObjectVisible(_potentionalTarget) == true)
            {
                _pursuetTarget = _potentionalTarget.transform;
                ActionAssignTargetAllTeamMember(_pursuetTarget);
                Alarm(AlarmType.Detected);
                timer.Stop();
            }
            else
            {
                timer.Play();
                _indicatorVisable.SetAllarmFill(timer.CurrentTime, timeDetectingPeripheral);
                if (timer.IsComplited)
                {
                    _pursuetTarget = _potentionalTarget.transform;
                    ActionAssignTargetAllTeamMember(_pursuetTarget);
                    Alarm(AlarmType.Detected);
                    timer.Stop();
                }
            }
        }
        else
        {
            timer.Restart(timeDetectingPeripheral);
            if (_pursuetTarget != null)
            {
                _seekTarget = _pursuetTarget.position;
                Alarm(AlarmType.Midle);
                _pursuetTarget = null;
                StartBehaviour(AIBehaviour.SeekTarget);
                timer.Stop();
            }
        }*/
    }

/*    [SerializeField] private float _radiusHearing;
    public void ApplyHearling(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < _radiusHearing)
        {
            _seekTarget = target;
            StartBehaviour(AIBehaviour.SeekTarget);
        }
    }*/


    //Behaviour
    private void StartBehaviour(AIBehaviour state)
    {
        if (_enemy.IsDestroy == true) return;

        if (state == AIBehaviour.Idle)
        {
            _agent.isStopped = true;
           // _characterMovement.UnAiming();
        }

        if (state == AIBehaviour.PatrolRandom)
        {
            _agent.isStopped = false;
            //_characterMovement.UnAiming();
            //SetDistinationByPathNode(_patrolPath.GetRandomPathNode());
        }

        if (state == AIBehaviour.CirclePatrol)
        {
            _agent.isStopped = false;
            //_characterMovement.UnAiming();
            //SetDistinationByPathNode(_patrolPath.GetNextNode(ref _patrolPatchNodeIndex));
        }

        if (state == AIBehaviour.PursuetTarget)
        {
            _agent.isStopped = false;

        }

        if (state == AIBehaviour.SeekTarget)
        {
            _agent.isStopped = false;
            //_characterMovement.UnAiming();
        }

        if (state == AIBehaviour.Attack)
        {
            _agent.isStopped = true;
        }
        _aiBehaviour = state;
    }

    private void ActionAssignTargetAllTeamMember(Transform other)
    {
        List<Destructible> team = Destructible.GetAllTeamMember(_enemy.TeamId);

        foreach (Destructible dest in team)
        {
            AI_EnemySlime ai = dest.transform.root.GetComponent<AI_EnemySlime>();

            if (ai != null && ai.enabled == true)
            {
                ai.SetPursueTarget(other);
                ai.StartBehaviour(AIBehaviour.PursuetTarget);
            }
        }
    }

    public void SetPosition(Vector3 pos)
    {
        _agent.Warp(pos);
    }

    public void SetPursueTarget(Transform target)
    {
        _pursuetTarget = target;
    }

    [SerializeField] private float _radiusAreaSeek;
    private Vector3 _pointCentreAreaSeek;
    public Vector3 GetPointRandomSeekArea()
    {
        Vector3 result = _pointCentreAreaSeek;

        result.x = Random.Range(result.x - _radiusAreaSeek, result.x + _radiusAreaSeek);
        result.z = Random.Range(result.z - _radiusAreaSeek, result.z + _radiusAreaSeek);


        return result;
    }

/*    private void SetDistinationByPathNode(PatrolPathNode node)
    {
        _currentPathNode = node;
        _agent.CalculatePath(node.transform.position, _navMeshPath);

        _agent.SetPath(_navMeshPath);
    }*/

    private void SyncAgentAndCharacterMovement()
    {
        _agent.speed = _characterMovement.CurrentSpeed;

        float factor = _agent.velocity.magnitude / _agent.speed;
        _characterMovement.TargetDirectionControl = transform.InverseTransformDirection(_agent.velocity.normalized) * factor;
    }

    private bool AgentReachedDistination()
    {
        if (_agent.pathPending == false)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (_agent.hasPath == false || _agent.velocity.magnitude == 0.0f)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

/*    private void Alarm(AlarmType alarmType)
    {

        switch (alarmType)
        {
            case AlarmType.Detected:
                _indicatorVisable?.Detected();
                break;
            case AlarmType.Midle:
                _indicatorVisable?.MidleDetected();
                break;
            case AlarmType.Non:
                _indicatorVisable?.NonDetected();
                break;
        }
    }*/

/*    private void FindPatrolPath()
    {
        if (_patrolPath == null)
        {
            PatrolPath[] patrolPath = FindObjectsOfType<PatrolPath>();
            float minDistance = float.MaxValue;

            for (int i = 0; i < patrolPath.Length; i++)
            {
                float dist1 = Vector3.Distance(transform.position, patrolPath[i].transform.position);

                if (dist1 < minDistance)
                {
                    minDistance = dist1;
                    _patrolPath = patrolPath[i];
                }
            }
        }
    }*/

    IEnumerator SetBehaviourOnTime(AIBehaviour state, float second)
    {
        AIBehaviour previous = _aiBehaviour;
        _aiBehaviour = state;
        StartBehaviour(_aiBehaviour);

        yield return new WaitForSeconds(second);

        StartBehaviour(previous);
    }

/*    IEnumerator SeekAreaTarget()
    {
        _seekTarget = GetPointRandomSeekArea();
        _agent.CalculatePath(_seekTarget, _navMeshPath);
        _agent.SetPath(_navMeshPath);


        yield return new WaitForSeconds(2);

        _seekTarget = GetPointRandomSeekArea();
        _agent.CalculatePath(_seekTarget, _navMeshPath);
        _agent.SetPath(_navMeshPath);

        *//*        yield return new WaitForSeconds(2);

                _seekTarget = GetPointRandomSeekArea();
                _agent.CalculatePath(_seekTarget, _navMeshPath);
                _agent.SetPath(_navMeshPath);*//*

        yield return new WaitForSeconds(2);
       // Alarm(AlarmType.Non);
        StartBehaviour(AIBehaviour.PatrolRandom);
    }*/
}
