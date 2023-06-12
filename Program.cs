using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

public class TsTable
{
    public int No { get; set; }
    public List<string> LocNo { get; set; }
}

class Program
{
    static void Main()
    {
        // TsTableクラスのインスタンスを2つ作成します
        TsTable tsTable1 = new TsTable
        {
            No = 1,
            LocNo = new List<string> { "Loc1", "Loc2", "Loc3", "Loc4", "Loc5" }
        };

        TsTable tsTable2 = new TsTable
        {
            No = 2,
            LocNo = new List<string> { "Loc3", "Loc4", "Loc5", "Loc6", "Loc7" }
        };

        // TsTableクラスのインスタンスをリストに追加します
        List<TsTable> tsTableList = new List<TsTable> { tsTable1, tsTable2 };

        // 重複なしのリストを作成し、ソートします
        List<string> mrglist = tsTable1.LocNo.Union(tsTable2.LocNo).OrderBy(x => x).ToList();

        // XLWorkbookを取得します
        var workbook = ExportToExcel(mrglist, tsTableList);

        // Excelファイルに出力します
        workbook.SaveAs("output.xlsx");
    }

    static XLWorkbook ExportToExcel(List<string> mrglist, List<TsTable> tsTableList)
    {
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        for (int i = 0; i < mrglist.Count; i++)
        {
            for (int j = 0; j < tsTableList.Count; j++)
            {
                worksheet.Cell(j + 1, i + 2).Value = tsTableList[j].LocNo.Contains(mrglist[i]) ? mrglist[i] : "";
            }
        }

        return workbook;
    }
}
