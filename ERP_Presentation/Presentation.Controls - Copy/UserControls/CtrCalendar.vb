
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools
#End Region

''' <summary>
''' Control que contiene el calendario customizado. Para agregar el CtrDateNavigator, hacerlo de esta manera "DateNavigator".CtrCalendar = Calendar
''' </summary>
Public Class CtrCalendar

#Region "Public Events"
    ''' <summary>
    ''' Evento que controla el doble click de los dias 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnDoubleClickDays As EventHandler

    ''' <summary>
    ''' Evento que controla el click de los dias 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnClickDays As EventHandler

    ''' <summary>
    ''' Evento publico para el cambio de fecha
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnChangeDate As EventHandler

    ''' <summary>
    ''' Evento publico para el hover sobre el icono del dia
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnMouseHoverDays As EventHandler

    ''' <summary>
    ''' Evento publico para el hover sobre los turnos de mas que tenga el empleado
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnMouseHoverMore As EventHandler

    ''' <summary>
    ''' Evento puclibo para el leave sobre el icono del dia
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnMouseLeaveDays As EventHandler

#End Region

#Region "Fields"
    ''' <summary>
    ''' Esta variable contiene el año del calendario
    ''' </summary>
    Dim _year As Integer = DateTime.Now.Year

    ''' <summary>
    ''' Esta variable contiene el mes del calendario
    ''' </summary>
    Dim _month As Integer = DateTime.Now.Month

    ''' <summary>
    ''' Esta variable contiene la fecha del calendario tipo (yyyy/mm/1)
    ''' </summary>
    Dim _date As Date '= New Date(_year, _month, 1)

    ''' <summary>
    ''' Variable que contiene el dia de la semana con que empieza el mes 
    ''' </summary>
    Dim _weekDay As Integer '= _date.DayOfWeek

    ''' <summary>
    ''' Variable que contiene el numero de dias que tiene el mes
    ''' </summary>
    Dim _daysOnMonth As Integer '= Date.DaysInMonth(_year, _month)

    ''' <summary>
    ''' Variable que contiene el punto de partida del primer panel control en X
    ''' </summary>
    Dim _locX As Integer

    ''' <summary>
    ''' Variable que contiene el punto de partida del primer panel control en Y
    ''' </summary>
    Dim _locY As Integer

    ''' <summary>
    ''' Variable que contiene el incremento de cada panel control en X
    ''' </summary>
    Dim _incX As Integer

    ''' <summary>
    ''' Variable que contiene el incremento de cada panel control en Y
    ''' </summary>
    Dim _incY As Integer

    ''' <summary>
    ''' Variable que contiene el tamaño de los panel control
    ''' </summary>
    Dim _sizeChilds As Drawing.Size

    ''' <summary>
    ''' Variable que contiene los valores de sesion
    ''' </summary>
    ''' <remarks></remarks>
    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que contiene la CultureInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ci As System.Globalization.CultureInfo = Indigo.Culture

    ''' <summary>
    ''' Variable que contiene el DateTimeFormatInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtfi As System.Globalization.DateTimeFormatInfo = Nothing

    ''' <summary>
    ''' Variable que contiene el dia seleccionado con click
    ''' </summary>
    Dim _daySelected As Integer

    ''' <summary>
    ''' Contiene el color del primer registro que se compara, para diferenciar los turnos
    ''' </summary>
    Dim _RegColor1 As System.Drawing.Color

    ''' <summary>
    ''' Contiene el color del segundo registro que se compara, para diferenciar los turnos
    ''' </summary>
    Dim _RegColor2 As System.Drawing.Color

    ''' <summary>
    ''' Objeto que contiene el  listado que contiene los dias festivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listHoliday As List(Of Domain.Common.Entities.Holiday)

    ''' <summary>
    ''' Variable que contiene la posicion del picture edit 1 solo
    ''' </summary>
    Private _Pic_PositionSingle As New Point(40, 2)

    ''' <summary>
    ''' Variable que contiene la posicion del picture edit 1 en pareja
    ''' </summary>
    Private _Pic_PositionCouple1 As New Point(10, 2)

    ''' <summary>
    ''' Variable que contiene la posicion del picture edit 2 en pareja
    ''' </summary>
    Private _Pic_PositionCouple2 As New Point(70, 2)

    ''' <summary>
    ''' Variable que contiene la posicion del label de letra 1 solo
    ''' </summary>
    Private _Lab_PositionSingle As New Point(40, 42)

    ''' <summary>
    ''' Variable que contiene la posicion del del label de letra 1 en pareja
    ''' </summary>
    Private _Lab_PositionCouple1 As New Point(15, 42)

    ''' <summary>
    ''' Variable que contiene la posicion del label de letra 2 en pareja
    ''' </summary>
    Private _Lab_PositionCouple2 As New Point(75, 42)

    ''' <summary>
    ''' Objeto que contiene el control pop up a mostrar de detalle de dia
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopUpControlToShowDetail As DevExpress.XtraBars.PopupControlContainer

    ''' <summary>
    ''' Objeto que contiene el control pop up a mostrar de mas schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopUpControlToShowMore As DevExpress.XtraBars.PopupControlContainer
