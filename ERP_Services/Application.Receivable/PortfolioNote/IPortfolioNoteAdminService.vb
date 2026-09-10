'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPortfolioNoteAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para pegar en la rejilla
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="CompanyType"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters As Object()) As ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance))

    ''' <summary>
    ''' obtener una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteById(id As Integer) As PortfolioNote

    ''' <summary>
    ''' obtener una nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteByCode(code As String, audit As AuditMessage) As PortfolioNote

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
    ''' guardar una nota
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePortfolioNote(PortfolioNote As PortfolioNote, audit As AuditMessage, session As SessionValues, Optional idSequence As Integer = 0) As ActionResult(Of PortfolioNote)

    ''' <summary>
    ''' funcion que se encarga de validar la cta del cxc seleccionada
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Function ValidateSelectedByAccountReceivableAccounting(obj As String) As ActionResult(Of Invoice)

End Interface
