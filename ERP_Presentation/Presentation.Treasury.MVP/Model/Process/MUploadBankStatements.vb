#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports System.ServiceModel
Imports Presentation.Controls.MVP
Imports RestSharp
Imports System.Net

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MUploadBankStatements
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Methods"

    Public Async Function SetCopyPasteOrImportFileUploadBankStatementsDetail(dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.UploadBankStatementsDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SetCopyPasteOrImportFileSetBankStatementsDetailAsync(Indigo, dataImportFile, dataCopyPaste)
    End Function

    Public Async Function GetUploadBankStatementsByCode(code As String) As Task(Of UploadBankStatements)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetUploadBankStatementsByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function SaveUploadBankStatements(UploadBankStatements As UploadBankStatements) As Task(Of ActionResult(Of UploadBankStatements))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveUploadBankStatementsAsync(UploadBankStatements, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetUploadBankStatementsDetail(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId)
    End Function
    ''' <summary>
    ''' Función que obtiene los Conceptos de Conciliación asociados al banco
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    Public Async Function GetBankConciliationConceptsByEntityBankAccountsId(EntityBankAccountId As Integer) As Task(Of ActionResult(Of List(Of BankConciliationConcepts)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankConciliationConceptsByEntityBankAccountIdAsync(EntityBankAccountId)
    End Function
    ''' <summary>
    ''' Función que se encarga de asignar el DocumentType acorde a la descripción de las transacciones
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    Public Async Function MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts As List(Of BankConciliationConcepts), UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As Task(Of ActionResult(Of List(Of UploadBankStatementsDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.MatchConciliationConceptsWithDescriptionTransactionAsync(ConciliationConcepts, UploadBankStatementDetails)
    End Function
    ''' <summary>
    ''' Función que me obtiene los Cargues del extracto bancario por entidad Bancaria y Periodo
    ''' que se encuentran confirmados o guardados
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetUploadBankStatementsByPeriod(ByVal IdEntityBankAccount As Integer, ByVal Year As Integer, ByVal Month As Integer) As Task(Of UploadBankStatementsXpo)
        Dim filter As String = "EntityBankAccountId.Id =" & IdEntityBankAccount & "And Year = " & Year & "And Month = " & Month & "And Status <> 3"
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetXPOObject(Of UploadBankStatementsXpo)(filter))
    End Function
    ''' <summary>
    ''' Función que obtiene la conciliación automática bancaría por Entidad de Cuenta Bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount"></param>
    ''' <returns></returns>
    Public Async Function GetConciliationBankByEntityBankAccount(ByVal IdEntityBankAccount As Integer) As Task(Of BankReconciliationAutomaticExtractDetailXpo)
        Dim filter As String = "BankReconciliationAutomaticId.EntityBankAccountId =" & IdEntityBankAccount
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetXPOObject(Of BankReconciliationAutomaticExtractDetailXpo)(filter))
    End Function


    ''' <summary>
    ''' Metodo que envia archivo a API para extraer detalle de extracto bancario.
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Async Function ExtractBankStatementDetail(ByVal fileName As String, ByVal Year As String) As Task(Of ActionResult(Of String))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Bank_Statement)

        Dim client = New RestClient(String.Format("{0}/groupby_items", endpoint.UrlBase))
        Dim req = New RestRequest(Method.POST)
        req.AddFile("file", fileName, ParameterType.RequestBody)
        req.AddParameter("Year", Year)


        Try
            Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)

            If response?.StatusCode <> HttpStatusCode.OK Is Nothing Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = response?.StatusDescription}
            End If
            Return New ActionResult(Of String) With {.StateResult = If(response?.StatusCode = 200, True, False), .Message = response.Content}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class