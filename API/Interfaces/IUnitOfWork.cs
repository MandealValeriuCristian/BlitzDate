namespace API.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IMessageRepository MessageRepository { get; }
    ILikesReporsitory LikesReporsitory { get; }
    Task<bool> Complete();
    bool HasChanges();
}