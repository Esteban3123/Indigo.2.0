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

#Region "librerias Importadas"
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Controls.MVP
Imports Domain.Security.Entities
#End Region
''' <summary>
''' Clase con las funciones para el control del Biometrico
''' </summary>
Public Class Util
#Region "Variables Generales"
    Public Const ERR_CANT_OPEN_BD As Integer = -999
    Public Const ERR_INVALID_ID As Integer = -998
    Public Const ERR_INVALID_TEMPLATE As Integer = -997
    Private Declare Function GetDC Lib "user32" (ByVal hwnd As Int32) As Int32
    Private Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Int32, ByVal hdc As Int32) As Int32
    ''' <summary>
    ''' variable que se usa para la imagen de la huella
    ''' </summary>
    Public raw As RawImage
    ''' <summary>
    ''' variable que se usa para instanciar el template
    ''' </summary>
    Public template As New TTemplate
    ''' <summary>
    ''' variable que se usa para instanciar el template
    ''' </summary>
    Public plantilla1 As New TTemplate
    ''' <summary>
    ''' variable que se usa para instanciar el template
    ''' </summary>
    Public plantilla2 As New TTemplate
    ''' <summary>
    ''' variable que se usa para instanciar el template
    ''' </summary>
    Public plantilla3 As New TTemplate
    ''' <summary>
    ''' variable que se usa para instanciar listbox
    ''' </summary>
    Private _lbLog As ListBox
    ''' <summary>
    ''' variable que se usa para instanciar el picturebox
    ''' </summary>
    Private _pbPic As PictureBox
    ''' <summary>
    ''' variable que se usa para instanciar el la calse axgrfingerxlib
    ''' </summary>
    Private _GrFingerX As Object
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
#End Region
#Region "Biometrico Login"
#Region "Metodos"
    ''' <summary>
    ''' metodo para extrar la plantilla de la informacion biometrica
    ''' </summary>
    ''' <returns></returns>
    Function ExtractTemplate() As Integer
        Dim ret As Integer
        'establece el tamaño de la plantilla
        template.Size = template.tpt.Length
        'funcion para extraer la plantilla
        ret = _GrFingerX.Extract(DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), CInt(raw.res), DirectCast(template.tpt, Byte()), CInt(template.Size))
        ' si el resultado es menor que 0 entonces hubo un error en la extraccion de la plantilla
        If ret < 0 Then template.Size = 0
        Return ret
    End Function

    ''' <summary>
    ''' Metodo para instancais la el listobx el picture edit y la libreria
    ''' </summary>
    Public Sub New(ByRef lbLog As ListBox, ByRef pbPic As PictureBox, ByRef GrFingerX As Object)
        _lbLog = lbLog
        _pbPic = pbPic
        _GrFingerX = GrFingerX
    End Sub
#End Region

#Region "Funciones"
    ''' <summary>
    ''' Funcion para iniciar el dispositivo Biometrico
    ''' </summary>
    Public Function InitializeGrFinger() As Integer
        Dim err As Integer
        template.Size = 0
        raw.img = Nothing
        raw.width = 0
        raw.height = 0
        err = _GrFingerX.Initialize()
        If err < 0 Then Return err
        Return _GrFingerX.CapInitialize()
    End Function

    ''' <summary>
    ''' Funcion Para finalizar el dispositivo Biometrico
    ''' </summary>
    Public Sub FinalizeGrFinger()
        ' _GrFingerX.Finalize()
        _GrFingerX.CapFinalize()
    End Sub
#End Region

#Region "Metodos Y Funciones que permiten la identificacion de la huella"
    ''' <summary>
    ''' Este metodo sirve para identificar una huella
    ''' </summary>
    Public Function identificar() As Integer
        Dim ret As Integer, score As Integer
        score = 0
        ret = Identify(score)
        Return ret
    End Function

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
#End Region
#End Region
#Region "Biometrico Guardar"

    ''' <summary>
    ''' Metodo para poner la imagen obtenida de la huella o la informacion biometrica sobre el control PictureEdit
    ''' </summary>
    Public Sub PrintBiometricDisplay(ByVal biometricDisplay As Boolean, ByVal context As Integer)
        ' Manejador de la imagen de la huella
        Dim handle As System.Drawing.Image = Nothing
        ' pantalla del dispositivo
        Dim hdc As Integer = GetDC(0)
        If biometricDisplay Then
            ' obtiene la informacion biometrica de la huella
            _GrFingerX.BiometricDisplay(template.tpt, DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), CInt(raw.res), hdc, handle, context)
        Else
            'obtiene la imagen de la huella
            _GrFingerX.CapRawImageToHandle(DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), hdc, handle)
        End If
        If Not (handle Is Nothing) Then
            ' pinta el control PictureEdit con la imagen de la huella o con la informacion biometrica
            _pbPic.Image = handle
            _pbPic.Update()
        End If
        ' Libera la pantalla del dispositivo
        ReleaseDC(0, hdc)
    End Sub

#Region "Metodos y funciones para extrar la plantilla de la huella"
    ''' <summary>
    ''' metodo para extrar la plantilla de la informacion biometrica
    ''' </summary>
    Function ExtraerPlantillas(ByVal plantilla As TTemplate) As TTemplate
        template = plantilla
        Dim ret As Integer
        'establece el tamaño de la plantilla
        template.Size = template.tpt.Length
        'funcion para extraer la plantilla
        ret = GrFinger.Extract(DirectCast(raw.img, Byte()), CInt(raw.width), CInt(raw.height), CInt(raw.res), DirectCast(template.tpt, Byte()), CInt(template.Size), GrFinger.GR_DEFAULT_CONTEXT)
        ' si el resultado es menor que 0 entonces hubo un error en la extraccion de la plantilla
        If ret < 0 Then template.Size = 0
        If ret >= 0 Then
            'crea la imagen obtenida en el control PictureEdit
            PrintBiometricDisplay(True, GrFinger.GR_NO_CONTEXT)
        End If
        Return template
    End Function
#End Region

#Region "Funciones para comprovar las tres huellas ingresadas"
    ''' <summary>
    ''' Funciones que utilizamos para verificar que las tres huellas ingresadas sean correctas almenos dos
    ''' </summary>
    ''' <returns></returns>
    Public Function comprovar(ByVal plantilla1 As TTemplate, ByVal plantilla2 As TTemplate, ByVal plantilla3 As TTemplate) As Integer
        If plantilla1 Is Nothing Or plantilla2 Is Nothing Or plantilla3 Is Nothing Then
            Return CInt(False)
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
            If comprovar2() = 2 Then
                Return 2
            Else
                Return 0
            End If
        Else
            Return 1
            limpiarplantillas()
        End If
    End Function
    Public Function comprovar2() As Integer
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
            If CInt(comprovar3()) = 3 Then
                Return CInt(True)
            Else
                Return 0
            End If
        Else
            Return 2
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
            Return CBool(0)
        Else
            Return CBool(3)
            limpiarplantillas()
        End If
    End Function

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
#End Region

    Public Sub WriteLog(ByVal message As String)
        _lbLog.Items.Add(message)
        _lbLog.SelectedIndex = _lbLog.Items.Count - 1
        _lbLog.ClearSelected()
    End Sub



End Class
