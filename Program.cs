Board board = new Board();
Player p1 = new Player(Celltype.X);
Player p2 = new Player(Celltype.Y);
TicTacGame game = new TicTacGame(board,p1,p2);
game.gamerun();

public class TicTacGame
{
    public Board _board { get; }
    private Player P1;
    private Player P2;
    public Player current { get; private set; }
    public TicTacGame(Board board, Player p1 , Player p2)
    {
        _board = board;
        P1 = p1;
        P2 = p2;
        current = P1;
    }
    
    public void gamerun()
    {

        while (!iswon())
        {
            _board.draw(this);
            Location location = current.getlocation(_board);
            _board.setcell(location.X, location.Y, current.Celltype);
            current = current == P1 ? P2 : P1;
        }

    }

    public bool iswon()
    {
        if (won(_board , Celltype.X)) return true;
        if (won(_board , Celltype.Y)) return true;
        if (draw(_board)) return true;
        return false;
    }

    public bool draw(Board board)
    {
        for(int row =0; row<3;row++)
            for (int col =0; col<3;col++)
                if(board.getcell(row,col) == Celltype._) return false;
        return true;
    }
    public bool won(Board board , Celltype celltype)
    {
        if (aresame(celltype ,board.getcell(0,0) , board.getcell(0,1) , board.getcell(0,2))) return true;
        if (aresame(celltype, board.getcell(1,0) , board.getcell(1, 1), board.getcell(1, 2))) return true;
        if (aresame(celltype, board.getcell(2, 0), board.getcell(2, 1), board.getcell(2, 2))) return true;
        
        if (aresame(celltype, board.getcell(0, 0), board.getcell(1, 0), board.getcell(2, 0))) return true;
        if (aresame(celltype, board.getcell(0, 1), board.getcell(1, 1), board.getcell(2, 1))) return true;
        if (aresame(celltype, board.getcell(0, 2), board.getcell(2 , 1), board.getcell(2, 2))) return true;

        if (aresame(celltype, board.getcell(0, 0), board.getcell(1, 1), board.getcell(2, 2))) return true;
        if (aresame(celltype, board.getcell(0, 2), board.getcell(1, 1), board.getcell(2, 0))) return true;
        return false;
    }
    private bool aresame(Celltype a ,Celltype b , Celltype c ,Celltype d)
    {
        if (a == d && a==b && a==c) return true;
        return false;
    }
}
public class Player
{
    public Celltype Celltype { get; }

    public Player(Celltype celltype)
    {
        Celltype = celltype;
    }


    //private Board _board; pass as constructor as refrence to class      private Board _board; 
    //
    //  public Player(Board board)                                        public player()
    //                                                                    {
    //  {                                                                 _board = new board; for having multiple players for board 
    //      _board = board;
    //  } or like here pass as parameter for method                        } they all know about board not seperate boards
    public Location getlocation(Board board)
    {
        while (true)
        {
            Console.WriteLine("enter your location: "); int loc = Convert.ToInt16(Console.ReadLine());
            Location location = loc switch
            {
                1 => new Location(2, 0),
                2 => new Location(2, 1),
                3 => new Location(2, 2),
                4 => new Location(1, 0),
                5 => new Location(1, 1),
                6 => new Location(1, 2),
                7 => new Location(0, 0),
                8 => new Location(0, 1),
                9 => new Location(0, 2),
            };

            if (!board.isoccupied(location)) return location;

            else Console.WriteLine("not valid try again.");
        }
    }
  

}

public class Location
{
    public int X { get; }
    public int Y { get; }

    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Board
{
    private Celltype[,] cells = new Celltype[3, 3];


    public Celltype getcell(int row, int column)
    {
        return cells[row, column];
    }
    public bool setcell(int row, int column, Celltype value)
    {
        if (cells[row,column] != Celltype._) return false;
        cells[row, column] = value;
        return true;
    }
    public bool isoccupied(Location location)
    {
       return cells[location.X, location.Y] != Celltype._;
    }
    public void draw(TicTacGame game)
    {
        
        Console.WriteLine($"it is the turn of {tochar(game.current.Celltype)}");
        Console.WriteLine($"{tochar(cells[0, 0])} | {tochar(cells[0, 1])} | {tochar(cells[0, 2])} ");
        Console.WriteLine("---------");
        Console.WriteLine($"{tochar(cells[1, 0])} | {tochar(cells[1, 1])} | {tochar(cells[1, 2])} ");
        Console.WriteLine("---------");
        Console.WriteLine($"{tochar(cells[2, 0])} | {tochar(cells[2, 1])} | {tochar(cells[2, 2])} ");
    }
 
    private char tochar(Celltype celltype)
    {
        return celltype switch
        {
            Celltype._ => ' ',
            Celltype.X => 'X',
            Celltype.Y => 'Y',
        };
    }
}

public enum Celltype { _,X, Y,}