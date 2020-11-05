namespace DataAccessLayer.BethanysPieShop
{
    public interface IUserRepository:IRepository<Models.User>
    {
        public Models.User GetUserById(System.Guid Id);
        public Models.User GetUserByUsername(string userName);
    }
}
