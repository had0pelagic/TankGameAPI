namespace TankGameAPI.Utils.Messages
{
    public static class Messages
    {
        public static class Tank
        {
            public static readonly string NotFound = "Tank was not found";
            public static readonly string NoTanks = "No tanks in the field";
            public static readonly string Exists = "Tank name already exists";
            public static readonly string Collision = "Collision with obstacle";
        }

        public static class User
        {
            public static readonly string NotFound = "User was not found";
            public static readonly string Exists = "Username already exist";
        }

        public static class Field
        {
            public static readonly string NotFound = "Field was not found";
            public static readonly string OutOfBorder = "Moving out of field is prohibited";
            public static readonly string AlreadyExists = "Field already exists";
        }
    }
}
