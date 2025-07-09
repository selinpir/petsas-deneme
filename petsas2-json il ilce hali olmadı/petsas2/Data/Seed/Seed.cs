namespace petsas2.Data.Seed
{
    public class ProvinceSeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DistrictSeed> Districts { get; set; }
    }

    public class DistrictSeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
//veritabanı entity’leri değil sadece JSON’dan veri okurken geçici olarak kullanılıyorlar