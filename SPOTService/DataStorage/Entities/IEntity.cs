namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Интерфейс для сущностей с уникальным идентификатором.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        public int Id { get; set; }
    }
}
