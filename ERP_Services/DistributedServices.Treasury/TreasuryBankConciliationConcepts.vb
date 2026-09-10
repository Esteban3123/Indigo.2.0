Imports Application.Treasury
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class TreasuryService

    ''' <summary>
    ''' Función para Eliminar los conceptos de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteBankConciliationConcepts(BankConciliationConcepts As Domain.Entities.BankConciliationConcepts, audit As AuditMessage) As ActionResult Implements IBankConciliationConceptsService.DeleteBankConciliationConcepts
        Using service As IBankConciliationConceptsAdminService = Container.Current.Resolve(Of IBankConciliationConceptsAdminService)()
            Return service.DeleteBankConciliationConcepts(BankConciliationConcepts, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene un conceptos de conciliación bancaria por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>BankConciliationConcepts</returns>
    ''' <remarks></remarks>
    Public Function GetBankConciliationConceptsByCode(Code As String, session As SessionValues) As Domain.Entities.BankConciliationConcepts Implements IBankConciliationConceptsService.GetBankConciliationConceptsByCode
        Using service As IBankConciliationConceptsAdminService = Container.Current.Resolve(Of IBankConciliationConceptsAdminService)()
            Return service.GetBankConciliationConceptsByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas los conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns>Lista de Fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllBankConciliationConcepts(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.BankConciliationConcepts) Implements IBankConciliationConceptsService.ListAllBankConciliationConcepts
        Using service As IBankConciliationConceptsAdminService = Container.Current.Resolve(Of IBankConciliationConceptsAdminService)()
            Return service.ListAllBankConciliationConcepts()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar un concepto de conciliación bancaria
    ''' </summary>
    ''' <param name="BankConciliationConcepts">Objeto BankConciliationConcepts</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveBankConciliationConcepts(BankConciliationConcepts As Domain.Entities.BankConciliationConcepts, idSequense As Int64, audit As AuditMessage) As ActionResult(Of BankConciliationConcepts) Implements IBankConciliationConceptsService.SaveBankConciliationConcepts
        Using service As IBankConciliationConceptsAdminService = Container.Current.Resolve(Of IBankConciliationConceptsAdminService)()
            Return service.SaveBankConciliationConcepts(BankConciliationConcepts, audit, idSequense)
        End Using
    End Function

    Public Function ChangeBankConciliationConceptsStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BankConciliationConcepts) Implements IBankConciliationConceptsService.ChangeBankConciliationConceptsStatus
        Using service As IBankConciliationConceptsAdminService = Container.Current.Resolve(Of IBankConciliationConceptsAdminService)()
            Return service.ChangeBankConciliationConceptsStatus(Code, state, audit)
        End Using
    End Function

End Class
