#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IBankConciliationConceptsService

    ''' <summary>
    ''' Función que obtiene todos los conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns>Lista de Fabricantess</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllBankConciliationConcepts(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.BankConciliationConcepts)

    ''' <summary>
    ''' Función que obtiene por codigo los conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="Code">Código de la Fabricantes</param>
    ''' <returns>BankConciliationConcepts</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBankConciliationConceptsByCode(Code As String, session As SessionValues) As BankConciliationConcepts

    ''' <summary>
    ''' Función para Almacenar los conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveBankConciliationConcepts(BankConciliationConcepts As Domain.Entities.BankConciliationConcepts, idSequense As Int64, audit As AuditMessage) As ActionResult(Of BankConciliationConcepts)

    ''' <summary>
    ''' Función para Eliminar los conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteBankConciliationConcepts(BankConciliationConcepts As Domain.Entities.BankConciliationConcepts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeBankConciliationConceptsStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BankConciliationConcepts)

End Interface
