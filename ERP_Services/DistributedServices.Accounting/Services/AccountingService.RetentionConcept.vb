#Region "Imports"

Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports DistributedServices.Accounting
Imports Domain.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountingService
    Implements IAccountingRetentionConcept

#Region "Methods"

    ''' <summary>
    ''' Elimina un concepto de retencion
    ''' </summary>
    ''' <returns></returns>
    Public Function DeleteRetentionConcept(retentionConcept As Domain.Entities.RetentionConcepts) As Domain.Base.Entities.ActionResult Implements IAccountingRetentionConcept.DeleteRetentionConcept
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteRetentionConcept(retentionConcept, audit)
        End Using
        'Return Me._retentionConceptAdminService.DeleteRetentionConcept(retentionConcept, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de retencion
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetRetentionConcept(code As String) As Domain.Entities.RetentionConcepts Implements IAccountingRetentionConcept.GetRetentionConcept
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetRetentionConcept(code, audit)
        End Using
        'Return Me._retentionConceptAdminService.GetRetentionConcept(code, audit)
    End Function

    ''' <summary>
    ''' Saves the retention concept.
    ''' </summary>
    ''' <param name="retentionConcept">The retention concept.</param>
    ''' <returns></returns>
    Public Function SaveRetentionConcept(retentionConcept As Domain.Entities.RetentionConcepts) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RetentionConcepts) Implements IAccountingRetentionConcept.SaveRetentionConcept
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveRetentionConcept(retentionConcept, audit, idSequense)
        End Using
        'Return Me._retentionConceptAdminService.SaveRetentionConcept(retentionConcept, audit, idSequense)
    End Function

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionById(id As Integer, Optional auditParam As AuditMessage = Nothing) As Domain.Entities.RetentionConcepts Implements IAccountingRetentionConcept.GetRetentionById
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Dim audit As AuditMessage = If(auditParam IsNot Nothing, auditParam, OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE))
            Return service.GetRetentionById(id, audit)
        End Using
        'Return Me._retentionConceptAdminService.GetRetentionById(id, audit)
    End Function

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity Implements IAccountingRetentionConcept.GetRetentionConceptByCity
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Return service.GetRetentionConceptByCity(id, addressId)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateRetentionConcept(code As String, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RetentionConcepts) Implements IAccountingRetentionConcept.ChangeStateRetentionConcept
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.ChangeStateRetentionConcept(code, state, audit)
        End Using
        'Return Me._retentionConceptAdminService.ChangeStateRetentionConcept(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.RetentionConceptRanges)) Implements IAccountingRetentionConcept.GetListRetentionRangeByRetentionConceptId
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Return service.GetListRetentionRangeByRetentionConceptId(RetentionConceptId)
        End Using
        'Return Me._retentionConceptAdminService.GetListRetentionRangeByRetentionConceptId(RetentionConceptId)
    End Function

    Public Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts Implements IAccountingRetentionConcept.GetRetentionConceptByIdBrachOfficeId
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Return service.GetRetentionConceptByIdBrachOfficeId(brachOfficeId)
        End Using
        'Return Me._retentionConceptAdminService.GetRetentionConceptByIdBrachOfficeId(brachOfficeId)
    End Function

    Public Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts Implements IAccountingRetentionConcept.GetIVARetentionConceptByThirdPartyId
        Using service As IRetentionConceptAdminService = Container.Current.Resolve(Of IRetentionConceptAdminService)()
            Return service.GetIVARetentionConceptByThirdPartyId(thirdPartyId)
        End Using
        'Return Me._retentionConceptAdminService.GetIVARetentionConceptByThirdPartyId(thirdPartyId)
    End Function

#End Region

End Class
