'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-12-2014
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

Public Interface IFixedAssetDistribution
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Property SettingsCost As InteropCostSetting

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Property Sequence As InteropCostSecuence

    ''' <summary>
    ''' Código de la distribución de Activos Fijos
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Id del Activo fijo a distribuir
    ''' </summary>
    Property FixedAssetId As Integer

    ''' <summary>
    ''' Código del Activo fijo a distribuir
    ''' </summary>
    Property FixedAssetCode As String

    ''' <summary>
    ''' Descripción de la distribución de Activos Fijos
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Año de la distribución de Activos Fijos
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución de Activos Fijos
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Estado de la distribución de Activos Fijos
    ''' </summary>
    Property Status As String

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

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Sub LoadControls()

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues()

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface