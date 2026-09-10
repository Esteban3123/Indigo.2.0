'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Daniel Eduardo Arévalo Bonilla  
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region
<ServiceContract()>
Public Interface IBudgetServicePrivateBudget

    ''' <summary>
    ''' Guarda o Actualiza un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePrivateBudget(ByVal PrivateBudget As List(Of SP_ListPrivateBudget_Result), idSequense As Int64, audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result))

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePrivateBudget(ByVal PrivateBudget As PrivateBudget, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPrivateBudget(audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result))

End Interface
