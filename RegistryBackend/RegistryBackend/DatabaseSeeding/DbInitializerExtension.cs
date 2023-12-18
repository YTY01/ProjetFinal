namespace RegistryBackend.DatabaseSeeding
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app)
        {

            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<RegistryDb>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }
}
