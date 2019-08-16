namespace etofsharp

open System
open Eto.Forms
open Eto.Drawing

open AppConstants
open db
open parinherm



type MainForm () as this =
    inherit Form()

    let listbox = new ListBox()
    let lstchapter = new ListBox()
    let txtBody = new RichTextArea()

    //this is the constructor
    do
        base.Title <- AppConstants.APP_NAME
        base.ClientSize <- new Size(400, 350)
        this.Closed.Add(fun e -> Database.closedatabase())
        Database.opendatabase()


        (*listbox.ItemTextBinding = Binding.Property(fun (t: AppConstants.intrecord) -> t.Name) |> ignore
        listbox.ItemKeyBinding = Binding.Property(fun (t:AppConstants.intrecord) -> t.id) |> ignore
        listbox.DataStore <- AppConstants.subjects
        *)
        let listsubjects = Database.getsubjects AppConstants.currentstate.bookid
        List.iter( fun (x: AppConstants.intrecord) -> listbox.Items.Add( new ListItem(Text=x.name, Key=x.id.ToString()) )) listsubjects

        //splitter
        let splitter = new Splitter()

        //panel 1
        let panel1 = new Panel()
        let layoutPanel1 = new TableLayout()
        layoutPanel1.Rows.Add(new TableRow(new TableCell(listbox)))
        panel1.Content <- layoutPanel1

        //panel 2
        let panel2 = new Panel()

        lstchapter.Width <- 200

        let gridview = new GridView()
        gridview.Columns.Add(new GridColumn(HeaderText = "magnificent"))


        let layoutPanel2 = new TableLayout(new TableRow(new TableCell(lstchapter), new TableCell( txtBody )))

        panel2.Content <- layoutPanel2

        listbox.SelectedIndexChanged.Add(fun e -> this.selectsubject)
        lstchapter.SelectedIndexChanged.Add(fun e -> this.selectchapter)

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
        cmdNewSubject.Executed.Add(fun e -> this.newsubject() |> ignore)

        let cmdNewChapter = new Command(MenuText="New &Chapter", ToolBarText="Chapter")
        cmdNewChapter.Executed.Add(fun e -> this.newchapter() |> ignore)

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


        let editItem = new ButtonMenuItem(Text = "&Edit")
        editItem.Items.Add(cmdNewSubject) |> ignore
        editItem.Items.Add(cmdNewChapter) |> ignore
        base.Menu.Items.Add(editItem)

        let viewItem = new ButtonMenuItem(Text = "&View")
        base.Menu.Items.Add(viewItem)
       

        base.Menu.ApplicationItems.Add(new ButtonMenuItem(Text = "&Preferences..."))
        base.Menu.QuitItem <- quitCommand.CreateMenuItem()
        base.Menu.AboutItem <- aboutCommand.CreateMenuItem()

        base.ToolBar <- new ToolBar()
        base.ToolBar.Items.Add(clickMe)
        base.ToolBar.Items.Add(cmdNew)


        //Database.runtestquery()


    member this.newsubject() = 
        let subjectDialog = new SubjectDialog()
        let result = subjectDialog.ShowModal(this)
        0

    member this.newchapter() = 
       let dialog = new ChapterDialog()
       let result = dialog.ShowModal(this)
       0

    member this.clearlistbox (list: ListBox) = 
        list.Items.Clear()

    member this.selectsubject =
    //    clearlistbox listboxsubjects |> ignore
        //get the selected key in the list and convert to an int64
        let selectedKey = listbox.SelectedKey
        AppConstants.currentstate.subjectid <- selectedKey |> int64
        match currentstate.subjectid with
        | 0L ->  List.iter( fun (x: String) -> lstchapter.Items.Add(x)) AppConstants.syntaxSubjects
        | _ -> printfn "nothing selected"


    member this.selectchapter = 
        let text = 
                   match lstchapter.SelectedIndex with
                        | 0 -> "variables"
                        | 1 -> "lists"
                        | 2 -> "functions"
                        | _ -> "f"
        txtBody.Text <- text

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


      