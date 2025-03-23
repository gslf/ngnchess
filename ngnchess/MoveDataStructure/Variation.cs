using System.Dynamic;
using System.Text;

namespace ngnchess.MoveDataStructure;

/// <summary>
/// Represents a variation of moves in a chess game.
/// </summary>
public class Variation {
    /// <summary>
    /// Gets the root move node in the variation.
    /// </summary>
    public MoveNode Root { get; private set; }
    /// <summary>
    /// Gets the current move node in the variation.
    /// </summary>
    public MoveNode CurrentNode { get; private set; }

    /// <summary>
    /// Gets the number of moves in the variation.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets the parent move node of the variation.
    /// </summary>
    public MoveNode Parent { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Variation"/> class with the specified move.
    /// </summary>
    /// <param name="variationMove">The initial move of the variation.</param>
    /// <param name="parent">The parent move node of the variation.</param>
    public Variation(MoveNode variationMove, MoveNode parent) {
        Root = variationMove;
        CurrentNode = variationMove;
        variationMove.Parent = parent;
        Size = 1;
        Parent = parent;
    }

    /// <summary>
    /// Adds a new move to the variation.
    /// </summary>
    /// <param name="newMove">The new move to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the new move is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the new move has the same color as the current move.</exception>
    public void PushMove(MoveNode newMove) {
        if (newMove == null)
            throw new ArgumentNullException(nameof(newMove));

        if (newMove.Move.Piece.Color == CurrentNode.Move.Piece.Color)
            throw new InvalidOperationException("The new move must have a different color than the current move.");

        CurrentNode.Next = newMove;
        newMove.Prev = CurrentNode;
        newMove.Parent = Parent;
        CurrentNode = newMove;
        Size++;
    }

    /// <summary>
    /// Drops the last move from the variation.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when there is only one move in the variation.</exception>
    public void DropLastMove() {
        if (CurrentNode.Prev == null)
            throw new InvalidOperationException("Each variation must contain at least one move.");

        CurrentNode = CurrentNode.Prev;
        CurrentNode.Next = null;
        Size--;
    }

    /// <summary>
    /// Drops the specified number of moves from the end of the variation.
    /// </summary>
    /// <param name="numberOfMoves">The number of moves to drop.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of moves to drop is less than or equal to zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown when there are not enough moves to remove, as each variation must contain at least one move.</exception>
    public void DropLastMoves(int numberOfMoves) {
        if (numberOfMoves <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfMoves), "Number of moves to drop must be greater than zero.");

        if (numberOfMoves >= Size)
            throw new InvalidOperationException("Not enough moves to remove. Each variation must contain at least one move.");

        for (int i = 0; i < numberOfMoves; i++) {
            DropLastMove();
        }
    }

    /// <inheritdoc/>
    public override string ToString() {
        var result = new StringBuilder();
        var currentNode = Root;

        while (currentNode != null) {
            result.Append(currentNode.ToString());
            if (currentNode.Next != null) {
                result.Append(" ");
            }
            currentNode = currentNode.Next;
        }

        return result.ToString();
    }
}
