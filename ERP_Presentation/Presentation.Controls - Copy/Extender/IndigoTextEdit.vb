'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 25-04-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias"
Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido textedit
''' </summary>
<ProvideProperty("Mascara", GetType(TextEdit))> _
<ProvideProperty("CampoObligatorio", GetType(TextEdit))> _
<ProvideProperty("TamañoMinimoString", GetType(TextEdit))> _
<ProvideProperty("CampoNumerico", GetType(TextEdit))> _
<ProvideProperty("ControlNavegacion", GetType(TextEdit))> _
<ProvideProperty("ApplyStyle", GetType(TextEdit))> _
Public Class IndigoTextEdit
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Variables Globales"
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' Variable que contiene el seperador decimal
    ''' </summary>
    Dim SeparadorDecimal As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clase que contiene las propiedades que se agregan al control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' Propiedad que contiene el tipo de mascara
        ''' </summary>
        Public Mascara As EMask
        ''' <summary>
        ''' propiedad que contiene si el campo es obligatorio
        ''' </summary>
        Public CampoObligatorio As Boolean
        ''' <summary>
        ''' propiedad que contiene el tamaño minimo del texto ingresado
        ''' </summary>
        Public TamañoMinimoString As Integer
        ''' <summary>
        ''' propiedad que contiene si el campo es valido o no
        ''' </summary>
        Public ControlNavegacion As Boolean
        ''' <summary>
        ''' propiedad que contiene si el campo es numerico o no
        ''' </summary>
        Public CampoNumerico As Boolean

        ''' <summary>
        ''' Valor que indica si se aplica estilo al control
        ''' </summary>
        Public ApplyStyle As Boolean

    End Class

    ''' <summary>
    ''' Funcion para agregar las propiedades a los controles
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(Hashtable(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "Mascara"
    ''' <summary>
    ''' Funcion que devuelve el tipo de mascara
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer la mascara del control")> _
    <Category("Appearance")> _
    Public Function GetMascara(ByVal p As TextEdit) As EMask
        Return EnsurePropertiesExists(p).Mascara
    End Function

    ''' <summary>
    ''' Metodo que establece el tipo de mascara
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">The value.</param>
    Public Sub SetMascara(ByVal p As TextEdit, ByVal value As EMask)
        EnsurePropertiesExists(p).Mascara = value
        p.Invalidate()
        If value = EMask.Numerico = True Then SetCampoNumerico(p, True)
    End Sub

    ''' <summary>
    ''' Resets the mascara.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetMascara(ByVal p As TextEdit)
        SetMascara(p, CType(System.Environment.Version.ToString(), EMask))
    End Sub
#End Region

#Region "CampoObligatorio"
    ''' <summary>
    ''' Funcion para obtener si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer el control como obligatorio")> _
    <Category("Appearance")> _
    Public Function GetCampoObligatorio(ByVal p As TextEdit) As Boolean
        Return EnsurePropertiesExists(p).CampoObligatorio
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetCampoObligatorio(ByVal p As TextEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoObligatorio = value
        p.Invalidate()
        EventoValidating(p, Nothing)
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoObligatorio(ByVal p As TextEdit)
        SetCampoObligatorio(p, CBool(System.Environment.Version.ToString()))
    End Sub


#End Region

#Region "TamañoMinimoString"
    ''' <summary>
    ''' Funcion que devuelve el tamaño minimo del control
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Category("Design")> _
    <DefaultValue(False)> _
    Public Function GetTamañoMinimoString(ByVal p As TextEdit) As Integer
        Return EnsurePropertiesExists(p).TamañoMinimoString
    End Function


    ''' <summary>
    ''' Metodo para establecer el tamaño minimo del control
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">The value.</param>
    Public Sub SetTamañoMinimoString(ByVal p As TextEdit, ByVal value As Integer)
        EnsurePropertiesExists(p).TamañoMinimoString = value
    End Sub
#End Region

#Region "ControlNavegacion"
    ''' <summary>
    ''' Funcion que devuelve si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad Saber Si el Campo Es valido")> _
    <Category("Appearance")> _
    Public Function GetControlNavegacion(ByVal p As TextEdit) As Boolean
        Return EnsurePropertiesExists(p).ControlNavegacion
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Private Sub SetControlNavegacion(ByVal p As TextEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).ControlNavegacion = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo valido.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetControlNavegacion(ByVal p As TextEdit)
        SetControlNavegacion(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#Region "CampoNumerico"
    ''' <summary>
    ''' Funcion para devolver si el campo es numerico
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad saber si el campo es numerico")> _
    <Category("Appearance")> _
    Public Function GetCampoNumerico(ByVal p As TextEdit) As Boolean
        Return EnsurePropertiesExists(p).CampoNumerico
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es numerico
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Private Sub SetCampoNumerico(ByVal p As TextEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoNumerico = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo numerico.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoNumerico(ByVal p As TextEdit)
        SetCampoNumerico(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#Region "ApplyStyle"

    ''' <summary>
    ''' Obtiene un valor que indica si se aplica estilo al control
    ''' </summary>
    ''' <param name="p">Control quien extiende la propiedad</param>
    ''' <returns>Valor asignado</returns>
    <Description("Obtiene o asigna un valor que indica si se aplica estilo al control"), Category("Appearance")> _
    Public Function GetApplyStyle(ByVal p As TextEdit) As Boolean
        Return EnsurePropertiesExists(p).ApplyStyle
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si se aplica estilo al control
    ''' </summary>
    ''' <param name="p">Control quien extiende la propiedad</param>
    ''' <param name="value">Valor a asignar</param>
    Public Sub SetApplyStyle(ByVal p As TextEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).ApplyStyle = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo numerico.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetApplyStyle(ByVal p As TextEdit)
        SetApplyStyle(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#End Region

#Region "Eventos"

    Private Sub EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case sender.GetType
            Case GetType(SearchLookUpEdit)
                Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)
                If p.Text <> String.Empty Then
                    If GetCampoNumerico(p) = True Then
                        p.EditValue.ToString.Replace(".", SeparadorDecimal)
                    End If
                End If
                ValidateTextColor(p, False)
        End Select
    End Sub

    ''' <summary>
    ''' Evento Validating del control donde preguntamos por la propiedad numerico para cambiar el separador decimales a .
    ''' preguntamos por el valor minimo del control para cambiar el color del control
    ''' preguntamos si el control es obligatorio para cambiar color en caso de que sea vacio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoValidating(ByVal sender As Object, ByVal e As System.EventArgs)
        ValidatingObligatory(sender)
    End Sub

    ''' <summary>
    ''' Evento enabled changed para cambiar el color de control en el caso que corresponda
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoEnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ValidateTextColor(CType(sender, TextEdit), False)
    End Sub

    ''' <summary>
    ''' Evento gotfocus para seleccionar todo el texto del control
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As TextEdit = TryCast(sender, TextEdit)
        p.SelectAll()
    End Sub

    ''' <summary>
    ''' Evento cuando se pierde el foco donde hacemos la operacion de comprobar campo obligatorio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        ValidatingObligatory(sender)
    End Sub

    ''' <summary>
    ''' Metodo para validar campos obligatorios y cambiar la apariencia del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ValidatingObligatory(ByVal sender As Object)
        Dim edit = CType(sender, TextEdit)
        If sender.GetType() = GetType(SearchLookUpEdit) Then
            If edit.Text <> String.Empty Then
                If GetCampoNumerico(edit) = True Then
                    edit.EditValue.ToString.Replace(".", SeparadorDecimal)
                End If
            End If
            ValidateTextColor(edit, False)
        Else
            ValidateTextColor(edit, True)
        End If
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento asigno la mascara correspondiente y la apariencia del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As TextEdit = TryCast(de.Key, TextEdit)
            Select Case GetMascara(p)
                Case EMask.AlfaNumerico
                    p.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.CorreoElectronico
                    p.Properties.Mask.EditMask = "([a-zA-Z0-9_\-\.]{0,25})@([a-z]{0,15}\.)([a-z]{0,5})"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.Numerico
                    p.Properties.Mask.EditMask = "[0-9]+"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.NumericoDosDecimales
                    p.Properties.Mask.EditMask = "f"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    p.Properties.Mask.UseMaskAsDisplayFormat = True
                Case EMask.Moneda
                    p.Properties.Mask.Culture = ConfigurationFile.Instance.Culture
                    p.Properties.Mask.EditMask = "c0"
                    p.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    p.Properties.Appearance.Options.UseTextOptions = True
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    p.Properties.Mask.UseMaskAsDisplayFormat = True
                Case EMask.MonedaDecimales
                    p.Properties.Mask.Culture = ConfigurationFile.Instance.Culture
                    p.Properties.Mask.EditMask = "c"
                    p.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    p.Properties.Appearance.Options.UseTextOptions = True
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    p.Properties.Mask.UseMaskAsDisplayFormat = True
                Case EMask.SoloLetraMayuscula
                    p.Properties.Mask.EditMask = "[A-Z Ñ]+"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.SoloLetraMayusculaMinuscula
                    p.Properties.Mask.EditMask = "[a-zA-Z Ññ]+"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.SoloLetraMinuscula
                    p.Properties.Mask.EditMask = "[a-z ñ]+"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Case EMask.Telefono
                    p.Properties.Mask.EditMask = "(\d?\d?\d?)\d\d\d-\d\d\d\d"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular
                Case EMask.Porcentaje
                    p.Properties.Mask.EditMask = "P"
                    p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    p.Properties.Mask.UseMaskAsDisplayFormat = True
                Case EMask.Ninguno
            End Select
            If Me.EnsurePropertiesExists(p).ApplyStyle Then
                p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                p.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                p.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                p.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                p.Size = New Size(p.Size.Width, 30)
            End If
            AddHandler p.TextChanged, AddressOf EventoValidating
            AddHandler p.EditValueChanged, AddressOf EditValueChanged
            AddHandler p.EnabledChanged, AddressOf EventoEnabledChanged
            AddHandler p.GotFocus, AddressOf GotFocus
            AddHandler p.LostFocus, AddressOf LostFocus
        Next
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inicializo el componente y inicializo la variable hashtable <see cref="IndigoTextEdit"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoTextEdit"/> class.
    ''' </summary>
    ''' <param name="container">The container.</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        If container IsNot Nothing Then
            container.Add(Me)
        End If
    End Sub

    ''' <summary>
    ''' Initializes the component.
    ''' </summary>
    Private Sub InitializeComponent()
        Contenedor = New System.ComponentModel.Container()
    End Sub

    ''' <summary>
    ''' Libera los recursos no administrados que utiliza <see cref="T:System.ComponentModel.Component" /> y libera los recursos administrados de forma opcional.
    ''' </summary>
    ''' <param name="disposing">Es true para liberar tanto recursos administrados como no administrados; es false para liberar únicamente recursos no administrados.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Contenedor IsNot Nothing Then
                Contenedor.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' Especifica si este objeto puede proporcionar las propiedades Extender al objeto especificado.
    ''' </summary>
    ''' <param name="extendee"><see cref="T:System.Object" /> para recibir las propiedades Extender.</param>
    ''' <returns>
    ''' truesi este objeto puede proporcionar propiedades extensoras al objeto especificado; en caso contrario, false.
    ''' </returns>
    Public Function CanExtend(ByVal extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend
        If TypeOf extendee Is TextEdit OrElse TypeOf extendee Is ComboBoxEdit OrElse TypeOf extendee Is SearchLookUpEdit OrElse TypeOf extendee Is GridLookUpEdit Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Valida el color que debe tener el TextEdit
    ''' </summary>
    ''' <param name="edit"></param>
    ''' <param name="validateSize"></param>
    ''' <remarks></remarks>
    Private Sub ValidateTextColor(edit As TextEdit, validateSize As Boolean)
        If validateSize And (edit.Text.Length >= GetTamañoMinimoString(edit) AndAlso edit.EditValue IsNot Nothing AndAlso edit.EditValue.ToString <> String.Empty) Then
            edit.BackColor = Color.White
        Else
            If GetCampoObligatorio(edit) = True AndAlso edit.EditValue Is Nothing Then
                edit.BackColor = Color.MistyRose
            Else
                edit.BackColor = Color.White
            End If
        End If
        If edit.Enabled = True Then
            edit.BackColor = Color.WhiteSmoke
        End If
    End Sub

#End Region

#Region "Enumeracion"
    ''' <summary>
    ''' Enumeracion para saber el tipo de mascara que se va utilizar
    ''' </summary>
    Public Enum EMask
        ''' <summary>
        ''' Ninguna mascara usar este tipo para personalizar la mascara del control
        ''' </summary>
        Ninguno = 0
        ''' <summary>
        ''' Mascara que permite el ingreso de valores alfanumericos y simbolos
        ''' </summary>
        AlfaNumerico = 1
        ''' <summary>
        ''' mascara de solo numeros del  0-9
        ''' </summary>
        Numerico = 2
        ''' <summary>
        ''' Mascara de tipo moneda sin decimales
        ''' </summary>
        Moneda = 3
        ''' <summary>
        ''' Mascara de tipo moneda con decimales
        ''' </summary>
        MonedaDecimales = 4
        ''' <summary>
        ''' Mascara numerico con dos decimales
        ''' </summary>
        NumericoDosDecimales = 5
        ''' <summary>
        ''' Mascara de solo letras con espacios mayusculas
        ''' </summary>
        SoloLetraMayuscula = 6
        ''' <summary>
        ''' Mascara de solo letras con espacios minusculas
        ''' </summary>
        SoloLetraMinuscula = 7
        ''' <summary>
        ''' Mascara de solo letras con espacios mayusculas y minusculas
        ''' </summary>
        SoloLetraMayusculaMinuscula = 8
        ''' <summary>
        ''' Mascara  para correo electronico ---@---.--
        ''' </summary>
        CorreoElectronico = 9
        ''' <summary>
        ''' mascara de porcentaje
        ''' </summary>
        Porcentaje = 10
        ''' <summary>
        ''' Mascara de telefono (---)-------
        ''' </summary>
        Telefono = 11
    End Enum

#End Region

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub
End Class
