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
        public override Player[] Players { get; protected set; }
        public override Tuple<int, int, int> GMState { get; protected set; }
        public override Tuple<int, int, List<int>> Map { get; protected set; }

        public SaveManagerSerial()
        {
            _formatter = new BinaryFormatter();
            _g = GameManager.Instance;
            Players = null;
            GMState = null;
            Map = null;
        }
        public override void load()
        {
            _stream = new FileStream(SaveManager.saveFolder + FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Map = (Tuple<int, int, List<int>>)_formatter.Deserialize(_stream);
            Players = (Player[])_formatter.Deserialize(_stream);
            GMState = (Tuple<int, int, int>)_formatter.Deserialize(_stream);
            _stream.Close();
        }

        public override void save()
        {
            _stream = new FileStream(SaveManager.saveFolder + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //_formatter.Serialize(_stream, GameManager.Instance);
            _formatter.Serialize(_stream, Tuple.Create<int, int, List<int>>(_g.Map.SizeX, _g.Map.SizeY, _g.Map.TilesList));
            _formatter.Serialize(_stream, _g.Players);
            _formatter.Serialize(_stream, Tuple.Create<int, int, int>(_g.TurnNumber, _g.TurnCurrent, _g.PlayerTurn));         
        }

        public override void end()
        {

        }

    }
}
