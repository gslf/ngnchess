using System.ComponentModel;

namespace ngnchess.Models.Enum;

/// <summary>
/// Represents annotations or metadata associated with a chess move.
/// This class is intended to define and categorize various types of move annotations,
/// such as special conditions, comments, or classifications used in chess engines or games.
/// </summary>
public enum MoveAnnotation {
    [Description("??")]
    BLUNDER,

    [Description("?")]
    MISTAKE,

    [Description("\"?!\"")]
    INACCURACY,

    [Description("!")]
    GOOD,

    [Description("!!")]
    BRILIANT 
}
