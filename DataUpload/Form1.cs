using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataUpload {
    public partial class Form1 : Form, LogBox {

        private Thread currentThread;
        private List<Thread> runningThreads;
        private Thread threadWatcher;

        private ArticleImporter articleImporter;

        private string sourceFile;

        public Form1() {
            InitializeComponent();

            runningThreads = new List<Thread>();

        }

        #region LogBox INterface

        public delegate void InsertDelegate(string line);
        public void insert(string line) {
            if (this.rtbLog.InvokeRequired) {
                InsertDelegate d = new InsertDelegate(insert);
                rtbLog.Invoke(d, new object[] { line });
            } else {
                rtbLog.AppendText(line + "\n");
            }
        }

        public delegate void ClearDelegate();
        public void clear() {
            if (this.rtbLog.InvokeRequired) {
                ClearDelegate d = new ClearDelegate(clear);
                rtbLog.Invoke(d, new object[] { });
            } else {
                rtbLog.Clear();
            }
        }

        public void counterUp(string text) {
            
        }


        public void setLockState(bool lockState) {
            
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (currentThread != null && (currentThread.ThreadState == ThreadState.Running || currentThread.ThreadState == ThreadState.WaitSleepJoin)) {
                e.Cancel = true;
                String warn = "You cannot close app because upload is still running";
                MessageBox.Show(warn, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else {
                e.Cancel = false;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e) {
            if (this.txtFilePath.Text.Length > 0) {
                rtbLog.Clear();
                currentThread = new Thread(new ThreadStart(updateArticles));
                currentThread.Priority = ThreadPriority.Highest;
                threadWatcher = new Thread(new ThreadStart(watchThread));
                setLockState(false);
                currentThread.Start();
                threadWatcher.Start();
            }
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e) {
            chooseFile();
            this.txtFilePath.Text = sourceFile;
        }

        private void updateArticles() {
            ArticleImporter ai = new ArticleImporter(this.sourceFile, this);
            ai.Save();
        }

        private void clearArticles() {
            ArticleImporter ai = new ArticleImporter(this);
            ai.DeleteAll();
        }

        private void watchThread() {
            while (currentThread.IsAlive) {
                Thread.Sleep(1000);
            }
            setLockState(true);
            threadWatcher.Abort();
        }

        private void chooseFile() {
            openFileDialog1.ShowDialog();
            sourceFile = openFileDialog1.FileName != null && openFileDialog1.FileName != "openFileDialog1" ? openFileDialog1.FileName : "";
        }

        private void btnClearImport_Click(object sender, EventArgs e) {
            rtbLog.Clear();
            currentThread = new Thread(new ThreadStart(clearArticles));
            currentThread.Priority = ThreadPriority.Highest;
            threadWatcher = new Thread(new ThreadStart(watchThread));
            setLockState(false);
            currentThread.Start();
            threadWatcher.Start();
        }
    }
}
