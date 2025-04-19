using ngnchess.Models.Enum;

/// <summary>
/// Represents a chess piece.
/// </summary>
public struct Piece {
    /// <summary>
    /// Gets or sets the type of the piece.
    /// </summary>
    public PieceType Type { get; set; }

    /// <summary>
    /// Gets or sets the color of the piece.
    /// </summary>
    public PieceColor Color { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Piece"/> struct.
    /// </summary>
    /// <param name="type">The type of the piece.</param>
    /// <param name="color">The color of the piece.</param>
    public Piece(PieceType type, PieceColor color) {
        Type = type;
        Color = color;
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() {
        char colorChar = (Color == PieceColor.White) ? 'W' : 'B';
        char typeChar = ' ';
        switch (Type) {
            case PieceType.Pawn:
                typeChar = 'P';
                break;
            case PieceType.Rook:
                typeChar = 'R';
                break;
            case PieceType.Knight:
                typeChar = 'N';
                break;
            case PieceType.Bishop:
                typeChar = 'B';
                break;
            case PieceType.Queen:
                typeChar = 'Q';
                break;
            case PieceType.King:
                typeChar = 'K';
                break;
        }
        return $"{colorChar}{typeChar}";
    }
}
