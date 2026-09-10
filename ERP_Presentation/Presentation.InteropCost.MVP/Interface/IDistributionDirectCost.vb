'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 19-12-2014
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

Public Interface IDistributionDirectCost
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Property SettingsCost As InteropCostSetting

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
    Property Sequence As InteropCostSecuence

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del gasto directo
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el id del gasto directo
    ''' </summary>
    Property GeneralExpenseId As Integer

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As String

    ''' <summary>
    ''' Obtiene los gastos generales
    ''' </summary>
    Property GeneralExpenseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de centros de produccion
    ''' </summary>
    Property ProductionCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource unidad de medida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MeasurementUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de centros de produccion para el repositorio
    ''' </summary>
    Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource

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
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    ''' <returns></returns>
    Property Observation As String

#End Region

End Interface