'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IPortfolioTransferRepository
    Inherits IRepository(Of PortfolioTransfer)

    Function ListPortfolioTransferMassiveConfirm(listDocuments As List(Of String)) As List(Of PortfolioTransfer)

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransfersByCode(code As String) As PortfolioTransfer
    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As PortfolioTransfer
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
    ''' Gets the portfolio transfer detail by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetPortfolioTransferDetailById(Id As Integer) As PortfolioTransferDetail

    ''' <summary>
    ''' Gets the portfolio transfer detail by account receivable identifier.
    ''' </summary>
    ''' <param name="AccountReceivableId">The account receivable identifier.</param>
    ''' <returns></returns>
    Function GetPortfolioTransferDetailByAccountReceivableId(AccountReceivableId As Integer) As List(Of PortfolioTransferDetail)

    ''' <summary>
    ''' Valida el CopyPaste del form de traslados de cartera
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteTransfer(xmlObject As String, TransferType As Integer, CompanyType As Integer) As List(Of SP_CopyAndPasteTransfer_Result)

    ''' <summary>
    ''' Guarda el cruce de anticipo vs cxc
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SavePortfolioTransfer(xmlObject As String, codeUser As String, companyType As Byte) As SP_SavePortfolioTransfer_Result

End Interface
