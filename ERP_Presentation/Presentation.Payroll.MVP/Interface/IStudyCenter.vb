'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-07-2013
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
Public Interface IStudyCenter
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del centro es estudio
    ''' </summary>
    Property CodeSC As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del centro es estudio
    ''' </summary>
    Property NameSC As String
    ''' <summary>
    ''' Esta propiedad contiene el estado del centro es estudio
    ''' </summary>
    Property StatusSC As Boolean
    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
