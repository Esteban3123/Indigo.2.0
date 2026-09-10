Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools

Public Class CtrDateNavigatorHorizontal

#Region "Fields"
    ''' <summary>
    ''' Objeto para almacenar el control del calendario que vamos a utilizar
    ''' </summary>
    Dim _CtrCalendar As CtrCalendar

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
    ''' Variable que contiene el mes del control
    ''' </summary>
    ''' <remarks></remarks>
    Private _month As Integer

    ''' <summary>
    ''' Variable que contiene el año del control
    ''' </summary>
    ''' <remarks></remarks>
    Private _year As Integer

    ''' <summary>
    ''' Evento que se dispara cuando se cambia la fecha
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnChangeDate As EventHandler
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el control del calendario que vamos a manejar
    ''' </summary>
    Public Property CtrCalendar As CtrCalendar
        Get
            If _CtrCalendar IsNot Nothing Then
                Return _CtrCalendar
            Else
                Return Nothing
            End If
        End Get
        Set(value As CtrCalendar)
            If value IsNot Nothing Then
                _CtrCalendar = value
                SetDateInformation()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Contiene el mes del control
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetMonth As Integer
        Set(value As Integer)
            If value <> Me._month Then
                If value > 12 Then
                    Me._year += 1
                    Me._month = 1
                ElseIf value < 1 Then
                    Me._year -= 1
                    Me._month = 12
                Else
                    Me._month = value
                End If
                SetDateInformation()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Contiene el año del control
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetYear As Integer
        Set(value As Integer)
            If value <> _year Then
                _year = value
                SetDateInformation()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el mes del control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetMonth
        Get
            Return Me._month
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el año del control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetYear
        Get
            Return Me._year
        End Get
    End Property

    ''' <summary>
    ''' Habilita o deshabilita los eventos
    ''' </summary>
    Public Property WithEvent As Boolean = True


#End Region

#Region "Events"

#Region "Click Control"

    ''' <summary>
    ''' Evento click del control Mes anterior
    ''' </summary>
    Private Sub BtnMonthPrev_Click(sender As Object, e As EventArgs)
        If CtrCalendar IsNot Nothing Then
            CtrCalendar.MonthControl -= 1
        Else
            If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                Dim _nextMonth As Integer = Me.GetMonth - 1
                For i As Integer = 1 To 12 Step 1
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = GetYear AndAlso CInt(x.Split("/").ElementAt(0)) = _nextMonth).Count() = 0 Then
                        _nextMonth -= 1
                        If _nextMonth < 1 Then
                            _nextMonth = 12
                        End If
                    Else
                        Me.SetMonth = _nextMonth
                        Exit For
                    End If
                Next
            Else
                Me.SetMonth = Me.GetMonth - 1
            End If
        End If
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento click del control Mes siguiente
    ''' </summary>
    Private Sub BtnMonthNext_Click(sender As Object, e As EventArgs)
        If CtrCalendar IsNot Nothing Then
            CtrCalendar.MonthControl += 1
        Else
            If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                Dim _nextMonth As Integer = Me.GetMonth + 1
                For i As Integer = 1 To 12 Step 1
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = GetYear AndAlso CInt(x.Split("/").ElementAt(0)) = _nextMonth).Count() = 0 Then
                        _nextMonth += 1
                        If _nextMonth > 12 Then
                            _nextMonth = 1
                        End If
                    Else
                        Me.SetMonth = _nextMonth
                        Exit For
                    End If
                Next
            Else
                Me.SetMonth = Me.GetMonth + 1
            End If
        End If
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento click del control Año anterior
    ''' </summary>
    Private Sub BtnYearPrev_Click(sender As Object, e As EventArgs)
        If CtrCalendar IsNot Nothing Then
            CtrCalendar.YearControl -= 1
        Else
            If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                Dim _nextYear As Integer = Me.GetYear - 1
                For i As Integer = 1 To 100 Step 1
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = _nextYear).Count() = 0 Then
                        _nextYear -= 1
                    Else
                        Me.SetYear = _nextYear
                        Exit For
                    End If
                Next
            Else
                Me.SetYear = Me.GetYear - 1
            End If
        End If
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento click del control Año siguiente
    ''' </summary>
    Private Sub BtnYearNext_Click(sender As Object, e As EventArgs)
        If CtrCalendar IsNot Nothing Then
            CtrCalendar.YearControl += 1
        Else
            If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                Dim _nextYear As Integer = Me.GetYear + 1
                For i As Integer = 1 To 100 Step 1
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = _nextYear).Count() = 0 Then
                        _nextYear += 1
                    Else
                        Me.SetYear = _nextYear
                        Exit For
                    End If
                Next
            Else
                Me.SetYear = Me.GetYear + 1
            End If
        End If
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub
#End Region

