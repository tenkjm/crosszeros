using AutoMapper;
using CrossZeros.Models;
using CrossZeros.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrossZeros.Controllers.Api
{
   
    public class CrossZerosController: Controller
    {
        IGameProcessor _gameProcessor;
        UserManager<CrossZeroUser> _userManager;

        public CrossZerosController(IGameProcessor gameProcessor, UserManager<CrossZeroUser> userManager)
        {
            this._gameProcessor = gameProcessor;
            this._userManager = userManager;
        }
        [Authorize]
        [Route("api/crosszeros/newgame")]
        public JsonResult CreateNewGame( string oponentId)
        {
            if(_gameProcessor.StartNewGameWithUser(
                _userManager.Users
                .Where(x => x.UserName == User.Identity.Name).FirstOrDefault(),
                _userManager.Users
                .Where(x => x.Id == oponentId).FirstOrDefault()))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("New Game Created");
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Json("Cannot create new game");
        }

        [Authorize]
        [Route("api/crosszeros/mygames")]
        public JsonResult GetAllMyGames()
        {
           IEnumerable<Game> games = _gameProcessor.GetAllMyGames(_userManager.Users
                                     .Where(x => x.UserName == User.Identity.Name)
                                     .FirstOrDefault());
             return Json(Mapper.Map<IEnumerable<GameViewModel>>(games));
        }

        [Authorize]
        [Route("api/crosszeros/move")]
        public JsonResult MakeMove(int row, int column, int GameId)
        {
            if ( _gameProcessor.IsFieldValid(row)&& 
                _gameProcessor.IsFieldValid(column)&&
                _gameProcessor.MakeMove(row, column, GameId, _userManager.Users
                .Where(x => x.UserName == User.Identity.Name).FirstOrDefault()))
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Move Is  ok");
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Json("Move Is  bad");
        }

        [Authorize]
        [Route("api/crosszeros/userlist")]
        public JsonResult GetUserList()
        {
            var results = _gameProcessor.GetAllUsers();
            if (results == null)
            {
                return Json(null);
            }
            return Json(results.Select(
                x => new UserViewModel { id = x.Id, userName = x.UserName }));
        }

        [Authorize]
        [Route("api/crosszeros/chekwin")]
        public JsonResult CheckWin(int GameId)
        {           
            return Json(_gameProcessor.CheckWin(GameId));
        }
    }
}
