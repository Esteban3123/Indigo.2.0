' Assembly         : Presentation.Portfolio
' Author           : Dayan Mauricio Sabi
' Created          : 09-03-2020
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IReportMainAccountsWithOutParameterization


    ''' <summary>
    ''' Establece y obtiene el año 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property GetYearPeriod As Integer

    ''' <summary>
    ''' Establece y obtiene el mes 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property GetMonthPeriod As Integer

End Interface
