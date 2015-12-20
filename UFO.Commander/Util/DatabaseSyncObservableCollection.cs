namespace UFO.Commander.Util
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public sealed class DatabaseSyncObservableCollection<T> : ObservableCollection<T>
        where T : DatabaseObject
    {
        #region Fields

        private readonly IBaseServerAsync<T> server;
        private readonly Func<Task<IEnumerable<T>>> updateMethod;

        #endregion

        public DatabaseSyncObservableCollection(IBaseServerAsync<T> server, Func<Task<IEnumerable<T>>> updateMethod)
        {
            this.server = server;
            this.updateMethod = updateMethod;

            ItemChanged += SyncChangedItem;
            CollectionChanged += SyncChangedCollection;

            PullUpdates();
        }

        public DatabaseSyncObservableCollection(IBaseServerAsync<T> server)
            : this(server, server.GetAllAsync)
        {
        }

        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        public async void PullUpdates()
        {
            // remove old
            UnregisterList(Items);
            Items.Clear();

            // add new
            (await updateMethod()).ToList().ForEach(Add);
            RegisterList(Items);
        }

        private async void SyncChangedItem(object sender, ItemChangedEventArgs args)
        {
            if (!args.Value.HasId)
            {
                await server.AddAsync(args.Value);
            }

            if (!await server.UpdateAsync(args.Value))
            {
                MessageBox.Show(
                    "Invalid data. The item will be reset",
                    "Invalid data.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                ItemChanged -= SyncChangedItem;
                args.Value.OverwriteProperties(await server.GetByIdAsync(args.Value.Id));
                ItemChanged += SyncChangedItem;
            }
        }

        private async void SyncChangedCollection(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RegisterList(args.NewItems?.Cast<T>());
                    foreach (var newItem in args.NewItems ?? new List<T>())
                    {
                        await server.AddAsync(newItem as T);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    UnregisterList(args.OldItems.Cast<T>());
                    foreach (var oldItem in args.OldItems ?? new List<T>())
                    {
                        await server.RemoveAsync(oldItem as T);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RegisterList(IEnumerable<T> list)
        {
            foreach (var item in list ?? new List<T>())
            {
                item.PropertyChanged += OnItemChanged;
            }
        }

        private void UnregisterList(IEnumerable<T> list)
        {
            foreach (var item in list ?? new List<T>())
            {
                item.PropertyChanged -= OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(sender as T));
        }

        #region Nested type: ItemChangedEventArgs

        public class ItemChangedEventArgs : EventArgs
        {
            public ItemChangedEventArgs(T value)
            {
                Value = value;
            }

            #region Properties

            public T Value { get; }

            #endregion
        }

        #endregion
    }
}
