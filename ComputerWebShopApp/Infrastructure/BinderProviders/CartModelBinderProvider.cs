using ComputerShopDomainLibrary;
using ComputerWebShopApp.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComputerWebShopApp.Infrastructure.BinderProviders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            IHttpContextAccessor contextAccessor = context.Services.GetRequiredService<IHttpContextAccessor>();
            return context.Metadata.ModelType == typeof(Cart) ? 
                new CartModelBinder(contextAccessor) : null;
        }
    }
}
