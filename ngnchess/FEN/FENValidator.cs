using System.Text.RegularExpressions;


namespace ngnchess.FEN;

/// <summary>
/// Provides methods to validate FENHandler (Forsyth-Edwards Notation) strings.
/// </summary>
public static class FENValidator {
    /// <summary>
    /// Validates the given FENHandler string.
    /// </summary>
    /// <param name="fen">The FENHandler string to validate.</param>
    /// <returns><c>true</c> if the FENHandler string is valid; otherwise, <c>false</c>.</returns>
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
}
