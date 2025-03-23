namespace ngnchess.Components;

/// <summary>
/// Represents a square on a chessboard.
/// </summary>
public class Square {
    /// <summary>
    /// Gets the file of the square ('a' to 'h').
    /// </summary>
    public char File { get; init; }

    /// <summary>
    /// Gets the rank of the square (1 to 8).
    /// </summary>
    public int Rank { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Square"/> class using algebraic notation.
    /// </summary>
    /// <param name="algebraicNotation">The algebraic notation string (e.g., "a1").</param>
    /// <exception cref="ArgumentException">Thrown when the algebraic notation string is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the file or rank is out of range.</exception>
    public Square(string algebraicNotation) {
        if (string.IsNullOrEmpty(algebraicNotation) || algebraicNotation.Length != 2) {
            throw new ArgumentException("Invalid algebraic notation string.", nameof(algebraicNotation));
        }

        char fileChar = algebraicNotation[0];
        char rankChar = algebraicNotation[1];

        if (fileChar < 'a' || fileChar > 'h' || rankChar < '1' || rankChar > '8') {
            throw new ArgumentOutOfRangeException("Invalid notation. The file must be between 'a' and 'h', and the rank between '1' and '8'.", nameof(algebraicNotation));
        }

        File = fileChar;
        Rank = int.Parse(rankChar.ToString());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Square"/> class using file and rank.
    /// </summary>
    /// <param name="file">The file of the square ('a' to 'h').</param>
    /// <param name="rank">The rank of the square (1 to 8).</param>
    public Square(char file, int rank) : this($"{file}{rank}") {
    }

    /// <summary>
    /// Converts the square to array coordinates.
    /// </summary>
    /// <returns>A tuple containing the row and column indices.</returns>
    public (int Row, int Col) ToArrayIndices() {
        return (8 - Rank, File - 'a');
    }

    /// <summary>
    /// Converts array coordinates to algebraic notation.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <param name="col">The column index.</param>
    /// <returns>The algebraic notation string.</returns>
    public static string ToAlgebraicNotation(int row, int col) {
        char file = (char)('a' + col);
        int rank = 8 - row;
        return $"{file}{rank}";
    }

    /// <summary>
    /// Returns the algebraic notation string of the square.
    /// </summary>
    /// <returns>The algebraic notation string.</returns>
    public override string ToString() {
        return $"{File}{Rank}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        Square other = (Square)obj;
        return File == other.File && Rank == other.Rank;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() {
        return HashCode.Combine(File, Rank);
    }
}
