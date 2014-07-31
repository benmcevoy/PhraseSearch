namespace PhraseSearch
{
    public interface IHitItem<T>
    {
        string Term { get; }
        int TermPosition { get; }
        T Document { get; }
    }
}