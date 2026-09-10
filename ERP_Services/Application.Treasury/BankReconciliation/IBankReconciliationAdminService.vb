#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IBankReconciliationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una conciliación bancaria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliation)

    ''' <summary>
    ''' Obtiene una conciliación bancaria por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationByCode(code As String, ByVal audit As AuditMessage) As ActionResult(Of BankReconciliation)

    ''' <summary>
    ''' Guarda o Actualiza una conciliación bancaria
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBankReconciliation(ByVal BankReconciliation As BankReconciliation, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BankReconciliation)

    ''' <summary>
    ''' Obtiene los detalles de una conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Function GetBankReconciliationDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationDetail))

End Interface
