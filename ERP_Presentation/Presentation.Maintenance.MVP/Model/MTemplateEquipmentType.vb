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
Imports Domain.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los tipos de poliza
''' </summary>
Public Class MTemplateEquipmentType
    Implements IDisposable
    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "589"
#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
    ''' <summary>
    ''' Obtener la plantilla por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del tipo de poliza.</param>
    ''' <returns>objeto tipo de poliza</returns>
    Public Async Function GetTemplateEquipmentTypeAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetTemplateEquipmentTypeAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba las plantillas en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el tipo de poliza</returns>
    Public Async Function SaveTemplateEquipmentTypeAsync(ByVal reg As TemplateEquipmentType, idSequense As Integer) As Task(Of ActionResult(Of TemplateEquipmentType))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveTemplateEquipmentTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using

    End Function
    ''' <summary>
    ''' Elimina la plantilla modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el tipo de poliza</returns>
    Public Async Function DeleteTemplateEquipmentTypeAsync(ByVal reg As TemplateEquipmentType) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteTemplateEquipmentTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todas las plantillas 
    ''' </summary>
    Public Function ListAllTemplateEquipmentType() As List(Of TemplateEquipmentType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllTemplateEquipmentType(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("TemplateEquipmentType", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of TemplateEquipmentType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateTemplateAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener la plantilla por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del tipo de poliza.</param>
    ''' <returns>objeto tipo de poliza</returns>
    Public Async Function GetTemplateEquipmentTypeByEquipmentTypeIdAsync(ByVal IdEquipmentType As Integer) As Task(Of TemplateEquipmentType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetTemplateEquipmentTypeEquipmentTypeIdAsync(Indigo.TransactionalContainer, IdEquipmentType, Indigo.AuditMessageWcf)
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
