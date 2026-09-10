'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Antony F. Córdoba P.
' Created          : 20/12/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo

Public Class MMaritalStatus
    Implements IDisposable

#Region "Fields"
    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene todos los tipos de estado civil
    ''' </summary>
    Public Async Function ListAllMaritalStatus() As Task(Of List(Of MaritalStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllMaritalStatusAsync(Indigo)
    End Function

    ''' <summary>
    ''' Elimina el tipo de estado civil
    ''' </summary>
    ''' <param name="maritalStatus">La justificacion control </param>
    ''' <returns></returns>
    Public Async Function DeleteMaritalStatus(ByVal maritalStatus As MaritalStatus) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteMaritalStatusAsync(maritalStatus, Me.Indigo.AuditMessageWcf, Indigo)
    End Function

    ''' <summary>
    ''' graba un tipo de estado civil
    ''' </summary>
    ''' <param name="maritalStatus">La justificacion control</param>
    ''' <returns></returns>
    Public Async Function SaveMaritalStatus(ByVal maritalStatus As MaritalStatus, Optional idSequense As Long = 0) As Task(Of ActionResult(Of MaritalStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveMaritalStatusAsync(maritalStatus, Me.Indigo.AuditMessageWcf, Indigo, idSequense)
    End Function

    ''' <summary>
    ''' consulta un tipo de estado civil
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetMaritalStatusByCode(ByVal code As String) As Task(Of ActionResult(Of MaritalStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMaritalStatusByCodeAsync(code, Me.Indigo.AuditMessageWcf, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve un tipo de estado civil por ID
    ''' </summary>
    ''' <param name="id">Id del tipo de estado civil </param>
    ''' <returns>La justificacion Control</returns>
    ''' <remarks></remarks>
    Public Async Function GetMaritalStatusById(ByVal id As Integer) As Task(Of ActionResult(Of MaritalStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetMaritalStatusByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve un tipo de estado civil por cultura
    ''' </summary>
    ''' <param name="CultureStatus">Estado civil por cultura </param>
    ''' <remarks></remarks>
    Public Async Function GetCultureMaritalStatus(ByVal CultureStatus As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCultureMaritalStatusAsync(CultureStatus, Indigo)
    End Function

#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
