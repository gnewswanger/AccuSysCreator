''' <summary>
''' File: Enums.vb
''' Author: Galen Newswanger
''' 
''' This file contains a group of type enumerations to provide constants 
''' to any class that needs to reference them.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Enum OperationPathMode
  doHaunch
  doMortise
  doPilot
  doTenon
End Enum
<Flags()> _
Public Enum PartEdgeTypes
  StileLeft = 2
  StileRight = 4
  StileCenterL = 8
  StileCenterR = 16
  TopRail = 32
  BotRail = 64
  RailCenterT = 128
  RailCenterB = 256
  GlueInStrip = 512
  OpeningL = 1024
  OpeningR = 2048
  OpeningT = 4096
  OpeningB = 8192
  Opening = OpeningL Or OpeningR Or OpeningT Or OpeningB
  Stile = StileLeft Or StileRight
  CenterStile = StileCenterL Or StileCenterR
  CenterRail = RailCenterT Or RailCenterB
  Center = CenterStile Or CenterRail
End Enum

Public Enum EventName
  evnmCustomHaunchAtFarendPathChanged
  evnmCustomHaunchAtZeroPathChanged
  evnmCustomTenonAtFarendPathChanged
  evnmCustomTenonAtZeroPathChanged
  evnmCurrentFrameEdgeDataChanged
  evnmCurrentFramePartDataChanged
  evnmCurrentFrontFrameDataChanged
  evnmCurrentJobIsNothing
  evnmDbAdjoiningListDataUpdated
  evnmDbCabinetPartListChanged
  evnmDbFrameEdgeDataUpdated
  evnmDbFramePartDataUpdated
  evnmDbFrontFrameDataUpdated
  evnmDbProgramInfoDataUpdated
  evnmDbProgramInfoDataDeleted
  evnmDbJobDataSelectionChanged
  evnmDbLoadedJobNumberChanged
  evnmDbJobInfoUpdated
  evnmDbJobHasNoParts
  evnmDbMthOperationDataUpdated
  evnmDbShipweekScheduleDataUpdated
  evnmDbStyleDefaultsReset
  evnmDBStyleDefaultSelectionChanged
  evnmDbStyleFrameEdgeDataUpdated
  evnmDbStyleHingeDataUpdated
  evnmDbStyleHingeRuleDataUpdated
  evnmDbStylePartDataUpdated
  evnmDbStyleThicknessDataUpdated
  evnmVwAdjoiningListViewChanged
  evnmVwPartEdgeViewChanged
  evnmVwFramePartViewChanged
  evnmVwFrontFrameViewChanged
  evnmVwMthOperationViewChanged
  evnmVwRefreshShipweekNoList
  evnmVwShipweekScheduleViewChanged
  evnmVwStyleDefaultSelectionChanged
  evnmVwStyleFrameEdgeViewChanged
  evnmVwStyleHingeRuleViewChanged
  evnmVwStyleHingeViewChanged
  evnmVwStylePartViewChanged
  evnmVwStyleThicknessViewChanged
End Enum