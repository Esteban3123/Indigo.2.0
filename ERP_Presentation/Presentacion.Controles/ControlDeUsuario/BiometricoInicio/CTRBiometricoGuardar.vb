'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Jorge Leonardo Vernaza
' Created          : 27-10-2011
'
' Last Modified By : Jorge Leonardo Vernaza
' Last Modified On : 27-10-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
#End Region

''' <summary>
''' Clase con las funcionaledidades del control para el puerto Biometrico que permite estar desacoplado del frontal
''' </summary>
Public Class CTRBiometricoGuardar
#Region "Variables Generales"
    Dim comprobacion As Integer
    ''' <summary>
    ''' Variable que se utiliza para contar la tres huella ingresadas
    ''' </summary>
    Dim contador As Integer
    ''' <summary>
    ''' variable que contiene la informacion biometrica de la primera huella ingresada
    ''' </summary>
    Dim plantilla1 As TTemplate
    ''' <summary>
    ''' variable que contiene la informacion biometrica de la segunda huella ingresada
    ''' </summary>
    Dim plantilla2 As TTemplate
    ''' <summary>
    ''' variable que contiene la informacion biometrica de la tercera huella ingresada
    ''' </summary>
    Dim plantilla3 As TTemplate
    ''' <summary>
    ''' Varialbe que se utiliza para instanciar la clase Util
    ''' </summary>
    Dim myxUtil As Util
    ''' <summary>
    ''' Evento Publico de mostrar mostrar el mensaje en el visor de eventos
    ''' </summary>
    Public Event MostrarMensaje()
#End Region
#Region "Propiedades"
    Public _ValorTemplate As TTemplate
    ''' <summary>
    ''' Propiedad que obtiene o devuelva la informacion biometrica de la huella
    ''' </summary>
    ''' <value>The valor template.</value>
    Public Property ValorTemplate As TTemplate
        Get
            'si no ha ingresado las tres huellas entonces envia nothing
            If plantilla1 Is Nothing Or plantilla2 Is Nothing Or plantilla3 Is Nothing Then
                myxUtil.template = Nothing
            Else
                'si la comprobacion es 1 o 2 entonces que retorne la primer huella ingresada
                If comprobacion = 1 Or comprobacion = 2 Then
                    myxUtil.template = plantilla1
                    'si comprobacion es 3 entonces guarde la segunda plantilla
                ElseIf comprobacion = 3 Then
                    myxUtil.template = plantilla2
                End If
            End If
            Return myxUtil.template
        End Get
        Set(ByVal value As TTemplate)
            myxUtil.template = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad Que Obtiene y establece el mensaje
    ''' </summary>
    Private _Mensaje As String
    Property Mensaje As String
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
        End Set
    End Property
#End Region
#Region "Eventos Del Biometrico"
    ' ''' <summary>
    ' ''' Evento Que se Ejecuta al Colocar Un Dedo Sobre el sensor Biometrico
    ' ''' </summary>
    'Private Sub AxGrFingerXCtrl1_ImageAcquired(ByVal sender As System.Object, ByVal e As Object) Handles AxGrFingerXCtrl1.ImageAcquired
    '    'se Lanza el evento bloquear controles del logion cuando se detecta que se ha colocado una huella sobre el sensor
    '    myxUtil.raw.height = e.height
    '    myxUtil.raw.width = e.width
    '    myxUtil.raw.res = e.res
    '    myxUtil.raw.img = CType(e.rawImage, Byte())
    '    myxUtil.PrintBiometricDisplay(False, GrFinger.GR_DEFAULT_CONTEXT)
    '    'realizamos la estracion de las 3 plantillas de la huella
    '    If contador <= 3 Then
    '        Select Case contador
    '            Case Is = 0
    '                plantilla1 = New TTemplate
    '                myxUtil.ExtraerPlantillas(plantilla1)
    '                plantilla1 = myxUtil.template
    '                contador = contador + 1
    '                myxUtil.WriteLog("Huella 1 de 3")
    '            Case Is = 1
    '                plantilla2 = New TTemplate
    '                myxUtil.ExtraerPlantillas(plantilla2)
    '                plantilla2 = myxUtil.template
    '                contador = contador + 1
    '                myxUtil.WriteLog("Huella 2 de 3")
    '            Case Is = 2
    '                plantilla3 = New TTemplate
    '                myxUtil.ExtraerPlantillas(plantilla3)
    '                plantilla3 = myxUtil.template
    '                contador = contador + 1
    '                myxUtil.WriteLog("Huella 3 de 3")
    '        End Select
    '        If contador = 3 Then
    '            contador = 0
    '            'comprobamos las 3 huellas entre si para encontrar al menos coincidencia entre dos huellas
    '            comprobacion = myxUtil.comprovar(plantilla1, plantilla2, plantilla3)
    '            If comprobacion = 0 Then
    '                plantilla1 = Nothing
    '                plantilla2 = Nothing
    '                plantilla3 = Nothing
    '                myxUtil.template = Nothing
    '                Mensaje = "Las Huellas Ingresadas No Coinciden"
    '                myxUtil.WriteLog("Ingrese Huellas de Nuevo")
    '                RaiseEvent MostrarMensaje()
    '            Else
    '                myxUtil.FinalizeGrFinger()
    '                INDbtnCerrarDispositivo.Enabled = False
    '                INDBtnIniciar.Enabled = True
    '                myxUtil.WriteLog("Huellas Adquiridas")
    '                Mensaje = "Las Huellas Han Sido Tomadas Satisfactoriamente"
    '                RaiseEvent MostrarMensaje()

    '            End If

    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ' ''' Evento que se lanza cuando se conecta el sensor biometrico entonces se inicia la captura
    ' ''' </summary>
    'Private Sub AxGrFingerXCtrl1_SensorPlug(ByVal sender As Object, ByVal e As Object) Handles AxGrFingerXCtrl1.SensorPlug
    '    AxGrFingerXCtrl1.CapStartCapture(e.idSensor)
    'End Sub
    ' ''' <summary>
    ' ''' Evento que se lanza cuando se desconecta el sensor biometrico entonces se detiene la captura
    ' ''' </summary>
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
                myxUtil.WriteLog("No Se Ha Podido Iniciar")
                Exit Sub
            Else
                myxUtil.WriteLog("Biometrico Esta Ensendido")
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
            myxUtil.WriteLog("El Biometrico Se Ha Finalizado")
        Catch
        End Try
    End Sub
#End Region
#Region "Eventos Botones"
    ''' <summary>
    ''' Evento Clic en El boton iniciar del dispositivo Biometrico
    ''' </summary>
    Private Sub INDBtnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnIniciar.Click
        Iniciar()
        INDBtnIniciar.Enabled = False
        INDbtnCerrarDispositivo.Enabled = True
    End Sub

    ''' <summary>
    ''' Evento Clic en el boton detener dispositivo
    ''' </summary>
    Private Sub INDbtnCerrarDispositivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnCerrarDispositivo.Click
        myxUtil.FinalizeGrFinger()
        INDBtnIniciar.Enabled = True
        INDbtnCerrarDispositivo.Enabled = False
    End Sub
#End Region
End Class
