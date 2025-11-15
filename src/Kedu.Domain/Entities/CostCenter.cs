using Kedu.Domain.SeedWork;
using Kedu.Domain.Validation;

namespace Kedu.Domain.Entities
{
    public class CostCenter : EntityBase
    {
        public string Name { get; private set; }

        public CostCenter(string name)
        {
            Name = name.ToUpper();
            Validate();
        }

        public void Update(string name)
        {
            name = name.ToUpper();
            Validate();
        }

        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        }
    }
}
