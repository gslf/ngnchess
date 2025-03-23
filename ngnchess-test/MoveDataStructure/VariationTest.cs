using ngnchess.Components;
using ngnchess.MoveDataStructure;

namespace ngnchess_test.MoveDataStructure;

public class VariationTest {
    private readonly Piece whitePawn;
    private readonly Piece blackPawn;
    private readonly Piece whiteKnight;
    private readonly Square fromE2;
    private readonly Square toE4;
    private readonly Square fromE7;
    private readonly Square toE5;
    private readonly Square fromG1;
    private readonly Square toF3;
    private readonly Square fromB8;
    private readonly Square toC6;
    private readonly Move moveE2E4;
    private readonly Move moveE7E5;
    private readonly Move moveG1F3;
    private readonly Move moveB8C6;
    private readonly MoveNode initialMove;
    private readonly MoveNode parentMove;
    private readonly MoveNode move1;
    private readonly MoveNode move2;
    private readonly MoveNode move3;

    public VariationTest() {
        whitePawn = new Piece(PieceType.Pawn, PieceColor.White);
        blackPawn = new Piece(PieceType.Pawn, PieceColor.Black);
        whiteKnight = new Piece(PieceType.Knight, PieceColor.White);
        fromE2 = new Square('e', 2);
        toE4 = new Square('e', 4);
        fromE7 = new Square('e', 7);
        toE5 = new Square('e', 5);
        fromG1 = new Square('g', 1);
        toF3 = new Square('f', 3);
        fromB8 = new Square('b', 8);
        toC6 = new Square('c', 6);
        moveE2E4 = new Move(whitePawn, fromE2, toE4);
        moveE7E5 = new Move(blackPawn, fromE7, toE5);
        moveG1F3 = new Move(whiteKnight, fromG1, toF3);
        moveB8C6 = new Move(whiteKnight, fromB8, toC6);
        initialMove = new MoveNode(moveE2E4);
        parentMove = new MoveNode(moveE7E5);
        move1 = new MoveNode(moveE7E5);
        move2 = new MoveNode(moveG1F3);
        move3 = new MoveNode(moveB8C6);
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
        variation.PushMove(move1);

        // Assert
        Assert.Equal(move1, variation.CurrentNode);
        Assert.Equal(2, variation.Size);
        Assert.Equal(move1, initialMove.Next);
        Assert.Equal(initialMove, move1.Prev);
    }

    [Fact]
    public void PushMove_ShouldThrowException_WhenNewMoveHasSameColor() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);
        var newMove = new MoveNode(moveB8C6);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => variation.PushMove(newMove));
    }

    [Fact]
    public void DropLastMove_ShouldRemoveLastMoveFromVariation() {
        // Arrange
        var variation = new Variation(initialMove, parentMove);
        variation.PushMove(move1);

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
        variation.PushMove(move1);
        variation.PushMove(move2);

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
