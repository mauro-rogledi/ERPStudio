#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;

#endregion

using ERPFramework.CounterManager;
using ERPFramework.Controls;
using ERPFramework.Preferences;
using ERPFramework.Forms;
using System.Data.Common;

namespace ERPFramework.Data
{
    #region DBCollection

    public class DBCollection : object
    {
        private string name;

        private SqlProxyDataAdapter dAdapter;
        private List<SqlProxyParameter> dParam;

        public SqlProxyDataAdapter DataAdapter { get { return dAdapter; } }

        public SqlProxyParameter[] Parameter { get { return dParam.ToArray(); } }

        public string Name { get { return name; } }

        public List<DBCollection> List = new List<DBCollection>();

        public bool HasToCreateCommand = true;

        public DBCollection()
        {
        }

        public DBCollection(string myMaster, SqlProxyDataAdapter myAdapter, List<SqlProxyParameter> myParam)
        {
            dAdapter = myAdapter;
            dParam = myParam;
            name = myMaster;
        }

        public void AddMaster(string myMaster, SqlProxyDataAdapter myAdapter, List<SqlProxyParameter> myParam)
        {
            dAdapter = myAdapter;
            dParam = myParam;
            name = myMaster;
        }

        public void AddSlave(string mySlave, SqlProxyDataAdapter myAdapter, List<SqlProxyParameter> myParam)
        {
            var myDbCollection = new DBCollection(mySlave, myAdapter, myParam);
            List.Add(myDbCollection);
        }

        public void AddSlave(string mySlaveSlavable, string mySlave, SqlProxyDataAdapter myAdapter, List<SqlProxyParameter> myParam)
        {
            var myDbCollection = new DBCollection(mySlave, myAdapter, myParam);
            RecursiveAdd(mySlaveSlavable, myDbCollection, this.List);
        }

        private void RecursiveAdd(string mySlaveSlavable, DBCollection myDbCollection, IList myList)
        {
            foreach (DBCollection collection in myList)
            {
                if (collection.name == mySlaveSlavable)
                {
                    collection.List.Add(myDbCollection);
                    break;
                }
                if (collection.List.Count > 0)
                    RecursiveAdd(mySlaveSlavable, myDbCollection, collection.List);
            }
        }
    }

    #endregion

    #region DBManager

    public enum DBPosition { First, Middle, Last }

    public enum DBMode { Edit, Browse, Run, Validating, Find }

    public enum DBLock { Lock, Unlock }

    public enum DBOperation { Get, New, Edit, Delete }

    public abstract class DBManager : IDisposable
    {
        public event EventHandler<RowUpdatingEventArgs> OnBeforeRowUpdating;

        #region private variables

        private DBCollection collectionDB;
        public ProviderType providerType;

        private BindingSource masterBinding;

        private BindingCollection slaveBindingCollection;
        private DBOperation dbOperation = DBOperation.Edit;
        private IRadarParameters lastKey;
        private List<ICounterManager> fiscalControlList;
        private IRadarForm myRadarDocument;
        private IDocument myDocument;
        private GlobalPreferences globalPref;

        #endregion

        #region protected variables

        #endregion

        #region public DataMemmber

        public IColumn ForeignKey { get; set; }

        public DataSet Dataset { get; set; }

        public SqlProxyConnection DBConnection { get; private set; }

        public DBPosition Position { get; private set; } = DBPosition.First;

        public DBMode Status { get; set; } = DBMode.Browse;

        //public Type RadarDocument { get; private set; }
        public object[] RadarParams;

        public int Count { get { return masterBinding.Count; } }

        public int Pos { get { return masterBinding.Position; } }

        public IRadarParameters LastKey { set { lastKey = value; } get { return lastKey; } }

        public SqlProxyTransaction SqlProxyTransaction { get; private set; }


        public bool isChanged
        {
            get
            {
                if (Dataset == null) return false;
                return Dataset.GetChanges() != null;
            }
        }

        public BindingSource MasterBinding { get { return masterBinding; } }

