using Content.Scripts.Mouse;
using Jam.VirtualKeyboard;
using Jam.VirtualKeyboard.Keys;
using UnityEngine;
using Zenject;

namespace Jam
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private MousePointer _mousePointer;
        [SerializeField] private PointerConfig _pointerConfig;

        public override void InstallBindings()
        {
            BindCameraService();
            BindPointerConfig();
            BindMousePointer();
            BindVirtualKeyboardService();
        }

        private void BindCameraService()
        {
            Container.Bind<CameraService>()
                .AsSingle()
                .WithArguments(mainCamera)
                .NonLazy();
        }
        
        private void BindMousePointer()
        {
            Container.Bind<MousePointer>()
                .FromInstance(_mousePointer)
                .AsSingle()
                .NonLazy();
        }

        private void BindPointerConfig()
        {
            Container.Bind<PointerConfig>()
                .FromInstance(_pointerConfig)
                .AsSingle()
                .NonLazy();
        }

  
        private void BindVirtualKeyboardService()
        {
            Container.Bind<UpArrowKey>().AsSingle().NonLazy();
            Container.Bind<DownArrowKey>().AsSingle().NonLazy();
            Container.Bind<LeftArrowKey>().AsSingle().NonLazy();
            Container.Bind<RightArrowKey>().AsSingle().NonLazy();
            Container.Bind<CapsLockKey>().AsSingle().NonLazy();
            Container.Bind<BackspaceKey>().AsSingle().NonLazy();

            Container.Bind<KeyFactory>().AsSingle().NonLazy();

            Container.Bind<VirtualKeyboardExecuter>().AsSingle().NonLazy();
        }
    }
}