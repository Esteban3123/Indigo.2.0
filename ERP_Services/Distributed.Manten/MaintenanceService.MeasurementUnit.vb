Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteMeasurementUnit(Empresa As String, MeasurementUnit As Domain.Entities.MeasurementUnit, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IMeasurementUnitService.DeleteMeasurementUnit
        Using DeleteMeasurementAdmin As IMeasurementUnitAdminService = Container.Current.Resolve(Of IMeasurementUnitAdminService)()
            Return DeleteMeasurementAdmin.DeleteMeasurementUnit(MeasurementUnit, audit)
        End Using
    End Function

    Public Function GetMeasurementUnit(Empresa As String, codeMeasurementUnit As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.MeasurementUnit Implements IMeasurementUnitService.GetMeasurementUnit
        Using DeleteMeasurementAdmin As IMeasurementUnitAdminService = Container.Current.Resolve(Of IMeasurementUnitAdminService)()
            Return DeleteMeasurementAdmin.GetMeasurementUnit(codeMeasurementUnit, audit)
        End Using
    End Function

    Public Function ListAllMeasurementUnit(Empresa As String) As List(Of Domain.Entities.MeasurementUnit) Implements IMeasurementUnitService.ListAllMeasurementUnit
        Using DeleteMeasurementAdmin As IMeasurementUnitAdminService = Container.Current.Resolve(Of IMeasurementUnitAdminService)()
            Return DeleteMeasurementAdmin.ListAllMeasurementUnit
        End Using
    End Function

    Public Function SaveMeasurementUnit(Empresa As String, MeasurementUnit As Domain.Entities.MeasurementUnit, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.MeasurementUnit) Implements IMeasurementUnitService.SaveMeasurementUnit
        Using DeleteMeasurementAdmin As IMeasurementUnitAdminService = Container.Current.Resolve(Of IMeasurementUnitAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return DeleteMeasurementAdmin.SaveMeasurementUnit(MeasurementUnit, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateMeasurementUnit(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MeasurementUnit) Implements IMeasurementUnitService.ChangeStateMeasurementUnit
        Using DeleteMeasurementAdmin As IMeasurementUnitAdminService = Container.Current.Resolve(Of IMeasurementUnitAdminService)()
            Return DeleteMeasurementAdmin.ChangeStateMeasurementUnit(code, state, audit)
        End Using
    End Function
End Class
