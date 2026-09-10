'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo

Public Interface IVacation
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de vacaciones
    ''' </summary>
    Property VacationDataSource As List(Of Employee)

    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de empleados
    ''' </summary>
    WriteOnly Property EmployeeDataSource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de grupo
    ''' </summary>
    WriteOnly Property GroupDataSource As XPInstantFeedbackSource

End Interface
