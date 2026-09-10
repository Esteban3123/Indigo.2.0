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
#Region "Librerias importadas"
Imports DevExpress.XtraEditors
#End Region
''' <summary>
''' Clase con las funcionaledidades del control para el puerto Biometrico que permite estar desacoplado del frontal
''' </summary>
''' 

Public Class CtrBiometrico

#Region "Variables Generales Y load"
    Public Const ERR_INVALID_ID As Integer = -998
    Public Const ERR_INVALID_TEMPLATE As Integer = -997
    Private Declare Function GetDC Lib "user32" (ByVal hwnd As Int32) As Int32
    Private Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Int32, ByVal hdc As Int32) As Int32
    ''' <summary>
    ''' variable que contiene el id del sensor
    ''' </summary>
    Dim idsensore As String
    ''' <summary>
    ''' Variable que contiene la imagen de la huella
    ''' </summary>
    Public raw As RawImage
    ''' <summary>
    ''' Variable que contiene la informacion Biometrica de la huella
    ''' </summary>
    Public template As TTemplate
    ''' <summary>
    ''' Variable que contiene temporalmente la informacion biometrica de la huella para comparaciones
    ''' </summary>
    Dim temporal As TTemplate
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
    ''' Variable que se utiliza para contar la tres huella ingresadas
    ''' </summary>
    Dim contador As Integer
    ''' <summary>
    ''' Evento Publico que me permite mostrar un mensaje en el frontal que este usando el control
    ''' </summary>
    Public Event MostrarMensaje()
    Private Sub CtrBiometrico_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        INDbtnCerrarDispositivo.Visible = False
    End Sub
#End Region
#Region "Metodos y funciones del dispositivo"
    ''' <summary>
    ''' Metodo que permite inicar el dispositivo
    ''' </summary>
    Public Sub IniciarDispositivo()
        Try
            FinalizarDispositivo()
            template = New TTemplate
            Dim err As Integer
            Control.CheckForIllegalCrossThreadCalls = False
            err = InitializeGrFinger()
            'si tiene errores entonces que finalize
            If err < 0 Then
                Exit Sub
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Funcion que permite la inicializacion del dispositivo y la creacion de una nueva imagen
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeGrFinger() As Integer
        Dim err As Integer
        template.Size = 0
        'limpia la imagen de la huella para crear una nueva
        raw.img = Nothing
        raw.width = 0
        raw.height = 0
        ' Inicializa la libreria
        err = GrFinger.GrInitialize()
        If err < 0 Then
            Mensaje = "Ocurrio Un Error Al Iniciar El Dispositivo"
            RaiseEvent MostrarMensaje()
            Return err
        Else
            INDbtnCerrarDispositivo.Visible = True
            INDBtnIniciar.Visible = False
        End If
        Return GrFinger.CapInitialize(AddressOf GrFinger_SensorPlugUnPlug)
    End Function

    ''' <summary>
    ''' Metodo que permite conectar el dispositivo de reconocimiento biometrico
    ''' </summary>
    ''' <param name="idSensor">la identificacion del dispositivo</param>
    ''' <param name="evt">el evento</param>
    Public Sub GrFinger_SensorPlugUnPlug(ByVal idSensor As String, ByVal evt As Integer)
        'si se conecta el dispositivo al computador
        If evt = GrFinger.GR_PLUG Then
            GrFinger.CapStartCapture(idSensor, AddressOf GrFinger_FingerUpDown, AddressOf GrFinger_ImageAcquired)
        End If
        'si se desconecta el dispositivo
        If evt = GrFinger.GR_UNPLUG Then
            GrFinger.CapStopCapture(idSensor)
        End If
        idsensore = idSensor
    End Sub

    ''' <summary>
    ''' metodo que se dispara al detectar un dedo sobre el dispositivo de captura de huella
    ''' </summary>
    ''' <param name="idSensor">la identificacion del sensor</param>
    ''' <param name="evt">el evento</param>
    Public Sub GrFinger_FingerUpDown(ByVal idSensor As String, ByVal evt As Integer)
        'si se pone el dedo sobre el sensor
        If evt = GrFinger.GR_FINGER_DOWN Then
        End If
        'si se retira el dedo sobre el sensor
        If evt = GrFinger.GR_FINGER_UP Then
        End If
    End Sub

    Public Sub BloquerDispositivo()
        INDBtnIniciar.Visible = False
        INDbtnCerrarDispositivo.Visible = False
    End Sub

    Public Sub DesBloquerDispositivo()
        INDBtnIniciar.Visible = True
        INDbtnCerrarDispositivo.Visible = True
    End Sub

