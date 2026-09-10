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

Public Interface ICostGeneralExpenses
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Tipo de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElementCostType As Byte

    ''' <summary>
    ''' Clase costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpenseType As Byte

    ''' <summary>
    ''' Id de la categoría
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryId As Integer

    ''' <summary>
    ''' Datasource de la categoria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryXpo As XPCollection

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

    ''' <summary>
    ''' Obtiene o establece el nombre del gasto
    ''' </summary>
    Property NameGeneralExpense As String

    ''' <summary>
    ''' Base de distribución
    ''' </summary>
    Property MultipleBase As Byte

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As Boolean

    Property SettingsCost As CostSetting

    ''' <summary>
    ''' Obtiene la distribución de elementos del costo.
    ''' </summary>
    Property DistributionType As Byte

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