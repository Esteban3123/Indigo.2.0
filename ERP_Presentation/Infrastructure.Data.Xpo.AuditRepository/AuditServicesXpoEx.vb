'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.AuditRepository
' Author           : Juan Diego Diaz
' Created          : 25-10-2013
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
Imports DevExpress.Data.Linq
Imports System.Configuration

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class AuditServicesXpoEx
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
    ''' Obtiene los objetos de Auditoria Cabecera.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditC(ByVal IdForm As String, ByVal IdEntity As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "'"
        End If
        If IdEntity <> "0" Then
            filter = filter & " AND EntityKey=" & IdEntity & ""
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        Dim classEntity = sessionNew.GetClassInfo(GetType(Audit_Audit))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Action;Users;Date1", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los objetos de Auditoria Cabecera Eliminados.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAuditCDelete(ByVal IdForm As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        Dim filter As String = String.Empty
        If IdForm <> "0" Then
            filter = "Form='" & IdForm & "' AND Action=3"
        End If
        If Not String.IsNullOrEmpty(filter) Then
            criteria = CriteriaOperator.Parse(filter)
        End If
        Dim classEntity = sessionNew.GetClassInfo(GetType(Audit_Audit))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Action;Users;Date1", criteria)
        Return serverMode
    End Function

#Region "Audit LinqInstantFeedBackSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetAuditList(ByVal IdAuditC As String) As LinqInstantFeedbackSource
        Dim vlinqAudit As New LinqInstantFeedbackSource
        AddHandler vlinqAudit.GetQueryable, AddressOf OnGetQueryableAudit
        AddHandler vlinqAudit.DismissQueryable, AddressOf DismissQueryableAudit
        vlinqAudit.KeyExpression = "Id"
        vlinqAudit.SetAttachedProperty("IdAuditC", IdAuditC)
        Return vlinqAudit
    End Function

    Private Sub OnGetQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim dd = CType(sender, LinqInstantFeedbackSource)
            Dim AuditC = dd.GetAttachedProperty("IdAuditC").ToString
            Dim tableObjD As XPQuery(Of Audit_AuditDetail) = New XPQuery(Of Audit_AuditDetail)(sessionNew)
            Dim tableObjc As XPQuery(Of Audit_Audit) = New XPQuery(Of Audit_Audit)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Where T1.IdAudit.Id = CInt(AuditC)
                                     Select T1.Id, T1.NewValue, T1.PreviousValue, T1.Property1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAudit(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

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
