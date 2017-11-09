namespace UserStorageInterfaces
{
    public interface IEntityValidator<T>
    {
        bool Validate(T user);
    }
}
