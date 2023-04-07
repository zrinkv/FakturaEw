namespace FakturaMEF
{
    public interface IPorez<in T>
    {
        decimal IzracunajPorez(T input);
    }
}
