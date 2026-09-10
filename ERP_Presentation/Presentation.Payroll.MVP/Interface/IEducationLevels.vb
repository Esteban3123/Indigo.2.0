'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 12-04-2013
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
Public Interface IEducationLevels
    Inherits IcrudBase
#Region "Properties"
    ''' <summary>
    ''' Contiene el codigo de los niveles de educacion
    ''' </summary>
    Property CodeEducationLevels As String
    ''' <summary>
    ''' Contiene el nombre de los niveles de educacion
    ''' </summary>
    Property NameEducationLevels As String
    ''' <summary>
    ''' Contiene el estado de los niveles de educacion
    ''' </summary>
    Property StateEducationLevels As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl
#End Region
End Interface