#Region "Style Control"
    ''' <summary>
    ''' Evento hover sobre los botones
    ''' </summary>
    Private Sub BtnMonthPrev_MouseMove(sender As Object, e As MouseEventArgs)
        Me.Cursor = Cursors.Hand
    End Sub
    ''' <summary>
    ''' Evento Leave sobre los botones
    ''' </summary>
    Private Sub BtnMonthPrev_MouseLeave(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Default
    End Sub
#End Region

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrDateNavigator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetEvents()

        '****CtrCalendar*****'
        If CtrCalendar IsNot Nothing Then
            AddHandler CtrCalendar.OnChangeDate, Sub(s, a)
                                                     SetDateInformation()
                                                     RaiseEvent OnChangeDate(Nothing, Nothing)
                                                 End Sub
        End If
        '********************'



        SetMonth = DateTime.Now().Month
        SetYear = DateTime.Now().Year
        'INDPopCMonths.Size = New System.Drawing.Size(INDLcMonth.Width, INDPopCMonths.Height)

    End Sub

#Region "Variables"
    Private _maximumDate As DateTime

    Private _blockControlsDate As Boolean = False

    Property MaximumDate As DateTime
        Get
            Return _maximumDate
        End Get
        Set(value As DateTime)
            _blockControlsDate = True
            _maximumDate = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de periodos abiertos (formato "mes/año")
    ''' </summary>
    Property ListEnablePeriods As List(Of String)

#End Region

    ''' <summary>
    ''' Evento que se dispara cuando se abre el popup de los meses
    ''' </summary>
    Private Sub INDPopCMonths_Popup(sender As Object, e As EventArgs)
        Dim _thisMonth As Integer = 0
        If CtrCalendar IsNot Nothing Then
            _thisMonth = CtrCalendar.MonthControl
        Else
            _thisMonth = GetMonth
        End If
        For i As Integer = 0 To INDLyCtrMonths.Controls.Count - 1
            Dim _control As Control = INDLyCtrMonths.Controls.Item(i)
            If _control.GetType().ToString() = "DevExpress.XtraEditors.SimpleButton" Then
                Dim SB As DevExpress.XtraEditors.SimpleButton = _control
                SB.Enabled = True
                If CInt(_control.Tag) = _thisMonth Then
                    SB.LookAndFeel.UseDefaultLookAndFeel = False
                    SB.LookAndFeel.UseWindowsXPTheme = False
                    SB.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                    SB.Appearance.BackColor = Color.WhiteSmoke
                    SB.ForeColor = Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
                End If

                'If _blockControlsDate Then
                '    If (GetYear = MaximumDate.Year AndAlso CType(_control.Tag, Integer) > MaximumDate.Month) OrElse (GetYear > MaximumDate.Year) Then
                '        SB.Enabled = False
                '    End If
                'End If

                If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = GetYear AndAlso CInt(x.Split("/").ElementAt(0)) = CInt(_control.Tag)).Count() = 0 Then
                        SB.Enabled = False
                    End If
                End If

                'If ListEnabledMonths IsNot Nothing AndAlso ListEnabledMonths.Count > 0 Then
                '    If ListEnabledMonths.Where(Function(x) x = CType(_control.Tag, Integer)).Count() = 0 Then
                '        SB.Enabled = False
                '    End If
                'End If

            End If
        Next
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento llamado cuando clickean algun boton de los meses en el popup
    ''' </summary>
    Private Sub MonthsClick(sender As Object, e As EventArgs)
        INDPopCMonths.HidePopup()
        If CType(sender, DevExpress.XtraEditors.SimpleButton).Tag <> "" Then
            If CtrCalendar IsNot Nothing Then
                If CtrCalendar.MonthControl <> CType(sender, DevExpress.XtraEditors.SimpleButton).Tag Then
                    CtrCalendar.MonthControl = CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Tag)
                End If
            Else
                If GetMonth <> CType(sender, DevExpress.XtraEditors.SimpleButton).Tag Then
                    SetMonth = CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Tag)
                End If
            End If
        End If
        For i As Integer = 0 To INDLyCtrMonths.Controls.Count - 1
            Dim _control As Control = INDLyCtrMonths.Controls.Item(i)
            If _control.GetType().ToString() = "DevExpress.XtraEditors.SimpleButton" Then
                Dim SB As DevExpress.XtraEditors.SimpleButton = _control
                SB.LookAndFeel.UseDefaultLookAndFeel = True
                SB.LookAndFeel.UseWindowsXPTheme = False
                SB.ForeColor = Color.White
            End If
        Next
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento llamado cuando se abre el pop up de los años
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPopCYears_Popup(sender As Object, e As EventArgs)
        Dim _thisYear As Integer = 0
        If CtrCalendar IsNot Nothing Then
            _thisYear = CtrCalendar.YearControl
        Else
            _thisYear = GetYear
        End If
        For i As Integer = 0 To INDLyCtrYears.Controls.Count - 1
            Dim _control As Control = INDLyCtrYears.Controls.Item(i)
            If _control.GetType().ToString() = "DevExpress.XtraEditors.SimpleButton" Then
                Dim SB As DevExpress.XtraEditors.SimpleButton = _control
                SB.Font = New Font("Segoe UI", 12.0!)
                SB.Enabled = True
                _control.Text = _thisYear + CInt(_control.Tag)
                If CInt(_control.Text) = _thisYear Then
                    SB.LookAndFeel.UseDefaultLookAndFeel = False
                    SB.LookAndFeel.UseWindowsXPTheme = False
                    SB.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                    SB.Appearance.BackColor = Color.WhiteSmoke
                    SB.ForeColor = Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
                End If
                'If _blockControlsDate Then
                '    If CInt(_control.Text) > MaximumDate.Year Then
                '        SB.Enabled = False
                '    End If
                'End If
                'If ListEnabledYears IsNot Nothing AndAlso ListEnabledYears.Count > 0 Then
                '    If ListEnabledYears.Where(Function(x) x = CInt(_control.Text)).Count() = 0 Then
                '        SB.Enabled = False
                '    End If
                'End If

                If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = CInt(_control.Text)).Count() = 0 Then
                        SB.Enabled = False
                    End If
                End If

            End If
        Next
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Evento llamado cuando se da click en alguno de los años del pop up
    ''' </summary>
    Private Sub Years_Click(sender As Object, e As EventArgs)
        INDPopCYears.HidePopup()
        If CType(sender, DevExpress.XtraEditors.SimpleButton).Text <> "" Then
            If CtrCalendar IsNot Nothing Then
                If CtrCalendar.YearControl <> CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Text) Then
                    CtrCalendar.YearControl = CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Text)
                End If
            Else
                If GetYear <> CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Text) Then
                    SetYear = CInt(CType(sender, DevExpress.XtraEditors.SimpleButton).Text)
                End If
            End If
        End If
        RaiseEvent OnChangeDate(Nothing, Nothing)
    End Sub


