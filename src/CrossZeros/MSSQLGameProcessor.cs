using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossZeros.Models;

namespace CrossZeros
{
    public enum WhoWins
    {
        NoOne,
        Cross,
        Zeros
    }

    public class MSSQLGameProcessor : IGameProcessor
    {
        CrossZeroContext _context;

        public MSSQLGameProcessor(CrossZeroContext context)
        {
            _context = context;
        }
        public int[][] GetDigitsFields(string fieldsSet)
        {
            String[] fields = fieldsSet.Split('|');
            int[][] digitFields = new int[3][];
            digitFields[0] = new int[3];
            digitFields[1] = new int[3];
            digitFields[1] = new int[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    digitFields[i][j] = int.Parse(fields[i * 3 + j]);
                }
            }
            return digitFields;
        }

        public string GetStringFromFields(int[][] fields)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Append(fields[i][j]+"|");
                }
            }
            return sb.ToString().TrimEnd('|');
        }

        public WhoWins CheckWin(int gameId)
        {
            Game game = _context.Games.Where(x => x.Id == gameId).FirstOrDefault();           
            int[][] digitFields = GetDigitsFields(_context.GameStates.Where(x => x.Id == game.GameStateID).FirstOrDefault().Fields);

            for (int i = 0; i < 3; i++)
            {
                if(digitFields[i][0]==digitFields[i][1]
                    && digitFields[i][1] == digitFields[i][2]
                    && digitFields[i][2] != 2)
                {
                    if (digitFields[i][2] == 0)
                    {
                        game.isFinished = true;
                        _context.SaveChanges();
                        return WhoWins.Zeros;
                    }
                     else
                    {
                        game.isFinished = true;
                        _context.SaveChanges();
                        return WhoWins.Cross;
                    }   
                }

                if (digitFields[0][i] == digitFields[1][i]
                    && digitFields[1][i] == digitFields[2][i]
                    && digitFields[2][i] != 2)
                {
                    if (digitFields[2][i] == 0)
                    {
                        game.isFinished = true;
                        _context.SaveChanges();
                        return WhoWins.Zeros;
                    }
                    else
                    {
                        game.isFinished = true;
                        _context.SaveChanges();
                        return WhoWins.Cross;
                    }
                }
            }
            if (digitFields[0][0] == digitFields[1][1]
                    && digitFields[1][1] == digitFields[2][2]
                    && digitFields[2][2] != 2)
            {
                if (digitFields[2][2] == 0)
                {
                    game.isFinished = true;
                    _context.SaveChanges();
                    return WhoWins.Zeros;
                }
                else
                {
                    game.isFinished = true;
                    _context.SaveChanges();
                    return WhoWins.Cross;
                }
            }
            if (digitFields[2][0] == digitFields[1][1]
                    && digitFields[1][1] == digitFields[0][2]
                    && digitFields[0][2] != 2)
            {
                if (digitFields[0][2] == 0)
                {
                    game.isFinished = true;
                    _context.SaveChanges();
                    return WhoWins.Zeros;
                }
                else
                {
                    game.isFinished = true;
                    _context.SaveChanges();
                    return WhoWins.Cross;
                }
            }
            return WhoWins.NoOne;
        }

        public IEnumerable<CrossZeroUser> GetAllUsers()
        {
            return _context.Users;
        }

        public bool MakeMove(int row, int column, int GameId , CrossZeroUser user)
        {
            Game game = _context.Games.Where(i => i.Id == GameId).FirstOrDefault();
            int[][] digitFields = GetDigitsFields(_context.GameStates.Where(x => x.Id == game.GameStateID).FirstOrDefault().Fields);

            if(game.isCrossTurn )
            {
                if( game.UserCrossId == user.Id)
                {
                    digitFields[row][column] = 1;
                    _context.GameStates.Where(x => x.Id == game.GameStateID).FirstOrDefault().Fields = GetStringFromFields(digitFields);
                    game.isCrossTurn = false;
                    return _context.SaveChanges() > 1;
                }            
            }
            else
            {
                if (game.UserZeroId == user.Id)
                {
                    digitFields[row][column] = 0;
                    _context.GameStates.Where(x => x.Id == game.GameStateID).FirstOrDefault().Fields = GetStringFromFields(digitFields);
                    game.isCrossTurn = true;
                    return _context.SaveChanges() > 1;
                }
            }
            return false;
        }

        public bool StartNewGameWithUser(CrossZeroUser me, CrossZeroUser oponent)
        {
            if(oponent==null)
            {
                return false;
            }
            if(_context.Games.Where(x=>x.UserCrossId==me.Id&&
                                    x.UserZeroId==oponent.Id&&
                                    x.isFinished==true).FirstOrDefault()!=null)
            {
                return false;
            }


            GameState gs = _context.GameStates.Add(new GameState
            {
                Fields = "2|2|2|2|2|2|2|2|2"
            }).Entity;
            Game g= _context.Games.Add(new Game()
            {
                Created = DateTime.Now,
                isCrossTurn = true,
                isFinished = false,
                UserCrossId = me.Id,
                UserZeroId = oponent.Id,
                 GameStateID =gs.Id
            }).Entity;

            

            return _context.SaveChanges() > 0;
        }

        public bool IsFieldValid(int fildNum)
        {
            if(fildNum<3&&fildNum>0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Game> GetAllMyGames(CrossZeroUser user)
        {
            return _context.Games.Where(x=>x.UserCrossId==user.Id||x.UserZeroId==user.Id);
        }
    }
}
