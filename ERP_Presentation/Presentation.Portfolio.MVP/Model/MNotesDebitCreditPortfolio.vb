'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 06-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports System.Dynamic

#End Region

Public Class MNotesDebitCreditPortfolio
    Implements IDisposable


#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    Public Function GetPortfolioAccountReceivableShareById(id As Integer) As PortfolioAccountReceivableShareXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAccountReceivableShareById(id)
    End Function

    Public Function GetPortfolioAccountReceivableById(id As Integer) As Infrastructure.Data.Xpo.PortfolioRepository.PortfolioAccountReceivableXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetAccountReceivableById(id)
    End Function

    Public Function GetPortfolioAccountReceivableAccountingById(id As Integer) As PortfolioAccountReceivableAccountingXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAccountReceivableAccountingById(id)
    End Function

    Public Function GetPortfolioAdvanceById(id As Integer) As Portfolio_PortfolioAdvance
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAdvanceById(id)
    End Function

    Public Function GetInvoiceById(id As Integer) As InvoiceXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetInvoiceById(id)
    End Function

    Public Async Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, parameters As List(Of Object)) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioNoteAccountReceivableAdvance)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SetCopyPasteOrImportFilePortfolioNoteAsync(dataImportFile, dataCopyPaste, CompanyType, OperatingUnit, parameters)
    End Function
    ''' <summary>
    ''' obtener la nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioNoteByCode(code As String) As Task(Of PortfolioNote)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtener la nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioNoteById(id As Integer) As PortfolioNote
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteById(id)
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote)
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of PortfolioNoteDistribution)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId)
    End Function
    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteAccountReceivableAdvance)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote)
    End Function
    ''' <summary>
    ''' guardar una nota
    ''' </summary>
    ''' <param name="PortfolioNote"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePortfolioNote(PortfolioNote As PortfolioNote, idSequence As Integer) As Task(Of ActionResult(Of PortfolioNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioNoteAsync(PortfolioNote, idSequence, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="accountReceivableAccountingId"></param>
    ''' <param name="noteType"></param>
    ''' <param name="CurrencyId"></param>
    ''' <param name="noteDate"></param>
    ''' <returns></returns>
    Public Async Function ValidateSelectedByAccountReceivableAccounting(accountReceivableAccountingId As Integer, noteType As Integer, CurrencyId As Integer, noteDate As Date) As Task(Of ActionResult(Of Invoice))
        Dim args As Object = New ExpandoObject()
        args.AccountReceivableAccountingId = accountReceivableAccountingId
        args.NoteType = noteType
        args.CurrencyId = CurrencyId
        args.NoteDate = noteDate
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ValidateSelectedByAccountReceivableAccountingAsync(parameter)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
