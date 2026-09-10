'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-01-2014
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

Public Interface IIncentivePayment
    Inherits IcrudBase

#Region "Properties"
    ' ''' <summary>
    ' ''' Establece el datasource de las empresas
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <remarks></remarks>
    'WriteOnly Property datasourceCompany As DevExpress.Xpo.XPInstantFeedbackSource

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



#End Region
End Interface
