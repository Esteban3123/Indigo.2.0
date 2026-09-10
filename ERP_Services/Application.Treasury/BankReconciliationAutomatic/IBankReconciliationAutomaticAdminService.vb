#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Treasury.Model
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IBankReconciliationAutomaticAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una conciliación bancaria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationAutomaticById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic)

    ''' <summary>
    ''' Obtiene una conciliación bancaria por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationAutomaticByCode(code As String, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic)

    ''' <summary>
    ''' Guarda o Actualiza una conciliación bancaria automatica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBankReconciliationAutomaticAsync(ByVal BankReconciliationAutomatic As BankReconciliationAutomatic, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of BankReconciliationAutomatic))

    ''' <summary> 
    ''' Obtiene los detalles de una conciliación bancaria automatica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GetBankReconciliationAutomaticDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticDetail))

    ''' <summary>
    ''' Obtiene los extractos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GetUploadBankStatementsDetailByUploadBankStatementsId(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail))
    ''' <summary>
    ''' Obtiene las reglas de bancos
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function GetBankAutomaticRecognitionRules(entityId As Integer) As ActionResult(Of List(Of BankAutomaticRecognitionRules))
    ''' <summary>
    ''' Crea la nota de tesoreria
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <returns></returns>
    Function MakeObjectNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), BankReconciliationAutomatic As BankReconciliationAutomatic, BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), Optional idOperativeUnit As Integer = 0) As ActionResult(Of TreasuryNote)
    ''' <summary>
    ''' Crea los detalles de la nota de
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <returns></returns>
    Function MakeObjectDetailNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), Optional entityBankAccount As EntityBankAccounts = Nothing) As List(Of TreasuryNoteDetail)
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <param name="_idOperativeUnit"></param>
    ''' <returns></returns>
    Function GetNoteSecuence(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?)
    ''' <summary>
    ''' Método que arma las partidas pendientes por Conciliar
    ''' </summary>
    ''' <param name="bankAutomaticDetail"></param>
    ''' <param name="bankExtractDetail"></param>
    ''' <returns></returns>
    Function CreatePendingItemsToReconciled(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)) As List(Of PendingItemsToReconciled)
    ''' <summary>
    ''' Divide una lista de PendingItemsToReconciled en dos listas separadas según su origen
    ''' </summary>
    ''' <param name="pendingItems"></param>
    ''' <returns>Tupla con lista de BankReconciliationAutomaticDetail y lista de BankReconciliationAutomaticExtractDetail</returns>
    Function SplitPendingItemsToReconciled(pendingItems As List(Of PendingItemsToReconciled)) As Tuple(Of List(Of BankReconciliationAutomaticDetail), List(Of BankReconciliationAutomaticExtractDetail))
    ''' <summary>
    ''' Obtiene los conjuntos de sumas de extractos bancarios que pueden conciliar un valor de Documentos de Tesorería
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Function FindSubsetsWithinToleranceA(items As List(Of BankReconciliationAutomaticExtractDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticExtractDetail)

    ''' <summary>
    ''' Obtiene conjuntos de sumas de documentos de Tesorería que pueden conciliar un valor del Extracto Bancario
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Function FindSubsetsWithinToleranceB(items As List(Of BankReconciliationAutomaticDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticDetail)

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento libro de bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticDetail))

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento extracto bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail))

    ''' <summary>
    ''' Función que realiza las validaciones para la conciliación manual
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    ''' <returns></returns>
    Function ValidateManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As ActionResult

    ''' <summary>
    ''' Función auxiliar que calcula el valor y naturaleza real de un documento considerando ListCashReceipts
    ''' </summary>
    ''' <param name="detail"></param>
    ''' <returns>Tupla con (valor calculado, naturaleza resultante)</returns>
    Function CalculateRealValueAndNature(detail As BankReconciliationAutomaticDetail) As Tuple(Of Decimal, Byte)

    ''' <summary>
    ''' Busca coincidencias entre extractos bancarios y documentos de tesorería para conciliación automática
    ''' </summary>
    ''' <param name="extractList">Lista de extractos bancarios</param>
    ''' <param name="documentList">Lista de documentos de tesorería</param>
    ''' <param name="existingAssociations">Lista de asociaciones existentes (opcional)</param>
    ''' <returns>Resultado con las listas actualizadas y las asociaciones (existentes y nuevas)</returns>
    Function FindCoincidencesAsync(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), Optional existingAssociations As List(Of BankReconciliationAutomaticAssociation) = Nothing) As Task(Of ActionResult(Of BankReconciliationCoincidencesResult))

End Interface
