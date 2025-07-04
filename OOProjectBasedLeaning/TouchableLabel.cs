using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{
    public abstract class TouchableLabel : Label
    {
        public TouchableLabel()
        {

            InitializeComponent();

        }

        private void InitializeComponent()
        {

            Click += touchLabel_Click;

        }

        private void touchLabel_Click(object? sender, EventArgs e)
        {

            OnTouch();

        }

        protected abstract void OnTouch();

    }

    public class NullTouchableLabel : TouchableLabel
    {

        private static readonly TouchableLabel instance = new NullTouchableLabel();

        private NullTouchableLabel()
        {

        }

        public static TouchableLabel Instance
        {

            get { return instance; }

        }

        protected override void OnTouch()
        {

        }

    }
}