#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene la posicion del label de letra 1 solo
    ''' </summary>
    ReadOnly Property PicPositionSingle As Point
        Get
            Return _Pic_PositionSingle
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la posicion del label de letra 1 en pareja
    ''' </summary>
    ReadOnly Property PicPositionCouple1 As Point
        Get
            Return _Pic_PositionCouple1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la posicion del label de letra 2 en pareja
    ''' </summary>
    ReadOnly Property PicPositionCouple2 As Point
        Get
            Return _Pic_PositionCouple2
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la posicion del picture edit 1 solo
    ''' </summary>
    ReadOnly Property LabPositionSingle As Point
        Get
            Return _Lab_PositionSingle
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la posicion del picture edit 1 en pareja
    ''' </summary>
    ReadOnly Property LabPositionCouple1 As Point
        Get
            Return _Lab_PositionCouple1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la posicion del picture edit 2 en pareja
    ''' </summary>
    ReadOnly Property LabPositionCouple2 As Point
        Get
            Return _Lab_PositionCouple2
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el año que muestra el control
    ''' </summary>
    Public Property YearControl As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            If value <> _year Then
                _year = value
                Generate_Calendar()
                'System.Threading.Thread.Sleep(1000)
                RaiseEvent OnChangeDate(Nothing, Nothing)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el mes que muestra el control
    ''' </summary>
    Public Property MonthControl As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            If value <> _month Then
                If value > 12 Then
                    _year += 1
                    _month = 1
                ElseIf value < 1 Then
                    _year -= 1
                    _month = 12
                Else
                    _month = value
                End If
                Generate_Calendar()
                'System.Threading.Thread.Sleep(1000)
                RaiseEvent OnChangeDate(Nothing, Nothing)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la fecha del control
    ''' </summary>
    Public ReadOnly Property ScheduleDate As Date
        Get
            Return New Date(YearControl, MonthControl, 1)
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que devuelve el numero de dias que tiene un determinado mes
    ''' </summary>
    Public ReadOnly Property DaysInThisMonth As Integer
        Get
            Return Date.DaysInMonth(YearControl, MonthControl)
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que devuelve el panel control principal para acceder a los otros
    ''' </summary>
    Public ReadOnly Property PanelControlMain As DevExpress.XtraEditors.PanelControl
        Get
            Return INDPcMain
        End Get
    End Property

    ''' <summary>
    ''' Contiene la ubicacion en x de el primer panel control child
    ''' </summary>
    Public Property LocationX As Integer
        Get
            Return _locX
        End Get
        Set(value As Integer)
            _locX = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene la ubicacion en y de el primer panel control child
    ''' </summary>
    Public Property LocationY As Integer
        Get
            Return _locY
        End Get
        Set(value As Integer)
            _locY = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el incremento en x de los panel control child
    ''' </summary>
    Public Property IncrementX As Integer
        Get
            Return _incX
        End Get
        Set(value As Integer)
            _incX = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el incremento en y de los panel control child
    ''' </summary>
    Public Property IncrementY As Integer
        Get
            Return _incY
        End Get
        Set(value As Integer)
            _incY = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el tamaño de los panel controls childs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SizeChilds As System.Drawing.Size
        Get
            Return _sizeChilds
        End Get
        Set(value As System.Drawing.Size)
            _sizeChilds = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero de el dia que se tiene clickeado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DaySelected As Integer
        Get
            Return _daySelected
        End Get
        Set(value As Integer)
            _daySelected = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los festivos y dominicales
    ''' </summary>
    Public Property ListHoliday As List(Of Domain.Common.Entities.Holiday)
        Get
            Return _listHoliday
        End Get
        Set(value As List(Of Domain.Common.Entities.Holiday))
            _listHoliday = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el color del primer registro que se compara, para diferenciar los turnos
    ''' </summary>
    Public Property RegColor1 As System.Drawing.Color
        Get
            If _RegColor1 = Nothing Then
                _RegColor1 = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(56, Byte), Integer))
            End If
            Return _RegColor1
        End Get
        Set(value As System.Drawing.Color)
            _RegColor1 = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el color del segundo registro que se compara, para diferenciar los turnos
    ''' </summary>
    Public Property RegColor2 As System.Drawing.Color
        Get
            If _RegColor2 = Nothing Then
                _RegColor2 = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(56, Byte), Integer))
            End If
            Return _RegColor2
        End Get
        Set(value As System.Drawing.Color)
            _RegColor2 = value
        End Set
    End Property

    ''' <summary>
    ''' Establece o Retorna el PopUp Control de detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PopUpControlToShowDetail As DevExpress.XtraBars.PopupControlContainer
        Get
            Return _PopUpControlToShowDetail
        End Get
        Set(value As DevExpress.XtraBars.PopupControlContainer)
            _PopUpControlToShowDetail = value
            AddHandler _PopUpControlToShowDetail.CloseUp, AddressOf HighLightPopUpDayClose
        End Set
    End Property

    ''' <summary>
    ''' Establece o Retorna el PopUp Control de turno en otra unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PopUpControlToShowMore As DevExpress.XtraBars.PopupControlContainer
        Get
            Return _PopUpControlToShowMore
        End Get
        Set(value As DevExpress.XtraBars.PopupControlContainer)
            _PopUpControlToShowMore = value
            AddHandler _PopUpControlToShowMore.CloseUp, AddressOf HighLightPopUpDayClose
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene el color del dia seleccionado para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _ModeDeleteColor As System.Drawing.Color
    ''' <summary>
    ''' contiene el color del dia seleccionado para eliminar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModeDeleteColor As System.Drawing.Color
        Get
            Return _ModeDeleteColor
        End Get
        Set(value As System.Drawing.Color)
            _ModeDeleteColor = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene si se va a eliminar o no
    ''' </summary>
    ''' <remarks></remarks> 
    Private _ModeDelete As Boolean
    ''' <summary>
    ''' contiene si se va a eliminar o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModeDelete As Boolean
        Get
            Return _ModeDelete
        End Get
        Set(value As Boolean)
            If value = False Then
                HighLightPopUpDayClose(Nothing, EventArgs.Empty)
            End If
            _ModeDelete = value
            ListDaysToDelete = New List(Of Integer)
        End Set
    End Property

    Private _listDaysToDelete As List(Of Integer)
    Public Property ListDaysToDelete As List(Of Integer)
        Get
            Return _listDaysToDelete
        End Get
        Set(value As List(Of Integer))
            _listDaysToDelete = value
        End Set
    End Property
