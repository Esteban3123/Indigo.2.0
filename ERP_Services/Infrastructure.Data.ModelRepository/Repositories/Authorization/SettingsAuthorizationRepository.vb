Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SettingsAuthorizationRepository
    Inherits GenericRepository(Of SettingsAuthorization)
    Implements ISettingsAuthorizationRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetSettingsAuthorizationById(id As Integer) As SettingsAuthorization Implements ISettingsAuthorizationRepository.GetSettingsAuthorizationById
        Dim res As SettingsAuthorization = (From sa As SettingsAuthorization In Me._context.SettingsAuthorization Where sa.Id = id Select sa).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From sa As SettingsAuthorization In Me._context.SettingsAuthorization.AsNoTracking() Where sa.Id = id Select sa).FirstOrDefault()
            Return res
        Else
            Return New SettingsAuthorization()
        End If
    End Function

    Public Function GetSettingsAuthorization() As SettingsAuthorization Implements ISettingsAuthorizationRepository.GetSettingsAuthorization
        Dim res As SettingsAuthorization = (From sa As SettingsAuthorization In Me._context.SettingsAuthorization Select sa).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From sa As SettingsAuthorization In Me._context.SettingsAuthorization.AsNoTracking() Where sa.Id = res.Id Select sa).FirstOrDefault()
            Return res
        Else
            Return New SettingsAuthorization()
        End If
    End Function

#End Region

End Class