using APBD5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Services
{
    public interface IDbService
    {
        public Task<int> AddProductWarehouse(ProductWarehouse pW);
    }
    
    public interface IDbService2
    {
        public Task<int> AddProductWarehouseProc(ProductWarehouse pW);
    }
}
