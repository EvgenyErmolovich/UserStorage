namespace UserStorageInterfaces
{
    public interface IEntityValidator<T>
    {
        void Validate(T user);
    }
}
