'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls

Public Interface IRetirementReason
    Inherits IcrudBase



    ''' <summary>
    ''' Esta propiedad contiene el codigo de la razon de retiro
    ''' </summary>
    Property CodeRetirementReason As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de la razon de retiro
    ''' </summary>
    Property NameRetirementReason As String

    ''' <summary>
    ''' Esta propiedad contiene el estado de la razon de retiro
    ''' </summary>
    Property StateRetirementReason As Boolean

    ''' <summary>
    ''' esta propiedad establece los valores de ControlAcciones
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl


    ReadOnly Property MyTag As Object

    Property Compensation As Boolean

End Interface
