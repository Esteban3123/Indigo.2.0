'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 06-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IPositionLevel
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del Nivel de Cargo
    ''' </summary>
    Property CodePositionLevel As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del Nivel de Cargo
    ''' </summary>
    Property NamePositionLevel As String
    ''' <summary>
    ''' Esta propiedad contiene el estado del Nivel de Cargo
    ''' </summary>
    Property StatePositionLevel As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl
#End Region

End Interface
