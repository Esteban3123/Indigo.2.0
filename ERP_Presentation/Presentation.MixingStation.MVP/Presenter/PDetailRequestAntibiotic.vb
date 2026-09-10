'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andrea Pahola Coqueco Cuellar
' Created          : 02-10-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDetailRequestAntibiotic

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IDetailRequestAntibiotic

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IDetailRequestAntibiotic)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Initializes the ATC.
    ''' </summary>
    Public Sub InitializeAtcByFormulationTypeWeigth(AtcByWeight As Boolean, Optional Ids As String = "")
        Using Model As New MBusqueda
            If AtcByWeight Then
                Dim filter As String = String.Format("FormulationType IN ({0}) AND Status = 1", Ids)
                Me._view.ATCDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListATCbyFilter(filter)
            Else
                Me._view.ATCDatasource = Model.ConsultarEntidades(eDataSource.ListATC)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the unit of measure
    ''' </summary>
    Public Sub InitializeMeasureUnit(formulationType As Integer)
        Dim Type As Integer = If(formulationType > 2, 1, formulationType)

        Using Model As New MBusqueda
            Me._view.MeasurementUnitDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, Type)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the unit of volume measurement
    ''' </summary>
    Public Sub InitializeMeasureUnitVolume()
        Using Model As New MBusqueda
            Me._view.MeasureUnitVolumeDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, 2)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the route of administration
    ''' </summary>s
    Public Sub InitializeAdministrationRoute(atcId As Integer)
        If Me._view.AdministrationRouteDatasource Is Nothing Then
            Using Model As New MBusqueda
                Me._view.AdministrationRouteDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListCollectionAdministrationRouteByAtcId(atcId)
            End Using
        End If

        If Me._view.AdministrationRouteDatasource.Count() = 1 Then
            Me._view.AdministrationRouteId = Me._view.AdministrationRouteDatasource.FirstOrDefault().AdministrationRouteId
        End If
    End Sub

    ''' <summary>
    ''' Initializes the reconstituent
    ''' </summary>
    Public Sub InitializeReconstituent()
        Using Model As New MBusqueda
            Me._view.ReconstituentDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListATCbyFilter("DiluentProduct = 1")
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the unit of measurement of the reconstituting agent
    ''' </summary>
    Public Sub InitializeReconstituentUnitMeasurement()
        Using Model As New MBusqueda
            Me._view.ReconstituentUnitMeasurementDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, 2)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the unit of measurement of the preparation
    ''' </summary>
    Public Sub InitializeTotalPreparedUnitMeasurement()
        Using Model As New MBusqueda
            Me._view.TotalPreparedUnitMeasurementDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, 2)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the Vehicle
    ''' </summary>
    Public Sub InitializeVehicle()
        Using Model As New MBusqueda
            Me._view.VehicleDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListATCbyFilter("DiluentProduct = 1")
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the unit of measurement of the vehicle
    ''' </summary>
    Public Sub InitializeVehicleUnitMeasurement()
        Using Model As New MBusqueda
            Me._view.VehicleUnitMeasurementDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, 2)
        End Using
    End Sub


    ''' <summary>
    ''' Obtain the administration route by ATC 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAdministrationRouteById(ByVal Id As Integer) As AdministrationRouteXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetAdministrationRouteById(Id)
    End Function

    ''' <summary>
    ''' Obtain the administration route by ATC 
    ''' </summary>
    ''' <param name="AtcId"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalFormById(AtcId As Integer) As PharmaceuticalFormXpo
        Dim filter As String = "AtcId = " & AtcId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of PharmaceuticalFormXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtain the Measurement Unit by Id
    ''' </summary>
    ''' <param name="MeasurementUnitId"></param>
    ''' <returns></returns>
    Public Function GetMeasurementUnitById(MeasurementUnitId As Integer) As MeasureUnitXpo
        Dim filter As String = "Id = " & MeasurementUnitId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of MeasureUnitXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
