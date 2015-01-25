﻿using System.Collections;
using System.Net.Mime;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using OrderServiceClient.Abstract;
using OrderServiceClient.WaiterDataAccessWCFService;

namespace OrderServiceClient.ViewModels
{
    internal class MainWindowViewModel : Conductor<object>, IMainWindowViewModel
    {
        private readonly IDialogLogin _dialogLogin;
        private readonly IWaiterDataModel _waiterDataModel;
        private readonly IWindowManager _windowManager;
        private IOrderDialog _orderDialog;
        private IConfirmDialogViewModel _confirmDialogViewModel;

        public MainWindowViewModel(IWindowManager windowManager, IOrderNotyficator _orderNotyficator,
            IWaiterDataModel waiterDataModel)
        {
            _windowManager = windowManager;
            _waiterDataModel = waiterDataModel;
            _orderNotyficator.SetTarget(this);

            _dialogLogin = new LoggerViewModel(this, _waiterDataModel);
            ActivateItem(_dialogLogin);
        }

        public void LogIn()
        {
            if (_waiterDataModel.IsLogged())
                DeactivateItem(_dialogLogin, true);
        }

        public void ShowNewOrder(Order order)
        {
            _orderDialog = new OrderViewModel(this, _waiterDataModel, order);
            ActivateItem(_orderDialog);
        }

        public bool GetConfirmFromWaiter(Order order)
        {
            //var result = _windowManager.ShowDialog(new ConfirmOrderViewModel(order));
          
            //if (result.HasValue)
            //    return result.Value;

            //return false;

            //Caliburn.Micro.Execute.OnUIThreadAsync(() => Acti)

            _confirmDialogViewModel = new ConfirmDialogViewModel(order);
            ActivateItem(_confirmDialogViewModel);
            bool result = _confirmDialogViewModel.GetResult();
            DeactivateItem(_confirmDialogViewModel,true);
            

            return result;

        }

        public void ShowAcceptedOrder(Order order)
        {
            _orderDialog = new OrderViewModel(this, _waiterDataModel, order);
            ActivateItem(_orderDialog);
        }

        public bool GetConfirmPayd()
        {
            var result = _windowManager.ShowDialog(new ConfirmPayViewModel());

            if (result.HasValue)
                return result.Value;

            return false;
        }

        public void CloseCurrentOrder()
        {
            DeactivateItem(_orderDialog, true);
        }
    }
}