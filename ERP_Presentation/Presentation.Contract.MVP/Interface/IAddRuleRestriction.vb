'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 16/12/2023
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

Public Interface IAddRuleRestriction
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene o establece la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RuleType As Integer?

    ''' <summary>
    ''' Obtiene o Asigna el Id de CUPS
    ''' </summary>
    ''' <returns></returns>
    Property CUPSEntityId As Integer?

    ''' <summary>
    ''' Obtiene o asinga el Id de Sub grupo  de CUPS
    ''' </summary>
    ''' <returns></returns>
    Property CUPSSubgroupId As Integer?

    ''' <summary>
    ''' Obtiene o asigna el Id de Grupo de CUPS
    ''' </summary>
    ''' <returns></returns>
    Property CUPSGroupId As Integer?

    ''' <summary>
    ''' Obtiene o asigna el Id de un producto
    ''' </summary>
    ''' <returns></returns>
    Property ProductId As Integer?

    ''' <summary>
    ''' Obtiene o asigna el Id Sub grupo de producto
    ''' </summary>
    ''' <returns></returns>
    Property ProductSubGroupId As Integer?

    ''' <summary>
    ''' Obtiene o asigna el Id de grupo de producto
    ''' </summary>
    ''' <returns></returns>
    Property ProductGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConditionType As Integer?

    ''' <summary>
    ''' Obtiene o establece el operador lógico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LogicOperator As Integer?

    ''' <summary>
    ''' Obtiene o establece el segundo tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConditionTypeSecond As Integer?

    ''' <summary>
    ''' Obtiene o establece el CUPS a donde se va incluir el servicio
    ''' </summary>
    ''' <returns></returns>
    Property IncludeToCUPSEntityId As Integer?

    ''' <summary>
    ''' DataSource CUPS
    ''' </summary>
    ''' <returns></returns>
    Property DataSourceIncludeToCUPSEntity As XPInstantFeedbackSource
End Interface
