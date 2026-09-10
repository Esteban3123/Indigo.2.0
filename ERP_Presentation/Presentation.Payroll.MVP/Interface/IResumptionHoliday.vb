'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/06/2016
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
Imports Domain.Entities
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IResumptionHoliday
    Inherits IcrudBase

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Property EmployeeId As Integer?

    ''' <summary>
    ''' Datasource empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmployeeXpo As LinqInstantFeedbackSource

End Interface

