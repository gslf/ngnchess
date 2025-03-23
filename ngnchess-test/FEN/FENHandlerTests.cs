using ngnchess.FEN;

namespace ngnchess_test.FEN;

public class FENHandlerTests
{
    private string validFen1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private string validFen2 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w kq - 0 1";
    private string invalidFen1 = "invalid_fen_string";
    private string invalidFen2 = "rnbqkbn/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private string invalidFen3 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/rnbqkbnrr w KQkq - 0 1";
    private string invalidFen4 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq d6 0 1";

    [Fact]
    public void validFen_ShouldBeAccepted() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal(validFen1, fen.GetFenString());

        FENHandler fen2 = new FENHandler(validFen2);
        Assert.Equal(validFen2, fen2.GetFenString());
    }

    [Fact]
    public void InvalidFen_ShouldThrowException() {
        Assert.Throws<ArgumentException>(() => new ngnchess.FEN.FENHandler(invalidFen1));
        Assert.Throws<ArgumentException>(() => new ngnchess.FEN.FENHandler(invalidFen2));
        Assert.Throws<ArgumentException>(() => new ngnchess.FEN.FENHandler(invalidFen3));
        Assert.Throws<ArgumentException>(() => new ngnchess.FEN.FENHandler(invalidFen4));

    }

    [Fact]
    public void GetBoardPosition_ShouldReturnCorrectPart() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", fen.GetBoardPosition());
    }

    [Fact]
    public void GetActiveColor_ShouldReturnCorrectColor() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal('w', fen.GetActiveColor());
    }

    [Fact]
    public void GetCastlingAvailability_ShouldReturnCorrectCastling() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal("KQkq", fen.GetCastlingAvailability());
    }

    [Fact]
    public void GetEnPassantTarget_ShouldReturnCorrectValue() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal("-", fen.GetEnPassantTarget());
    }

    [Fact]
    public void GetHalfMoveClock_ShouldReturnCorrectValue() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal(0, fen.GetHalfMoveClock());
    }

    [Fact]
    public void GetFullMoveNumber_ShouldReturnCorrectValue() {
        FENHandler fen = new FENHandler(validFen1);
        Assert.Equal(1, fen.GetFullMoveNumber());
    }
}
