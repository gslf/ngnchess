using ngnchess.Components;
using ngnchess.Models.Abstractions;
using System.Text.RegularExpressions;

namespace ngnchess.MoveDataStructure;

/// <summary>
/// Represents a node in a chess move sequence.
/// The move is represented by an instance of the <see cref="Move"/> class
/// and an optional comment providing further insights or context.
/// </summary>
public class MoveNode {
    /// <summary>
    /// Get or init the move.
    /// </summary>
    public Move Move { get; init; }

    /// <summary>
    /// Gets or sets the previous move node in the move sequence.
    /// The previous move must have a different color as the current move.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the previous move has the same color as the current move
    /// or when a node points to itself as Next.
    /// </exception>
    public MoveNode? Prev {
        get => _prev;
        set {
            if (value != null && value.Move.Piece.Color == this.Move.Piece.Color)
                throw new InvalidOperationException("The previous move must have a different color as the current move.");

            if (value == this)
                throw new InvalidOperationException("A node cannot point to itself as Next.");

            _prev = value;
        }
    }
    private MoveNode? _prev;

    /// <summary>
    /// Gets or sets the next move node in the move sequence.
    /// The next move must have a different color as the current move.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the next move has the same color as the current move
    /// or when a node points to itself as Prev.
    /// </exception>
    public MoveNode? Next {
        get => _next;
        set {
            if (value != null && value.Move.Piece.Color == this.Move.Piece.Color)
                throw new InvalidOperationException("The next move must have a different color as the current move.");

            if (value == this)
                throw new InvalidOperationException("A node cannot point to itself as Prev.");

            _next = value;
        }
    }
    private MoveNode? _next;

    /// <summary>
    /// Gets or sets the parent move node in the move sequence.
    /// The parent move must have a different color as the current move.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a node points to itself as Parent.
    /// </exception>
    public MoveNode? Parent {
        get => _parent;
        set {
            if (value == this)
                throw new InvalidOperationException("A node cannot point to itself as Parent.");

            _parent = value;
        }
    }
    private MoveNode? _parent;

    /// <summary>
    /// Get variations of the current move.
    /// </summary>
    public VariationLines Variations { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveNode"/> class with the specified move and comment.
    /// </summary>
    /// <param name="move">The move representing the chess move.</param>
    /// <param name="prev">The previous move node in the move sequence.</param>
    /// <param name="next">The next move node in the move sequence.</param>
    /// <param name="parent">The parent move node, if this move is a variation.</param>
    public MoveNode(Move move,
                    MoveNode? prev = null,
                    MoveNode? next = null,
                    MoveNode? parent = null) {
        Move = move;
        Prev = prev;
        Next = next;
        Parent = parent;
        Variations = new VariationLines(this);
    }

    /// <summary>
    /// Returns a string that represents the current chess move.
    /// </summary>
    /// <returns>
    /// A string representation of the move, including the move details. 
    /// If a comment is present, it is included in parentheses.
    /// For example: "White e4" or "Black Nf3 (Good move)".
    /// </returns>
    public override string ToString() {
        return Move.ToString();
    }
}

