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

Public Interface IManpower
    Inherits IcrudBase

#Region "Properties"

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
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    Property EmployeeId As Integer

    ''' <summary>
    ''' Descripcion de la distribución de mano de obra
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Año de la distribución de mano de obra
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución de mano de obra
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As String

    ''' <summary>
    ''' Listado del detalle de distribución por mano de obra
    ''' </summary>
    Property ListDistributionManpowerDetail As List(Of DistributionManpowerDetail)

    ''' <summary>
    ''' Obtiene los parámetros de costos
    ''' </summary>
    Property SettingsCost As InteropCostSetting

    ''' <summary>
    ''' Datasource de los empleados
    ''' </summary>
    Property EmployeeDatasource As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollEmployeeXpo)

    ''' <summary>
    ''' Datasource de los centros de producción
    ''' </summary>
    Property ProductionCenterDataSource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los centros de producción para el repositorio de la rejilla
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

#End Region

End Interface