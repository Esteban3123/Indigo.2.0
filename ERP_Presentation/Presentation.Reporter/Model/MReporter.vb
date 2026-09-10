'***********************************************************************
' Assembly         : Presentacion.Reporter
' Author           : Juan Diego Diaz
' Created          : 2014-02-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Modelo del frontal de evaluación
''' </summary>
Public Class MReporter
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guardar un registro de auditoria reportes
    ''' </summary>
    ''' <param name="Id">Id de la entidad</param>
    ''' <param name="NameEntity">Nombre de la entidad</param>
    ''' <returns>Action result</returns>
    Public Async Function saveAudit(ByVal Id As String, ByVal NameEntity As String, Parameters As String, ReportName As String, ActionAudit As ActionsAudit, ByVal count As Integer) As Task(Of ActionResult(Of BasicAudit))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBasicAuditAsync(Id, NameEntity, Parameters, ReportName, ActionAudit, _indigoSessionValues, count)
    End Function

    Public Function GetTotalPrint(ByVal entityName As String, ByVal entityKey As Integer) As Integer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetTotalPrint(entityName, entityKey, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Lista la auditoria basica
    ''' </summary>
    ''' <returns>Lista de Auditoria</returns>
    Public Function ListAuditBasic(Month As String, Year As String, IdForm As String, IdEntity As String) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.SecurityContainer).SecurityService.ListBasicAuditReports(Month, Year, IdForm, IdEntity, _indigoSessionValues.IndigoCompany)
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
    '   'Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
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