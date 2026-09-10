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

    Public Class ServiceConfigurationRepository
    Inherits GenericRepository(Of ServiceConfiguration)
    Implements IServiceConfigurationRepository

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

    Public Function GetServiceConfigurationById(id As Byte) As ServiceConfiguration Implements IServiceConfigurationRepository.GetServiceConfigurationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From sc As ServiceConfiguration In _context.ServiceConfiguration Where sc.Id = id Select sc).FirstOrDefault
        If res IsNot Nothing Then
            'res.OriginalValue = (From sc As ServiceConfiguration In _context.ServiceConfiguration Where sc.Id = id Select sc).FirstOrDefault
            Return res
        Else
            Return New ServiceConfiguration()
        End If
    End Function

End Class
