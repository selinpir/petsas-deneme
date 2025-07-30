using Microsoft.EntityFrameworkCore;
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IFaturaService
    {
        //admine fatura getirme
        Task<Fatura> GetFaturaByFaturaNo(int FaturaId);


    }
}
