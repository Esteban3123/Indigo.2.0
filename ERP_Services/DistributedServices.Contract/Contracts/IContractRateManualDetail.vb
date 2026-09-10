'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractRateManualDetail

    ''' <summary>
    ''' Guarda o Actualiza un detalle de manual tarifario
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRateManualDetail(RateManualDetail As Domain.Entities.RateManualDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail)

    ''' <summary>
    ''' Guarda o actualiza el listado de manual de servicios
    ''' </summary>
    ''' <param name="ListRateManualDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveListRateManualDetail(ListRateManualDetail As List(Of Domain.Entities.RateManualDetail), ListDeleteRateManualDetail As List(Of RateManualDetail), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.RateManualDetail))

    ''' <summary>
    ''' Elimina un detalle de manual tarifario
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRateManualDetail(RateManualDetail As Domain.Entities.RateManualDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un detalle de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRateManualDetailById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateRateManualDetail(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail)

End Interface
