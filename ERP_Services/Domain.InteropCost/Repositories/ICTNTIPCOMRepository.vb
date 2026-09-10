'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface ICTNTIPCOMRepository
    Inherits IRepository(Of CTNTIPCOM)

    ''' <summary>
    ''' Obtiene un activo fijo por codigo
    ''' </summary>
    Function GetCTNTIPCOMById(oid As Integer) As CTNTIPCOM

End Interface