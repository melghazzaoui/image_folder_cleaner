using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgComparer
{
    public partial class Form1 : Form
    {
        private string refFolderPath;
        private string targetFolderPath;
        private ImageFolderComparer imageFolderComparer;
        private ImageFolderCleaner cleaner;
        private TaskRunner taskRunner;
        private ListBox listBox = null;
        private TextBox focusedTextBox;
        private string text_backup;
        private History history;
        private const string historyFilePath = "history.txt";

        public Form1()
        {
            InitializeComponent();

            createHistory();
        }

        private void createHistory()
        {
            history = History.FromFile(historyFilePath);
            if (history == null)
            {
                history = new History();
            }
        }

        private string selectFolder(string entryPoint)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (entryPoint != null)
            {
                folderBrowser.SelectedPath = FolderComparer.toNormalizedPath(entryPoint);
            }            
            DialogResult res = folderBrowser.ShowDialog();
            if (res == DialogResult.OK)
            {
                return folderBrowser.SelectedPath;
            }
            folderBrowser.Dispose();
            return null;
        }

        private void addLogLine(string text)
        {
            richTextLog.Text += text + "\n";
        }
        private void clearLog()
        {
            richTextLog.Text = "";
        }


        private bool test(object o)
        {
            string msg = o as string;
            System.Console.WriteLine(msg);
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string msg = "Hello!!!";
            GenericTaskRunner gen = new GenericTaskRunner(test, msg);
            gen.TaskFinishedEvent += Gen_TaskFinishedEvent;
            gen.Run();

            txtRefFolder.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtTargetFolder.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            text_backup = "";
            Size minSize = new Size(800, 450);
            this.MinimumSize = minSize;
            this.SetBounds(Location.X, Location.Y, 1200, 675);

            btnAbort.Enabled = false;
            richTextLog.ReadOnly = true;
            refreshPathsDataAndUI();
        }

        private void Action_GenTaskFinished(TaskFinishedEventArgs args)
        {
            //MessageBox.Show(args.Status.ToString() + (args.Msg != null ? (" (" + args.Msg + ")") : ""));
        }

        private void Gen_TaskFinishedEvent(object sender, TaskFinishedEventArgs args)
        {
            Invoke(new Action(() => Action_GenTaskFinished(args)));
        }

        private void btnSelectRef_Click(object sender, EventArgs e)
        {
            string path = selectFolder(refFolderPath);
            if (path != null)
            {
                refFolderPath = path;
                txtRefFolder.Text = refFolderPath;
            }
        }

        private void setTextBoxContent(TextBox txtBox, string text)
        {
            txtBox.Text = text;
            text_backup = text;
        }

        private void btnSelectTarget_Click(object sender, EventArgs e)
        {
            string path = selectFolder(targetFolderPath);
            if (path != null)
            {
                targetFolderPath = path;
                setTextBoxContent(txtTargetFolder, targetFolderPath);
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (radioComparison.Checked)
            {
                btnSelectRef.Enabled = false;
                btnSelectTarget.Enabled = false;
                btnCompare.Enabled = false;
                btnAbort.Enabled = true;
                txtRefFolder.Enabled = false;
                txtTargetFolder.Enabled = false;
                progressBarComparer.Value = 0;


                refFolderPath = FolderComparer.toNormalizedPath(txtRefFolder.Text);
                targetFolderPath = FolderComparer.toNormalizedPath(txtTargetFolder.Text);

                history.Add(targetFolderPath);

                imageFolderComparer = new ImageFolderComparer(refFolderPath, targetFolderPath);
                taskRunner = imageFolderComparer;
                taskRunner.TaskFinishedEvent += ImageFolderComparer_TaskFinishedEvent;
                imageFolderComparer.FilesComparedEvent += ImageFolderComparer_FilesComparedEvent;
                
                
                clearLog();
                addLogLine("Comparison started");
            }
            else
            {
                btnSelectRef.Enabled = false;
                btnCompare.Enabled = false;
                btnAbort.Enabled = true;
                txtRefFolder.Enabled = false;
                progressBarComparer.Value = 0;

                cleaner = new ImageFolderCleaner(txtRefFolder.Text);
                taskRunner = cleaner;
                richTextLog.Text = "";
                cleaner.FileDeletedEvent += Cleaner_FileDeletedEvent;
                cleaner.TaskFinishedEvent += Cleaner_TaskFinishedEvent;
                clearLog();
                addLogLine("Clean started");
            }

            taskRunner.stepsCountComputedEvent += ImageFolderComparer_stepsCountComputedEvent;
            taskRunner.progressInfoEvent += ImageFolderComparer_progressInfoEvent;
            taskRunner.Run();

            history.Add(refFolderPath);
            history.SaveToFile(historyFilePath);
        }

        private void ImageFolderComparer_progressInfoEvent(object sender, ProgressInfoEventArgs args)
        {
            Invoke(new Action(() => progressBarComparer.Value = args.Progress));
        }

        private void ImageFolderComparer_stepsCountComputedEvent(object sender, StepsCountComputedEventArgs args)
        {
            Invoke(new Action(() => Action_stepsCountComputed(args)));
        }

        private void Action_stepsCountComputed(StepsCountComputedEventArgs args)
        {
            if (args.Count > 0)
            {
                progressBarComparer.Maximum = args.Count;
            }
            else
            {
                progressBarComparer.Maximum = 100;
            }            
        }

        private void Action_FileDeleted(string file)
        {
           addLogLine(file + " deleted");
        }

        private void Action_FileMoved(string file, string newFilePath)
        {
            addLogLine( file + " moved to " + newFilePath);
        }

        private void Action_ComparisonTerminated(TaskFinishedEventArgs args)
        {
            btnSelectRef.Enabled = true;
            btnSelectTarget.Enabled = true;
            btnCompare.Enabled = true;
            btnAbort.Enabled = false;
            txtRefFolder.Enabled = true;
            txtTargetFolder.Enabled = true;
            progressBarComparer.Value = progressBarComparer.Maximum;

            string msg = "Comparison terminated.";
            if (args.Status == TaskFinishedEventArgs.StatusValues.FAILURE)
            {
                msg += " But with some failures (" + args.Msg + ")";
            }
            addLogLine(msg);
            MessageBox.Show(msg);
        }

        private void ImageFolderComparer_FilesComparedEvent(object sender, FilesComparedEventArgs args)
        {
            string file1 = args.File1;
            string file2 = args.File2;
            if (args.FilesEqual)
            {
                System.IO.File.Delete(file1);
                Invoke(new Action(() => Action_FileDeleted(file1)));
            }
            else
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(file1);
                string ext = System.IO.Path.GetExtension(file1);
                string newFilePath = System.IO.Path.GetDirectoryName(file1) + System.IO.Path.DirectorySeparatorChar + filename + "-renamed" + ext;
                System.IO.File.Move(file1, newFilePath);
                Invoke(new Action(() => Action_FileMoved(file1, newFilePath)));
            }
        }

        private void ImageFolderComparer_TaskFinishedEvent(object sender, TaskFinishedEventArgs args)
        {
            Invoke(new Action(() => Action_ComparisonTerminated(args)));
        }

        private IList<string> getDriveDirectories(string driveName)
        {
            IList<string> dirList = new List<string>();
            System.IO.DriveInfo di = new System.IO.DriveInfo(driveName);
            System.IO.DirectoryInfo root = di.RootDirectory;
            System.IO.DirectoryInfo[] rootDirs = root.GetDirectories();
            foreach (System.IO.DirectoryInfo dirInfo in rootDirs)
            {
                dirList.Add(dirInfo.FullName);
            }
            return dirList;
        }

        private bool isDrivePath(string path, out string driveName)
        {
            string[] segments = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 1 && segments[0].EndsWith(":"))
            {
                driveName = segments[0] + System.IO.Path.DirectorySeparatorChar;
                return true;
            }
            driveName = "";
            return false;
        }
                
        private IList<string> getDirListFromPath(string path)
        {
            IList<string> dirList = new List<string>();
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    string driveName;
                    if (isDrivePath(path, out driveName)) // if it is a drive
                    {
                        dirList = getDriveDirectories(driveName);
                    }
                    else
                    {
                        dirList = System.IO.Directory.GetDirectories(path).ToList();
                    }
                }
                else if (path.Length == 0) // empty path: list all drives
                {
                    System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                    foreach (System.IO.DriveInfo di in drives)
                    {
                        dirList.Add(di.Name);
                    }
                }
                else
                {
                    string[] segments = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length >= 2)
                    {
                        string prefix = segments[segments.Length - 1].ToLower();
                        string parentPath = string.Join(System.IO.Path.DirectorySeparatorChar.ToString(), segments, 0, segments.Length - 1);
                        IList<string> children = null;
                        string driveName = "";
                        if (isDrivePath(parentPath, out driveName))
                        {
                            children = getDriveDirectories(driveName);
                        }
                        else
                        {
                            children = System.IO.Directory.GetDirectories(parentPath).ToList();
                        }
                        foreach (string child in children)
                        {
                            string filename = System.IO.Path.GetFileName(child);
                            if (filename.ToLower().StartsWith(prefix))
                            {
                                dirList.Add(child);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return dirList;
        }

        private void TextBox_ListViewWhenTextChanged(object sender)
        {
            try
            {
                TextBox txtBox = sender as TextBox;
                focusedTextBox = txtBox;
                if (txtBox != null)
                {
                    string text = FolderComparer.toNormalizedPath(txtBox.Text);
                    IList<string> dirList = getDirListFromPath(text);
                    if (dirList.Count > 0)
                    {
                        attachListViewToControl(txtBox);
                        listBox.Items.Clear();
                        foreach (string path in dirList)
                        {
                            listBox.Items.Add(path);
                        }
                        if (history.Count > 0)
                        {
                            listBox.Items.Add(new StringContainer("-------------- From History --------------"));
                            foreach (string path in history.ToList())
                            {
                                listBox.Items.Add(path);
                            }
                        }
                    }
                    else
                    {
                        removeListView();
                    }
                }
            }
            catch (Exception exc)
            {
                removeListView();
                MessageBox.Show(exc.Message);
            }
        }

        private void refreshPathsDataAndUI()
        {            
            refFolderPath = FolderComparer.toNormalizedPath(txtRefFolder.Text);
            if (radioComparison.Checked)
            {
                targetFolderPath = FolderComparer.toNormalizedPath(txtTargetFolder.Text);
                bool comparisonEnabled = false;
                if (targetFolderPath.Length > 0 && refFolderPath.Length > 0)
                {
                    if (System.IO.Directory.Exists(targetFolderPath) && System.IO.Directory.Exists(refFolderPath))
                    {
                        if (!FolderComparer.checkFolderInclusionNormalized(refFolderPath, targetFolderPath))
                        {
                            comparisonEnabled = true;
                        }
                    }
                }
                btnCompare.Enabled = comparisonEnabled;
            }
            else
            {
                btnCompare.Enabled = System.IO.Directory.Exists(refFolderPath);
            }
        }

        private void TextBox_ChangedCommon(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null && txtBox.Focused)
            {
                text_backup = txtBox.Text;
                TextBox_ListViewWhenTextChanged(sender);
            }
            if (System.IO.Directory.Exists(txtBox.Text))
            {
                txtBox.BackColor = Color.GreenYellow;
            }
            else
            {
                txtBox.BackColor = Color.LightSalmon;
            }
            refreshPathsDataAndUI();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (taskRunner != null)
            {
                taskRunner.Abort();
            }
        }

        private Point getControlAbsolutePosition(Control control)
        {
            return control.FindForm().PointToClient(control.Parent.PointToScreen(control.Location));
        }

        private void attachListViewToControl(Control control)
        {
            if (listBox == null)
            {
                listBox = new ListBox();
                listBox.Parent = this;
                Point location = getControlAbsolutePosition(control);
                listBox.SetBounds(location.X, location.Y + control.Height, control.Width, 200);
                listBox.Anchor = control.Anchor;
                listBox.Font = control.Font;
                listBox.Visible = true;
                listBox.BringToFront();
                listBox.Click += ListView_SelectionConfirmed;
                listBox.KeyDown += ListView_KeyDown;
                listBox.SelectedIndexChanged += ListView_SelectedIndexChanged;
                listBox.KeyPress += ListBox_KeyPress;
            }
        }

        private void ListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '/' || e.KeyChar == '\\')
            {
                ListView_SelectionConfirmed(sender, null);
            }
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (focusedTextBox != null)
            {
                string text = listBox.SelectedItem as string;
                if (text != null)
                {
                    focusedTextBox.Text = text;
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListView_SelectionConfirmed(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                focusedTextBox.Text = text_backup;
                removeListView();
                focusedTextBox.Focus();
            }
        }

        private void ListView_SelectionConfirmed(object sender, EventArgs e)
        {
            if (listBox != null)
            {
                string path = listBox.SelectedItem as string;
                if (path != null)
                {
                    removeListView();
                    if (focusedTextBox != null)
                    {
                        path += System.IO.Path.DirectorySeparatorChar;
                        setTextBoxContent(focusedTextBox, path);
                        focusedTextBox.Focus();
                        focusedTextBox.Select(path.Length, 0);
                        TextBox_ChangedCommon(focusedTextBox, null);
                        
                    }                    
                }
            }
        }

        private void removeListView()
        {
            if (listBox != null)
            {
                listBox.Parent = null;
                listBox.Dispose();
                listBox = null;
            }
        }

        private void txtBox_CommonFocusEnter(object sender, EventArgs e)
        {
            text_backup = (sender as TextBox).Text;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            removeListView();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            removeListView();
        }

        private void txtBox_Leave(object sender, EventArgs e)
        {
            if (listBox != null)
            {
                if (!listBox.Focused)
                {
                    removeListView();
                }
            }
        }

        private void txtRefFolder_Leave(object sender, EventArgs e)
        {
            txtBox_Leave(sender, e);
        }

        private void txtTargetFolder_Leave(object sender, EventArgs e)
        {
            txtBox_Leave(sender, e);
        }

        private void TextBox_CommonKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (e.KeyCode == Keys.Escape)
            {
                removeListView();
                txtBox.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (System.IO.Directory.Exists(txtBox.Text))
                {
                    if (txtBox == txtRefFolder)
                    {
                        txtTargetFolder.Focus();
                    }
                    else if (txtBox == txtTargetFolder)
                    {
                        if (btnCompare.CanFocus)
                        {
                            btnCompare.Focus();
                        }
                    }
                }
                removeListView();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (listBox == null)
                {
                    TextBox_ListViewWhenTextChanged(sender);
                }
                if (listBox != null)
                {
                    listBox.Focus();
                    if (listBox.Items.Count > 0)
                    {
                        listBox.SelectedIndex = 0;
                    }
                }
            }
        }


        private void Action_CleanFinished(TaskFinishedEventArgs args)
        {
            btnSelectRef.Enabled = true;
            btnCompare.Enabled = true;
            btnAbort.Enabled = false;
            txtRefFolder.Enabled = true;
            progressBarComparer.Value = progressBarComparer.Maximum;

            string msg = "Clean finished.";
            if (args.Status == TaskFinishedEventArgs.StatusValues.FAILURE)
            {
                msg += " But with some failures";
            }
            if (args.Msg != null)
            {
                msg += " (" + args.Msg + ")";
            }
            addLogLine(msg);

            refreshPathsDataAndUI();
            MessageBox.Show(msg);
        }

        private void Cleaner_TaskFinishedEvent(object sender, TaskFinishedEventArgs args)
        {
            Invoke(new Action(() => Action_CleanFinished(args)));
        }

        private void Cleaner_FileDeletedEvent(object sender, FileEventArgs args)
        {
            Invoke(new Action(() => addLogLine(args.Path + " deleted")));
        }

        private void radioComparison_CheckedChanged(object sender, EventArgs e)
        {
            gbRefFolder.Enabled = true;
            gbTargetFolder.Enabled = gbTargetFolder.Visible = radioComparison.Checked;
            btnCompare.Text = radioComparison.Checked ? "Compare" : "Clean";
            refreshPathsDataAndUI();
        }
    }
}
