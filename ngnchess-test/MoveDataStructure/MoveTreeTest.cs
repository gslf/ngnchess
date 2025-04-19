namespace ngnchess_test.MoveDataStructure;

using ngnchess.Components;
using ngnchess.Models.Abstractions;
using ngnchess.Models.Enum;
using ngnchess.MoveDataStructure;
public class MoveTreeTest {
    private readonly Piece whitePawn;
    private readonly Piece blackPawn;
    private readonly Square fromE2;
    private readonly Square toE4;
    private readonly Square fromE7;
    private readonly Square toE5;
    private readonly Square fromD2;
    private readonly Square toD4;
    private readonly Move moveE2E4;
    private readonly Move moveE7E5;
    private readonly Move moveD2D4;
    private readonly MoveNode moveNode1;
    private readonly MoveNode moveNode2;
    private readonly MoveNode moveNode3;

    public MoveTreeTest() {
        whitePawn = new Piece(PieceType.Pawn, PieceColor.White);
        blackPawn = new Piece(PieceType.Pawn, PieceColor.Black);
        fromE2 = new Square('e', 2);
        toE4 = new Square('e', 4);
        fromE7 = new Square('e', 7);
        toE5 = new Square('e', 5);
        fromD2 = new Square('d', 2);
        toD4 = new Square('d', 4);
        moveE2E4 = new StandardMove(whitePawn, fromE2, toE4);
        moveE7E5 = new StandardMove(blackPawn, fromE7, toE5);
        moveD2D4 = new StandardMove(whitePawn, fromD2, toD4);
        moveNode1 = new MoveNode(moveE2E4);
        moveNode2 = new MoveNode(moveE7E5);
        moveNode3 = new MoveNode(moveD2D4);
    }

    [Fact]
    public void AppendMove_EmptyTree_SetsRootAndCurrent() {
        // Arrange
        var tree = new MoveTree();

        // Act
        tree.PushMove(moveNode1);

        // Assert
        Assert.Equal(moveNode1, tree.Root);
        Assert.Equal(moveNode1, tree.CurrentNode);
    }

    [Fact]
    public void AppendMove_AddsToChain() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(moveNode1);
        tree.PushMove(moveNode2);

        // Assert
        Assert.Equal(moveNode1, tree.Root);
        Assert.Equal(moveNode2, tree.CurrentNode);
        Assert.Equal(moveNode2, moveNode1.Next);
        Assert.Equal(moveNode1, moveNode2.Prev);
    }

    [Fact]
    public void RemoveLastMove_RemovesMoveCorrectly() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(moveNode1);
        tree.PushMove(moveNode2);

        // Act
        tree.DropLastMove();

        // Assert
        Assert.Equal(moveNode1, tree.CurrentNode);
        Assert.Null(moveNode1.Next);
    }

    [Fact]
    public void RemoveRangeMoves_RemovesMovesCorrectly() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(moveNode1);
        tree.PushMove(moveNode2);
        tree.PushMove(moveNode3);

        // Act
        tree.DropLastMoves(2);

        // Assert
        Assert.Equal(moveNode1, tree.CurrentNode);
        Assert.Null(moveNode1.Next);
    }

    [Fact]
    public void RemoveRangeMoves_WhenRemovingMoreThanPresent_ThrowsException() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(moveNode1);
        tree.PushMove(moveNode2);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => tree.DropLastMoves(3));
    }

    [Fact]
    public void RemoveLastMove_WhenTreeEmpty_ThrowsException() {
        // Arrange
        var tree = new MoveTree();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => tree.DropLastMove());
    }
}
