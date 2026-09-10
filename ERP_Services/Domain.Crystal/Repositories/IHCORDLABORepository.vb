'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDLABORepository
    Inherits IRepository(Of HCORDLABO)

    Function GetIHCORDLABOByAuto(auto As Integer) As HCORDLABO

End Interface
