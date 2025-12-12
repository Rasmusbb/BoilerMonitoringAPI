namespace BoilerMonitoringAPI.Interface
{
    public interface ICRUD<TDto, TDtoId>
    {
        Task<TDtoId> GetByIdAsync(Guid id);
        Task<TDtoId> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(Guid id, TDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

