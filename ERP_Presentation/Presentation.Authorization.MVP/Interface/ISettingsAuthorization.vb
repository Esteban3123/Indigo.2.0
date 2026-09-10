#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface ISettingsAuthorization
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Esta propiedad establece el valor MyLayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Establece si la asignación por colas se realiza de manera manual o automática
    ''' </summary>
    ''' <returns></returns>
    Property AutomaticAllocation As Boolean

    ''' <summary>
    ''' Identifica la cantidad de dias que se validaran al momento de realizar la asignación por colas
    ''' </summary>
    ''' <returns></returns>
    Property DaysToAssign As Integer

#End Region

End Interface
