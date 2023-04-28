using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RollerCoasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturerers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    County = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturerers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    County = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RideType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RollerCoasterDesign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Design = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollerCoasterDesign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RollerCoasterType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollerCoasterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Member = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteTypeId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_NoteType_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "NoteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThemeParks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeParkName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    County = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeParks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemeParks_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    ThemeParkId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    YearOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatingStatusId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attractions_Manufacturerers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturerers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attractions_OperatingStatuses_OperatingStatusId",
                        column: x => x.OperatingStatusId,
                        principalTable: "OperatingStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attractions_ThemeParks_ThemeParkId",
                        column: x => x.ThemeParkId,
                        principalTable: "ThemeParks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rides_RideType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "RideType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RollerCoasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    RollerCoasterTypeId = table.Column<int>(type: "int", nullable: false),
                    RollerCoasterDesignId = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    TrackLength = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MaxSpeed = table.Column<decimal>(type: "decimal(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollerCoasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RollerCoasters_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RollerCoasters_RollerCoasterDesign_RollerCoasterDesignId",
                        column: x => x.RollerCoasterDesignId,
                        principalTable: "RollerCoasterDesign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RollerCoasters_RollerCoasterType_RollerCoasterTypeId",
                        column: x => x.RollerCoasterTypeId,
                        principalTable: "RollerCoasterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NoteType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Standard" },
                    { 2, "HighPriority" },
                    { 3, "LowPriority" }
                });

            migrationBuilder.InsertData(
                table: "OperatingStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "Operating" },
                    { 2, "Closed" },
                    { 3, "UnderConstruction" },
                    { 4, "SBNO" },
                    { 5, "Demolished" }
                });

            migrationBuilder.InsertData(
                table: "RideType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "FlatRide" },
                    { 2, "DarkRide" },
                    { 3, "WaterRide" },
                    { 4, "TransportRide" },
                    { 5, "TracklessRide" }
                });

            migrationBuilder.InsertData(
                table: "RollerCoasterDesign",
                columns: new[] { "Id", "Design" },
                values: new object[,]
                {
                    { 1, "SitDown" },
                    { 2, "Inverted" },
                    { 3, "Suspended" },
                    { 4, "Wing" },
                    { 5, "Flying" },
                    { 6, "StandUp" },
                    { 7, "Bobsled" },
                    { 8, "Pipeline" },
                    { 9, "Spinning" },
                    { 10, "FourDimensional" }
                });

            migrationBuilder.InsertData(
                table: "RollerCoasterType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Wooden" },
                    { 2, "Steel" },
                    { 3, "Hybrid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_ManufacturerId",
                table: "Attractions",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_OperatingStatusId",
                table: "Attractions",
                column: "OperatingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_ThemeParkId",
                table: "Attractions",
                column: "ThemeParkId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_AttractionId",
                table: "Rides",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_TypeId",
                table: "Rides",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RollerCoasters_AttractionId",
                table: "RollerCoasters",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_RollerCoasters_RollerCoasterDesignId",
                table: "RollerCoasters",
                column: "RollerCoasterDesignId");

            migrationBuilder.CreateIndex(
                name: "IX_RollerCoasters_RollerCoasterTypeId",
                table: "RollerCoasters",
                column: "RollerCoasterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemeParks_OwnerId",
                table: "ThemeParks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "RollerCoasters");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NoteType");

            migrationBuilder.DropTable(
                name: "RideType");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "RollerCoasterDesign");

            migrationBuilder.DropTable(
                name: "RollerCoasterType");

            migrationBuilder.DropTable(
                name: "Manufacturerers");

            migrationBuilder.DropTable(
                name: "OperatingStatuses");

            migrationBuilder.DropTable(
                name: "ThemeParks");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
