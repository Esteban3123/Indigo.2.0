'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractRateManual

    ''' <summary>
    ''' Guarda o Actualiza un manual tarifario
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRateManual(RateManual As Domain.Entities.RateManual, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual)

    ''' <summary>
    ''' Elimina un manual tarifario
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRateManual(RateManual As Domain.Entities.RateManual, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un manual tarifario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRateManual(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual)

    ''' <summary>
    ''' Obtiene un manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRateManualById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateRateManual(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual)

    ''' <summary>
    ''' metodo para pegar en la rejilla del form de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteRateManual(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' metodo para pegar en la rejilla del form de manual de tarifas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteRateManualSurgical(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of RateManualDetailSurgical), List(Of Tuple(Of String, Integer)))

End Interface
