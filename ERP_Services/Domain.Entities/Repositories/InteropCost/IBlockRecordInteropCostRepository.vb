'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBlockRecordInteropCostRepository
    Inherits IRepository(Of BlockRecordInteropCost)

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    Function GetBlockRecordInteropCostByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInteropCost

End Interface

