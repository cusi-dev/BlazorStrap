﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorStrap.util
{
    public class DropDownManager
    {
        public EventHandler OnToggle { get; set; }
        public List<KeyAndValue<string, bool>> MenuState { get; set; } = new List<KeyAndValue<string, bool>>();

        public void AddDropDownMenu(string id)
        {
            if (id == null || id == "")
                throw new InvalidOperationException("Id must be used with Drop Down Manager");

            MenuState.Add(new KeyAndValue<string, bool>(id, false));
        }

        public void RemoveDropDownMenu(string id)
        {
            if (id == null || id == "")
                throw new InvalidOperationException("Id must be used with Drop Down Manager");
            var remove = MenuState.FirstOrDefault(q => q.Key == id);
            MenuState.Remove(remove);
        }

        public void Toggle(string id)
        {
            if (id == null || id == "")
                throw new InvalidOperationException("Id must be used with Drop Down Manager");

            foreach (var menu in MenuState)
            {
                if (menu.Key == id)
                {

                    menu.Value = !menu.Value;
                }
                else
                {
                    menu.Value = false;
                }
            }
            OnToggle.Invoke(this, new EventArgs());
        }

        public bool IsOpen(string id)
        {
            if (id == null || id == "")
                throw new InvalidOperationException("Id must be used with Drop Down Manager");

            return MenuState.FirstOrDefault(q => q.Key == id).Value;
        }
    }

    public class KeyAndValue<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public KeyAndValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
