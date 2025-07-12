using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;

namespace petsas2.Services
{
    //ctrl . implement interface

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }
        //urunleri doner
        public async Task<List<Product>> GetAllAsync()
        { 
            return await _db.Products.ToListAsync(); 
        }
        //gonderilen urune ait id varsa getirir
        public async Task<Product?> GetByIdAsync(int id)
        { 
            return await _db.Products.FindAsync(id); 
        }
        //yeni urun ekleme
        public async Task AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync(); //her islemde kaydet/er
        }
        //guncelleme icin
        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }
       
        //urun varsa sil- yoksa silme hata da verme
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
//remove-delete
//update-update
//tolistasync-select
//findasync-select pk ile
//add-insert
//12.07