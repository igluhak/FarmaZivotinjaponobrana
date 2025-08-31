namespace FarmaZivotinja.Core
{
    public readonly struct Lokacija
    {
        public int X { get; }
        public int Y { get; }

        public Lokacija(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object? obj) =>
            obj is Lokacija l && l.X == X && l.Y == Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Lokacija a, Lokacija b) => a.Equals(b);
        public static bool operator !=(Lokacija a, Lokacija b) => !a.Equals(b);
    }
}
