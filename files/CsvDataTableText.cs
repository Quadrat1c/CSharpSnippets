/*
 * C# Snippet Code
 */

 // CSV File to DataTable
 // Example Path: @"C:\Documents\myFile.csv"
private static DataTable CsvToDataTable(string path, bool isFirstRowHeader)
{
    string header = isFirstRowHeader ? "Yes" : "No";
    string pathOnly = Path.GetDirectoryName(path);
    string fileName = Path.GetFileName(path);

    string sql = @"SELECT * FROM [" + fileName + "]";

    using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
        ";Extended Properties=\"Text;HDR=" + header + "\""))
    {
        using (OleDbCommand cmd = new OleDbCommand(sql, con))
        {
            using (OleDbDataAdapter ad = new OleDbDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                dt.Locale = CultureInfo.CurrentCulture;
                ad.Fill(dt);
                return dt;
            }
        }
    }
}

// DataTable to CSV
private static void DataTableToCsv(DataTable dt)
{
    StreamWriter sw = new StreamWriter("file.csv", false);
    for (int i = 0; i < dt.Columns.Count; i++)
    {
        sw.Write(dt.Columns[i]);
        if (i < dt.Columns.Count - 1)
        {
            sw.Write(",");
        }
    }
    sw.Write(sw.NewLine);
    foreach (DataRow dr in dt.Rows)
    {
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (!Convert.IsDBNull(dr[i]))
            {
                string value = dr[i].ToString();
                if (value.Contains(","))
                {
                    value = String.Format("\"{0}\"", value);
                    sw.Write(value);
                }
                else if (value.Contains(".000000"))
                {
                    sw.Write(dr[i].ToString().Replace(".000000", ""));
                }
                else
                {
                    sw.Write(dr[i].ToString());
                }
            }
            if (i < dt.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
    }
    sw.Close();
}

 // Text document to IEnumerable<string>
 private static IEnumerable<string> GetFileContents()
 {
     string fileName = @"C:\Documents\MyFile.txt";
     IEnumerable<string> fileTextLines = File.ReadLines(fileName);
     return fileTextLines;
 }

 // IEnumerable to List
 IEnumerable<string> test = GetFileContents();
 List<string> testList = test.ToList();
