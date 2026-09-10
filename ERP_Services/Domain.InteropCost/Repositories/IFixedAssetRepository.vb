'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface IFixedAssetRepository
    Inherits IRepository(Of AFNACTIVO)

    ''' <summary>
    ''' Obtiene un activo fijo por codigo
    ''' </summary>
    Function GetFixedAssetByCode(code As String) As AFNACTIVO

    ''' <summary>
    ''' Obtiene un activo fijo por id
    ''' </summary>
    Function GetFixedAssetById(oid As Integer) As AFNACTIVO

End Interface