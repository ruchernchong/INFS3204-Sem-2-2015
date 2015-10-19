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
using System.Globalization;

namespace NBA_League_REST.Controllers
{
    public class PlayerController : ApiController
    {
        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"players.txt";
        private string finalPathname = path + file;
        private string tempFile = Path.GetTempFileName();

        List<Player> listPlayers = new List<Player>();

        // GET: api/Player
        public IHttpActionResult Get_AllPlayers()
        {
            String[] readerPlayers = this.ReadLines().ToArray();
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
        public IHttpActionResult Get_Player(string type, string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new NullReferenceException("Input is empty.");
            }
            else
            {
                String[] readerPlayers = this.ReadLines().ToArray();
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

                    var RegistrationID = arrayPlayers[0];
                    var First_Name = arrayPlayers[1];
                    var Last_Name = arrayPlayers[2];

                    switch (type)
                    {
                        case "ID":
                            if (RegistrationID.Equals(input))
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

                                    return Ok(listPlayers);
                                }
                                catch (Exception Ex)
                                {
                                    throw new Exception(Ex.Message);
                                }
                            }
                            break;
                        case "Name":
                            if (First_Name.ToLower().Contains(input.ToLower()) || Last_Name.ToLower().Contains(input.ToLower()))
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

                                    return Ok(listPlayers);
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
                return NotFound();
            }
        }

        // POST: api/Player
        [HttpPost]
        public IHttpActionResult Post_Player(Player getPlayer)
        {
            Debug.WriteLine(getPlayer);
            Player thisPlayer = new Player();
            bool isPlayerNew = true;

            try
            {
                //String[] readerPlayers = ReadLines().ToArray();
                String[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

                //for (int i = 0; i < readerPlayers.GetLength(0); i++)
                //{
                //    String[] arrayPlayers = readerPlayers[i].Split(delimiters,
                //        StringSplitOptions.RemoveEmptyEntries)
                //        .Select(Player => Player.Trim())
                //        .ToArray();

                //    var RegistrationID = arrayPlayers[0];
                //    var First_Name = arrayPlayers[1];
                //    var Last_Name = arrayPlayers[2];
                //    var Team_Name = arrayPlayers[3];
                //    var DOB = arrayPlayers[4];
                using (StreamReader readerPlayers = new StreamReader(finalPathname)) {
                    string line;
                    while ((line = readerPlayers.ReadLine()) != null)
                    {
                        String[] arrayPlayers = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        var RegistrationID = arrayPlayers[0];

                        if (RegistrationID == getPlayer.RegistrationID)
                        {
                            isPlayerNew = false;

                            readerPlayers.Close();

                            this.DeleteThisPlayer("ID", RegistrationID);
                            this.CreateThisPlayer(getPlayer);

                            return Get_AllPlayers();
                        }
                    }
                    readerPlayers.Close();
                }

                if (isPlayerNew)
                {
                    bool isPlayerCreated = CreateThisPlayer(getPlayer);

                    return Get_AllPlayers();
                }
                return NotFound();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        // PUT: api/Player/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Player/5
        [Route("api/player/{type}/{input}")]
        public IHttpActionResult Delete_Player(string type, string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new NullReferenceException("Input is empty.");
            }
            else
            {
                bool PlayerDeleted = this.DeleteThisPlayer(type, input);

                if (PlayerDeleted)
                {

                }
                return this.Get_AllPlayers();
            }
        }

        private Boolean CreateThisPlayer(Player newPlayer)
        {
            Debug.WriteLine(newPlayer);

            try
            {
                using (StreamWriter writerBooks = new StreamWriter(finalPathname, true))
                {
                    writerBooks.WriteLineAsync(String.Format(
                        "{0},{1},{2},{3},{4:yyyy-MM-dd}",
                        newPlayer.RegistrationID,
                        newPlayer.First_Name,
                        newPlayer.Last_Name,
                        newPlayer.Team_Name,
                        newPlayer.DOB
                        ));

                    writerBooks.Close();

                    return true;
                }
            }
            catch (Exception Ex)
            {
                return false;
                throw new Exception(Ex.Message);
            }
        }

        public Boolean DeleteThisPlayer(string type, string input)
        {
            try
            {
                using (StreamReader readerPlayers = new StreamReader(finalPathname))
                {
                    string line;
                    bool isDeleted = false;

                    List<String> playerLines = new List<String>();
                    String[] delimiters = {
                                         ",",
                                         "\r\n"
                                     };

                    while ((line = readerPlayers.ReadLine()) != null)
                    {
                        String[] arrayPlayers = line.Split(delimiters,
                        StringSplitOptions.RemoveEmptyEntries);

                        var RegistrationID = arrayPlayers[0];
                        var First_Name = arrayPlayers[1];
                        var Last_Name = arrayPlayers[2];

                        switch (type)
                        {
                            case "ID":
                                if (RegistrationID.Equals(input))
                                {
                                    isDeleted = true;
                                }
                                else
                                {
                                    playerLines.Add(line);
                                }
                                break;

                            case "Name":
                                try
                                {
                                    if (First_Name.ToLower().Equals(input.ToLower()) || Last_Name.ToLower().Equals(input.ToLower()))
                                    {
                                        isDeleted = true;
                                    }
                                    else
                                    {
                                        playerLines.Add(line);
                                    }
                                    break;
                                }
                                catch (FormatException thisFormatException)
                                {
                                    throw new FormatException(thisFormatException.Message);
                                }
                            default:
                                return false;
                        }
                    }
                    readerPlayers.Close();

                    if (isDeleted)
                    {
                        try
                        {
                            using (StreamWriter writerPlayers = new StreamWriter(tempFile))
                            {
                                foreach (string playerLine in playerLines)
                                {
                                    writerPlayers.WriteLine(playerLine);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new Exception(Ex.Message);
                        }

                        this.deleteFile(); //Call deleteFile() method;
                    }
                    return false;
                }
            }
            catch (FormatException thisFormatException)
            {
                throw new FormatException(thisFormatException.Message);
            }
        }

        private void deleteFile()
        {
            try
            {
                if (File.Exists(finalPathname))
                {
                    File.Delete(finalPathname);
                    File.Move(tempFile, finalPathname);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        private IEnumerable<String> ReadLines()
        {
            StreamReader readerPlayers;
            string line;

            try
            {
                readerPlayers = new StreamReader(finalPathname);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            while ((line = readerPlayers.ReadLine()) != null)
            {
                yield return line;
                Console.WriteLine(line);
            }
            readerPlayers.Close();
        }
    }
}