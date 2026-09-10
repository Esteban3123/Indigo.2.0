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
#Region "Librerias Importadas"
Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Skins

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido DateEdit
''' </summary>
<ProvideProperty("MascaraDate", GetType(DateEdit))> _
<ProvideProperty("CampoObligatorio", GetType(DateEdit))> _
Public Class IndigoDate
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize


#Region "Variable Generales"
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Clase que contiene todas las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' Propiedad que contiene el tipo de mascara
        ''' </summary>
        Public MascaraDate As EMask
        ''' <summary>
        ''' Propiedad que establece si el campo es obligatorio
        ''' </summary>
        Public CampoObligatorio As Boolean
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
    <Description("Propiedad para establecer la mascara del Date Edit")> _
    <Category("Appearance")> _
    Public Function GetMascaraDate(ByVal p As DateEdit) As EMask
        Return EnsurePropertiesExists(p).MascaraDate
    End Function

    ''' <summary>
    ''' Metodo que establece el tipo de mascara
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">The value.</param>
    Public Sub SetMascaraDate(ByVal p As DateEdit, ByVal value As EMask)
        EnsurePropertiesExists(p).MascaraDate = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the mascara.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetMascara(ByVal p As DateEdit)
        SetMascaraDate(p, EMask.Fecha)
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
    Public Function GetCampoObligatorio(ByVal p As DateEdit) As Boolean
        Return EnsurePropertiesExists(p).CampoObligatorio
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetCampoObligatorio(ByVal p As DateEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoObligatorio = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoObligatorio(ByVal p As DateEdit)
        SetCampoObligatorio(p, CBool(System.Environment.Version.ToString()))
    End Sub
#End Region
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento cuando se pierde el foco donde hacemos la operacion de comprobar campo obligatorio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        ValidatingObligatory(sender, True)
    End Sub
    ''' <summary>
    ''' Evento Validating del control donde preguntamos por la propiedad numerico para cambiar el separador decimales a .
    ''' preguntamos por el valor minimo del control para cambiar el color del control
    ''' preguntamos si el control es obligatorio para cambiar color en caso de que sea vacio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoValidating(ByVal sender As Object, ByVal e As System.EventArgs)
        ValidatingObligatory(sender, False)
    End Sub

    ''' <summary>
    ''' Metodo para validar campos obligatorios y cambiar la apariencia del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ValidatingObligatory(ByVal sender As Object, ByVal _LostFocus As Boolean)
        Dim p As DateEdit = TryCast(sender, DateEdit)
        If p.Enabled AndAlso Not p.ReadOnly Then
            If _LostFocus OrElse p.Text <> String.Empty Then
                p.Properties.Appearance.BackColor = p.Properties.AppearanceFocused.BackColor
                p.Properties.Appearance.ForeColor = p.Properties.AppearanceFocused.ForeColor
            ElseIf p.BackColor = Color.MistyRose Then
                p.Properties.Appearance.ForeColor = Color.Black                
            End If

            If p.EditValue Is Nothing OrElse p.Text = String.Empty OrElse p.EditValue = DateTime.MinValue Then
                If GetCampoObligatorio(p) Then
                    p.BackColor = Color.MistyRose
                    p.Properties.Appearance.ForeColor = Color.Black
                    p.ToolTip = "Este Campo es Necesario"
                Else
                    'p.BackColor = Color.Transparent
                    p.ToolTip = ""
                End If
            Else
                p.ToolTip = ""
            End If


        Else
            'p.BackColor = Color.WhiteSmoke
            p.ToolTip = ""
        End If
    End Sub

    ''' <summary>
    ''' Evento enabled changed para cambiar el color de control en el caso que corresponda
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoEnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As DateEdit = TryCast(sender, DateEdit)
        Dim skin = DevExpress.Skins.CommonSkins.GetSkin(p.LookAndFeel)
        Dim colr As Color
        If p.Enabled = True AndAlso Not p.ReadOnly Then
            'p.BackColor = Color.Transparent
            colr = skin.Colors.GetColor(DevExpress.Skins.CommonColors.Window)
            p.Properties.Appearance.BackColor = colr
            p.Properties.AppearanceFocused.BackColor = colr            
            p.Properties.Appearance.ForeColor = skin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText)
            p.Properties.AppearanceFocused.ForeColor = p.Properties.Appearance.ForeColor
            'End Select
            If GetCampoObligatorio(p) And p.Text = String.Empty Then
                p.Properties.Appearance.BackColor = Color.MistyRose
                If p.Properties.Appearance.ForeColor = colr Then
                    p.Properties.Appearance.ForeColor = Color.Black
                End If
                If p.Tag Is Nothing Then
                    p.ToolTip = "Este Campo es Necesario"
                Else
                    p.ToolTip = "Este Campo es Necesario: " + p.Tag.ToString()
                End If
            End If
        Else
            colr = skin.Colors.GetColor(DevExpress.Skins.CommonColors.DisabledControl)
            p.Properties.Appearance.BackColor = colr
            p.Properties.AppearanceFocused.BackColor = colr
            'p.BackColor = Color.WhiteSmoke
            p.Properties.Appearance.ForeColor = skin.Colors.GetColor(DevExpress.Skins.CommonColors.DisabledText)
            p.Properties.AppearanceFocused.ForeColor = p.Properties.Appearance.ForeColor
            p.ToolTip = ""
        End If
    End Sub

    ''' <summary>
    ''' Evento gotfocus para seleccionar todo el texto del control
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As DateEdit = TryCast(sender, DateEdit)
        p.SelectAll()
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento asigno la mascara correspondiente y la apariencia del control
    ''' y agregamos la mascara por defecto del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As DateEdit = TryCast(de.Key, DateEdit)
            p.Properties.Mask.Culture = ConfigurationFile.Instance.Culture
            p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
            p.Properties.Mask.UseMaskAsDisplayFormat = True

            Dim IndigoSession = SessionValues.Instance
            Dim MaskDate = Utils.GetCustomDateFormat(If(IndigoSession IsNot Nothing, IndigoSession.dateFormat, -1))
            Dim MaskTime = Utils.GetCustomTimeFormat(If(IndigoSession IsNot Nothing, IndigoSession.timeFormat, -1))

            Select Case GetMascaraDate(p)
                Case EMask.Fecha
                    p.Properties.Mask.EditMask = MaskDate
                Case EMask.FechaHora
                    p.Properties.Mask.EditMask = String.Format("{0} {1}", MaskDate, MaskTime.Replace(":ss", ""))
                Case EMask.FechaHoraSegundos
                    p.Properties.Mask.EditMask = String.Format("{0} {1}", MaskDate, MaskTime)
                Case EMask.FechaHorarioMilitar
                    p.Properties.Mask.EditMask = String.Format("{0} {1}", MaskDate, Utils.GetCustomTimeFormat(CustomFormatTime.HH_mm_ss))
                Case Else
                    p.Properties.Mask.EditMask = MaskDate
            End Select
            If DesignMode Then
                p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                p.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                'p.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                'p.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            End If
            p.Size = New Size(p.Size.Width, 30)
            AddHandler p.TextChanged, AddressOf EventoValidating
            AddHandler p.EnabledChanged, AddressOf EventoEnabledChanged
            AddHandler p.GotFocus, AddressOf GotFocus
            AddHandler p.LostFocus, AddressOf LostFocus
        Next
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoDate"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoDate"/> class.
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
    ''' Libera los recursos no administrados que utiliza <see cref="T:System.ComponentModel.Component"/> y libera los recursos administrados de forma opcional.
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
    ''' <param name="extendee"><see cref="T:System.Object"/> para recibir las propiedades Extender.</param>
    ''' <returns>
    ''' truesi este objeto puede proporcionar propiedades extensoras al objeto especificado; en caso contrario, false.
    ''' </returns>
    Public Function CanExtend(ByVal extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend
        Return True
    End Function
#End Region


#Region "Enumeraciones"

    ''' <summary>
    ''' Enumeracion para saber el tipo de mascara que se va utilizar
    ''' </summary>
    Public Enum EMask
        ''' <summary>
        ''' Mascara que permite el ingreso de una fecha
        ''' </summary>
        Fecha = 0
        ''' <summary>
        ''' mascara de fecha y hora
        ''' </summary>
        FechaHora = 1
        ''' <summary>
        ''' mascara de fecha hora y segundos
        ''' </summary>
        FechaHoraSegundos = 2
        ''' <summary>
        ''' mascara de fecha con horario militar (24 horas)
        ''' </summary>
        FechaHorarioMilitar = 3
    End Enum

#End Region


End Class
