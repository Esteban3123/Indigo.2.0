#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MDashboardManagementMedicalOrder
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
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManagementMedicalOrderById(ByVal id As Integer) As Task(Of ActionResult(Of ManagementMedicalOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetManagementMedicalOrderByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function SaveManagementMedicalOrder(ListManagementMedicalOrder As List(Of ManagementMedicalOrder)) As Task(Of ActionResult(Of ManagementMedicalOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveManagementMedicalOrderAsync(ListManagementMedicalOrder, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guarda el desistimiento
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function SaveManagementMedicalOrderWithdrawal(managementMedicalOrder As ManagementMedicalOrder, attachment As Attachment) As Task(Of ActionResult(Of ManagementMedicalOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveManagementMedicalOrderWithdrawalAsync(managementMedicalOrder, attachment, Me.Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
