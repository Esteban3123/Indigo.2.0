'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls

Public Interface IBudgetEntities
    Inherits IcrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la seccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Section As String

    ''' <summary>
    ''' Obtiene o establece la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Unit As String

    ''' <summary>
    ''' Obtiene o establece la region
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Region As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece la vigencia actual y activa de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityIdXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' Obtiene o extablece el tercero
    ''' </summary>
    ''' <value>
    ''' The thyrd party xpo.
    ''' </value>
    Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece si maneja reconocimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Recognition As Boolean

    ''' <summary>
    ''' Obtiene o establece si la entidad es empresa social del estado E.S.E
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ESE As Boolean

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Boolean

    ''' <summary>
    ''' Establece el datasource del representante legal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalRepresentativeXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del representante legal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalRepresentativeId As Integer?

    ''' <summary>
    ''' Establece el datasource del jefe financiero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinancialBossXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del jefe financiero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinancialBossId As Integer?

    ''' <summary>
    ''' Establece el datasource del jefe de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetBossXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del jefe de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetBossId As Integer?

    ''' <summary>
    ''' Obtiene o establece el año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property YearValidity As Integer

    ''' <summary>
    ''' Obtiene o establece el mes proceso ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EarningsProcessMonth As Integer

    ''' <summary>
    ''' Obtiene o establece el med proceso gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpenseProcessMonth As Integer

    ''' <summary>
    ''' Obtiene o establece el numero de la resolucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResolutionNumber As String

    ''' <summary>
    ''' Obtiene o establece el valor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResolutionValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el estado de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusValidity As Integer

    ''' <summary>
    ''' Obtiene o establece manejo o control de PAC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PACControl As Boolean?

    ''' <summary>
    ''' Obtiene o establece control de fecha y consecutivos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DatesAndConsecutivesControl As Boolean?

End Interface
