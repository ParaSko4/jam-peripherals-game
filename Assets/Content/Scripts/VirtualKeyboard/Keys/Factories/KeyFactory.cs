using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class KeyFactory
    {
        private readonly DiContainer container;

        [Inject]
        public KeyFactory(DiContainer container)
        {
            this.container = container;
        }

        public T CreateKey<T>() where T : VirtualKey
        {
            return container.Resolve<T>();
        }
    }
}
