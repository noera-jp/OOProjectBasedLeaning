namespace OOProjectBasedLeaning
{

    public partial class HomeForm : DragDropForm
    {

        private Home home = NullHome.Instance;

        public HomeForm()
        {

            InitializeComponent();

            home = new HomeModel("myhome");

        }


        protected override void OnFormDragEnterSerializable(DragEventArgs dragEventArgs)
        {

            dragEventArgs.Effect = DragDropEffects.Move;

        }

        protected override void OnFormDragDropSerializable(object? serializableObject, DragEventArgs dragEventArgs)
        {

            if (serializableObject is DragDropPanel)
            {

                (serializableObject as DragDropPanel).AddDragDropForm(this, PointToClient(new Point(dragEventArgs.X, dragEventArgs.Y)));
                EmployeePanel employPanel = serializableObject as EmployeePanel;
                employPanel.AddHome(home);

            }

        }
    }
}
