'***********************************************************************
' Assembly         : Domain.Security.Entities
' Author           : Juan Diego Diaz
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
''' <summary>
''' Clase para encapsular los formularios activos de un usuario	
''' </summary>

Public Class PermissionsFormsActive

    Private _formTag As String
    ''' <summary>
    ''' Obtiene o Establece el Tag del formulario.	
    ''' </summary>
    ''' <value>Tag del formulario.</value>
    Property FormTag As String
        Get
            Return _formTag
        End Get
        Set(value As String)
            _formTag = value
        End Set
    End Property

    Private _nameForm As String
    ''' <summary>
    ''' Obtiene o Establece el nombre del formulario.	
    ''' </summary>
    ''' <value>Nombre del formulario.</value>
    Property NameForm As String
        Get
            Return _nameForm
        End Get
        Set(ByVal value As String)
            _nameForm = value
        End Set
    End Property

    Private _nameGroup As String
    ''' <summary>
    ''' Obtiene o Establece el nombre del grupo
    ''' </summary>
    ''' <value>String.</value>
    Property NameGroup As String
        Get
            Return _nameGroup
        End Get
        Set(ByVal value As String)
            _nameGroup = value
        End Set
    End Property

    Private _nameModule As String
    ''' <summary>
    ''' Obtiene o establece el nombre del modulo
    ''' </summary>
    ''' <value>Nombre del modulo</value>
    ''' <returns>El nombre del modulo</returns>
    Property NameModule As String
        Get
            Return _nameModule
        End Get
        Set(value As String)
            _nameModule = value
        End Set
    End Property

    Private _enableEmbedded As Boolean
    ''' <summary>
    ''' Obtiene o Establece si el formulario puede ser embebido
    ''' </summary>
    ''' <value>Boolean.</value>
    Property EnableEmbedded As Boolean
        Get
            Return _enableEmbedded
        End Get
        Set(ByVal value As Boolean)
            _enableEmbedded = value
        End Set
    End Property

    Private _listActions As List(Of String)
    ''' <summary>
    ''' Obtiene o Establece la lista de acciones del formulario.
    ''' </summary>
    ''' <value>Lista de Acciones.</value>
    Property ListActions As List(Of String)
        Get
            Return _listActions
        End Get
        Set(ByVal value As List(Of String))
            _listActions = value
        End Set
    End Property

    Private _printRunSave As Boolean
    ''' <summary>
    ''' Obtiene o Establece si el formulario ejecuta automaticamente 
    ''' la generación de un reporte al guardar 
    ''' </summary>
    ''' <value>Boolean.</value>
    Property PrintRunSave As Boolean
        Get
            Return _printRunSave
        End Get
        Set(ByVal value As Boolean)
            _printRunSave = value
        End Set
    End Property

    Private _printRunConfirm As Boolean
    ''' <summary>
    ''' Obtiene o Establece si el formulario ejecuta automaticamente 
    ''' la generación de un reporte al confirmar
    ''' </summary>
    ''' <value>Boolean.</value>
    Property PrintRunConfirm As Boolean
        Get
            Return _printRunConfirm
        End Get
        Set(ByVal value As Boolean)
            _printRunConfirm = value
        End Set
    End Property

End Class