#End Region

#Region "Builder"
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        MonthControl = DateTime.Now().Month
        YearControl = DateTime.Now().Year
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub XtraForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If
        Inizialite_Parameters()
        Create_Box()
        Generate_Calendar()
    End Sub

    ''' <summary>
    ''' Evento cuando el mouse se coloca encima de un dia
    ''' </summary>
    Private Sub PanelControlDay_MouseMove(sender As Object, e As EventArgs)
        Dim _LabelControl As DevExpress.XtraEditors.LabelControl = sender
        _LabelControl.Appearance.Font = New Drawing.Font(_LabelControl.Appearance.Font, FontStyle.Bold)
        Me.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Evento cuando el mouse sale del dia
    ''' </summary>
    Private Sub PanelControlDay_MouseLeave(sender As Object, e As EventArgs)
        Dim _LabelControl As DevExpress.XtraEditors.LabelControl = sender
        If DaySelected <> _LabelControl.Tag Then
            _LabelControl.Appearance.Font = New Drawing.Font(_LabelControl.Appearance.Font, FontStyle.Regular)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Evento para dibujar en los dias seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DrawSelectedDay(sender As Object, e As PaintEventArgs)
        Dim _label As DevExpress.XtraEditors.LabelControl = sender
        Dim _day As Integer = _label.Tag
        Dim _daysInMonth As Integer = DateTime.DaysInMonth(YearControl, MonthControl)
        If _day <= _daysInMonth Then
            Dim _date As Date = New Date(YearControl, MonthControl, _day)
            If ListHoliday IsNot Nothing Then
                If ListHoliday.Count > 0 Then
                    If ListHoliday.Find(Function(x) x.Holiday1 = _date) IsNot Nothing Then
                        _label.ForeColor = Color.Red
                    ElseIf _date.DayOfWeek = DayOfWeek.Sunday Then
                        _label.ForeColor = Color.OrangeRed
                    Else
                        _label.ForeColor = Color.DimGray
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para pintar el borde de los iconos y pde
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DrawRectangle(sender As Object, e As PaintEventArgs)
        Dim pen1 As Pen = New Pen(RegColor1, 3)
        Dim pen2 As Pen = New Pen(RegColor2, 3)
        Dim PictureEdit As DevExpress.XtraEditors.PictureEdit = sender
        If PictureEdit.Tag = "+" Or PictureEdit.Tag = "1+" Or PictureEdit.Tag = "2+" Then
            e.Graphics.DrawString("+", New Font("Segoe UI", 15.76!, FontStyle.Bold), Brushes.DimGray, PictureEdit.Width - 10, 0)
        End If
        If PictureEdit.Tag = "1" Or PictureEdit.Tag = "1+" Then
            'Dim rect As New Rectangle(0, -1, 1.4, Me.ClientSize.Height) ' linea
            Dim rect As New Rectangle(3, 0, 3, 3)
            'e.Graphics.DrawString("+", New Font("Segoe UI Light", 12.0!, FontStyle.Bold), Brushes.DimGray, PictureEdit.Width - 10, 0)
            e.Graphics.DrawRectangle(pen1, rect)
        End If
        If PictureEdit.Tag = "2" Or PictureEdit.Tag = "2+" Then
            Dim rect As New Rectangle(3, 0, 3, 3)
            'e.Graphics.DrawString("+", New Font("Segoe UI Light", 8.75!, FontStyle.Bold), Brushes.DimGray, PictureEdit.Width - 10, 0)
            e.Graphics.DrawRectangle(pen2, rect)
        End If
        If PictureEdit.Tag Is String.Empty Then
            Dim rect As New Rectangle(0, 0, 0, 0)
            e.Graphics.DrawRectangle(Pens.White, rect)
        End If
    End Sub

    ''' <summary>
    ''' Evento para quitar el color resaltado en el show pop up
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub HighLightPopUpDayClose(s As Object, e As EventArgs)
        Dim _PanelControlMain As DevExpress.XtraEditors.PanelControl = PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = _PanelControlMain.Controls
        For i As Integer = 0 To PanelControlChilds.Count - 1
            If PanelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = PanelControlChilds.Item(i)
                If item.Tag <> "" Then
                    Dim LabelControlItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcDays" & item.Tag) 'item.Tag contiene el Numero del dia
                    Dim LcItemMore As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcMore" & item.Tag)
                    Dim PictureEditItem1 As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeFirst" & item.Tag)
                    Dim PictureEditItem2 As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeSecond" & item.Tag)
                    Dim LcItem1 As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterFirst" & item.Tag)
                    Dim LcItem2 As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterSecond" & item.Tag)
                    item.BackColor = Color.White
                    LabelControlItem.BackColor = Color.WhiteSmoke
                    LcItemMore.BackColor = LabelControlItem.BackColor
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Evento para Resaltar el show pop up de los dias
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub HighLightPopUpDay(s As Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs)
        Dim DDB As DevExpress.XtraEditors.DropDownButton = s
        Dim _parent As DevExpress.XtraEditors.PanelControl = DDB.Parent
        Dim LcDays As DevExpress.XtraEditors.LabelControl = _parent.Controls.Item("INDLcDays" & DDB.Tag)
        Dim LcMore As DevExpress.XtraEditors.LabelControl = _parent.Controls.Item("INDLcMore" & DDB.Tag)
        LcDays.BackColor = Color.Gainsboro
        LcMore.BackColor = LcDays.BackColor
        _parent.BackColor = Color.WhiteSmoke
    End Sub

    ''' <summary>
    ''' Evento para pintar los dias a borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ClikDaysToDelete(sender As Object, e As EventArgs)
        If ModeDelete = True Then
            Dim LcDays As DevExpress.XtraEditors.LabelControl = sender
            Dim _parent As DevExpress.XtraEditors.PanelControl = LcDays.Parent
            Dim _pic1 As DevExpress.XtraEditors.PictureEdit = _parent.Controls.Item("INDPeFirst" & LcDays.Tag)
            Dim _pic2 As DevExpress.XtraEditors.PictureEdit = _parent.Controls.Item("INDPeSecond" & LcDays.Tag)
            If _parent.Tag <> "" Then
                If _pic1.Visible = True Or _pic2.Visible = True Then
                    If _parent.BackColor = Color.White Then 'marcado
                        _parent.BackColor = ModeDeleteColor
                        ListDaysToDelete.Add(_parent.Tag)
                    ElseIf _parent.BackColor = ModeDeleteColor Then 'desmarcado
                        _parent.BackColor = Color.White
                        ListDaysToDelete.Remove(_parent.Tag)
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que inicializa los parametros de tamaño y posicion del calendario
    ''' </summary>
    Private Sub Inizialite_Parameters()
        If _incX = 0 Then
            _locX = 5
        End If
        If _locY = 0 Then
            _locY = 50
        End If
        If _incX = 0 Then
            _incX = 80
        End If
        If _incY = 0 Then
            _incY = 46
        End If
        If _sizeChilds = Nothing Then
            _sizeChilds = New Drawing.Size(70, 40)
        End If

    End Sub

    ''' <summary>
    ''' Metodo para asignar valores a las variables globales 
    ''' </summary>
    Private Sub AssignValues()
        _date = New Date(_year, _month, 1)
        _weekDay = (_date.DayOfWeek + 1) 'sumo 1 porq el array del framework comienza en 0
        _daysOnMonth = Date.DaysInMonth(_year, _month)
    End Sub

    ''' <summary>
    ''' Metodo para crear los panel control que contendran los dias del mes
    ''' </summary>
    Private Sub Create_Box()
        Dim _num As Integer = 1 'contador de los panel control en total
        'posiciones y tamaños iniciales
        Dim accumX As Integer
        Dim accumY As Integer
        For i As Integer = 1 To 6 'for para las columnas
            If i = 1 Then
                accumY = _locY
            Else
                accumY += _incY
            End If
            For j As Integer = 1 To 7 'for para las filas
                If j = 1 Then
                    accumX = _locX
                Else
                    accumX += _incX
                End If
                'crear los 7 label de la cabecera
                If i = 1 Then
                    Dim fontColor As Drawing.Color = Color.DimGray
                    If j = 1 Then
                        fontColor = Color.OrangeRed
                    End If
                    Dim newLcHeader As New DevExpress.XtraEditors.LabelControl
                    With newLcHeader
                        .Appearance.ForeColor = fontColor
                        .Appearance.Font = New Drawing.Font("Segoe UI", 13.0!)
                        .Text = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetDayName((j - 1)), Microsoft.VisualBasic.VbStrConv.ProperCase) 'resto 1 porq el array del framework comienza en 0
                        .Location = New System.Drawing.Point(accumX + 5, (accumY - 25))
                    End With
                    INDPcMain.Controls.Add(newLcHeader)
                End If
                'Crear panel controls dentro del panel control main
                Dim newPc As New DevExpress.XtraEditors.PanelControl
                With newPc
                    .Location = New System.Drawing.Point(accumX, accumY)
                    .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                    .BackColor = Color.White
                    .Appearance.BorderColor = Color.Gainsboro
                    .LookAndFeel.UseDefaultLookAndFeel = False
                    .LookAndFeel.UseWindowsXPTheme = True
                    .Name = "Box" & _num
                    .Size = _sizeChilds
                    .TabIndex = _num
                    .Enabled = True
                End With
                _num += 1
                INDPcMain.Controls.Add(newPc)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Metodo que limpiara los panel controls que contienen los dias del mes
    ''' </summary>
    Private Sub Reset_Calendar()
        For i As Integer = 0 To INDPcMain.Controls.Count - 1
            If INDPcMain.Controls.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                'INDPcMain.Controls.Item(i).Enabled = False
                INDPcMain.Controls.Item(i).Tag = ""
                INDPcMain.Controls.Item(i).Controls.Clear()
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo para crear los dias del calendario
    ''' </summary>
    Private Sub Create_Calendar()
        For i As Integer = 0 To INDPcMain.Controls.Count - 1
            If INDPcMain.Controls.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = INDPcMain.Controls.Item(i)
                Dim numBox = item.Name.Replace("Box", "")
                Dim DayEquals As String
                DayEquals = ((numBox + 1) - _weekDay) - 1
                Dim numDay As String
                If DayEquals = 0 Then
                    Dim _date As Date = New Date(YearControl, MonthControl, 1)
                    numDay = _date.Day
                Else
                    Dim _date As Date = New Date(YearControl, MonthControl, 1).AddDays(DayEquals)
                    numDay = _date.Day
                End If

                If Not (numBox >= _weekDay And numBox < (_daysOnMonth + _weekDay)) Then
                    'creacion label control para los dias por fuera del mes
                    Dim newLabelControlOther As New DevExpress.XtraEditors.LabelControl
                    Dim fontColor As Drawing.Color = Color.LightGray

                    With newLabelControlOther
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Appearance.BackColor = Color.WhiteSmoke
                        .Size = New Drawing.Size(118, 23)
                        .Name = "INDLcDays" & item.Tag
                        .Tag = item.Tag
                        .Appearance.ForeColor = fontColor
                        .Appearance.Font = New Drawing.Font("Segoe UI", 16.0!)
                        .Text = numDay
                        .Location = New Point(1, 56)
                        .Cursor = Cursors.Hand
                    End With
                    item.Controls.Add(newLabelControlOther)
                    AddHandler newLabelControlOther.Click, Sub(s, a)
                                                               If DayEquals > 0 Then
                                                                   MonthControl += 1
                                                               ElseIf DayEquals < 0 Then
                                                                   MonthControl -= 1
                                                               End If
                                                           End Sub

                Else

                    'item.Enabled = True
                    'creacion label control para el numero de dias
                    Dim newLabelControlDays As New DevExpress.XtraEditors.LabelControl
                    Dim fontColor As Drawing.Color = Color.Gray
                    If numBox = 1 Or numBox = 8 Or numBox = 15 Or numBox = 22 Or numBox = 29 Or numBox = 36 Or numBox = 42 Then
                        fontColor = Color.OrangeRed
                    End If
                    If CStr(numDay).Length = 1 Then 'if para agregar espacio antes del numero si este es de solo un digito
                        numDay = "  " & numDay
                    End If
                    item.Tag = numDay.Trim()
                    With newLabelControlDays
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Appearance.BackColor = Color.WhiteSmoke
                        .Size = New Drawing.Size(94, 23)
                        .Name = "INDLcDays" & item.Tag
                        .Tag = item.Tag
                        .Appearance.ForeColor = fontColor
                        .Appearance.Font = New Drawing.Font("Segoe UI", 16.0!)
                        .Text = numDay
                        .Location = New Point(1, 56)
                    End With
                    item.Controls.Add(newLabelControlDays)
                    AddHandler newLabelControlDays.Paint, AddressOf DrawSelectedDay
                    AddHandler newLabelControlDays.DoubleClick, Sub(s, a)
                                                                    RaiseEvent OnDoubleClickDays(newLabelControlDays, EventArgs.Empty)
                                                                End Sub
                    AddHandler newLabelControlDays.Click, Sub(s, a)
                                                              ClikDaysToDelete(s, a)
                                                              RaiseEvent OnClickDays(newLabelControlDays, EventArgs.Empty)
                                                          End Sub
                    Dim newLabelMoreSchedule As New DevExpress.XtraEditors.LabelControl
                    With newLabelMoreSchedule
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Appearance.BackColor = Color.WhiteSmoke
                        .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        .Size = New Drawing.Size(25, 23)
                        .Name = "INDLcMore" & item.Tag
                        .Appearance.ForeColor = Color.Gray
                        .Appearance.Font = New Drawing.Font("Segoe UI Light", 12.0!)
                        .Text = ""
                        .Tag = item.Tag
                        .Location = New Point(94, 56)
                    End With
                    item.Controls.Add(newLabelMoreSchedule)
                   

                    'Drop down button en la misma posicion del label del dia para desplegar el pop-up
                    Dim newDropDownButton As New DevExpress.XtraEditors.DropDownButton
                    With newDropDownButton
                        .Location = New System.Drawing.Point(-1, -1)
                        .Name = "DDB" & item.Tag
                        .Size = New System.Drawing.Size(1, 80) '23
                        .TabIndex = 7
                        .Tag = item.Tag
                    End With
                    AddHandler newDropDownButton.ShowDropDownControl, AddressOf HighLightPopUpDay
                    item.Controls.Add(newDropDownButton)

                    'creacion picture edit para el icono del turno para empleado 1
                    Dim newPictureEdit1 As New DevExpress.XtraEditors.PictureEdit
                    With newPictureEdit1
                        .BackColor = Color.Transparent
                        .Name = "INDPeFirst" & item.Tag
                        .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                        .Size = New Size(40, 40)
                        .Properties.NullText = " "
                        .Properties.PictureAlignment = ContentAlignment.BottomCenter
                        .Visible = False
                        .Cursor = Cursors.Hand
                        .Properties.ContextMenu = New ContextMenu()
                        .Properties.AllowFocused = False
                    End With


                    AddHandler newPictureEdit1.Paint, AddressOf DrawRectangle
                    AddHandler newPictureEdit1.MouseHover, Sub(s, e)
                                                               newDropDownButton.DropDownControl = PopUpControlToShowDetail
                                                               RaiseEvent OnMouseHoverDays(s, e)
                                                           End Sub
                    AddHandler newLabelMoreSchedule.MouseHover, Sub(s, e)
                                                                    newDropDownButton.DropDownControl = PopUpControlToShowMore
                                                                    RaiseEvent OnMouseHoverMore(s, e)
                                                                End Sub
                    AddHandler newPictureEdit1.MouseLeave, Sub(s, e)
                                                               RaiseEvent OnMouseLeaveDays(s, e)
                                                           End Sub
                    item.Controls.Add(newPictureEdit1)

                    'Letra del turno debajo del icono
                    Dim newLcLetter1 As New DevExpress.XtraEditors.LabelControl
                    With newLcLetter1
                        .BackColor = Color.Transparent
                        .Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!)
                        .Appearance.ForeColor = System.Drawing.Color.DimGray
                        .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Name = "INDLcLetterFirst" & item.Tag
                        .Size = New System.Drawing.Size(35, 12)
                        .Visible = False
                        .Cursor = Cursors.Hand
                    End With
                    AddHandler newLcLetter1.MouseHover, Sub(s, e)
                                                            newDropDownButton.DropDownControl = PopUpControlToShowDetail
                                                            RaiseEvent OnMouseHoverDays(s, e)
                                                        End Sub
                    AddHandler newLcLetter1.MouseLeave, Sub(s, e)
                                                            RaiseEvent OnMouseLeaveDays(s, e)
                                                        End Sub
                    item.Controls.Add(newLcLetter1)

                    'creacion picture edit para el icono del turno para empleado 2
                    Dim newPictureEdit2 As New DevExpress.XtraEditors.PictureEdit
                    With newPictureEdit2
                        .BackColor = Color.Transparent
                        .Name = "INDPeSecond" & item.Tag
                        .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                        .Size = New Size(40, 40)
                        .Properties.PictureAlignment = ContentAlignment.BottomCenter
                        .Properties.NullText = " "
                        .Visible = False
                        .Cursor = Cursors.Hand
                        .Properties.ContextMenu = New ContextMenu()
                        .Properties.AllowFocused = False
                    End With
                    item.Controls.Add(newPictureEdit2)
                    AddHandler newPictureEdit2.Paint, AddressOf DrawRectangle
                    AddHandler newPictureEdit2.MouseHover, Sub(s, e)
                                                               newDropDownButton.DropDownControl = PopUpControlToShowDetail
                                                               RaiseEvent OnMouseHoverDays(s, e)
                                                           End Sub
                    AddHandler newPictureEdit2.MouseLeave, Sub(s, e)
                                                               RaiseEvent OnMouseLeaveDays(s, e)
                                                           End Sub

                    'Label para informacion del turno debajo del icono
                    Dim newLcLetter2 As New DevExpress.XtraEditors.LabelControl
                    With newLcLetter2
                        .BackColor = Color.Transparent
                        .Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!)
                        .Appearance.ForeColor = System.Drawing.Color.DimGray
                        .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Name = "INDLcLetterSecond" & item.Tag
                        .Size = New System.Drawing.Size(35, 12)
                        .Cursor = Cursors.Hand
                        .Visible = False
                    End With
                    AddHandler newLcLetter2.MouseHover, Sub(s, e)
                                                            newDropDownButton.DropDownControl = PopUpControlToShowDetail
                                                            RaiseEvent OnMouseHoverDays(s, e)
                                                        End Sub
                    AddHandler newLcLetter2.MouseLeave, Sub(s, e)
                                                            RaiseEvent OnMouseLeaveDays(s, e)
                                                        End Sub
                    item.Controls.Add(newLcLetter2)

                    'Se agregan manejadores a los controles
                    AddHandler newLabelControlDays.MouseMove, AddressOf PanelControlDay_MouseMove
                    AddHandler newLabelControlDays.MouseLeave, AddressOf PanelControlDay_MouseLeave
                End If

            End If
        Next
    End Sub

    ''' <summary>
    ''' Funcion que devuelve el icono a dibujar en el dia
    ''' </summary>
    ''' <param name="icon">el icono en una enumeracion</param>
    ''' <returns>el icono tipo System.Drawing.Bitmap</returns>
    ''' <remarks></remarks>
    Public Function SetImageIconSchedule(ByVal icon As EImageIconSchedule, ByVal Big As Boolean) As System.Drawing.Bitmap
        'Big = False
        Select Case icon
            Case Is = EImageIconSchedule.Morning
                If Big = False Then
                    Return My.Resources.manana_16x16
                Else
                    Return My.Resources.manana
                End If


            Case Is = EImageIconSchedule.Morning_Afternoon
                If Big = False Then
                    Return My.Resources.manana_tarde_16x16
                Else
                    Return My.Resources.mananatarde
                End If



            Case Is = EImageIconSchedule.Afternoon
                If Big = False Then
                    Return My.Resources.tarde_16x16
                Else
                    Return My.Resources.tarde
                End If


            Case Is = EImageIconSchedule.Morning_Night
                If Big = False Then
                    Return My.Resources.manana_noche_16x16
                Else
                    Return My.Resources.manananoche
                End If


            Case Is = EImageIconSchedule.Afternoon_Night
                If Big = False Then
                    Return My.Resources.tarde_noche_16x16
                Else
                    Return My.Resources.tarde_noche
                End If


            Case Is = EImageIconSchedule.Night
                If Big = False Then
                    Return My.Resources.noche_16x16
                Else
                    Return My.Resources.noche
                End If




            Case Is = EImageIconSchedule.Inability

                Return My.Resources.incapacidad

            Case Is = EImageIconSchedule.License

                Return My.Resources.permiso

            Case Is = EImageIconSchedule.Sanction

                Return My.Resources.sancion2

            Case Is = EImageIconSchedule.Vacation

                Return My.Resources.trabajador_en_vacaciones

            Case Is = EImageIconSchedule.PermisoVacationes

                Return My.Resources.trabajador_en_vacaciones
        End Select
    End Function

    ''' <summary>
    ''' Metodo que genera el calendario (Asigna valores, limpia, y crea)
    ''' </summary>
    Private Sub Generate_Calendar()
        AssignValues()
        Reset_Calendar()
        Create_Calendar()
    End Sub


#End Region

End Class
#Region "Enums"
''' <summary>
''' Enumeracion que contiene el icono de el turno a pintar
''' </summary>
''' <remarks></remarks>
Public Enum EImageIconSchedule As Integer
    ''' <summary>
    ''' Mañana
    ''' </summary>
    Morning = 1
    ''' <summary>
    ''' Tarde
    ''' </summary>
    Afternoon = 2
    ''' <summary>
    ''' Mañana - Tarde
    ''' </summary>
    Morning_Afternoon = 3
    ''' <summary>
    ''' Mañana - Noche
    ''' </summary>
    Morning_Night = 4
    ''' <summary>
    ''' Tarde - Noche
    ''' </summary>
    Afternoon_Night = 5
    ''' <summary>
    ''' Noche
    ''' </summary>
    Night = 6


    ''' <summary>
    ''' Incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    Inability = 7

    ''' <summary>
    ''' Permiso
    ''' </summary>
    ''' <remarks></remarks>
    License = 8

    ''' <summary>
    ''' Sanción
    ''' </summary>
    ''' <remarks></remarks>
    Sanction = 9

    ''' <summary>
    ''' Vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Vacation = 10

    ''' <summary>
    ''' Permiso de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    PermisoVacationes

End Enum

#End Region