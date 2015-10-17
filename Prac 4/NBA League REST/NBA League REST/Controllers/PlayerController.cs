using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NBA_League_REST.Models;
using System.IO;
using System.Web;

namespace NBA_League_REST.Controllers
{
    public class PlayerController : ApiController
    {
        List<Player> listPlayers = new List<Player>();

        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"players.txt";
        private string finalPathname = path + file;

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

        public IHttpActionResult GetPlayer(string id)
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
                    Console.WriteLine(listPlayers);

                    if (arrayPlayers[0].Equals(id))
                    {
                        Player Player = new Player();

                        Player.RegistrationID = arrayPlayers[0];
                        Player.First_Name = arrayPlayers[1];
                        Player.Last_Name = arrayPlayers[2];
                        Player.Team_Name = arrayPlayers[3];
                        Player.DOB = DateTime.Parse(arrayPlayers[4]);

                        listPlayers.Add(Player);
                    }
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }

                //if (getPlayer == null)
                //{
                //    return NotFound();
                //}

            //switch (field)
            //{
            //    case "ID":
            //        foreach (var player in listPlayers)
            //        {
            //            if (player.RegistrationID.Equals(value))
            //            {
            //                listPlayers.Add(player);
            //            }
            //        }
            //        break;
            //    case "Name":
            //        break;
            //}

            return Ok(listPlayers);
        }

        // GET: api/Player
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Player/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Player
        public void Post([FromBody]string value)
        {
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
