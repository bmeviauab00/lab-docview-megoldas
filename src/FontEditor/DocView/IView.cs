namespace FontEditor.DocView
{
    /// <summary>
    /// Az egyes nézetek közös interfésze. Nem lehet osztály, mert az egyes nézetek a UserControl-ból származnak
    /// és akkor View-val együtt már két ősük lenne.
    /// </summary>
    public interface IView
    {
        void Update();
    }
}
