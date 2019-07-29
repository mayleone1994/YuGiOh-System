using UnityEngine;
using System;
using System.Data;
using Mono.Data.SqliteClient;
using System.Collections.Generic;
using YuGiOhManager;

public class DBConnection : MonoBehaviour {

	public int idFromOponnentTable;

	void Start(){

		StartConn (DeckManager.deck, 1);

		StartConn (DeckManager.deckAI, idFromOponnentTable);

		InitializeGame.StartGame ();
	}

	void StartConn (Stack<CardData> deck, int playerID) {

		string conn = "URI=file:" + Application.dataPath + "/Plugins/YGO.db"; 
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); 
		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "SELECT Cards.id, Cards.name, Cards.atk, Cards.def, Cards.img FROM Cards INNER JOIN AddCard ON AddCard.id_card = Cards.id INNER JOIN Players ON AddCard.id_player = Players.id WHERE Players.id = "
			+ playerID.ToString();
		
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();

		while (reader.Read ()) {

			CardData _card = new CardData ();

			_card.Id =  Convert.ToInt32 (reader ["id"]);
			_card.Atk = Convert.ToInt32 (reader ["atk"]);
			_card.Def = Convert.ToInt32 (reader ["def"]);
			_card.Name = reader ["name"].ToString ();

			var tex = new Texture2D (200, 280);
			byte[] imageBLOB = (byte[]) reader ["img"];
			tex.LoadImage (imageBLOB);
			_card.imageUI = tex;

			deck.Push (_card);
		}

		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}
		
}
