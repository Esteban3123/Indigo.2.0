#Region "Imports"

Imports Application.Accounting
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class AccountingService

#Region "Methods"

    Public Function GetHealthSuperParametersById(id As Integer, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IAccountingHealthSuperParameters.GetHealthSuperParametersById
        Using service As IHealthSuperParametersAdminService = Container.Current.Resolve(Of IHealthSuperParametersAdminService)()
            Return service.GetHealthSuperParametersById(id, audit)
        End Using
    End Function

    Public Function GetHealthSuperParametersByCode(code As String, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IAccountingHealthSuperParameters.GetHealthSuperParametersByCode
        Using service As IHealthSuperParametersAdminService = Container.Current.Resolve(Of IHealthSuperParametersAdminService)()
            Return service.GetHealthSuperParametersByCode(code, audit)
        End Using
    End Function

    Public Function SaveHealthSuperParameters(HealthSuperParameters As HealthSuperParameters, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of HealthSuperParameters) Implements IAccountingHealthSuperParameters.SaveHealthSuperParameters
        Using service As IHealthSuperParametersAdminService = Container.Current.Resolve(Of IHealthSuperParametersAdminService)()
            Return service.SaveHealthSuperParameters(HealthSuperParameters, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateHealthSuperParameters(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IAccountingHealthSuperParameters.ChangeStateHealthSuperParameters
        Using service As IHealthSuperParametersAdminService = Container.Current.Resolve(Of IHealthSuperParametersAdminService)()
            Return service.ChangeStateHealthSuperParameters(code, state, audit)
        End Using
    End Function

    Public Function DeleteHealthSuperParameters(HealthSuperParameters As HealthSuperParameters, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IAccountingHealthSuperParameters.DeleteHealthSuperParameters
        Using service As IHealthSuperParametersAdminService = Container.Current.Resolve(Of IHealthSuperParametersAdminService)()
            Return service.DeleteHealthSuperParameters(HealthSuperParameters, audit)
        End Using
    End Function
#End Region

End Class
