'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO

#End Region

''' <summary>
''' Ensapsula las características y lógica de un
''' objeto de configuración de almacén para mensajes de auditoria
''' </summary>
Public Class AuditingConfigStore
    Inherits ConfigStoreBase

#Region "Consts"

    ''' <summary>
    ''' Nombre del archivo de almacén
    ''' </summary>
    Public Const FILENAME As String = "AuditingMessageFileStore"

#End Region

#Region "Fields"

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la extensión del archivo de almacén
    ''' </summary>
    ''' <returns>Extensión del archivo de almacén</returns>
    Public Overrides ReadOnly Property ExtFileStore As String
        Get
            Return "mfs"
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el nombre del archivo de almacén
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property NameFileStore As String
        Get
            Return FILENAME & "." & ExtFileStore
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta del archivo de almacén
    ''' </summary>
    ''' <returns>La ruta del archivo de almacén</returns>
    Public Overrides ReadOnly Property PathStore As String
        Get
            Return Me._pathStore
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta al archivo de almacén
    ''' </summary>
    ''' <returns>La ruta al archivo de almacén</returns>
    Public Overrides ReadOnly Property PathFileStore As String
        Get
            Return Path.Combine(Me._pathStore, NameFileStore)
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="pathStore">Ruta al almacén de mensajes</param>
    Public Sub New(ByVal pathStore As String)
        MyBase.New(pathStore)
    End Sub

#End Region

#Region "Methods"

#End Region

End Class