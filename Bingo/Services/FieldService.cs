using Bingo.Context;
using Bingo.Models.Entity;
using Bingo.Services.Interfaces;

namespace Bingo.Services
{
    public class FieldService(BingoContext context) : IFieldService
    {
        public async Task MarkField(Guid fieldId)
        {
            Field field = context.Fields
                .FirstOrDefault(f => f.Id == fieldId);
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field), "Field cannot be null.");
            }
            field.IsMarked = true;
            context.Fields.Update(field);
            context.SaveChangesAsync();
        }

        public async Task UnmarkField(Guid fieldId)
        {
            Field field = context.Fields
                .FirstOrDefault(f => f.Id == fieldId);
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field), "Field cannot be null.");
            }
            field.IsMarked = false;
            context.Fields.Update(field);
            context.SaveChangesAsync();
        }
    }
}
