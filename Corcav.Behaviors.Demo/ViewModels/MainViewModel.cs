// <copyright file="MainViewModel.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo.ViewModels
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        string firstName = "FirstName";
        string lastName = "LastName";
        string message;
        string welcomeMessage;
        Command testCommand;
        Command<object> unfocusedCommand;
        Command<string> nickSelectedCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            Items = new ObservableCollection<Item>()
            {
                new Item()
                {
                    NickName = "corcav",
                },
                new Item()
                {
                    NickName = "foo99",
                },
                new Item()
                {
                    NickName = "bar76",
                },
            };
        }

        /// <summary>
        /// Fired when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the first name property.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get => firstName;

            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    RaisePropertyChanged();
                    TestCommand.ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name property.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get => lastName;

            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    RaisePropertyChanged();
                    TestCommand.ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Gets the message for the user.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get => message;

            private set
            {
                if (value != message)
                {
                    message = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the welcome message.
        /// </summary>
        /// <value>The welcome message.</value>
        public string WelcomeMessage
        {
            get => welcomeMessage;

            private set
            {
                if (value != welcomeMessage)
                {
                    welcomeMessage = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the test command.
        /// </summary>
        /// <value>The test command.</value>
        public Command TestCommand
        {
            get
            {
                if (testCommand != null)
                {
                    return testCommand;
                }

                testCommand = new Command(
                    () => { WelcomeMessage = $"Hello {FirstName} {LastName}"; },
                    () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));

                return testCommand;
            }
        }

        /// <summary>
        /// Gets the unfocused command.
        /// </summary>
        /// <value>The unfocused command.</value>
        public Command<object> UnfocusedCommand
        {
            get
            {
                if (unfocusedCommand != null)
                {
                    return unfocusedCommand;
                }

                unfocusedCommand = new Command<object>(param => { Message = $"Unfocused raised with param {param}"; }, _ => true);
                return unfocusedCommand;
            }
        }

        /// <summary>
        /// Gets the nick selected command.
        /// </summary>
        /// <value>The nick selected command.</value>
        public Command<string> NickSelectedCommand
        {
            get
            {
                if (nickSelectedCommand != null)
                {
                    return nickSelectedCommand;
                }

                nickSelectedCommand = new Command<string>(param => { Message = $"Item {param} selected"; }, _ => true);
                return nickSelectedCommand;
            }
        }

        /// <summary>
        /// Gets the items in this view.
        /// </summary>
        /// <value>The items in this view.</value>
        public ObservableCollection<Item> Items { get; }

        /// <summary>
        /// Raise a property changed event, based on the caller name.
        /// </summary>
        /// <param name="propertyName">The name of the property that is calling this method.</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
