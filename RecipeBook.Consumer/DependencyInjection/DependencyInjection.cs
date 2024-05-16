using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Consumer.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddConsumer(this IServiceCollection services)
        {
            services.AddHostedService<RabbitMqListener>();
        }
    }
}
