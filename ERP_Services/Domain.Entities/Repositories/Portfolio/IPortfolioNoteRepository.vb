'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IPortfolioNoteRepository
    Inherits IRepository(Of PortfolioNote)

    ''' <summary>
    ''' obtiene una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getPortfolioNoteById(id As Integer, Optional tracking As Boolean = True) As PortfolioNote

    ''' <summary>
    ''' obtiene una nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteByCode(code As String) As PortfolioNote

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteDetail)

    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteAccountReceivableAdvance)

    ''' <summary>
    ''' obtiene las distribuciones 
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of PortfolioNoteDistribution)

    ''' <summary>
    ''' metodo para obtener la factura asociada al detalle de la nota
    ''' </summary>
    ''' <param name="PortfolioNoteAccountReceivableAdvanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(PortfolioNoteAccountReceivableAdvanceId As Integer) As Invoice

    ''' <summary>
    ''' Lista notas para la confirmacion masiva
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    Function ListPortfolioNoteMassiveConfirm(listDocuments As List(Of String)) As List(Of PortfolioNote)

    ''' <summary>
    ''' Guarda y confirma una nota
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SavePortfolioNote(xml As String, UserCode As String, CompanyType As Byte) As Entity.Core.Objects.ObjectResult(Of SP_SavePortfolioNote_Result)

    ''' <summary>
    ''' Metodo para el copy y paste de facturas o anticipos de notas de cartera
    ''' </summary>
    ''' <param name="XmlParameter"></param>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPastePortfolioNoteAccountReceivableAdvance(XmlParameter As String, XmlObject As String) As List(Of SP_CopyAndPastePortfolioNoteAccountReceivableAdvance_Result)

    ''' <summary>
    '''Method to query the invoices associated with a credit note
    ''' </summary>
    ''' <param name="listPortfolioNoteAccountReceivableAdvanceId"></param>
    ''' <returns></returns>
    Function GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(listPortfolioNoteAccountReceivableAdvanceId As List(Of Integer)) As List(Of Invoice)
End Interface
