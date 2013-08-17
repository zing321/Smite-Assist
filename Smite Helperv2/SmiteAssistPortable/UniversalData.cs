using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace SmiteAssistPortable
{
    public static class UniversalData
    {
        public static bool loading { get; private set; }
        public static bool loaded { get; private set; }
        public static List<ParseObject> godsList { get; private set; }
        public static List<ParseObject> itemsList { get; private set; }
        public static async void loadData(StreamReader godStream, StreamReader itemStream)
        {
            if (godStream == null || itemStream == null)
            {
                try
                {
                    loading = true;
                    loaded = false;
                    ParseQuery<ParseObject> query = new ParseQuery<ParseObject>("Gods");
                    IEnumerable<ParseObject> result = await query.FindAsync();
                    godsList = result.ToList<ParseObject>();
                    ParseQuery<ParseObject> query2 = new ParseQuery<ParseObject>("Items");
                    query2 = query2.Limit(500);
                    result = await query2.FindAsync();
                    itemsList = result.ToList<ParseObject>();
                    loading = false;
                    loaded = true;
                }
                catch (Exception e)
                {
                    loading = false;
                    return;
                }
                onReady(EventArgs.Empty);
            }
        }
        public static event EventHandler ready;
        private static void onReady(EventArgs e)
        {
            if (loaded)
                ready(null, e);
        }


        public static List<Dictionary<string, object>> parseListToDictionaryList(List<ParseObject> list)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            List<string> keys = list[0].Keys.ToList<string>();

            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
                for (int j = 0; j < keys.Count; j++)
                {
                    dictionaryItem.Add(keys[j], list[i][keys[j]]);
                }
                result.Add(dictionaryItem);
            }
            return result;
        }

        public static async Task<Dictionary<string, object>> getPlayer(string playerName)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"player", playerName}
                };
            List<object> result = await ParseCloud.CallFunctionAsync<List<object>>("getPlayer", parameter);
            return (Dictionary<string,object>)result[0];
        }

        public static async Task<List<object>> getMatchHistory(string playerName)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"player", playerName}
                };
            List<object> result = await ParseCloud.CallFunctionAsync<List<object>>("getMatchHistory", parameter);
            return result;
        }

        //matchId comes from getMatchHistory's "Match" key
        public static async Task<List<object>> getMatchDetails(long matchId)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"matchId", matchId}
                };
            List<object> result = await ParseCloud.CallFunctionAsync<List<object>>("getMatchDetails", parameter);
            return result;
        }

//        The queueId for getqueuestats use the following values:
//        423 	Conquest 5v5
//        424 	Novice
//        426 	Conquest
//        427 	Practice
//        429 	Challenge
//        430 	Ranked
//        431	Joust
//        433 	Domination
//        434   Match of the Day
//        435 	Arena
//        438 	Arena Challenge
//        439 	Domination Challenge
//        440   Joust Queued
//        441   Renaissance Joust Challenge
//        442   Conquest Team Ranked
//        445	Assault
//        446	AssaultChallenge
//        449	JoustRankedSolo
//        450	JoustRanked3v3
        public static async Task<List<object>> getQueueStats(string playerName, int queueId)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"player", playerName},
                    {"queue", queueId}
                };
            List<object> result = await ParseCloud.CallFunctionAsync<List<object>>("getQueueStats", parameter);
            return result;
        }

        //errors, do not use! there is some issue with the api's gettopranked method.
        public static async Task<List<object>> getTopRanked()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            List<object> result = await ParseCloud.CallFunctionAsync<List<object>>("getTopRanked", parameter);
            return result;
        }
    }
}
