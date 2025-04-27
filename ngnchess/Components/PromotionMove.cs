using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;

namespace ngnchess.Components;
/// <summary>
/// Represents a pawn promotion move.
/// </summary>
public class PromotionMove : Move {
    /// <summary>
    /// The piece to which the pawn is promoted.
    /// </summary>
    public Piece Promotion { get; }

    /// <summary>
    /// The type of the move is Standard as promotions are standard moves with an additional property.
    /// </summary>
    public override MoveType Type => MoveType.Standard;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionMove"/> class.
    /// </summary>
    /// <param name="piece">The chess piece being moved (pawn).</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="promotion">The piece to which the pawn is promoted.</param>
    /// <param name="annotation">Optional annotation.</param>
    /// <param name="comments">Optional comments about the move.</param>
    public PromotionMove(Piece piece, Square from, Square to, Piece promotion, MoveAnnotation? annotation = null, string? comments = null)
        : base(piece, from, to, annotation, comments) {
        Promotion = promotion;
    }

    /// <summary>
    /// Returns a string specific to the promotion move.
    /// </summary>
    /// <returns>A string indicating the promotion piece.</returns>
    protected override string GetMoveSpecificString() => $" (promotion to {Promotion})";
}
