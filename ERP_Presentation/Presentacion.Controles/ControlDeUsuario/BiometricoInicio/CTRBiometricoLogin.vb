'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Jorge Leonardo Vernaza
' Created          : 08-11-2011
'
' Last Modified By : Jorge Leonardo Vernaza
' Last Modified On : 08-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
#End Region

''' <summary>
''' Clase con las funcionaledidades del control para el puerto Biometrico que permite estar desacoplado del frontal
''' </summary>
Public Class CTRBiometricoLogin
#Region "Variables Generales"
    ''' <summary>
    ''' Varialbe que se utiliza para instanciar la clase Util
    ''' </summary>
    Dim myxUtil As Util
    ''' <summary>
    ''' Evento Publica que se utiliza para devolver los datos obtenidos despues de verificar la huella
    ''' </summary>
    Public Event DevolverDatos()
    ''' <summary>
    ''' Evento publico que permite bloquear los controles cuando se ha comprobado la huella
    ''' </summary>
    Public Event BloquearControles()
    ''' <summary>
    ''' Evento publico que permite desbloquear los controles cuando la huella se ingreso mal
    ''' </summary>
    Public Event DesbloquearControles()
#End Region
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad que Envia y recive la contraseña
    ''' </summary>
    Private _Contrasena As String
    Property Contraseña As String
        Get
            Return _Contrasena
        End Get
        Set(ByVal value As String)
            _Contrasena = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que Envia y recive El codigo
    ''' </summary>
    Private _Codigo As String
    Property Codigo As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

#End Region
#Region "Eventos Del Biometrico"
    ' ''' <summary>
    ' ''' Evento Que se Ejecuta al Colocar Un Dedo Sobre el sensor Biometrico
    ' ''' </summary>
    'Private Sub AxGrFingerXCtrl1_ImageAcquired(ByVal sender As System.Object, ByVal e As Object) Handles AxGrFingerXCtrl1.ImageAcquired
    '    'se Lanza el evento bloquear controles del logion cuando se detecta que se ha colocado una huella sobre el sensor
    '    RaiseEvent BloquearControles()
    '    myxUtil.raw.height = e.height
    '    myxUtil.raw.width = e.width
    '    myxUtil.raw.res = e.res
    '    myxUtil.raw.img = CType(e.rawImage, Byte())
    '    Dim ret As Integer
    '    'Metodo para extraer la informacion biometrica de la huella
    '    ret = myxUtil.ExtractTemplate()
    '    'Metodo Para identificar la huella ingresada con las huellas guardadas en la base de datos
    '    ret = CInt(myxUtil.identificar())
    '    'si es mayor que sero es porque hubo coincidencia
    '    If ret > 0 Then
    '        ptbHuella1.Image = Global.Presentation.Controls.My.Resources.Resources.biometricochulo
    '        Contraseña = myxUtil.Contraseña
    '        Codigo = myxUtil.Codigo
    '        'enviamos los datos de codigo y contraseña con el evento devolver datos
    '        RaiseEvent DevolverDatos()
    '        'si es igual a cero entonces es porque no encontro ninguna coincidencia
    '    ElseIf ret = 0 Then
    '        ptbHuella1.Image = Global.Presentation.Controls.My.Resources.Resources.biometricocerrar
    '        'Cuando No encontro conicidencias entonces desbloque los controles
    '        RaiseEvent DesbloquearControles()
    '    End If
    'End Sub
    ''' <summary>
    ''' Evento que se lanza cuando se conecta el sensor biometrico entonces se inicia la captura
    ''' </summary>
    'Private Sub AxGrFingerXCtrl1_SensorPlug(ByVal sender As Object, ByVal e As Object) Handles AxGrFingerXCtrl1.SensorPlug
    '    AxGrFingerXCtrl1.CapStartCapture(e.idSensor)
    'End Sub
    ''' <summary>
    ''' Evento que se lanza cuando se desconecta el sensor biometrico entonces se detiene la captura
    ''' </summary>
    'Private Sub AxGrFingerXCtrl1_SensorUnplug(ByVal sender As Object, ByVal e As Object) Handles AxGrFingerXCtrl1.SensorUnplug
    '    AxGrFingerXCtrl1.CapStopCapture(e.idSensor)
    'End Sub
#End Region
#Region "metodos del control BIometrico"
    ''' <summary>
    ''' Metodo para iniciar el sensor biometrico
    ''' </summary>
    Public Sub Iniciar()
        Try
            Dim err As Integer
            'instanciamos el picturebox el listbox y la libreria en el constructor de la clase util
            myxUtil = New Util(lstLogHuella, ptbHuella1, AxGrFingerXCtrl1)
            'inicializamos el dispositivo
            err = myxUtil.InitializeGrFinger()
            'Si es menor que sero entonces hubo un error al iniciar el sensor
            If err < 0 Then
                Exit Sub
            End If
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' Metodo Para detener el sensor biometrico
    ''' </summary>
    Public Sub finalizar()
        Try
            myxUtil.FinalizeGrFinger()
        Catch
        End Try
    End Sub
#End Region
End Class
