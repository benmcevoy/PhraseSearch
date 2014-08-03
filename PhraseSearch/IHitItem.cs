namespace PhraseSearch
{
    public interface IHitItem<out T>
    {
        string Term { get; }
        int TermPosition { get; }
        T Document { get; }
        int Depth { get; }
    }
}