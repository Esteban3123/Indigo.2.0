'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 10-07-2013
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
Public Interface ITimeUnit
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la unidad de tiempo
    ''' </summary>
    Property CodeTU As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la unidad de tiempo
    ''' </summary>
    Property NameTU As String
    ''' <summary>
    ''' Esta propiedad contiene el estado de la unidad de tiempo
    ''' </summary>
    Property StatusTU As Boolean
    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
