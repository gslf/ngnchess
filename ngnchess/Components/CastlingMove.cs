using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;

namespace ngnchess.Components;
/// <summary>
/// Represents a castling move.
/// </summary>
public class CastlingMove : Move {
    /// <summary>
    /// The type of the move is always Castling.
    /// </summary>
    public override MoveType Type => MoveType.Castling;

    /// <summary>
    /// Initializes a new instance of the <see cref="CastlingMove"/> class.
    /// </summary>
    /// <param name="piece">The chess piece being moved (king).</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="annotation">Optional annotation.</param>
    /// <param name="comments">Optional comments about the move.</param>
    public CastlingMove(Piece piece, Square from, Square to, MoveAnnotation? annotation = null, string? comments = null)
        : base(piece, from, to, annotation, comments) {
    }

    /// <summary>
    /// Returns a string specific to the castling move.
    /// </summary>
    /// <returns>A string indicating castling.</returns>
    protected override string GetMoveSpecificString() => " (castling)";
}