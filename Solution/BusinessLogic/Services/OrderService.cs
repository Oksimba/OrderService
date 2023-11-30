using BusinessLogic.Interfaces;
using DataAccess;
using Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        UnitOfWork _unitOfWork;
        ILogger<OrderService> _logger;
        ServiceErrorWrapper _errorWrapper;
        string _entityName = nameof(Order);
        string _servicName = nameof(OrderService);

        public OrderService(UnitOfWork unitOfWork, ILogger<OrderService> logger, ServiceErrorWrapper errorWrapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this._errorWrapper = errorWrapper;
        }
        public IEnumerable<Order> GetAll(int userId)
        {
            var users = _unitOfWork.OrderRepository.GetAll(userId);
            return users;
        }

        public Task<Order> Get(int id)
        {
            return _unitOfWork.OrderRepository.Get(id);
        }

        public async Task Create(Order order)
        {
            await _errorWrapper.ExecuteAsync(async () =>
            {
                order.OpenPrice = BitcoinSimulatorService.BitcoinValue;

                await _unitOfWork.OrderRepository.Create(order);

                _logger.LogInformation(LogMessages.OnEntityCreatingLog(order.Id, _entityName));
            },
            _unitOfWork,
            _servicName,
            LogMessages.OnEntityCreatingErrorLog(_entityName));
        }

        public async Task Update(int id, Order updatedOrder)
        {
            await _errorWrapper.ExecuteAsync(async () =>
            {
                var order = await _unitOfWork.OrderRepository.Get(id);
                if (order.Status)
                {
                    _logger.LogInformation(LogMessages.OnUpdatingClosedOrderLog(id));
                }
                else
                {
                    if (updatedOrder.Status)
                    {
                        CalculateProfit(updatedOrder);
                    }
                    await _unitOfWork.OrderRepository.Update(id, updatedOrder);

                    _logger.LogInformation(LogMessages.OnEntityUpdatingLog(id, _entityName));
                }
            },
            _unitOfWork,
            _servicName,
            LogMessages.OnEntityUpdatingErrorLog(id, _entityName));
        }

        public async Task<Order> Delete(int id)
        {
            return await _errorWrapper.ExecuteAsync(async() =>
            {
                var deletedOrder = await _unitOfWork.OrderRepository.Delete(id);

                _logger.LogInformation(LogMessages.OnEntityDeletingLog(id, _entityName));

                return deletedOrder;
            },
            _unitOfWork,
            _servicName,
            LogMessages.OnEntityDeletingErrorLog(id, _entityName));
        }

        public bool IsUserOrderOwner(int userId, int orderId)
        {
            return _unitOfWork.OrderRepository.IsUserOrderOwner(userId, orderId);
        }

        private void CalculateProfit(Order order)
        {
            order.ClosePrice = BitcoinSimulatorService.BitcoinValue;

            var buyProfit = (order.ClosePrice - order.OpenPrice) * order.Volume;

            if (order.Type)  //buy
                order.Profit = buyProfit;
            else //sell
                order.Profit = -buyProfit;
        }
    }
}
