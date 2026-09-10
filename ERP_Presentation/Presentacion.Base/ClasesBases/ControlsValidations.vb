'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Oscar Sierra
' Created          : 22-11-2011
'
' Last Modified By : Oscar Sierra
' Last Modified On : 22-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************



''' <summary>
''' esta clase sirve para devolver el tag y el mensaje especificado en las reglas de enterprise library para los controles que no cumplan con la validacion
''' </summary>
Public Class ControlsValidations


    Private _Tag As String
    ''' <summary>
    '''devuelve el Nombre del control especificado en enterprise library para los que no cumplan con la validacion
    ''' </summary>
    ''' <value>The tag.</value>
    Property Tag As String
        Get
            Return _Tag
        End Get
        Set(value As String)
            _Tag = value
        End Set
    End Property

    Private _Mensaje As String
    ''' <summary>
    ''' devuelve el nombre del mensaje especificado en las reglas de validacion de enterprise library
    ''' </summary>
    ''' <value>The mensaje.</value>
    Property Mensaje As String
        Get
            Return _Mensaje
        End Get
        Set(value As String)
            _Mensaje = value
        End Set
    End Property

End Class
