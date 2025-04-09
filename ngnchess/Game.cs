using ngnchess.Components;
using ngnchess.Engine;
using ngnchess.MoveDataStructure;

namespace ngnchess;

public class Game {
    public IEngine Engine { get; init; }
    public Board Board { get; init; }
    public MoveTree Moves { get; init; }

    public PieceColor NextMove { get; private set; }
    public List<CastlingRights> CastlingRights { get; private set; }
    public Move? EnPassantAvailable { get; private set; }

    public int HalfMoveClock { get; private set; }
    public int FullMoveClock { get; private set; }

    public Game(IEngine engine) {
        Engine = engine;
        Board = new Board();
        Board.SetupStandardPosition();
        Moves = new MoveTree();
        NextMove = PieceColor.White;
        HalfMoveClock = 0;
        FullMoveClock = 0;  
    }

    // TODO: Implement a constructor to load a game from a PGN string
    // TODO: Implement a constructor to load a game from a FEN string

    public void MakeMove(Move move) {
        // TODO Game.MakeMove()
        throw new NotImplementedException();
    }

    public void MakeVariationMove(Move move) {
        // TODO Game.MakeMove()
        throw new NotImplementedException();
    }

    public void Undo() {
        // TODO Game.MakeMove()
        throw new NotImplementedException();
    }

}
