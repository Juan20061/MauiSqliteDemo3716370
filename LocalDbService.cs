using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqliteDemo3716370
{
    public  class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DB_NAME));

            //le indicamos al sistema que cre la tabla de nuestro contexto 
            _connection.CreateTableAsync<Cliente>();

        }

        //metodo para alistar los registros de nuestra tabla
        public async Task<List<Cliente>> GetClientes()
        {
            return await _connection.Table<Cliente>().ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _connection.Table<Cliente>().Where(x => x.id == id).FirstOrDefaultAsync();
        }

        //Metodod para crear registros
        public  async Task Create(Cliente clientes)
        {
            await _connection.InsertAsync(clientes);
        }

        //Metodo para actualizar 
        public async Task Update(Cliente clientes)
        {
            await _connection.UpdateAsync(clientes);
        }

        //Metodo para eliminar
        public async Task Delete(Cliente clientes)
        {
            await _connection.DeleteAsync(clientes);
        }
    }
}
