'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface IAFNDEPRECIRepository
    Inherits IRepository(Of AFNDEPRECI)

    ''' <summary>
    ''' Obtiene un activo fijo por codigo
    ''' </summary>
    Function GetAFNDEPRECIByYearMonth(year As Integer, month As Integer) As AFNDEPRECI

End Interface