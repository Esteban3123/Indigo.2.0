'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 18-01-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security

#End Region

Public Class GeneralConfigurationRepository
    Implements IGeneralConfigurationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="contex"></param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener un contenedor
    ''' </summary>
    ''' <returns>Objeto Contenedor</returns>
    Public Function GetGeneralConfiguration() As GeneralConfiguration Implements IGeneralConfigurationRepository.GetGeneralConfiguration
        Dim result = (From gc In _context.GeneralConfiguration
                      Select gc).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New GeneralConfiguration
        End If
    End Function
End Class
