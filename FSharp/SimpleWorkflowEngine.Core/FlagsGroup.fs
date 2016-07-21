namespace SimpleWorkflowEngine.Core

open System.Collections.Generic

type FlagsGroup = {
    Id:string
    LogicOperand:LogicOperand
    Flags:Map<string,bool>
}
with
    member me.ContainsFlag(flag) = me.Flags.ContainsKey(flag)

    member me.AddFlag(flag) =
        if me.ContainsFlag(flag) then raise (FlagExistsException (me.Id,flag))
        {me with Flags = me.Flags.Add(flag,false)}
    
    member me.SetFlag(flag) =
        if not <| me.ContainsFlag(flag) then raise (FlagNotExistsException (me.Id,flag))
        {me with Flags = me.Flags.Add(flag,true)}

    member me.Satisfies() =
        let values = me.Flags |> Map.toSeq |> Seq.map (fun(_,v) -> v)
        match me.LogicOperand with
        |LogicOperand.And   -> values |> Seq.contains(false) |> not
        |_                  -> values |> Seq.contains(true)
