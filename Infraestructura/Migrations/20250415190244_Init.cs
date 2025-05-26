﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "ApprovalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ApprovalStatus", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "ApproverRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ApproverRole", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Area", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "ProjectType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ProjectType", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_User", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_User_ApproverRole_Role",
                        column: x => x.Role,
                        principalTable: "ApproverRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "ApprovalRule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ApprovalRule", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_ApprovalRule_ApproverRole_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "ApproverRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ApprovalRule_Area_Area",
                        column: x => x.Area,
                        principalTable: "Area",
                        principalColumn: "Id");
                    _ = table.ForeignKey(
                        name: "FK_ApprovalRule_ProjectType_Type",
                        column: x => x.Type,
                        principalTable: "ProjectType",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "ProjectProposal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ProjectProposal", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_ProjectProposal_ApprovalStatus_Status",
                        column: x => x.Status,
                        principalTable: "ApprovalStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ProjectProposal_Area_Area",
                        column: x => x.Area,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ProjectProposal_ProjectType_Type",
                        column: x => x.Type,
                        principalTable: "ProjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ProjectProposal_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "ProjectApprovalStep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApproverUserId = table.Column<int>(type: "int", nullable: true),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observations = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ProjectApprovalStep", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApprovalStatus_Status",
                        column: x => x.Status,
                        principalTable: "ApprovalStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApproverRole_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "ApproverRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposalId",
                        column: x => x.ProjectProposalId,
                        principalTable: "ProjectProposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    _ = table.ForeignKey(
                        name: "FK_ProjectApprovalStep_User_ApproverUserId",
                        column: x => x.ApproverUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Observed" }
                });

            _ = migrationBuilder.InsertData(
                table: "ApproverRole",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Líder de Área" },
                    { 2, "Gerente" },
                    { 3, "Director" },
                    { 4, "Comité Técnico" }
                });

            _ = migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Finanzas" },
                    { 2, "Tecnología" },
                    { 3, "Recursos Humanos" },
                    { 4, "Operaciones" }
                });

            _ = migrationBuilder.InsertData(
                table: "ProjectType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mejora de Procesos" },
                    { 2, "Innovación y Desarrollo" },
                    { 3, "Infrastructure" },
                    { 4, "Capacitación Interna" }
                });

            _ = migrationBuilder.InsertData(
                table: "ApprovalRule",
                columns: new[] { "Id", "ApproverRoleId", "Area", "MaxAmount", "MinAmount", "StepOrder", "Type" },
                values: new object[,]
                {
                    { 1L, 1, null, 100000m, 0m, 1, null },
                    { 2L, 2, null, 20000m, 5000m, 2, null },
                    { 3L, 2, 2, 20000m, 0m, 1, 2 },
                    { 4L, 3, null, 0m, 20000m, 3, null },
                    { 5L, 2, 1, 0m, 5000m, 2, 1 },
                    { 6L, 1, null, 10000m, 0m, 1, 2 },
                    { 7L, 4, 2, 10000m, 0m, 1, 1 },
                    { 8L, 2, 2, 30000m, 10000m, 2, null },
                    { 9L, 3, 3, 0m, 30000m, 2, null },
                    { 10L, 4, null, 50000m, 0m, 1, 4 }
                });

            _ = migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "jferreyra@unaj.com", "José Ferreyra", 2 },
                    { 2, "alucero@unaj.com", "Ana Lucero", 1 },
                    { 3, "gmolinas@unaj.com", "Gonzalo Molinas", 2 },
                    { 4, "lolivera@unaj.com", "Lucas Olivera", 3 },
                    { 5, "dfagundez@unaj.com", "Danilo Fagundez", 4 },
                    { 6, "ggalli@unaj.com", "Gabriel Galli", 4 }
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_ApproverRoleId",
                table: "ApprovalRule",
                column: "ApproverRoleId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_Area",
                table: "ApprovalRule",
                column: "Area");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_Type",
                table: "ApprovalRule",
                column: "Type");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverRoleId",
                table: "ProjectApprovalStep",
                column: "ApproverRoleId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverUserId",
                table: "ProjectApprovalStep",
                column: "ApproverUserId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ProjectProposalId",
                table: "ProjectApprovalStep",
                column: "ProjectProposalId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_Status",
                table: "ProjectApprovalStep",
                column: "Status");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Area",
                table: "ProjectProposal",
                column: "Area");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_CreatedBy",
                table: "ProjectProposal",
                column: "CreatedBy");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Status",
                table: "ProjectProposal",
                column: "Status");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Type",
                table: "ProjectProposal",
                column: "Type");

            _ = migrationBuilder.CreateIndex(
                name: "IX_User_Role",
                table: "User",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "ApprovalRule");

            _ = migrationBuilder.DropTable(
                name: "ProjectApprovalStep");

            _ = migrationBuilder.DropTable(
                name: "ProjectProposal");

            _ = migrationBuilder.DropTable(
                name: "ApprovalStatus");

            _ = migrationBuilder.DropTable(
                name: "Area");

            _ = migrationBuilder.DropTable(
                name: "ProjectType");

            _ = migrationBuilder.DropTable(
                name: "User");

            _ = migrationBuilder.DropTable(
                name: "ApproverRole");
        }
    }
}
