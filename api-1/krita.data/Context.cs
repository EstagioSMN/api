namespace Krita.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=192.168.7.12;Initial Catalog=GestaoSMNHomolog;Persist Security Info=True;User ID=userSMN;Password=smn2022@!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(property => property.Id)
                    .HasName("pk_usuario");


                entity.ToTable("usuario");

                entity.HasIndex(x => x.Email, "unq_usuario");

                entity.Property(x => x.Apelido)
                    .HasColumnName("apelido")
                    .HasMaxLength(36);

                entity.Property(x => x.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(x => x.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(35);

                entity.Property(x => x.DataExpiracaoSenha)
                    .HasColumnName("dataExpiracaoSenha");

                entity.Property(x => x.DataBloqueio)
                    .HasColumnName("dataBloqueio");

                entity.Property(x => x.UsuarioCadastro)
                    .HasColumnName("usuarioCadastro");

                entity.Property(x => x.DataCadastro)
                    .HasColumnName("dataCadastro");

                entity.Property(x => x.UsuarioUltimaAlteracao)
                    .HasColumnName("usuarioUltimaAlteracao");

                entity.Property(x => x.DataUltimaAlteracao)
                    .HasColumnName("dataUltimaAlteracao");

                entity.HasMany(x => x.Colaboradores).WithOne(x => x.Usuario).HasForeignKey(x => x.IdUsuario);
            });
            modelBuilder.Entity<Colaborador>(entity =>
            {
                entity.HasKey(proprety => proprety.Id)
                    .HasName("pk_colaborador");


                entity.ToTable("colaborador");

                entity.Property(x => x.IdUsuario)
                    .HasColumnName("idUsuario");

                entity.Property(x => x.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(60);

                entity.Property(x => x.DataAdmissao)
                    .HasColumnName("dataAdmissao");

                entity.Property(x => x.UsuarioCadastro)
                    .HasColumnName("usuarioCadastro");

                entity.Property(x => x.DataCadastro)
                    .HasColumnName("dataCadastro");

                entity.Property(x => x.UsuarioUltimaAlteracao)
                    .HasColumnName("usuarioUltimaAlteracao");

                entity.Property(x => x.DataUltimaAlteracao)
                    .HasColumnName("dataUltimaAlteracao");

                entity.HasOne(x => x.Usuario).WithMany(x => x.Colaboradores).HasForeignKey(x => x.IdUsuario);

            });
        }
    }
}
