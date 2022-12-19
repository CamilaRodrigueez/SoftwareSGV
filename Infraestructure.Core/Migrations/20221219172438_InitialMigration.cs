using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GA");

            migrationBuilder.EnsureSchema(
                name: "Master");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Class",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrClass = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ficha",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num_Ficha = table.Column<string>(maxLength: 15, nullable: true),
                    Nombre = table.Column<string>(maxLength: 300, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "Security",
                columns: table => new
                {
                    IdRol = table.Column<int>(nullable: false),
                    Rol = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TypePermission",
                schema: "Security",
                columns: table => new
                {
                    IdTypePermission = table.Column<int>(nullable: false),
                    TypePermission = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePermission", x => x.IdTypePermission);
                });

            migrationBuilder.CreateTable(
                name: "TypeUser",
                schema: "Security",
                columns: table => new
                {
                    IdTypeUser = table.Column<int>(nullable: false),
                    TypeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeUser", x => x.IdTypeUser);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    IdUser = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identification = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    Telefono = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "FichaClass",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClass = table.Column<int>(nullable: false),
                    IdFicha = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaClass_Class_IdClass",
                        column: x => x.IdClass,
                        principalSchema: "GA",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaClass_Ficha_IdFicha",
                        column: x => x.IdFicha,
                        principalSchema: "GA",
                        principalTable: "Ficha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Security",
                columns: table => new
                {
                    IdPermission = table.Column<int>(nullable: false),
                    Permission = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    IdTypePermission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.IdPermission);
                    table.ForeignKey(
                        name: "FK_Permission_TypePermission_IdTypePermission",
                        column: x => x.IdTypePermission,
                        principalSchema: "Security",
                        principalTable: "TypePermission",
                        principalColumn: "IdTypePermission",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excuse",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    ExcuseDate = table.Column<DateTime>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    NameImage = table.Column<string>(nullable: true),
                    IdUser = table.Column<int>(nullable: false),
                    IdFicha = table.Column<int>(nullable: false),
                    IdClass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excuse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excuse_Class_IdClass",
                        column: x => x.IdClass,
                        principalSchema: "GA",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excuse_Ficha_IdFicha",
                        column: x => x.IdFicha,
                        principalSchema: "GA",
                        principalTable: "Ficha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excuse_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaUser",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IdFicha = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaUser_Ficha_IdFicha",
                        column: x => x.IdFicha,
                        principalSchema: "GA",
                        principalTable: "Ficha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaUser_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presence",
                schema: "GA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoExcuse = table.Column<bool>(nullable: false),
                    Excuse = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    IdFicha = table.Column<int>(nullable: false),
                    IdClass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presence_Class_IdClass",
                        column: x => x.IdClass,
                        principalSchema: "GA",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presence_Ficha_IdFicha",
                        column: x => x.IdFicha,
                        principalSchema: "GA",
                        principalTable: "Ficha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presence_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    Seen = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SeenDate = table.Column<DateTime>(nullable: true),
                    IdUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolUser",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(nullable: false),
                    IdUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Security",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUser_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeUser",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTypeUser = table.Column<int>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    Selected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTypeUser_TypeUser_IdTypeUser",
                        column: x => x.IdTypeUser,
                        principalSchema: "Security",
                        principalTable: "TypeUser",
                        principalColumn: "IdTypeUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTypeUser_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(nullable: false),
                    IdPermission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permission_IdPermission",
                        column: x => x.IdPermission,
                        principalSchema: "Security",
                        principalTable: "Permission",
                        principalColumn: "IdPermission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Rol_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Security",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excuse_IdClass",
                schema: "GA",
                table: "Excuse",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_Excuse_IdFicha",
                schema: "GA",
                table: "Excuse",
                column: "IdFicha");

            migrationBuilder.CreateIndex(
                name: "IX_Excuse_IdUser",
                schema: "GA",
                table: "Excuse",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "Index_NumFicha",
                schema: "GA",
                table: "Ficha",
                column: "Num_Ficha",
                unique: true,
                filter: "[Num_Ficha] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FichaClass_IdClass",
                schema: "GA",
                table: "FichaClass",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_FichaClass_IdFicha",
                schema: "GA",
                table: "FichaClass",
                column: "IdFicha");

            migrationBuilder.CreateIndex(
                name: "IX_FichaUser_IdFicha",
                schema: "GA",
                table: "FichaUser",
                column: "IdFicha");

            migrationBuilder.CreateIndex(
                name: "IX_FichaUser_IdUser",
                schema: "GA",
                table: "FichaUser",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_IdClass",
                schema: "GA",
                table: "Presence",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_IdFicha",
                schema: "GA",
                table: "Presence",
                column: "IdFicha");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_IdUser",
                schema: "GA",
                table: "Presence",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IdUser",
                schema: "Master",
                table: "Notification",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_IdTypePermission",
                schema: "Security",
                table: "Permission",
                column: "IdTypePermission");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_IdPermission",
                schema: "Security",
                table: "RolesPermissions",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_IdRol",
                schema: "Security",
                table: "RolesPermissions",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_IdRol",
                schema: "Security",
                table: "RolUser",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_IdUser",
                schema: "Security",
                table: "RolUser",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "Index_Email",
                schema: "Security",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_Identification",
                schema: "Security",
                table: "User",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeUser_IdTypeUser",
                schema: "Security",
                table: "UserTypeUser",
                column: "IdTypeUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeUser_IdUser",
                schema: "Security",
                table: "UserTypeUser",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excuse",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "FichaClass",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "FichaUser",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "Presence",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "RolesPermissions",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RolUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserTypeUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Class",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "Ficha",
                schema: "GA");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "TypeUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "TypePermission",
                schema: "Security");
        }
    }
}
