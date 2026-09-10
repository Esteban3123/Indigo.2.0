#Region "Imports"
Imports Domain.AccountManagement.Model
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
#End Region
Public Class MDashboardAccountAssignment
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

    ''' <summary>
    ''' Obtiene los traslados por usuario y unidad operativa
    ''' </summary>
    ''' <param name="CareCenter"></param>
    Public Async Function GetPendingAssignmentByCareCenter(ByVal careCenter As String, ByVal entryType As String) As Task(Of ActionResult(Of List(Of VPendingAssignment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetPendingAssignmentByCareCenterAndEntryTypeAsync(careCenter, entryType)
    End Function

    ''' <summary>
    ''' Genera la asignación automática de los pacientes a un usuario facturador
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <returns></returns>
    Public Async Function GenerateAutomaticAssignment(ByVal admissionToAssign As List(Of AutomaticDistributionMessage)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GenerateAutomaticAssignmentAsync(admissionToAssign, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guarda la asignación manual de los pacientes a un usuario facturador
    ''' </summary>
    ''' <param name="automaticEntryDistributions"></param>
    ''' <returns></returns>
    Public Async Function GenerateManualAssignment(ByVal automaticEntryDistributions As List(Of AutomaticEntryDistribution)) As Task(Of ActionResult(Of List(Of AutomaticEntryDistribution)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GenerateManualAssignmentAsync(automaticEntryDistributions)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
