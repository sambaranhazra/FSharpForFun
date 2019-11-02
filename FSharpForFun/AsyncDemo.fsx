open System

let userTimerWithCallback = 
    let event = new System.Threading.AutoResetEvent(false)

    let timer = new System.Timers.Timer(2000.0)

    timer.Elapsed.Add (fun _ -> event.Set() |> ignore )

    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    // keep working
    printfn "Doing something useful while waiting for event"

    // block on the timer via the AutoResetEvent
    event.WaitOne() |> ignore

    //done
    printfn "Timer ticked at %O" DateTime.Now.TimeOfDay
