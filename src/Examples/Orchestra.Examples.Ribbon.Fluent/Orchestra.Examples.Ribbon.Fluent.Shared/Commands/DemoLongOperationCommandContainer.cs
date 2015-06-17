﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoLongOperation.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Examples.Ribbon
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Services;

    internal class DemoLongOperationCommandContainer : Catel.MVVM.CommandContainerBase
    {
        private readonly IPleaseWaitService _pleaseWaitService;

        public DemoLongOperationCommandContainer(ICommandManager commandManager, 
            IPleaseWaitService pleaseWaitService)
            : base(Commands.Demo.LongOperation, commandManager)
        {
            _pleaseWaitService = pleaseWaitService;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            const int TotalItems = 250;

            await Task.Factory.StartNew(() =>
            {
                var random = new Random();

                for (var i = 0; i < TotalItems; i++)
                {
                    _pleaseWaitService.UpdateStatus(i + 1, TotalItems);

                    ThreadHelper.Sleep(random.Next(20, 40));
                }
            });
        }
    }
}