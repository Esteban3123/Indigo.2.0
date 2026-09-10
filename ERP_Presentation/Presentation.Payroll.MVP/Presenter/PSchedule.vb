'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay
' Created          : 01-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

''' <summary>
''' Presentador del frontal de Cuadro de turnos
''' </summary>
''' <remarks></remarks>
Public Class PSchedule

#Region "Fields and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As Ischedule

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que contiene la lista de todos los empleados de una unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEmployeeByFU As List(Of Domain.Payroll.Entities.Employee)

    ''' <summary>
    ''' Variable que contiene la lista de todos los empleados de una unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListSchedule As List(Of Schedule)

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As Ischedule)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Me.View.CtrCalendar.MonthControl = DateTime.Now().Month
        Me.View.CtrCalendar.YearControl = DateTime.Now().Year
    End Sub



#End Region

#Region "Methods"

#Region "CtrCalendar"
    ''' <summary>
    ''' Metodo que dibuja los turnos en el calendar segun el empleado
    ''' </summary>
    ''' <param name="items">los ID de los shedule a pintar</param>
    Public Sub Load_Schedule_Detail_Calendar(ByVal items As List(Of Integer))
        Clean_Schedule_Detail_Calendar()
        Clean_Columns_Grid()
        If items.Count = 1 Then
            Load_Schedule_Detail_Calendar_1_Employee(items.Item(0))
        ElseIf items.Count = 2 Then
            Load_Schedule_Detail_Calendar_2_Employee(items.Item(0), items.Item(1))
            Me.View.ActionOnDetailToCompare = True
        End If
        'Mostrar y agregar controles a la vista del detalle
        With Me.View.PanelScheduleDetailComplete
            .Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End With
    End Sub

    ''' <summary>
    ''' Metodo que dibuja los turnos en el calendar del empleado
    ''' </summary>
    ''' <param name="id">el id de empleado que esta seleccionado </param>
    Public Sub Load_Schedule_Detail_Calendar_1_Employee(ByVal id As Integer)
        If Me.View.ScheduleComplete IsNot Nothing Then
            For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = id Then
                    Me.View.ScheduleInPeriodN1 = _listSchDet
                    Exit For
                End If
            Next
        End If
        Me.View.ScheduleN1 = Me.View.ScheduleDatasource.Find(Function(x) x.Employee.Id = id)
        Me.View.DictionaryScheduleDetail_First = Generate_Dictionary(Me.View.ScheduleN1)
        Dim PanelControlMain As DevExpress.XtraEditors.PanelControl = Me.View.CtrCalendar.PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = PanelControlMain.Controls
        'for que recorre todos los dias del calendario
        For i As Integer = 0 To PanelControlChilds.Count - 1
            If PanelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = PanelControlChilds.Item(i)
                If item.Tag <> "" Then
                    Dim LabelControlItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcDays" & item.Tag) 'item.Tag contiene el Numero del dia
                    If Me.View.DictionaryScheduleDetail_First.Item(item.Tag) IsNot Nothing Then
                        Dim PictureEditItem As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeFirst" & item.Tag)
                        With PictureEditItem
                            Dim _enumIcon As Controls.EImageIconSchedule = IconSchedule(Me.View.DictionaryScheduleDetail_First.Item(item.Tag).Letter)
                            Dim _image As System.Drawing.Image = Me.View.CtrCalendar.SetImageIconSchedule(_enumIcon, True)
                            PictureEditItem.Location = Me.View.CtrCalendar.PicPositionSingle
                            PictureEditItem.Image = _image
                            PictureEditItem.Visible = True
                        End With

                        Dim LcItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterFirst" & item.Tag)
                        With LcItem
                            .Text = Me.View.DictionaryScheduleDetail_First.Item(item.Tag).Letter
                            .Visible = True
                            .Location = Me.View.CtrCalendar.LabPositionSingle
                        End With
                    End If
                    Dim ScheduleInOtherFunctionalUnitInThisDay As List(Of ScheduleDetail)
                    If Me.View.ScheduleInPeriodN1 IsNot Nothing Then
                        ScheduleInOtherFunctionalUnitInThisDay = Me.View.ScheduleInPeriodN1.FindAll(Function(x) x.ScheduleFunctionalUnitId <> Me.View.FunctionalUnit.Id And x.DateDetail = New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, item.Tag))
                        If ScheduleInOtherFunctionalUnitInThisDay IsNot Nothing Then
                            If ScheduleInOtherFunctionalUnitInThisDay.Count > 0 Then
                                Dim LcItemMore As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcMore" & item.Tag)
                                With LcItemMore
                                    .Text = "+" & ScheduleInOtherFunctionalUnitInThisDay.Count.ToString
                                    .Cursor = System.Windows.Forms.Cursors.Hand
                                End With
                            End If
                        End If
                    End If
                End If
            End If
        Next

        'Validar Numero total de horas en el mes, de acuerdo al minimo y maximo de horas en el contrato
        Dim Contract As Contract = Me.View.ScheduleN1.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
        Dim Min_Hours As Integer = Contract.Position.MinHourAmount
        Dim Max_Hours As Integer = Contract.Position.MaxHourAmount
        Dim HoursLabelColor As Drawing.Color
        HoursLabelColor = Drawing.Color.Gray
        'If Me.View.ScheduleN1.TotalHour >= Min_Hours Then
        '    HoursLabelColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(155, Byte), Integer))
        'Else
        '    HoursLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        'End If

        Dim TotalHours As Decimal = 0
        Dim TotalHoursEvent As Decimal = 0
        If Me.View.ScheduleComplete IsNot Nothing Then
            For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                If _listSchDet IsNot Nothing Then
                    If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = Me.View.ScheduleN1.EmployeeId Then
                        TotalHours += _listSchDet.Sum(Function(x) x.TotalNumberHours)
                    End If

                    If TotalHours > 0 Then
                        For Each ObjScheduleDetail As ScheduleDetail In _listSchDet
                            For Each ObjScheduleDetailHour As ScheduleDetailHour In ObjScheduleDetail.ScheduleDetailHour
                                If ObjScheduleDetailHour.Event = True AndAlso ObjScheduleDetailHour.Approved = True Then
                                    TotalHoursEvent += ObjScheduleDetailHour.TotalNumberHours
                                End If
                            Next
                        Next
                    End If
                End If
            Next
        End If
        TotalHours = FormatNumber(TotalHours, 1)
        TotalHoursEvent = FormatNumber(TotalHoursEvent, 1)

        Me.View.SDLabelTotalHoursText1.Appearance.BorderColor = HoursLabelColor
        Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 32.0!)
        Me.View.SDLabelTotalHours1.Appearance.BackColor = HoursLabelColor
        Me.View.SDLabelTotalHours1.Text = TotalHours

        If TotalHoursEvent > 0 Then
            Me.View.SDLabelTotalHours1.Text = (TotalHours - TotalHoursEvent) & " Ev.(" & TotalHoursEvent & ")"
            If (TotalHoursEvent + TotalHours) < 100 Then
                Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 13.0!)
            Else
                Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 11.0!)
            End If

        End If


    End Sub

    ''' <summary>
    ''' Metodo que dibuja los turnos en el calendar del empleado
    ''' </summary>
    ''' <param name="id1">id del primer empleado a comparar</param>
    ''' <param name="id2">id del segundo empleado a comparar</param>
    ''' <remarks></remarks>
    Public Sub Load_Schedule_Detail_Calendar_2_Employee(ByVal id1 As Integer, ByVal id2 As Integer)
        If Me.View.ScheduleComplete IsNot Nothing Then
            For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = id1 Then
                    Me.View.ScheduleInPeriodN1 = _listSchDet
                    Exit For
                End If
            Next
            For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = id2 Then
                    Me.View.ScheduleInPeriodN2 = _listSchDet
                    Exit For
                End If
            Next
        End If

        Me.View.ScheduleN1 = Me.View.ScheduleDatasource.Find(Function(x) x.Employee.Id = id1)
        Me.View.DictionaryScheduleDetail_First = Generate_Dictionary(Me.View.ScheduleN1)
        Me.View.ScheduleN2 = Me.View.ScheduleDatasource.Find(Function(x) x.Employee.Id = id2)
        Me.View.DictionaryScheduleDetail_Second = Generate_Dictionary(Me.View.ScheduleN2)

        Dim PanelControlMain As DevExpress.XtraEditors.PanelControl = Me.View.CtrCalendar.PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = PanelControlMain.Controls
        'for que recorre todos los dias del calendario
        For i As Integer = 0 To PanelControlChilds.Count - 1
            If PanelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = PanelControlChilds.Item(i)
                If item.Tag <> "" Then
                    Dim LabelControlItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcDays" & item.Tag) 'item.Tag contiene el Numero del dia
                    Dim numDay = CInt(item.Tag)
                    If Me.View.DictionaryScheduleDetail_First.Item(numDay) IsNot Nothing Then
                        Dim PictureEditItem As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeFirst" & item.Tag)
                        With PictureEditItem
                            .Image = Me.View.CtrCalendar.SetImageIconSchedule(IconSchedule(Me.View.DictionaryScheduleDetail_First.Item(item.Tag).Letter), True)
                            .Visible = True
                            .Tag = "1"
                            .Location = Me.View.CtrCalendar.PicPositionCouple1
                        End With
                        Dim LcItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterFirst" & item.Tag)
                        With LcItem
                            .Text = Me.View.DictionaryScheduleDetail_First.Item(item.Tag).Letter
                            .Visible = True
                            .Location = Me.View.CtrCalendar.LabPositionCouple1
                            .Tag = "1"
                        End With
                    End If
                    If Me.View.DictionaryScheduleDetail_Second.Item(numDay) IsNot Nothing Then
                        Dim PictureEditItem As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeSecond" & item.Tag)
                        With PictureEditItem
                            .Image = Me.View.CtrCalendar.SetImageIconSchedule(IconSchedule(Me.View.DictionaryScheduleDetail_Second.Item(item.Tag).Letter), True)
                            .Visible = True
                            .Tag = "2"
                            .Location = Me.View.CtrCalendar.PicPositionCouple2
                        End With
                        Dim LcItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterSecond" & item.Tag)
                        With LcItem
                            .Text = Me.View.DictionaryScheduleDetail_Second.Item(item.Tag).Letter
                            .Visible = True
                            .Location = Me.View.CtrCalendar.LabPositionCouple2
                            .Tag = "2"
                        End With
                    End If
                End If
            End If
        Next

        'Calculo total de horas de los 2 empleados en todo el periodo
        Dim TotalHours1 As Decimal = 0
        Dim TotalHours2 As Decimal = 0
        Dim TotalHoursEvent1 As Decimal = 0
        Dim TotalHoursEvent2 As Decimal = 0

        If Me.View.ScheduleComplete IsNot Nothing Then
            For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                If _listSchDet IsNot Nothing Then
                    If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = Me.View.ScheduleN1.EmployeeId Then
                        TotalHours1 += _listSchDet.Sum(Function(x) x.TotalNumberHours)

                        If TotalHours1 > 0 Then
                            For Each ObjScheduleDetail As ScheduleDetail In _listSchDet
                                For Each ObjScheduleDetailHour As ScheduleDetailHour In ObjScheduleDetail.ScheduleDetailHour
                                    If ObjScheduleDetailHour.Event = True AndAlso ObjScheduleDetailHour.Approved = True Then
                                        TotalHoursEvent1 += ObjScheduleDetailHour.TotalNumberHours
                                    End If
                                Next
                            Next
                        End If

                    End If
                    If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = Me.View.ScheduleN2.EmployeeId Then
                        TotalHours2 += _listSchDet.Sum(Function(x) x.TotalNumberHours)

                        If TotalHours2 > 0 Then
                            For Each ObjScheduleDetail As ScheduleDetail In _listSchDet
                                For Each ObjScheduleDetailHour As ScheduleDetailHour In ObjScheduleDetail.ScheduleDetailHour
                                    If ObjScheduleDetailHour.Event = True AndAlso ObjScheduleDetailHour.Approved = True Then
                                        TotalHoursEvent2 += ObjScheduleDetailHour.TotalNumberHours
                                    End If
                                Next
                            Next
                        End If

                    End If
                End If
            Next
        End If
        TotalHours1 = FormatNumber(TotalHours1, 1)
        TotalHours2 = FormatNumber(TotalHours2, 1)
        TotalHoursEvent1 = FormatNumber(TotalHoursEvent1, 1)
        TotalHoursEvent2 = FormatNumber(TotalHoursEvent2, 1)

        'Validar Numero total de horas en el mes, de acuerdo al minimo y maximo de horas en el contrato del registro No 1
        Dim Contract1 As Contract = Me.View.ScheduleN1.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
        Dim Min_Hours1 As Integer = Contract1.Position.MinHourAmount
        Dim Max_Hours1 As Integer = Contract1.Position.MaxHourAmount
        Dim HoursLabelColor1 As Drawing.Color = Me.View.CtrCalendar.RegColor1
        'If Me.View.ScheduleN1.TotalHour >= Min_Hours1 Then
        '    HoursLabelColor1 = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(155, Byte), Integer))
        'Else
        '    HoursLabelColor1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        'End If       

        Me.View.SDLabelTotalHoursText1.Appearance.BorderColor = HoursLabelColor1
        Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 32.0!)
        Me.View.SDLabelTotalHours1.Appearance.BackColor = HoursLabelColor1
        Me.View.SDLabelTotalHours1.Text = TotalHours1
        Me.View.SDLabelEmployee1.Text = Me.View.ScheduleN1.Employee.ThirdParty.Name

        If TotalHoursEvent1 > 0 Then
            Me.View.SDLabelTotalHours1.Text = (TotalHours1 - TotalHoursEvent1) & " Ev.(" & TotalHoursEvent1 & ")"

            If (TotalHoursEvent1 + TotalHours1) < 100 Then
                Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 13.0!)
            Else
                Me.View.SDLabelTotalHours1.Appearance.Font = New Drawing.Font("Segoe UI", 11.0!)
            End If

        End If

        'Validar Numero total de horas en el mes, de acuerdo al minimo y maximo de horas en el contrato del registro No 2
        Dim Contract2 As Contract = Me.View.ScheduleN2.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
        Dim Min_Hours2 As Integer = Contract2.Position.MinHourAmount
        Dim Max_Hours2 As Integer = Contract2.Position.MaxHourAmount
        Dim HoursLabelColor2 As Drawing.Color = Me.View.CtrCalendar.RegColor2
        'If Me.View.ScheduleN2.TotalHour >= Min_Hours2 Then
        '    HoursLabelColor2 = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(155, Byte), Integer))
        'Else
        '    HoursLabelColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        'End If
        Me.View.SDLabelTotalHoursText2.Appearance.BorderColor = HoursLabelColor2
        Me.View.SDLabelTotalHours2.Appearance.Font = New Drawing.Font("Segoe UI", 32.0!)
        Me.View.SDLabelTotalHours2.Appearance.BackColor = HoursLabelColor2
        Me.View.SDLabelTotalHours2.Text = TotalHours2
        Me.View.SDLabelEmployee2.Text = Me.View.ScheduleN2.Employee.ThirdParty.Name

        If TotalHoursEvent2 > 0 Then
            Me.View.SDLabelTotalHours2.Text = (TotalHours2 - TotalHoursEvent2) & " Ev.(" & TotalHoursEvent2 & ")"

            If (TotalHoursEvent2 + TotalHours2) < 100 Then
                Me.View.SDLabelTotalHours2.Appearance.Font = New Drawing.Font("Segoe UI", 13.0!)
            Else
                Me.View.SDLabelTotalHours2.Appearance.Font = New Drawing.Font("Segoe UI", 11.0!)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar el ctrCalendar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Schedule_Detail_Calendar()
        Me.View.ScheduleGridControl.DataSource = Nothing
        Me.View.DictionaryScheduleDetail_First = Nothing
        Me.View.ScheduleN1 = Nothing
        Me.View.DictionaryScheduleDetail_Second = Nothing
        Me.View.ScheduleN2 = Nothing
        Me.View.ActionOnDetailToCompare = False
        With Me.View.PanelScheduleDetailComplete
            .Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End With
        Me.View.CtrCalendar.DaySelected = 0 'limpio la propiedad de el calendari que me dice si algun dia esta seleccionado

        Dim PanelControlMain As DevExpress.XtraEditors.PanelControl = Me.View.CtrCalendar.PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = PanelControlMain.Controls
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
                    PictureEditItem1.Visible = False
                    PictureEditItem1.Image = Nothing
                    PictureEditItem1.Tag = ""
                    PictureEditItem2.Visible = False
                    PictureEditItem2.Image = Nothing
                    PictureEditItem2.Tag = ""
                    LcItem1.Tag = ""
                    LcItem1.Text = ""
                    LcItem1.Visible = False
                    LcItem2.Tag = ""
                    LcItem2.Text = ""
                    LcItem2.Visible = False
                    LabelControlItem.Font = New Drawing.Font(LabelControlItem.Font, Drawing.FontStyle.Regular)
                    LcItemMore.Text = ""
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo para subrayar el dia clickeado que muestra su detalle
    ''' </summary>
    ''' <param name="_numday">el numero del dia</param>
    Public Sub HighlightNumDay(ByVal _numday As String)
        Dim PanelControlMain As DevExpress.XtraEditors.PanelControl = Me.View.CtrCalendar.PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = PanelControlMain.Controls
        For i As Integer = 0 To PanelControlChilds.Count - 1
            If PanelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = PanelControlChilds.Item(i)
                If item.Tag <> "" Then
                    Dim LabelControlItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcDays" & item.Tag) 'item.Tag contiene el Numero del dia
                    If LabelControlItem.Tag <> _numday Then
                        LabelControlItem.Font = New Drawing.Font(LabelControlItem.Font, Drawing.FontStyle.Regular)
                    End If
                End If
            End If
        Next
        Me.View.CtrCalendar.DaySelected = _numday
    End Sub

    ''' <summary>
    ''' Metodo que devuelve la enumeracion del icono que se debe colocar en un determinado turno dependiendo el horario
    ''' </summary>
    ''' <param name="_letter">la letra del turno Ejm: "M","T","N","MT","MN","TN"</param>
    Public Function IconSchedule(ByVal _letter As String) As Controls.EImageIconSchedule
        Select Case _letter
            Case Is = "M"
                Return Controls.EImageIconSchedule.Morning
            Case Is = "MT"
                Return Controls.EImageIconSchedule.Morning_Afternoon
            Case Is = "T"
                Return Controls.EImageIconSchedule.Afternoon
            Case Is = "MN"
                Return Controls.EImageIconSchedule.Morning_Night
            Case Is = "TN"
                Return Controls.EImageIconSchedule.Afternoon_Night
            Case Is = "N"
                Return Controls.EImageIconSchedule.Night
            Case Is = "I"
                Return Controls.EImageIconSchedule.Inability
            Case Is = "L"
                Return Controls.EImageIconSchedule.License
            Case Is = "S"
                Return Controls.EImageIconSchedule.Sanction
            Case Is = "V"
                Return Controls.EImageIconSchedule.Vacation
            Case Is = "PV"
                Return Controls.EImageIconSchedule.PermisoVacationes
        End Select
    End Function

    ''' <summary>
    ''' Metodo que retorna los schedule detail en forma de diccionario
    ''' </summary>
    ''' <param name="_schedule">el registro de schedule</param>
    ''' <returns>el diccionario</returns>
    Public Function Generate_Dictionary(ByVal _schedule As Schedule) As Dictionary(Of Integer, ScheduleDetail)
        Dim dictionaryScheduleDetail As New Dictionary(Of Integer, ScheduleDetail)
        dictionaryScheduleDetail.Add(1, _schedule.ScheduleDetail)
        dictionaryScheduleDetail.Add(2, _schedule.ScheduleDetail1)
        dictionaryScheduleDetail.Add(3, _schedule.ScheduleDetail2)
        dictionaryScheduleDetail.Add(4, _schedule.ScheduleDetail3)
        dictionaryScheduleDetail.Add(5, _schedule.ScheduleDetail4)
        dictionaryScheduleDetail.Add(6, _schedule.ScheduleDetail5)
        dictionaryScheduleDetail.Add(7, _schedule.ScheduleDetail6)
        dictionaryScheduleDetail.Add(8, _schedule.ScheduleDetail7)
        dictionaryScheduleDetail.Add(9, _schedule.ScheduleDetail8)
        dictionaryScheduleDetail.Add(10, _schedule.ScheduleDetail9)
        dictionaryScheduleDetail.Add(11, _schedule.ScheduleDetail10)
        dictionaryScheduleDetail.Add(12, _schedule.ScheduleDetail11)
        dictionaryScheduleDetail.Add(13, _schedule.ScheduleDetail12)
        dictionaryScheduleDetail.Add(14, _schedule.ScheduleDetail13)
        dictionaryScheduleDetail.Add(15, _schedule.ScheduleDetail14)
        dictionaryScheduleDetail.Add(16, _schedule.ScheduleDetail15)
        dictionaryScheduleDetail.Add(17, _schedule.ScheduleDetail16)
        dictionaryScheduleDetail.Add(18, _schedule.ScheduleDetail17)
        dictionaryScheduleDetail.Add(19, _schedule.ScheduleDetail18)
        dictionaryScheduleDetail.Add(20, _schedule.ScheduleDetail19)
        dictionaryScheduleDetail.Add(21, _schedule.ScheduleDetail20)
        dictionaryScheduleDetail.Add(22, _schedule.ScheduleDetail21)
        dictionaryScheduleDetail.Add(23, _schedule.ScheduleDetail22)
        dictionaryScheduleDetail.Add(24, _schedule.ScheduleDetail23)
        dictionaryScheduleDetail.Add(25, _schedule.ScheduleDetail24)
        dictionaryScheduleDetail.Add(26, _schedule.ScheduleDetail25)
        dictionaryScheduleDetail.Add(27, _schedule.ScheduleDetail26)
        dictionaryScheduleDetail.Add(28, _schedule.ScheduleDetail27)
        dictionaryScheduleDetail.Add(29, _schedule.ScheduleDetail28)
        dictionaryScheduleDetail.Add(30, _schedule.ScheduleDetail29)
        dictionaryScheduleDetail.Add(31, _schedule.ScheduleDetail30)
        Return dictionaryScheduleDetail
    End Function
#End Region

#Region "Rejilla"

    ''' <summary>
    ''' Metodo para cargar los datos en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Load_Schedule_Detail_Grid()
        Clean_Schedule_Detail_Calendar()
        Create_Columns_Grid()
        For Each _sch As Schedule In Me.View.ScheduleDatasource
            _sch.Apply = False
        Next


        For Each _int As Integer In Me.View.ScheduleIdListChecked
            Dim _sch As Schedule = Me.View.ScheduleDatasource.Find(Function(x) x.EmployeeId = _int)
            If _sch IsNot Nothing Then
                _sch.Apply = True
            End If
        Next
        Me.View.ScheduleGridControl.DataSource = Me.View.ScheduleDatasource
        Best_fit_columns()
    End Sub

    ''' <summary>
    ''' Metodo para crear las columnas de la rejilla
    ''' </summary>
    Public Sub Create_Columns_Grid()
        Dim list_Band As New List(Of DevExpress.XtraGrid.Views.BandedGrid.GridBand)()
        Dim list_Column As New List(Of DevExpress.XtraGrid.Columns.GridColumn)
        Dim ScheduleDate As Date
        ScheduleDate = Me.View.CtrCalendar.ScheduleDate
        Dim Week_Day As Integer
        Dim fieldName As String
        Clean_Columns_Grid()


        'Crea columna de Ubicación
        Dim BandLocation As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        With BandLocation
            .Caption = "Ubicación"
            .Name = "INDBandLocation"
        End With
        Dim ColumnLocation As New DevExpress.XtraGrid.Columns.GridColumn
        With ColumnLocation
            .FieldName = "Location"
            .Caption = "SU-UF"
            .Name = "INDColLocation"
            .Visible = True
        End With
        'BandLocation.Columns.Add(ColumnLocation)
        'list_Band.Add(BandLocation)
        'list_Column.Add(ColumnLocation)
        'Crea las columna de los 31 dias
        For i As Integer = 1 To Me.View.CtrCalendar.DaysInThisMonth
            Week_Day = Weekday(ScheduleDate)
            If i = 1 Then
                fieldName = "ScheduleDetail" & ""
            Else
                fieldName = "ScheduleDetail" & i - 1 & ""
            End If
            Dim BandDay As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            With BandDay
                .Caption = WeekdayName(Week_Day, False, FirstDayOfWeek.Sunday)
                .Name = "INDBandDay" & i
            End With
            Dim ColumnDay As New DevExpress.XtraGrid.Columns.GridColumn
            With ColumnDay
                Dim dateValue As Date
                dateValue = New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i)
                .AppearanceHeader.Font = New Drawing.Font("Segoe UI", 9.0!, Drawing.FontStyle.Bold)
                .AppearanceHeader.Options.UseFont = True
                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                .AppearanceCell.Options.UseTextOptions = True
                .AppearanceHeader.Options.UseTextOptions = True
                .FieldName = fieldName
                .Caption = i & Microsoft.VisualBasic.Strings.StrConv(Left(dateValue.ToString("ddd", Indigo.Culture), 1), Microsoft.VisualBasic.VbStrConv.ProperCase)
                .Name = "INDColDay" & i
                .Visible = True
                .Tag = i
                .OptionsColumn.AllowEdit = False
            End With
            'BandDay.Columns.Add(ColumnDay)
            'list_Band.Add(BandDay)
            list_Column.Add(ColumnDay)
            ScheduleDate = DateAdd(DateInterval.Day, +1, ScheduleDate)
        Next
        'Crea columna de Total Horas
        Dim BandTotalHour As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        With BandTotalHour
            .Caption = "Total de"
            .Name = "INDBandThour"
        End With
        Dim ColumnTotalHour As New DevExpress.XtraGrid.Columns.GridColumn
        With ColumnTotalHour
            .AppearanceHeader.Font = New Drawing.Font("Segoe UI Semibold", 8.5!, Drawing.FontStyle.Bold)
            .FieldName = "TotalHour"
            .Caption = "Horas"
            .Name = "INDColTHour"
            .Visible = True
            .OptionsColumn.AllowEdit = False
        End With
        'BandTotalHour.Columns.Add(ColumnTotalHour)
        'list_Band.Add(BandTotalHour)
        list_Column.Add(ColumnTotalHour)

        'Repositorio check edit
        Dim RepCheckApply = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        RepCheckApply.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        'Columna para checkear
        Dim ColumnCheck As New DevExpress.XtraGrid.Columns.GridColumn
        With ColumnCheck
            .ColumnEdit = RepCheckApply
            .Caption = " "
            .Visible = True
            .Name = "Apply"
            .FieldName = "Apply"
            .OptionsColumn.AllowEdit = True
        End With
        list_Column.Add(ColumnCheck)

        'Crea columna de empleado
        Dim BandEmployee As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        With BandEmployee
            .Caption = "Nombre"
        End With
        Dim ColumnEmployee As New DevExpress.XtraGrid.Columns.GridColumn
        With ColumnEmployee
            .AppearanceHeader.Font = New Drawing.Font("Segoe UI Semibold", 8.5!, Drawing.FontStyle.Bold)
            .FieldName = "Employee.ThirdParty.Name"
            .Caption = "Funcionario"
            .Name = "INDColEmployee"
            .Visible = True
            .OptionsColumn.AllowEdit = False
        End With
        'BandEmployee.Columns.Add(ColumnEmployee)
        'list_Band.Add(BandEmployee)
        list_Column.Add(ColumnEmployee)


        For i As Integer = 0 To list_Column.Count - 1
            Me.View.ScheduleGridView.Columns.Add(list_Column.Item(i))
        Next

    End Sub

    ''' <summary>
    ''' Metodo para limpiar las columnas de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Columns_Grid()
        Me.View.ScheduleGridControl.DataSource = Nothing
        Me.View.ScheduleGridView.Columns.Clear()
    End Sub


