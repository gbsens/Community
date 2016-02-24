using System;
using System.Collections.Generic;
using MKS.Core;
using MKS.Core.Security;

namespace MKS.Core.Presenter.UI
{

    #region FrameworkUIBASE

    public enum Select
    {
        Selected = 0,
        Unselect = 1,
        Bolt = 2
    }

    public class UIBase : IUIBase
    {
        private bool _enabled = true;
        private bool _visible = true;

        public UIBase()
        {
            Permissions = null;
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public string Info { get; set; }
        public string Tips { get; set; }
        public UIVAlidation UIValidations { get; set; }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public List<Permission> Permissions { get; set; }


    }

    public class Menu : UIBase, IUIMenu
    {
        private List<Menu> _menus = new List<Menu>();

        public Menu()
        {
            Url = null;
        }
        public List<Menu> Value
        {
            get
            {
                return Menus;
            }
            set
            {
                Menus = value;
            }
        }
        public Menu(string text, string command)
        {
            Url = null;
            Text = text;
            Command = command;
        }

        public string Command { get; set; }

        public Uri Url { get; set; }

        public List<Menu> Menus
        {
            get { return _menus; }
            set { _menus = value; }
        }

        public string Text { get; set; }
    }

    public class Option : UIBase, IUIOption
    {
        private Label _Titre = new Label();

        public Label Texte
        {
            get { return _Titre; }
            set { _Titre = value; }
        }
        public bool Value
        {
            get
            {
                switch (Selection)
                {
                    case Select.Selected:
                        return true;
                        break;
                    case Select.Unselect:
                        return false;
                        break;
                    case Select.Bolt:
                        return true;
                        break;
                    default:
                        return false;
                        break;
                }
            }
            set
            {
                if (value == true)
                    Selection = Select.Selected;
                else
                    Selection = Select.Unselect;
            }

        }
        public Select Selection { get; set; }
    }

    public class CheckBox : UIBase, IUICheck
    {
        private bool _enabled = true;
        private Label _Titre = new Label();

        public Label Texte
        {
            get { return _Titre; }
            set { _Titre = value; }
        }
        public bool Value
        {
            get
            {
                switch (Selection)
                {
                    case Select.Selected:
                        return true;
                        break;
                    case Select.Unselect:
                        return false;
                        break;
                    case Select.Bolt:
                        return true;
                        break;
                    default:
                        return false;
                        break;
                }
                
            }
            set
            {
                if (value == true)
                    Selection = Select.Selected;
                else
                    Selection = Select.Unselect;
            }

        }
        public Select Selection { get; set; }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }

    public class Input : UIBase, IUIInput
    {
        public Input()
        {
        }
        public Input(string value)
        {
            Text = value;
        }
        public Input(string text, Label label)
        {
            Text = text;
            Label = label;
        }
        public string Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text=value;
            }
        }
        public string Text { get; set; }
        public Label Label { get; set; }
    }

    public class NumericInput : UIBase, IUINumericInput
    {
        public NumericInput()
        {
        }
        public NumericInput(int value)
        {
            Text = value;
        }
        public NumericInput(int value, Label label)
        {
            Text = value;
            Label = label;
        }
        public int Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public int Text { get; set; }
        public Label Label { get; set; }
    }
    public class NumericInputLong : UIBase, IUINumericInputLong
    {
        public NumericInputLong()
        {
        }
        public NumericInputLong(long value)
        {
            Text = value;
        }
        public NumericInputLong(long value, Label label)
        {
            Text = value;
            Label = label;
        }
        public long Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public long Text { get; set; }
        public Label Label { get; set; }
    }
    public class NumericInputFloat : UIBase, IUINumericInputFloat
    {
        public NumericInputFloat()
        {
        }
        public NumericInputFloat(float value)
        {
            Text = value;
        }
        public NumericInputFloat(float value, Label label)
        {
            Text = value;
            Label = label;
        }
        public float Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public float Text { get; set; }
        public Label Label { get; set; }
    }
    public class NumericInputDecimal : UIBase, IUINumericInputDecimal
    {
        public NumericInputDecimal()
        {
        }
        public NumericInputDecimal(decimal value)
        {
            Text = value;
        }
        public NumericInputDecimal(decimal value, Label label)
        {
            Text = value;
            Label = label;
        }
        public decimal Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public decimal Text { get; set; }
        public Label Label { get; set; }
    }
    public class NumericInputDouble : UIBase, IUINumericInputDouble
    {
        public NumericInputDouble()
        {
        }
        public NumericInputDouble(double value)
        {
            Text = value;
        }
        public NumericInputDouble(double value, Label label)
        {
            Text = value;
            Label = label;
        }
        public double Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public double Text { get; set; }
        public Label Label { get; set; }
    }

    public class DateInput : UIBase, IUIDateInput
    {
        public DateInput()
        {
        }
        public DateInput(DateTime date)
        {
            Date = date;
        }
        public DateInput(DateTime date, Label label)
        {
            Date = date;
            Label = label;
        }
        public DateTime Value
        {
            get
            {
                return Date;
            }
            set
            {
                Date = value;
            }
        }
        public DateTime Date { get; set; }
        public Label Label { get; set; }
    }

    public class Label : UIBase, IUILabel
    {
        public Label()
        {
        }

        public Label(string text)
        {
            Text = text;
        }
        public string Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public string Text { get; set; }
    }

    public class Button : UIBase, IUIButton
    {
        public Button()
        {
        }

        public Button(string text)
        {
            Text = text;
        }
        public string Value
        {
            get
            {
                return Command;
            }
            set
            {
                Command = value;
            }
        }
        public string Text { get; set; }

        public string Command { get; set; }
    }

    public class Tab<TView> : UIBase, IUITab<TView> where TView : IUIForm
    {
        private Label _Titre { get; set; }
        private TView Body { get; set; }

        public Label Title
        {
            get { return _Titre; }
            set { _Titre = value; }
        }

        public TView View
        {
            get { return Body; }
            set { Body = value; }
        }
        public TView Value
        {
            get
            {
                return View;
            }
            set
            {
                View = value;
            }
        }
    }

    public class Grid : UIBase, IUIGrid
    {
        private VirtualSkip _VirtualPagingParameters = new VirtualSkip();

        public VirtualSkip VirtualPaging
        {
            get { return _VirtualPagingParameters; }
            set { _VirtualPagingParameters = value; }
        }
    }

    #endregion FrameworkUIBASE
}