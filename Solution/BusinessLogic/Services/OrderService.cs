using AutoMapper;
using BusinessLogic.Interfaces;
using DataAccess;
using Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        UnitOfWork _unitOfWork;
        IMapper _mapper;
        ILogger<OrderService> _logger;
        public OrderService(UnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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

        public async Task Create(Order card)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.OrderRepository.Create(card);

                    await transaction.CommitAsync();

                    _logger.LogInformation($"New card with id: {card.Id} was creared.");
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Exception while creating new card. {ex.Message}");
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(int id, Order updatedCard)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.OrderRepository.Update(id, updatedCard);

                    await transaction.CommitAsync();

                    _logger.LogInformation($"Card with id: {updatedCard.Id} was updated.");
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Exception while updating the card with id: {id}. {ex.Message}");
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Order> Delete(int id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var deletedCard = await _unitOfWork.OrderRepository.Delete(id);

                    await transaction.CommitAsync();

                    _logger.LogInformation($"Card with id: {id} was deleted.");

                    return deletedCard;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Exception while deleting the card with id: {id}. {ex.Message}");
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public bool IsUserOrderOwner(int userId, int orderId)
        {
            return _unitOfWork.OrderRepository.IsUserOrderOwner(userId, orderId);
        }
    }
}
