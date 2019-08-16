module AppConstants

open System
open Eto.Forms
open Eto.Drawing

let APP_NAME = "kernai"

//maybe can change this to not be mutable
type appstate =  {mutable bookid:int64; mutable subjectid:int64; mutable chapterid:int64; mutable noteid:int64}
type intrecord = {id:int64; name:string}
type noterecord = {name:string; body:string; script:string}
let currentstate = {bookid=3L; subjectid=0L; chapterid=0L; noteid=0L}
