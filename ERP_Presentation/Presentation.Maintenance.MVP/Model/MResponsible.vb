'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 02-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Maintenance.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en el funcional de responsables de mantenimiento
''' </summary>
Public Class MResponsible

    Implements IDisposable
    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "571"

#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
    ''' <summary>
    ''' Obtener un responsable por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la habitacion</param>
    ''' <returns>objeto tipo habitacion</returns>
    Public Async Function GetResponsibleAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetResponsibleAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba el objeto habitacion en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la habitacion</returns>
    Public Async Function SaveResponsibleAsync(ByVal reg As Responsible, idSequense As Integer) As Task(Of ActionResult(Of Responsible))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveResponsibleAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using

    End Function
    ''' <summary>
    ''' Elimina el objeto habitacion modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la habitacion</returns>
    Public Async Function DeleteResponsibleAsync(ByVal reg As Responsible) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteResponsibleAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos las habitaciones
    ''' </summary>
    Public Async Function ListAllResponsible() As Task(Of List(Of Responsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllResponsibleAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    Public Async Function ListAllBranch() As Task(Of List(Of Branch))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllBranchAsync(Indigo.TransactionalContainer)
    End Function

    Public Async Function ListAllCostCenter() As Task(Of List(Of CostCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllCostCenterAsync(Indigo)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULLAsync("Responsible", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of Responsible))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateResponsibleAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
