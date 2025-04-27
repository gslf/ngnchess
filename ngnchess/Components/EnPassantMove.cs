using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;

namespace ngnchess.Components;
/// <summary>
/// Represents an en passant capture move.
/// </summary>
public class EnPassantMove : Move {
    /// <summary>
    /// The target square for en passant capture.
    /// </summary>
    public Square EnPassantTargetSquare { get; }

    /// <summary>
    /// The type of the move is always EnPassant.
    /// </summary>
    public override MoveType Type => MoveType.EnPassant;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnPassantMove"/> class.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="enPassantTargetSquare">The target square for en passant capture.</param>
    /// <param name="annotation">Optional annotation.</param>
    /// <param name="comments">Optional comments about the move.</param>
    public EnPassantMove(Piece piece, Square from, Square to, Square enPassantTargetSquare, MoveAnnotation? annotation = null, string? comments = null)
        : base(piece, from, to, annotation, comments) {
        EnPassantTargetSquare = enPassantTargetSquare;
    }

    /// <summary>
    /// Returns a string specific to the en passant move.
    /// </summary>
    /// <returns>A string indicating en passant capture and the target square.</returns>
    protected override string GetMoveSpecificString() => $" (en passant on {EnPassantTargetSquare})";
}