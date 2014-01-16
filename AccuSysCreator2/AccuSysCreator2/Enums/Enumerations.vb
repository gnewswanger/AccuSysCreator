''' <summary>
''' <file>File: Enumerations.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This file contains a group of type enumerations to provide constants 
''' to any class that needs to reference them.
''' </summary>
''' <remarks></remarks>

Public Enum CabinetTypes
    Unassigned = 0
    Wall = 1
    Base = 2
    Tall = 4
    Oven = 8
    Refrigerator = 16
End Enum

<Flags()> _
Public Enum HingePlacement
    N = 0
    L = 2
    R = 4
    T = 8
    B = 16
End Enum

Public Enum dbTransactionTypes
    dbFail
    dbUpdate
    dbInsert
    dbSelect
End Enum

Public Enum SourceOfData
    dsNone
    dsExistingData
    dsNewCSV
    dsExistingCSV
    dsTemplateCSV
End Enum

Public Enum StyleTypes
    rgFrameEdgeStyle
    rgFramePartStyle
    rgThickStyle
    rgHingeStyle
    rgHingeRule
End Enum

<Flags()> _
Public Enum JobFileStatus
    jfsNone = 1
    jfsCSVExists = 2
    jfsDatabaseExists = 4
End Enum

