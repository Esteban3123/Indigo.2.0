'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface ICopyBase
    Inherits IcrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityIdSource As Integer?

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityXpoSource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityIdSource As Integer?

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityXpoSource As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityIdDestiny As Integer?

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityXpoDestiny As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityIdDestiny As Integer?

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityXpoDestiny As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Recursos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Resource As Boolean?

    ''' <summary>
    ''' Dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Dependency As Boolean?

    ''' <summary>
    ''' Conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Concepts As Boolean?

    ''' <summary>
    ''' Tipos de Ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IncomeType As Boolean?

    ''' <summary>
    ''' Tipos de Gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpenseType As Boolean?

    ''' <summary>
    ''' Rubros de Ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryIncome As Boolean?

    ''' <summary>
    ''' Rubros de Gastos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryExpense As Boolean?

    ''' <summary>
    ''' Actualizar dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UpdateParameterizedDependencies As Boolean?

    ''' <summary>
    ''' Actualizar rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UpdateParameterizedCategories As Boolean?

End Interface
