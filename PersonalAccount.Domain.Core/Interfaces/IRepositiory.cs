namespace PersonalAccount.Domain.Core.Interfaces
{
    internal interface IRepositiory<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T? GetByLogin(string login);
        T GetById(int idUser);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
