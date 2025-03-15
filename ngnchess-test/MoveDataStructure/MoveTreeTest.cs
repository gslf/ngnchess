namespace ngnchess_test.MoveDataStructure;

using ngnchess.MoveDataStructure;
public class MoveTreeTest
{

    private MoveNode _move1 = new MoveNode("e4", PieceColor.White);
    private MoveNode _move2 = new MoveNode("e5", PieceColor.Black);
    private MoveNode _move3 = new MoveNode("d4", PieceColor.White);

    [Fact]
    public void AppendMove_EmptyTree_SetsRootAndCurrent() {
        // Arrange
        var tree = new MoveTree();

        // Act
        tree.PushMove(_move1);

        // Assert
        Assert.Equal(_move1, tree.Root);
        Assert.Equal(_move1, tree.CurrentNode);
    }

    [Fact]
    public void AppendMove_AddsToChain() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(_move1);
        tree.PushMove(_move2);

        // Assert
        Assert.Equal(_move1, tree.Root);
        Assert.Equal(_move2, tree.CurrentNode);
        Assert.Equal(_move2, _move1.Next);
        Assert.Equal(_move1, _move2.Prev);
    }

    [Fact]
    public void RemoveLastMove_RemovesMoveCorrectly() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(_move1);
        tree.PushMove(_move2);

        // Act
        tree.DropLastMove();

        // Assert
        Assert.Equal(_move1, tree.CurrentNode);
        Assert.Null(_move1.Next);
    }

    [Fact]
    public void RemoveRangeMoves_RemovesMovesCorrectly() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(_move1);
        tree.PushMove(_move2);
        tree.PushMove(_move3);

        // Act
        tree.DropLastMoves(2);

        // Assert
        Assert.Equal(_move1, tree.CurrentNode);
        Assert.Null(_move1.Next);
    }

    [Fact]
    public void RemoveRangeMoves_WhenRemovingMoreThanPresent_ThrowsException() {
        // Arrange
        var tree = new MoveTree();

        tree.PushMove(_move1);
        tree.PushMove(_move2);

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
