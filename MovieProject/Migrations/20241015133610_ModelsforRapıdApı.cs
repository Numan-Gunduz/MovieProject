using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieProject.Migrations
{
    /// <inheritdoc />
    public partial class ModelsforRapıdApı : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenresGenreId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MoviesMovieId",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Metascore",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "MovieGenres",
                newName: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "MovieID");

            migrationBuilder.RenameColumn(
                name: "MovieName",
                table: "Movies",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "GenreMovie",
                newName: "MoviesMovieID");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_MoviesMovieId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_MoviesMovieID");

            migrationBuilder.AddColumn<string>(
                name: "BigImage",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImdbLink",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Movies",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "GenresGenreId", "MoviesMovieID" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenresGenreId",
                table: "GenreMovie",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MoviesMovieID",
                table: "GenreMovie",
                column: "MoviesMovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenresGenreId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MoviesMovieID",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.DropColumn(
                name: "BigImage",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImdbLink",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "GenreMovie",
                newName: "MovieGenres");

            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Movies",
                newName: "MovieName");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieID",
                table: "MovieGenres",
                newName: "MoviesMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_MoviesMovieID",
                table: "MovieGenres",
                newName: "IX_MovieGenres_MoviesMovieId");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Metascore",
                table: "Movies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres",
                columns: new[] { "GenresGenreId", "MoviesMovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenresGenreId",
                table: "MovieGenres",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MoviesMovieId",
                table: "MovieGenres",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
