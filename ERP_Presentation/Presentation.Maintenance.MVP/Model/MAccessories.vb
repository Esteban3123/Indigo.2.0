'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 01-09-2013
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los accesorios
''' </summary>
Public Class MAccessories
    Inherits MBlockRecordAndSequenseMaintenance
    Implements IDisposable


    Dim Indigo As SessionValues
    Public Shared TAG As String = "565"
    Public Sub New()
        MyBase.New(TAG)
        Indigo = SessionValues.Instance
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtener un accesorio por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del accesorio</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetAccessoryAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetAccessoryAsync(Me.Indigo.TransactionalContainer, code)
    End Function
    ''' <summary>
    ''' Graba el accesorio en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el accesorio</returns>
    Public Async Function SaveAccessoryAsync(ByVal reg As Accessory, idSequense As Int64) As Task(Of ActionResult(Of Accessory))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Me.Indigo.AuditMessageWcf.Functional = TAG
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveAccessoryAsync(reg, Indigo)
        End Using
    End Function
    ''' <summary>
    ''' Elimina accessorio modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el accesorio</returns>
    Public Async Function DeleteAccessoryAsync(ByVal reg As Accessory) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteAccessoryAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllAccessories(IdEquipmentType As Integer) As Task(Of List(Of AccesoryDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAccesoryEquipmentTypeAsync(Indigo.TransactionalContainer, IdEquipmentType)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(Code As String, State As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Accessory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.Change_StateAccessoryAsync(Code, State, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Accessory", Indigo)
    End Function

    Public Function ListAccesoriesXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListAccesoryByStatus(True)
    End Function

    Public Function ListAccesoriesByEquipmentType(equipmentType As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListAccesoriesByEquipmentType(equipmentType, True)
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
