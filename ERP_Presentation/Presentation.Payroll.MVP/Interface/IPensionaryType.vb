'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 26-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Interfaz que maneja el frontal de tipo de pensionado
''' </summary>

Public Interface IPensionaryType
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del tipo de pensionado
    ''' </summary>
    Property PensionaryTypeCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property PensionaryTypeName As String

    ''' <summary>
    ''' Propiedad que contiene el estado del tipo de pensionado
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
