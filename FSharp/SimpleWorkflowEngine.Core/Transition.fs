namespace SimpleWorkflowEngine.Core
open System.Collections.Generic

type Transition = {
    FromStepId:string
    ToStepId:string
    TransitionOperand:LogicOperand
    FlagsGroups:Map<string,FlagsGroup>
}
with
    member me.AddFlag(flagGroup,flag,createGroup,groupLogicOperand) =
        let group = match (me.FlagsGroups.ContainsKey(flagGroup),createGroup) with
                    |(false,true)   -> {Id = flagGroup; LogicOperand = groupLogicOperand;Flags = Map.empty}
                    |(true, _)      -> me.FlagsGroups |> Map.find flagGroup
                    |_              -> raise (FlagsGroupNotExistsException flagGroup)
        {me with FlagsGroups = me.FlagsGroups.Add(flagGroup, group.AddFlag(flag))}