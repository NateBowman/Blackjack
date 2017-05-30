namespace Blackjack
{
    public struct Vector2
    {
        public Vector2(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        public int x { get; set; }

        public int y { get; set; }
    }
}