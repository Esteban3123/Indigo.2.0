'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Andres Alarcon
' Created          : 26-08-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceCategoryDefects

    Public Function SaveCategoryDefects(CategoryDefects As DefectClassificationGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DefectClassificationGroup) Implements IMixingStationServiceCategoryDefects.SaveCategoryDefects
        Using service As ICategoryDefectsAdminService = Container.Current.Resolve(Of ICategoryDefectsAdminService)()
            Return service.SaveCategoryDefects(CategoryDefects, audit, idSequense)
        End Using
    End Function

    Public Function DeleteCategoryDefects(CategoryDefects As DefectClassificationGroup, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCategoryDefects.DeleteCategoryDefects
        Using service As ICategoryDefectsAdminService = Container.Current.Resolve(Of ICategoryDefectsAdminService)()
            Return service.DeleteCategoryDefects(CategoryDefects, audit)
        End Using
    End Function

    Public Function GetCategoryDefects(code As String, audit As AuditMessage) As DefectClassificationGroup Implements IMixingStationServiceCategoryDefects.GetCategoryDefects
        Using service As ICategoryDefectsAdminService = Container.Current.Resolve(Of ICategoryDefectsAdminService)()
            Return service.GetCategoryDefects(code, audit)
        End Using
    End Function

    Public Function GetCategoryDefectsById(id As Integer) As DefectClassificationGroup Implements IMixingStationServiceCategoryDefects.GetCategoryDefectsById
        Using service As ICategoryDefectsAdminService = Container.Current.Resolve(Of ICategoryDefectsAdminService)()
            Return service.GetCategoryDefectsById(id)
        End Using
    End Function

End Class
