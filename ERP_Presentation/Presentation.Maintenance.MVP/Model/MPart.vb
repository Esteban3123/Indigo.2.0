'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
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
''' Esta clase tiene el modelo del patron MVP implementado en los consumibles
''' </summary>
Public Class MPart
    Implements IDisposable
    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "573"
#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
    ''' <summary>
    ''' Obtener una parte por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la parte.</param>
    ''' <returns>objeto parte</returns>
    Public Async Function GetPartAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetPartAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba la parte en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la parte</returns>
    Public Async Function SavePartAsync(ByVal reg As Part, idSequense As Integer) As Task(Of ActionResult(Of Part))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SavePartAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using

    End Function
    ''' <summary>
    ''' Elimina el objeto parte modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la parte</returns>
    Public Async Function DeletePartAsync(ByVal reg As Part) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeletePartAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos las partes
    ''' </summary>
    Public Function ListAllPart() As List(Of Part)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllPart(Indigo.IndigoCompany)
    End Function
    Public Async Function ListPartEquipmentType(IdEquipment As Integer) As Task(Of List(Of PartDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListPartEquipmentTypeAsync(Indigo.TransactionalContainer, IdEquipment)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Part", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of Part))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStatePartAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
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
