'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostBlockRecordAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    Function GetBlockRecordCostByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordCost

    ''' <summary>
    ''' Bloquea un registro
    ''' </summary>
    Function SaveBlockRecordCost(ByVal blockRecordCost As BlockRecordCost) As ActionResult(Of BlockRecordCost)

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    Function DeleteBlockRecordCost(ByVal blockRecordCost As BlockRecordCost) As ActionResult

End Interface