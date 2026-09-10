'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractMarketingUnit

    ''' <summary>
    ''' Guarda o Actualiza una unidad de mercadeo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMarketingUnit(MarketingUnit As Domain.Entities.MarketingUnit, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit)

    ''' <summary>
    ''' Elimina una unidad de mercadeo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMarketingUnit(MarketingUnit As Domain.Entities.MarketingUnit, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una unidad de mercadeo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMarketingUnit(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit)

    ''' <summary>
    ''' Obtiene una unidad de mercadeo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMarketingUnitById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateMarketingUnit(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit)

End Interface
