namespace Invoicing.Helpers
{
    public class ValidationError
    {
        public string Field { get; set; }

        public string Message { get; set; }

        public ValidationError(string message, string field = "")
        {
            Field = field;
            Message = message;
        }

        public static ValidationError CreateError(string message)
        {
            return new ValidationError(message);
        }

    }
}
