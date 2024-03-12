using Jam.Mouse;
using Jam.UI.VirtualKeyboard;
using Jam.VirtualKeyboard;
using Jam.VirtualKeyboard.Keys;
using UnityEngine;
using Zenject;

namespace Jam
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [Space]
        [SerializeField] private VirtualKeyPlaneHolder keyPlaneHolder;
        [SerializeField] private VirtualKeyButtonHolder keyButtonHolder;

        public override void InstallBindings()
        {
            BindMouseService();
            BindCameraService();
            BindVirtualKeyboardService();
        }

        private void BindCameraService()
        {
            Container.Bind<CameraService>()
                .AsSingle()
                .WithArguments(mainCamera)
                .NonLazy();
        }

        private void BindMouseService()
        {
            Container.Bind<MouseContext>()
                .AsSingle()
                .NonLazy();
        }

        private void BindVirtualKeyboardService()
        {
            Container.Bind<VirtualKeyPlaneHolder>().FromInstance(keyPlaneHolder);
            Container.Bind<VirtualKeyButtonHolder>().FromInstance(keyButtonHolder);

            Container.Bind<UpArrowKey>().AsSingle().NonLazy();
            Container.Bind<DownArrowKey>().AsSingle().NonLazy();
            Container.Bind<LeftArrowKey>().AsSingle().NonLazy();
            Container.Bind<RightArrowKey>().AsSingle().NonLazy();
            Container.Bind<CapsLockKey>().AsSingle().NonLazy();
            Container.Bind<BackspaceKey>().AsSingle().NonLazy();

            Container.Bind<KeyFactory>().AsSingle().NonLazy();

            Container.Bind<VirtualKeyboardExecuter>().AsSingle().NonLazy();
            Container.Bind<VirtualKeyboardTransitionHandler>().AsSingle().NonLazy();
        }
    }
}