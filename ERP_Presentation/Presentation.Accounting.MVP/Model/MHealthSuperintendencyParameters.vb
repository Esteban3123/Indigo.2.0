#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class MHealthSuperParameters
    Implements IDisposable


#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GeneralLedgerSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un formato por id
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetHealthSuperParametersById(ByVal id As Integer) As ActionResult(Of HealthSuperParameters)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetHealthSuperParametersById(id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un formato por código
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetHealthSuperParametersByCode(ByVal code As String) As Task(Of ActionResult(Of Domain.Entities.HealthSuperParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetHealthSuperParametersByCodeAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un formato de exógena
    ''' </summary>
    ''' <param name="HealthSuperParameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveHealthSuperParameters(ByVal HealthSuperParameters As Domain.Entities.HealthSuperParameters, Optional ByVal idSequense As Int64 = 0) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthSuperParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveHealthSuperParametersAsync(HealthSuperParameters, _indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.HealthSuperParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ChangeStateHealthSuperParametersAsync(code, state, _indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function Delete(ByVal HealthSuperParameters As HealthSuperParameters) As Task(Of ActionResult(Of Domain.Entities.HealthSuperParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteHealthSuperParametersAsync(HealthSuperParameters, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the block record accounting.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordAccounting(ByVal idForm As String, ByVal idRecord As String) As Task(Of Domain.Entities.BlockRecordGeneralLedger)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordAccounting(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult(Of Domain.Entities.BlockRecordGeneralLedger))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordAccounting(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
    End Function

    Public Async Function GenerateFormatExogena(criterias As Dictionary(Of String, String)) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateExogenaFormatsAsync(criterias, _indigoSessionValues)
    End Function

    Public Function JournalVouchersByMainAccount(MainAccountId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.JournalVouchersByMainAccount(MainAccountId)
    End Function

    Public Function ListAccounts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsByClass()
    End Function

    Public Function ThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListAllThirdParty()
    End Function

    Public Function JournalVouchersById(JournalVouchersId As String, IdMainAccount As Integer) As List(Of ViewJournalVouchersByMainAccountXpo)
        Dim Filter As String = "JournalVouchersId IN (" & JournalVouchersId & ") AND IdMainAccount = " & IdMainAccount
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of ViewJournalVouchersByMainAccountXpo)(Nothing, Filter)
    End Function

    Public Function ListAccountsById(Ids As String) As List(Of PUCServiceXpo)
        Dim Filter As String = "Id IN (" & Ids & ")"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of PUCServiceXpo)(Nothing, Filter)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
