'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class ApplicationSettingRepository
    Inherits GenericRepository(Of ApplicationSettings)
    Implements IApplicationSettingRepository

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

    Public Function GetApplicationSettingsByContainerId(containerId As Integer) As ApplicationSettings Implements IApplicationSettingRepository.GetApplicationSettingsByContainerId
        Dim res = (From ap As ApplicationSettings In _context.ApplicationSettings Where ap.ContainerId = containerId Select ap).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From ap As ApplicationSettings In _context.ApplicationSettings.AsNoTracking Where ap.ContainerId = containerId Select ap).FirstOrDefault
            Return res
        Else
            Return New ApplicationSettings()
        End If
    End Function

End Class
