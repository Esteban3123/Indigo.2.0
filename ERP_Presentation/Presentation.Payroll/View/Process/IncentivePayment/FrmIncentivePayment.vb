'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-01-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.XtraEditors
Imports Presentation.Payroll.MVP
Imports Presentation.Common.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils.Drawing
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Controls
Imports DevExpress.Utils.Design.DesignTimeTools
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region
Public Class FrmIncentivePayment
    Implements IIncentivePayment

#Region "Field and Globals"

    ''' <summary>
    ''' Variable para manejar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PIncentivePayment

    ''' <summary>
    ''' Variable para manejar el modelo del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MIncentivePayment

    ''' <summary>
    ''' Listado de los grupos checkeados para mandar a liquidar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listGroupsChecks As List(Of Group)

    ''' <summary>
    ''' Variable para almacenar la liquidación de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListActionResultUnemployedLiquidation As List(Of ActionMessageResult(Of UnemployedLiquidation))

    ''' <summary>
    ''' Variable para controlar el empleado que se liquidara parcial
    ''' </summary>
    ''' <remarks></remarks>
    Dim Employee As Employee

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

    Dim StringIdGroup As String = ""

    Dim NumberIncentivePayment As Integer = 0

    ''' <summary>
    ''' Variable para almacenar los detalles de la preliquidacion de primas
    ''' </summary>
    Dim ListIncentivePayment As New List(Of IncentivePayment)

    Dim PeriodInitialDate As Date
    Dim PeriodEndDate As Date

    Dim liquitadionEmployee As List(Of IncentivePayment)
    ''' <summary>
    ''' Obtiene la mondeda oficial
    ''' </summary>
    Private _CurrencyAbbreviation As String

#End Region

