using ngnchess.Components;
using System.Text;

namespace ngnchess.FEN;

/// <summary>
/// Adapter that enables conversion between Board and FEN representations of a chessboard.
/// Implements the Adapter pattern to integrate the FEN system with the Board system.
/// </summary>
public static class FENBoardAdapter {
        /// <summary>
        /// Converts a Board object to a FEN string and vice-versa.
        /// </summary>
        /// <param name="board">The chessboard to convert.</param>
        /// <param name="activeColor">Active color ('w' for white, 'b' for black). Default 'w'.</param>
        /// <param name="castlingAvailability">Castling availability (e.g., "KQkq"). Default "-".</param>
        /// <param name="enPassantTarget">En passant target square in algebraic notation. Default "-".</param>
        /// <param name="halfMoveClock">Half-move clock (50-move rule). Default 0.</param>
        /// <param name="fullMoveNumber">Full move number. Default 1.</param>
        /// <returns>The FEN string representing the chessboard state.</returns>
        public static string BoardToFEN(
            Board board,
            char activeColor = 'w',
            string castlingAvailability = "-",
            string enPassantTarget = "-",
            int halfMoveClock = 0,
            int fullMoveNumber = 1) {
            StringBuilder fen = new StringBuilder();

            // Generate the position part
            for (int row = 0; row < 8; row++) {
                int emptyCount = 0;

                for (int col = 0; col < 8; col++) {
                    Piece? piece = board.GetPiece(row, col);

                    if (piece == null) {
                        emptyCount++;
                    } else {
                        // If there were empty squares before this piece, add the count
                        if (emptyCount > 0) {
                            fen.Append(emptyCount);
                            emptyCount = 0;
                        }

                        char pieceChar = PieceToFENChar(piece.Value);
                        fen.Append(pieceChar);
                    }
                }

                // If the row ends with empty squares, add the count
                if (emptyCount > 0) {
                    fen.Append(emptyCount);
                }

                // Add the row separator, except for the last row
                if (row < 7) {
                    fen.Append('/');
                }
            }

            // Complete the FEN string with the other data
            fen.Append($" {activeColor} {castlingAvailability} {enPassantTarget} {halfMoveClock} {fullMoveNumber}");

            return fen.ToString();
        }

        /// <summary>
        /// Converts a FEN string to a Board object.
        /// </summary>
        /// <param name="fen">The FEN string to convert.</param>
        /// <returns>A Board object representing the chessboard specified by the FEN string.</returns>
        /// <exception cref="ArgumentException">Thrown if the FEN string is invalid.</exception>
        public static Board FENToBoard(string fen) {
            FENHandler fenHandler;
            try {
                fenHandler = new FENHandler(fen);
            } catch (ArgumentException) {
                throw new ArgumentException("Invalid FEN string", nameof(fen));
            }

            Board board = new Board();
            board.Clear();

            string boardPosition = fenHandler.GetBoardPosition();
            string[] ranks = boardPosition.Split('/');

            for (int row = 0; row < 8; row++) {
                int col = 0;
                foreach (char c in ranks[row]) {
                    if (char.IsDigit(c)) {
                        // Empty squares
                        col += (c - '0');
                    } else {
                        // Piece on the board
                        Piece piece = FENCharToPiece(c);
                        board.SetPiece(row, col, piece);
                        col++;
                    }
                }
            }

            return board;
        }

        /// <summary>
        /// Converts a piece to a FEN character.
        /// </summary>
        /// <param name="piece">The piece to convert.</param>
        /// <returns>The FEN character corresponding to the piece.</returns>
        private static char PieceToFENChar(Piece piece) {
            char pieceChar = piece.Type switch {
                PieceType.Pawn => 'p',
                PieceType.Rook => 'r',
                PieceType.Knight => 'n',
                PieceType.Bishop => 'b',
                PieceType.Queen => 'q',
                PieceType.King => 'k',
                _ => throw new ArgumentException("Invalid piece type")
            };

            // White pieces are uppercase in FEN notation
            if (piece.Color == PieceColor.White) {
                pieceChar = char.ToUpper(pieceChar);
            }

            return pieceChar;
        }

        /// <summary>
        /// Converts a FEN character to a piece.
        /// </summary>
        /// <param name="fenChar">The FEN character to convert.</param>
        /// <returns>The piece corresponding to the FEN character.</returns>
        /// <exception cref="ArgumentException">Thrown if the FEN character is invalid.</exception>
        private static Piece FENCharToPiece(char fenChar) {
            PieceColor color = char.IsUpper(fenChar) ? PieceColor.White : PieceColor.Black;
            char lowerChar = char.ToLower(fenChar);

            PieceType type = lowerChar switch {
                'p' => PieceType.Pawn,
                'r' => PieceType.Rook,
                'n' => PieceType.Knight,
                'b' => PieceType.Bishop,
                'q' => PieceType.Queen,
                'k' => PieceType.King,
                _ => throw new ArgumentException($"Invalid FEN character: {fenChar}")
            };

            return new Piece(type, color);
        }
    }

