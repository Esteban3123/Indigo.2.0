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

Public Interface ICostIntermediateDistribution
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Layout principal
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Property SettingsCost As CostSetting

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Property Sequence As CostSecuence

    ''' <summary>
    ''' Código de la distribución intermedia
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Descripción de la distribución intermedia
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Centro de produccion
    ''' </summary>
    Property ProductionCenterId As Integer

    ''' <summary>
    ''' Año de la distribución intermedia
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución intermedia
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Estado de la distribución intermedia
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Property ProductionCenterDataSource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the production center data source rerpository.
    ''' </summary>
    Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the production center data source header.
    ''' </summary>
    Property ProductionCenterDataSourceHeader As XPInstantFeedbackSource

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