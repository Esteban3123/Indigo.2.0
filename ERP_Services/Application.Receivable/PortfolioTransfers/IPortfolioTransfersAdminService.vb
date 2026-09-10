'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPortfolioTransfersAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para pegar en la rejilla de traslado
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetBillsTransfersCopyPaste(data As List(Of List(Of String)), PortfolioTransfer As PortfolioTransfer, CompanyType As Integer, OperatingUnitId As Integer) As ActionResult(Of List(Of PortfolioTransferDetail))

    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As PortfolioTransfer

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransfersByCode(code As String, audit As AuditMessage) As PortfolioTransfer

    ''' <summary>
    ''' obtiene los detalles del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferDetail)

    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferOtherConcept)

    ''' <summary>
    ''' guardar un traslado
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePortfolioTransfer(PortfolioTransfer As PortfolioTransfer, audit As AuditMessage) As ActionResult(Of PortfolioTransfer)

End Interface
