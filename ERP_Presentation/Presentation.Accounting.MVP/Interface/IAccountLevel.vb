'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base

#End Region

''' <summary>
''' Define las propiedades y métodos de la vista
''' </summary>
Public Interface IAccountLevel
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el valor del codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CodeAccountLevel As String

    ''' <summary>
    ''' Obtiene o establece el valor del nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameAccountLevel As String

    ''' <summary>
    ''' Obtiene o establece el valor del auxiliar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AuxAccountLevel As Boolean

    ''' <summary>
    ''' Obtiene o asigna el estado del registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Property StateAccountLevel As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
