using api.Models;

namespace api.Services
{

    public class CustomerOrderService
    {
        // private readonly AppDBContext _appDbContext;

        public CustomerOrderService()
        {
        }

        public static List<CustomerOrderModel> _customerOrders = new List<CustomerOrderModel>(){
            new CustomerOrderModel{
                UserId = Guid.NewGuid(),
                OrderStatus = Status.delivered,
                Payment = new OrderPayment(PaymentMethod.CreditCard, 2000)
            },
            new CustomerOrderModel{
                UserId = Guid.NewGuid(),
                OrderStatus = Status.shipped,
                Payment = new OrderPayment(PaymentMethod.Visa, 500)
            },
            new CustomerOrderModel{
                UserId = Guid.NewGuid(),
                OrderStatus = Status.processing,
                Payment = new OrderPayment(PaymentMethod.Cash, 2000)
            }
        };

        public IEnumerable<CustomerOrderModel> GetAllCustomerOrdersService()
        {

            return _customerOrders;
        }

        public CustomerOrderModel? GetCustomerOrderByIdService(Guid orderId)
        {
            return _customerOrders.Find(order => order.OrderId == orderId);
        }
        public CustomerOrderModel? CreateCustomerOrderService(CustomerOrderModel newCustomerOrder)
        {
            _customerOrders.Add(newCustomerOrder);
            return newCustomerOrder;
        }

        public CustomerOrderModel? UpdateCustomerOrderService(Guid orderId, CustomerOrderModel updatedCustomerOrder)
        {
            var exsistingOrder = _customerOrders.FirstOrDefault(order => order.OrderId == orderId);
            if (exsistingOrder != null)
            {
                exsistingOrder.OrderStatus = updatedCustomerOrder.OrderStatus;
            }

            return exsistingOrder;
        }

        public bool DeleteCustomerOrderService(Guid orderId)
        {
            var orderToBeDeleted = _customerOrders.FirstOrDefault(order => order.OrderId == orderId);
            if (orderToBeDeleted != null)
            {
                _customerOrders.Remove(orderToBeDeleted);
                return true;
            }
            return false;
        }
    }

}