'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Configuration
Imports Domain.Entities.Service
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Interface
Imports Infrastructure.Data.Base

#End Region

Public Class UserConfigurationRepository
    Inherits GenericRepository(Of UserConfiguration)
    Implements IUserConfigurationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="contex"></param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration Implements IUserConfigurationRepository.GetUserConfigurationByUserId
        If userId = 0 Then
            Throw New ArgumentNullException("userId")
        End If
        Dim res = (From uc As UserConfiguration In _context.UserConfiguration.Include("Containers").Include("Timezone") Where uc.UserId = userId Select uc).FirstOrDefault
        If res IsNot Nothing Then
            res.TimezoneName = res.Timezone.Name
            res.OriginalValue = (From uc As UserConfiguration In _context.UserConfiguration.AsNoTracking Where uc.UserId = userId Select uc).FirstOrDefault
            Return res
        Else
            Return New UserConfiguration()
        End If
    End Function

    Public Function UpdateUserConfiguration(UserConfigurationCulture As UserConfigurationCulture) As Boolean Implements IUserConfigurationRepository.UpdateUserConfiguration
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.UserConfiguration SET IdTimezone = @Idtimezone,DateFormat = @DateFormat,TimeFormat = @TimeFormat,LanguageCulture = @LanguageCulture WHERE UserId = @UserId"
            conx.AddParam("UserId", SqlDbType.Int, UserConfigurationCulture.IdUser)
            conx.AddParam("Idtimezone", SqlDbType.Int, UserConfigurationCulture.Idtimezone)
            conx.AddParam("DateFormat", SqlDbType.Int, UserConfigurationCulture.dateFormat)
            conx.AddParam("TimeFormat", SqlDbType.Int, UserConfigurationCulture.timeFormat)
            conx.AddParam("LanguageCulture", SqlDbType.VarChar, UserConfigurationCulture.LanguageCulture)
            Return conx.ExecuteCommandParams(query)
        End Using
    End Function



End Class
