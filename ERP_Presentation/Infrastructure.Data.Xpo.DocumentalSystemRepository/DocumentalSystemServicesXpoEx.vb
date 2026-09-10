'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.DocumentalSystemRepository
' Author           : Juan Diego Diaz
' Created          : 07-10-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports System.Configuration
Imports Infrastructure.Data.Xpo.DocumentalSystemRepository.GENESISDOCUMENTAL01

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class DocumentalSystemServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Fields"

#End Region

#Region "Builders"

    ' ''' <summary>
    ' ''' Incializa una nueva instancia de la clase
    ' ''' </summary>
    ' ''' <param name="Company">Empresa que se debe consultar</param>
    ' ''' <param name="endpoint">Punto de configuración del servicio</param>
    ' ''' <param name="remoteaddress">Dirección remota del servicio</param>
    'Public Sub New(Company As String, ByVal endpoint As String, ByVal remoteaddress As String)
    '    XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStoreEx(endpoint, remoteaddress, Company))
    'End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Consulta los contenedores de archivos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFileContainer() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim classEntity = sessionNew.GetClassInfo(GetType(FileContainer))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;UseFormMetada", Nothing)
        Return serverMode
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
