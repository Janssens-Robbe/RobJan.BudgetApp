using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Entities.Base
{
    internal abstract class AggregateRoot
    {
        private IList<DomainEvent> _changes = new List<DomainEvent>();

        protected void Apply(DomainEvent @event)
        {
            throw new NotImplementedException();
        }

        protected abstract void EnsureValidation();
    }
}
