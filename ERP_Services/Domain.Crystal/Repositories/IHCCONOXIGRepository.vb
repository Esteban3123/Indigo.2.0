'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCCONOXIGRepository
    Inherits IRepository(Of HCCONOXIG)

    Function GetHCCONOXIGByIDCONOXIG(idconoxig As Integer) As HCCONOXIG

End Interface