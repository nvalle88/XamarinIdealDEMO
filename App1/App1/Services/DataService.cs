using System;

namespace App1.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using App1.Domain.ModelsResult;
    using Common.Models;
    using Interfaces;
    using SQLite;
    using Xamarin.Forms;

    public class DataService
    {
        private SQLiteAsyncConnection connection;

        public DataService()
        {
            this.OpenOrCreateDB().GetAwaiter();
        }

        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);

            try
            {
                await connection.CreateTableAsync<ClienteSqLite>().ConfigureAwait(false);
               // await connection.CreateTableAsync<FacturaSqLite>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }

        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }

        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }

        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }

        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }

        public async Task<List<ClienteSqLite>> ListarClientes()
        {
            var query = await this.connection.QueryAsync<Cliente>("select * from [ClienteSqLite]");
            var array = query.ToArray();
            var list = array.Select(c => new ClienteSqLite
            {
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                ClienteId = c.ClienteId,
                Direccion = c.Direccion,
            }).ToList();
            return list;
        }

        public async Task EliminarTodosClientes()
        {
            var query = await this.connection.QueryAsync<ClienteSqLite>("delete from [ClienteSqLite]");
        }
    }
}
