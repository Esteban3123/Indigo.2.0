'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IPortfolioServicePortfolioTransfers

    ''' <summary>
    ''' metodo para pegar en la rejilla de traslado
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SetBillsTransfersCopyPaste(data As List(Of List(Of String)), PortfolioTransfer As PortfolioTransfer, CompanyType As Integer, OperatingUnitId As Integer) As ActionResult(Of List(Of PortfolioTransferDetail))

    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As PortfolioTransfer

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioTransfersByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioTransfer

    ''' <summary>
    ''' obtiene los detalles del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferDetail)

    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferOtherConcept)

    ''' <summary>
    ''' guardar un traslado
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePortfolioTransfer(PortfolioTransfer As Domain.Entities.PortfolioTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioTransfer)

End Interface
