using System.Text;

namespace ngnchess.MoveDataStructure;

/// <summary>
/// Represents a tree structure to manage chess moves.
/// </summary>
public class MoveTree {
    /// <summary>
    /// Gets the root node of the move tree.
    /// </summary>
    public MoveNode? Root { get; private set; } = null;

    /// <summary>
    /// Gets the current node of the move tree.
    /// </summary>
    public MoveNode? CurrentNode { get; private set; } = null;

    /// <summary>
    /// Gets the size of the move tree.
    /// </summary>
    public int Size { get; private set; } = 0;


    /// <summary>
    /// Appends a new move to the current sequence of moves.
    /// The new move must have a different color than the current move.
    /// </summary>
    /// <param name="newMove">The new move to append.</param>
    /// <exception cref="ArgumentNullException">Thrown when the new move is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the new move has the same color as the current move.</exception>
    public void PushMove(MoveNode newMove) {
        if (newMove == null)
            throw new ArgumentNullException(nameof(newMove));

        if (CurrentNode == null) {
            Root = newMove;
            CurrentNode = newMove;
            Size = 1;
        } else {
            if (newMove.Color == CurrentNode.Color)
                throw new InvalidOperationException("The new move must have a different color than the current move.");

            CurrentNode.Next = newMove;
            newMove.Prev = CurrentNode;
            CurrentNode = newMove;
            Size++;
        }
    }

    /// <summary>
    /// Removes the last move from the current sequence of moves.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the tree is empty.</exception>
    public void DropLastMove() {
        if (CurrentNode == null)
            throw new InvalidOperationException("Empty tree.");

        if (CurrentNode.Prev != null) {
            CurrentNode.Prev.Next = null;
            CurrentNode = CurrentNode.Prev;
            Size--;
        } else {
            Root = null;
            CurrentNode = null;
            Size = 0;
        }
    }

    /// <summary>
    /// Removes the specified number of moves from the current sequence of moves.
    /// </summary>
    /// <param name="numberOfMoves">The number of moves to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of moves to remove is less than 1.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the tree does not contain enough moves to remove.</exception>
    public void DropLastMoves(int numberOfMoves) {
        if (numberOfMoves < 1)
            throw new ArgumentOutOfRangeException(nameof(numberOfMoves), "The number of moves to remove must be at least 1.");

        if (numberOfMoves > Size)
            throw new InvalidOperationException("Not enough moves to remove.");

        for (int i = 0; i < numberOfMoves; i++) {
            DropLastMove();
        }
    }

    /// <summary>
    /// Returns a string that represents the current state of the move tree.
    /// </summary>
    /// <returns>A string representation of the move tree.</returns>
    public override string ToString() {
        var sb = new StringBuilder();
        BuildString(sb, Root, 0);
        return sb.ToString();
    }

    /// <summary>
    /// Builds a string representation of the move tree.
    /// </summary>
    /// <param name="sb">The StringBuilder to append to.</param>
    /// <param name="node">The current move node.</param>
    /// <param name="indent">The current indentation level.</param>
    private void BuildString(StringBuilder sb, MoveNode? node, int indent) {
        if (node == null)
            return;

        sb.AppendLine(new string(' ', indent * 4) + node.ToString());

        var variations = node.Variations;
        foreach (var variation in variations) {
            sb.AppendLine(new string(' ', (indent + 1) * 4) + "[Variation]");
            sb.AppendLine(variation.ToString());
        }

        BuildString(sb, node.Next, indent);
    }
}

