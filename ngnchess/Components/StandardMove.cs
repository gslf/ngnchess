using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;

namespace ngnchess.Components;

/// <summary>
/// Represents a standard chess move.
/// </summary>
public class StandardMove : Move {
    /// <summary>
    /// The type of the move is always Standard.
    /// </summary>
    public override MoveType Type => MoveType.Standard;

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardMove"/> class.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="annotation">Optional annotation.</param>
    /// <param name="comments">Optional comments about the move.</param>
    public StandardMove(Piece piece, Square from, Square to, MoveAnnotation? annotation = null, string? comments = null)
        : base(piece, from, to, annotation, comments) {
    }

    /// <summary>
    /// Returns a string specific to the standard move type.
    /// </summary>
    /// <returns>An empty string as there is no special notation for standard moves.</returns>
    protected override string GetMoveSpecificString() => "";
}