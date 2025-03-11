using System.Collections;

namespace ngnchess.MoveDataStructure;

/// <summary>
/// Represents a collection of variation lines in a chess game.
/// </summary>
public partial class VariationLines : IEnumerable<Variation> {
    /// <summary>
    /// Gets the parent move node of the variation lines.
    /// </summary>
    public MoveNode Parent { get; init; }
    private List<Variation> Lines { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VariationLines"/> class with the specified parent move node.
    /// </summary>
    /// <param name="parent">The parent move node.</param>
    public VariationLines(MoveNode parent) {
        Parent = parent;
        Lines = new List<Variation>();
    }

    /// <summary>
    /// Adds a new variation line starting with the specified move.
    /// </summary>
    /// <param name="move">The move to start the new variation line.</param>
    /// <exception cref="ArgumentNullException">Thrown when the move is null.</exception>
    public void AddVariationLine(MoveNode move) {
        if (move == null) throw new ArgumentNullException(nameof(move));

        Lines.Add(new Variation(move, Parent));
    }

    /// <summary>
    /// Removes a variation line at the specified index.
    /// </summary>
    /// <param name="variationIndex">The index of the variation line to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the variation index is out of range.</exception>
    public void RemoveVariationLine(int variationIndex) {
        if (Math.Abs(variationIndex) > Lines.Count())
            throw new ArgumentOutOfRangeException(nameof(variationIndex));

        Lines.RemoveAt(variationIndex);
    }

    /// <summary>
    /// Gets the variation line at the specified index.
    /// </summary>
    /// <param name="variationIndex">The index of the variation line to get.</param>
    /// <returns>The variation line at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the variation index is out of range.</exception>
    public Variation GetVariationLine(int variationIndex) {
        if (Math.Abs(variationIndex) > Lines.Count())
            throw new ArgumentOutOfRangeException(nameof(variationIndex));

        return Lines[variationIndex];
    }

    /// <inheritdoc/>
    public IEnumerator<Variation> GetEnumerator() {
        return Lines.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
