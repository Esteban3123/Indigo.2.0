Imports Presentation.Base

Public Interface IMixingStationSetting
    Inherits ICrudBase

    Property InventoryAdjustmentConceptOutputId As Integer?
    Property InventoryAdjustmentConceptInputId As Integer?
    Property ManufacturerId As Integer?
    Property ExpirationDays As Integer
    Property MeasurementUnitId As Integer?
    Property ProductTypeId As Integer?
    Property PackageUnitId As Integer?
    Property TransitWarehouseId As Integer?
    ''' <summary>
    ''' Activar central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property ActivateMS As Boolean

    Sub DeleteBlockedRecord()
    Sub CleanControls()
    Function LoadControls() As Task

End Interface
