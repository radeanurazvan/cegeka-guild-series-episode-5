using System;

namespace Cegeka.Guild.Pokeverse.Common
{
    public abstract class DocumentModel
    {
        public string Id { get; set; }

        public DateTime LastUpdate { get; set; }

        public abstract string GetCollectionName();

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentModel other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Id == Guid.Empty.ToString() || other.Id == Guid.Empty.ToString())
            {
                return false;
            }

            return Id == other.Id;
        }

        public static bool operator ==(DocumentModel a, DocumentModel b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(DocumentModel a, DocumentModel b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