#End Region
#Region "metodos y funciones para la creacion de la huella "



    ''' <summary>
    ''' Metodo que permite establecer una nueva imagen de huella 
    ''' </summary>
    ''' <param name="idSensor">la identificacion del sensor</param>
    ''' <param name="width">el ancho</param>
    ''' <param name="height">el alto</param>
    ''' <param name="rawImage">la imagen de la huella</param>
    ''' <param name="res">la resolucion</param>
    Public Sub GrFinger_ImageAcquired(ByVal idSensor As String, ByVal width As Integer, ByVal height As Integer, ByVal rawImage As Byte(), ByVal res As Integer)
        'Se establece la nueva imagen adquirida
        raw.height = height
        raw.width = width
        raw.res = res
        raw.img = rawImage
        'coloca la imagen obtenida sobre el control PictureEdit
        PrintBiometricDisplay(False, GrFinger.GR_DEFAULT_CONTEXT)

        If contador <= 3 Then
            Select Case contador
                Case Is = 0
                    plantilla1 = New TTemplate
                    extrarplantilla(plantilla1)
                    plantilla1 = template
                    INDpeImagenBiometrico.Image = INDicBiometrico.Images(1)
                    contador = contador + 1
                Case Is = 1
                    plantilla2 = New TTemplate
                    extrarplantilla(plantilla2)
                    plantilla2 = template
                    INDpeImagenBiometrico.Image = INDicBiometrico.Images(2)
                    contador = contador + 1
                Case Is = 2
                    plantilla3 = New TTemplate
                    extrarplantilla(plantilla3)
                    plantilla3 = template
                    INDpeImagenBiometrico.Image = INDicBiometrico.Images(3)
                    contador = contador + 1
                Case Is = 4
                    Exit Sub
            End Select
            If contador >= 3 Then
                If comprovar() = False Then
                    plantilla1 = Nothing
                    plantilla2 = Nothing
                    plantilla3 = Nothing
                    template = Nothing
                    INDpeImagenBiometrico.Image = INDicBiometrico.Images(4)
                    Mensaje = "Las Huellas Ingresadas No Coinciden"
                    RaiseEvent MostrarMensaje()
                    contador = 0
                Else
                    FinalizarDispositivo()
                    Mensaje = "Las Huellas Han Sido Tomadas Satisfactoriamente"
                    RaiseEvent MostrarMensaje()
                    contador = 4
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo para poner la imagen obtenida de la huella o la informacion biometrica sobre el control PictureEdit
    ''' </summary>
    Public Sub PrintBiometricDisplay(ByVal biometricDisplay As Boolean, ByVal context As Integer)
        ' Manejador de la imagen de la huella
        Dim handle As Integer
        ' pantalla del dispositivo
        Dim hdc As Integer = GetDC(0)

        If biometricDisplay Then
            ' obtiene la informacion biometrica de la huella
            GrFinger.BiometricDisplay(DirectCast(template.tpt, Byte()), DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), CInt(raw.res), hdc, handle, context)
        Else
            'obtiene la imagen de la huella
            GrFinger.CapRawImageToHandle(DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), hdc, handle)
        End If

        ' pinta el control PictureEdit con la imagen de la huella o con la informacion biometrica
        If handle <> 0 Then
            INDpeImagenBiometrico.Image = System.Drawing.Bitmap.FromHbitmap(New IntPtr(handle))
            INDpeImagenBiometrico.Update()
        End If
        ' Libera la pantalla del dispositivo
        ReleaseDC(0, hdc)

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para finalizar el dispositivo
    ''' </summary>
    Public Sub FinalizarDispositivo()
        Try
            GrFinger.GrFinalize()
            GrFinger.CapFinalize()
            INDbtnCerrarDispositivo.Visible = False
            INDBtnIniciar.Visible = True
            contador = 0
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' Metodo que coloca la imagen por defecto en el control, finaliza el dispositivo
    ''' </summary>
    Public Sub LimpiarControles()
        contador = 0
        FinalizarDispositivo()
        INDpeImagenBiometrico.Image = INDicBiometrico.Images(0)
        limpiarplantillas()
    End Sub

    ''' <summary>
    ''' Este metodo lo ulizamos para limpiar todas las plantillas
    ''' </summary>
    Public Sub limpiarplantillas()
        plantilla1 = Nothing
        plantilla2 = Nothing
        plantilla3 = Nothing
        template = Nothing
    End Sub

