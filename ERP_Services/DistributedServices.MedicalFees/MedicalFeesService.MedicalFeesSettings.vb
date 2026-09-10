'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.MedicalFees
Imports Microsoft.Practices.Unity

Partial Class MedicalFeesService

    ''' <summary>
    ''' Obtiene los parametros de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesSettings(audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingMedicalFees) Implements IMedicalFeesMedicalFeesSettings.GetMedicalFeesSettings
        Using service As IMedicalFeesSettingsAdminService = Container.Current.Resolve(Of IMedicalFeesSettingsAdminService)()
            Return service.GetMedicalFeesSettings(audit)
        End Using
        'Return Me._medicalFeesSettingsAdminService.GetMedicalFeesSettings(audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza los parametros de honorarios medicos
    ''' </summary>
    ''' <param name="MedicalFeesSettings"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesSettings(MedicalFeesSettings As Domain.Entities.SettingMedicalFees, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingMedicalFees) Implements IMedicalFeesMedicalFeesSettings.SaveMedicalFeesSettings
        Using service As IMedicalFeesSettingsAdminService = Container.Current.Resolve(Of IMedicalFeesSettingsAdminService)()
            Return service.SaveMedicalFeesSettings(MedicalFeesSettings, audit)
        End Using
        'Return Me._medicalFeesSettingsAdminService.SaveMedicalFeesSettings(MedicalFeesSettings, audit)
    End Function

End Class
