'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System

#End Region

''' <summary>
''' Ensapsula las características y lógica base de un
''' objeto de configuración de almacén
''' </summary>
Public MustInherit Class ConfigStoreBase
    Implements IConfigStore

#Region "Fields"

    ''' <summary>
    ''' Ruta ruta al almacén de mensajes
    ''' </summary>
    Protected _pathStore As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la extensión del archivo de almacén
    ''' </summary>
    ''' <returns>Extensión del archivo de almacén</returns>
    Public MustOverride ReadOnly Property ExtFileStore As String Implements IConfigStore.ExtFileStore

    ''' <summary>
    ''' Obtiene el nombre del archivo de almacén
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride ReadOnly Property NameFileStore As String Implements IConfigStore.NameFileStore

    ''' <summary>
    ''' Obtiene la ruta del archivo de almacén
    ''' </summary>
    ''' <returns>La ruta del archivo de almacén</returns>
    Public MustOverride ReadOnly Property PathStore As String Implements IConfigStore.PathStore

    ''' <summary>
    ''' Obtiene la ruta al archivo de almacén
    ''' </summary>
    ''' <returns>La ruta al archivo de almacén</returns>
    Public MustOverride ReadOnly Property PathFileStore As String Implements IConfigStore.PathFileStore

#End Region

#Region "Buildes"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.New(Helpers.EnsureDefaultPathFileStoreParamExists())
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="pathStore">Ruta al almacén de mensajes</param>
    Public Sub New(ByVal pathStore As String)
        Me._pathStore = pathStore
    End Sub

#End Region

#Region "Methods"

#End Region

End Class