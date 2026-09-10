'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesSettingsRepository
    Inherits IRepository(Of SettingMedicalFees)

    ''' <summary>
    ''' Obtiene los parametros de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesSetting() As SettingMedicalFees

End Interface
