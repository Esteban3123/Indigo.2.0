Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Maintenance.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel

'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-10-2014
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
Public Class MSupplierMaintenance

    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

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

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <param name="Code">El codigo del fabricante</param>
    ''' <returns></returns>
    Public Async Function GetSupplier(ByVal Code As String) As Task(Of SupplierMaintenance)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierMaintenanceAsync(Code, Indigo)
    End Function


    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSupplierById(ByVal id As Integer) As Task(Of SupplierMaintenance)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierMaintenanceByIdAsync(id, Indigo)
    End Function

    
    ''' <summary>
    ''' Funcion para guardar el fabricante 
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveSupplier(ByVal Record As SupplierMaintenance) As Task(Of ActionResult(Of SupplierMaintenance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveSupplierMaintenanceAsync(Record, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el fabricante
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteSupplier(ByVal Record As SupplierMaintenance) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteSupplierMaintenanceAsync(Record, Indigo)
    End Function

    Public Async Function ListAllSupplier() As Task(Of List(Of SupplierMaintenance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllSupplierMaintenanceAsync(Indigo)
    End Function
    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("SupplierMaintenance", Me.Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of SupplierMaintenance))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateSupplierMaintenanceAsync(code, state, Indigo)
        End Using
    End Function

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