#End Region
#Region "Metodos Y Funciones que permiten la identificacion de la huella"
    ''' <summary>
    ''' Este metodo sirve para identificar una huella
    ''' </summary>
    Public Sub identificar()
        Dim ret As Integer, score As Integer
        score = 0
        ret = Identify(score)
        If ret = 0 Then
            PrintBiometricDisplay(True, GrFinger.GR_DEFAULT_CONTEXT)
        End If
    End Sub

    ''' <summary>
    ''' en esta funcion se compara la informacion biometrica de la huella que se acaba de colocar con la huella que se cargo desde la base de datos.
    ''' </summary>
    ''' <param name="score">la puntuacion</param>
    ''' <returns></returns>
    Public Function Identify(ByRef score As Integer) As Integer
        temporal = New TTemplate
        Dim ret As Integer
        ' comprobamos que la plantilla sea valida
        If Not TemplateIsValid() Then Return ERR_INVALID_TEMPLATE

        ' iniciamos la la comparacion primero de la informacion biometrica de la huella que acabamosd de colocar
        Dim tmpTpt As Array = Array.CreateInstance(GetType(Byte), template.Size)
        Array.Copy(template.tpt, tmpTpt, template.Size)
        ret = GrFinger.IdentifyPrepare(DirectCast(tmpTpt, Byte()), GrFinger.GR_DEFAULT_CONTEXT)

        If ret < 0 Then Return ret
        ' obtenemos la informacion biometrica de la huella que estaba guardada en la base de datos
        Dim templates As TTemplate = temporal

        ' ahora comparamos con la huella que estaba guardada en la base de datos
        Dim tempTpt As Array = Array.CreateInstance(GetType(Byte), templates.Size)
        Array.Copy(templates.tpt, tempTpt, templates.Size)
        ret = GrFinger.Identify(DirectCast(tmpTpt, Byte()), score, GrFinger.GR_DEFAULT_CONTEXT)
        If ret = GrFinger.GR_MATCH Then
        End If
        If ret < 0 Then Return ret
        Return GrFinger.GR_NOT_MATCH
    End Function

    ''' <summary>
    ''' validamos que el templete es valido
    ''' </summary>
    ''' <returns></returns>
    Private Function TemplateIsValid() As Boolean
        Return template.Size > 0
    End Function

#End Region
#Region "Propiedades"

    Private _Mensaje As String
    Property Mensaje As String
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
        End Set
    End Property


    Public Sub ValorGauget(ByVal valor As Integer)
        If valor = 0 Then
            INDpeImagenBiometrico.Image = INDicBiometrico.Images(0)
        ElseIf valor = 1 Then
            INDpeImagenBiometrico.Image = INDicBiometrico.Images(5)
        End If
    End Sub

    Public _ValorTemplate As TTemplate
    ''' <summary>
    ''' Propiedad que obtiene o devuelva la informacion biometrica de la huella
    ''' </summary>
    ''' <value>The valor template.</value>
    Public Property ValorTemplate As TTemplate
        Get
            If plantilla1 Is Nothing Or plantilla2 Is Nothing Or plantilla3 Is Nothing Then
                template = Nothing
            Else
                If comprovar() = True Or comprovar2() = True Then
                    template = plantilla1
                ElseIf comprovar3() = True Then
                    template = plantilla2
                End If
            End If
            Return template
        End Get
        Set(ByVal value As TTemplate)
            temporal = value
        End Set
    End Property


#End Region
#Region "Eventos del control"
    ''' <summary>
    ''' Evento Clic en el boton Identificar Huella
    ''' </summary>
    Private Sub INDbtnIdentificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        identificar()
    End Sub

    ''' <summary>
    ''' Evento Clic en iniciar dispositivo
    ''' </summary>
    Private Sub INDBtnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnIniciar.Click
        IniciarDispositivo()
    End Sub

    ''' <summary>
    ''' Evento Clic en cerrar dispositivo
    ''' </summary>
    Private Sub INDbtnCerrarDipositivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnCerrarDispositivo.Click
        LimpiarControles()
        FinalizarDispositivo()
    End Sub
#End Region
#Region "Metodos y funciones para extrar la plantilla de la huella"
    ''' <summary>
    ''' Metodo para extrar la plantilla de la huella y dibujarla en el control
    ''' </summary>
    Public Sub extrarplantilla(ByVal plantilla As TTemplate)
        Dim ret As Integer
        'fucion para extraer la plantilla
        ret = ExtractTemplate(plantilla)
        If ret >= 0 Then
            'crea la imagen obtenida en el control PictureEdit
            PrintBiometricDisplay(True, GrFinger.GR_NO_CONTEXT)
        End If
    End Sub

    ''' <summary>
    ''' metodo para extrar la plantilla de la informacion biometrica
    ''' </summary>
    ''' <returns></returns>
    Function ExtractTemplate(ByVal plantilla As TTemplate) As Integer
        template = plantilla
        Dim ret As Integer
        'establece el tamaño de la plantilla
        template.Size = template.tpt.Length
        'funcion para extraer la plantilla
        ret = GrFinger.Extract(DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), CInt(raw.res), DirectCast(template.tpt, Byte()), CInt(template.Size), GrFinger.GR_DEFAULT_CONTEXT)
        ' si el resultado es menor que 0 entonces hubo un error en la extraccion de la plantilla
        If ret < 0 Then template.Size = 0
        Return ret
    End Function
