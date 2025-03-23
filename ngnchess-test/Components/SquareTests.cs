
using ngnchess.Components;

namespace ngnchess_test.Components;

public class SquareTests {
    [Theory]
    [InlineData("a1", 'a', 1)]
    [InlineData("h8", 'h', 8)]
    [InlineData("d4", 'd', 4)]
    public void Constructor_ValidAlgebraicNotation_ShouldSetProperties(string notation, char expectedFile, int expectedRank) {
        // Act
        var square = new Square(notation);

        // Assert
        Assert.Equal(expectedFile, square.File);
        Assert.Equal(expectedRank, square.Rank);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("a")]
    public void Constructor_InvalidAlgebraicNotation_ShouldThrowArgumentException(string notation) {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Square(notation));
    }

    [Theory]
    [InlineData("a9")]
    [InlineData("i1")]
    [InlineData("a0")]
    public void Constructor_InvalidAlgebraicNotation_ShouldThrowArgumentOutOfRangeException(string notation) {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Square(notation));
    }

    [Theory]
    [InlineData('a', 1, "a1")]
    [InlineData('h', 8, "h8")]
    [InlineData('d', 4, "d4")]
    public void Constructor_ValidFileAndRank_ShouldSetProperties(char file, int rank, string expectedNotation) {
        // Act
        var square = new Square(file, rank);

        // Assert
        Assert.Equal(file, square.File);
        Assert.Equal(rank, square.Rank);
        Assert.Equal(expectedNotation, square.ToString());
    }

    [Theory]
    [InlineData('i', 1)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    public void Constructor_InvalidFileOrRank_ShouldThrowArgumentOutOfRangeException(char file, int rank) {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Square(file, rank));
    }

    [Theory]
    [InlineData(0, 0, "a8")]
    [InlineData(7, 7, "h1")]
    [InlineData(3, 3, "d5")]
    public void ToAlgebraicNotation_ValidIndices_ShouldReturnCorrectNotation(int row, int col, string expectedNotation) {
        // Act
        var notation = Square.ToAlgebraicNotation(row, col);

        // Assert
        Assert.Equal(expectedNotation, notation);
    }

    [Theory]
    [InlineData('a', 1, 7, 0)]
    [InlineData('h', 8, 0, 7)]
    [InlineData('d', 4, 4, 3)]
    public void ToArrayIndices_ValidSquare_ShouldReturnCorrectIndices(char file, int rank, int expectedRow, int expectedCol) {
        // Arrange
        var square = new Square(file, rank);

        // Act
        var (row, col) = square.ToArrayIndices();

        // Assert
        Assert.Equal(expectedRow, row);
        Assert.Equal(expectedCol, col);
    }

    [Fact]
    public void Equals_SameSquare_ShouldReturnTrue() {
        // Arrange
        var square1 = new Square('a', 1);
        var square2 = new Square('a', 1);

        // Act & Assert
        Assert.True(square1.Equals(square2));
    }

    [Fact]
    public void Equals_DifferentSquare_ShouldReturnFalse() {
        // Arrange
        var square1 = new Square('a', 1);
        var square2 = new Square('b', 2);

        // Act & Assert
        Assert.False(square1.Equals(square2));
    }

    [Fact]
    public void GetHashCode_SameSquare_ShouldReturnSameHashCode() {
        // Arrange
        var square1 = new Square('a', 1);
        var square2 = new Square('a', 1);

        // Act & Assert
        Assert.Equal(square1.GetHashCode(), square2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentSquare_ShouldReturnDifferentHashCode() {
        // Arrange
        var square1 = new Square('a', 1);
        var square2 = new Square('b', 2);

        // Act & Assert
        Assert.NotEqual(square1.GetHashCode(), square2.GetHashCode());
    }
}
