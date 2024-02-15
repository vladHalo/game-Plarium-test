using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    public GameManager _gameManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        Container.QueueForInject(_gameManager);
    }
}