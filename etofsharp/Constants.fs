module AppConstants

open System
open Eto.Forms
open Eto.Drawing

let APP_NAME = "kernai"

//list of subjects
let subjects = ["syntax in 60 secs"; "Thinking Functionally"]
let syntaxSubjects = ["variables"; "lists"; "functions"]

let syntaxfunctions = "
let square x = x * x
"

let clearlistbox (list: ListBox) = 
    list.Items.Clear()

let selectsubject (listboxsubjects: ListBox) (listboxsyntax: ListBox) =
    clearlistbox listboxsyntax |> ignore
    match listboxsubjects.SelectedIndex with
    | 0 ->  List.iter( fun (x: String) -> listboxsyntax.Items.Add(x)) syntaxSubjects
    | _ -> printfn "nothing selected"

let selectsyntax (listboxsyntax: ListBox) (textbox: RichTextArea) = 
    let text = 
               match listboxsyntax.SelectedIndex with
                    | 0 -> "variables"
                    | 1 -> "lists"
                    | 2 -> "functions"
                    | _ -> "f"
    textbox.Text <- text
