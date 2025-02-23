using APIWithFireBase.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
public class FirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _firebaseUrl;
    private readonly string _authToken;

    public FirebaseService(HttpClient httpClient, IOptions<FirebaseSettings> settings)
    {
        _httpClient = httpClient;
        _firebaseUrl = settings.Value.BaseUrl;
        _authToken = settings.Value.AuthToken;
    }

    public async Task<string> PostDataAsync<T>(string path, T data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var url = $"{_firebaseUrl}/{path}.json?auth={_authToken}";

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<Dictionary<string, BabyTemperature>> GetAllDataAsync(string path)
    {
        var url = $"{_firebaseUrl}/{path}.json?auth={_authToken}"; // Construct the Firebase URL

        var response = await _httpClient.GetAsync(url); // Send GET request to Firebase
       var success = response.EnsureSuccessStatusCode(); // Ensure the request was successful
        var json = await response.Content.ReadAsStringAsync(); // Get the JSON response

        // Deserialize the JSON response into a dictionary of DataDto objects
        return JsonConvert.DeserializeObject<Dictionary<string, BabyTemperature>>(json);
    }
}
