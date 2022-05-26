namespace Krita.Configurations
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<Connection>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
