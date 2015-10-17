using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NBA_League_REST.Models;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace NBA_League_REST.Controllers
{
    public class PlayerController : ApiController
    {
        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"players.txt";
        private string finalPathname = path + file;

        List<Player> listPlayers = new List<Player>();

        // GET: api/Player
        public IHttpActionResult GetAllPlayers()
        {
            String[] readerPlayers = ReadLines().ToArray();
            String[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerPlayers.GetLength(0); i++)
            {
                String[] arrayPlayers = readerPlayers[i].Split(delimiters,
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(Players => Players.Trim())
                    .ToArray();

                Console.WriteLine(arrayPlayers);

                try
                {
                    Player Player = new Player();

                    Player.RegistrationID = arrayPlayers[0];
                    Player.First_Name = arrayPlayers[1];
                    Player.Last_Name = arrayPlayers[2];
                    Player.Team_Name = arrayPlayers[3];
                    Player.DOB = DateTime.Parse(arrayPlayers[4]);

                    listPlayers.Add(Player);
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }

            return Ok(listPlayers);
        }

        // GET: api/Player/{type}/{input}
        [Route("api/player/{type}/{input}")]
        public IHttpActionResult GetPlayer(string type, string input)
        {
            String[] readerPlayers = ReadLines().ToArray();
            String[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerPlayers.GetLength(0); i++)
            {
                String[] arrayPlayers = readerPlayers[i].Split(delimiters,
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(Players => Players.Trim())
                    .ToArray();

                switch (type)
                {
                    case "ID":
                        if (arrayPlayers[0].Equals(input))
                        {
                            try
                            {
                                Player Player = new Player();

                                Player.RegistrationID = arrayPlayers[0];
                                Player.First_Name = arrayPlayers[1];
                                Player.Last_Name = arrayPlayers[2];
                                Player.Team_Name = arrayPlayers[3];
                                Player.DOB = DateTime.Parse(arrayPlayers[4]);

                                listPlayers.Add(Player);
                            }
                            catch (Exception Ex)
                            {
                                throw new Exception(Ex.Message);
                            }
                        }
                        break;
                    case "Name":
                        if (arrayPlayers[1].ToLower().Contains(input.ToLower()) || arrayPlayers[2].ToLower().Contains(input.ToLower()))
                        {
                            try
                            {
                                Player Player = new Player();

                                Player.RegistrationID = arrayPlayers[0];
                                Player.First_Name = arrayPlayers[1];
                                Player.Last_Name = arrayPlayers[2];
                                Player.Team_Name = arrayPlayers[3];
                                Player.DOB = DateTime.Parse(arrayPlayers[4]);

                                listPlayers.Add(Player);
                            }
                            catch (Exception Ex)
                            {
                                throw new Exception(Ex.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return Ok(listPlayers);
        }

        // POST: api/Player
        public IHttpActionResult PlayerRegistration(String[] newPlayer)
        {
            return Ok(listPlayers);
        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }

        public IEnumerable<String> ReadLines()
        {
            StreamReader readerBooks;
            string line;

            try
            {
                readerBooks = new StreamReader(finalPathname);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            while ((line = readerBooks.ReadLine()) != null)
            {
                yield return line;
                Console.WriteLine(line);
            }
            readerBooks.Close();
        }
    }
}
