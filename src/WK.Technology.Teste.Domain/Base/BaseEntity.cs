namespace WK.Technology.Teste.Domain.Base
{
    /// <summary>
    /// Classe base abstrata de domínio <see cref="BaseEntity"/>
    /// </summary>
    public abstract class BaseEntity<TId>
        where TId : IComparable, IConvertible, IComparable<TId>, IEquatable<TId>
    {
        /// <summary>
        /// Identificador único universal para a entidade.
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Usuario da inclusão do registro.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Timestamp for when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Usuario da alteração do registro.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Timestamp for when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
