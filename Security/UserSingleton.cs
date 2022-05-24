namespace Quality_Control_EF.Security
{
    public sealed class UserSingleton
    {
        private static volatile UserSingleton _instance = null;
        private static readonly object _syncRoot = new object();
        public static long Id { get; private set; } = 0;
        public static string Name { get; private set; }
        public static string SurName { get; private set; }
        public static string Permission { get; private set; }
        public static string Identifier { get; private set; }
        public static bool IsActive { get; private set; }

        private UserSingleton(long id, string name, string surname, string permission, string identifier, bool isActive)
        {
            Id = id;
            Name = name;
            SurName = surname;
            Permission = permission;
            Identifier = identifier;
            IsActive = isActive;
        }

        public static UserSingleton CreateInstance(long id, string name, string surname, string permission, string identifier, bool isActive)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new UserSingleton(id, name, surname, permission, identifier, isActive);
                }
            }

            return _instance;
        }

        public static UserSingleton GetInstance => _instance;
    }
}
