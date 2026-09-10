'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 12/05/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.CloudAgent
#End Region

Public Class MBankConciliationAutomatic
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
    ''' Obtiene una conciliación por id
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetBankReconciliationAutomaticById(ByVal id As Integer) As ActionResult(Of BankReconciliationAutomatic)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationAutomaticById(id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una conciliación por código
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetBankReconciliationAutomaticByCode(ByVal code As String) As Task(Of ActionResult(Of BankReconciliationAutomatic))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationAutomaticByCodeAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un formato de exógena
    ''' </summary>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveBankReconciliationAutomatic(ByVal BankReconciliationAutomatic As BankReconciliationAutomatic, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of BankReconciliationAutomatic))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBankReconciliationAutomaticAsync(BankReconciliationAutomatic, _indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de una conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Async Function ListBankReconciliationAutomaticDetail(ByVal criterias As Dictionary(Of String, String)) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationAutomaticDetailsAsync(criterias)
    End Function

    ''' <summary>
    ''' Obtiene los detalles del cargue de Extracto Bancario
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Async Function GetUploadBankStatementsDetailByEntityBankAccountAutomatic(ByVal criterias As Dictionary(Of String, String)) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetUploadBankStatementsDetailByEntityBankAccountAutomaticAsync(criterias)
    End Function

    ''' <summary>
    ''' Obtiene el cargue de extracto bancario por cuenta bancaria y fecha
    ''' </summary>
    ''' <param name="IdBank"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Public Async Function GetUploadBankStatementsByEntityBankAccountAndPeriod(ByVal IdBank As Integer, ByVal Month As Integer, ByVal Year As Integer) As Task(Of UploadBankStatementsXpo)
        Dim Filter As String = "EntityBankAccountId.Id =" & IdBank & "And Month =" & Month & "And Year =" & Year
        Return Await Task.Run(Function() XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetXPOObject(Of UploadBankStatementsXpo)(Filter))
    End Function

    ''' <summary>
    ''' Obtiene la Conciliación Bancaria Automática por entidad bancaria
    ''' </summary>
    ''' <param name="IdBank"></param>
    ''' <returns></returns>
    Public Async Function GetBankConciliationAutomaticByBank(ByVal IdBank As Integer) As Task(Of List(Of BankReconciliationAutomaticXpo))
        Dim Filter As String = "EntityBankAccountId.Id =" & IdBank
        Return Await Task.Run(Function() XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetCollectionAsList(Of BankReconciliationAutomaticXpo)(Nothing, Filter))
    End Function

    ''' <summary>
    ''' Obtiene la Entidad Bancaria por el Id
    ''' </summary>
    ''' <param name="IdBank"></param>
    ''' <returns></returns>
    Public Async Function GetEntityBankAccountById(ByVal IdBank As Integer) As Task(Of EntityBankAccountXpo)
        Dim Filter As String = "Id =" & IdBank
        Return Await Task.Run(Function() XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetXPOObject(Of EntityBankAccountXpo)(Filter))
    End Function


    Public Async Function GetBankReconciliationAutomaticByEntityBankAccountId(ByVal code As String) As Task(Of ActionResult(Of BankReconciliationAutomatic))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankReconciliationAutomaticByCodeAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene las reglas de reconocimiento
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Async Function GetBankAutomaticRecognitionRules(ByVal entityId As Integer) As Task(Of ActionResult(Of List(Of BankAutomaticRecognitionRules)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBankAutomaticRecognitionRulesAsync(entityId)
    End Function
    ''' <summary>
    ''' Crea la nota
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <param name="idOperativeUnit"></param>
    ''' <returns></returns>
    Public Async Function MakeObjectTreasuryNote(ByVal BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), ByVal BankReconciliationAutomatic As BankReconciliationAutomatic, ByVal BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), ByVal idOperativeUnit As Integer) As Task(Of ActionResult(Of TreasuryNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.MakeObjectNoteBankAsync(BankAutomaticRecognitionRules, BankReconciliationAutomatic, BankReconciliationAutomaticExtractDetail, idOperativeUnit)
    End Function
    ''' <summary>
    ''' Trae la secuencia de tesoreria
    ''' </summary>
    ''' <param name="idform"></param>
    ''' <param name="_idOperativeUnit"></param>
    ''' <returns></returns>
    Public Async Function GetTreasurySecuence(ByVal idform As String, Optional _idOperativeUnit As Integer? = Nothing) As Task(Of ActionResult(Of Integer?))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNoteSecuenceAsync(idform, _idOperativeUnit)
    End Function

    ''' <summary>
    ''' Función que arma y retorna las partidas pendientes por conciliar
    ''' </summary>
    ''' <param name="bankAutomaticDetail"></param>
    ''' <param name="bankExtractDetail"></param>
    ''' <returns></returns>
    Public Async Function CreatePendingItemsToReconciled(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)) As Task(Of List(Of PendingItemsToReconciled))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.CreatePendingItemsToReconciledAsync(bankAutomaticDetail, bankExtractDetail)
    End Function

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento Libro de Bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Async Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetPendingItemsBankBookAsync(entityBankAccountId, documentDate)
    End Function

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento Extracto Bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Async Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetPendingItemsExtractAsync(entityBankAccountId, documentDate)
    End Function

    ''' <summary>
    ''' Función que realiza las validaciones para la conciliación manual
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    ''' <returns></returns>
    Public Async Function ValidateManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ValidateManualReconciliationAsync(documentDetails, extractDetails)
    End Function

    ''' <summary>
    ''' Divide una lista de PendingItemsToReconciled en dos listas separadas según su origen
    ''' </summary>
    ''' <param name="pendingItems"></param>
    ''' <returns></returns>
    Public Async Function SplitPendingItemsToReconciled(pendingItems As List(Of PendingItemsToReconciled)) As Task(Of Tuple(Of List(Of BankReconciliationAutomaticDetail), List(Of BankReconciliationAutomaticExtractDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SplitPendingItemsToReconciledAsync(pendingItems)
    End Function

    ''' <summary>
    ''' Función que realiza el proceso de Conciliación Automática
    ''' </summary>
    ''' <param name="extractList"></param>
    ''' <param name="documentList"></param>
    ''' <returns></returns>
    Public Async Function FindCoincidences(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associationList As List(Of BankReconciliationAutomaticAssociation)) As Task(Of ActionResult(Of BankReconciliationCoincidencesResult))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.FindCoincidencesAsync(extractList, documentList, associationList)
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