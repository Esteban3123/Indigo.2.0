'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCREGEGRERepository
    Inherits IRepository(Of HCREGEGRE)

    Function GetHCREGEGREByAdmissionCode(code As String) As HCREGEGRE

End Interface
