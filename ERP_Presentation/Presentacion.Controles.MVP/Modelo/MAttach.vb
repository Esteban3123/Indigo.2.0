'***********************************************************************
' Assembly         : Presentacion.Controls.MVP
' Author           : Juan Diego Diaz
' Created          : 21-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.DocumentalSystem.Entities
Imports System.Threading.Tasks
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Modelo de conexion con los servicios necesarios para FrmAttach
''' </summary>
Public Class MAttach
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"

    ''' <summary>
    ''' Función para mandar el documento al motor de indexación
    ''' </summary>
    ''' <param name="Document">Documento a indexar</param>
    ''' <returns>ActionResult</returns>
    Public Async Function SaveIndexingDocument(Document As IndexedDocument2) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.IndexingDocumentAsync(Me.Indigo.VituelContainer, Document)
    End Function

    ''' <summary>
    ''' Funcion para obtener archivadores 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function getFileContainers(IdForm As String) As Task(Of List(Of FileContainersForm))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = IdForm}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.ListFileContainersFormByIdFormAsync(IdForm, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener metadata por archivadores
    ''' </summary>
    ''' <param name="searchWord">Texto a buscar</param>
    ''' <returns></returns>
    Public Async Function getMetaData(Id As String) As Task(Of List(Of Metadata))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = Id}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetMetadataByFileContainerAsync(sessionInfo)
    End Function

    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    Public Async Function saveDocument(length As Long, name As String, localFileStream As IO.Stream, metaData As String, attachDate As DateTime, idForm As Integer, idEntity As Integer, idFileContainer As Integer, UserFormMetaData As Boolean) As Task(Of Presentation.CloudAgent.IndigoReference.DocumentalSystem.ResponseDocument)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.SaveDocumentAsync(attachDate, length, name, idEntity, idFileContainer, idForm, metaData, sessionInfo, UserFormMetaData, localFileStream)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>DateTime</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


