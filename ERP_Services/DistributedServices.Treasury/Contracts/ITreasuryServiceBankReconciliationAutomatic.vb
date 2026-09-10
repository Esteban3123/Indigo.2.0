#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Treasury.Model
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface ITreasuryServiceBankReconciliationAutomatic

    ''' <summary>
    ''' Obtiene una conciliación bancaria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBankReconciliationAutomaticById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic)

    ''' <summary>
    ''' Obtiene una conciliación bancaria por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBankReconciliationAutomaticByCode(code As String, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic)

    ''' <summary>
    ''' Guarda o Actualiza una conciliación bancaria
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBankReconciliationAutomaticAsync(ByVal BankReconciliationAutomatic As BankReconciliationAutomatic, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of BankReconciliationAutomatic))

    ''' <summary>
    ''' Obtiene los detalles de una conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankReconciliationAutomaticDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticDetail))

    ''' <summary>
    ''' Obtiene los extractos bancarios
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUploadBankStatementsDetailByEntityBankAccountAutomatic(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail))

    ''' <summary>
    ''' Obtiene las reglas bancarias
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankAutomaticRecognitionRules(entityId As Integer) As ActionResult(Of List(Of Domain.Entities.BankAutomaticRecognitionRules))
    ''' <summary>
    ''' Metodo que crea nota
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <param name="idOperativeUnit"></param>
    ''' <returns></returns>
    <OperationContract>
    Function MakeObjectNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), BankReconciliationAutomatic As BankReconciliationAutomatic,
                                BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), Optional idOperativeUnit As Integer = 0) As ActionResult(Of TreasuryNote)
    ''' <summary>
    ''' Obtiene Secuencia de tesoreria
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <param name="_idOperativeUnit"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetNoteSecuence(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?)

    ''' <summary>
    ''' Método que arma las partidas pendientes por conciliar
    ''' </summary>
    ''' <param name="bankAutomaticDetail"></param>
    ''' <param name="bankExtractDetail"></param>
    ''' <returns></returns>
    <OperationContract>
    Function CreatePendingItemsToReconciled(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)) As List(Of PendingItemsToReconciled)

    ''' <summary>
    ''' Divide una lista de PendingItemsToReconciled en dos listas separadas según su origen
    ''' </summary>
    ''' <param name="pendingItems"></param>
    ''' <returns>Tupla con lista de BankReconciliationAutomaticDetail y lista de BankReconciliationAutomaticExtractDetail</returns>
    <OperationContract>
    Function SplitPendingItemsToReconciled(pendingItems As List(Of PendingItemsToReconciled)) As Tuple(Of List(Of BankReconciliationAutomaticDetail), List(Of BankReconciliationAutomaticExtractDetail))

    ''' <summary>
    ''' Obtiene las sumas de extratos bancarios que concilian un valor de documentos de tesorería
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    <OperationContract>
    Function FindSubsetsWithinToleranceA(items As List(Of BankReconciliationAutomaticExtractDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticExtractDetail)

    ''' <summary>
    ''' Obtiene las sumas de documentos de tesorería que concilian un valor de extracto bancario
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    <OperationContract>
    Function FindSubsetsWithinToleranceB(items As List(Of BankReconciliationAutomaticDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticDetail)

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento libro de bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticDetail))

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento extracto bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail))

    ''' <summary>
    ''' Función que realiza las validaciones para la conciliación manual
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ValidateManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As ActionResult

    ''' <summary>
    ''' Función auxiliar que calcula el valor y naturaleza real de un documento considerando ListCashReceipts
    ''' </summary>
    ''' <param name="detail"></param>
    ''' <returns>Tupla con (valor calculado, naturaleza resultante)</returns>
    <OperationContract>
    Function CalculateRealValueAndNature(detail As BankReconciliationAutomaticDetail) As Tuple(Of Decimal, Byte)

End Interface
