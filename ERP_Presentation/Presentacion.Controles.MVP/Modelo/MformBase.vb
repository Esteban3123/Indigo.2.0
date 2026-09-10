'***********************************************************************
' Assembly         : Presentation.Controls.MVP.MBusqueda
' Author           : Andres Bonilla
' Created          : 28-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 28-03-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Indexing
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' Clase Modelo del Formulario Base
''' </summary>
Public Class MformBase
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable de sesión
    ''' </summary>
    Private _sessionValues As SessionValues = SessionValues.Instance

#End Region

    ''' <summary>
    ''' Función para mandar el documento al motor de indexación
    ''' </summary>
    ''' <param name="Document">Documento a indexar</param>
    ''' <returns>ActionResult</returns>
    Public Async Function SaveIndexingDocument(Document As Domain.Base.Entities.IndexedDocument2) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.IndexingDocumentAsync(Me._sessionValues.VituelContainer, Document)
    End Function

    Public Function SaveIndexingDocumentNormal(Document As Domain.Base.Entities.IndexedDocument2) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.IndexingDocument(Me._sessionValues.VituelContainer, Document)
    End Function

    ''' <summary>
    ''' Función para eliminar un documento indexado
    ''' </summary>
    ''' <param name="Doc">Documento a eliminar</param>
    ''' <returns>IndexedDocumentResultSet</returns>
    Public Async Function DeleteIndexingDocument(Doc As Domain.Base.Entities.IndexedDocument2) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.DeleteDocumentAsync(Me._sessionValues.VituelContainer, Doc)
    End Function

    ''' <summary>
    ''' Función para buscar un documento indexado
    ''' </summary>
    ''' <param name="DocumentTextSearch">Texto a buscar</param>
    ''' <returns>IndexedDocumentResultSet</returns>
    Public Async Function SearchIndexingDocument(TextSearch As String) As Threading.Tasks.Task(Of Domain.Base.Entities.IndexedDocumentResultSet2)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.SearchDocumentAsync(Me._sessionValues.VituelContainer, TextSearch, Nothing, Nothing, 0, 1, True)
    End Function

    Public Function SearchIndexingDocumentNormal(TextSearch As String) As Domain.Base.Entities.IndexedDocumentResultSet2
        Return IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.SearchDocument(Me._sessionValues.VituelContainer, TextSearch, Nothing, Nothing, 0, 1, True)
    End Function

    ''' <summary>
    ''' Consultar la fecha y hora del servidor
    ''' </summary>
    Public Async Function GetDateServerAsync() As Threading.Tasks.Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync
    End Function

    ''' <summary>
    ''' Consultar la fecha y hora del servidor
    ''' </summary>
    Public Function GetDateServer() As DateTime
        Return IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate
    End Function

    ''' <summary>
    ''' Consultar la fecha y hora del servidor
    ''' </summary>
    Public Async Function GetHolidayAsync(holidayDate As Date) As Threading.Tasks.Task(Of Domain.Entities.Holiday)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetHolidayAsync(holidayDate, _sessionValues)
    End Function

    ''' <summary>
    ''' Consultar la fecha y hora del servidor
    ''' </summary>
    Public Function GetHoliday(holidayDate As Date) As Domain.Entities.Holiday
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetHoliday(holidayDate, _sessionValues)
    End Function

    'Public Async Function GetOperatingUnitByContainerPermission(idContainer As Integer) As Threading.Tasks.Task(Of List(Of Domain.Entities.OperatingUnit))
    '    Dim listOperatingUnit = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitAsync(_sessionValues, _sessionValues.TransactionalContainer)
    '    Dim listPermissionUser = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionOperatingUnitAsync(_sessionValues.UserIndigo, idContainer, _sessionValues)
    '    Dim listPermissionOperating As New List(Of Domain.Entities.OperatingUnit)
    '    For Each permissionOperating In listPermissionUser
    '        If permissionOperating.Status = True Then
    '            listPermissionOperating.Add(listOperatingUnit.Where(Function(x) x.Id = permissionOperating.IdOperatingUnit).SingleOrDefault)
    '        End If
    '    Next
    '    Return listPermissionOperating
    'End Function

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
