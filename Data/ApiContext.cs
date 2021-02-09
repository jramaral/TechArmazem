using System;
using System.Collections.Generic;
using ArmazemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmazemAPI.Data
{
    public class ApiContext : DbContext
    {
     

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ClienteFornecedor> ClienteFornecedores { get; set; }
        public DbSet<CompraVenda> CompraVendas { get; set; }
        public DbSet<Item> Items { get; set; }
        
        

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(p =>
            {
                p.ToTable("Usuario");
                p.HasKey(a => a.Id);
                p.Property(a => a.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(a => a.NomeUsuario).HasColumnType("VARCHAR(30)").IsRequired();
                p.Property(a => a.Cpf).HasColumnType("VARCHAR(14)");
                p.Property(a => a.Email).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(a => a.Role).HasColumnType("VARCHAR(50)");
                p.Property(a => a.Senha).HasColumnType("VARCHAR(50)").IsRequired();
            });

            modelBuilder.Entity<Produto>(p =>
            {
                p.ToTable("Produto");
                p.HasKey(c => c.Id);
                p.Property(c => c.NomeProduto).HasColumnType("varchar(80)").IsRequired();
                p.Property(c => c.QtdeEstoque).HasColumnType("int");
                p.Property(c => c.ValorVenda).HasColumnType("DECIMAL(10,2)").IsRequired();
                p.Property(c => c.ValorCompra).HasColumnType("DECIMAL(10,2)");
                p.Property(c => c.CategoriaProduto).HasColumnType("VARCHAR(10)").IsRequired().HasConversion(
                    v => v.ToString(),
                    v => (Categoria)Enum.Parse(typeof(Categoria), v));
            });


            modelBuilder.Entity<Item>(p =>
            {
                
                p.ToTable("Item");
                p.HasKey(c => c.Id);
                p.Property(c => c.Qtde).HasColumnType("int").IsRequired();
                p.Property(c => c.CompraVendaId).HasColumnType("int").IsRequired();
                p.Property(c => c.ProdutoId).HasColumnType("int").IsRequired();
                p.Property(c => c.ValorUnitario).HasColumnType("DECIMAL(10,2)");
                p.Property(c => c.ValorTotal).HasColumnType("DECIMAL(10,2)");
                p.Property(c => c.TipoMovimentacao).HasColumnType("VARCHAR(1)");
                

            });
            modelBuilder
                .Entity<ClienteFornecedor>()
                .Property(e => e.Tipo)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoCadastro)Enum.Parse(typeof(TipoCadastro), v));
            
            modelBuilder
                .Entity<CompraVenda>()
                .Property(e => e.TipoMovimentacao)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoMovimentacao)Enum.Parse(typeof(TipoMovimentacao), v));
           
            //has data
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Nome = "jose roberto",
                    NomeUsuario = "jose",
                    Senha = "123456",
                    Cpf = "12345678978",
                    Email = "jose@email.com",
                    Role = "admin"
                }
            );
            modelBuilder.Entity<ClienteFornecedor>().HasData(
                new ClienteFornecedor()
                {
                    Id = 1,
                    Nome = "Maria da Silva",
                    Tipo = TipoCadastro.C
                 
                }
            );
            modelBuilder.Entity<Produto>().HasData(
                new List<Produto>()
                {
                    new Produto()
                    {
                        Id = 1,
                        NomeProduto = "PRODUTO TESTE 1",
                        QtdeEstoque = 25,
                        ValorVenda = 150,
                        ValorCompra = 0,
                        CategoriaProduto = Categoria.DOCE
                    },
                    new Produto()
                    {
                        Id = 2,
                        NomeProduto = "PRODUTO TESTE 2",
                        QtdeEstoque = 25,
                        ValorVenda = 15,
                        ValorCompra = 0,
                        CategoriaProduto = Categoria.SALGADO
                    },
                });
        }
    }
}