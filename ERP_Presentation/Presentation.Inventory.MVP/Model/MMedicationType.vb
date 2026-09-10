'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-09
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
#End Region

Public Class MMedicationType
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Constructor de la clase.
    ''' </summary>
    ''' <param name="tag">Etiqueta del formulario.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        If String.IsNullOrWhiteSpace(tag) Then
            Throw New ArgumentException("El parámetro 'tag' no puede ser nulo o vacío.", NameOf(tag))
        End If
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Método para guardar un registro
    ''' </summary>
    ''' <param name="medicationType"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Async Function Save(ByVal medicationType As MedicationType, ByVal idSequence As Integer) As Task(Of ActionResult(Of MedicationType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveMedicationTypeAsync(
            medicationType,
            _indigoSessionValues.AuditMessageWcf,
            idSequence
            )
    End Function

    ''' <summary>
    ''' Método para eliminar un registro
    ''' </summary>
    ''' <param name="medicationType"></param>
    ''' <returns></returns>
    Public Async Function Delete(ByVal medicationType As MedicationType) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteMedicationTypeAsync(
            medicationType,
            _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Método para obtener un registro por código.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetByCode(ByVal code As String) As Task(Of ActionResult(Of MedicationType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetMedicationTypeByCodeAsync(
            code,
            _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Método para cambiar el estado del registro
    ''' </summary>
    ''' <param name="medicationType"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function ChangeState(ByVal medicationType As MedicationType, ByVal state As Boolean) As Task(Of ActionResult(Of MedicationType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateMedicationTypeAsync(
            medicationType,
            state,
            Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean
    ''' <summary>
    ''' Manejo correcto de recursos
    ''' </summary>
    ''' <param name="disposing"></param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
