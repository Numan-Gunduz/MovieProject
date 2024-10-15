using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_movieGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieID",
                table: "Genres",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieID",
                table: "Genres",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieID",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_MovieID",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresGenreId = table.Column<int>(type: "int", nullable: false),
                    MoviesMovieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresGenreId, x.MoviesMovieID });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesMovieID",
                        column: x => x.MoviesMovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesMovieID",
                table: "GenreMovie",
                column: "MoviesMovieID");
        }
    }
}
