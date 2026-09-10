'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 09-12-2014
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
Imports DevExpress.Data.PLinq
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Interface IProductionCenter
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
    ''' Cuenta de cancelación de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CancellationCostMainAccountId As Integer?

    ''' <summary>
    ''' Datasource cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CancellationCostMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the sequense.
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As InteropCostSecuence

    ''' <summary>
    ''' Obtiene o establece el código del centro de produccion
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del centro de produccion
    ''' </summary>
    Property NameProductionCenter As String

    ''' <summary>
    ''' Obtiene o establece la estructura organizacional
    ''' </summary>
    Property OrganizationalStructureOfCostId As Integer

    ''' <summary>
    ''' Obtiene o establece el area
    ''' </summary>
    Property Area As Decimal

    ''' <summary>
    ''' Obtiene o establece el tipo del centro
    ''' </summary>
    Property CenterType As Byte

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por mano de obra
    ''' </summary>
    Property ListHomologationLabor As List(Of ProductionCenterHomologation)

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por suministro
    ''' </summary>
    Property ListHomologationSupply As List(Of ProductionCenterHomologation)

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por consumo
    ''' </summary>
    Property ListHomologationConsumption As List(Of ProductionCenterHomologation)

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por egresos generales
    ''' </summary>
    Property ListHomologationGeneralExpenses As List(Of ProductionCenterHomologation)

    ''' <summary>
    ''' Obtiene o establece el listado de la homologacion por depreciacion
    ''' </summary>
    Property ListHomologationDeprecation As List(Of ProductionCenterHomologation)

    Property ListHomologationSales As List(Of ProductionCenterHomologation)

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasourde de estructura organizacional
    ''' </summary>
    Property OrganizationalStructureDatasourse As List(Of Domain.Entities.OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene o establece el datasource de centros de costo de dinamica
    ''' </summary>
    Property CostCenterDatasource As XPInstantFeedbackSource

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