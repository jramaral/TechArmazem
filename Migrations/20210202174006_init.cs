using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArmazemAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteFornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteFornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "varchar(80)", nullable: false),
                    QtdeEstoque = table.Column<int>(type: "int", nullable: false),
                    ValorVenda = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    ValorCompra = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    CategoriaProduto = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Cpf = table.Column<string>(type: "VARCHAR(14)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    NomeUsuario = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompraVendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteFornecedorId = table.Column<int>(nullable: false),
                    DataMovimentacao = table.Column<DateTime>(nullable: false),
                    TipoMovimentacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraVendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraVendas_ClienteFornecedores_ClienteFornecedorId",
                        column: x => x.ClienteFornecedorId,
                        principalTable: "ClienteFornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    CompraVendaId = table.Column<int>(type: "int", nullable: false),
                    Qtde = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    TipoMovimentacao = table.Column<string>(type: "VARCHAR(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_CompraVendas_CompraVendaId",
                        column: x => x.CompraVendaId,
                        principalTable: "CompraVendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClienteFornecedores",
                columns: new[] { "Id", "Nome", "Tipo" },
                values: new object[] { 1, "Maria da Silva", "C" });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaProduto", "NomeProduto", "QtdeEstoque", "ValorCompra", "ValorVenda" },
                values: new object[,]
                {
                    { 1, "DOCE", "PRODUTO TESTE 1", 25, 0m, 150m },
                    { 2, "SALGADO", "PRODUTO TESTE 2", 25, 0m, 15m }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Cpf", "Email", "Nome", "NomeUsuario", "Role", "Senha" },
                values: new object[] { 1, "12345678978", "jose@email.com", "jose roberto", "jose", "admin", "123456" });

            migrationBuilder.CreateIndex(
                name: "IX_CompraVendas_ClienteFornecedorId",
                table: "CompraVendas",
                column: "ClienteFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CompraVendaId",
                table: "Item",
                column: "CompraVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ProdutoId",
                table: "Item",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CompraVendas");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "ClienteFornecedores");
        }
    }
}
