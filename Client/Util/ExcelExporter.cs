using Data.Model.Diagram;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Client.Util {
    public class ExcelExporter {
        Application oApp;
        Workbook oWorkbook;
        Worksheet oWorksheet;

        public String TempFile { get; set; }
        private string fileName = Constants.EXCEL_EXPORT_NAME;
        public String FileName {
            get {
                return this.fileName;
            }
            set {
                this.fileName = value;
            }
        }

        #region FieldIndexes

        int nameCellIndex = 0;
        int valueCellIndex = 0;
        int barCodeCellIndex = 0;
        int articleGroupNameCellIndex = 0;
        int articleGroupBarCodeCellIndex = 0;
        int supplierNameCellIndex = 0;
        int roomNameCellIndex = 0;
        int roomPathCellIndex = 0;
        int depreciationValueCellIndex = 0;

        int nameRowIndex = 0;
        int valueRowIndex = 0;
        int barCodeRowIndex = 0;
        int articleGroupNameRowIndex = 0;
        int articleGroupBarCodeRowIndex = 0;
        int supplierNameRowIndex = 0;
        int roomNameRowIndex = 0;
        int roomPathRowIndex = 0;
        int depreciationValueRowIndex = 0;

        #endregion

        public ExcelExporter(String path) {
            handleExcelFile(path);
        }

        private void handleExcelFile(String path) {
            this.oApp = new Application();
            try {
                this.TempFile = Path.GetTempPath() + FileName;
                oWorkbook = oApp.Workbooks.Open(path);

                //Copy template
                oWorkbook.SaveCopyAs(this.TempFile);
                oWorkbook.Close();

                //open template
                oWorkbook = oApp.Workbooks.Open(this.TempFile);
                oWorksheet = oWorkbook.Worksheets[1];

            } catch (Exception e) {
                CloseExcel();
                throw new Exception(e.Message);
            }
        }

        public void CloseExcel() {
            if (oApp.ActiveWorkbook != null) {
                oApp.ActiveWorkbook.Close(false);
            }
            oApp.Quit();
            oApp = null;
        }

        public void DataBind() {
            if (this.DataSource != null && this.oWorksheet != null) {

                //Get first empty row 
                Range rowRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, 1];
                long lastRow = (long)rowRange.get_End(XlDirection.xlUp).Row;
                long newRow = lastRow + 1;

                foreach (Range row in oWorksheet.UsedRange.Rows) {
                    int cellIndex = 1;
                    foreach (Range cell in row.Cells) {
                        String cellValue = cell.Value2 != null ? cell.Value2.ToString() : "";
                        switch (cellValue) {
                            case "&=Name":
                                nameCellIndex = cellIndex;
                                nameRowIndex = row.Row;
                                break;
                            case "&=Value":
                                valueCellIndex = cellIndex;
                                valueRowIndex = row.Row;
                                break;
                            case "&=Barcode":
                                barCodeCellIndex = cellIndex;
                                barCodeRowIndex = row.Row;
                                break;
                            case "&=ArticleGroup.Name":
                                articleGroupNameCellIndex = cellIndex;
                                articleGroupNameRowIndex = row.Row;
                                break;
                            case "&=ArticleGroup.Barcode":
                                articleGroupBarCodeCellIndex = cellIndex;
                                articleGroupBarCodeRowIndex = row.Row;
                                break;
                            case "&=Supplier.Name":
                                supplierNameCellIndex = cellIndex;
                                supplierNameRowIndex = row.Row;
                                break;
                            case "&=Room.Name":
                                roomNameCellIndex = cellIndex;
                                roomNameRowIndex = row.Row;
                                break;
                            case "&=Room.Path":
                                roomPathCellIndex = cellIndex;
                                roomPathRowIndex = row.Row;
                                break;
                            case "&=Depreciation.Value":
                                depreciationValueCellIndex = cellIndex;
                                depreciationValueRowIndex = row.Row;
                                break;
                        }
                        cellIndex++;
                    }
                }

                foreach (Article article in this.DataSource) {
                    //Name
                    if (nameCellIndex > 0)
                        oWorksheet.Cells[nameRowIndex++, nameCellIndex] = article.Name;
                    //Value
                    if (valueCellIndex > 0)
                        oWorksheet.Cells[valueRowIndex++, valueCellIndex] = article.Value;
                    //BarCode
                    if (barCodeCellIndex > 0)
                        oWorksheet.Cells[barCodeRowIndex++, barCodeCellIndex] = article.Barcode;
                    //Article.Name
                    if (articleGroupNameCellIndex > 0)
                        oWorksheet.Cells[articleGroupNameRowIndex++, articleGroupNameCellIndex] = article.ArticleGroup != null ? article.ArticleGroup.Name : "";
                    //Article.Barcode
                    if (articleGroupBarCodeCellIndex > 0)
                        oWorksheet.Cells[articleGroupBarCodeRowIndex++, articleGroupBarCodeCellIndex] = article.ArticleGroup != null ? article.ArticleGroup.Barcode : "";
                    //Supplier.Name
                    if (supplierNameCellIndex > 0)
                        oWorksheet.Cells[supplierNameRowIndex++, supplierNameCellIndex] = article.SupplierBranch != null ? article.SupplierBranch.Supplier.Name : "";
                    //Room.Naem
                    if (roomNameCellIndex > 0)
                        oWorksheet.Cells[roomNameRowIndex++, roomNameCellIndex] = article.Room != null ? article.Room.Name : "";
                    //Room.Path
                    if (roomPathCellIndex > 0)
                        oWorksheet.Cells[roomPathRowIndex++, roomPathCellIndex] = article.Room != null ? article.Room.RoomPath : "";
                    //Depreciation.Value
                    if (depreciationValueCellIndex > 0)
                        oWorksheet.Cells[depreciationValueRowIndex++, depreciationValueCellIndex] = article.Depreciation != null ? article.DepreciationValue.HasValue ? article.DepreciationValue.Value.ToString() : "" : "";
                }

                //applyStyles();

                this.oWorkbook.Save();

                this.CloseExcel();
            }
        }

        private void applyStyles() {
            Range nameRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, nameCellIndex];
            Range valueRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, valueCellIndex];
            Range barCodeRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, barCodeCellIndex];
            Range depreciationRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, depreciationValueCellIndex];
            Range groupBarCodeRange = (Range)oWorksheet.Cells[oWorksheet.Rows.Count, articleGroupBarCodeCellIndex];

            nameRange.NumberFormat = "@";
            valueRange.NumberFormat = "0.00";
            barCodeRange.NumberFormat = "@";
            depreciationRange.NumberFormat = "0.00";
            groupBarCodeRange.NumberFormat = "0.00";
        }

        #region Properties

        public List<Article> DataSource { get; set; }

        #endregion

    }
}