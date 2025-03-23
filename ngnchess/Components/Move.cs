namespace ngnchess.Components;

/// <summary>
/// Represents a chess move, including the piece being moved, the starting and ending squares,
/// the type of move, and any special conditions such as promotion or en passant.
/// </summary>
/// <param name="Piece">The chess piece being moved.</param>
/// <param name="From">The starting square of the move.</param>
/// <param name="To">The ending square of the move.</param>
/// <param name="Promotion">The piece to which a pawn is promoted, if applicable.</param>
/// <param name="Type">The type of the move (standard, castling, en passant).</param>
/// <param name="EnPassantTargetSquare">The target square for en passant capture, if applicable.</param>
public record Move(
    Piece Piece,
    Square From,
    Square To,
    Piece? Promotion = null,
    MoveType Type = MoveType.Standard,
    Square? EnPassantTargetSquare = null) {

    /// <summary>
    /// Initializes a new instance of the <see cref="Move"/> class for a standard move.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="promotion">The piece to which a pawn is promoted, if applicable.</param>
    public Move(Piece piece, Square from, Square to, Piece? promotion = null)
        : this(piece, from, to, promotion, MoveType.Standard, null) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Move"/> class for castling.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    public Move(Piece piece, Square from, Square to)
        : this(piece, from, to, null, MoveType.Castling, null) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Move"/> class for an en passant capture.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="enPassantTargetSquare">The target square for en passant capture.</param>
    public Move(Piece piece, Square from, Square to, Square enPassantTargetSquare)
        : this(piece, from, to, null, MoveType.EnPassant, enPassantTargetSquare) {
    }

    /// <summary>
    /// Returns a string that represents the current move.
    /// </summary>
    /// <returns>A string that represents the current move.</returns>
    public override string ToString() {
        string promotionString = Promotion != null ? $" (promotion to {Promotion})" : "";
        string specialMoveString = "";
        switch (Type) {
            case MoveType.Castling:
                specialMoveString = " (castling)";
                break;
            case MoveType.EnPassant:
                specialMoveString = $" (en passant on {EnPassantTargetSquare})";
                break;
            case MoveType.Standard:
            default:
                break;
        }
        return $"{Piece} from {From} to {To}{promotionString}{specialMoveString}";
    }
}
