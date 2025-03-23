namespace ngnchess_test.MoveDataStructure;

using System;
using Xunit;
using ngnchess.MoveDataStructure;
using ngnchess.Components;

public class MoveNodeTests {
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

    public MoveNodeTests() {
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
    }

    [Fact]
    public void Constructor_ValidMove_ShouldInitializeProperties() {
        // Arrange
        string comment = "Opening move";

        // Act
        MoveNode moveNode = new MoveNode(moveE2E4, comment);

        // Assert
        Assert.Equal(moveE2E4, moveNode.Move);
        Assert.Equal(comment, moveNode.Comment);
    }

    [Fact]
    public void ToString_WithComment_ShouldReturnFormattedString() {
        // Arrange
        MoveNode moveNode = new MoveNode(moveE2E4, "Good move");

        // Act
        string result = moveNode.ToString();

        // Assert
        Assert.Equal("WP from e2 to e4 (castling) (Good move)", result);
    }

    [Fact]
    public void Prev_SetValidPrev_ShouldSetPrev() {
        // Arrange
        MoveNode moveNode1 = new MoveNode(moveE2E4);
        MoveNode moveNode2 = new MoveNode(moveE7E5);

        // Act
        moveNode2.Prev = moveNode1;

        // Assert
        Assert.Equal(moveNode1, moveNode2.Prev);
    }

    [Fact]
    public void Prev_SetInvalidColorPrev_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode moveNode1 = new MoveNode(moveE2E4);
        MoveNode moveNode2 = new MoveNode(moveG1F3);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => moveNode2.Prev = moveNode1);
    }

    [Fact]
    public void Next_SetValidNext_ShouldSetNext() {
        // Arrange
        MoveNode moveNode1 = new MoveNode(moveE2E4);
        MoveNode moveNode2 = new MoveNode(moveE7E5);

        // Act
        moveNode1.Next = moveNode2;

        // Assert
        Assert.Equal(moveNode2, moveNode1.Next);
    }

    [Fact]
    public void Next_SetInvalidColorNext_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode moveNode1 = new MoveNode(moveE2E4);
        MoveNode moveNode2 = new MoveNode(moveG1F3);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => moveNode1.Next = moveNode2);
    }

    [Fact]
    public void Parent_SetValidParent_ShouldSetParent() {
        // Arrange
        MoveNode moveNode1 = new MoveNode(moveE2E4);
        MoveNode moveNode2 = new MoveNode(moveE7E5);

        // Act
        moveNode2.Parent = moveNode1;

        // Assert
        Assert.Equal(moveNode1, moveNode2.Parent);
    }

    [Fact]
    public void Parent_SetSelfAsParent_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode moveNode = new MoveNode(moveE2E4);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => moveNode.Parent = moveNode);
    }
}
