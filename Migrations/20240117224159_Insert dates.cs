using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesi.Migrations
{
    /// <inheritdoc />
    public partial class Insertdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Dates (Date)
            SELECT 
                DATEADD(HOUR, number, GETDATE()) AS Date
            FROM 
                master.dbo.spt_values
            WHERE 
                type = 'P'
                AND number BETWEEN 0 AND DATEDIFF(HOUR, GETDATE(), DATEADD(MONTH, 1, GETDATE()));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
