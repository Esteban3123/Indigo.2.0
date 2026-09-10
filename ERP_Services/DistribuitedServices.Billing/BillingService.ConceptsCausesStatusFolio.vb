
#Region "Imports"
Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetConceptsCausesStatusFolioById(Id As Integer, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolio.GetConceptsCausesStatusFolioById
        Using service As IConceptsCausesStatusFolioAdminService = Container.Current.Resolve(Of IConceptsCausesStatusFolioAdminService)()
            Return service.GetConceptsCausesStatusFolioById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Concepto por code
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetConceptsCausesStatusFolioByCode(Code As String, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolio.GetConceptsCausesStatusFolioByCode
        Using service As IConceptsCausesStatusFolioAdminService = Container.Current.Resolve(Of IConceptsCausesStatusFolioAdminService)()
            Return service.GetConceptsCausesStatusFolioByCode(Code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage, idSequence As Int64) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolio.SaveConceptsCausesStatusFolio
        Using service As IConceptsCausesStatusFolioAdminService = Container.Current.Resolve(Of IConceptsCausesStatusFolioAdminService)()
            Return service.SaveConceptsCausesStatusFolio(ConceptsCausesStatusFolio, audit, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateConceptsCausesStatusFolio(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolio.ChangeStateConceptsCausesStatusFolio
        Using service As IConceptsCausesStatusFolioAdminService = Container.Current.Resolve(Of IConceptsCausesStatusFolioAdminService)()
            Return service.ChangeStateConceptsCausesStatusFolio(Id, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolio.DeleteConceptsCausesStatusFolio
        Using service As IConceptsCausesStatusFolioAdminService = Container.Current.Resolve(Of IConceptsCausesStatusFolioAdminService)()
            Return service.DeleteConceptsCausesStatusFolio(ConceptsCausesStatusFolio, audit)
        End Using
    End Function


End Class
