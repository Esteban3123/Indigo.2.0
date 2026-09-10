'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 04-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Interfaz que maneja el frontal de discapacidad
''' </summary>
Public Interface IDisability
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del discapacidad
    ''' </summary>
    Property DisabilityCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del discapacidad
    ''' </summary>
    Property DisabilityDescription As String

    ''' <summary>
    ''' Propiedad que contiene el estado del discapacidad
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
