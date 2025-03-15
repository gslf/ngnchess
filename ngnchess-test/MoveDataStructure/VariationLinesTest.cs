using ngnchess.MoveDataStructure;

namespace ngnchess_test.MoveDataStructure;

public class VariationLinesTest{
    private readonly MoveNode parentMove;
    private readonly MoveNode initialMove;
    private readonly MoveNode move2;

    public VariationLinesTest() {
        parentMove = new MoveNode("e5", PieceColor.Black);
        initialMove = new MoveNode("e4", PieceColor.White);
        move2 = new MoveNode("Nf3", PieceColor.Black);
    }

    [Fact]
    public void InitializeVariationLines_ShouldSetPropertiesCorrectly() {
        // Act
        var variationLines = new VariationLines(parentMove);

        // Assert
        Assert.Equal(parentMove, variationLines.Parent);
        Assert.Empty(variationLines);
    }

    [Fact]
    public void AddVariationLine_ShouldAddNewVariation() {
        // Arrange
        var variationLines = new VariationLines(parentMove);

        // Act
        variationLines.AddVariationLine(initialMove);

        // Assert
        Assert.Single(variationLines);
        Assert.Equal(initialMove, variationLines.GetVariationLine(0).Root);
    }

    [Fact]
    public void AddVariationLine_ShouldThrowException_WhenMoveIsNull() {
        // Arrange
        var variationLines = new VariationLines(parentMove);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => variationLines.AddVariationLine(null));
    }

    [Fact]
    public void RemoveVariationLine_ShouldRemoveVariationAtIndex() {
        // Arrange
        var variationLines = new VariationLines(parentMove);
        variationLines.AddVariationLine(initialMove);

        // Act
        variationLines.RemoveVariationLine(0);

        // Assert
        Assert.Empty(variationLines);
    }

    [Fact]
    public void RemoveVariationLine_ShouldThrowException_WhenIndexIsOutOfRange() {
        // Arrange
        var variationLines = new VariationLines(parentMove);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => variationLines.RemoveVariationLine(0));
    }

    [Fact]
    public void GetVariationLine_ShouldReturnVariationAtIndex() {
        // Arrange
        var variationLines = new VariationLines(parentMove);
        variationLines.AddVariationLine(initialMove);

        // Act
        var variation = variationLines.GetVariationLine(0);

        // Assert
        Assert.Equal(initialMove, variation.Root);
    }

    [Fact]
    public void GetVariationLine_ShouldThrowException_WhenIndexIsOutOfRange() {
        // Arrange
        var variationLines = new VariationLines(parentMove);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => variationLines.GetVariationLine(0));
    }

    [Fact]
    public void Enumerator_ShouldEnumerateVariations() {
        // Arrange
        var variationLines = new VariationLines(parentMove);
        variationLines.AddVariationLine(initialMove);
        variationLines.AddVariationLine(move2);

        // Act
        var variations = variationLines.ToList();

        // Assert
        Assert.Equal(2, variations.Count);
        Assert.Equal(initialMove, variations[0].Root);
        Assert.Equal(move2, variations[1].Root);
    }
}
