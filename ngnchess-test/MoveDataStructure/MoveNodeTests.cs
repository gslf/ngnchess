namespace ngnchess_test.MoveDataStructure;

using System;
using Xunit;
using ngnchess.MoveDataStructure;


public class MoveNodeTests {
    [Fact]
    public void Constructor_ValidMove_ShouldInitializeProperties() {
        // Arrange
        string name = "e4";
        PieceColor color = PieceColor.White;
        string comment = "Opening move";

        // Act
        MoveNode move = new MoveNode(name, color, comment);

        // Assert
        Assert.Equal(name, move.Name);
        Assert.Equal(color, move.Color);
        Assert.Equal(comment, move.Comment);

        // Test Long Castling
        MoveNode longCastling = new MoveNode("O-O-O", PieceColor.White, "Long castling");
        Assert.Equal("O-O-O", longCastling.Name);
        Assert.Equal(PieceColor.White, longCastling.Color);
        Assert.Equal("Long castling", longCastling.Comment);

        // Test Short Castling
        MoveNode shortCastling = new MoveNode("O-O", PieceColor.White, "Short castling");
        Assert.Equal("O-O", shortCastling.Name);
        Assert.Equal(PieceColor.White, shortCastling.Color);
        Assert.Equal("Short castling", shortCastling.Comment);

        // Test Piece moves
        MoveNode pieceMove = new MoveNode("Nf3", PieceColor.White, "Knight to f3");
        Assert.Equal("Nf3", pieceMove.Name);
        Assert.Equal(PieceColor.White, pieceMove.Color);
        Assert.Equal("Knight to f3", pieceMove.Comment);

        // Test Pawn moves with capture
        MoveNode pawnCapture = new MoveNode("exd5", PieceColor.White, "Pawn captures on d5");
        Assert.Equal("exd5", pawnCapture.Name);
        Assert.Equal(PieceColor.White, pawnCapture.Color);
        Assert.Equal("Pawn captures on d5", pawnCapture.Comment);

        // Test Pawn moves without capture
        MoveNode pawnMove = new MoveNode("e4", PieceColor.White, "Pawn to e4");
        Assert.Equal("e4", pawnMove.Name);
        Assert.Equal(PieceColor.White, pawnMove.Color);
        Assert.Equal("Pawn to e4", pawnMove.Comment);

        // Test annotations
        MoveNode annotation1 = new MoveNode("e4!", PieceColor.White, "Good move");
        Assert.Equal("e4!", annotation1.Name);
        Assert.Equal(PieceColor.White, annotation1.Color);
        Assert.Equal("Good move", annotation1.Comment);

        MoveNode annotation2 = new MoveNode("e4?", PieceColor.White, "Bad move");
        Assert.Equal("e4?", annotation2.Name);
        Assert.Equal(PieceColor.White, annotation2.Color);
        Assert.Equal("Bad move", annotation2.Comment);

        MoveNode annotation3 = new MoveNode("e4!!", PieceColor.White, "Brilliant move");
        Assert.Equal("e4!!", annotation3.Name);
        Assert.Equal(PieceColor.White, annotation3.Color);
        Assert.Equal("Brilliant move", annotation3.Comment);

        MoveNode annotation4 = new MoveNode("e4!?", PieceColor.White, "Interesting move");
        Assert.Equal("e4!?", annotation4.Name);
        Assert.Equal(PieceColor.White, annotation4.Color);
        Assert.Equal("Interesting move", annotation4.Comment);

        MoveNode annotation5 = new MoveNode("e4?!", PieceColor.White, "Dubious move");
        Assert.Equal("e4?!", annotation5.Name);
        Assert.Equal(PieceColor.White, annotation5.Color);
        Assert.Equal("Dubious move", annotation5.Comment);
    }

    [Fact]
    public void Constructor_InvalidMove_ShouldThrowArgumentException() {
        // Arrange
        string invalidName = "invalid";
        PieceColor color = PieceColor.White;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new MoveNode(invalidName, color));
    }

    [Fact]
    public void ToString_WithComment_ShouldReturnFormattedString() {
        // Arrange
        MoveNode move = new MoveNode("e4", PieceColor.White, "Good move");

        // Act
        string result = move.ToString();

        // Assert
        Assert.Equal("White e4 (Good move)", result);
    }


    [Fact]
    public void Prev_SetValidPrev_ShouldSetPrev() {
        // Arrange
        MoveNode move1 = new MoveNode("e4", PieceColor.White);
        MoveNode move2 = new MoveNode("e5", PieceColor.Black);

        // Act
        move2.Prev = move1;

        // Assert
        Assert.Equal(move1, move2.Prev);
    }

    [Fact]
    public void Prev_SetInvalidColorPrev_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode move1 = new MoveNode("e4", PieceColor.White);
        MoveNode move2 = new MoveNode("Nf3", PieceColor.White);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => move2.Prev = move1);
    }

    [Fact]
    public void Next_SetValidNext_ShouldSetNext() {
        // Arrange
        MoveNode move1 = new MoveNode("e4", PieceColor.White);
        MoveNode move2 = new MoveNode("e5", PieceColor.Black);

        // Act
        move1.Next = move2;

        // Assert
        Assert.Equal(move2, move1.Next);
    }

    [Fact]
    public void Next_SetInvalidColorNext_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode move1 = new MoveNode("e4", PieceColor.White);
        MoveNode move2 = new MoveNode("Nf3", PieceColor.White);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => move1.Next = move2);
    }

    [Fact]
    public void Parent_SetValidParent_ShouldSetParent() {
        // Arrange
        MoveNode move1 = new MoveNode("e4", PieceColor.White);
        MoveNode move2 = new MoveNode("e5", PieceColor.White);

        // Act
        move2.Parent = move1;

        // Assert
        Assert.Equal(move1, move2.Parent);
    }

    [Fact]
    public void Parent_SetSelfAsParent_ShouldThrowInvalidOperationException() {
        // Arrange
        MoveNode move = new MoveNode("e4", PieceColor.White);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => move.Parent = move);
    }
}
