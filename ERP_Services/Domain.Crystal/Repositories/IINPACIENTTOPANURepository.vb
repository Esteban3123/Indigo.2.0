'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IINPACIENTTOPANURepository
    Inherits IRepository(Of INPACIENTTOPANU)

    Function GetINPACIENTTOPANUByIPCODPACI(IPCODPACI As String, year As Integer) As INPACIENTTOPANU

End Interface