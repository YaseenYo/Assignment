using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechnicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TechnicianRefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TechnicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderId);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId");
                });

            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianId", "FirstName", "LastName", "Status" },
                values: new object[,]
                {
                    { new Guid("47aee500-1bc3-4e7b-a590-13641c934cb5"), "yaseen", "Ahmed", false },
                    { new Guid("d50d48f3-096c-4b0a-8152-43ed4d2b86ad"), "Karan", "Lala", true },
                    { new Guid("d88c457f-4513-402c-9b12-b6a0418fb06c"), "yaseen", "Ahmed", true }
                });

            migrationBuilder.InsertData(
                table: "WorkOrders",
                columns: new[] { "WorkOrderId", "DateTime", "Place", "TechnicianId", "TechnicianRefId" },
                values: new object[,]
                {
                    { new Guid("45f925be-be1a-45ad-b930-097cc1e4f3f4"), new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangalore Ramanagar", null, null },
                    { new Guid("a82a42b2-28bf-46de-8c92-cdef42daa19e"), new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangalore Kasargudu", null, null },
                    { new Guid("b9193ebc-e41c-4b04-9841-8a95ec4cfd30"), new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delhi kudur", null, null },
                    { new Guid("ce10c9a1-4e79-42e5-894b-fc38f0a055c5"), new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangalore magadi", null, null },
                    { new Guid("ddd0833d-f911-42d5-9909-ab77630d16ec"), new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tumkur sira", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_TechnicianId",
                table: "WorkOrders",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Technicians");
        }
    }
}
