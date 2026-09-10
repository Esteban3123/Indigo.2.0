'***********************************************************************
' Assembly         : Presentacion.Controls.MVP
' Author           : Juan Diego Diaz
' Created          : 25-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.DocumentalSystem.Entities
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.Linq

#End Region
''' <summary>
''' Modelo de conexion con los servicios necesarios para Auditoria Avanzada
''' </summary>
Public Class MAdvancedAudit
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"

    ''' <summary>
    ''' Obtiene una lista de detalles de auditoria según parametro.
    ''' </summary>
    ''' <param name="IdAuditC">Id Auditoria Cabecera</param>
    Public Function GetAuditList(IdAuditC As String, entityName As String) As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.GetAuditList(IdAuditC, entityName, Indigo.IndigoCompany)
    End Function

    ''' <summary>
    ''' Obtiene registros de auditoria cabecera según parametros.
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdEntity">Id del registro</param>
    Public Function GetAuditC(entityName As String, IdForm As String, IdEntity As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.GetAuditC(entityName, Me.Indigo.IndigoCompany, IdForm, IdEntity)
    End Function

    ''' <summary>
    ''' Obtiene registros eliminados de auditoria cabecera según parametro.
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    Public Function GetAuditCDelete(entityName As String, IdForm As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.GetAuditCDelete(entityName, Me.Indigo.IndigoCompany, IdForm)
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


