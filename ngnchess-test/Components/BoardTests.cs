using ngnchess.Components;

namespace ngnchess_test.Components;
public class BoardTests {
    [Fact]
    public void IsValidPosition_ReturnsTrue_ForValidPositions() {
        var board = new Board();

        Assert.True(board.IsValidPosition(0, 0));
        Assert.True(board.IsValidPosition(7, 7));
        Assert.True(board.IsValidPosition(3, 4));
    }

    [Fact]
    public void IsValidPosition_ReturnsFalse_ForInvalidPositions() {
        var board = new Board();

        Assert.False(board.IsValidPosition(-1, 0));
        Assert.False(board.IsValidPosition(0, -1));
        Assert.False(board.IsValidPosition(8, 0));
        Assert.False(board.IsValidPosition(0, 8));
    }

    [Fact]
    public void GetPiece_ReturnsNull_ForEmptySquare() {
        var board = new Board();

        Assert.Null(board.GetPiece(0, 0));
    }

    [Fact]
    public void GetPiece_ReturnsNull_ForInvalidPosition() {
        var board = new Board();

        Assert.Null(board.GetPiece(-1, -1));
    }

    [Fact]
    public void SetPiece_ReturnsFalse_ForInvalidPosition() {
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, PieceColor.White);

        Assert.False(board.SetPiece(-1, -1, piece));
    }

    [Fact]
    public void SetPiece_PlacesPiece_AndReturnsTrue() {
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, PieceColor.White);

        Assert.True(board.SetPiece(0, 0, piece));
        Assert.Equal(piece.Type, board.GetPiece(0, 0)?.Type);
    }

    [Fact]
    public void RemovePiece_ReturnsFalse_ForInvalidPosition() {
        var board = new Board();

        Assert.False(board.RemovePiece(-1, -1));
    }

    [Fact]
    public void RemovePiece_RemovesPiece_AndReturnsTrue() {
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, PieceColor.White);

        board.SetPiece(0, 0, piece);
        Assert.True(board.RemovePiece(0, 0));
        Assert.Null(board.GetPiece(0, 0));
    }

    [Fact]
    public void Clone_CreatesDeepCopy() {
        var board = new Board();
        board.SetPiece(0, 0, new Piece(PieceType.Pawn, PieceColor.White));

        var clone = board.Clone();

        Assert.NotSame(board, clone);
        Assert.Equal(board.GetPiece(0, 0)?.Type, clone.GetPiece(0, 0)?.Type);
        Assert.Equal(board.GetPiece(0, 0)?.Type, clone.GetPiece(0, 0)?.Type);
    }

    [Fact]
    public void Clear_EmptiesBoard() {
        var board = new Board();
        board.SetupStandardPosition();

        board.Clear();

        for (int row = 0; row < 8; row++) {
            for (int col = 0; col < 8; col++) {
                Assert.Null(board.GetPiece(row, col));
            }
        }
    }

    [Fact]
    public void SetupStandardPosition_SetsCorrectPieces() {
        var board = new Board();
        board.SetupStandardPosition();

        // Check corners
        Assert.Equal(PieceType.Rook, board.GetPiece(0, 0)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 0)?.Color);

        Assert.Equal(PieceType.Rook, board.GetPiece(0, 7)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 7)?.Color);

        Assert.Equal(PieceType.Rook, board.GetPiece(7, 0)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(7, 0)?.Color);

        Assert.Equal(PieceType.Rook, board.GetPiece(7, 7)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(7, 7)?.Color);

        // Check kings
        Assert.Equal(PieceType.King, board.GetPiece(0, 4)?.Type);
        Assert.Equal(PieceColor.Black, board.GetPiece(0, 4)?.Color);

        Assert.Equal(PieceType.King, board.GetPiece(7, 4)?.Type);
        Assert.Equal(PieceColor.White, board.GetPiece(7, 4)?.Color);

        // Check pawns
        for (int col = 0; col < 8; col++) {
            Assert.Equal(PieceType.Pawn, board.GetPiece(1, col)?.Type);
            Assert.Equal(PieceColor.Black, board.GetPiece(1, col)?.Color);

            Assert.Equal(PieceType.Pawn, board.GetPiece(6, col)?.Type);
            Assert.Equal(PieceColor.White, board.GetPiece(6, col)?.Color);
        }
    }

    [Fact]
    public void SetupChess960Position_GeneratesValidPosition() {
        var board = new Board();
        int id = board.SetupChess960Position(42); // Using fixed ID for deterministic test

        Assert.Equal(42, id);

        // Check if bishops are on opposite colored squares
        int whiteLightBishopCol = -1;
        int whiteDarkBishopCol = -1;

        for (int col = 0; col < 8; col++) {
            if (board.GetPiece(7, col)?.Type == PieceType.Bishop) {
                if ((col % 2) == 0) {
                    whiteLightBishopCol = col;
                } else {
                    whiteDarkBishopCol = col;
                }
            }
        }

        Assert.True(whiteLightBishopCol >= 0);
        Assert.True(whiteDarkBishopCol >= 0);
        Assert.NotEqual(whiteLightBishopCol % 2, whiteDarkBishopCol % 2);

        // Check if king is between rooks
        int kingCol = -1;
        int leftRookCol = -1;
        int rightRookCol = -1;

        for (int col = 0; col < 8; col++) {
            if (board.GetPiece(7, col)?.Type == PieceType.King) {
                kingCol = col;
            } else if (board.GetPiece(7, col)?.Type == PieceType.Rook) {
                if (leftRookCol == -1) {
                    leftRookCol = col;
                } else {
                    rightRookCol = col;
                }
            }
        }

        Assert.True(kingCol > leftRookCol);
        Assert.True(kingCol < rightRookCol);

        // Check that pawns are in standard positions
        for (int col = 0; col < 8; col++) {
            Assert.Equal(PieceType.Pawn, board.GetPiece(1, col)?.Type);
            Assert.Equal(PieceColor.Black, board.GetPiece(1, col)?.Color);

            Assert.Equal(PieceType.Pawn, board.GetPiece(6, col)?.Type);
            Assert.Equal(PieceColor.White, board.GetPiece(6, col)?.Color);
        }
    }

    [Fact]
    public void MakeMove_StandardMove_MovesThePiece() {
        var board = new Board();
        board.SetupStandardPosition();

        var piece = board.GetPiece(6, 0); // White pawn at a2
        var move = new Move(piece!.Value, new Square('a', 2), new Square('a', 4));

        Assert.True(board.MakeMove(move));
        Assert.Null(board.GetPiece(6, 0)); // Original square is empty
        Assert.Equal(PieceType.Pawn, board.GetPiece(4, 0)?.Type); // Pawn moved to a4
        Assert.Equal(PieceColor.White, board.GetPiece(4, 0)?.Color);
    }

    [Fact]
    public void MakeMove_Castling_MovesKingAndRook() {
        var board = new Board();

        // Set up a position suitable for castling
        board.SetPiece(7, 4, new Piece(PieceType.King, PieceColor.White));
        board.SetPiece(7, 7, new Piece(PieceType.Rook, PieceColor.White));

        var piece = board.GetPiece(7, 4); // White king
        var move = new Move(piece!.Value, new Square('e', 1), new Square('g', 1)) { Type = MoveType.Castling };

        Assert.True(board.MakeMove(move));
        Assert.Null(board.GetPiece(7, 4)); // Original king square is empty
        Assert.Null(board.GetPiece(7, 7)); // Original rook square is empty
        Assert.Equal(PieceType.King, board.GetPiece(7, 6)?.Type); // King moved to g1
        Assert.Equal(PieceType.Rook, board.GetPiece(7, 5)?.Type); // Rook moved to f1
    }

    [Fact]
    public void MakeMove_EnPassant_CapturesPawn() {
        var board = new Board();

        // Set up a position suitable for en passant
        board.SetPiece(3, 1, new Piece(PieceType.Pawn, PieceColor.Black)); // Black pawn at b5
        board.SetPiece(3, 0, new Piece(PieceType.Pawn, PieceColor.White)); // White pawn at a5

        var piece = board.GetPiece(3, 0); // White pawn
        var move = new Move(piece!.Value, new Square('a', 5), new Square('b', 6)) {
            Type = MoveType.EnPassant,
            EnPassantTargetSquare = new Square('b', 5)
        };

        Assert.True(board.MakeMove(move));
        Assert.Null(board.GetPiece(3, 0)); // Original pawn square is empty
        Assert.Null(board.GetPiece(3, 1)); // Captured pawn square is empty
        Assert.Equal(PieceType.Pawn, board.GetPiece(2, 1)?.Type); // White pawn moved to b6
        Assert.Equal(PieceColor.White, board.GetPiece(2, 1)?.Color);
    }

    [Fact]
    public void MakeMove_Promotion_PromotesPawn() {
        var board = new Board();

        // Set up a position suitable for promotion
        board.SetPiece(1, 0, new Piece(PieceType.Pawn, PieceColor.White)); // White pawn at a7

        var piece = board.GetPiece(1, 0); // White pawn
        var move = new Move(piece!.Value, new Square('a', 7), new Square('a', 8)) {
            Promotion = new Piece(PieceType.Queen, PieceColor.White)
        };

        Assert.True(board.MakeMove(move));
        Assert.Null(board.GetPiece(1, 0)); // Original pawn square is empty
        Assert.Equal(PieceType.Queen, board.GetPiece(0, 0)?.Type); // Promoted to queen at a8
        Assert.Equal(PieceColor.White, board.GetPiece(0, 0)?.Color);
    }
}
