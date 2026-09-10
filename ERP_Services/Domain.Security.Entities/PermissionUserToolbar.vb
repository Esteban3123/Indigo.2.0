'***********************************************************************
' Assembly         : Domain.Security.Entities
' Author           : OscarSierra
' Created          : 03-23-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' Esta clase tiene mapeada la propiedad tag del boton de la entidad Seguridad Permisos Usuaios Y Roles para poder retornar una entidad
''' plana y de esta manera asiganrle los permisos a la barra de usuario 	
''' </summary>
<DataContract(IsReference:=True)>
Public Class PermissionUserToolbar

    Private _tagButton As Integer
    ''' <summary>
    ''' Obtiene o Establece el Tag del Boton.	
    ''' </summary>
    ''' <value>Tag del boton.</value>
    ''' <remarks></remarks>
    <DataMember()>
    Property TagButton As Integer
        Get
            Return _tagButton
        End Get
        Set(value As Integer)
            _tagButton = value
        End Set
    End Property

    Private _tagForm As Integer
    ''' <summary>
    ''' Obtiene o Establece el Tag del Boton.	
    ''' </summary>
    ''' <value>Tag del boton.</value>
    ''' <remarks></remarks>
    <DataMember()>
    Property TagForm As Integer
        Get
            Return _tagForm
        End Get
        Set(ByVal value As Integer)
            _tagForm = value
        End Set
    End Property

End Class
