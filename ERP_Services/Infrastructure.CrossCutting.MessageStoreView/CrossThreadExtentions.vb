Imports System.ComponentModel
Imports System.Reflection

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

    ''' <summary>
    ''' Asigna valor a una propiedad de un Control de forma segura
    ''' evitando el cruce de hilos
    ''' </summary>
    ''' <param name="ctr">Control que contiene la propiedad a asignar</param>
    ''' <param name="pathPropertyName">Ruta completa de la propiedad que se va a asignar</param>
    ''' <param name="value">Valor a asignar</param>
    <System.Runtime.CompilerServices.Extension>
    Public Sub SetValueToProperty(ByVal ctr As System.Windows.Forms.Control, ByVal pathPropertyName As String, ByVal value As Object)
        If ctr.InvokeRequired Then
            ctr.BeginInvoke(Sub()
                                InternalSetValueToProperty(ctr, pathPropertyName, value)
                            End Sub)
        Else
            InternalSetValueToProperty(ctr, pathPropertyName, value)
        End If
    End Sub
    Private Sub InternalSetValueToProperty(ByVal obj As Object, ByVal pathPropertyName As String, ByVal value As Object)
        Dim props() As String = pathPropertyName.Split(".")
        Dim listProps As New List(Of String)(props)
        If props.Length = 0 Then Return
        If props IsNot Nothing AndAlso props.Length > 1 Then
            listProps.RemoveAt(0)
            Dim newPathPropertyName As String = String.Join(".", listProps.ToArray())
            Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    If p IsNot Nothing Then
                        Dim objAux = p.GetValue(obj)
                        If objAux IsNot Nothing Then
                            InternalSetValueToProperty(objAux, newPathPropertyName, value)
                        End If
                    End If
                Next
            End If
        Else
            Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    If p IsNot Nothing Then
                        p.SetValue(obj, value)
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna valor a la propiedad DoubleBuffered del control
    ''' </summary>
    ''' <param name="c">Control que extiende el metodo</param>
    ''' <param name="value">Valor a asignar</param>
    <System.Runtime.CompilerServices.Extension> _
    Public Sub SetDoubleBuffered(ByVal c As System.Windows.Forms.Control, ByVal value As Boolean)
        If System.Windows.Forms.SystemInformation.TerminalServerSession Then
            Return
        End If
        Dim aProp As System.Reflection.PropertyInfo = GetType(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        aProp.SetValue(c, value, Nothing)
    End Sub

#End Region

End Module
