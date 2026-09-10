'***********************************************************************
' Assembly         : Domain.Security.Entities
' Author           : OscarSierra
' Created          : 03-21-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
''' <summary>
''' Esta clase Tiene mapeada las propiedades de la entidad Modulos y Menu de la aplicacion Para respectivamente poder retornar
''' una entidad plana cuando se necesito realizar un DataSource en los forntales de Usuario o Roles.	
''' </summary>
Public Class IndigoModule

    Private _nameModule As String
    ''' <summary>
    '''Obtiene o Estable el Nombre del Modulo.	
    ''' </summary>
    ''' <value>Nombre Modulo.</value>
    ''' <remarks></remarks>
    Property NameModule As String
        Get
            Return _nameModule
        End Get
        Set(value As String)
            _nameModule = value
        End Set
    End Property

    Private _optionMenu As String
    ''' <summary>
    ''' Obtiene o Establece la Opcion de Menu.	
    ''' </summary>
    ''' <value>Opcion del Menu.</value>
    ''' <remarks></remarks>
    Property OptionMenu As String
        Get
            Select Case _optionMenu
                Case Is = "0"
                    Return "General"
                Case Is = "1"
                    Return "Archivo"
                Case Is = "2"
                    Return "Proceso"
                Case Is = "3"
                    Return "Utilidades"
                Case Is = "4"
                    Return "Reportes"
                Case Else
                    Return "No Aplica"
            End Select
        End Get
        Set(value As String)
            _optionMenu = value
        End Set
    End Property

    Private _nameMenu As String
    ''' <summary>
    ''' Obtiene o Establece Nombre del Menu.	
    ''' </summary>
    ''' <value>Nombre del Menu.</value>
    ''' <remarks></remarks>
    Property NameMenu As String
        Get
            Return _nameMenu
        End Get
        Set(value As String)
            _nameMenu = value
        End Set
    End Property

    Private _codeMenu As String
    ''' <summary>
    ''' Gets or sets the codigo menu.	
    ''' </summary>
    ''' <value>The codigo menu.</value>
    ''' <remarks></remarks>
    Property CodeMenu As String
        Get
            Return _codeMenu
        End Get
        Set(value As String)
            _codeMenu = value
        End Set
    End Property

End Class
