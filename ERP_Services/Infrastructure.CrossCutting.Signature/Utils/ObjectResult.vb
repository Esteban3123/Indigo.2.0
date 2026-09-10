''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion encapsulando un objeto de tipo T.
''' </summary>
''' <typeparam name="T">Tipo a encapsular en la respuesta</typeparam>
Public Class ObjectResult(Of T As {Class})

    Public Sub New()

    End Sub

    Public Sub New(stateresult As Boolean, messageresult As List(Of String), message As String, objectresult As T)
        Me.StateResult = stateresult
        Me.ObjectEmbbeded = objectresult
    End Sub

    ''' <summary>
    ''' Propiedad que obtiene o asigna el estado de la acción.
    ''' </summary>
    ''' <value>Valor de la acción</value>
    ''' <returns>Valor de la ación</returns>
    Public Property StateResult As Boolean

    ''' <summary>
    ''' Propiedad que obtiene o asigna el objeto de tipo T
    ''' </summary>
    ''' <value>Objecto de tipo T</value>
    ''' <returns>El objeto de tipo T</returns>    
    Public Property ObjectEmbbeded As T

End Class
