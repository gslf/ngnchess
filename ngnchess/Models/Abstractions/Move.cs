using ngnchess.Components;
using ngnchess.Models.Enum;
using ngnchess.Extensions;

namespace ngnchess.Models.Abstractions;

/// <summary>
/// Represents a chess move, including the piece being moved, the starting and ending squares,
/// and an optional annotation.
/// </summary>
public abstract class Move {
    /// <summary>
    /// The chess piece being moved.
    /// </summary>
    public Piece Piece { get; }

    /// <summary>
    /// The starting square of the move.
    /// </summary>
    public Square From { get; }

    /// <summary>
    /// The ending square of the move.
    /// </summary>
    public Square To { get; }

    /// <summary>
    /// An optional annotation of the move.
    /// </summary>
    public MoveAnnotation? Annotation { get; }

    /// <summary>
    /// The type of the move.
    /// </summary>
    public abstract MoveType Type { get; }

    /// <summary>
    /// Constructor for the Move class.
    /// </summary>
    /// <param name="piece">The chess piece being moved.</param>
    /// <param name="from">The starting square of the move.</param>
    /// <param name="to">The ending square of the move.</param>
    /// <param name="annotation">Optional annotation.</param>
    protected Move(Piece piece, Square from, Square to, MoveAnnotation? annotation = null) {
        Piece = piece;
        From = from;
        To = to;
        Annotation = annotation;
    }

    /// <summary>
    /// Returns a string that represents the current move.
    /// </summary>
    /// <returns>A string that represents the current move.</returns>
    public override string ToString() {
        string annotationString = Annotation.HasValue ? $" {Annotation.GetDescription()}" : "";
        return $"{Piece} from {From} to {To}{GetMoveSpecificString()}{annotationString}";
    }

    /// <summary>
    /// Returns a string specific to the move type.
    /// </summary>
    /// <returns>A string specific to the move type.</returns>
    protected abstract string GetMoveSpecificString();
}