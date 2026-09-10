'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Carlos Mario Arias Rubiano  
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
Public Interface IBudgetServicePrivateBudgetItemsStructure

    ''' <summary>
    ''' Guarda o Actualiza un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure)

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPrivateBudgetItemsStructure(code As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure)

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPrivateBudgetItemsStructureById(id As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure)

End Interface
