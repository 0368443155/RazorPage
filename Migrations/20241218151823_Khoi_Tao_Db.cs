using System;
using Bogus;
using CS58_Razor09_Entity_ASP.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS58_Razor09_Entity_ASP.Migrations
{
	/// <inheritdoc />
	public partial class Khoi_Tao_Db : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Articles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					Created = table.Column<DateTime>(type: "datetime2", nullable: false),
					Content = table.Column<string>(type: "ntext", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Articles", x => x.Id);
				});
			//Insert Data
			//Fake data: dữ liệu giả định - Bogus

			Randomizer.Seed = new Random(8675309);
			var fakerArticle = new Faker<Article>(); //khởi tạo đối tượng để tạo Fake data
			fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5)); //tham số đầu: thuộc tính, tham số 2 là dữ liệu thiết lập cho Faker
																			 //Lorem: GenerateWords, Sentence: câu (tối thiểu, thêm được bao nhiêu từ)
			fakerArticle.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2021,1,1), new DateTime(2024,1,1)));
			fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));// từ 1 đến 4 đoạn văn

			for (int i = 0; i <= 50; i++)
			{
				Article article = fakerArticle.Generate(); //tạo đối tượng
				migrationBuilder.InsertData(
						table: "Articles",
						columns: new[] { "Title", "Created", "Content" },
						values: new object[]
						{
						article.Title,
						article.Created,
						article.Content
						}
					);
			}

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Articles");
		}
	}
}
