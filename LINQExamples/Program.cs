using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            GameObjects _gobjs = new GameObjects();

            // Lab Instruction
            foreach (var item in _gobjs.Collectables)
            {
                if(item.selected)
                {
                    Console.WriteLine(" Selected {0} with value {1}", item.id, item.val);
                }

                else
                {
                    Console.WriteLine(" Not Selected {0} with value {1}", item.id, item.val);
                }
            }

           
            var selected = _gobjs.Collectables
                                 .Where(s => s.selected == true);
            foreach (var item in selected)
                Console.WriteLine("Collected {0}", item.ToString());

            var orderedSelected = _gobjs.Collectables
                                        .Where(s => s.selected == false)
                                        .OrderByDescending(s => s.val);
            foreach (var item in orderedSelected)
                Console.WriteLine("Ordered descending Collected {0}", item.ToString());

            var _playerXPDetails = _gobjs.players
                .Select(p => new {
                                    Name = String.Concat(p.firstName, " ", p.sceondName),
                                    PlayerXP = p.XP });

            foreach (var pxp in _playerXPDetails)
                Console.WriteLine("Player XP is {0}", 
                    String.Concat(pxp.Name, " ", pxp.PlayerXP.ToString()));


            var playerScores = (from p in _gobjs.players
                                join s in _gobjs.scores
                                on p.playerId equals s.PlayerID
                                join g in _gobjs.games
                                on  s.GameID equals g.GameID
                                select new { Game = g.GameName,
                                            Name = String.Concat(p.firstName, " ", p.sceondName),
                                             PlayerScore = s.score}).
                                             OrderByDescending(o => o.PlayerScore);

            foreach (var item in playerScores)
                Console.WriteLine("Player Score for {0} ", String.Concat("Game name ", item.Game," ",item.Name," score ", item.PlayerScore.ToString())  );

            // Exercises
            #region EX 1
            // Select all scores greater than 500.
            var greaterThan500 = playerScores.Where(f => f.PlayerScore > 500);

            // Display
            foreach(var player in greaterThan500)
            {
                Console.WriteLine("\nABOVE 500: {0} with a score of {1}", player.Name, player.PlayerScore);
            }
            #endregion

            #region EX 2
            // Select all XP values greater than 250.
            var XPgreaterThan200 = _playerXPDetails.Where(f => f.PlayerXP > 250);

            // Display
            foreach (var XPplayer in XPgreaterThan200)
            {
                Console.WriteLine("\nABOVE 250 XP: {0} with an XP value of {1}", XPplayer.Name, XPplayer.PlayerXP);
            }
            #endregion

            #region EX 3 Attempt.
            //var selectedPlayer = _gobjs.players.Where(
            //    p => p.GamerTag == "Twinny"
            //    );

            //foreach (var sp in selectedPlayer)
            //{
            //    Console.WriteLine("{0} Found", sp.GamerTag);
            //}

            #endregion

            #region EX 3 Corrected. (& EX 4).
            // Find Twinny. FirstOrDefault is used instead of Where when you know what you are looking for is unique.
            Player twinny = _gobjs.players.FirstOrDefault(p => p.GamerTag == "Twinny");

            // Display
            if (twinny != null)
            {
                Console.WriteLine("\n{0} found", twinny.ToString());
            }

            // Exercise 4: Get Twinny's scores.
            var twinnyScores = _gobjs.scores.Where(s => s.PlayerID == twinny.playerId);

            foreach (var item in twinnyScores)
            {
                Console.WriteLine("\n{0}'s score is {1}", twinny.GamerTag, item.ToString());
            }

            #endregion

            #region EX 5 
            // Select all players that have a score greater than 300.
            var scoresForPlayersOver300 = (from p in _gobjs.players
                                    join s in _gobjs.scores
                                    on p.playerId equals s.PlayerID
                                    where s.score > 300
                                    select new { player = p.GamerTag,
                                        playerScore = s.score });

            // Display
            foreach(var item in scoresForPlayersOver300)
            {
                Console.WriteLine("\n {0} {1}", item.player, item.playerScore);
            }
            #endregion

            //foreach (var game in _gobjs.games)
            //    Console.WriteLine("Game is {0} ", game.ToString());
            //foreach (var player in _gobjs.players)
            //    Console.WriteLine("Player is {0}", player.ToString());
            //foreach (var score in _gobjs.scores)
            //    Console.WriteLine("Score is {0}", score.ToString());

            Console.ReadKey();
        }
    }
}