#Region "Properties"

    ' ''' <summary>
    ' ''' establece el datasource de las compañias
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <remarks></remarks>
    'Public WriteOnly Property datasourceCompany As DevExpress.Xpo.XPInstantFeedbackSource Implements IIncentivePayment.datasourceCompany
    '    Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
    '        INDSleCompany.Properties.DataSource = value
    '    End Set
    'End Property


    ''' <summary>
    ''' Establece los grupos filtrados por empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property datasourceGroups As List(Of Domain.Payroll.Entities.Group) Implements IIncentivePayment.datasourceGroups
        Set(value As List(Of Domain.Payroll.Entities.Group))
            value = value.Where(Function(x) x.PayrollParameter.MaxPremiumByYear > 0).ToList()
            INDgcGroups.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el control del periodo o año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PeriodControl As ComboBoxEdit Implements IIncentivePayment.PeriodControl
        Get
            Return INDCbeYear
        End Get
    End Property


    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' <summary>
    ''' Esta propiedad se utiliza para consultar la moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyAbbreviation As String
        Get
            Return _CurrencyAbbreviation
        End Get
        Set(value As String)
            _CurrencyAbbreviation = value
        End Set
    End Property

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        model = Nothing
        listGroupsChecks = Nothing
        ListActionResultUnemployedLiquidation = Nothing
        Employee = Nothing
        ci = Nothing
        dtfi = Nothing
        StringIdGroup = Nothing
        NumberIncentivePayment = Nothing
        ListIncentivePayment = New List(Of IncentivePayment)
        PeriodInitialDate = Nothing
        PeriodEndDate = Nothing
        liquitadionEmployee = Nothing
    End Sub

    Private Sub FrmIncentivePayment_Load(sender As Object, e As EventArgs) Handles Me.Load
        presenter = New PIncentivePayment(Me)
        presenter.initialize()
        presenter.Load_Group()
        CurrencyAbbreviation = presenter.LoadPayrollSettings.CurrencyId.Abbreviation
    End Sub
    Private Sub FrmIncentivePayment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If INDCbeSemestre.Enabled Then
            INDCbeSemestre.Focus()
        End If
    End Sub

#End Region

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        AsyncLoader(False)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.RibbonPageRejillas.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

    ''' <summary>
    ''' Click liquidar del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickLiquidar() Handles BarraBotones.ClickLiquidar
        Await IncentivePaymentLiquidation()
    End Sub

    ''' <summary>
    ''' Click consultar liquidacion del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConsultLiquidar() Handles BarraBotones.ClickConsultLiquidar
        Await SearchIncentivePayment()
    End Sub

    Private Async Sub BarraBotones_ClickConfimarLiquidacion() Handles BarraBotones.ClickConfirmLiquidation
        If MessageIndigo.Show(String.Format(obtenerRecurso(ConfirmarPrimas, Eform.LiquidacionPrimas)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Await ConfirmLiquidation()
        End If
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, 2, 0, listGroupsChecks, PeriodInitialDate, PeriodEndDate, Me.BarraBotones.OperatingUnit)
    End Sub

    ''' <summary>
    ''' Barra de botones Click Generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
        GenerateFile()
    End Sub
#End Region
#Region "ICRUD BASE"
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        AsyncLoader(False)
        ListIncentivePayment = New List(Of IncentivePayment)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.RibbonPageRejillas.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
        INDCbeSemestre.EditValue = Nothing
        INDCbeYear.EditValue = Nothing
        INDCbePagoNomina.EditValue = Nothing
        INDlyItemControlNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemGCResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemGCGroups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        CargarLiquidacionesPrevias()
    End Sub

    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de liquidar primas
    ''' </summary>
    Dim progress As CtrProgress

    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    Dim totalItems As Integer

    ''' <summary>
    ''' items procesados
    ''' </summary>
    Dim totalProcessedItems As Integer

    ''' <summary>
    ''' items que se van a enviar en cada proceso (lotes de 50 como en nómina)
    ''' </summary>
    Dim itemsSend As Integer = 50

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesados
    ''' </summary>
    Private Function getInfoProgress() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function


    Private Async Function IncentivePaymentLiquidation() As Task

        Try
            Dim PaymentType As Char
            Dim errors As New StringBuilder()

            INDCbeYear.Focus()
            Using model = New MIncentivePayment(MIncentivePayment.TAG)

                If StringIdGroup = "" Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionadosPrimas, Eform.LiquidacionPrimas)
                    Return
                End If

                If INDCbeYear.EditValue.ToString() = "" Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayLiquidacionesGeneradas, Eform.LiquidacionPrimas)
                    Return
                End If

                If INDCbeSemestre.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHaySeleccionadoSemestrePrimas, Eform.LiquidacionPrimas)
                    Return
                End If

                If INDCbePagoNomina.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se ha Seleccionado el Tipo de Pago"
                    Return
                End If


                If INDCbePagoNomina.EditValue.ToString = "SI" Then
                    PaymentType = "1"
                Else
                    PaymentType = "2"
                End If

                AsyncLoader(True)
                ' ===== OBTENER COUNT DE EMPLEADOS DESDE SERVICIO =====
                Dim totalEmployees = Await model.GetEmployeeCountForIncentivePaymentAsync(StringIdGroup, INDCbeSemestre.EditValue.ToString())

                If totalEmployees = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No existen empleados para liquidar primas"
                    AsyncLoader(False)
                    Return
                End If

                totalProcessedItems = 0
                totalItems = totalEmployees

                ''Barra de progreso
                progress = New CtrProgress
                progress.SetInfoFunction(AddressOf getInfoProgress)
                progress.PrintInfo()
                progress.Dock = DockStyle.Fill
                progress.SetTitle = "Empleados con Primas Liquidadas"
                progress.Visible = True

                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))


                Dim allIncentivePayments As New List(Of IncentivePayment)

                ' ===== LOOP POR LOTES CON PAGINACIÓN =====
                For offset As Integer = 0 To totalEmployees - 1 Step itemsSend

                    Dim currentBatchSize = Math.Min(itemsSend, totalEmployees - offset)
                    Dim result = Await model.CalculateIncentivePaymentAsync(StringIdGroup, INDCbeSemestre.EditValue.ToString(), NumberIncentivePayment, INDCbeYear.EditValue, PaymentType, "", 0, offset, itemsSend)

                    If result.StateResult Then
                        If result.ObjectEmbbeded IsNot Nothing Then
                            allIncentivePayments.AddRange(result.ObjectEmbbeded)
                            totalProcessedItems += result.ObjectEmbbeded.Count
                        End If
                    Else
                        errors.AppendLine($"Error en lote {offset + 1}-{offset + currentBatchSize}: {result.Message}")
                        ' Actualizar contador aunque haya error
                        totalProcessedItems += currentBatchSize
                    End If

                    progress.PrintInfo()
                Next

                AsyncLoader(False)
                progress.Visible = False
                AdditionalControlPanel.Controls.Clear()

                If allIncentivePayments.Count > 0 Then
                    ListIncentivePayment = New List(Of IncentivePayment)(allIncentivePayments)

                    Dim resultWrapper As New ActionMessageResult(Of List(Of IncentivePayment)) With {
                        .StateResult = True,
                        .ObjectEmbbeded = allIncentivePayments,
                        .MessageResult = New List(Of MessageResult)
                    }
                    If allIncentivePayments.All(Function(x) x.RegisterStatus = "1") Then ''Primas no confirmadas
                        resultWrapper.MessageResult.Add(New MessageResult("002"))
                    End If
                    ResultLiquidation(resultWrapper)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se generaron liquidaciones de primas"
                End If

                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                End If
            End Using
        Catch ex As Exception
            If progress IsNot Nothing Then
                progress.Visible = False
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            End If
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try

    End Function

    ''' <summary>
    ''' Resultado de la Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResultLiquidation(ByVal result As ActionMessageResult(Of List(Of IncentivePayment)))

        Dim MessageCode As String = ""
        Dim ListIncentivePaymentProcess = New List(Of IncentivePayment)

        For i As Integer = 0 To result.MessageResult.Count() - 1
            MessageCode = result.MessageResult.Item(i).CodeMessage
        Next

        If result.StateResult = True Then


            If MessageCode = "001" Then ' Se liquidaron correctamente y FUE confirmada
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(LiquidacionPrimasConfirmada, Eform.LiquidacionPrimas)
            ElseIf MessageCode = "002" Then 'Se liquidaron correctamente pero no fue confirmada
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(LiquidacionPrimasNoConfirmada, Eform.LiquidacionPrimas)
            End If

            ListIncentivePayment.RemoveAll(Function(x) x.GroupId.ToString() = StringIdGroup AndAlso x.Period = INDCbeSemestre.EditValue.ToString())
            ListIncentivePayment.AddRange(result.ObjectEmbbeded.ToList())
            ListIncentivePaymentProcess = ListIncentivePayment.Where(Function(x) x.GroupId.ToString() = StringIdGroup AndAlso x.Period = INDCbeSemestre.EditValue.ToString()).ToList()

            OpenFormAddAssets(False, ListIncentivePaymentProcess)
            INDgcLiquidationResult.DataSource = ListIncentivePayment.Where(Function(x) x.GroupId.ToString() = StringIdGroup AndAlso x.Period = INDCbeSemestre.EditValue.ToString()).ToList()
        Else
            If MessageCode = "-001" Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(LiquidacionPrimasYaConfirmada, Eform.LiquidacionPrimas)
            ElseIf MessageCode = "-003" Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayDatosAcumuladosLiquidacion, Eform.LiquidacionPrimas)
            ElseIf MessageCode = "-004" Then
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(IBCPrimasVacio, Eform.LiquidacionPrimas)
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoGeneroPrimas, Eform.LiquidacionPrimas)
            End If
        End If

    End Sub

    Private Async Function SearchIncentivePayment() As Task

        Using model = New MIncentivePayment(MIncentivePayment.TAG)
            AsyncLoader(True)
            If StringIdGroup = "" Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionadosPrimas, Eform.LiquidacionPrimas)
                AsyncLoader(False)
                Return
            End If

            If INDCbeYear.EditValue.ToString() = "" Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayLiquidacionesGeneradas, Eform.LiquidacionPrimas)
                AsyncLoader(False)
                Return
            End If

            If INDCbeSemestre.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHaySeleccionadoSemestrePrimas, Eform.LiquidacionPrimas)
                AsyncLoader(False)
                Return
            End If

            Dim result = Await model.GetIncentivePaymentByDatesGroupId(StringIdGroup, NumberIncentivePayment, INDCbeSemestre.EditValue.ToString(), INDCbeYear.EditValue)
            If result.ObjectEmbbeded Is Nothing Then
                Dim groupSelected = CType(INDgcGroups.DataSource, List(Of Group)).FirstOrDefault(Function(x) x.Apply = True)
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron primas liquidadas con el grupo  " & groupSelected.Code + " - " + groupSelected.Name & " en el periodo " &
                INDCbeSemestre.EditValue.ToString() &
                " del año " &
                INDCbeYear.EditValue.ToString()
                AsyncLoader(False)
                Return
            End If
            ListIncentivePayment = result.ObjectEmbbeded
            AsyncLoader(False)
            OpenFormAddAssets(True, result.ObjectEmbbeded)
        End Using

    End Function

    ''' <summary>
    ''' Confirmar Liquidación de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ConfirmLiquidation() As Task

        Dim PaymentType As Char

        Using Model As New MIncentivePayment(MIncentivePayment.TAG)
            Dim liquidationProcessSave = New List(Of IncentivePayment)
            liquidationProcessSave = ListIncentivePayment.Where(Function(x) x.GroupId.ToString() = StringIdGroup AndAlso x.Period = INDCbeSemestre.EditValue.ToString()).ToList()
            Exit Function

            If liquidationProcessSave IsNot Nothing Then

                If INDCbePagoNomina.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHaSeleccionadoTipoPagoPrimas, Eform.LiquidacionPrimas)
                    Return
                End If

                If INDCbePagoNomina.EditValue.ToString() = "" Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHaSeleccionadoTipoPagoPrimas, Eform.LiquidacionPrimas)
                    Return
                End If

                If INDCbePagoNomina.EditValue.ToString = "SI" Then
                    PaymentType = "1"
                Else
                    PaymentType = "2"
                End If

                For i As Integer = 0 To liquidationProcessSave.Count() - 1
                    liquidationProcessSave.Item(i).PaymentType = PaymentType
                Next

                AsyncLoader(True)
                Dim incentivePaymentConfirm = Await Model.SaveIncentivePaymentAsync(liquidationProcessSave)
                AsyncLoader(False)
                If incentivePaymentConfirm.StateResult = True Then

                    Mensaje(EeventViewerImages.Informacion) = incentivePaymentConfirm.Message
                    CleanControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = incentivePaymentConfirm.Message
                    Return
                End If
            Else
                ' No se generó liquidación
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(PrimeroNominaBorrador, Eform.LiquidacionNomina)
            End If
        End Using

    End Function

    ''' <summary>
    ''' Click Atrás del Control de Navegación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        'INDlyItemPagoNomina.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemControlNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemGCResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemGCGroups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
    End Sub


    Private Sub INDgvGroups_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles INDgvGroups.CellValueChanged

        Dim list_group As List(Of Group) = INDgcGroups.DataSource
        Dim list_group_chek As List(Of Group)

        StringIdGroup = ""

        INDCbeSemestre.Properties.Items.Clear()


        list_group_chek = list_group.FindAll(Function(x) x IsNot Nothing AndAlso x.Apply = True)

        For i As Integer = 0 To (list_group_chek.Count() - 1)

            If list_group_chek.Count() = 1 Then
                StringIdGroup = list_group_chek.Item(i).Id.ToString()
                If list_group_chek.Item(0).PayrollParameter.MaxPremiumByYear = 1 Then
                    INDCbeSemestre.EditValue = 1
                Else
                    INDCbeSemestre.Properties.Items.Add(1)
                    INDCbeSemestre.Properties.Items.Add(2)
                End If
            Else
                list_group_chek.Item(1).Apply = False
                Mensaje(EeventViewerImages.MensajeError) = "Sólo puede seleccionar un grupo para su liquidación"
                Exit Sub
            End If
        Next

    End Sub

    ''' <summary>
    ''' Metodo que abre el form popup donde se escoge los activos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddAssets(FlagSearch As Boolean, incentivePayment As List(Of IncentivePayment))
        Using formulario As New FrmDetailIncentivePayment
            Me.Cursor = ChangeCursorIndigo()
            'AddHandler formulario.AddFixedAssetActiveOutputDetailEventArgs, AddressOf ReturnAddEventArgs
            formulario.ListIncentivePayment = incentivePayment
            formulario.PaidType = ConvertTextToPaymentType(INDCbePagoNomina.EditValue.ToString)
            formulario.Size = New System.Drawing.Size(1200, 696)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.FlagSearch = FlagSearch
            formulario.CurrencyAbbreviation = CurrencyAbbreviation
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Sub GenerateFile()
        Using Form1 As New FrmGenerateBankFileIncentivePayment
            Dim transparent As New FrmTransparent(Form1, False)
            With Form1
                transparent.ShowDialog()
                .Dispose()
            End With
        End Using
    End Sub

    Async Function CargarLiquidacionesPrevias() As Task
        Try
            Using Model As New MIncentivePayment(Me.Tag)
                AsyncLoader(True)
                ListIncentivePayment = Await Model.GetHeadIncentivePaymentAsync(INDCbeYear.EditValue, INDCbeSemestre.EditValue)
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Convierte el valor numérico de PaymentType a texto para el control
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ConvertPaymentTypeToText(paymentType As String) As String
        If paymentType = "1" Then
            Return "SI"
        ElseIf paymentType = "2" Then
            Return "NO"
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Convierte el texto del control a valor numérico de PaymentType
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ConvertTextToPaymentType(textValue As String) As String
        If textValue = "SI" Then
            Return "1"
        ElseIf textValue = "NO" Then
            Return "2"
        Else
            Return ""
        End If
    End Function
#End Region

#Region "EditValueChanged"

    Private Async Sub INDCbeSemestre_EditValueChanged(sender As Object, e As EventArgs) Handles INDCbeSemestre.EditValueChanged
        Await CargarLiquidacionesPrevias()
    End Sub
#End Region

    Private Async Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit1.Click
        Try
            Dim GroupToShow = INDgvGroups.GetFocusedRow()
            StringIdGroup = GroupToShow.Id
            Using Model As New MIncentivePayment(Me.Tag)
                Dim liquidationProcessView = New List(Of IncentivePayment)
                AsyncLoader(True)
                liquidationProcessView = Await Model.GetDetailIncentivePaymentAsync(GroupToShow.Id, INDCbeYear.EditValue, INDCbeSemestre.EditValue)
                AsyncLoader(False)
                If liquidationProcessView IsNot Nothing And liquidationProcessView.Count > 0 Then
                    INDCbePagoNomina.EditValue = ConvertPaymentTypeToText(liquidationProcessView.Item(0).PaymentType)
                    OpenFormAddAssets(False, liquidationProcessView)
                    INDgcLiquidationResult.DataSource = liquidationProcessView
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

#Region "CustomDrawCell"
    Private Sub INDgvGroups_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDgvGroups.CustomDrawCell
        If IndColVisualizarLiquidacion.Name = e.Column.Name Then
            If ListIncentivePayment IsNot Nothing Then
                Dim group = CType(INDgvGroups.GetRow(e.RowHandle), Group)
                If group IsNot Nothing Then
                    Dim LiquidationShow = ListIncentivePayment.FindAll(Function(x) x.GroupId = group.Id AndAlso x.Period = INDCbeSemestre.EditValue).Count()

                    If LiquidationShow > 0 Then
                        e.DisplayText = "Visualizar"
                    Else
                        e.DisplayText = ""
                    End If
                Else
                    e.DisplayText = ""
                End If
            End If
        End If
    End Sub
#End Region


End Class