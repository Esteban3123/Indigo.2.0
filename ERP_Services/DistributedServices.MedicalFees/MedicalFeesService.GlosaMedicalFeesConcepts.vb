
#Region "Imports"
Imports Application.MedicalFees
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class MedicalFeesService


    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    '''<param name="code">Id del GlosaMedicalFeesConcepts</param>
    ''' <returns></returns>
    Public Function GetGlosaMedicalFeesConceptsByCode(code As String, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IMedicalGlosaMedicalFeesConcepts.GetGlosaMedicalFeesConceptsByCode
        Using service As IGlosaMedicalFeesConceptsAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesConceptsAdminService)()
            Return service.GetGlosaMedicalFeesConceptsByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetGlosaMedicalFeesConceptsById(Id As Integer, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IMedicalGlosaMedicalFeesConcepts.GetGlosaMedicalFeesConceptsById
        Using service As IGlosaMedicalFeesConceptsAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesConceptsAdminService)()
            Return service.GetGlosaMedicalFeesConceptsById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IMedicalGlosaMedicalFeesConcepts.SaveGlosaMedicalFeesConcepts
        Using service As IGlosaMedicalFeesConceptsAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesConceptsAdminService)()
            Return service.SaveGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts, audit, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateGlosaMedicalFeesConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IMedicalGlosaMedicalFeesConcepts.ChangeStateGlosaMedicalFeesConcepts
        Using service As IGlosaMedicalFeesConceptsAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesConceptsAdminService)()
            Return service.ChangeStateGlosaMedicalFeesConcepts(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IMedicalGlosaMedicalFeesConcepts.DeleteGlosaMedicalFeesConcepts
        Using service As IGlosaMedicalFeesConceptsAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesConceptsAdminService)()
            Return service.DeleteGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts, audit)
        End Using
    End Function


End Class
