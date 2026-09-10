'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-09-2013
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
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en el funcional de ingreso de equipos
''' </summary>
Public Class MEquipmentReception
    Implements IDisposable
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#Region "Methods"

    ''' <summary>
    ''' Obtener una equipo por su placa, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la torre</param>
    ''' <returns>objeto tipo de poliza</returns>
    Public Async Function GetEquipmentRegistrationCAsync(ByVal code As String) As Task(Of EquipmentRegistration)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetEquipmentRegistrationAsync(Indigo.TransactionalContainer, code)
    End Function
    ''' <summary>
    ''' Graba el objeto equipo en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el equipo</returns>
    Public Async Function SaveEquipmentRegistrationAsync(ByVal reg As EquipmentRegistration, ByVal _IdSecuence As Long) As Task(Of ActionResult(Of EquipmentRegistration))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveEquipmentRegistrationAsync(Indigo.TransactionalContainer, reg, _IdSecuence, Indigo.AuditMessageWcf)
        'Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
        '    Dim mess1 As New MessageHeader(Of Int64)(_IdSecuence)
        '    Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
        '    OperationContext.Current.OutgoingMessageHeaders.Add(header1)
        '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveEquipmentRegistrationAsync(Indigo.TransactionalContainer, reg, _IdSecuence, Indigo.AuditMessageWcf)
        'End Using
    End Function
    ''' <summary>
    ''' Elimina el el objeto equipo
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la torre</returns>
    Public Async Function DeleteEquipmentRegistrationAsync(ByVal reg As EquipmentRegistration) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteEquipmentRegistrationAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos las torres
    ''' </summary>
    Public Async Function ListAllEquipment() As Task(Of List(Of Equipment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllEquipmentAsync(Indigo)
    End Function
    ''' <summary>
    ''' Lista todos las funciones de equipo
    ''' </summary>
    Public Async Function ListAllEquipmentFunctionAsync() As Task(Of List(Of EquipmentFunction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllEquipmentFunctionAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista todos los antecedentes del equipo
    ''' </summary>
    Public Async Function ListAllEquipmentHistoryAsync() As Task(Of List(Of EquipmentHistory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllEquipmentHistoryAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista todos los requerimientos del equipo
    ''' </summary>
    Public Async Function ListAllEquipmentRequirementAsync() As Task(Of List(Of EquipmentRequirement))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllEquipmentRequirementAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista todos los requerimientos del equipo
    ''' </summary>
    Public Async Function ListAllPhysicalRiskAsync() As Task(Of List(Of PhysicalRisk))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllPhysicalRiskAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Obtener una torre por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="IdCity">El codigo la ciudad</param>
    ''' <returns>objeto ciudad</returns>
    Public Async Function GetCityCountry(ByVal IdCity As Integer) As Task(Of City)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListCityCountryAsync(Indigo.IndigoCompany, IdCity)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Equipment", Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    Public Function ListAllEquipmentFunction() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListEquipmentFunction()
    End Function

    Public Function ListAllEquipmentHistory() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListEquipmentHistory()
    End Function

    Public Function ListAllPhysicalRisk() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListAllPhysicalRisk()
    End Function

    Public Function ListAllEquipmentRequirement() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListAllEquipmentRequirement()
    End Function

    Public Async Function ListTechnicalLogByFixedAssetPhysicalId(fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As Task(Of List(Of TechnicalLogDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistrationAsync(Indigo.TransactionalContainer, fixedAssetPhysicalAssetId, equipmentRegistration)
    End Function

    Public Function ListViewPartAccesoryConsumables() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListViewPartAccesoryConsumables()
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