#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo para establecer el periodo en el control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDateInformation()
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If
        If CtrCalendar IsNot Nothing Then
            INDLcMonth.Text = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(CtrCalendar.MonthControl), Microsoft.VisualBasic.VbStrConv.ProperCase)
            INDLcYear.Text = CtrCalendar.YearControl
        Else
            If ListEnablePeriods IsNot Nothing AndAlso ListEnablePeriods.Count > 0 Then
                If ListEnablePeriods.FindAll(Function(x) CInt(x.Split("/").ElementAt(1)) = GetYear).Count = 0 Then
                    SetYear = ListEnablePeriods.Select(Function(x) CInt(x.Split("/").ElementAt(1))).ToList().Max()
                End If
                Dim _viewMonth As Integer = Me.GetMonth
                For i As Integer = 1 To 12 Step 1
                    If ListEnablePeriods.Where(Function(x) CInt(x.Split("/").ElementAt(1)) = GetYear AndAlso CInt(x.Split("/").ElementAt(0)) = _viewMonth).Count() = 0 Then
                        _viewMonth += 1
                        If _viewMonth > 12 Then
                            _viewMonth = 1
                        End If
                    Else
                        Me.SetMonth = _viewMonth
                        Exit For
                    End If
                Next
                RaiseEvent OnChangeDate(Nothing, Nothing)
            End If
            INDLcMonth.Text = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(Me.GetMonth), Microsoft.VisualBasic.VbStrConv.ProperCase)
            INDLcYear.Text = Me.GetYear
        End If
    End Sub

    ''' <summary>
    ''' Sets the events.
    ''' </summary>
    Private Sub SetEvents()
        If WithEvent Then
            AddHandler INDPopCMonths.Popup, AddressOf INDPopCMonths_Popup
            AddHandler INDBtnEne.Click, AddressOf MonthsClick
            AddHandler INDBtnFeb.Click, AddressOf MonthsClick
            AddHandler INDBtnMar.Click, AddressOf MonthsClick
            AddHandler INDBtnAbr.Click, AddressOf MonthsClick
            AddHandler INDBtnMay.Click, AddressOf MonthsClick
            AddHandler INDBtnJun.Click, AddressOf MonthsClick
            AddHandler INDBtnJul.Click, AddressOf MonthsClick
            AddHandler INDBtnAgo.Click, AddressOf MonthsClick
            AddHandler INDBtnSep.Click, AddressOf MonthsClick
            AddHandler INDBtnOct.Click, AddressOf MonthsClick
            AddHandler INDBtnNov.Click, AddressOf MonthsClick
            AddHandler INDBtnDic.Click, AddressOf MonthsClick
            AddHandler INDPopCYears.Popup, AddressOf INDPopCYears_Popup
            AddHandler INDBtnY1.Click, AddressOf Years_Click
            AddHandler INDBtnY2.Click, AddressOf Years_Click
            AddHandler INDBtnY3.Click, AddressOf Years_Click
            AddHandler INDBtnY4.Click, AddressOf Years_Click
            AddHandler INDBtnY5.Click, AddressOf Years_Click
            AddHandler INDBtnY6.Click, AddressOf Years_Click
            AddHandler INDBtnY7.Click, AddressOf Years_Click
            AddHandler INDBtnY8.Click, AddressOf Years_Click
            AddHandler INDBtnY9.Click, AddressOf Years_Click
            AddHandler INDBtnY10.Click, AddressOf Years_Click
            AddHandler INDBtnY11.Click, AddressOf Years_Click
            AddHandler INDBtnY12.Click, AddressOf Years_Click
            AddHandler BtnMonthPrev.Click, AddressOf BtnMonthPrev_Click
            AddHandler BtnMonthNext.Click, AddressOf BtnMonthNext_Click
            AddHandler BtnYearPrev.Click, AddressOf BtnYearPrev_Click
            AddHandler BtnYearNext.Click, AddressOf BtnYearNext_Click
            Me.INDLcMonth.DropDownControl = Me.INDPopCMonths
            Me.INDLcYear.DropDownControl = Me.INDPopCYears
        End If
    End Sub
#End Region

End Class
