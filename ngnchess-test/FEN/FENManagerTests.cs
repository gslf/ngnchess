using ngnchess.FEN;

namespace ngnchess_test.FEN;

public class FENManagerTests
{
    private string validFen1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private string validFen2 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w kq - 0 1";
    private string invalidFen1 = "invalid_fen_string";
    private string invalidFen2 = "rnbqkbn/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private string invalidFen3 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/rnbqkbnrr w KQkq - 0 1";
    private string invalidFen4 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq d6 0 1";

    [Fact]
    public void validFen_ShouldBeAccepted() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal(validFen1, FENManager.GetFen());

        FENManager = new FENManager(validFen2);
        Assert.Equal(validFen2, FENManager.GetFen());
    }

    [Fact]
    public void InvalidFen_ShouldThrowException() {
        Assert.Throws<ArgumentException>(() => new FENManager(invalidFen1));
        Assert.Throws<ArgumentException>(() => new FENManager(invalidFen2));
        Assert.Throws<ArgumentException>(() => new FENManager(invalidFen3));
        Assert.Throws<ArgumentException>(() => new FENManager(invalidFen4));

    }

    [Fact]
    public void GetBoardPosition_ShouldReturnCorrectPart() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", FENManager.GetBoardPosition());
    }

    [Fact]
    public void GetActiveColor_ShouldReturnCorrectColor() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal('w', FENManager.GetActiveColor());
    }

    [Fact]
    public void GetCastlingAvailability_ShouldReturnCorrectCastling() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal("KQkq", FENManager.GetCastlingAvailability());
    }

    [Fact]
    public void GetEnPassantTarget_ShouldReturnCorrectValue() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal("-", FENManager.GetEnPassantTarget());
    }

    [Fact]
    public void GetHalfMoveClock_ShouldReturnCorrectValue() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal(0, FENManager.GetHalfMoveClock());
    }

    [Fact]
    public void GetFullMoveNumber_ShouldReturnCorrectValue() {
        FENManager FENManager = new FENManager(validFen1);
        Assert.Equal(1, FENManager.GetFullMoveNumber());
    }
}
