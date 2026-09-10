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

Public Interface IGeneralExpenses
    Inherits IcrudBase

#Region "Properties"

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
    ''' Obtiene o establece el nombre del gasto
    ''' </summary>
    Property NameGeneralExpense As String

    ''' <summary>
    ''' Obtiene o establece el tipo de gasto
    ''' </summary>
    Property ExpendidureType As Byte

    ''' <summary>
    ''' Base de distribución
    ''' </summary>
    Property MultipleBase As Byte

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As String

    ''' <summary>
    ''' Propiedad para almacenar el xpo de las categorias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GeneralExpenseCategoryXpo As XPCollection

    Property SettingsCost As InteropCostSetting

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