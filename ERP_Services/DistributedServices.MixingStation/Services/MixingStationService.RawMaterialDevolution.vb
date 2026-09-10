Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MixingStationService
    Implements IMixingStationServiceRawMaterialDevolution

    ''' <summary>
    ''' Consulta una devolucion de materia prima por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionByCode(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution) Implements IMixingStationServiceRawMaterialDevolution.GetRawMaterialDevolutionByCode
        Using service As IRawMaterialDevolutionAdminService = Container.Current.Resolve(Of IRawMaterialDevolutionAdminService)()
            Return service.GetRawMaterialDevolutionByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda la devolucion de material prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution) Implements IMixingStationServiceRawMaterialDevolution.SaveRawMaterialDevolution
        Using service As IRawMaterialDevolutionAdminService = Container.Current.Resolve(Of IRawMaterialDevolutionAdminService)()
            Return service.SaveRawMaterialDevolution(rawMaterialDevolution, audit, idSecuence)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los detalles de devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMatertialDevolutionId"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId As Integer) As List(Of RawMaterialDevolutionDetail) Implements IMixingStationServiceRawMaterialDevolution.GetRawMaterialDevolutionDetailByRawMaterialDevolutionId
        Using service As IRawMaterialDevolutionAdminService = Container.Current.Resolve(Of IRawMaterialDevolutionAdminService)()
            Return service.GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y confirma una devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, operatingUnitId As Integer, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution) Implements IMixingStationServiceRawMaterialDevolution.SaveAndConfirmRawMaterialDevolution
        Using service As IRawMaterialDevolutionAdminService = Container.Current.Resolve(Of IRawMaterialDevolutionAdminService)()
            Return service.SaveAndConfirmRawMaterialDevolution(rawMaterialDevolution, operatingUnitId, audit, idSecuence)
        End Using
    End Function

    ''' <summary>
    ''' Anula un documento
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function AnnulateRawMaterialDevolution(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution) Implements IMixingStationServiceRawMaterialDevolution.AnnulateRawMaterialDevolution
        Using service As IRawMaterialDevolutionAdminService = Container.Current.Resolve(Of IRawMaterialDevolutionAdminService)()
            Return service.AnnulateRawMaterialDevolution(code, audit)
        End Using
    End Function
End Class
