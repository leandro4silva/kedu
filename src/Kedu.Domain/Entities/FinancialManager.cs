using Kedu.Domain.SeedWork;
using Kedu.Domain.Validation;

namespace Kedu.Domain.Entities
{
    public class FinancialManager : EntityBase
    {
        public string Name { get; private set; }

        public FinancialManager(string name) : base()
        {
            Name = name;
            Validate();
        }

        public void Update(string name)
        {
            Name = name;
            Validate();
        }

        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        }
    }
}
