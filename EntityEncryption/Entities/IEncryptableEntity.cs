namespace EntityEncryption.Entities
{
    public interface IEncryptableEntity
    {
        /// <summary>
        /// Initialisation vector for any encrypted property values; null if record isn't encrypted.
        /// </summary>
        string Iv { get; set; }
    }
}