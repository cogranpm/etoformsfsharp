namespace parinherm

open System
open Eto.Forms
open Eto.Drawing

open db
open AppConstants

type NoteDialog() as this =
    inherit Dialog()

    let layout = new TableLayout()
    let txtName: TextBox = new TextBox()
    let txtBody: TextArea = new TextArea()
    let txtScript: TextArea = new TextArea()

    do
        base.Title <- "Chapter"
        base.ClientSize <- new Size(600, 600)
        let padding = new Padding(10)
        let spacing = new Size(5, 5)

        layout.Spacing <- spacing
        layout.Padding <- padding

        let lblHeader = new Label()
        lblHeader.Text <- "Subject"

        let lblName = new Label()
        lblName.Text <- "Name:"

        let lblBody = new Label()
        lblBody.Text <- "Body:"

        let lblScript = new Label()
        lblScript.Text <- "Script:"


        let btnOK = new Button()
        btnOK.Text <- "OK"
        btnOK.Click.Add(fun e -> this.onok())

        let btnCancel = new Button()
        btnCancel.Text <- "Cancel"
        btnCancel.Click.Add(fun e -> this.oncancel())

        base.DefaultButton <- btnCancel

        let headerRow = new TableRow()
        layout.Rows.Add(headerRow)
        headerRow.Cells.Add(new TableCell(lblHeader, ScaleWidth=true))

        this.addrow lblName txtName
        this.addrowtarea lblBody txtBody
        this.addrowtarea lblScript txtScript

        let buttonRow = new TableRow()
        layout.Rows.Add(buttonRow)
        buttonRow.Cells.Add(new TableCell(null, ScaleWidth=true))
        let cellButtons = new TableCell(btnCancel)
        buttonRow.Cells.Add(cellButtons)
        buttonRow.Cells.Add(new TableCell(btnOK))
        

        layout.Rows.Add(null)
        base.Content <- layout


    member this.addrow label edit =
        let tableRow = new TableRow()
        layout.Rows.Add(tableRow)       
        tableRow.ScaleHeight <- false
        tableRow.Cells.Add(new TableCell(label, ScaleWidth=false))
        tableRow.Cells.Add(new TableCell(edit, ScaleWidth=true))

    member this.addrowtarea label edit =
        let tableRow = new TableRow()
        layout.Rows.Add(tableRow)       
        tableRow.ScaleHeight <- true
        tableRow.Cells.Add(new TableCell(label, ScaleWidth=false))
        tableRow.Cells.Add(new TableCell(edit, ScaleWidth=true))

    member this.onok() =
        let nameval = txtName.Text
        let bodyval = txtBody.Text
        let scriptval = txtScript.Text
        if not (nameval.Equals("")) then
            AppConstants.currentstate.noteid <- Database.insertnote AppConstants.currentstate.chapterid nameval bodyval scriptval

        this.Close()

    member this.oncancel() =
        this.Close()