
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataUpload {
    class ArticleImporter {
        #region properties

        private const int PROGRESS_STEP = 500;
        private const int MEMORY_CLEAR_STEP = 500;
        private string sourceFile;
        private int year;
        private int month;
        private LogBox log;

        private List<DepreciationCategory> depCategories;
        private List<ArticleCategory> articleCategories;
        private List<InsuranceCategory> insuranceCategories;
        private List<Room> rooms;
        private List<Floor> floors;
        private List<Building> buildings;
        private List<Supplier> suppliers;
        private List<SupplierBranch> branches;

        private List<string[]> source;
        public List<string[]> Source {
            get {
                if (source == null) {
                    List<string[]> dataMap = new List<string[]>();
                    StringBuilder sb = new StringBuilder();
                    try {
                        StreamReader sr = new StreamReader(sourceFile, Encoding.Default);
                        int counter = 0;
                        string line;

                        while (!sr.EndOfStream) {
                            //Let title line out
                            if (counter > 0) {
                                line = sr.ReadLine();
                                dataMap.Add(line.Split(';'));
                                sb.Append(line + "<br/><br/>");
                            }
                            counter++;
                        }
                        sr.Close();
                        source = dataMap;
                    } catch {
                        log.insert("Cannot read the file! Close all excel files!");
                    }
                }
                return source;
            }
        }

        #endregion

        public ArticleImporter(Form1 window) {
            this.source = null;
            this.log = (LogBox)window;
        }

        public ArticleImporter(string sourceFile, Form1 window) {
            this.source = null;
            this.log = (LogBox)window;
            this.sourceFile = sourceFile;
            cacheData();
        }

        private void cacheData() {
            this.depCategories = DepreciationCategory.GetAll().ToList();
            this.articleCategories = ArticleCategory.GetAll().ToList();
            this.rooms = new List<Room>();
            this.floors = new List<Floor>();
            this.buildings = new List<Building>();
            this.suppliers = new List<Supplier>();
            this.insuranceCategories = InsuranceCategory.GetAll().ToList();
            this.branches = new List<SupplierBranch>();
        }

        public void DeleteAll() {
            log.clear();
            log.insert("Deleting Data");

            using (var context = new IP3AnlagenInventarEntities()) {
                // delete existing records
                context.Database.ExecuteSqlCommand("DELETE Article");
                context.Database.ExecuteSqlCommand("DELETE ArticleGroup");
                context.Database.ExecuteSqlCommand("DELETE SupplierBranch");
                context.Database.ExecuteSqlCommand("DELETE Supplier");
                context.Database.ExecuteSqlCommand("DELETE Room");
                context.Database.ExecuteSqlCommand("DELETE Floor");
                context.Database.ExecuteSqlCommand("DELETE Building");
            }
            
            EntityFactory.Context.SaveChanges();
            log.insert("Deleted all data");
        }

        public void Save() {
            
            log.clear();
            log.insert("Starting upload2");

            List<string[]> dataMap = this.Source;
            log.insert("[INFO] Reading data...");

            log.insert("[INFO] Preparing data");

            //GO THROUGH THE DATA SOURCE
            if (dataMap != null) {
                for (int i = 1; i <= dataMap.Count -1 ; i++) {

                    String floorName = dataMap.ElementAt(i)[0]; //
                    String buildingName = dataMap.ElementAt(i)[1]; //
                    String roomName = dataMap.ElementAt(i)[2]; //
                    String responsible = dataMap.ElementAt(i)[3]; //

                    bool insured = dataMap.ElementAt(i)[21] != "0" ? true : false;

                    //Set responsible name as email if it is a x.yyyy pattern
                    if (responsible != null) {
                        responsible = responsible.Replace(" ", String.Empty);
                        String[] respSplit = responsible.Split('.');
                        if (respSplit.Any() && respSplit[0].Count() == 1) {
                            responsible.Replace(" ", string.Empty);
                            responsible += "@bsl.lan";
                        }
                    }

                    int amount = dataMap.ElementAt(i)[6] != null && dataMap.ElementAt(i)[6] != "NULL" ? int.Parse(dataMap.ElementAt(i)[6]) : 0; //

                    String reason = dataMap.ElementAt(i)[7];

                    String articleName = dataMap.ElementAt(i)[12];      //
                    String comment = dataMap.ElementAt(i)[13];          //
                    String articleValue = dataMap.ElementAt(i)[15];     //
                    String acquisitionYear = dataMap.ElementAt(i)[22];
                    String oldbarcode = dataMap.ElementAt(i)[4] +" | "+ dataMap.ElementAt(i)[5];
                    DateTime acquisitionDate = new DateTime(acquisitionYear != null && acquisitionYear != "0" && acquisitionYear!="NULL" ? int.Parse(acquisitionYear) : 1900, 1, 1); //

                    String articleCategoryName = dataMap.ElementAt(i)[24]; 
                    String supplierName = dataMap.ElementAt(i)[25];
                    String depreciationCategory = dataMap.ElementAt(i)[26]; //

                    //Do barcode preperations
                    String groupBarCode = LatestBarCode.GenerateFullBarCode();
                    LatestBarCode.Set(groupBarCode);

                    String barCodeCounterPart = groupBarCode != "" && groupBarCode != null ? groupBarCode.Split('.')[groupBarCode.Split('.').Length - 1] : null;
                    int barCode = barCodeCounterPart != "" && barCodeCounterPart != null ? int.Parse(barCodeCounterPart) : -1;
                    int barCodeDigits = barCode > -1 ? barCode.ToString().Length : -1;

                    for (int a = 0; a <= amount-1; a++) {
                        Article articleToCreate = new Article();
                        articleToCreate.Name = articleName;
                        articleToCreate.Value = articleValue != null && articleValue != "NULL" ? double.Parse(articleValue) : 0;
                        articleToCreate.Comment = comment;
                        articleToCreate.AcquisitionDate = acquisitionDate;
                        articleToCreate.IsAvailable = true;
                        articleToCreate.OldBarcode = oldbarcode;


                          DepreciationCategory depCategory = depCategories.Where(c => c.Name == depreciationCategory).SingleOrDefault();
                          if (depCategory == null) {
                              articleToCreate.DepreciationCategory = depCategories.Where(d => d.Name == "Keine Abschreibung").SingleOrDefault();
                          } else {
                              articleToCreate.DepreciationCategory = depCategory;
                          }

                        //Barcode
                        barCode++;
                        //Handle Barcode
                        String newBarCodeEnd = barCodeCounterPart.Remove(barCodeDigits.ToString().Length - 1, barCode.ToString().Length) + barCode;

                        //Override articles barcode
                        articleToCreate.Barcode = groupBarCode.Remove((groupBarCode.Length) - (barCodeCounterPart.Length), barCodeCounterPart.Length)
                            + newBarCodeEnd;

                        //Raum zeugs
                        if (roomName != null) {
                            Room articleRoom = this.rooms.Where(r => r != null && r.Name == roomName 
                                && r.Floor != null && r.Floor.Name == floorName 
                                && r.Floor.Building != null && r.Floor.Building.Name == buildingName).SingleOrDefault();
                            if (articleRoom == null) {
                                articleRoom = new Room();
                                articleRoom.Name = roomName;
                                articleRoom.ResponsiblePerson = responsible;

                                Floor roomFloor = this.floors.Where(f => f.Name == floorName && f.Building != null && f.Building.Name == buildingName).SingleOrDefault();
                                if (roomFloor == null) {
                                    roomFloor = new Floor();
                                    roomFloor.Name = floorName;

                                    Building roomFloorBuilding = this.buildings.Where(b => b.Name == buildingName).SingleOrDefault();
                                    if (roomFloorBuilding == null) {
                                        roomFloorBuilding = new Building();
                                        roomFloorBuilding.Name = buildingName;
                                        this.buildings.Add(roomFloorBuilding);
                                    }

                                    roomFloor.Building = roomFloorBuilding;

                                    this.floors.Add(roomFloor);

                                }

                                articleRoom.Floor = roomFloor;
                                this.rooms.Add(articleRoom);
                                //Add to context
                            }
                            articleToCreate.Room = articleRoom;
                        }

                        if (supplierName != null) {
                            Supplier supplier = this.suppliers.Where(s => s.Name == supplierName).SingleOrDefault();
                            if (supplier == null){
                                supplier = new Supplier();
                                supplier.Name = supplierName;
                                this.suppliers.Add(supplier);

                                SupplierBranch branch = new SupplierBranch();
                                branch.Place = "*unbekannt*";
                                branch.ZipCode = "0000";
                                branch.Supplier = supplier;
                                this.branches.Add(branch);
                            }
                            articleToCreate.SupplierBranch = this.branches.Where(b => b.Supplier.Name == supplierName).SingleOrDefault();
                        }

                        if (insured) {
                            articleToCreate.InsuranceCategory = this.insuranceCategories.Where(c => c.Name == "Mobiliarversichert").SingleOrDefault();
                        } 

                        EntityFactory.Context.Articles.Add(articleToCreate);
                        log.insert("[Created][Article] " + articleName);
                        Thread.Sleep(50);

                        if (i % PROGRESS_STEP == 0) {
                            try {
                                EntityFactory.Context.SaveChanges();
                                log.insert("[SAVING] Saved " + PROGRESS_STEP + " rows");
                                Thread.Sleep(100);
                            } catch (Exception ex) {
                                log.insert("[ERROR] Cannot save!");
                            }
                        }
                    }
                }
                try {
                    EntityFactory.Context.SaveChanges();
                    log.insert("[SAVING] Saved all data");
                } catch (Exception ex) {
                    log.insert("[ERROR] Cannot save!");
                }
            }
            
        }

    }
}
