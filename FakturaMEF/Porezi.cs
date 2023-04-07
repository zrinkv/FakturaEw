using System.ComponentModel.Composition;

namespace FakturaMEF
{
    [Export(typeof(IPorez<decimal>))]
    [ExportMetadata("Naziv", "BiH")]
    public class PorezBiH : IPorez<decimal>
    {
        public decimal IzracunajPorez(decimal Iznos)
        {
            return Iznos * 1.17m;
        }
    }

    [Export(typeof(IPorez<decimal>))]
    [ExportMetadata("Naziv", "RH")]
    public class PorezRH : IPorez<decimal>
    {
        public decimal IzracunajPorez(decimal Iznos)
        {
            return Iznos * 1.25m;
        }
    }

    //[Export(typeof(IPorez<decimal>))]
    //[ExportMetadata("Naziv", "MK")]
    //public class PorezMK : IPorez<decimal>
    //{
    //    public decimal IzracunajPorez(decimal Iznos)
    //    {
    //        return Iznos * 1.20m;
    //    }
    //}
}
