namespace NpgSQLConnectionWrapper.Exceptions
{
    public class NotSupportedTypeException : Exception
    {
        public NotSupportedTypeException(Type type) : base($"{type.Name} is not child of DbContext")
        {

        }
    }
}
