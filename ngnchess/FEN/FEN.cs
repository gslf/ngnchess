using System.Text.RegularExpressions;

namespace ngnchess.FEN;

/// <summary>
/// Manages the FEN (Forsyth-Edwards Notation) string for a chess board state.
/// </summary>
public class FENManager {
    private string fen;

    /// <summary>
    /// Initializes a new instance of the <see cref="FENManager"/> class with the specified FEN string.
    /// </summary>
    /// <param name="fen">The FEN string representing the board state.</param>
    /// <exception cref="ArgumentException">Thrown when the provided FEN string is invalid.</exception>
    public FENManager(string fen) {
        if (!IsValidFen(fen))
            throw new ArgumentException("Invalid FEN string.");
        this.fen = fen;
    }

    /// <summary>
    /// Gets the FEN string.
    /// </summary>
    /// <returns>The FEN string representing the board state.</returns>
    public string GetFen() {
        return fen;
    }

    /// <summary>
    /// Validates the given FEN string.
    /// </summary>
    /// <param name="fen">The FEN string to validate.</param>
    /// <returns><c>true</c> if the FEN string is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidFen(string fen) {
        if (string.IsNullOrWhiteSpace(fen))
            return false;

        string[] parts = fen.Split(' ');
        if (parts.Length != 6)
            return false;

        // Board size validation
        string board = parts[0];
        string[] rows = board.Split('/');
        if (rows.Length != 8)
            return false;

        // Line validation
        Regex rowRegex = new Regex(@"^[rnbqkpRNBQKP1-8]+$");
        foreach (string row in rows) {
            if (!rowRegex.IsMatch(row))
                return false;

            // Square count validation
            int squareCount = 0;
            foreach (char c in row) {
                if (char.IsDigit(c))
                    squareCount += (int)char.GetNumericValue(c);
                else
                    squareCount++;
            }
            if (squareCount != 8)
                return false;
        }

        // Active turn validation
        Regex activeRegex = new Regex("^(w|b)$");
        if (!activeRegex.IsMatch(parts[1]))
            return false;

        // Castling validation
        string castling = parts[2];
        if (castling != "-") {
            Regex castlingRegex = new Regex(@"^(?:(?!.*(.).*\1)[KQkq]{1,4})$");
            if (!castlingRegex.IsMatch(castling))
                return false;
        }

        // En passant validation
        Regex enPassantRegex = new Regex(@"^(-|[a-h][36])$");
        if (!enPassantRegex.IsMatch(parts[3]))
            return false;

        if (parts[3] != "-") {
            char rank = parts[3][1];
            if (parts[1] == "w" && rank != '6')
                return false;
            if (parts[1] == "b" && rank != '3')
                return false;
        } 

        // Halfmove clock validation
        Regex halfmoveRegex = new Regex(@"^\d+$");
        if (!halfmoveRegex.IsMatch(parts[4]))
            return false;

        // Fullmove clock validation
        Regex fullmoveRegex = new Regex(@"^[1-9]\d*$");
        if (!fullmoveRegex.IsMatch(parts[5]))
            return false;

        return true;
    }

    /// <summary>
    /// Splits the FEN string into its constituent parts.
    /// </summary>
    /// <returns>An array of strings representing the parts of the FEN string.</returns>
    public string[] GetFenParts() {
        return fen.Split(' ');
    }

    /// <summary>
    /// Gets the board position part of the FEN string.
    /// </summary>
    /// <returns>The board position part of the FEN string.</returns>
    public string GetBoardPosition() {
        return GetFenParts()[0];
    }

    /// <summary>
    /// Gets the active color part of the FEN string.
    /// </summary>
    /// <returns>The active color part of the FEN string.</returns>
    public char GetActiveColor() {
        return GetFenParts()[1][0];
    }

    /// <summary>
    /// Gets the castling availability part of the FEN string.
    /// </summary>
    /// <returns>The castling availability part of the FEN string.</returns>
    public string GetCastlingAvailability() {
        return GetFenParts()[2];
    }

    /// <summary>
    /// Gets the en passant target square part of the FEN string.
    /// </summary>
    /// <returns>The en passant target square part of the FEN string.</returns>
    public string GetEnPassantTarget() {
        return GetFenParts()[3];
    }

    /// <summary>
    /// Gets the half-move clock part of the FEN string.
    /// </summary>
    /// <returns>The half-move clock part of the FEN string.</returns>
    public int GetHalfMoveClock() {
        return int.Parse(GetFenParts()[4]);
    }

    /// <summary>
    /// Gets the full-move number part of the FEN string.
    /// </summary>
    /// <returns>The full-move number part of the FEN string.</returns>
    public int GetFullMoveNumber() {
        return int.Parse(GetFenParts()[5]);
    }
}
