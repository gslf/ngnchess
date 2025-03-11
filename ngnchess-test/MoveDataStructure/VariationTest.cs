using ngnchess.MoveDataStructure;

namespace ngnchess_test.MoveDataStructure;

public class VariationTest{
    private readonly MoveNode initialMove;
    private readonly MoveNode parentMove;
    private readonly MoveNode move2;
    private readonly MoveNode move3;

    public VariationTest() {
        initialMove = new MoveNode("e4", PieceColor.White);
        parentMove = new MoveNode("e5", PieceColor.White);
        move2 = new MoveNode("Nf3", PieceColor.Black);
        move3 = new MoveNode("Nc6", PieceColor.White);
    }

    [Fact]
    public void InitializeVariation_ShouldSetPropertiesCorrectly() {
        // Act
        var variation = new Variation(initialMove, parentMove);

        // Assert
        Assert.Equal(initialMove, variation.Root);
        Assert.Equal(initialMove, variation.CurrentNode);
        Assert.Equal(1, variation.Size);
        Assert.Equal(parentMove, variation.Parent);
    }

    [Fact]
    public void PushMove_ShouldAddNewMoveToVariation() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);

        // Act
        variation.PushMove(move2);

        // Assert
        Assert.Equal(move2, variation.CurrentNode);
        Assert.Equal(2, variation.Size);
        Assert.Equal(move2, initialMove.Next);
        Assert.Equal(initialMove, move2.Prev);
    }

    [Fact]
    public void PushMove_ShouldThrowException_WhenNewMoveHasSameColor() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);
        var newMove = new MoveNode("Nf3", PieceColor.White);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => variation.PushMove(newMove));
    }

    [Fact]
    public void DropLastMove_ShouldRemoveLastMoveFromVariation() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);
        variation.PushMove(move2);

        // Act
        variation.DropLastMove();

        // Assert
        Assert.Equal(initialMove, variation.CurrentNode);
        Assert.Equal(1, variation.Size);
        Assert.Null(initialMove.Next);
    }

    [Fact]
    public void DropLastMove_ShouldThrowException_WhenOnlyOneMoveExists() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => variation.DropLastMove());
    }

    [Fact]
    public void DropLastMoves_ShouldRemoveSpecifiedNumberOfMoves() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);
        variation.PushMove(move2);
        variation.PushMove(move3);

        // Act
        variation.DropLastMoves(2);

        // Assert
        Assert.Equal(initialMove, variation.CurrentNode);
        Assert.Equal(1, variation.Size);
        Assert.Null(initialMove.Next);
    }

    [Fact]
    public void DropLastMoves_ShouldThrowException_WhenNumberOfMovesIsInvalid() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => variation.DropLastMoves(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => variation.DropLastMoves(-1));
        Assert.Throws<InvalidOperationException>(() => variation.DropLastMoves(2));
    }
}
