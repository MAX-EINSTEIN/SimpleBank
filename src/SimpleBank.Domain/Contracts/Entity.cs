namespace SimpleBank.Domain.Contracts
{
    public abstract class Entity
    {
        public long Id { get; private set; }

        protected Entity()
        {
        }

        protected Entity(long id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Entity;

            if (other is null)
                return false;    

            if (ReferenceEquals(this, other))
                return true;

            if (GetUnproxiedType(this) != GetUnproxiedType(other))
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }

        internal static Type GetUnproxiedType(object obj)
        {
            const string EFCoreProxyPrefix = "Castle.Proxies.";

            Type type = obj.GetType();
            string typeString = type.ToString();

            if (typeString.Contains(EFCoreProxyPrefix))
                return type.BaseType!;

            return type;
        }
    }
}

