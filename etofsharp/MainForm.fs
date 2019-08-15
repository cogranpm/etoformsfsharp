namespace etofsharp

open System
open Eto.Forms
open Eto.Drawing

open AppConstants
open db



type MainForm () as this =
    inherit Form()
    //this is the constructor
    do
        base.Title <- AppConstants.APP_NAME
        base.ClientSize <- new Size(400, 350)
        this.Closed.Add(fun e -> Database.closedatabase())

        let listbox = new ListBox()
        List.iter( fun (x: String) -> listbox.Items.Add(x)) AppConstants.subjects

        //splitter
        let splitter = new Splitter()

        //panel 1
        let panel1 = new Panel()
        let layoutPanel1 = new TableLayout()
        layoutPanel1.Rows.Add(new TableRow(new TableCell(listbox)))
        panel1.Content <- layoutPanel1

        //panel 2
        let panel2 = new Panel()
        let lstSubjectBody = new ListBox()
        lstSubjectBody.Width <- 200

        let gridview = new GridView()
        gridview.Columns.Add(new GridColumn(HeaderText = "magnificent"))

        let txtBody = new RichTextArea()
        let layoutPanel2 = new TableLayout(new TableRow(new TableCell(lstSubjectBody), new TableCell( txtBody )))

        panel2.Content <- layoutPanel2

        listbox.SelectedIndexChanged.Add(fun e -> AppConstants.selectsubject listbox lstSubjectBody)
        lstSubjectBody.SelectedIndexChanged.Add(fun e -> AppConstants.selectsyntax lstSubjectBody txtBody)

        splitter.Panel1 <- panel1
        splitter.Panel2 <- panel2 
        splitter.Orientation <- Orientation.Horizontal 
        splitter.Position <- 200

        base.Content <- splitter

        // create a few commands that can be used for the menu and toolbar
        let clickMe = new Command(MenuText = "Click Me!", ToolBarText = "Click Me Toolbar!")
        clickMe.Executed.Add(fun e -> MessageBox.Show(this, "I was clicked!") |> ignore)

        let cmdNew = new Command(MenuText = "New", ToolBarText = "New")
        cmdNew.Executed.Add(fun e -> MessageBox.Show(this, "New Clicked") |> ignore)

        let cmdNewSubject = new Command(MenuText="New &Subject", ToolBarText="Subject")
        cmdNewSubject.Executed.Add(fun e -> this.somefunc() |> ignore)

        let quitCommand = new Command(MenuText = "Quit")
        quitCommand.Shortcut <- Application.Instance.CommonModifier ||| Keys.Q
        quitCommand.Executed.Add(fun e -> Application.Instance.Quit())

        let aboutCommand = new Command(MenuText = "About...")
        aboutCommand.Executed.Add(fun e ->
            let dlg = new AboutDialog()
            dlg.ShowDialog(this) |> ignore
            )

        Database.opendatabase()
        //Database.runinsertcommand()
        Database.runtestquery()

        base.Menu <- new MenuBar()
        let fileItem = new ButtonMenuItem(Text = "&File")
        fileItem.Items.Add(clickMe) |> ignore
        fileItem.Items.Add(cmdNew) |> ignore
        base.Menu.Items.Add(fileItem)


        let editItem = new ButtonMenuItem(Text = "&Edit")
        editItem.Items.Add(cmdNewSubject) |> ignore
        base.Menu.Items.Add(editItem)
        let viewItem = new ButtonMenuItem(Text = "&View")
        base.Menu.Items.Add(viewItem)
       

        base.Menu.ApplicationItems.Add(new ButtonMenuItem(Text = "&Preferences..."))
        base.Menu.QuitItem <- quitCommand.CreateMenuItem()
        base.Menu.AboutItem <- aboutCommand.CreateMenuItem()

        base.ToolBar <- new ToolBar()
        base.ToolBar.Items.Add(clickMe)
        base.ToolBar.Items.Add(cmdNew)


    member this.somefunc() = 
        printfn("hello there")
        (* all old stuff
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
        layout.Items.Add(new StackLayoutItem(dtp))
        *)


      