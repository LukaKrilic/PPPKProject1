using Npgsql;

using var conn = new NpgsqlConnection("Host=localhost;Username=user;Password=password;Database=medicaldb");
conn.Open();
Console.WriteLine(new NpgsqlCommand("SELECT version()", conn).ExecuteScalar());
