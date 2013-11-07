﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
namespace OSU_player
{
    public partial class ChooseColl : RadForm
    {
        public ChooseColl()
        {
            InitializeComponent();
        }
        private List<int> tmpindex = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            string collectpath = Path.Combine(Core.osupath, "collection.db");
            if (File.Exists(collectpath)) { OsuDB.ReadCollect(collectpath); }
            listBox1.Items.Clear();
            foreach (string key in Core.Collections.Keys)
            {
                listBox1.Items.Add(key);
            }
            if (listBox1.Items.Count != 0) { listBox1.SelectedIndex = 0; }
            RadMessageBox.Show("刷新完毕！");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0) { return; }
            List<int> CollectionMaps = Core.Collections[listBox1.SelectedItem.ToString()];
            listBox2.Items.Clear();
            tmpindex.Clear();
            foreach (int mapindex in CollectionMaps)
            {
                listBox2.Items.Add(Core.allsets[mapindex].ToString());
                tmpindex.Add(mapindex);
            }
        }
        private void listBox2_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (listBox2.SelectedItems.Count == 0) { return; }
            Core.AddSet(tmpindex[listBox2.SelectedIndex]);
            RadMessageBox.Show(String.Format("成功导入{0}",listBox2.SelectedItem.ToString()));
        }
        private void listBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0) { return; }
            Core.AddRangeSet(tmpindex);
            RadMessageBox.Show(String.Format("成功导入{0}首曲目", tmpindex.Count.ToString()));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}