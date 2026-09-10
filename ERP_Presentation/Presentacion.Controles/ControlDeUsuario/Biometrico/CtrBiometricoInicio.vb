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
#Region "Librerias importadas"
Imports DevExpress.XtraEditors
Imports System.Data
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent.IndigoReference.Security
Imports System.Windows.Forms
Imports Domain.Security.Entities
#End Region
''' <summary>
''' Clase con las funcionaledidades del control para el puerto Biometrico que permite estar desacoplado del frontal
''' </summary>
''' 

Public Class CtrBiometricoInicio

#Region "Variables Generales Y load"
    Public Const ERR_INVALID_ID As Integer = -998
    Public Const ERR_INVALID_TEMPLATE As Integer = -997
    Private Declare Function GetDC Lib "user32" (ByVal hwnd As Int32) As Int32
    Private Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Int32, ByVal hdc As Int32) As Int32
    ''' <summary>
    ''' Variable que contiene la imagen de la huella
    ''' </summary>
    Public raw As RawImage
    ''' <summary>
    ''' Variable que contiene el binario con la informacion biometrica de la huella
    ''' </summary>
    Public template As TTemplate
    ''' <summary>
    ''' variable que recive la huella que se recibe temporalmente para ser comparada con la que existe en la base de datos.
    ''' </summary>
    Dim temporal As TTemplate
    ''' <summary>
    ''' variable que se usa para instanciar el modelo
    ''' </summary>
    Dim modelo As MCtrBiometricoInicio
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
    ''' <summary>
    ''' variable que contiene la identificacion del sensor
    ''' </summary>
    Dim idsensore As String

#End Region
#Region "Metodos y funciones del dispositivo"
    ''' <summary>
    ''' Metodo que permite inicar el dispositivo
    ''' </summary>
    Public Sub IniciarDispositivo()
        Try
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
            Return err
        Else
        End If
        Return GrFinger.CapInitialize(AddressOf GrFinger_SensorPlugUnPlug)
    End Function

    ''' <summary>
    ''' Metodo que permite conectar el dispositivo de reconocimiento biometrico
    ''' </summary>
    ''' <param name="idSensor">la identificacion del dispositivo</param>
    ''' <param name="evt">el evento</param>
    Public Sub GrFinger_SensorPlugUnPlug(ByVal idSensor As String, ByVal evt As Integer)
        Try
            If evt = GrFinger.GR_PLUG Then
                GrFinger.CapStartCapture(idSensor, AddressOf GrFinger_FingerUpDown, AddressOf GrFinger_ImageAcquired)
            End If
            If evt = GrFinger.GR_UNPLUG Then
                GrFinger.CapStopCapture(idSensor)
            End If
            idsensore = idSensor
        Catch
        End Try
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
        RaiseEvent BloquearControles()
        PrintBiometricDisplay(False, GrFinger.GR_DEFAULT_CONTEXT)
        extrarplantilla(template)
        identificar()
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
            'INDpeImagenBiometrico.Image = System.Drawing.Bitmap.FromHbitmap(New IntPtr(handle))
            'INDpeImagenBiometrico.Update()
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
        Catch
        End Try
    End Sub

    Public Sub parar(ByVal idSensor As String)
        GrFinger.CapStopCapture(idSensor)
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
        If ret > 0 Then
            'INDpeImagenBiometrico.EditValue = Global.Presentation.Controls.My.Resources.Resources.biometricochulo
            RaiseEvent DevolverDatos()
        ElseIf ret = 0 Then
            'INDpeImagenBiometrico.EditValue = Global.Presentation.Controls.My.Resources.Resources.biometricocerrar
            RaiseEvent DesbloquearControles()
        End If
    End Sub

    ''' <summary>
    ''' en esta funcion se compara la informacion biometrica de la huella que se acaba de colocar con la huella que se cargo desde la base de datos.
    ''' </summary>
    ''' <param name="score">la puntuacion</param>
    ''' <returns></returns>
    Public Function Identify(ByRef score As Integer) As Integer
        Dim ret As Integer
        Dim i As Integer
        ' comprobamos que la plantilla de la huella sea valida
        If Not TemplateIsValid() Then Return ERR_INVALID_TEMPLATE
        ' iniciamos peraprando la huella temporal para ser identificada
        Dim tmpTpt As Array = Array.CreateInstance(GetType(Byte), template.Size)
        Array.Copy(template.tpt, tmpTpt, template.Size)
        ret = GrFinger.IdentifyPrepare(DirectCast(tmpTpt, Byte()), GrFinger.GR_DEFAULT_CONTEXT)
        If ret < 0 Then Return ret
        ' obtenemos todas las huellas registradas en la base de datos para realizar la comparacion
        Dim templates As TTemplates() = getTemplates()
        ' procesamos todas las huellas
        For i = 1 To templates.Length
            If Not (templates(i - 1).template Is Nothing) Then
                Dim tempTpt As Array = Array.CreateInstance(GetType(Byte), templates(i - 1).template.Size)
                Array.Copy(templates(i - 1).template.tpt, tempTpt, templates(i - 1).template.Size)
                ret = GrFinger.Identify(DirectCast(tmpTpt, Byte()), score, GrFinger.GR_DEFAULT_CONTEXT)
            End If
            ' comprovamos que la plantilla de consulta con la temporal coincidan
            If ret = GrFinger.GR_MATCH Then
                Contraseña = templates(i - 1).Contraseña
                Codigo = CStr(templates(i - 1).ID)
                Return templates(i - 1).ID
            End If
            If ret < 0 Then Return ret
        Next
        ' si no encuntra conicidencias
        Return GrFinger.GR_NOT_MATCH
    End Function

    ''' <summary>
    ''' Funcion que obtiene las huellas registradas en la base de datos y las mete en templetes
    ''' </summary>
    ''' <returns></returns>
    Public Function getTemplates() As TTemplates()
        modelo = New MCtrBiometricoInicio
        Dim ttpts As TTemplates()
        Dim i As Integer
        Dim huellas As List(Of Person) = modelo.ListarTodaslashuellas
        ReDim ttpts(huellas.Count)
        If huellas.Count = 0 Then Return ttpts
        For i = 1 To huellas.Count - 1
            ttpts(i).template = New TTemplate
            ttpts(i).template.tpt = huellas.Item(i).Fingerprint
            If ttpts(i).template.tpt IsNot Nothing Then
                ttpts(i).ID = CInt(huellas.Item(i).User.Item(0).UserCode.Trim)
                ttpts(i).Contraseña = huellas.Item(i).User.Item(0).Password.Trim
                ttpts(i).template.Size = ttpts(i).template.tpt.Length
            Else
                ttpts(i).template = Nothing
            End If
        Next
        Return ttpts
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

    Private _Codigo As String
    ''' <summary>
    ''' propiedad que obtiene y envia el codigo
    ''' </summary>
    ''' <value>The codigo.</value>
    Property Codigo As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Private _Contraseña As String
    ''' <summary>
    ''' Propiedad que obtiene y envia la contraseña
    ''' </summary>
    ''' <value>la contraseña.</value>
    Property Contraseña As String
        Get
            Return _Contraseña
        End Get
        Set(ByVal value As String)
            _Contraseña = value
        End Set
    End Property


    Public _ValorTemplate As TTemplate
    ''' <summary>
    ''' Propiedad que obtiene o devuelva la informacion biometrica de la huella
    ''' </summary>
    ''' <value>The valor template.</value>
    Public Property ValorTemplate As TTemplate
        Get
            Return template
        End Get
        Set(ByVal value As TTemplate)
            temporal = value
        End Set
    End Property


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


End Class