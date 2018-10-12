using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CFCSMobile.Controls
{
    public partial class LabledDateControl : Grid
    {
        public int TitleFontSize { get { return (int)TheTitle.FontSize; } set { TheTitle.FontSize = value; } }
        public int ValueFontSize { get { return (int)TheValue.FontSize; } set { TheValue.FontSize = value; } }

        public string TitleText { get { return TheTitle.Text; } set { TheTitle.Text = value; } }
        public string ValueText { get { return TheValue.Text; } set { TheValue.Text = value; } }


        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
                                                         propertyName: "TitleFontSize",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(LabledDateControl),
                                                         defaultValue: 12,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleFontSizePropertyChanged);

        public static readonly BindableProperty ValueFontSizeProperty = BindableProperty.Create(
                                                         propertyName: "ValueFontSize",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(LabledDateControl),
                                                         defaultValue: 16,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ValueFontSizePropertyChanged);

        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(LabledDateControl),
                                                         defaultValue: "Title Here",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
                                                         propertyName: "ValueText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(LabledDateControl),
                                                         defaultValue: "Value Here",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ValueTextPropertyChanged);


        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LabledDateControl)bindable;
            control.TheTitle.Text = newValue.ToString();
        }

        private static void TitleFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LabledDateControl)bindable;
            control.TheTitle.FontSize = int.Parse(newValue.ToString());
        }

        private static void ValueTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LabledDateControl)bindable;
            control.TheValue.Text = newValue.ToString();
        }

        private static void ValueFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LabledDateControl)bindable;
            control.TheValue.FontSize = int.Parse(newValue.ToString());
        }


        public LabledDateControl()
        {
            InitializeComponent();
        }

        //public LabledDateControl(string LBL, string VAL, int FS)
        //{
        //    InitializeComponent();

        //    TheTitle.Text = LBL;
        //    TheValue.Text = VAL;

        //    TitleFontSize = FS - 2;
        //    ValueFontSize = FS;

        //}

    }
}
