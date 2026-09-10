'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
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

Public Interface ICostManpower
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
    Property ListDistributionManpowerDetail As List(Of CostDistributionManpowerDetail)

    ''' <summary>
    ''' Obtiene los parámetros de costos
    ''' </summary>
    Property SettingsCost As CostSetting

#End Region

#Region "Properties"

    ''' <summary>
    ''' Año de la distribución de mano de obra
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución de mano de obra
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Tipo de Registro
    ''' </summary>
    Property ManpowerTypeId As String

    ''' <summary>
    ''' Tipo de Registro
    ''' </summary>
    Property ManpowerType As Byte

    ''' <summary>
    ''' Id del Registro
    ''' </summary>
    Property EntityId As Integer?

    ''' <summary>
    ''' Descripcion de la distribución de mano de obra
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Total horas laboradas
    ''' </summary>
    ''' <returns></returns>
    Property HoursWorked As Integer

    ''' <summary>
    ''' Especifica el total devengado (Sin ningun tipo de deducciones) del empleado en el periodo especifico
    ''' </summary>
    Property TotalAccrued As Decimal
    ''' <summary>
    ''' Especifica el total de las provisiones del periodo actual
    ''' </summary>
    Property TotalProvision As Decimal
    ''' <summary>
    ''' Especifica el total de los aportes patronales
    ''' </summary>
    Property TotalEmployerContribution As Decimal
    ''' <summary>
    ''' Especifica el total de los parafiscales del empleado en el periodo seleccionado
    ''' </summary>
    Property TotalParafiscal As Decimal

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Datasource de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As XPInstantFeedbackSource

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Datasource de los centros de producción
    ''' </summary>
    Property ProductionCenterDataSource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los centros de producción para el repositorio de la rejilla
    ''' </summary>
    Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource

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