        public BindingSource SlaveBinding(string text)
        {
            return slaveBindingCollection[text];
        }

        //public CurrencyManager		currManager =	 null;
        public bool SilentMode;

        public void BindCounter(ref ICounterManager fiscalnocontrol)
        {
            if (fiscalControlList == null)
                fiscalControlList = new List<ICounterManager>();

            fiscalControlList.Add(fiscalnocontrol);
        }

        public SqlProxyTransaction Transaction { get { return SqlProxyTransaction; } }

        public Type RadarDocument { get; private set; }

        #endregion

        public void AttachRadar<I>(params object[] list)
        {
            RadarDocument = typeof(I);
            RadarParams = list;
        }

        #region Protected virtual method

        protected abstract string CreateMasterQuery(ref List<SqlProxyParameter> dParam);

        protected virtual string CreateSlaveQuery(string name, List<SqlProxyParameter> dParam)
        {
            return string.Empty;
        }

        protected virtual List<SqlProxyParameter> CreateMasterParam()
        {
            return null;
        }

        protected virtual List<SqlProxyParameter> CreateSlaveParam(string name)
        {
            return null;
        }

        protected virtual List<SqlProxyParameter> CreateSlaveParam(string name, string slavename)
        {
            return null;
        }

        protected virtual void dAdapter_MasterRowUpdating(object sender, RowUpdatingEventArgs e)
        {
            if (OnBeforeRowUpdating != null)
                OnBeforeRowUpdating(sender, e);

            if (e.StatementType == StatementType.Insert)
            {
                if (myRadarDocument == null)
                    myRadarDocument = Activator.CreateInstance(RadarDocument, RadarParams) as IRadarForm;

                lastKey = myRadarDocument.GetRadarParameters(e.Row[ForeignKey.Name].ToString());
            }
        }

        protected virtual void dAdapter_MasterRowUpdated(object sender, RowUpdatedEventArgs e)
        {
            // AddOn management
            if (myDocument.AddonList != null)
                foreach (Addon scr in myDocument.AddonList)
                    scr.MasterRowUpdated(this, e);
        }

        protected virtual void dAdapter_RowUpdating(object sender, RowUpdatingEventArgs e)
        {
            // AddOn management
            if (myDocument.AddonList != null)
                foreach (Addon scr in myDocument.AddonList)
                    scr.RowUpdating(this, e);
        }

        protected virtual bool OnEdit()
        {
            return true;
        }

        protected virtual bool OnBeforeAddNew()
        {
            return true;
        }

        protected virtual bool OnAfterAddNew()
        {
            return true;
        }

        protected virtual bool OnBeforeSave()
        {
            return true;
        }

        protected virtual bool OnBeforeDelete()
        {
            return true;
        }

        protected virtual bool OnAfterSave()
        {
            return true;
        }

        protected virtual bool OnAfterDelete()
        {
            return true;
        }
        #endregion

        #region Costructor

