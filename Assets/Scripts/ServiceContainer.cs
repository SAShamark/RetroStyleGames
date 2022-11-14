using System.Collections.Generic;
using Entities.Enemy;
using Entities.Enemy.EnemyObject.Data;
using UI;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

public class ServiceContainer : MonoBehaviour
{
    public static ServiceContainer Instance;
    public EnemyRegistry EnemyRegistry { get; private set; }


    [SerializeField] private GamePanelView _gamePanelView;
    [SerializeField] private List<EnemyData> _enemyDates;
    [SerializeField] private EnemyData _defaultEnemy;
    [SerializeField] private Transform _enemyContainer;

    private GamePanelsController _gamePanelsController;

    private EnemySpawner _enemySpawner;

    private CharacterController _characterController;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _characterController = CharacterController.Instance;
        
        _gamePanelsController = new GamePanelsController(_gamePanelView, _characterController);
        EnemyRegistry = new EnemyRegistry();
        _enemySpawner = new EnemySpawner(_characterController, EnemyRegistry, _enemyDates, _defaultEnemy,
            _enemyContainer);

        _gamePanelsController.Init();
        _enemySpawner.Init();
        StartCoroutine(_enemySpawner.Spawner());
    }

    private void OnDestroy()
    {
        _gamePanelsController.OnDestroy();
    }
}