module AppConstants

open System
open Eto.Forms
open Eto.Drawing

let APP_NAME = "kernai"

type appstate =  {mutable bookid:int64; mutable subjectid:int64; mutable chapterid:int64}
type intrecord = {id:int64; name:string}
let currentstate = {bookid=3L; subjectid=0L; chapterid=0L}

//list of subjects
let subjects = ["syntax in 60 secs"; "Thinking Functionally"]
let syntaxSubjects = ["variables"; "lists"; "functions"]

let syntaxfunctions = "
let square x = x * x
"


