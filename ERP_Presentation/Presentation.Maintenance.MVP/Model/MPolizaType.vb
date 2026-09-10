'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-08-2013
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
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los tipos de poliza
''' </summary>
Public Class MPolizaType
    Implements IDisposable

    Private _tagForm As String = "560"

    Dim Indigo As SessionValues = SessionValues.Instance

    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
#Region "Methods"

    ''' <summary>
    ''' Obtener un tipo de poliza por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del tipo de poliza.</param>
    ''' <returns>objeto tipo de poliza</returns>
    Public Async Function GetPolizaTypeAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetPolizaTypeAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba el tipo de poliza en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el tipo de poliza</returns>
    Public Async Function SavePolizaTypeAsync(ByVal reg As PolizaType, idSequense As Integer) As Task(Of ActionResult(Of PolizaType))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SavePolizaTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Elimina el tipo de poliza modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el tipo de poliza</returns>
    Public Async Function DeletePolizaTypeAsync(ByVal reg As PolizaType) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeletePolizaTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos los tipo de poliza
    ''' </summary>
    Public Function ListAllKinship() As List(Of PolizaType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllPolizaType(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("PolizaType", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of PolizaType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStatePolizaTypeAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
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
