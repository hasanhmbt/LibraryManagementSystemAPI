namespace JWTAuthentication.Repositories.Abstract
{
    public interface IAuthHelperRepository
    {
        int GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}