#End Region
#Region "Funciones para comprovar las tres huellas ingresadas"
    ''' <summary>
    ''' Funciones que utilizamos para verificar que las tres huellas ingresadas sean correctas almenos dos
    ''' </summary>
    ''' <returns></returns>
    Public Function comprovar() As Boolean
        If plantilla1 Is Nothing Or plantilla2 Is Nothing Or plantilla3 Is Nothing Then
            Return False
        End If
        Dim ret As Integer, score As Integer
        ' iniciamos la la comparacion primero de la informacion biometrica de la huella que acabamosd de colocar
        Dim tmpTpt As Array = Array.CreateInstance(GetType(Byte), plantilla1.Size)
        Array.Copy(plantilla1.tpt, tmpTpt, plantilla1.Size)
        ret = GrFinger.IdentifyPrepare(DirectCast(tmpTpt, Byte()), GrFinger.GR_DEFAULT_CONTEXT)
        Dim templates As TTemplate = plantilla2
        ' ahora comparamos con la huella 2
        Dim tempTpt As Array = Array.CreateInstance(GetType(Byte), templates.Size)
        Array.Copy(templates.tpt, tempTpt, templates.Size)
        ret = GrFinger.Identify(DirectCast(tmpTpt, Byte()), score, GrFinger.GR_DEFAULT_CONTEXT)
        If ret = GrFinger.GR_MATCH Then
        End If
        If score < 50 Then
            If comprovar2() = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
            limpiarplantillas()
        End If
    End Function
    Public Function comprovar2() As Boolean
        Dim ret As Integer, score As Integer
        ' iniciamos la la comparacion primero de la informacion biometrica de la huella que acabamosd de colocar
        Dim tmpTpt As Array = Array.CreateInstance(GetType(Byte), plantilla1.Size)
        Array.Copy(plantilla1.tpt, tmpTpt, plantilla1.Size)
        ret = GrFinger.IdentifyPrepare(DirectCast(tmpTpt, Byte()), GrFinger.GR_DEFAULT_CONTEXT)
        Dim templates As TTemplate = plantilla3
        'ahora comparamos con la huella 3
        Dim tempTpt As Array = Array.CreateInstance(GetType(Byte), templates.Size)
        Array.Copy(templates.tpt, tempTpt, templates.Size)
        ret = GrFinger.Identify(DirectCast(tmpTpt, Byte()), score, GrFinger.GR_DEFAULT_CONTEXT)
        If ret = GrFinger.GR_MATCH Then
        End If
        If score < 50 Then
            If comprovar3() = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
            limpiarplantillas()
        End If
    End Function
    Public Function comprovar3() As Boolean
        Dim ret As Integer, score As Integer
        ' iniciamos la la comparacion primero de la informacion biometrica de la huella que acabamosd de colocar
        Dim tmpTpt As Array = Array.CreateInstance(GetType(Byte), plantilla2.Size)
        Array.Copy(plantilla2.tpt, tmpTpt, plantilla2.Size)
        ret = GrFinger.IdentifyPrepare(DirectCast(tmpTpt, Byte()), GrFinger.GR_DEFAULT_CONTEXT)


        Dim templates As TTemplate = plantilla3

        ' ahora comparamos con la huella 3
        Dim tempTpt As Array = Array.CreateInstance(GetType(Byte), templates.Size)
        Array.Copy(templates.tpt, tempTpt, templates.Size)
        ret = GrFinger.Identify(DirectCast(tmpTpt, Byte()), score, GrFinger.GR_DEFAULT_CONTEXT)
        If ret = GrFinger.GR_MATCH Then
        End If
        If score < 50 Then
            Return False
        Else
            Return True
            limpiarplantillas()
        End If
    End Function
#End Region

End Class