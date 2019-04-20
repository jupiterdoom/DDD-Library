using Library.Logic.Common;

namespace Library.Logic.Model
{
    public class Role : ValueObject<Role>
    {
        public static readonly Role BookReader = new Role(RoleType.BookReader);
        
        public RoleType Type { get; private set; }

        protected Role() {}

        private Role(RoleType role) : this()
        {
            Type = role;
        }

        public bool IsArchivarius() => Type == RoleType.Archivarius; 

        public Role ChangeRole(RoleType role)
        {
            return new Role(role);
        }

        protected override bool EqualsCore(Role other)
        {
            return Type == other.Type;
        }

        protected override int GetHashCodeCore()
        {
            return Type.GetHashCode();
        }

        public enum RoleType
        {
            BookReader = 1,
            Archivarius = 2,
            Admin = 3
        }
    }
}
