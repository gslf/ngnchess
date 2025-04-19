using ngnchess.Components;
using ngnchess.Models.Enum;

namespace ngnchess_test.Components;

public class MoveTests {
    private readonly Piece _pawnWhite;
    private readonly Piece _kingWhite;
    private readonly Piece _queenWhite;
    private readonly Square _fromSquare;
    private readonly Square _toSquare;
    private readonly Square _castlingToSquare;
    private readonly Square _enPassantFromSquare;
    private readonly Square _enPassantToSquare;
    private readonly Square _enPassantTargetSquare;
    private readonly Square _promotionFromSquare;
    private readonly Square _promotionToSquare;

    public MoveTests() {
        _pawnWhite = new Piece(PieceType.Pawn, PieceColor.White);
        _kingWhite = new Piece(PieceType.King, PieceColor.White);
        _queenWhite = new Piece(PieceType.Queen, PieceColor.White);

        _fromSquare = new Square('a', 2);
        _toSquare = new Square('a', 4);
        _castlingToSquare = new Square('g', 1);
        _enPassantFromSquare = new Square('e', 5);
        _enPassantToSquare = new Square('f', 6);
        _enPassantTargetSquare = new Square('f', 5);
        _promotionFromSquare = new Square('a', 7);
        _promotionToSquare = new Square('a', 8);
    }

    [Fact]
    public void StandardMove_ToString_ReturnsCorrectString() {
        // Arrange
        var move = new StandardMove(_pawnWhite, _fromSquare, _toSquare);

        // Act
        var result = move.ToString();

        // Assert
        Assert.Equal("WP from a2 to a4", result);
    }

    [Fact]
    public void CastlingMove_ToString_ReturnsCorrectString() {
        // Arrange
        var move = new CastlingMove(_kingWhite, new Square('e', 1), _castlingToSquare);

        // Act
        var result = move.ToString();

        // Assert
        Assert.Equal("WK from e1 to g1 (castling)", result);
    }

    [Fact]
    public void EnPassantMove_ToString_ReturnsCorrectString() {
        // Arrange
        var move = new EnPassantMove(_pawnWhite, _enPassantFromSquare, _enPassantToSquare, _enPassantTargetSquare);

        // Act
        var result = move.ToString();

        // Assert
        Assert.Equal("WP from e5 to f6 (en passant on f5)", result);
    }

    [Fact]
    public void PromotionMove_ToString_ReturnsCorrectString() {
        // Arrange
        var move = new PromotionMove(_pawnWhite, _promotionFromSquare, _promotionToSquare, _queenWhite);

        // Act
        var result = move.ToString();

        // Assert
        Assert.Equal("WP from a7 to a8 (promotion to WQ)", result);
    }

    [Fact]
    public void Move_WithAnnotation_IncludesAnnotationInToString() {
        // Arrange
        var move = new StandardMove(_pawnWhite, _fromSquare, _toSquare, MoveAnnotation.GOOD);

        // Act
        var result = move.ToString();

        // Assert
        Assert.Equal("WP from a2 to a4 !", result);
    }

    [Fact]
    public void Move_Type_ReturnsCorrectMoveType() {
        // Arrange
        var standardMove = new StandardMove(_pawnWhite, _fromSquare, _toSquare);
        var castlingMove = new CastlingMove(_kingWhite, new Square('e', 1), _castlingToSquare);
        var enPassantMove = new EnPassantMove(_pawnWhite, _enPassantFromSquare, _enPassantToSquare, _enPassantTargetSquare);
        var promotionMove = new PromotionMove(_pawnWhite, _promotionFromSquare, _promotionToSquare, _queenWhite);

        // Assert
        Assert.Equal(MoveType.Standard, standardMove.Type);
        Assert.Equal(MoveType.Castling, castlingMove.Type);
        Assert.Equal(MoveType.EnPassant, enPassantMove.Type);
        Assert.Equal(MoveType.Standard, promotionMove.Type);
    }
}
