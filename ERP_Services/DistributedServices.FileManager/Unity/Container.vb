#Region "Imports"

Imports Microsoft.Practices.Unity
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Application.FileManager

#End Region

Public NotInheritable Class Container

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia del contenedor
    ''' </summary>
    Private Shared _currentContainer As IUnityContainer
    ''' <summary>
    ''' Obtiene la unica instancia del contenedor
    ''' </summary>
    ''' <returns>Contenedor configurado</returns>
    Public Shared ReadOnly Property Current() As IUnityContainer
        Get
            If _currentContainer Is Nothing Then
                _currentContainer = New UnityContainer()
                ConfigureContainer()
            End If
            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer()

        _currentContainer.RegisterType(Of IFileManagerAdminService, FileManagerAdminService)()
    End Sub

#End Region

End Class