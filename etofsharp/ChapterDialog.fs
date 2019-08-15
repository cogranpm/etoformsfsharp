namespace parinherm

open System
open Eto.Forms
open Eto.Drawing

open db
open AppConstants

type ChapterDialog() as this =
    inherit Dialog()

    let mutable txtName: TextBox = new TextBox()

    do
        base.Title <- "Chapter"
        base.ClientSize <- new Size(600, 600)
        let padding = new Padding(10)
        let spacing = new Size(5, 5)
        let layout = new TableLayout()
        layout.Spacing <- spacing
        layout.Padding <- padding

        let lblHeader = new Label()
        lblHeader.Text <- "Subject"

        let lblName = new Label()
        lblName.Text <- "Name:"

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


        let tableRow = new TableRow()
        layout.Rows.Add(tableRow)       

        tableRow.ScaleHeight <- false
        tableRow.Cells.Add(new TableCell(lblName, ScaleWidth=false))
        tableRow.Cells.Add(new TableCell(txtName, ScaleWidth=true))

        let buttonRow = new TableRow()
        layout.Rows.Add(buttonRow)
        tableRow.Cells.Add(new TableCell(null, ScaleWidth=true))
        let cellButtons = new TableCell(btnCancel)
        buttonRow.Cells.Add(cellButtons)
        buttonRow.Cells.Add(new TableCell(btnOK))
        

        layout.Rows.Add(null)
        base.Content <- layout

   

    member this.onok() =
        let nameval = txtName.Text
        if not (nameval.Equals("")) then
            AppConstants.currentstate.chapterid <- Database.insertchapter AppConstants.currentstate.subjectid nameval

        this.Close()

    member this.oncancel() =
        this.Close()