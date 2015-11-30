using System;

namespace MKS.Core
{
    [Obsolete("Utilisez l'objet fournit par le connecteur de concurrency")]
    public abstract class ConcurrencyDefaultAdapter : IConcurrencyAdapter
    {
        public virtual ConcurrencyResult DoReservationWorkSpace(LogicalLock reservation)
        {
            return null;
        }

        public virtual ConcurrencyResult DoReservationOccurence(LogicalLock reservation)
        {
            return null;
        }

        public virtual ConcurrencyResult EndReservationWorkSpace(LogicalLock reservation)
        {
            return null;
        }

        public virtual ConcurrencyResult EndReservationOccurence(LogicalLock reservation)
        {
            return null;
        }

        public virtual ConcurrencyResult EndSessionReservations()
        {
            return null;
        }

        public virtual bool CheckReservationWorkSpaceExistence(LogicalLock reservation)
        {
            return false;
        }

        public virtual bool CheckReservationOccurenceExistence(LogicalLock reservation)
        {
            return false;
        }

        public virtual LogicalLock GetReservationWorkSpaceDetails(LogicalLock reservation)
        {
            return null;
        }

        public virtual LogicalLock GetReservationOccurenceDetails(LogicalLock reservation)
        {
            return null;
        }
    }
}