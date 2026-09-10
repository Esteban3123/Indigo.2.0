'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools
Imports Infrastructure.CrossCutting.Resources
#End Region

''' <summary>
''' Clase que contiene ayudas en las construccion del modulo presupuesto
''' </summary>
''' <remarks></remarks>
Public Class BudgetHelper

#Region "Const"
    Private Const NAME_MODULE As String = "Budget"
#End Region
#Region "Properties"

#Region "Common"
    ''' <summary>
    ''' Propiedad que contiene lista de los meses en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _months As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad que contiene lista de los meses en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Public Shared ReadOnly Property Months As List(Of Tuple(Of Byte, String))
        Get
            Dim Indigo As SessionValues = SessionValues.Instance
            Dim ci As System.Globalization.CultureInfo = Indigo.Culture
            Dim dtfi As System.Globalization.DateTimeFormatInfo = Nothing
            If IsDesignMode = False Then
                dtfi = ci.DateTimeFormat
            Else
                dtfi = New Globalization.DateTimeFormatInfo
            End If

            If _months Is Nothing Then
                _months = New List(Of Tuple(Of Byte, String))()
                _months.Add(New Tuple(Of Byte, String)(1, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(1), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(2, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(2), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(3, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(3), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(4, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(4), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(5, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(5), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(6, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(6), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(7, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(7), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(8, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(8), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(9, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(9), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(10, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(10), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(11, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(11), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(12, Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(12), Microsoft.VisualBasic.VbStrConv.ProperCase)))
                _months.Add(New Tuple(Of Byte, String)(13, "Periodos Cerrados"))
                _months.Add(New Tuple(Of Byte, String)(14, "Vigencia Cerrada"))
            End If
            Return _months
        End Get
    End Property
#End Region

#Region "Financial Source"
    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de clasificación en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _classificationType As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property ClassificationType As List(Of Tuple(Of Byte, String))
        Get
            If _classificationType Is Nothing Then
                _classificationType = New List(Of Tuple(Of Byte, String))()
                _classificationType.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("FrmFinancialSourceConSituacion", NAME_MODULE)))
                _classificationType.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("FrmFinancialSourceSinSituacion", NAME_MODULE)))
                _classificationType.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("FrmFinancialSourceRecursosPropios", NAME_MODULE)))
            End If
            Return _classificationType
        End Get
    End Property
#End Region


#Region "Budget Institutions - BudgetaryValidity"

    Private Shared _validityStatus As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad que contiene lista de los Estados de las vigencias en entidades presupuestales en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Public Shared ReadOnly Property ValidityStatus As List(Of Tuple(Of Byte, String))
        Get
            If _validityStatus Is Nothing Then
                _validityStatus = New List(Of Tuple(Of Byte, String))()
                _validityStatus.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("FrmBudgetEntitiesRegistrada", NAME_MODULE)))
                _validityStatus.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("FrmBudgetEntitiesActiva", NAME_MODULE)))
                _validityStatus.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("FrmBudgetEntitiesCerrada", NAME_MODULE)))
            End If
            Return _validityStatus
        End Get
    End Property

    Private Shared _earningsParameters As List(Of BudgetAccountParameters)
    ''' <summary>
    ''' Contiene lista de los parametros contables de ingreso en entidades presupuestales
    ''' T1 - idform  | T2 - nombre | T3 numero comprobante | T4 -  numero consecutivo | T5 -  usado 
    ''' </summary>
    Public Shared ReadOnly Property EarningsParameters As List(Of BudgetAccountParameters)
        Get
            _earningsParameters = New List(Of BudgetAccountParameters)()
            _earningsParameters.Add(New BudgetAccountParameters(207, ResourceManager.GetString("FrmBudgetEntitiesPresupuestoInicial", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(208, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAlPresupuesto", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(209, ResourceManager.GetString("FrmBudgetEntitiesTrasladosLaPresupuesto", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(211, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAlPAC", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(212, ResourceManager.GetString("FrmBudgetEntitiesTrasladosAlPAC", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(213, ResourceManager.GetString("FrmBudgetEntitiesReconocimientos", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(214, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAReconocimiento", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(215, ResourceManager.GetString("FrmBudgetEntitiesRecaudos", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(216, ResourceManager.GetString("FrmBudgetEntitiesModificacionARecaudos", NAME_MODULE), 0, 0, False))
            _earningsParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesCuentasPorCobrar", NAME_MODULE), 0, 0, False))
            Return _earningsParameters
        End Get
    End Property

    Private Shared _expenseParameters As List(Of BudgetAccountParameters)
    ''' <summary>
    ''' Contiene lista de los parametros contables de ingreso en entidades presupuestales
    ''' T1 - idform  | T2 - nombre | T3 numero comprobante | T4 -  numero consecutivo | T5 -  usado 
    ''' </summary>
    Public Shared ReadOnly Property ExpenseParameters As List(Of BudgetAccountParameters)
        Get
            _expenseParameters = New List(Of BudgetAccountParameters)
            _expenseParameters.Add(New BudgetAccountParameters(207, ResourceManager.GetString("FrmBudgetEntitiesPresupuestoInicial", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(208, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAlPresupuesto", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(209, ResourceManager.GetString("FrmBudgetEntitiesTrasladosLaPresupuesto", NAME_MODULE), 0, 0, False)) '222
            _expenseParameters.Add(New BudgetAccountParameters(211, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAlPAC", NAME_MODULE), 0, 0, False)) '226
            _expenseParameters.Add(New BudgetAccountParameters(212, ResourceManager.GetString("FrmBudgetEntitiesTrasladosAlPAC", NAME_MODULE), 0, 0, False)) '227
            _expenseParameters.Add(New BudgetAccountParameters(228, ResourceManager.GetString("FrmBudgetEntitiesDisponibilidades", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(229, ResourceManager.GetString("FrmBudgetEntitiesModificacionesADisponibilidades", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(231, ResourceManager.GetString("FrmBudgetEntitiesCompromisos", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(232, ResourceManager.GetString("FrmBudgetEntitiesModificacionesACompromisos", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(234, ResourceManager.GetString("FrmBudgetEntitiesObligaciones", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(235, ResourceManager.GetString("FrmBudgetEntitiesModificacionesAObligaciones", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(237, ResourceManager.GetString("FrmBudgetEntitiesOrdenDePago", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(230, ResourceManager.GetString("FrmBudgetEntitiesProrrogasDeDisponibilidades", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(236, ResourceManager.GetString("FrmBudgetEntitiesLiberacionDeRecursos", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(238, ResourceManager.GetString("FrmBudgetEntitiesReintegroDeRecurso", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesReservasPresupuestales", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesCuentasPorPagarPresupuestales", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesDisponibilidadesVFT", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesCompromisosVFT", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesObligacionesVFT", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesOrdenesDePagoVFT", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesSuspencionAlPresupuestoDeGastos", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(224, ResourceManager.GetString("FrmBudgetEntitiesLevantamientoDeSuspencionesAlPresupuestoDeGastos", NAME_MODULE), 0, 0, False))
            _expenseParameters.Add(New BudgetAccountParameters(0, ResourceManager.GetString("FrmBudgetEntitiesProrrogasDeDocumentos", NAME_MODULE), 0, 0, False))
            Return _expenseParameters
        End Get
    End Property
#End Region

#Region "Earnings Type"
    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de origen de ingreso en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _earningSource As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property EarningSource As List(Of Tuple(Of Byte, String))
        Get
            If _earningSource Is Nothing Then
                _earningSource = New List(Of Tuple(Of Byte, String))()
                _earningSource.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("FrmEarningsTypeAportesNacionales", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("FrmEarningsTypeIngresospropios", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("FrmEarningsTypeDisponibilidadinicial", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("FrmEarningsTypeVentadeservicios", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(5, ResourceManager.GetString("FrmEarningsTypeSistemageneraldeparticipaciones", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(6, ResourceManager.GetString("FrmEarningsTypeOtrosingresos", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(7, ResourceManager.GetString("FrmEarningsTypeIngresoscapital", NAME_MODULE)))
                _earningSource.Add(New Tuple(Of Byte, String)(8, ResourceManager.GetString("FrmEarningsTypeTotalingresos", NAME_MODULE)))
            End If
            Return _earningSource
        End Get
    End Property
#End Region

#Region "Expense Type"
    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de clasificación en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _classificationExpense As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property ClassificationExpense As List(Of Tuple(Of Byte, String))
        Get
            If _classificationExpense Is Nothing Then
                _classificationExpense = New List(Of Tuple(Of Byte, String))()
                _classificationExpense.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("FrmExpenseTypeDisponibilidadFinal", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("FrmExpenseTypeFuncionamiento", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("FrmExpenseTypeGastosGenerales", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("FrmExpenseTypeGastosOperaconComercial", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(5, ResourceManager.GetString("FrmExpenseTypeGastosPersonal", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(6, ResourceManager.GetString("FrmExpenseTypeInversión", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(7, ResourceManager.GetString("FrmExpenseTypeMantenimientoHospitalario", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(8, ResourceManager.GetString("FrmExpenseTypeServiciosDeuda", NAME_MODULE)))
                _classificationExpense.Add(New Tuple(Of Byte, String)(9, ResourceManager.GetString("FrmExpenseTypeTrasferencias", NAME_MODULE)))
            End If
            Return _classificationExpense
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene lista de los tipos de clasificación en la forma de una tupla(idBd, descripcion)
    ''' </summary>
    Private Shared _definitionExpense As List(Of Tuple(Of Byte, String))
    Public Shared ReadOnly Property DefinitionExpense As List(Of Tuple(Of Byte, String))
        Get
            If _definitionExpense Is Nothing Then
                _definitionExpense = New List(Of Tuple(Of Byte, String))()
                _definitionExpense.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("FrmEarningsTypeAportesNacionales", NAME_MODULE)))
                _definitionExpense.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("FrmEarningsTypeIngresospropios", NAME_MODULE)))
            End If
            Return _definitionExpense
        End Get
    End Property
#End Region


#End Region

End Class

''' <summary>
''' Clase que contiene los parametros contables para ingreso y gasto
''' </summary>
''' <remarks></remarks>
Public Class BudgetAccountParameters

    Sub New(_item1, _item2, _item3, _item4, _item5)
        Me._item1 = _item1
        Me._item2 = _item2
        Me._item3 = _item3
        Me._item4 = _item4
        Me._item5 = _item5
    End Sub

    Private _item1 As Integer
    Public Property Item1 As Integer
        Get
            Return _item1
        End Get
        Set(value As Integer)
            _item1 = value
        End Set
    End Property

    Private _item2 As String
    Public Property Item2 As String
        Get
            Return _item2
        End Get
        Set(value As String)
            _item2 = value
        End Set
    End Property

    Private _item3 As Integer
    Public Property Item3 As Integer
        Get
            Return _item3
        End Get
        Set(value As Integer)
            _item3 = value
        End Set
    End Property
    Private _item4 As Integer
    Public Property Item4 As Integer
        Get
            Return _item4
        End Get
        Set(value As Integer)
            _item4 = value
        End Set
    End Property

    Private _item5 As Boolean
    Public Property Item5 As Boolean
        Get
            Return _item5
        End Get
        Set(value As Boolean)
            _item5 = value
        End Set
    End Property
End Class
