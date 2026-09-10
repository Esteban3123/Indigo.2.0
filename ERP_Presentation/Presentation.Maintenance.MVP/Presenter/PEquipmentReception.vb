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
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region
''' <summary>
''' 
''' </summary>
Public Class PEquipmentReception
    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>s
    Dim View As IEquipmentReception

    ''' <summary>
    ''' Variable que se usa para tratar el ingreso de un objeto
    ''' </summary>
    Dim EquipmentReception As PEquipmentReception

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEquipmentReception)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Async Sub Initializes()
        Using ModelMeasureUnit As New MUnitMeasure
            View.MeasureUnitDataSource = Await ModelMeasureUnit.ListAllUnitMeasure
        End Using
        'Dim ModelInventoryType As New MInventoryType
        ''Dim ModelLocation As New MLocation
        'Dim ModelEquipmentType As New MEquipamentType
        'Dim ModelSupplier As New MSupplierMaintenance(CStr(View.MyTag))
        'Dim ModelPoliza As New MPoliza
        'Dim ModelTrademark As New MTrademark("570")
        'Dim ModelEquipment As New MEquipment
        'Dim ModelPartsAccesoriesConsumables As New MPartsAccesoriesConsumables("1531")

        ''View.LocationDataSource = Await ModelLocation.ListAllLocationAsync
        'View.EquipmenteTypeDataSource = Await ModelEquipmentType.ListAllEquipamentType
        'View.SupplierDataSource = Await ModelSupplier.ListAllSupplier
        'View.PolizaDataSource = Await ModelPoliza.ListAllPoliza
        'Using ModelResponsible As New MResponsible
        '    View.ResponsibleDataSource = Await ModelResponsible.ListAllResponsible
        'End Using
        'Using ModelMeasureUnit As New MUnitMeasure
        '    View.MeasureUnitDataSource = Await ModelMeasureUnit.ListAllUnitMeasure
        'End Using
        'Using ModelTechnicalLog As New MTechnicalLog
        '    View.TechnicalLogDataSource = Await ModelTechnicalLog.ListAllTechnicalLog
        'End Using
        'Using ModelEquipmentReception As New MEquipmentReception
        '    View.EquipmentFunctionDataSource = Await ModelEquipmentReception.ListAllEquipmentFunctionAsync
        '    View.EquipmentHistoryDataSource = Await ModelEquipmentReception.ListAllEquipmentHistoryAsync
        '    View.EquipmentRequirementDataSource = Await ModelEquipmentReception.ListAllEquipmentRequirementAsync
        '    View.PhysicalRiskDataSource = Await ModelEquipmentReception.ListAllPhysicalRiskAsync
        'End Using
        'Using ModelTrademark
        '    View.TrademarkDataSource = Await ModelTrademark.ListAllTrademarkAsync
        'End Using
        ''Using ModelInventoryType
        ''    View.TypeInventoryDataSource = Await ModelInventoryType.ListAllInventoryType()
        ''End Using
        'Using ModelEquipment
        '    View.EquipmentDataSource = Await ModelEquipment.ListAllEquipment()
        'End Using
        'Using ModelPartsAccesoriesConsumables
        '    View.PartsAccesoriesConsumablesDataSource = Await ModelPartsAccesoriesConsumables.ListAllPartsAccesoriesConsumablesAsync()
        'End Using
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Async Sub GetMaintenanceParameters()
        Using model As New MMaintenanceParameter(CStr(Me.View.MyTag))
            Me.View.MaintenanceParameters = Await model.ListMaintenanceParameterAsync()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotificationsByWorkOrderId(workOrderId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListWorkOrderNotificationsByWorkOrderId(workOrderId)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotificationsBySource(entityName As String, entityId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListWorkOrderNotificationsBySource(entityName, entityId)
    End Function

End Class
