'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-02-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

Public Interface ICostFixedAssetDistribution
    Inherits IcrudBase

#Region "Variables"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Property Sequence As CostSecuence

    ''' <summary>
    ''' Listado del detalle de distribución por mano de obra
    ''' </summary>
    Property ListDistributionFixedAssetDetail As List(Of CostDistributionFixedAssetDetail)

    ''' <summary>
    ''' Obtiene los parámetros de costos
    ''' </summary>
    Property SettingsCost As CostSetting

#End Region

#Region "Properties"

    ''' <summary>
    ''' Año de la distribución de Activos Fijos
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución de Activos Fijos
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Código de la distribución de Activos Fijos
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Id del Activo fijo a distribuir
    ''' </summary>
    Property FixedAssetId As Integer

    ''' <summary>
    ''' Descripción de la distribución de Activos Fijos
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Total horas laboradas
    ''' </summary>
    ''' <returns></returns>
    Property HoursWorked As Integer

    ''' <summary>
    ''' Valor depreciado
    ''' </summary>
    ''' <returns></returns>
    Property DeprecationValue As Decimal

    ''' <summary>
    ''' Estado de la distribución de Activos Fijos
    ''' </summary>
    Property Status As Boolean

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Property ProductionCenterDataSource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the production center data source rerpository.
    ''' </summary>
    Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de activos fijos del erp a interfazar
    ''' </summary>
    Property FixedAssetDatasource As XPInstantFeedbackSource

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Sub LoadControls()

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls(Optional isNavigation As Boolean = False)

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues()

#End Region

End Interface