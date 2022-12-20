dotnet aspnet-codegenerator controller -name CollectionsController -async -api -m Collection -dc DefaultDBContext -outDir Controllers

dotnet aspnet-codegenerator controller -name UsersController -async -api -m User -dc DefaultDBContext -outDir Controllers


dotnet ef dbcontext scaffold "connection-string" MySql.EntityFrameworkCore -o sakila -f
 


 docker run -d -p 5432:5432 -e POSTGRES_DB=mtg  -e POSTGRES_USER=mtg  -e POSTGRES_PASSWORD=password   postgres