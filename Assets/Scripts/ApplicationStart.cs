using Entities.Enemy;
using Services;
using UI;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

public class ApplicationStart : MonoBehaviour
{

    [SerializeField] private GamePanelView _gamePanelView;
    [SerializeField] private EnemiesData _enemiesData;
    [SerializeField] private Transform _enemyContainer;

    private GamePanelsController _gamePanelsController;
    private EnemySpawner _enemySpawner;
    private CharacterController _characterController;


    private void Awake()
    {
        _enemySpawner = new EnemySpawner(_enemiesData, _enemyContainer);
        ServiceLocator.SharedInstance.Register(_enemySpawner);
    }

    private void Start()
    {
        _gamePanelsController = new GamePanelsController(_gamePanelView);

        _gamePanelsController.Init();
        _enemySpawner.Init();
        StartCoroutine(_enemySpawner.Spawner());
    }

    private void OnDestroy()
    {
        _gamePanelsController.OnDestroy();
    }
}