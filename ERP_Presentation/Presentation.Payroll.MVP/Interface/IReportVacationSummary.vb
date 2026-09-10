'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports IndigoReference.Payroll

Public Interface IReportVacationSummary

    ''' <summary>
    ''' Establece los datos del reporte de resumen de vacaciones
    ''' </summary>
    WriteOnly Property VacationSummaryDataSource As List(Of SP_SummaryVacationLiquidation_Result)

End Interface

