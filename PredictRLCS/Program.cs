using System.Text.Json;
using PredictRLCS;

static List<GameData.Match> LoadData()
{
    var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\matches_test.json"));
    var matches = JsonSerializer.Deserialize<List<GameData.Match>>(json);

    if (matches == null)
    {
        throw new Exception("Deserialization failed.");
    }

    return matches;
}

static void DisplayRatings()
{

}

var matches = LoadData();
var simulator = new Simulator(matches);
simulator.Simulate();
DisplayRatings();
