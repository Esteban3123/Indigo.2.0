'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-07-23
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Función que obtiene un indicio de deterioro por Código
    ''' </summary>
    ''' <param name="Code">Código del indicio de deterioro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeteriorationIndicationByCode(Code As String) As Domain.Entities.DeteriorationIndications Implements IFixedAssetDeteriorationIndicationService.GetDeteriorationIndicationByCode
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.GetDeteriorationIndication(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas los indices de deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllDeteriorationIndications(audit As AuditMessage) As List(Of Domain.Entities.DeteriorationIndications) Implements IFixedAssetDeteriorationIndicationService.ListAllDeteriorationIndications
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.ListAllDeteriorationIndications()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar una Indice de deterioro
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetDeteriorationIndication(DeteriorationIndication As Domain.Entities.DeteriorationIndications, idSequense As Int64, audit As AuditMessage) As ActionResult(Of DeteriorationIndications) Implements IFixedAssetDeteriorationIndicationService.SaveFixedAssetDeteriorationIndication
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.SaveDeteriorationIndication(DeteriorationIndication, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeteriorationIndicationChangeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of DeteriorationIndications) Implements IFixedAssetDeteriorationIndicationService.DeteriorationIndicationChangeState
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.DeteriorationIndicationChangeState(Code, State, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función para Eliminar un indicio de deterioro
    ''' </summary>
    ''' <param name="DeteriorationIndication"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteFixedAssetDeteriorationIndication(DeteriorationIndication As Domain.Entities.DeteriorationIndications, audit As AuditMessage) As ActionResult Implements IFixedAssetDeteriorationIndicationService.DeleteFixedAssetDeteriorationIndication
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.DeleteDeteriorationIndication(DeteriorationIndication, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Indicios de Deterioro de Activos Fijos.
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="ReportType"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="LegalBookId"></param>
    ''' <param name="InitialMainAccount"></param>
    ''' <param name="FinalMainAccount"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GetListReportDeteriorationIndicationsByPhysicalAsset(Year As Integer, Month As Integer, ReportType As Integer, InitialPlate As String, FinalPlate As String, LegalBookId As Integer, InitialMainAccount As String, FinalMainAccount As String, session As SessionValues) As DataSet Implements IFixedAssetDeteriorationIndicationService.GetListReportDeteriorationIndicationsByPhysicalAsset
        Using service As IDeteriorationIndicationAdminService = Container.Current.Resolve(Of IDeteriorationIndicationAdminService)()
            Return service.GetListReportDeteriorationIndicationsByPhysicalAsset(Year, Month, ReportType, InitialPlate, FinalPlate, LegalBookId, InitialMainAccount, FinalMainAccount, session)
        End Using
    End Function

End Class
