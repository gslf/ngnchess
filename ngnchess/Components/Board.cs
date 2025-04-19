using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;

namespace ngnchess.Components;

/// <summary>
/// Represents the state of a chess board.
/// </summary>
public class Board {
    /// <summary>
    /// The 8x8 array representing the chess board.
    /// </summary>
    public Piece?[,] Squares { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class.
    /// </summary>
    public Board() {
        Squares = new Piece?[8, 8];
    }

    /// <summary>
    /// Checks if the specified position is inside the board.
    /// </summary>
    /// <param name="row">The row of the position.</param>
    /// <param name="col">The column of the position.</param>
    /// <returns>True if the position is valid, false otherwise.</returns>
    public bool IsValidPosition(int row, int col) {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }

    /// <summary>
    /// Gets the piece at the specified position.
    /// </summary>
    /// <param name="row">The row of the position (0-7).</param>
    /// <param name="col">The column of the position (0-7).</param>
    /// <returns>The piece at the specified position or null if position is invalid.</returns>
    public Piece? GetPiece(int row, int col) {
        if (!IsValidPosition(row, col)) {
            return null;
        }
        return Squares[row, col];
    }

    /// <summary>
    /// Sets the piece at the specified position.
    /// </summary>
    /// <param name="row">The row of the position (0-7).</param>
    /// <param name="col">The column of the position (0-7).</param>
    /// <param name="piece">The piece to place at the specified position.</param>
    /// <returns>True if the piece was set, false if the position is invalid.</returns>
    public bool SetPiece(int row, int col, Piece piece) {
        if (!IsValidPosition(row, col)) {
            return false;
        }
        Squares[row, col] = piece;
        return true;
    }

    /// <summary>
    /// Removes the piece at the specified position.
    /// </summary>
    /// <param name="row">The row of the position (0-7).</param>
    /// <param name="col">The column of the position (0-7).</param>
    /// <returns>True if the piece was removed, false if the position is invalid.</returns>
    public bool RemovePiece(int row, int col) {
        if (!IsValidPosition(row, col)) {
            return false;
        }
        Squares[row, col] = null;
        return true;
    }

    /// <summary>
    /// Creates a deep copy of the current board.
    /// </summary>
    /// <returns>A new board with the same piece configuration.</returns>
    public Board Clone() {
        var clone = new Board();
        for (int row = 0; row < 8; row++) {
            for (int col = 0; col < 8; col++) {
                clone.Squares[row, col] = Squares[row, col];
            }
        }
        return clone;
    }

    /// <summary>
    /// Resets the board to an empty state.
    /// </summary>
    public void Clear() {
        Squares = new Piece?[8, 8];
    }

    /// <summary>
    /// Initializes the board with the standard starting position for chess.
    /// </summary>
    public void SetupStandardPosition() {
        Clear();

        // Set up white pieces
        SetPiece(7, 0, new Piece(PieceType.Rook, PieceColor.White));
        SetPiece(7, 1, new Piece(PieceType.Knight, PieceColor.White));
        SetPiece(7, 2, new Piece(PieceType.Bishop, PieceColor.White));
        SetPiece(7, 3, new Piece(PieceType.Queen, PieceColor.White));
        SetPiece(7, 4, new Piece(PieceType.King, PieceColor.White));
        SetPiece(7, 5, new Piece(PieceType.Bishop, PieceColor.White));
        SetPiece(7, 6, new Piece(PieceType.Knight, PieceColor.White));
        SetPiece(7, 7, new Piece(PieceType.Rook, PieceColor.White));

        // Set up white pawns
        for (int col = 0; col < 8; col++) {
            SetPiece(6, col, new Piece(PieceType.Pawn, PieceColor.White));
        }

        // Set up black pieces
        SetPiece(0, 0, new Piece(PieceType.Rook, PieceColor.Black));
        SetPiece(0, 1, new Piece(PieceType.Knight, PieceColor.Black));
        SetPiece(0, 2, new Piece(PieceType.Bishop, PieceColor.Black));
        SetPiece(0, 3, new Piece(PieceType.Queen, PieceColor.Black));
        SetPiece(0, 4, new Piece(PieceType.King, PieceColor.Black));
        SetPiece(0, 5, new Piece(PieceType.Bishop, PieceColor.Black));
        SetPiece(0, 6, new Piece(PieceType.Knight, PieceColor.Black));
        SetPiece(0, 7, new Piece(PieceType.Rook, PieceColor.Black));

        // Set up black pawns
        for (int col = 0; col < 8; col++) {
            SetPiece(1, col, new Piece(PieceType.Pawn, PieceColor.Black));
        }
    }

    /// <summary>
    /// Initializes the board with a random Chess960 position.
    /// Chess960 (or Fischer Random Chess) follows these rules:
    /// 1. Bishops must be on opposite-colored squares
    /// 2. King must be between the two rooks
    /// 3. Back rank pieces are randomized according to these constraints
    /// 4. Pawns are in their standard positions
    /// </summary>
    /// <param name="positionId">Optional specific Chess960 position ID (0-959). If null, a random position is used.</param>
    /// <returns>The position ID of the generated setup (0-959)</returns>
    public int SetupChess960Position(int? positionId = null) {
        Clear();

        // Generate a random position ID if not provided
        Random random = new Random();
        int id = positionId ?? random.Next(960);

        // Set up pawns (standard positions)
        for (int col = 0; col < 8; col++) {
            SetPiece(6, col, new Piece(PieceType.Pawn, PieceColor.White));
            SetPiece(1, col, new Piece(PieceType.Pawn, PieceColor.Black));
        }

        // Generate back rank according to Chess960 rules
        List<int> positions = GenerateChess960BackRank(id);

        // Set up white back rank
        for (int col = 0; col < 8; col++) {
            PieceType pieceType = GetPieceTypeForChess960(positions[col]);
            SetPiece(7, col, new Piece(pieceType, PieceColor.White));
            SetPiece(0, col, new Piece(pieceType, PieceColor.Black));
        }

        return id;
    }

    /// <summary>
    /// Makes a move on the board.
    /// </summary>
    /// <param name="move">The move to make.</param>
    /// <returns>True if the move was successfully made, false otherwise.</returns>
    public bool MakeMove(Move move) {
        var (sourceRow, sourceCol) = move.From.ToArrayIndices();
        var (destinationRow, destinationCol) = move.To.ToArrayIndices();

        // Validate positions
        if (!IsValidPosition(sourceRow, sourceCol) || !IsValidPosition(destinationRow, destinationCol)) {
            return false;
        }

        // Remove the piece from the source position
        RemovePiece(sourceRow, sourceCol);

        // Handle special move types using MoveType enum
        if (move.Type == MoveType.EnPassant) {
            // For EnPassantMove, we need to get the EnPassantTargetSquare
            // Since we're using the Type property, we need to cast to access specific properties
            if (move is Components.EnPassantMove enPassantMove) {
                var (enPassantRow, enPassantCol) = enPassantMove.EnPassantTargetSquare.ToArrayIndices();
                if (IsValidPosition(enPassantRow, enPassantCol)) {
                    RemovePiece(enPassantRow, enPassantCol);
                }
            }
        } else if (move.Type == MoveType.Castling) {
            // Handle castling
            if (move.To.File == 'g') {
                // Kingside castling
                if (IsValidPosition(destinationRow, 7)) {
                    RemovePiece(destinationRow, 7);
                    SetPiece(destinationRow, 5, new Piece(PieceType.Rook, move.Piece.Color));
                }
            } else if (move.To.File == 'c') {
                // Queenside castling
                if (IsValidPosition(destinationRow, 0)) {
                    RemovePiece(destinationRow, 0);
                    SetPiece(destinationRow, 3, new Piece(PieceType.Rook, move.Piece.Color));
                }
            }
        }

        // Place the piece at the destination position
        // For promotion, use the promotion piece if available
        if (move is Components.PromotionMove promotionMove) {
            SetPiece(destinationRow, destinationCol, promotionMove.Promotion);
        } else {
            // For standard moves and non-promotion special moves, place the original piece
            SetPiece(destinationRow, destinationCol, move.Piece);
        }

        return true;
    }

    /// <summary>
    /// Generates a list of piece positions for a Chess960 back rank based on position ID.
    /// </summary>
    /// <param name="positionId">The Chess960 position ID (0-959)</param>
    /// <returns>List of integers representing piece types by position</returns>
    private List<int> GenerateChess960BackRank(int positionId) {
        // Ensure position ID is valid
        positionId = Math.Clamp(positionId, 0, 959);

        int[] pieces = new int[8];
        int[] positions = new int[8];
        for (int i = 0; i < 8; i++) {
            positions[i] = -1;
        }

        // Convert position ID to piece positions according to Chess960 rules

        // Place bishops on opposite colored squares
        int bishopIdx = positionId / 16;
        positions[2 * (bishopIdx % 4)] = 2; // Light-squared bishop (0=Rook, 1=Knight, 2=Bishop, etc.)
        bishopIdx /= 4;
        positions[2 * (bishopIdx % 4) + 1] = 2; // Dark-squared bishop

        // Place queen
        int queenIdx = positionId / 96 % 6;
        int queenPos = 0;
        for (int i = 0; i < 8; i++) {
            if (positions[i] == -1) {
                if (queenIdx == 0) {
                    positions[i] = 3; // Queen
                    queenPos = i;
                    break;
                }
                queenIdx--;
            }
        }

        // Place knights
        int knightIdx = positionId / 960 * 10 + positionId % 96 / 6;
        int n1 = knightIdx / 5;
        int n2 = knightIdx % 5;
        int knightCount = 0;

        for (int i = 0; i < 8; i++) {
            if (positions[i] == -1) {
                if (knightCount == n1 || knightCount == n2 && n1 >= n2) {
                    positions[i] = 1; // Knight
                }
                knightCount++;
            }
        }

        // Place rooks and king
        // First find empty positions
        List<int> emptyPositions = new List<int>();
        for (int i = 0; i < 8; i++) {
            if (positions[i] == -1) {
                emptyPositions.Add(i);
            }
        }

        // First rook, king, second rook
        positions[emptyPositions[0]] = 0; // Rook
        positions[emptyPositions[1]] = 4; // King
        positions[emptyPositions[2]] = 0; // Rook

        return positions.ToList();
    }

    /// <summary>
    /// Converts a Chess960 piece index to a PieceType.
    /// </summary>
    /// <param name="index">Index value from Chess960 position generation</param>
    /// <returns>The corresponding PieceType</returns>
    private PieceType GetPieceTypeForChess960(int index) {
        return index switch {
            0 => PieceType.Rook,
            1 => PieceType.Knight,
            2 => PieceType.Bishop,
            3 => PieceType.Queen,
            4 => PieceType.King,
            _ => PieceType.Pawn  // Should never happen
        };
    }
}
