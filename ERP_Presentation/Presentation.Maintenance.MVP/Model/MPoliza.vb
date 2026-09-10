Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Maintenance.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel

'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 10-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MPoliza
    Implements IDisposable

    Private _tagForm As String = "559"
#Region "Metodos"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub

    ''' <summary>
    ''' Funcion para obtener la poliza
    ''' </summary>
    ''' <param name="Code">El codigo de la poliza</param>
    ''' <returns></returns>
    Public Async Function GetPoliza(ByVal Code As String) As Task(Of Poliza)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetPolizaAsync(Indigo.TransactionalContainer, Code, Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Funcion para guardar el objeto poliza
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SavePoliza(ByVal Record As Poliza, ByVal idSequense As Integer) As Task(Of ActionResult(Of Poliza))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SavePolizaAsync(Indigo.TransactionalContainer, Record, Indigo.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para eliminar la poliza
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeletePoliza(ByVal Record As Poliza) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeletePolizaAsync(Indigo.TransactionalContainer, Record, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Funcion para listar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllInsurance() As Task(Of List(Of Insurance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllInsuranceAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Funcion para listar todas los tipos de poliza
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllPolizaType() As Task(Of List(Of PolizaType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllPolizaTypeAsync(Indigo.TransactionalContainer)
    End Function
    Public Async Function ListAllPoliza() As Task(Of List(Of Poliza))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllPolizaAsync(Indigo.TransactionalContainer)
    End Function

    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Poliza", Me.Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of Poliza))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStatePolizaAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
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