        /// <summary>
        /// 9
        /// </summary>
        /// <param name="name"></param>
        /// <param name="conString"></param>
        ///
        protected DBManager(string name, IDocument document) // ProviderType providerType, string conString)
        {
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            this.myDocument = document;
            globalPref = new PreferencesManager<GlobalPreferences>("", null).ReadPreference();

            DBConnection = new SqlProxyConnection();
            Dataset = new DataSet(name)
            {
                Locale = System.Globalization.CultureInfo.InvariantCulture
            };
            collectionDB = new DBCollection();
            slaveBindingCollection = new BindingCollection();
            try
            {
                if (DBConnection.State != ConnectionState.Open)
                    DBConnection.Open();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void Dispose()
        {
            if (DBConnection != null && DBConnection.State == ConnectionState.Open)
                DBConnection.Close();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Add Relaction

        public DataRelation AddRelation(
                                        string relactionName,
                                        IColumn masterColumn,
                                        IColumn slaveColumn
                                        )
        {
            return AddRelation(
                relactionName,
                masterColumn.Tablename, masterColumn.Name,
                slaveColumn.Tablename, slaveColumn.Name, true
            );
        }

        public DataRelation AddRelation(
            string relactionName,
            IColumn masterColumn,
            IColumn slaveColumn,
            bool createConstraint
            )
        {
            return AddRelation(
                relactionName,
                masterColumn.Tablename, masterColumn.Name,
                slaveColumn.Tablename, slaveColumn.Name, createConstraint
            );
        }

        public DataRelation AddRelation(
            string relactionName,
            IColumn[] masterColumn,
            IColumn[] slaveColumn,
            bool createConstraint
            )
        {
            if (Dataset == null) return null;

            System.Data.DataRelation drDB;
            System.Data.DataColumn[] dcM;
            System.Data.DataColumn[] dcS;

            Debug.Assert(masterColumn.Length == slaveColumn.Length, "AddRelation - disambiguous number column");

            dcM = new DataColumn[masterColumn.Length];
            dcS = new DataColumn[slaveColumn.Length];

            for (int t = 0; t < masterColumn.Length; t++)
            {
                dcM[t] = Dataset.Tables[masterColumn[t].Tablename].Columns[masterColumn[t].Name];
                dcS[t] = Dataset.Tables[slaveColumn[t].Tablename].Columns[slaveColumn[t].Name];
            }

            drDB = new System.Data.DataRelation(relactionName, dcM, dcS, createConstraint);

            Dataset.Relations.Add(drDB);
            Dataset.EnforceConstraints = createConstraint;

            AddSlaveBinding(relactionName);

            return drDB;
        }

        /// <summary>
        /// Add a relation between two table
        /// </summary>
        /// <param name="relactionName"></param>
        /// <param name="masterTable"></param>
        /// <param name="masterColumn"></param>
        /// <param name="slaveTable"></param>
        /// <param name="slaveColumn"></param>
        /// <param name="createConstraint">todo: describe createConstraint parameter on AddRelation</param>
        /// <returns></returns>
        public DataRelation AddRelation(
            string relactionName,
            string masterTable, string masterColumn,
            string slaveTable, string slaveColumn,
            bool createConstraint
            )
        {
            if (Dataset == null) return null;

            System.Data.DataRelation drDB;
            var dc1 = new DataColumn();
            var dc2 = new DataColumn();

            // Get the parent and child columns of the two tables.
            dc1 = Dataset.Tables[masterTable].Columns[masterColumn];
            dc2 = Dataset.Tables[slaveTable].Columns[slaveColumn];
            drDB = new System.Data.DataRelation(relactionName, dc1, dc2, createConstraint);
            try
            {
                Dataset.Relations.Add(drDB);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            Dataset.EnforceConstraints = createConstraint;

            AddSlaveBinding(relactionName);

            return drDB;
        }

        #endregion

        #region Add Master & Slave

        /// <summary>
        /// Add a master Table
        /// </summary>
        /// <param name="createCommand"></param>
        /// <returns></returns>
        public SqlProxyDataAdapter AddMaster<T>(bool createCommand = true)
        {
            if (collectionDB == null || Dataset == null) return null;

            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            ForeignKey = typeof(T).GetField("ForeignKey").GetValue(null) as IColumn;

            List<SqlProxyParameter> dParam = null;
            var dCommand = CreateMasterCommand(ref dParam);

            var dAdapter = CreateDataAdapter(tableName, dCommand);
            collectionDB.AddMaster(tableName, dAdapter, dParam);
            AddMasterBinding(tableName);

            dAdapter.RowUpdated += dAdapter_MasterRowUpdated;
            //dAdapter.RowUpdated += new SqlABRowUpdatedEventHandler(dAdapter_MasterRowUpdated);
            //dAdapter.RowUpdating += new SqlABRowUpdatingEventHandler(dAdapter_MasterRowUpdating);

            collectionDB.HasToCreateCommand = createCommand;
            return dAdapter;
        }

        private void DAdapter_RowUpdated(object sender, RowUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual SqlProxyCommand CreateMasterCommand(ref List<SqlProxyParameter> dParam)
        {
            dParam = CreateMasterParam();
            var sqlQuery = CreateMasterQuery(ref dParam);

            var dCommand = new SqlProxyCommand(sqlQuery, DBConnection);
            if (dParam != null)
                dCommand.Parameters.AddRange(dParam);

            return dCommand;
        }

        //private void AddCurrencyManager(Form myform, DataTable myMasterTable)
        //{
        //    if (myform==null) return;

        //    currManager = (CurrencyManager)myform.BindingContext[Dataset.Tables[myMasterTable.TableName]];
        //    bindingManager = myform.BindingContext[Dataset.Tables[myMasterTable.TableName]];
        //}

        private void AddMasterBinding(string tableName)
        {
            Debug.Assert(masterBinding == null);
            masterBinding = new BindingSource(Dataset, tableName)
            {
                AllowNew = true
            };
            masterBinding.BindingComplete += new BindingCompleteEventHandler(masterBinding_BindingComplete);
        }

        private void masterBinding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            MessageBox.Show(e.BindingCompleteState.ToString());
        }

        /// <summary>
        /// Add a slave table
        /// </summary>
        /// <param name="createCommand"></param>
        /// <returns></returns>
        public SqlProxyDataAdapter AddSlave<T>(bool createCommand = true)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            if (collectionDB == null || Dataset == null) return null;

            var dParam = CreateSlaveParam(tableName);
            var sqlQuery = CreateSlaveQuery(tableName, dParam);

            // AddOn management
            if (sqlQuery == string.Empty && myDocument.AddonList != null)
                foreach (Addon scr in myDocument.AddonList)
                {
                    sqlQuery = scr.CreateSlaveQuery(tableName, dParam);
                    if (sqlQuery != string.Empty)
                        break;
                }

            var dCommand = new SqlProxyCommand(sqlQuery, DBConnection);

            if (dParam != null)
                dCommand.Parameters.AddRange(dParam);

            // AddOn management
            if ((dParam == null || dParam.Count == 0) && myDocument.AddonList != null)
                foreach (Addon scr in myDocument.AddonList)
                {
                    dParam = scr.CreateSlaveParam(tableName);
                    if (dParam == null || dParam.Count == 0)
                        continue;
                    dCommand.Parameters.AddRange(dParam);
                    break;
                }

            var dAdapter = CreateDataAdapter(tableName, dCommand);
            collectionDB.AddSlave(tableName, dAdapter, dParam);

            dAdapter.RowUpdating += dAdapter_RowUpdating;
            collectionDB.HasToCreateCommand = createCommand;
            return dAdapter;
        }

        /// <summary>
        /// Add Slave to slave table
        /// </summary>
        /// <param name="slavename"></param>
        /// <param name="createCommand"></param>
        /// <returns></returns>
        public SqlProxyDataAdapter AddSlave<T>(string slavename, bool createCommand = true)
        {
            if (collectionDB == null || Dataset == null) return null;

            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            var dParam = CreateSlaveParam(slavename);

            var sqlQuery = CreateSlaveQuery(tableName, dParam);
            var dCommand = new SqlProxyCommand(sqlQuery, DBConnection);

            if (dParam != null)
                dCommand.Parameters.AddRange(dParam);

            var dAdapter = CreateDataAdapter(tableName, dCommand);
            collectionDB.AddSlave(slavename, tableName, dAdapter, dParam);

            collectionDB.HasToCreateCommand = createCommand;
            return dAdapter;
        }

        public void AddSlaveBinding(string tableName)
        {
            slaveBindingCollection.Add(new BindingSource(masterBinding, tableName)).AllowNew = false;
        }

        #endregion

        #region Moving between Record

        public void MoveFirst()
        {
            if (masterBinding == null) return;

            masterBinding.Position = 0;
            Position = DBPosition.First;
        }

        public void MoveLast()
        {
            if (masterBinding == null) return;

            masterBinding.Position = masterBinding.Count - 1;
            Position = DBPosition.Last;
        }

        public void MovePrevious()
        {
            if (masterBinding == null) return;

            if (masterBinding.Position > 0)
                masterBinding.Position--;

            if (masterBinding.Position == 0)
                Position = DBPosition.First;
            else
                Position = DBPosition.Middle;
        }

        public void MoveNext()
        {
            if (masterBinding == null) return;

            Position = DBPosition.Middle;

            if (masterBinding.Position < masterBinding.Count - 1)
                masterBinding.Position++;

            if (masterBinding.Position == masterBinding.Count - 1)
                Position = DBPosition.Last;
            else
                Position = DBPosition.Middle;
        }

        #endregion

        #region Find Record

        public bool FindRecord(IRadarParameters key)
        {
            if (key != null)
            {
                lastKey = key;
                Dataset.Clear();
                return FindRecursiveDataAdapter(key, collectionDB);
            }
            return false;
        }

        #endregion

        #region private functions

        private SqlProxyDataAdapter CreateDataAdapter(string name, SqlProxyCommand sqlCommand)
        {
            SqlProxyDataAdapter dAdapter = null;
            dAdapter = new SqlProxyDataAdapter(sqlCommand);

            try
            {
                dAdapter.FillSchema(Dataset, SchemaType.Source);
                dAdapter.Fill(Dataset, name);
                SetDefaultValue(name);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            return dAdapter;
        }

        private void SetDefaultValue(string name)
        {
            var dt = Dataset.Tables[name];
            foreach (DataColumn cl in dt.Columns)
            {
                var tp = cl.DataType;
                if (tp == typeof(string) || tp == typeof(String))
                    cl.DefaultValue = "";
                else
                    if (tp == typeof(Int32) || tp == typeof(int) || tp.BaseType == typeof(Enum) || tp == typeof(Byte) ||
                        tp == typeof(bool) || tp == typeof(Boolean))
                    cl.DefaultValue = 0;
                else
                    if (tp == typeof(Decimal) || tp == typeof(float) || tp == typeof(Double))
                    cl.DefaultValue = 0.0;
                else
                    if (tp == typeof(DateTime))
                    cl.DefaultValue = "1753-01-01 00:00:00";
                else
                    Debug.Assert(false, "CreateTable " + tp.ToString(), "Tipo colonna sconosciuto");
            }
        }

        private static void CreateCommand(SqlProxyDataAdapter dAdapter)
        {
            var cBuilder = new SqlProxyCommandBuilder(dAdapter)
            {
                QuotePrefix = "[",
                QuoteSuffix = "]",
                ConflictOption = ConflictOption.CompareRowVersion
            };
            dAdapter.UpdateCommand = cBuilder.GetUpdateCommand();
            dAdapter.InsertCommand = cBuilder.GetInsertCommand();
            dAdapter.DeleteCommand = cBuilder.GetDeleteCommand();
        }

        #endregion

        #region Update Record

        public void ValidateControl()
        {
            Status = DBMode.Validating;
            Debug.Assert(masterBinding != null);
            UpdateFiscalControl(dbOperation);
            slaveBindingCollection.EndEdit();
            masterBinding.EndEdit();
        }

        public void Find()
        {
            Status = DBMode.Find;
        }

        public bool AddNew()
        {
            if (masterBinding == null) return false;
            if (!OnBeforeAddNew()) return false;

            Dataset.Clear();

            masterBinding.CancelEdit();
            masterBinding.AddNew();
            slaveBindingCollection.AllowNew = true;
            Status = DBMode.Edit;
            dbOperation = DBOperation.New;
            UpdateFiscalControl(DBOperation.Get);
            return OnAfterAddNew();
        }

        public void Edit()
        {
            if (!FindRecord(lastKey))
            {
                MessageBox.Show(Properties.Resources.Msg_DocumentNotFound,
                            Properties.Resources.Warning,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                slaveBindingCollection.AllowNew = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            dbOperation = DBOperation.Edit;
            Status = DBMode.Edit;
            OnEdit();
        }

        public void Undo()
        {
            if (masterBinding == null || Dataset == null) return;
            Status = DBMode.Browse;
            masterBinding.CancelEdit();
            Dataset.RejectChanges();

            if (lastKey != null)
                FindRecord(lastKey);

            slaveBindingCollection.AllowNew = false;
        }

        public bool Save()
        {
            if (masterBinding == null || Dataset == null || collectionDB == null) return false;
            if (!OnBeforeSave()) return false;
            try
            {
                var ds2 = Dataset.GetChanges();
                if (ds2 != null)
                {
                    if (ds2.HasErrors) MessageBox.Show("Errori");
                    if (ds2.HasChanges())
                    {
                        Dataset.AcceptChanges();
                        UpdateRecursiveDataAdapter(ds2, collectionDB);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Save");
                return false;
            }
            return OnAfterSave();
        }

        public void UnlockRecordAndFind()
        {
            Status = DBMode.Browse;
            Dataset.EnforceConstraints = true;
            if (lastKey != null)
                FindRecord(lastKey);

            slaveBindingCollection.AllowNew = false;
        }

        public void Refresh()
        {
            if (lastKey != null)
                FindRecord(lastKey);
        }

        public bool Delete()
        {
            if (Dataset == null || collectionDB == null) return false;
            if (!OnBeforeDelete())
                return false;

            UpdateFiscalControl(DBOperation.Delete);

            FindRecord(lastKey);
            Dataset.Tables[collectionDB.Name].Rows[masterBinding.Position].Delete();
            DeleteRecursive(collectionDB.List);

            var ds2 = Dataset.GetChanges();
            if (ds2 != null)
            {
                // Visto che esiste il delete recursivo, devo solo cancellare la testa
                collectionDB.DataAdapter.Update(Dataset, collectionDB.Name);
                UpdateRecursiveDataAdapter(Dataset, collectionDB);
                Dataset.AcceptChanges();
            }
            slaveBindingCollection.AllowNew = false;
            return OnAfterDelete();
        }

        private void DeleteRecursive(List<DBCollection> list)
        {
            foreach (DBCollection collection in list)
            {
                for (int t = Dataset.Tables[collection.Name].Rows.Count - 1; t >= 0; t--)
                    Dataset.Tables[collection.Name].Rows[t].Delete();

                DeleteRecursive(collection.List);
            }
        }

        private void UpdateRecursiveDataAdapter(DataSet dataSet, DBCollection collection)
        {
            try
            {
                SetDataAdapterTransaction(collection.DataAdapter);
                if (true)
                {
                    CreateCommand(collection.DataAdapter);
                    collection.HasToCreateCommand = false;
                }

                collection.DataAdapter.Update(dataSet, collection.Name);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "UpdateRecursiveDataAdapter");
                return;
            }

            foreach (DBCollection slavecollection in collection.List)
                UpdateRecursiveDataAdapter(dataSet, slavecollection);
        }

        private bool FindRecursiveDataAdapter(IRadarParameters key, DBCollection collection)
        {
            bool found = false;
            try
            {
                SetParameters(key, collection);

                // AddOn management
                if (myDocument.AddonList != null)
                    foreach (Addon scr in myDocument.AddonList)
                        scr.SetParameters(this, key, collection);

                found = collection.DataAdapter.Fill(Dataset, collection.Name) > 0;

                if (found)
                {
                    foreach (DataRow row in Dataset.Tables[collection.Name].Rows)
                        OnPrepareAuxColumns(row);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, exc.StackTrace);
            }

            if (found)
                foreach (DBCollection slavecollection in collection.List)
                    FindRecursiveDataAdapter(key, slavecollection);

            return found;
        }

        protected virtual void OnPrepareAuxColumns(DataRow row)
        {
        }

        protected abstract void SetParameters(IRadarParameters key, DBCollection collection);

        private void UpdateFiscalControl(DBOperation operation)
        {
            if (fiscalControlList != null)
                foreach (ICounterManager fC in fiscalControlList)
                {
                    if (fC.IsUpdatable)
                        fC.UpdateValue(operation);

                    if (operation == DBOperation.Get)
                        fC.StatesButton = false;
                }
        }

        #endregion

        #region Column Management

        public DataColumn AddColumn(IColumn column)
        {
            return AddColumn(column.Tablename, column.Name, column.ColType);
        }

        public DataColumn AddColumn(string tableName, string columnName, System.Type columnType)
        {
            try
            {
                return Dataset.Tables[tableName].Columns.Add(columnName, columnType);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
        }

        public void SetColumn<T>(string tableName, string columnName, int row, T val)
        {
            if (row >= 0 && Dataset.Tables[tableName].Rows[row].RowState != DataRowState.Deleted)
                Dataset.Tables[tableName].Rows[row][columnName] = val;
        }

        public void SetColumn<T>(IColumn column, int row, T val)
        {
            if (row >= 0 && Dataset.Tables[column.Tablename].Rows[row].RowState != DataRowState.Deleted)
                Dataset.Tables[column.Tablename].Rows[row][column.Name] = val;
        }

        public T GetColumn<T>(IColumn column)
        {
            return GetColumn<T>(column, 0);
        }

        public T GetColumn<T>(IColumn column, int row)
        {
            if (Dataset.Tables[column.Tablename].Rows.Count < 1)
                return default(T);

            if (Dataset.Tables[column.Tablename].Rows[row].RowState == DataRowState.Deleted)
                return (T)GetColumn<T>(column, row, DataRowVersion.Original);

            if (Dataset.Tables[column.Tablename].Rows[row][column.Name] == System.DBNull.Value)
            {
                if (typeof(T) == typeof(string))
                    return (T)Convert.ChangeType("", typeof(T));
                else
                    return default(T);
            }
            if (typeof(T).IsEnum)
                return (T)System.Enum.ToObject(typeof(T), Dataset.Tables[column.Tablename].Rows[row][column.Name]);
            else
                return (T)Convert.ChangeType(Dataset.Tables[column.Tablename].Rows[row][column.Name], typeof(T));
        }

        public T GetColumn<T>(IColumn column, int row, DataRowVersion drs)
        {
            if (Dataset.Tables[column.Tablename].Rows.Count == 0 ||
                Dataset.Tables[column.Tablename].Rows[row][column.Name, drs] == System.DBNull.Value)
                return default(T);

            if (typeof(T).IsEnum)
                return (T)System.Enum.ToObject(typeof(T), Dataset.Tables[column.Tablename].Rows[row][column.Name, drs]);
            else
                return (T)Convert.ChangeType(Dataset.Tables[column.Tablename].Rows[row][column.Name, drs], typeof(T));
        }

        public DataTable GetDataTable<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            return Dataset.Tables[tableName];
        }

        public DataColumn GetDataColumn(IColumn column)
        {
            return Dataset.Tables[column.Tablename] == null
                ? null
                : Dataset.Tables[column.Tablename].Columns[column.Name];
        }

        public DataRow GetDataRow(string tableName, int row)
        {
            return Dataset.Tables[tableName].Rows[row];
        }

        public DataRow GetDataRow<T>(int row = 0)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            return Dataset.Tables[tableName].Rows[row];
        }

        #endregion

        #region Transaction
        public void StartTransaction()
        {
            if (globalPref.UseTransaction)
                SqlProxyTransaction = DBConnection.BeginTransaction();

        }

        public void Commit()
        {
            if (globalPref.UseTransaction && SqlProxyTransaction != null)
                SqlProxyTransaction.Commit();
        }

        public void Rollback()
        {
            if (globalPref.UseTransaction && SqlProxyTransaction != null)
                SqlProxyTransaction.Rollback();
        }

        private void SetDataAdapterTransaction(SqlProxyDataAdapter dataAdapter)
        {
            if (!globalPref.UseTransaction || SqlProxyTransaction == null)
                return;

            dataAdapter.InsertCommand.Transaction = SqlProxyTransaction;
            dataAdapter.DeleteCommand.Transaction = SqlProxyTransaction;
            dataAdapter.SelectCommand.Transaction = SqlProxyTransaction;
            dataAdapter.UpdateCommand.Transaction = SqlProxyTransaction;
        }
        #endregion
    }

    #endregion
}