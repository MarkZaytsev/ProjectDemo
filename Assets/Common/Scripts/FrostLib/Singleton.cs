namespace Common.FrostLib
{    
	public class Singleton<T> where T : class, new()
	{
        public static T Instance
        {
            get => _instance ?? (_instance = new T());
            private set => _instance = value;
        }

        private static T _instance;

	    protected Singleton() { }

        public static void Reset() => Instance = null;
    }
}