'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Juan Pablo Daza Medina
' Created          : 28-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region
Public Interface IMinimumSalary
    Inherits ICrudBase

#Region "Properties"
    Property MinimumSalaryByYear As String
    ''' <summary>
    ''' Esta propiedad contiene el valor del salario minimo 
    ''' </summary>
    Property LegalMinimumSalary As Integer
    ''' <summary>
    ''' Esta propiedad contiene el auxilio de transporte 
    ''' </summary>
    Property TransportHelpValue As Integer

    WriteOnly Property ActionsOnControls As Boolean

    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ReadOnly Property MyTag As Object


    'Property Compensation As Boolean
#End Region

End Interface