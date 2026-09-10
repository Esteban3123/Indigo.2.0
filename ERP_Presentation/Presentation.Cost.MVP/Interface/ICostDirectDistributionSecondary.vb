'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities
#End Region

Public Interface ICostDirectDistributionSecondary
    Inherits IcrudBase

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
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Property Code As String

    Property DistributionSecondaryId As Integer?

    Property Description As String

    Property Year As Integer

    Property Month As Integer

    Property SettingsCost As CostSetting

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

End Interface
