using System.Text.RegularExpressions;

namespace ngnchess.MoveDataStructure;

/// <summary>
/// Represents a chess move using Portable Game Notation (PGN).
/// The move is represented by the PGN name (e.g., "e4", "Nf3")
/// and an optional comment providing further insights or context.
/// </summary>
public class MoveNode {
    /// <summary>
    /// Get or init the move's PGN notation.
    /// For example, "e4", "Nf3", etc.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Get or init the color of piece.
    /// </summary>
    public PieceColor Color { get; init; }

    /// <summary>
    /// Gets or sets an optional comment related to the move.
    /// </summary>
    public string? Comment { get; set; }

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
            if (value != null && value.Color == this.Color)
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
            if (value != null && value.Color == this.Color)
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
    /// Initializes a new instance of the <see cref="MoveNode"/> class with the specified PGN name and comment.
    /// </summary>
    /// <param name="name">The PGN notation representing the move.</param>
    /// <param name="color">The piece color of the move.</param>
    /// <param name="comment">An optional comment providing additional details about the move.</param>
    /// <param name="prev">The previous move node in the move sequence.</param>
    /// <param name="next">The next move node in the move sequence.</param>
    /// <param name="parent">The parent move node, if this move is a variation.</param>
    /// <exception cref="ArgumentException">Thrown when the provided move name is invalid.</exception>
    public MoveNode(string name, 
                    PieceColor color, 
                    string? comment = null, 
                    MoveNode? prev = null, 
                    MoveNode? next = null, 
                    MoveNode? parent = null) {
        if (!_validateMove(name))
            throw new ArgumentException($"Invalid move name: '{name}'", nameof(name));

        Name = name;
        Comment = comment;
        Color = color;
        Prev = prev;
        Next = next;
        Parent = parent;
        Variations = new  VariationLines(this);
    }

    /// <summary>
    /// Returns a string that represents the current chess move.
    /// </summary>
    /// <returns>
    /// A string representation of the move, including the piece color and PGN notation. 
    /// If a comment is present, it is included in parentheses.
    /// For example: "White e4" or "Black Nf3 (Good move)".
    /// </returns>
    public override string ToString() {
        return $"{Color} {Name}" + (Comment != null ? $" ({Comment})" : "");
    }

    private bool _validateMove(string move) {
        string pattern = @"^(?:O-O(?:-O)?[+#]?|                              # Castling
                        [KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](?:=[QRBN])?[+#]?|   # Piece moves
                        [a-h]x?[a-h][1-8](?:=[QRBN])?[+#]?|                  # Pawn moves with capture
                        [a-h][1-8](?:=[QRBN])?[+#]?)                         # Pawn moves without capture
                        [!?]{0,2}$                                           # Optional annotations (!, ?, !!, !?, ?!)";

        return Regex.IsMatch(move, pattern, RegexOptions.IgnorePatternWhitespace);
    }
}

