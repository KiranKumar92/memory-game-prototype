using MemoryGame.Pooling;

namespace MemoryGame.UI.FlipCard
{
    public interface IFlipCard
    {
        FlipCard FlipCard { get; set; }
        FlipCardPooling FlipCardPooling { get; set; }
        FlipCard GetCurrentCard();
    }
}
