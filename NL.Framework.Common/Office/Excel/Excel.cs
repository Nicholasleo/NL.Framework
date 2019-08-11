//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-11 16:08:53
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Log;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Common.Office.Excel
{
    public class Excel : IExcel
    {
        private readonly ILogger _ILogger;
        public Excel(ILogger logger)
        {
            this._ILogger = logger;
        }
        public void ExcprtToExcel(DataTable dt, string filename)
        {
            throw new NotImplementedException();
        }

        public void ExcprtToExcel(DataTable dt, string filename, string sheetname)
        {
            throw new NotImplementedException();
        }

        public void ExportToExcel(DataSet ds, string filename)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSetFromExcel(string filename)
        {
            throw new NotImplementedException();
        }

        public DataTable GetTableFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            try
            {
                FileStream fs = File.OpenRead(filename);
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                fs.Close();
                ISheet sheet = workbook.GetSheetAt(0);//默认读取第一个sheet
                IRow row = sheet.GetRow(0);//读取表头
                IRow tempRow = sheet.GetRow(1);//读取Excel内容用于获取列的数据类型
                //创建DataTable
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = row.GetCell(j).ToString();
                    dc.DataType = GetCellValue(tempRow.GetCell(j)).GetType();
                    dc.Caption = row.GetCell(j).ToString();
                    dt.Columns.Add(dc);
                }
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row != null)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //SetCellValue(row.GetCell(j), dataRow[j]);
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                _ILogger.Error("获取excel数据异常GetTableFromExcel", ex);
                throw;
            }
            return dt;
        }

        public DataTable GetTableFromExcel(string filename, bool hasMapField)
        {
            if (!hasMapField)
                return GetTableFromExcel(filename);
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            try
            {
                FileStream fs = File.OpenRead(filename);
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                fs.Close();
                ISheet sheet = workbook.GetSheetAt(0);//默认读取第一个sheet
                IRow row = sheet.GetRow(1);//读取表头
                IRow tempRow = sheet.GetRow(2);//读取Excel内容用于获取列的数据类型
                //创建DataTable
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = row.GetCell(j).ToString();
                    dc.DataType = GetCellValue(tempRow.GetCell(j)).GetType();
                    dc.Caption = row.GetCell(j).ToString();
                    dt.Columns.Add(dc);
                }
                for (int i = 2; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row != null)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //SetCellValue(row.GetCell(j), dataRow[j]);
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                _ILogger.Error("获取excel数据异常GetTableFromExcel", ex);
                throw;
            }
            return dt;
        }

        public DataTable GetTableFromExcel(string filename, string sheetname)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            try
            {
                FileStream fs = File.OpenRead(filename);
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                fs.Close();
                ISheet sheet = workbook.GetSheet(sheetname);//默认读取第一个sheet
                IRow row = sheet.GetRow(0);//读取表头
                IRow tempRow = sheet.GetRow(1);//读取Excel内容用于获取列的数据类型
                //创建DataTable
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = row.GetCell(j).ToString();
                    dc.DataType = GetCellValue(tempRow.GetCell(j)).GetType();
                    dc.Caption = row.GetCell(j).ToString();
                    dt.Columns.Add(dc);
                }
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row != null)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //SetCellValue(row.GetCell(j), dataRow[j]);
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                _ILogger.Error("获取excel数据异常GetTableFromExcel", ex);
                throw;
            }
            return dt;
        }

        public DataTable GetTableFromExcel(string filename, string sheetname, bool hasMapField)
        {
            if (!hasMapField)
                return GetTableFromExcel(filename, sheetname);
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            try
            {
                FileStream fs = File.OpenRead(filename);
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                fs.Close();
                ISheet sheet = workbook.GetSheet(sheetname);//默认读取第一个sheet
                IRow row = sheet.GetRow(1);//读取表头
                IRow tempRow = sheet.GetRow(2);//读取Excel内容用于获取列的数据类型
                //创建DataTable
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = row.GetCell(j).ToString();
                    dc.DataType = GetCellValue(tempRow.GetCell(j)).GetType();
                    dc.Caption = row.GetCell(j).ToString();
                    dt.Columns.Add(dc);
                }
                for (int i = 2; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row != null)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //SetCellValue(row.GetCell(j), dataRow[j]);
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                _ILogger.Error("获取excel数据异常GetTableFromExcel", ex);
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 获取cell的数据，并设置为对应的数据类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private object GetCellValue(ICell cell)
        {
            object value = null;
            try
            {
                if (cell.CellType != CellType.Blank)
                {
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            // Date comes here
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = cell.DateCellValue;
                            }
                            else
                            {
                                // Numeric type
                                value = cell.NumericCellValue;
                            }
                            break;
                        case CellType.Boolean:
                            // Boolean type
                            value = cell.BooleanCellValue;
                            break;
                        case CellType.Formula:
                            value = cell.CellFormula;
                            break;
                        default:
                            // String type
                            value = cell.StringCellValue;
                            break;
                    }
                }
                else
                {
                    value = cell.StringCellValue;
                }
            }
            catch (Exception)
            {
                value = "";
            }

            return value;
        }

        /// <summary>
        /// 根据数据类型设置不同类型的cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="obj"></param>
        private void SetCellValue(ICell cell, object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                cell.SetCellValue((int)obj);
            }
            else if (obj.GetType() == typeof(double))
            {
                cell.SetCellValue((double)obj);
            }
            else if (obj.GetType() == typeof(IRichTextString))
            {
                cell.SetCellValue((IRichTextString)obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                cell.SetCellValue((DateTime)obj);
            }
            else if (obj.GetType() == typeof(bool))
            {
                cell.SetCellValue((bool)obj);
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }
        }
    }
}
