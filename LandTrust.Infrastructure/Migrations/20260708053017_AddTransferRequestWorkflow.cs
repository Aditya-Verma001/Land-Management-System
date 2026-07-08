using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandTrust.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferRequestWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FraudScore = table.Column<int>(type: "integer", nullable: false),
                    RiskLevel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OfficerRemarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    VerifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RejectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRequests", x => x.RequestId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnershipRecords_PropertyId",
                table: "OwnershipRecords",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnershipRecords_Properties_PropertyId",
                table: "OwnershipRecords",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnershipRecords_Properties_PropertyId",
                table: "OwnershipRecords");

            migrationBuilder.DropTable(
                name: "TransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_OwnershipRecords_PropertyId",
                table: "OwnershipRecords");
        }
    }
}
