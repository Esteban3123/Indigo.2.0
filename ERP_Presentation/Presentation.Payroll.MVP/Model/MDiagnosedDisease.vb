'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Juan Diego Díaz
' Created          : 07-09-2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Realiza la conexion con los servicios
''' </summary>
Public Class MDiagnosedDisease
    Implements IDisposable


#Region "Fields"

    ''' <summary>
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
    ''' Obtiene la enfermedad diagnosticada de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo de la enfermedad diagnosticada</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    Public Async Function GetDiagnosedDisease(ByVal code As String, ByVal tracking As Boolean) As Task(Of ActionResult(Of DiagnosedDisease))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetDiagnosedDiseaseAsync(code, tracking, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios de la enfermedad diagnosticada de forma asincrona
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease, idSequense As Integer) As Task(Of ActionResult(Of DiagnosedDisease))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveDiagnosedDiseaseAsync(diagnosedDisease, Indigo, idSequense)
    End Function


    ''' <summary>
    ''' Borra la enfermedad diagnosticada de forma asincrona
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteDiagnosedDiseaseAsync(diagnosedDisease, Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateDiagnosedDisease(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DiagnosedDisease))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateDiagnosedDiseaseAsync(code, state, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("SportPractice", Indigo)
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
