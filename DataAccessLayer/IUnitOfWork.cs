using System.Linq;
namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        public BethanysPieShop.IUnitOfWork BethanyPieShopUnitOfWork { get; }
    }
}
