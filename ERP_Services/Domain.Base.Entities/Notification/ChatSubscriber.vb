''' <summary>
''' Contrato de notificación de los clientes subscritos al servicio de mensajeria y administración
''' </summary>
Public Class ChatSubscriber
    Inherits Subscriber

#Region "Fields"
#End Region

#Region "Properties"
#End Region

#Region "Builders"

    ''' <summary>
    ''' Genera una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        MyBase.New(String.Empty, New Machine(), New Application(), String.Empty)
    End Sub

    ''' <summary>
    ''' Genera una nueva instancia de la clase
    ''' </summary>
    ''' <param name="uid">Código de identificación única del subscriptor al servicio</param>
    ''' <param name="appMahine">Maquina desde donde se realiza la subscripción al servicio</param>
    ''' <param name="app">Aplicación desde donde se realiza la subscripción al servicio</param>
    ''' <param name="idCallBack">Id de la conexion del subscriptor</param>
    Public Sub New(ByVal uid As String, ByVal appMahine As Machine, ByVal app As Application, ByVal idCallBack As String)
        MyBase.New(uid, appMahine, app, idCallBack)
    End Sub

#End Region

End Class