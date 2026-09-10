#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Reporter

#End Region

Public Class FrmReportStatusEfecty

#Region "Properties"

    ''' <summary>
    ''' Propiedad para acceder a la última fecha de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property INDLastClosingDate As Date

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos del tipo de periodo (Mensual, Trimestral, Anual, Comparativo)
    ''' </summary>
    Private _FillingTypePeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypePeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypePeriod Is Nothing Then
                _FillingTypePeriod = New List(Of Tuple(Of Integer, String))
                _FillingTypePeriod.Add(New Tuple(Of Integer, String)(1, "Mensual"))
                _FillingTypePeriod.Add(New Tuple(Of Integer, String)(2, "Trimestral"))
                _FillingTypePeriod.Add(New Tuple(Of Integer, String)(3, "Anual"))
                _FillingTypePeriod.Add(New Tuple(Of Integer, String)(4, "Comparativo"))
            End If
            Return _FillingTypePeriod
        End Get
    End Property

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
#End Region
#Region "Methods"
    ''' <summary>
    ''' se ejecuta en el evento EditValueChanged del control INDGlePeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGlePeriod_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePeriod.EditValueChanged
        INDLciLabelDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciLabelDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciDateStartComp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciDateEndComp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'MENSUAL
        If INDGlePeriod.EditValue = 1 Then
            INDCdnDateEnd.Enabled = False
            INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
            INDCdnDateStart.SetYear = Year(INDLastClosingDate)
            INDCdnDateEnd.SetMonth = Month(INDLastClosingDate)
            INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
            'TRIMESTRAL
        ElseIf INDGlePeriod.EditValue = 2 Then
            INDCdnDateEnd.Enabled = False
            INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
            INDCdnDateStart.SetYear = Year(INDLastClosingDate)
            'If INDCdnDateStart.GetMonth = 12 Then
            '    INDCdnDateStart.SetMonth = 1
            'End If
            'INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth + 2
            'INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            AjustarControl(INDCdnDateStart, INDCdnDateEnd, 2)
            'ANUAL
        ElseIf INDGlePeriod.EditValue = 3 Then
            INDCdnDateEnd.Enabled = False
            INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
            INDCdnDateStart.SetYear = Year(INDLastClosingDate)
            AjustarControl(INDCdnDateStart, INDCdnDateEnd, 11)
            'Comparativo
        ElseIf INDGlePeriod.EditValue = 4 Then
            INDCdnDateEnd.Enabled = True
            INDCdnDateEndComp.Enabled = False
            INDLciLabelDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateStartComp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateEndComp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Asigna el periodo final a partir del perido inicial, adiciona un numero de meses
    ''' </summary>
    ''' <param name="_controlStart"></param>
    ''' <param name="_controlEnd"></param>
    ''' <param name="_meses"></param>
    Private Sub AjustarControl(ByVal _controlStart As Controls.CtrDateNavigator, ByVal _controlEnd As Controls.CtrDateNavigator, ByVal _meses As Integer)
        If _controlStart.GetYear IsNot Nothing AndAlso _controlStart.GetMonth IsNot Nothing Then
            Dim _fecha As Date = New Date(_controlStart.GetYear, _controlStart.GetMonth, 1)
            _fecha = DateAdd(DateInterval.Month, _meses, _fecha)
            _controlEnd.SetMonth = _fecha.Month
            _controlEnd.SetYear = _fecha.Year
        End If
    End Sub

    ''' <summary>
    ''' valida que la fecha final del periodo no sea menor a la fecha inicial del perido
    ''' </summary>
    ''' <param name="_controlStart"></param>
    ''' <param name="_controlEnd"></param>
    ''' <param name="ajustarStart"></param>
    Private Sub ValidarRango(ByVal _controlStart As Controls.CtrDateNavigator, ByVal _controlEnd As Controls.CtrDateNavigator, ByVal ajustarStart As Boolean)
        If _controlStart.GetYear IsNot Nothing AndAlso _controlStart.GetMonth IsNot Nothing Then
            Dim _fechaIni As Date = New Date(_controlStart.GetYear, _controlStart.GetMonth, 1)
            If _controlEnd.GetYear IsNot Nothing AndAlso _controlEnd.GetMonth IsNot Nothing Then
                Dim _fechafin As Date = New Date(_controlEnd.GetYear, _controlEnd.GetMonth, 1)
                If DateDiff(DateInterval.DayOfYear, _fechaIni, _fechafin) < 0 Then
                    If ajustarStart Then
                        _controlStart.SetMonth = _controlEnd.GetMonth
                        _controlStart.SetYear = _controlEnd.GetYear
                    Else
                        _controlEnd.SetMonth = _controlStart.GetMonth
                        _controlEnd.SetYear = _controlStart.GetYear
                    End If
                End If
                AjustarComparativoFinal()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Asigna el periodo final del comparativo teniendo en cuenta los meses del periodo inicial a partir del perido inicial del comparativo
    ''' </summary>
    Private Sub AjustarComparativoFinal()
        If INDCdnDateStartComp.GetYear IsNot Nothing AndAlso INDCdnDateStartComp.GetMonth IsNot Nothing Then
            If INDCdnDateStart.GetYear IsNot Nothing AndAlso INDCdnDateStart.GetMonth IsNot Nothing AndAlso INDCdnDateEnd.GetYear IsNot Nothing AndAlso INDCdnDateEnd.GetMonth IsNot Nothing Then
                AjustarControl(INDCdnDateStartComp, INDCdnDateEndComp, DateDiff(DateInterval.Month, New Date(INDCdnDateStart.GetYear, INDCdnDateStart.GetMonth, 1), New Date(INDCdnDateEnd.GetYear, INDCdnDateEnd.GetMonth, 1)))
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del evento INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateStart_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStart.OnChangeDate
        'si el periodo es mensual
        If INDGlePeriod.EditValue = 1 Then
            ''cuando el usuario cambie la fecha inicial si la fecha inicial es menor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
            'If INDCdnDateStart.GetYear <= Year(INDLastClosingDate) Then
            '    INDCdnDateStart.SetYear = Year(INDLastClosingDate)
            '    If INDCdnDateStart.GetMonth < Month(INDLastClosingDate) Then
            '        INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
            '    End If
            'End If
            ''cuando el usuario cambie la fecha inicial si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
            'If INDCdnDateEnd.GetYear <= INDCdnDateStart.GetYear Then
            '    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            '    If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
            '        INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            '    End If
            'End If
            ''cuando el usuario cambie la fecha incial si la fecha final es mayor que la fecha inicial + 1 año  hacer la fecha final igual que la fecha inicial + 1 año
            'If INDCdnDateStart.GetMonth = 12 Then
            '    If INDCdnDateEnd.GetYear > INDCdnDateStart.GetYear + 1 Then
            '        INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear + 1
            '        INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            '    End If
            'ElseIf INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear Then
            '    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            '    If INDCdnDateEnd.GetMonth <= 1 Then
            '        INDCdnDateEnd.SetMonth = 12
            '    End If
            'End If
            INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            'si el periodo es trimestral
        ElseIf INDGlePeriod.EditValue = 2 Then
            'If INDCdnDateStart.GetMonth > 10 Then
            '    INDCdnDateStart.SetMonth = 10
            '    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            'End If
            'INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth + 2
            'INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            AjustarControl(INDCdnDateStart, INDCdnDateEnd, 2)
        ElseIf INDGlePeriod.EditValue = 3 Then
            AjustarControl(INDCdnDateStart, INDCdnDateEnd, 11)
            'comparativo
        ElseIf INDGlePeriod.EditValue = 4 Then
            ValidarRango(INDCdnDateStart, INDCdnDateEnd, True)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del evento INDCdnDateStartComp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateStartComp_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStartComp.OnChangeDate
        AjustarComparativoFinal()
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del control INDCdnDateEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateEnd_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateEnd.OnChangeDate
        ''si el periodo es mensual
        'If INDGlePeriod.EditValue = 1 Then
        '    'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
        '    If INDCdnDateEnd.GetYear <= INDCdnDateStart.GetYear Then
        '        INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
        '        If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
        '            INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
        '        End If
        '    End If
        '    'cuando el usuario cambie la fecha final si la fecha final es mayor que la fecha inicial + 1 año  hacer la fecha final igual que la fecha inicial + 1 año
        '    If INDCdnDateStart.GetMonth = 12 Then
        '        If INDCdnDateEnd.GetYear > INDCdnDateStart.GetYear + 1 Then
        '            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear + 1
        '            INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
        '        End If
        '    ElseIf INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear Then
        '        INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
        '        If INDCdnDateEnd.GetMonth <= 1 Then
        '            INDCdnDateEnd.SetMonth = 12
        '        End If
        '    End If
        'End If
        If INDGlePeriod.EditValue = 4 Then
            ValidarRango(INDCdnDateStart, INDCdnDateEnd, False)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento Click del control INDSbGenerateReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        'Dim reporte As New rptStatusEfecty()
        Dim _mensaje As String = String.Empty
        'AsyncLoader(True)
        'fechas
        Dim INDDateStart As Date = New Date(INDCdnDateStart.GetYear, INDCdnDateStart.GetMonth, 1)
        Dim INDDateEnd As Date = DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Month, 1, New Date(INDCdnDateEnd.GetYear, INDCdnDateEnd.GetMonth, 1)))
        If INDGlePeriod.EditValue = 4 Then
            Dim INDDateStartComp As Date = New Date(INDCdnDateStartComp.GetYear, INDCdnDateStartComp.GetMonth, 1)
            Dim INDDateEndComp As Date = DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Month, 1, New Date(INDCdnDateEndComp.GetYear, INDCdnDateEndComp.GetMonth, 1)))
            If INDDateStart < INDDateEndComp AndAlso INDDateEnd > INDDateStartComp Then
                _mensaje = "Ajuste los periodos para que no se crucen"
            End If
        End If

        If String.IsNullOrEmpty(_mensaje) Then
            AsyncLoader(True)
            Dim parametros As String = RecuperarParametros()
            Dim reporte As New rptGLCashFlowStatus()
            reporte.ParametrosReporte = New Object() {parametros}
            INDDvReportView.DocumentSource = reporte
            Await reporte.CargarDataSource()
            If reporte.EncontroInformacion Then
                reporte.CreateDocument(True)
                Me.INDLcHomeBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvReportView.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDCdnDateStart.Focus()
            End If
            AsyncLoader(False)
        Else
            Mensaje(EeventViewerImages.Advertencia) = _mensaje
            Me.INDCdnDateStartComp.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Crea una cadena XML que contiene varios parámetros, incluyendo fechas de inicio y fin, opciones y un indicador de comparativo
    ''' </summary>
    ''' <returns></returns>
    Private Function RecuperarParametros() As String
        Dim parametros As String = String.Format("<{0}>{1:dd/MM/yyyy hh:mm:ss}</{0}>", "InitialDate", New Date(INDCdnDateStart.GetYear, INDCdnDateStart.GetMonth, 1))
        parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "EndDate", DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Month, 1, New Date(INDCdnDateEnd.GetYear, INDCdnDateEnd.GetMonth, 1))))
        parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Opcion", 3)
        If INDGlePeriod.EditValue = 4 Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Comparativo", "1")
            parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "InitialDateComp", New Date(INDCdnDateStartComp.GetYear, INDCdnDateStartComp.GetMonth, 1))
            parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "EndDateComp", DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Month, 1, New Date(INDCdnDateEndComp.GetYear, INDCdnDateEndComp.GetMonth, 1))))
        Else
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Comparativo", "0")
        End If
        parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
        Return parametros
    End Function

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcHomeBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportStatusEfecty_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGlePeriod.Properties.DataSource = FillingTypePeriod

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGlePeriod.EditValue = 1

        'asignar la fecha del ultimo cierre a los controles date
        Using msearch As New MBusqueda
            If msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate) = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
            End If
        End Using

        INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
        INDCdnDateStart.SetYear = Year(INDLastClosingDate)
        INDCdnDateEnd.SetMonth = Month(INDLastClosingDate)
        INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
        INDCdnDateStartComp.SetMonth = Month(INDLastClosingDate)
        INDCdnDateStartComp.SetYear = Year(INDLastClosingDate)
        INDCdnDateEndComp.SetMonth = Month(INDLastClosingDate)
        INDCdnDateEndComp.SetYear = Year(INDLastClosingDate)
    End Sub

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region
End Class