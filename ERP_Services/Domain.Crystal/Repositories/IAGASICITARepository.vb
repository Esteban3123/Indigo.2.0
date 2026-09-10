'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAGASICITARepository
    Inherits IRepository(Of AGASICITA)

    Function GetAGASICITAByAuto(auto As Integer) As AGASICITA

End Interface