namespace SimpleWorkflowEngine.Core

exception FlagExistsException of string * string
exception FlagNotExistsException of string * string
exception FlagsGroupExistsException of string
exception FlagsGroupNotExistsException of string