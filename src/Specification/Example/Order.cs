namespace Specification.Example
{
    public class Order
    {
        public Order(long id, bool isPaid, bool isReadyToShip)
        {
            Id = id;
            IsPaid = isPaid;
            IsReadyToShip = isReadyToShip;
        }

        public long Id { get; }
        
        public bool IsPaid { get; private set; }
        
        public bool IsReadyToShip { get; private set; }

        public void MarkAsPaid()
        {
            IsPaid = true;
        }
    }
}