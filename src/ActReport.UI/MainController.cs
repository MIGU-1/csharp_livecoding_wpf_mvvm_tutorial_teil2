﻿using ActReport.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ActReport.UI
{
    public class MainController : IController
    {
        public void ShowWindow(BaseViewModel viewModel)
        {
            Window window = viewModel switch
            {
                null => throw new ArgumentNullException(nameof(viewModel)),

                EmployeeViewModel _ => new MainWindow(),

                ActivityViewModel _ => new ActivityWindow(),

                _ => throw new InvalidOperationException(nameof(viewModel)),
            };

            window.DataContext = viewModel;
            window.ShowDialog();
        }
        public void CloseWindow(BaseViewModel viewModel)
        {
            throw new NotImplementedException();
        }

    }
}
