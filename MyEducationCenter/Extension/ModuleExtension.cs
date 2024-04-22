using Microsoft.EntityFrameworkCore;
using MyEducationCenter.DataLayer;
using MyEducationCenter.Logiclayer;

namespace MyEducationCenter.Extension;

public static class ModuleExtension
{
    public static async Task SynchronizeModulesAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var existingModules = await context
                .Modules
                .ToListAsync();

            var enumModuleNames = Enum
                .GetValues(typeof(ModuleCode))
                .Cast<ModuleCode>()
                .Select(e => e.ToString());

            var modulesToDelete = existingModules
                .Where(m => !enumModuleNames.Contains(m.Name)).ToList();

            var modulesToAdd = enumModuleNames
                .Except(existingModules.Select(m => m.Name))
                .Select(name => new Module { Name = name, CreatedUserId = 1 });

            if (modulesToDelete.Any())
                context.Modules.RemoveRange(modulesToDelete);
            
            if (modulesToAdd.Any())
                await context.Modules.AddRangeAsync(modulesToAdd);
            
            await context.SaveChangesAsync();
        }
    }
}
