(* attempting to implement EtoForms databinding *)
namespace model

module models =

    open System
    open System.ComponentModel

    type MyModel() =

        let ev = new Event<_,_>()

        interface INotifyPropertyChanged with
            [<CLIEvent>]
            member x.PropertyChanged = ev.Publish
