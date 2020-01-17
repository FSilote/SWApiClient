using System;

namespace Kneat.SW.Domain.Entity.Base
{
    public abstract class BaseEntity
    {
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(obj, this))
                return false;

            if (obj.GetType() != this.GetType())
                return false;

            BaseEntity toCheck = obj as BaseEntity;

            return this.Url.Equals(toCheck.Url);
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode()
                 ^ this.Created.GetHashCode()
                 ^ this.Edited.GetHashCode();
        }

        public override string ToString()
        {
            return this.Url;
        }

        public static bool operator == (BaseEntity optionsA, BaseEntity optionsB)
        {
            return Object.Equals(optionsA, optionsB);
        }

        public static bool operator != (BaseEntity optionsA, BaseEntity optionsB)
        {
            return !Object.Equals(optionsA, optionsB);
        }
    }
}
