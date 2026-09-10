'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 04-09-2013
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
Imports Domain.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en las unidades de medida 
''' </summary>
Public Class MUnitMeasure
    Implements IDisposable
    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "575"
#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
    ''' <summary>
    ''' Obtener un tipo unidad de medida por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo la unidad de medida</param>
    ''' <returns>objeto tipo de inventario</returns>
    Public Async Function GetUnitMeasureAsync(ByVal code As String) As Task(Of MeasurementUnit)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetMeasurementUnitAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba la unidad de medida modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la unidad de medida</returns>
    Public Async Function SaveUnitMeasureAsync(ByVal reg As MeasurementUnit, idSequense As Integer) As Task(Of ActionResult(Of MeasurementUnit))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveMeasurementUnitAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using

    End Function
    ''' <summary>
    ''' Elimina la unidad de medida modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la unidad de medida</returns>
    Public Async Function DeleteUnitMeasureAsync(ByVal reg As MeasurementUnit) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteMeasurementUnitAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista las unidades de medida
    ''' </summary>
    Public Async Function ListAllUnitMeasure() As Task(Of List(Of MeasurementUnit))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllMeasurementUnitAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("MeasurementUnit", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of MeasurementUnit))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateMeasurementUnitAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
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
