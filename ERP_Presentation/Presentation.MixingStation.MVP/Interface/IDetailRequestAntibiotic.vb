'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andrea Pahola Coqueco Cuellar
' Created          : 02-10-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

Public Interface IDetailRequestAntibiotic

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    Property AdministrationRouteId As Integer?

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Obtiene o establece el datasource para los medicamentos principales
    ''' </summary>
    Property ATCDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la unidad de medida de los medicamentos principales
    ''' </summary>
    Property MeasurementUnitDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la unidad de volumen del medicamento principal
    ''' </summary>
    Property MeasureUnitVolumeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la vía de administración
    ''' </summary>
    Property AdministrationRouteDatasource As XPCollection(Of ViewATCAdministrationRouteXpo)

    ''' <summary>
    ''' Obtiene o establece el datasource para el reconstituyente
    ''' </summary>
    Property ReconstituentDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la unidad de medida del reconstituyente
    ''' </summary>
    Property ReconstituentUnitMeasurementDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la unidad de medida del preparado
    ''' </summary>
    Property TotalPreparedUnitMeasurementDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para el vehículo
    ''' </summary>
    Property VehicleDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para la unidad de medida del vehículo
    ''' </summary>
    Property VehicleUnitMeasurementDatasource As XPInstantFeedbackSource

#End Region

End Interface
