namespace parinherm

open System
open Eto.Forms
open Eto.Drawing

open db

type SubjectDialog() as this =
    inherit Dialog()
    do
        base.Title <- "Subject"
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

        let txtName = new TextBox()

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
        
        this.Close()

    member this.oncancel() =
        this.Close()