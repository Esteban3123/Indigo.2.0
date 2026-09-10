'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "imports"
Imports Presentation.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Interfaz que contiene las propiedades del frontal de Tipos de telefono
''' </summary>
Public Interface IPhoneType
    Inherits IcrudBase
    ''' <summary>
    ''' Propiedad que contiene el codigo del Departamento
    ''' </summary>
    Property CodePhoneType As String
    ''' <summary>
    ''' Propiedad que contiene el nombre del Departamento
    ''' </summary>
    Property NamePhoneType As String
    ''' <summary>
    ''' Propiedad que contiene el estado de el tipo de telefono
    ''' </summary>
    Property StatePhoneType As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
End Interface
