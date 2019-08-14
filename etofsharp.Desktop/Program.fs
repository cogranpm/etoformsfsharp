namespace etofsharp.Desktop
module Program =

    open System
    open etofsharp

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platform.Detect)
        app.Terminating.Add(fun e -> printfn "I am quitting")
        app.Run(new MainForm())
        0