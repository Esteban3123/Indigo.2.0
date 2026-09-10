Imports System.ComponentModel

''' <summary>
''' Encapsula los metodos extendidos en los objetos de tipo ISynchronizeInvoke
''' </summary>
Public Module CrossThreadExtentions

#Region "Methods"

    ''' <summary>
    ''' Invoca una función delegado que me retorna el valor de un método o una propiedad
    ''' </summary>
    ''' <typeparam name="T">Tipo del objeto quien extiende el metodo</typeparam>
    ''' <typeparam name="TResult">Tipo del resultado que va a retornarl la función delegado</typeparam>
    ''' <param name="isi">Objeto quien extiende el metodo</param>
    ''' <param name="call">Función delegado</param>
    ''' <returns>Valor retornado por la función delegado</returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function SafeInvoke(Of T As ISynchronizeInvoke, TResult)(isi As T, [call] As Func(Of T, TResult)) As TResult
        If isi.InvokeRequired Then
            Dim result As IAsyncResult = isi.BeginInvoke([call], New Object() {isi})
            Dim endResult As Object = isi.EndInvoke(result)
            Return DirectCast(endResult, TResult)
        Else
            Return [call](isi)
        End If
    End Function

    ''' <summary>
    ''' Invoca un procedimiento delegado
    ''' </summary>
    ''' <typeparam name="T">Tipo del objeto quien extiende el metodo</typeparam>
    ''' <param name="isi">Objeto quien extiende el metodo</param>
    ''' <param name="call">Procedimiento delegado</param>
    <System.Runtime.CompilerServices.Extension> _
    Public Sub SafeInvoke(Of T As ISynchronizeInvoke)(isi As T, [call] As Action(Of T))
        If isi.InvokeRequired Then
            isi.BeginInvoke([call], New Object() {isi})
        Else
            [call](isi)
        End If
    End Sub

#End Region

End Module
