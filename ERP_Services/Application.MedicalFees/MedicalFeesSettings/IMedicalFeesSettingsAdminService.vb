'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMedicalFeesSettingsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicalFeesSettings(ByVal MedicalFeesSettings As SettingMedicalFees, ByVal audit As AuditMessage) As ActionResult(Of SettingMedicalFees)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesSettings(ByVal audit As AuditMessage) As ActionResult(Of SettingMedicalFees)

End Interface
