'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base

Public Interface ITaxExemptions
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo de la exoneracion tributaria
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Nombre de la exoneracion tributaria
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Tipo de aplicacion
    ''' </summary>
    Property ApplicationType As Boolean

    ''' <summary>
    ''' Codigo interno de la exoneracion tributaria
    ''' </summary>
    ''' <returns></returns>
    Property InternalCode As String

    ''' <summary>
    ''' Estado de la exoneracion tributaria
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Activa o desactiva los controles del formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
