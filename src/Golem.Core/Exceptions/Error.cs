namespace Golem.Core.Exceptions
{
    /// <summary>
    ///     Error model
    /// </summary>
    public class Error
    {
        /// <summary>
        ///     Specifies the field to which the error relates
        /// </summary>
        public string RelatedTo { get; set; }

        /// <summary>
        ///     Error message
        /// </summary>
        public string Message { get; set; }
    }
}
