Imports DevExpress.Utils.Design.DesignTimeTools
''' <summary>
''' Control de calendario pequeño para seleccionar dias del mes
''' </summary>
''' <remarks></remarks>
Public Class CtrCalendarMini

#Region "Globals variables"
    ''' <summary>
    ''' Esta variable contiene el año del calendario
    ''' </summary>
    Dim _year As Integer = DateTime.Now.Year
    Public Property YearControl As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
        End Set
    End Property

    ''' <summary>
    ''' Esta variable contiene el mes del calendario
    ''' </summary>
    Dim _month As Integer = DateTime.Now.Month
    Public Property MonthControl As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
        End Set
    End Property

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
    ''' Variable que contiene la CultureInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ci As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ES-CO")

    ''' <summary>
    ''' Variable que contiene el DateTimeFormatInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtfi As System.Globalization.DateTimeFormatInfo = Nothing

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

#Region "Load"
    ''' <summary>
    ''' Evento load del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If
        Inizialite_Parameters()
        Create_Box()
        Generate_Calendar()
        ListDaysToDelete = New List(Of Integer)
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
            _locY = 25
        End If
        If _incX = 0 Then
            _incX = 24
        End If
        If _incY = 0 Then
            _incY = 23
        End If
        If _sizeChilds = Nothing Then
            _sizeChilds = New Drawing.Size(25, 25)
        End If

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
                        .Appearance.Font = New Drawing.Font("Segoe UI", 8.0!)
                        .Text = Microsoft.VisualBasic.Strings.StrConv(Microsoft.VisualBasic.Left(dtfi.GetDayName((j - 1)), 2), Microsoft.VisualBasic.VbStrConv.ProperCase) 'resto 1 porq el array del framework comienza en 0
                        .Location = New System.Drawing.Point(accumX + 2, (accumY - 15))
                    End With
                    INDpcMain.Controls.Add(newLcHeader)
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
                INDpcMain.Controls.Add(newPc)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Metodo que limpiara los panel controls que contienen los dias del mes
    ''' </summary>
    Private Sub Reset_Calendar()
        For i As Integer = 0 To INDpcMain.Controls.Count - 1
            If INDpcMain.Controls.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                'INDPcMain.Controls.Item(i).Enabled = False
                INDpcMain.Controls.Item(i).Tag = ""
                INDpcMain.Controls.Item(i).Controls.Clear()
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo para crear los dias del calendario
    ''' </summary>
    Private Sub Create_Calendar()
        For i As Integer = 0 To INDpcMain.Controls.Count - 1
            If INDpcMain.Controls.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = INDpcMain.Controls.Item(i)
                Dim numBox = item.Name.Replace("Box", "")
                Dim DayEquals As String
                DayEquals = ((numBox + 1) - _weekDay) - 1
                Dim numDay As String
                If DayEquals = 0 Then
                    Dim _date As Date = New Date(_year, _month, 1)
                    numDay = _date.Day
                Else
                    Dim _date As Date = New Date(_year, _month, 1).AddDays(DayEquals)
                    numDay = _date.Day
                End If

                If Not (numBox >= _weekDay And numBox < (_daysOnMonth + _weekDay)) Then
                    'creacion label control para los dias por fuera del mes
                    Dim newLabelControlOther As New DevExpress.XtraEditors.LabelControl
                    Dim fontColor As Drawing.Color = Color.LightGray

                    With newLabelControlOther
                        .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                        .Appearance.BackColor = Color.White
                        .Size = New Drawing.Size(23, 23)
                        .Name = "INDLcDays" & item.Tag
                        .Tag = item.Tag
                        .Appearance.ForeColor = fontColor
                        .Appearance.Font = New Drawing.Font("Segoe UI", 8.0!)
                        .Text = numDay
                        .Location = New Point(1, 1)
                    End With
                    item.Controls.Add(newLabelControlOther)

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
                        .Appearance.BackColor = Color.White
                        .Size = New Drawing.Size(23, 23)
                        .Name = "INDLcDays" & item.Tag
                        .Tag = item.Tag
                        .Appearance.ForeColor = fontColor
                        .Appearance.Font = New Drawing.Font("Segoe UI", 8.0!)
                        .Text = numDay
                        .Location = New Point(1, 1)
                        .Cursor = Cursors.Hand
                    End With
                    item.Controls.Add(newLabelControlDays)
                    AddHandler newLabelControlDays.MouseMove, AddressOf PanelControlDay_MouseMove
                    AddHandler newLabelControlDays.MouseLeave, AddressOf PanelControlDay_MouseLeave
                    AddHandler newLabelControlDays.Click, Sub(s, a)
                                                              ClikDaysToDelete(s, a)
                                                          End Sub
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que genera el calendario (Asigna valores, limpia, y crea)
    ''' </summary>
    Public Sub Generate_Calendar()
        AssignValues()
        Reset_Calendar()
        Create_Calendar()
        ListDaysToDelete = New List(Of Integer)
    End Sub

    ''' <summary>
    ''' Metodo para asignar valores a las variables globales 
    ''' </summary>
    Private Sub AssignValues()
        _date = New Date(_year, _month, 1)
        _weekDay = (_date.DayOfWeek + 1) 'sumo 1 porq el array del framework comienza en 0
        _daysOnMonth = Date.DaysInMonth(_year, _month)
    End Sub
#End Region

#Region "Handles"
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
        'If DaySelected <> _LabelControl.Tag Then
        '    _LabelControl.Appearance.Font = New Drawing.Font(_LabelControl.Appearance.Font, FontStyle.Regular)
        'End If
        _LabelControl.Appearance.Font = New Drawing.Font(_LabelControl.Appearance.Font, FontStyle.Regular)
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Evento para pintar los dias a borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ClikDaysToDelete(sender As Object, e As EventArgs)

        Dim LcDays As DevExpress.XtraEditors.LabelControl = sender
        Dim _parent As DevExpress.XtraEditors.PanelControl = LcDays.Parent
        If _parent.Tag <> "" Then
            If LcDays.BackColor = Color.White Then 'marcado
                LcDays.BackColor = Color.LightGreen
                ListDaysToDelete.Add(_parent.Tag)
            Else  'desmarcado
                LcDays.BackColor = Color.White
                ListDaysToDelete.Remove(_parent.Tag)
            End If
        End If
    End Sub
#End Region

End Class
