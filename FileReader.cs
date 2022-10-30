namespace ConsoleApp
{
  class FileReader
  {
    public static string ReadFhirInputFile(string filename)
    {
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{filename}");
      FileStream stream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{filename}"), FileMode.Open, FileAccess.Read);
      using (StreamReader reader = new StreamReader(stream))
      {
        string result = reader.ReadToEnd();
        stream.Close();
        return result;
      };
    }
  }
}