namespace ngnchess.FEN;

/// <summary>
/// Manages the FENHandler (Forsyth-Edwards Notation) string for a chess board state.
/// </summary>
public class FENHandler {
    private string fen;

    /// <summary>
    /// Initializes a new instance of the <see cref="FENHandler"/> class with the specified FENHandler string.
    /// </summary>
    /// <param name="fen">The FENHandler string representing the board state.</param>
    /// <exception cref="ArgumentException">Thrown when the provided FENHandler string is invalid.</exception>
    public FENHandler(string fen) {
        if (!FENValidator.IsValidFen(fen))
            throw new ArgumentException("Invalid FEN string.");
        this.fen = fen;
    }

    /// <summary>
    /// Gets the FENHandler string.
    /// </summary>
    /// <returns>The FENHandler string representing the board state.</returns>
    public string GetFenString() {
        return fen;
    }

   

    /// <summary>
    /// Splits the FENHandler string into its constituent parts.
    /// </summary>
    /// <returns>An array of strings representing the parts of the FENHandler string.</returns>
    public string[] GetFenParts() {
        return fen.Split(' ');
    }

    /// <summary>
    /// Gets the board position part of the FENHandler string.
    /// </summary>
    /// <returns>The board position part of the FENHandler string.</returns>
    public string GetBoardPosition() {
        return GetFenParts()[0];
    }

    /// <summary>
    /// Gets the active color part of the FENHandler string.
    /// </summary>
    /// <returns>The active color part of the FENHandler string.</returns>
    public char GetActiveColor() {
        return GetFenParts()[1][0];
    }

    /// <summary>
    /// Gets the castling availability part of the FENHandler string.
    /// </summary>
    /// <returns>The castling availability part of the FENHandler string.</returns>
    public string GetCastlingAvailability() {
        return GetFenParts()[2];
    }

    /// <summary>
    /// Gets the en passant target square part of the FENHandler string.
    /// </summary>
    /// <returns>The en passant target square part of the FENHandler string.</returns>
    public string GetEnPassantTarget() {
        return GetFenParts()[3];
    }

    /// <summary>
    /// Gets the half-move clock part of the FENHandler string.
    /// </summary>
    /// <returns>The half-move clock part of the FENHandler string.</returns>
    public int GetHalfMoveClock() {
        return int.Parse(GetFenParts()[4]);
    }

    /// <summary>
    /// Gets the full-move number part of the FENHandler string.
    /// </summary>
    /// <returns>The full-move number part of the FENHandler string.</returns>
    public int GetFullMoveNumber() {
        return int.Parse(GetFenParts()[5]);
    }
}
