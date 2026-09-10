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

Public Interface ICostSecondaryDistributionElements
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
    ''' Gets or sets the sequence.
    ''' </summary>
    Property Sequence As CostSecuence

    ''' <summary>
    ''' Obtiene o establece el codigo de la distribucion segundaria
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Centro de prtoducción de la distribución secundaria
    ''' </summary>
    Property ProductionCenterId As Integer

    ''' <summary>
    ''' Descripción de la distribución secundaria
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Property SettingsCost As CostSetting

    ''' <summary>
    ''' Datasource de los centros de produccion
    ''' </summary>
    Property ProductionCenterDatasource As XPInstantFeedbackSource

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