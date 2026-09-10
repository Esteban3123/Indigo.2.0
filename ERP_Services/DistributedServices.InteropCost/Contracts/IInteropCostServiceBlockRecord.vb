'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceBlockRecord

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    <OperationContract()>
    Function GetBlockRecordInteropCostByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInteropCost

    ''' <summary>
    ''' Bloquea un registro
    ''' </summary>
    <OperationContract()>
    Function SaveBlockRecordInteropCost(ByVal blockRecordInteropCost As BlockRecordInteropCost) As ActionResult(Of BlockRecordInteropCost)

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    <OperationContract()>
    Function DeleteBlockRecordInteropCost(ByVal blockRecordInteropCost As BlockRecordInteropCost) As ActionResult

End Interface