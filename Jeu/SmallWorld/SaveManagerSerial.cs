using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmallWorld
{
    public class SaveManagerSerial : SaveManager, ISaveManagerSerial
    {

        IFormatter _formatter;
        Stream _stream;
        GameManager _g;

        public SaveManagerSerial()
        {
            _formatter = new BinaryFormatter();
            _g = GameManager.Instance;
        }

        public override Tuple<int, int, int> getGameState()
        {
            return (Tuple<int, int, int>)_formatter.Deserialize(_stream);
        }
        public override Tuple<int, int, List<int>> getMapData()
        {
            return (Tuple<int, int, List<int>>)_formatter.Deserialize(_stream);
        }

        public override Tuple<string, string, int> getPlayerData()
        {
            return (Tuple<string, string, int>)_formatter.Deserialize(_stream);
        }
        public override Player getPlayers()
        {
            return (Player)_formatter.Deserialize(_stream);
        }
        public override List<Unit> getUnits(int numPlayer)
        {
            return (List<Unit>)_formatter.Deserialize(_stream);
        }
        public override void load()
        {
            _stream = new FileStream(SaveManager.saveFolder + FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
        }

        public override void save()
        {
            _stream = new FileStream(SaveManager.saveFolder + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //_formatter.Serialize(_stream, GameManager.Instance);
            saveMap();
            savePlayer();
            //saveUnit();
            saveGameManagerState();
            _stream.Close();
            
        }


        public void saveGameManagerState() 
        {
            Tuple<int, int, int> state = Tuple.Create<int, int, int>(_g.TurnNumber, _g.TurnCurrent, _g.PlayerTurn);
            _formatter.Serialize(_stream, state);
        }
        protected override void saveMap()
        {
            Tuple<int, int, List<int>> map = Tuple.Create<int, int, List<int>>( _g.Map.SizeX, _g.Map.SizeY, _g.Map.TilesList );
            _formatter.Serialize(_stream, map);
        }

        protected override void savePlayer()
        {
            _formatter.Serialize(_stream, _g.Players[0]);
            _formatter.Serialize(_stream, _g.Players[1]);
        }

        protected override void saveUnit()
        {
            _formatter.Serialize(_stream, _g.Players[0].UnitList);
            _formatter.Serialize(_stream, _g.Players[1].UnitList);
        }
        public override void end()
        {
            _stream.Close();
        }

    }
}
