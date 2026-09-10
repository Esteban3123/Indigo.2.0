'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-01-31
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
#End Region

Public Interface IConsignmentCostList
    Inherits ICrudBase

#Region "Properties"
    ''' <summary>
    '''  Obtiene o establece  Id del proveedor
    ''' </summary>
    ''' <returns></returns>
    Property SupplierId As Integer

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <returns></returns>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o establece fecha de asignacion
    ''' </summary>
    ''' <returns></returns>
    Property EffectiveDate As Date

    ''' <summary>
    ''' Esta propiedad que contiene el layout del formulario
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>PortfolioSequence
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.ConsignmentCostList


    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region




End Interface
