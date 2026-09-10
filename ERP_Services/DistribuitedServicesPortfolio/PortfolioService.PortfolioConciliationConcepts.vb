
#Region "Imports"
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Partial Class PortfolioService


    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    '''<param name="code">Id del PortfolioConciliationConcepts</param>
    ''' <returns></returns>
    Public Function GetPortfolioConciliationConceptsByCode(code As String, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConcepts.GetPortfolioConciliationConceptsByCode
        Using service As IPortfolioConciliationConceptsAdminService = Container.Current.Resolve(Of IPortfolioConciliationConceptsAdminService)()
            Return service.GetPortfolioConciliationConceptsByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetPortfolioConciliationConceptsById(Id As Integer, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConcepts.GetPortfolioConciliationConceptsById
        Using service As IPortfolioConciliationConceptsAdminService = Container.Current.Resolve(Of IPortfolioConciliationConceptsAdminService)()
            Return service.GetPortfolioConciliationConceptsById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SavePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConcepts.SavePortfolioConciliationConcepts
        Using service As IPortfolioConciliationConceptsAdminService = Container.Current.Resolve(Of IPortfolioConciliationConceptsAdminService)()
            Return service.SavePortfolioConciliationConcepts(PortfolioConciliationConcepts, audit, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioConciliationConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConcepts.ChangeStatePortfolioConciliationConcepts
        Using service As IPortfolioConciliationConceptsAdminService = Container.Current.Resolve(Of IPortfolioConciliationConceptsAdminService)()
            Return service.ChangeStatePortfolioConciliationConcepts(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeletePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConcepts.DeletePortfolioConciliationConcepts
        Using service As IPortfolioConciliationConceptsAdminService = Container.Current.Resolve(Of IPortfolioConciliationConceptsAdminService)()
            Return service.DeletePortfolioConciliationConcepts(PortfolioConciliationConcepts, audit)
        End Using
    End Function


End Class
