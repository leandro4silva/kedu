namespace Kedu.Domain.SeedWork
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime? ModifyDate { get; protected set; }
        
        protected EntityBase() 
        {
            SetCreated();
        }

        public void SetCreated()
        {
            CreateDate = DateTime.UtcNow;
        }

        public void SetModified()
        {
            ModifyDate = DateTime.UtcNow;
        }
    }
}
