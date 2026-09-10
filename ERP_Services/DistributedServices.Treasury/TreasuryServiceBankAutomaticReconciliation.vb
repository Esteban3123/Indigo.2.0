Imports Application.Treasury
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Treasury.Model
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceBankReconciliationAutomatic

    Public Function GetBankReconciliationAutomaticById(id As Integer, audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic) Implements ITreasuryServiceBankReconciliationAutomatic.GetBankReconciliationAutomaticById
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetBankReconciliationAutomaticById(id, audit)
        End Using
    End Function

    Public Function GetBankReconciliationAutomaticByCode(code As String, audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic) Implements ITreasuryServiceBankReconciliationAutomatic.GetBankReconciliationAutomaticByCode
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetBankReconciliationAutomaticByCode(code, audit)
        End Using
    End Function

    Public Async Function SaveBankReconciliationAutomaticAsync(BankReconciliationAutomatic As BankReconciliationAutomatic, audit As AuditMessage, Optional idSequense As Long = 0) As Task(Of ActionResult(Of BankReconciliationAutomatic)) Implements ITreasuryServiceBankReconciliationAutomatic.SaveBankReconciliationAutomaticAsync
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return Await service.SaveBankReconciliationAutomaticAsync(BankReconciliationAutomatic, audit, idSequense)
        End Using
    End Function

    Public Function GetBankReconciliationAutomaticDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticDetail)) Implements ITreasuryServiceBankReconciliationAutomatic.GetBankReconciliationAutomaticDetails
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetBankReconciliationAutomaticDetails(criterias)
        End Using
    End Function

    Public Function GetUploadBankStatementsDetailByEntityBankAccountAutomatic(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) Implements ITreasuryServiceBankReconciliationAutomatic.GetUploadBankStatementsDetailByEntityBankAccountAutomatic
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetUploadBankStatementsDetailByUploadBankStatementsId(criterias)
        End Using
    End Function
    ''' <summary>
    ''' Trae las reglas desde bancos
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function GetBankAutomaticRecognitionRules(entityId As Integer) As ActionResult(Of List(Of Domain.Entities.BankAutomaticRecognitionRules)) Implements ITreasuryServiceBankReconciliationAutomatic.GetBankAutomaticRecognitionRules
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetBankAutomaticRecognitionRules(entityId)
        End Using
    End Function
    ''' <summary>
    ''' Crea la nota
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <param name="idOperativeUnit"></param>
    ''' <returns></returns>
    Public Function MakeObjectNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), BankReconciliationAutomatic As BankReconciliationAutomatic, BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), Optional idOperativeUnit As Integer = 0) As ActionResult(Of TreasuryNote) Implements ITreasuryServiceBankReconciliationAutomatic.MakeObjectNoteBank
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.MakeObjectNoteBank(BankAutomaticRecognitionRules, BankReconciliationAutomatic, BankReconciliationAutomaticExtractDetail, idOperativeUnit)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene la secuencia de notas
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <param name="_idOperativeUnit"></param>
    ''' <returns></returns>
    Public Function GetNoteSecuence(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?) Implements ITreasuryServiceBankReconciliationAutomatic.GetNoteSecuence
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetNoteSecuence(idForm, _idOperativeUnit)
        End Using
    End Function

    ''' <summary>
    ''' Función que arma y retorna las partidas pendientes por Conciliar
    ''' </summary>
    ''' <param name="bankAutomaticDetail"></param>
    ''' <param name="bankExtractDetail"></param>
    ''' <returns></returns>
    Public Function CreatePendingItemsToReconciled(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)) As List(Of PendingItemsToReconciled) Implements ITreasuryServiceBankReconciliationAutomatic.CreatePendingItemsToReconciled
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.CreatePendingItemsToReconciled(bankAutomaticDetail, bankExtractDetail)
        End Using
    End Function

    ''' <summary>
    ''' Divide una lista de PendingItemsToReconciled en dos listas separadas según su origen
    ''' </summary>
    ''' <param name="pendingItems"></param>
    ''' <returns>Tupla con lista de BankReconciliationAutomaticDetail y lista de BankReconciliationAutomaticExtractDetail</returns>
    Public Function SplitPendingItemsToReconciled(pendingItems As List(Of PendingItemsToReconciled)) As Tuple(Of List(Of BankReconciliationAutomaticDetail), List(Of BankReconciliationAutomaticExtractDetail)) Implements ITreasuryServiceBankReconciliationAutomatic.SplitPendingItemsToReconciled
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.SplitPendingItemsToReconciled(pendingItems)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las sumas de extratos bancarios que concilian un valor de documentos de tesorería
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Public Function FindSubsetsWithinToleranceA(items As List(Of BankReconciliationAutomaticExtractDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticExtractDetail) Implements ITreasuryServiceBankReconciliationAutomatic.FindSubsetsWithinToleranceA
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.FindSubsetsWithinToleranceA(items, minTarget, maxTarget)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las sumas de documentos de tesorería que concilian un valor de extracto bancario
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Public Function FindSubsetsWithinToleranceB(items As List(Of BankReconciliationAutomaticDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticDetail) Implements ITreasuryServiceBankReconciliationAutomatic.FindSubsetsWithinToleranceB
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.FindSubsetsWithinToleranceB(items, minTarget, maxTarget)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento libro de bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticDetail)) Implements ITreasuryServiceBankReconciliationAutomatic.GetPendingItemsBankBook
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetPendingItemsBankBook(entityBankAccountId, documentDate)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene las partidas pendientes por conciliar del segmento extracto bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) Implements ITreasuryServiceBankReconciliationAutomatic.GetPendingItemsExtract
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.GetPendingItemsExtract(entityBankAccountId, documentDate)
        End Using
    End Function

    ''' <summary>
    ''' Función que realiza las validaciones para la conciliación manual
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    ''' <returns></returns>
    Public Function ValidateManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As ActionResult Implements ITreasuryServiceBankReconciliationAutomatic.ValidateManualReconciliation
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.ValidateManualReconciliation(documentDetails, extractDetails)
        End Using
    End Function

    ''' <summary>
    ''' Función auxiliar que calcula el valor y naturaleza real de un documento considerando ListCashReceipts
    ''' </summary>
    ''' <param name="detail"></param>
    ''' <returns>Tupla con (valor calculado, naturaleza resultante)</returns>
    Public Function CalculateRealValueAndNature(detail As BankReconciliationAutomaticDetail) As Tuple(Of Decimal, Byte) Implements ITreasuryServiceBankReconciliationAutomatic.CalculateRealValueAndNature
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.CalculateRealValueAndNature(detail)
        End Using
    End Function

    ''' <summary>
    ''' Busca coincidencias entre extractos bancarios y documentos de tesorería para conciliación automática
    ''' </summary>
    ''' <param name="extractList">Lista de extractos bancarios</param>
    ''' <param name="documentList">Lista de documentos de tesorería</param>
    ''' <param name="existingAssociations">Lista de asociaciones existentes (opcional)</param>
    ''' <returns>Resultado con las listas actualizadas y las asociaciones (existentes y nuevas)</returns>
    Public Function FindCoincidencesAsync(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), Optional existingAssociations As List(Of BankReconciliationAutomaticAssociation) = Nothing) As Task(Of ActionResult(Of BankReconciliationCoincidencesResult)) Implements ITreasuryServiceBankReconciliationAutomatic.FindCoincidencesAsync
        Using service As IBankReconciliationAutomaticAdminService = Container.Current.Resolve(Of IBankReconciliationAutomaticAdminService)()
            Return service.FindCoincidencesAsync(extractList, documentList, existingAssociations)
        End Using
    End Function
End Class
