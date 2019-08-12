namespace etofsharp

open System
open Eto.Forms
open Eto.Drawing


type MainForm () as this =
    inherit Form()
    do
        base.Title <- "My Eto Form"
        base.ClientSize <- new Size(400, 350)

        // table with three rows
        let layout = new StackLayout()
        layout.Items.Add(new StackLayoutItem(new Label(Text = "Hello World!")))
        // Add more controls here
        let txtName = new TextBox()
        layout.Items.Add(new StackLayoutItem(txtName))

        //calendar
        let cal = new Calendar()
        layout.Items.Add(new StackLayoutItem(cal))

        //checkbox
        let chk = new CheckBox()
        layout.Items.Add(new StackLayoutItem(chk))

        //combo box 
        let cbo = new ComboBox()
        cbo.Items.Add("Feathers")
        cbo.Items.Add("Scales")
        cbo.Items.Add("Fur")
        layout.Items.Add(new StackLayoutItem(cbo))
        cbo.SelectedKeyChanged.Add(fun e -> MessageBox.Show("combo box clicked " + cbo.SelectedValue.ToString()) |> ignore)

        //date time picker
        let dtp = new DateTimePicker()
        //layout.Items.Add(new StackLayoutItem(dtp))

        //splitter
        let splitter = new Splitter()
        let panel1 = new Panel()
        let panel2 = new Panel()
        splitter.Panel1 = panel1.Content |> ignore
        splitter.Panel2 = panel2.Content |> ignore
        splitter.Orientation = Orientation.Vertical |> ignore
        splitter.Position = 200 |> ignore


        layout.Items.Add(new StackLayoutItem(splitter))
        base.Content <- layout;

        // create a few commands that can be used for the menu and toolbar
        let clickMe = new Command(MenuText = "Click Me!", ToolBarText = "Click Me Toolbar!")
        clickMe.Executed.Add(fun e -> MessageBox.Show(this, "I was clicked!") |> ignore)

        let cmdNew = new Command(MenuText = "New", ToolBarText = "New")
        cmdNew.Executed.Add(fun e -> MessageBox.Show(this, "New Clicked") |> ignore)

        let quitCommand = new Command(MenuText = "Quit")
        quitCommand.Shortcut <- Application.Instance.CommonModifier ||| Keys.Q
        quitCommand.Executed.Add(fun e -> Application.Instance.Quit())

        let aboutCommand = new Command(MenuText = "About...")
        aboutCommand.Executed.Add(fun e ->
            let dlg = new AboutDialog()
            dlg.ShowDialog(this) |> ignore
            )

        base.Menu <- new MenuBar()
        let fileItem = new ButtonMenuItem(Text = "&File")
        fileItem.Items.Add(clickMe) |> ignore
        fileItem.Items.Add(cmdNew) |> ignore
        base.Menu.Items.Add(fileItem)

        (* add more menu items to the main menu...
        let editItem = new ButtonMenuItem(Text = "&Edit")
        base.Menu.Items.Add(editItem)
        let viewItem = new ButtonMenuItem(Text = "&View")
        base.Menu.Items.Add(viewItem)
        *)

        base.Menu.ApplicationItems.Add(new ButtonMenuItem(Text = "&Preferences..."))
        base.Menu.QuitItem <- quitCommand.CreateMenuItem()
        base.Menu.AboutItem <- aboutCommand.CreateMenuItem()

        base.ToolBar <- new ToolBar()
        base.ToolBar.Items.Add(clickMe)
        base.ToolBar.Items.Add(cmdNew)
