using ngnchess.Components;
using ngnchess.FEN;
using ngnchess.Models.Enum;
using System.Reflection;


namespace ngnchess_test.FEN;
public class FENBoardAdapterTests {
    [Fact]
    public void BoardToFEN_EmptyBoard_ReturnsCorrectFEN() {
        // Arrange
        var board = new Board();

        // Act
        string fen = FENBoardAdapter.BoardToFEN(board);

        // Assert
        Assert.Equal("8/8/8/8/8/8/8/8 w - - 0 1", fen);
    }

    [Fact]
    public void BoardToFEN_StartingPosition_ReturnsCorrectFEN() {
        // Arrange
        var board = new Board();
        board.SetupStandardPosition();

        // Act
        string fen = FENBoardAdapter.BoardToFEN(board);

        // Assert
        Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1", fen);
    }

    [Fact]
    public void BoardToFEN_CustomPosition_ReturnsCorrectFEN() {
        // Arrange
        var board = new Board();

        // Set up a custom position
        board.SetPiece(0, 4, new Piece(PieceType.King, PieceColor.Black));
        board.SetPiece(7, 4, new Piece(PieceType.King, PieceColor.White));
        board.SetPiece(3, 3, new Piece(PieceType.Queen, PieceColor.White));

        // Act
        string fen = FENBoardAdapter.BoardToFEN(board);

        // Assert
        Assert.Equal("4k3/8/8/3Q4/8/8/8/4K3 w - - 0 1", fen);
    }

    [Fact]
    public void BoardToFEN_WithCustomParameters_ReturnsCorrectFEN() {
        // Arrange
        var board = new Board();
        board.SetupStandardPosition();

        // Act
        string fen = FENBoardAdapter.BoardToFEN(
            board,
            activeColor: 'b',
            castlingAvailability: "KQ",
            enPassantTarget: "e3",
            halfMoveClock: 10,
            fullMoveNumber: 5);

        // Assert
        Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQ e3 10 5", fen);
    }

    [Fact]
    public void FENToBoard_StartingPosition_CreatesCorrectBoard() {
        // Arrange
        string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        // Act
        Board board = FENBoardAdapter.FENToBoard(fen);

        // Assert
        // Check top left corner - black rook
        Assert.Equal(PieceType.Rook, board.GetPiece(0, 0)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 0)?.Color);

