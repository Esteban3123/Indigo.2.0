#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Interface IBankConciliationConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todos los conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns>Lista de  conceptos de conciliación bancaria</returns>
    ''' <remarks></remarks>
    Function ListAllBankConciliationConcepts() As List(Of BankConciliationConcepts)

    ''' <summary>
    ''' Función que obtiene por codigo el conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="Code">Código del concepto de conciliación bancaria</param>
    ''' <returns>BankConciliationConcepts</returns>
    ''' <remarks></remarks>
    Function GetBankConciliationConceptsByCode(Code As String) As BankConciliationConcepts

    ''' <summary>
    ''' Función para Almacenar un concepto de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveBankConciliationConcepts(BankConciliationConcepts As BankConciliationConcepts, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BankConciliationConcepts)

    ''' <summary>
    ''' Función para Eliminar los conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteBankConciliationConcepts(BankConciliationConcepts As BankConciliationConcepts, audit As AuditMessage) As Domain.Base.Entities.ActionResult


    ''' <summary>
    ''' Cambiar el Estado del concepto de conciliación bancaria
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeBankConciliationConceptsStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BankConciliationConcepts)
End Interface
