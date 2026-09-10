'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractCareGroup
    <OperationContract()> _
    Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of CareGroupInvoiceCategories)
    ''' <summary>
    ''' Guarda o Actualiza un grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCareGroup(CareGroup As Domain.Entities.CareGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup)

    ''' <summary>
    ''' Elimina un grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCareGroup(id As Integer, Company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCareGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup)

    ''' <summary>
    ''' Obtiene un determinado grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCareGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCareGroup(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup)

    <OperationContract()>
    Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer, audit As AuditMessage) As GroupersCareGroup


End Interface
