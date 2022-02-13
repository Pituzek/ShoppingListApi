﻿using Microsoft.Extensions.DependencyInjection;
using ShoppingListApi.Models;
using ShoppingListApi.Services;

namespace ShoppingListApi.Bootstrap
{
    public static class TaxPoliciesSetup
    {
        public static void AddTaxPolicies(this IServiceCollection services)
        {
            services.AddSingleton<ITaxedShoppingListConverter, TaxedShoppingListConverter>();

            // Add dependency of ITaxPolicy
            services.AddSingleton<ITaxPolicy, ProgressiveTaxedPolicy>();

            // way to custom create a thing, not only inject it
            services.AddSingleton<ITaxPolicy, FixedTaxPolicy>(_ => new FixedTaxPolicy(1.01m));
        }
    }
}
