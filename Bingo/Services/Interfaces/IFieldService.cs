using Bingo.Models.Entity;

namespace Bingo.Services.Interfaces
{
    public interface IFieldService
    {
        Task MarkField(Guid fieldId);
        Task UnmarkField(Guid fieldId);
    }
}
