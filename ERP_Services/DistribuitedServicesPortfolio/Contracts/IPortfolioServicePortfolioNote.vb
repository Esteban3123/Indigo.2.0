'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IPortfolioServicePortfolioNote

    ''' <summary>
    ''' metodo para pegar en la rejilla
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters As Object()) As ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance))

    ''' <summary>
    ''' obtener una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioNoteById(id As Integer) As PortfolioNote

    ''' <summary>
    ''' obtener una nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioNoteByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioNote

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteDetail)

    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteAccountReceivableAdvance)

    ''' <summary>
    ''' obtiene las distribuciones 
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of PortfolioNoteDistribution)

    ''' <summary>
    ''' guardar una nota
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePortfolioNote(PortfolioNote As Domain.Entities.PortfolioNote, idSequence As Int64, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioNote)

    ''' <summary>
    ''' valida la nota que se selcciona en el combo de facturas de Notas de cartera
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateSelectedByAccountReceivableAccounting(obj As String) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Invoice)

End Interface
