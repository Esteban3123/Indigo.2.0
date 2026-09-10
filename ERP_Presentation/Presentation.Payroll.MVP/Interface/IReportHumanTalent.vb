'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Mariana Gonzalez Calderon
' Created          : 2025-12-03
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base

Public Interface IReportHumanTalent

    ''' <summary>
    ''' Establece los datos del reporte de talento humano
    ''' </summary>
    WriteOnly Property HumanTalentDataSource As Object

End Interface