        // Check kings
        Assert.Equal(PieceType.King, board.GetPiece(0, 4)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 4)?.Color);
        Assert.Equal(PieceType.King, board.GetPiece(7, 4)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(7, 4)?.Color);

        // Check all pawns
        for (int col = 0; col < 8; col++) {
            Assert.Equal(PieceType.Pawn, board.GetPiece(1, col)?.Type);
            Assert.Equal(PieceColor.Black, board.GetPiece(1, col)?.Color);
            Assert.Equal(PieceType.Pawn, board.GetPiece(6, col)?.Type);
            Assert.Equal(PieceColor.White, board.GetPiece(6, col)?.Color);
        }

        // Check middle is empty
        for (int row = 2; row < 6; row++) {
            for (int col = 0; col < 8; col++) {
                Assert.Null(board.GetPiece(row, col));
            }
        }
    }

    [Fact]
    public void FENToBoard_CustomPosition_CreatesCorrectBoard() {
        // Arrange
        string fen = "4k3/8/8/8/4Q3/8/8/4K3 w - - 0 1";

        // Act
        Board board = FENBoardAdapter.FENToBoard(fen);

        // Assert
        // Check that only the three pieces exist
        Assert.Equal(PieceType.King, board.GetPiece(0, 4)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 4)?.Color);

        Assert.Equal(PieceType.Queen, board.GetPiece(4, 4)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(4, 4)?.Color);

        Assert.Equal(PieceType.King, board.GetPiece(7, 4)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(7, 4)?.Color);

        // Count total pieces
        int pieceCount = 0;
        for (int row = 0; row < 8; row++) {
            for (int col = 0; col < 8; col++) {
                if (board.GetPiece(row, col) != null) {
                    pieceCount++;
                }
            }
        }
        Assert.Equal(3, pieceCount);
    }

    [Fact]
    public void FENToBoard_InvalidFEN_ThrowsArgumentException() {
        // Arrange
        string invalidFen = "invalid/fen/string";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => FENBoardAdapter.FENToBoard(invalidFen));
    }

    [Fact]
    public void BoardToFEN_FENToBoard_Roundtrip() {
        // Arrange
        string originalFen = "r1bqkbnr/pppp1ppp/2n5/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3";

        // Act
        Board board = FENBoardAdapter.FENToBoard(originalFen);
        string reconstructedFen = FENBoardAdapter.BoardToFEN(
            board,
            activeColor: 'w',
            castlingAvailability: "KQkq",
            enPassantTarget: "-",
            halfMoveClock: 2,
            fullMoveNumber: 3);

        // Assert
        Assert.Equal(originalFen, reconstructedFen);
    }

    [Fact]
    public void PieceToFENChar_AllPieceTypes_ReturnsCorrectChars() {
        // This test uses reflection to test the private method
        var method = typeof(FENBoardAdapter).GetMethod("PieceToFENChar",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        // White pieces (uppercase)
        Assert.Equal('P', method.Invoke(null, new object[] { new Piece(PieceType.Pawn, PieceColor.White) }));
        Assert.Equal('R', method.Invoke(null, new object[] { new Piece(PieceType.Rook, PieceColor.White) }));
        Assert.Equal('N', method.Invoke(null, new object[] { new Piece(PieceType.Knight, PieceColor.White) }));
        Assert.Equal('B', method.Invoke(null, new object[] { new Piece(PieceType.Bishop, PieceColor.White) }));
        Assert.Equal('Q', method.Invoke(null, new object[] { new Piece(PieceType.Queen, PieceColor.White) }));
        Assert.Equal('K', method.Invoke(null, new object[] { new Piece(PieceType.King, PieceColor.White) }));

        // Black pieces (lowercase)
        Assert.Equal('p', method.Invoke(null, new object[] { new Piece(PieceType.Pawn, PieceColor.Black) }));
        Assert.Equal('r', method.Invoke(null, new object[] { new Piece(PieceType.Rook, PieceColor.Black) }));
        Assert.Equal('n', method.Invoke(null, new object[] { new Piece(PieceType.Knight, PieceColor.Black) }));
        Assert.Equal('b', method.Invoke(null, new object[] { new Piece(PieceType.Bishop, PieceColor.Black) }));
        Assert.Equal('q', method.Invoke(null, new object[] { new Piece(PieceType.Queen, PieceColor.Black) }));
        Assert.Equal('k', method.Invoke(null, new object[] { new Piece(PieceType.King, PieceColor.Black) }));
    }

    [Fact]
    public void FENCharToPiece_AllChars_ReturnsCorrectPieces() {
        // This test uses reflection to test the private method
        var method = typeof(FENBoardAdapter).GetMethod("FENCharToPiece",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        // White pieces (uppercase)
        var whitePawn = (Piece)method.Invoke(null, new object[] { 'P' });
        Assert.Equal(PieceType.Pawn, whitePawn.Type);
        Assert.Equal(PieceColor.White, whitePawn.Color);

        var whiteRook = (Piece)method.Invoke(null, new object[] { 'R' });
        Assert.Equal(PieceType.Rook, whiteRook.Type);
        Assert.Equal(PieceColor.White, whiteRook.Color);

        // Black pieces (lowercase)
        var blackBishop = (Piece)method.Invoke(null, new object[] { 'b' });
        Assert.Equal(PieceType.Bishop, blackBishop.Type);
        Assert.Equal(PieceColor.Black, blackBishop.Color);

        var blackQueen = (Piece)method.Invoke(null, new object[] { 'q' });
        Assert.Equal(PieceType.Queen, blackQueen.Type);
        Assert.Equal(PieceColor.Black, blackQueen.Color);
    }

    [Fact]
    public void FENCharToPiece_InvalidChar_ThrowsArgumentException() {
        // This test uses reflection to test the private method
        var method = typeof(FENBoardAdapter).GetMethod("FENCharToPiece",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        // Act & Assert
        Assert.Throws<TargetInvocationException>(() => method.Invoke(null, new object[] { 'x' }));
    }
}