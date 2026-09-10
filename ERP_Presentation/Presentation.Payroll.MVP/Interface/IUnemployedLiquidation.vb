'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IUnemployedLiquidation
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Establece el datasource de las empresas
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property datasourceCompany As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property datasourceEmployee As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de los grupos dependiendo de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property datasourceGroups As List(Of Group)

    ''' <summary>
    ''' Propiedad que contiene el combo box de el periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PeriodControl As DevExpress.XtraEditors.ComboBoxEdit

    ''' <summary>
    ''' Habilita los controles si existen nominas liquidadas
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

    'Propeidad del combo si-no del campo "¿Intereses sobre cesantías se pagan con Nómina?"
    Property UnemployedInterestPaidWithPayroll As Boolean
#End Region

End Interface
