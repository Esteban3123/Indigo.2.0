Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Interface IContractLiquidation
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de empleados (XPO)
    ''' </summary>
    Property HumanTalentList As LinqInstantFeedbackSource


    ''' <summary>
    ''' Propiedad que contiene el listado de razones de retiros (XPO)
    ''' </summary>
    Property RetirementReasonList As List(Of RetirementReason)


    ''' <summary>
    ''' Propiedad que contiene el listado de empleados para ser usado en la rejilla
    ''' </summary>
    Property EmployeeList As List(Of Employee)

    ''' <summary>
    ''' Propiedad que contiene el empleado seleccionado
    ''' </summary>
    Property Employee As Employee

    ''' <summary>
    ''' 
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
