using ngnchess.Components;
using ngnchess.MoveDataStructure;

namespace ngnchess_test.MoveDataStructure;

public class VariationLinesTest {
    private readonly Piece whitePawn;
    private readonly Piece blackPawn;
    private readonly Piece whiteKnight;
    private readonly Square fromE2;
    private readonly Square toE4;
    private readonly Square fromE7;
    private readonly Square toE5;
    private readonly Square fromG1;
    private readonly Square toF3;
    private readonly Move moveE2E4;
    private readonly Move moveE7E5;
    private readonly Move moveG1F3;
    private readonly MoveNode parentMove;
    private readonly MoveNode initialMove;
    private readonly MoveNode move2;

    public VariationLinesTest() {
        whitePawn = new Piece(PieceType.Pawn, PieceColor.White);
        blackPawn = new Piece(PieceType.Pawn, PieceColor.Black);
        whiteKnight = new Piece(PieceType.Knight, PieceColor.White);
        fromE2 = new Square('e', 2);
        toE4 = new Square('e', 4);
        fromE7 = new Square('e', 7);
        toE5 = new Square('e', 5);
        fromG1 = new Square('g', 1);
        toF3 = new Square('f', 3);
        moveE2E4 = new Move(whitePawn, fromE2, toE4);
        moveE7E5 = new Move(blackPawn, fromE7, toE5);
        moveG1F3 = new Move(whiteKnight, fromG1, toF3);
        parentMove = new MoveNode(moveE7E5);
        initialMove = new MoveNode(moveE2E4);
        move2 = new MoveNode(moveG1F3);
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
