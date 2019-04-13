﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frame.View
{
    public enum EnumState
    {
        OK,
        NG,
        WAITING,
    }
    /// <summary>
    /// UC_ResultPanel.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ResultPanel : UserControl, INotifyPropertyChanged
    {
        private string oKCOntent;

        private string nGContent;

        private string waitingContent;

        EnumState state;


        public UC_ResultPanel()
        {
            InitializeComponent();
            state = EnumState.WAITING;
        }

        public EnumState State
        {
            get { return state; }
            set {
                if (state != value)
                {
                    state = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackGroundColor"));
                }
            }
        }
        public string Header { get; set; }

        public string Text { get {
                switch (State)
                {
                    case EnumState.OK:
                        return oKCOntent;
                    case EnumState.NG:
                        return nGContent;
                    case EnumState.WAITING:
                        return waitingContent;
                    default:
                        return "";
                }
            } }

        public SolidColorBrush BackGroundColor { get {
                switch (State)
                {
                    case EnumState.OK:
                        return new SolidColorBrush(Color.FromRgb(0,255,0));
                    case EnumState.NG:
                        return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    case EnumState.WAITING:
                        return new SolidColorBrush(Color.FromRgb(255, 178, 0));
                    default:
                        return new SolidColorBrush(Color.FromRgb(0,0,0));
                }
            } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetContent(string OkContent, string NGContent, string WaitingContent)
        {
            oKCOntent = OkContent;
            nGContent = NGContent;
            waitingContent = WaitingContent;
        }

    }
}