#End Region

#Region "Otros"

    ''' <summary>
    ''' Lista los contratos que tiene el empleado
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListContractsByEmployeeId(EmployeeId As Integer) As List(Of PayrollContractXpo)
        Dim filtroConsulta As String = "EmployeeId.Id = " & EmployeeId
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollContractXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista el contrato activo que tiene el empleado
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetContractByEmployeeId(EmployeeId As Integer) As PayrollContractXpo
        Dim filtroConsulta As String = "EmployeeId.Id = " & EmployeeId & " And Valid = " & True
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollContractXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Inicializa el datasource de Unidades Funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FunctionalUnit_Datasource()
        Me.View.FunctionalUnit_Datasource = FunctiontalUnit_XPInstantFeedbackSource()
    End Sub

    Public Sub FunctionalUnit_DatasourceUser(IdUser As Integer)
        Me.View.FunctionalUnit_Datasource = FunctiontalUnit_XPInstantFeedbackSourceUserId(IdUser)
    End Sub

    ''' <summary>
    ''' Contiene el objeto XPInstantFeedbackSource de las unidades funcionales
    ''' </summary>
    Public Function FunctiontalUnit_XPInstantFeedbackSource() As DevExpress.Xpo.XPInstantFeedbackSource
        Dim modelBusqueda As New Presentation.Controls.MVP.MBusqueda
        Return modelBusqueda.ConsultarEntidades(eDataSource.ListFunctionalUnit, (True))
    End Function

    Public Function FunctiontalUnit_XPInstantFeedbackSourceUserId(IdUser As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Dim modelBusqueda As New Presentation.Controls.MVP.MBusqueda
        Return modelBusqueda.ConsultarEntidades(eDataSource.ListFunctionalUnitByUser, (IdUser))
    End Function

    ''' <summary>
    ''' Limpiar lista de empleados
    ''' </summary>
    Private Sub Clean_Employee_List_Box()
        Me.View.ListEmployee.Items.Clear()
    End Sub

    ''' <summary>
    ''' Inicializa el listado de festivos
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Load_Holidays()
        Using Model As New Presentation.Common.MVP.MHoliday
            Dim _date() = Me.View.Period.Split("/")
            Me.View.List_Holiday = Await Model.ListAllHolidaysbyYearsAsync(_date(1))
        End Using
    End Sub

    ''' <summary>
    ''' Retorna una lista de las plantillas de horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ScheduleTemplate_Datasource() As Task(Of List(Of ScheduleTemplate))
        Using Model As New MScheduleControl
            Return Await Model.ListAllScheduleTemplateByState(True)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para generar los empleados y scheduls detail apartir de la unidad funcional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function Load_Employee(PermissionAllPosition As Boolean, SessionValue As SessionValues) As Task
        Me.View.ScheduleComplete = Nothing
        Clean_Employee_List_Box()
        Me.View.ScheduleDatasource = New List(Of Schedule)
        Using Model As New MEmployee(MEmployee.TAG)
            ListEmployeeByFU = Await Model.GetEmployeesByFunctionalUnitAsync(Me.View.FunctionalUnit.Id)
        End Using
        Using Model As New MSchedule
            ListSchedule = Await Model.ListAllScheduleByFunctionalUnitWithPeriodAsync(Me.View.FunctionalUnit.Id, Me.View.Period)
        End Using

        Dim ListRollPosition As List(Of PositionRoll)
        Dim ListUserPosition As List(Of PositionUser)
        Dim ListIdPosition As New List(Of Integer)

        If PermissionAllPosition = False Then
            Using Model As New MSchedule
                ListRollPosition = Await Model.GetPositionRoll(0, SessionValue.UserRol)
                ListUserPosition = Await Model.GetPositionUser(SessionValue.UserIndigoId)

                If ListRollPosition Is Nothing AndAlso ListUserPosition Is Nothing Then
                    Exit Function
                End If

                If ListRollPosition IsNot Nothing AndAlso ListRollPosition.Count > 0 Then
                    For Each ObjRolePosition As PositionRoll In ListRollPosition
                        ListIdPosition.Add(ObjRolePosition.IdPosition)
                    Next
                End If

                If ListUserPosition IsNot Nothing AndAlso ListUserPosition.Count > 0 Then
                    For Each ObjUserPosition As PositionUser In ListUserPosition
                        ListIdPosition.Add(ObjUserPosition.IdPosition)
                    Next
                End If

                If ListIdPosition.Count = 0 Then
                    Exit Function
                End If

                ListIdPosition = ListIdPosition.Distinct().ToList()

            End Using
        End If

        Dim ListPosition As New List(Of Position)
        Dim ListIdEmployee As New List(Of Integer)
        Dim ListNewSchedule As New List(Of Schedule)


        If ListSchedule?.Any() Then
            '            Me.View.ScheduleDatasource = ListSchedule
            For Each _employee As Employee In ListEmployeeByFU
                If ListSchedule.Find(Function(x) x.EmployeeId = _employee.Id) Is Nothing Then
                    Dim _reg As Boolean = False

                    'Todos los permisos, no se filtran cargos
                    Dim _validContract As Contract = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
                    For i As Integer = 1 To DateTime.DaysInMonth(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl)
                        If New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) > _validContract.JobBondingDate _
                                AndAlso New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) < _validContract.ContractEndingDate Then
                            _reg = True
                            Exit For
                        End If
                    Next

                    If PermissionAllPosition = True Then
                        If _reg Then
                            ListNewSchedule.Add(NewSchedule(_employee))
                            'Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    Else
                        If _reg And _employee.Contract.Any(Function(x) ListIdPosition.Any(Function(y) y = x.PositionId And x.Valid = True)) Then
                            ListNewSchedule.Add(NewSchedule(_employee))
                            'Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    End If

                    'Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                Else

                    Dim _reg As Boolean = False

                    'Todos los permisos, no se filtran cargos
                    Dim _validContract As Contract = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
                    For i As Integer = 1 To DateTime.DaysInMonth(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl)
                        If New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) > _validContract.JobBondingDate _
                                AndAlso New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) < _validContract.ContractEndingDate Then
                            _reg = True
                            Exit For
                        End If
                    Next

                    If PermissionAllPosition = True Then
                        If _reg Then
                            ListIdEmployee.Add(_employee.Id)
                            'Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    Else
                        If _reg And _employee.Contract.Any(Function(x) ListIdPosition.Any(Function(y) y = x.PositionId And x.Valid = True)) Then
                            ListIdEmployee.Add(_employee.Id)
                            'Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    End If
                End If
            Next
        Else
            If ListEmployeeByFU?.Any() Then
                For Each _employee As Employee In ListEmployeeByFU
                    Dim _reg As Boolean = False
                    Dim _validContract As Contract = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
                    For i As Integer = 1 To DateTime.DaysInMonth(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl)
                        If New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) > _validContract.JobBondingDate _
                            AndAlso New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, i) < _validContract.ContractEndingDate Then
                            _reg = True
                            Exit For
                        End If
                    Next

                    If PermissionAllPosition = True Then
                        If _reg Then
                            Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    Else
                        If _reg And _employee.Contract.Any(Function(x) ListIdPosition.Any(Function(y) y = x.PositionId) And x.Valid = True) Then
                            Me.View.ScheduleDatasource.Add(NewSchedule(_employee))
                            ListPosition.Add((_validContract.Position))
                        End If
                    End If

                Next
            End If
        End If


        If ListPosition?.Any() Then
            Me.View.PositionDatasource = ListPosition.Distinct.ToList()
        End If

        Dim tmpCompleteListSchedule As New List(Of Schedule)

        If ListNewSchedule IsNot Nothing AndAlso ListNewSchedule.Count > 0 Then
            tmpCompleteListSchedule.AddRange(ListNewSchedule)
        End If

        If ListSchedule IsNot Nothing AndAlso ListSchedule.Count > 0 And ListIdEmployee.Count <= 0 Then
            tmpCompleteListSchedule.AddRange(ListSchedule)
        End If

        If ListIdEmployee?.Any() And ListSchedule?.Any() Then
            tmpCompleteListSchedule.AddRange(ListSchedule)
        End If

        If tmpCompleteListSchedule IsNot Nothing AndAlso tmpCompleteListSchedule.Count > 0 Then
            Me.View.ScheduleDatasource = tmpCompleteListSchedule
            Me.View.ShowOptionsMenu = True
        Else
            Me.View.ShowOptionsMenu = False
        End If

        LoadEmployeeListBox(Nothing)
    End Function

    ''' <summary>
    ''' Metodo publico que devuelve la entidad Schedule que se agrega nueva para insertar en la rejilla
    ''' </summary>
    ''' <param name="_employee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewSchedule(ByVal _employee As Employee) As Schedule
        Dim _newSchedule As New Schedule
        _newSchedule.Employee = _employee
        _newSchedule.EmployeeId = _employee.Id
        _newSchedule.Period = Me.View.Period
        _newSchedule.FunctionalUnit = Me.View.FunctionalUnit
        _newSchedule.FunctionalUnitId = Me.View.FunctionalUnit.Id
        Return _newSchedule
    End Function

    ''' <summary>
    ''' Metodo que ejecuta el ajuste perfecto de las comunas de  la rejilla
    ''' </summary>
    Public Sub Best_fit_columns()
        For i As Integer = 0 To Me.View.ScheduleGridView.Columns.Count - 1
            Me.View.ScheduleGridView.Columns.Item(i).BestFit()
        Next
    End Sub

    ''' <summary>
    ''' Metodo que ejecuta el ajuste perfecto de las comunas del Serch Look up de las unidades funcionales
    ''' </summary>
    Public Sub Best_fit_columns_Serch_Look_Up()
        For i As Integer = 0 To Me.View.FunctionalUnitGridView.Columns.Count - 1
            Me.View.FunctionalUnitGridView.Columns.Item(i).BestFit()
        Next
    End Sub

    ''' <summary>
    ''' Metodo para cargar todos los schedule detail de todas las unidades funcionales, para un periodo especifico y agruparlo por empleados
    ''' </summary>
    ''' <param name="list_Employees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function Load_ScheduleDetailComplete(list_Employees As List(Of Integer)) As Task
        Me.View.ScheduleInPeriodN1 = Nothing
        Me.View.ScheduleInPeriodN2 = Nothing
        Dim dateInitial As Date = New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, 1)
        Dim dateEnding As Date = New Date(Me.View.CtrCalendar.YearControl, Me.View.CtrCalendar.MonthControl, Me.View.CtrCalendar.DaysInThisMonth)
        If Me.View.ScheduleComplete Is Nothing Then
            Me.View.ScheduleComplete = New List(Of List(Of ScheduleDetail))
        End If
        For Each _int As Integer In list_Employees
            Dim reg As Boolean = True
            If Me.View.ScheduleComplete IsNot Nothing Then
                For i As Integer = 0 To Me.View.ScheduleComplete.Count - 1
                    Dim _listSchDet As List(Of ScheduleDetail) = Me.View.ScheduleComplete.Item(i)
                    If _listSchDet IsNot Nothing Then
                        If _listSchDet.Count > 0 AndAlso _listSchDet.Item(0).EmployeeId = _int Then
                            reg = False
                        End If
                    End If
                Next
                If reg = True Then
                    Using model As New MSchedule
                        Dim schDetByEmployee As List(Of ScheduleDetail) = Await model.GetScheduleDetailByEmployeeBetweenDateAsync(_int, dateInitial, dateEnding)
                        If schDetByEmployee IsNot Nothing Then
                            If schDetByEmployee.Count > 0 Then
                                If Me.View.ScheduleComplete IsNot Nothing Then
                                    Me.View.ScheduleComplete.Add(schDetByEmployee)
                                End If
                            End If
                        End If

                    End Using
                End If
            End If
        Next
        If Me.View.ScheduleComplete Is Nothing Then
            Me.View.ScheduleComplete = New List(Of List(Of ScheduleDetail))
        End If

    End Function

    Public Sub LoadEmployeeListBox(pPositionId As Integer?)
        Me.View.ListEmployee.Items.Clear()

        Dim NewList As List(Of Schedule) = (From a In Me.View.ScheduleDatasource
                                            Where a.Employee.Contract(a.Employee.Contract.Count - 1).Position.Id = pPositionId Or pPositionId Is Nothing
                                            Order By a.Employee.ThirdParty.Name Ascending
                                            Select a).ToList

        For Each item In NewList
            Dim employeeName As String = String.Format("{0} {1}", item.Employee.ThirdParty.Person.FirstName, item.Employee.ThirdParty.Person.FirstLastName)
            Dim employeeJobPosition As String = item.Employee.Contract(item.Employee.Contract.Count - 1).Position.Name
            Dim employeeInfo As String = String.Format("{0} - {1}", employeeName, employeeJobPosition)
            Me.View.ListEmployee.Items.Add(item.EmployeeId, employeeInfo)
        Next
    End Sub
#End Region

#End Region

End Class