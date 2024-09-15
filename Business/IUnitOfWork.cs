namespace Business
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        IProductBusiness ProductBusiness { get; }
        IUserBusiness UserBusiness { get; }
    }
}
