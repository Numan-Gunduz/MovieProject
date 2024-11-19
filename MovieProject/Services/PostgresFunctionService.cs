using Microsoft.EntityFrameworkCore;
using MovieProject.Context;

namespace MovieProject.Services
{
    public class PostgresFunctionService
    {
        private readonly MovieContext _context;

        public PostgresFunctionService(MovieContext context)
        {
            _context = context;
        }

        // Fonksiyon var mı kontrol et
        public bool FunctionExists(string functionName)
        {
            var query = @"
        SELECT EXISTS (
            SELECT 1
            FROM pg_proc
            WHERE proname = @functionName
        );
    ";

            // ExecuteScalar kullanımı daha uygundur
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new Npgsql.NpgsqlParameter("functionName", functionName));
                _context.Database.OpenConnection();

                var result = command.ExecuteScalar();
                return result != null && (bool)result;
            }
        }

        // Fonksiyonu oluştur
        public void CreateFunction()
        {
            var createFunctionQuery = @"
                CREATE OR REPLACE FUNCTION example_function()
                RETURNS void AS $$
                BEGIN
                    RAISE NOTICE 'Bu, PostgreSQL fonksiyonunun çalıştığını gösterir!';
                END;
                $$ LANGUAGE plpgsql;
            ";

            _context.Database.ExecuteSqlRaw(createFunctionQuery);
        }
        public void CreateFunctionCounter()
        {
            var createFunctionQuery = @"
                CREATE OR REPLACE FUNCTION get_movie_count()
                RETURNS INTEGER AS $$
                BEGIN
                    RETURN (SELECT COUNT(*) FROM ""Movies"");
                END;
                $$ LANGUAGE plpgsql;
            ";

            _context.Database.ExecuteSqlRaw(createFunctionQuery);
        }
        public int GetMovieCount()
        {
            var query = "SELECT get_movie_count();";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();

                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }


        // Fonksiyonu çağır
        public void CallFunction(string functionName)
        {
            var callFunctionQuery = $"SELECT {functionName}();";
            _context.Database.ExecuteSqlRaw(callFunctionQuery);
        }
    }
}
