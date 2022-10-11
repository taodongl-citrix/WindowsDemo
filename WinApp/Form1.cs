﻿/*
 * Copyright © Citrix Systems, Inc.  All rights reserved.
 *
 * CITRIX CONFIDENTIAL – THIS IS PROPRIETARY SOFTWARE OF CITRIX SYSTEMS, INC. and includes Citrix trade secrets.
 * This software is fully protected under copyright and trade secret laws.
 * You may not use, copy, or distribute this software without a valid written license from Citrix.
 * You shall not modify, adapt, translate, or create derivative works based upon this software.
 * If you received this file in error, please immediately notify sourcecodelicensing@citrix.com.
 */

using System;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(direct.Text))
            {
                MessageBox.Show("Please select the direct", "Warning");
                return;
            }
            var aDir = (PipeDirection)Enum.Parse(typeof(PipeDirection), this.direct.Text);
            var serv = GetPipeServer(aDir);
            var sr = new StreamReader(serv);
            var sw = new StreamWriter("message.txt");
            // todo
            // ...
            var test = sr.ReadLine();
            sw.WriteLine(test);
        }
        private NamedPipeServerStream GetPipeServer(PipeDirection aDir)
        {
            var ps = new PipeSecurity();
            var par = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
            ps.AddAccessRule(par);
            return new NamedPipeServerStream("Message" + aDir.ToString(), aDir, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 4096, 4096, ps);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
