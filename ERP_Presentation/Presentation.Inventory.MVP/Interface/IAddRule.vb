'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Angi Camila Duran Vargas
' Created          : 22/07/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IAddRule
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene o establece la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RuleType As Integer


    ''' <summary>
    ''' Obtiene o establece el id del tipo de producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del tipo de grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece el operador lógico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductSubGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As DateTime

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el tipo de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateType As Integer

    ''' <summary>
    ''' Obtiene o establece variable de porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageBasedOn As Integer?

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Percentage As Decimal?

    ''' <summary>
    ''' Establece el datasource de la especialidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValue As Decimal?

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

    ''' <summary>
    ''' Obtiene o establece el tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConditionType As Integer
End Interface
