Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Infrastructure.CrossCutting.Exceptions

Public Class MessageIndigo

    Shared Formulario As DialogResult

    Public Shared Function Show(ByVal Mensaje As String, ByVal Icono As MessageType, ByVal Titulo As String, ByVal Botones As Botones, Optional AditionalMessage As String = "", Optional customTexButtons As String() = Nothing) As DialogResult
        Dim Mensajes As New Mensajes
        Dim _form As Form = Nothing
        If Not String.IsNullOrEmpty(Titulo) Then
            _form = New Form
            _form.Text = Titulo
        End If
        Select Case Botones
            Case Botones.SiNo, Botones.AceptarCancelar, Botones.Continuar
                Formulario = New FrmTransparent(New FrmFlyoutIndigoError(Mensaje, Botones, Icono, Nothing, Nothing, customTexButtons)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
                Mensajes.NotificationCenter(Titulo, Mensaje, Icono, Date.Now, AditionalMessage)
            Case Botones.Aceptar
                Formulario = System.Windows.Forms.DialogResult.Yes
                Mensajes.Notificar(Mensaje, Icono, _form)
                Mensajes.NotificationCenter(Titulo, Mensaje, Icono, Date.Now, "")
            Case Botones.Cancelar
                Formulario = System.Windows.Forms.DialogResult.Cancel
                Mensajes.Notificar(Mensaje, Icono, _form)
                Mensajes.NotificationCenter(Titulo, Mensaje, Icono, Date.Now, "")

            Case Botones.Ok
                Formulario = System.Windows.Forms.DialogResult.OK
                Mensajes.Notificar(Mensaje, Icono, _form)
                Mensajes.NotificationCenter(Titulo, Mensaje, Icono, Date.Now, "")
        End Select

        Return Formulario
    End Function

    Public Shared Sub Show(ByVal Mensaje As String, ByVal Icono As MessageType, ByVal Titulo As String, Optional form As Form = Nothing)
        Dim Mensajes As New Mensajes
        If form Is Nothing AndAlso Not String.IsNullOrEmpty(Titulo) Then
            form = New Form
            form.Text = Titulo
        End If
        Mensajes.Notificar(Mensaje, Icono, form)
        Mensajes.NotificationCenter(Titulo, Mensaje, Icono, Date.Now, "")
    End Sub
End Class

Public Class Mensajes
    Implements ISujeto
    Implements IDisposable

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As MDISingleInstance = MDISingleInstance.Instance

    Public Sub Desregistrar(objObservador As IObservador) Implements ISujeto.Desregistrar
        indigo.ObserversList.Remove(objObservador)
    End Sub

    Public Sub Notificar(ByVal Mensaje As String, ByVal Tipo As MessageType, Optional form As Form = Nothing) Implements ISujeto.Notificar
        If indigo.ObserversList.Count = 0 Then Exit Sub
        For Each objObservador As IObservador In indigo.ObserversList
            objObservador.MostrarMensajeSlide(Mensaje, Tipo, form)
        Next
    End Sub

    Public Sub NotificationCenter(Title As String, Message As String, TypeMessage As MessageType, MessageDate As Date, ByVal AditionalMessage As String) Implements ISujeto.NotificationCenter
        If ConfigurationFile.Instance.TypeAlertControl = eTypeAlertControl.AlertWindows Then
            If indigo.ObserversList.Count = 0 Then Exit Sub
            For Each objObservador As IObservador In indigo.ObserversList
                objObservador.NotificationCenter(Title, Message, TypeMessage, MessageDate, AditionalMessage)
            Next
        End If
    End Sub

    Public Sub Registrar(objObservador As IObservador) Implements ISujeto.Registrar
        If Not indigo.ObserversList.Any(Function(o) CType(o, Form).Name.Equals(CType(objObservador, Form).Name)) Then
            indigo.ObserversList.Add(objObservador)
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

