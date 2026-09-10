
#Region "Imports"
Imports Domain.Payroll.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Payroll.MVP
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmDetailIncentivePayment
    Public ListIncentivePayment As List(Of IncentivePayment)
    Public PaidType As String
    Dim IncentivePayment As IncentivePayment

    Public FlagSearch As Boolean = False
    ''' <summary>
    ''' Obtie
    ''' </summary>
    Public CurrencyAbbrbiation As String

    Private Async Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        IncentivePayment = (From e In ListIncentivePayment Where e.ContractId = CType(Record, IncentivePayment).ContractId And e.GroupId = CType(Record, IncentivePayment).GroupId).FirstOrDefault()

        AssignValuesLiquidation(IncentivePayment)
    End Sub

    ''' <summary>b  
    ''' Función para Asignar los Valores de la Liquidación
    ''' </summary>
    ''' <param name="LiquidationToShow"></param>
    ''' <remarks></remarks>
    Sub AssignValuesLiquidation(ByVal IncentivePayment As IncentivePayment)
        If IncentivePayment IsNot Nothing Then
            BarraBotones.StatusRecord = IncentivePayment.RegisterStatus
            If IncentivePayment.RegisterStatus = 2 Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Else
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            End If
            With IncentivePayment
                INDTxtNameEmployee.EditValue = .FullNameEmployee
                INDTxtWorkedDays.EditValue = .PaidDays
                INDTxtSanctionDays.EditValue = .SanctionDays
                INDTxtBasicSalary.EditValue = .BasicSalary
                INDTxtTotalAccrued.EditValue = .TotalAccrued
                INDTxtTotalDeducted.EditValue = .TotalDeducted
                INDTxtTotalPaid.EditValue = .PaidValue
                INDTxtPosition.EditValue = If(.PositionName = Nothing, .Contract.Position.Name, .PositionName)
                INDtxtGroupName.EditValue = If(.GroupName = Nothing, .Group.Name, .GroupName)
                INDTxtContractDate.EditValue = .ContractInitialDate.ToShortDateString()
                INDTxtNumberContract.EditValue = If(.ContractNumber = Nothing, .Contract.InitialContractNumber, .ContractNumber)
                INDGcDetailIncentivePayment.DataSource = (From x In IncentivePayment.IncentivePaymentDetail Where x.ConceptType <> 3 Select x).ToList()
            End With
        End If

    End Sub

    Private Sub FrmDetailIncentivePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)
        GridColumn2 = Window.Utils.FormatGrid(GridColumn2, CurrencyAbbreviation)
        GridColumn3 = Window.Utils.FormatGrid(GridColumn3, CurrencyAbbreviation)
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True

        'se establece la cultura especifica Solo a los controles de texto que muestran valores con formato moneda
        If CurrencyAbbreviation IsNot Nothing AndAlso CurrencyAbbreviation IsNot String.Empty Then
            Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            INDteResultFormula.Properties.Mask.Culture = _culture
        End If

        If ListIncentivePayment IsNot Nothing And ListIncentivePayment.Count > 0 Then
            If FlagSearch = False Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If

            Me.BarraBotones.FilterDataSource = ListIncentivePayment
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Identificación -  Nombre", .FieldName = "FullNameEmployee"}}.ToList()

            If ListIncentivePayment.Count = 1 Then
                AssignValuesLiquidation(ListIncentivePayment.Item(0))
            End If
        End If

    End Sub


    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = 1, .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 2, .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Sub AssignValues(ByVal FormulasToShow As IncentivePaymentDetail)
        INDteUsedFormula.Text = String.Empty
        INDteReplaceFormula.Text = String.Empty
        INDteResultFormula.Text = String.Empty
        INDteUsedFormula.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormula.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormula.Text = IIf(FormulasToShow.AccruedValue > 0, FormulasToShow.AccruedValue, FormulasToShow.DeductedValue)
    End Sub

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr("597"))
        AsyncLoader(False)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)

    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer

        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo

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

    End Sub

    ''' <summary>
    ''' Click consultar liquidacion del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConsultLiquidar() Handles BarraBotones.ClickConsultLiquidar

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
        'GenerateFile()
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private _currencyAbbreviation As String
    Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Confirmar Liquidación de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ConfirmLiquidation() As Task

        Try
            Dim PaymentType As Char

            Using Model As New MIncentivePayment(MIncentivePayment.TAG)
                If ListIncentivePayment IsNot Nothing Then

                    If PaidType = "SI" Then
                        PaymentType = "1"
                    Else
                        PaymentType = "2"
                    End If

                    For i As Integer = 0 To ListIncentivePayment.Count() - 1
                        ListIncentivePayment.Item(i).PaymentType = PaymentType
                    Next
                    AsyncLoader(True)
                    Dim incentivePaymentConfirm = Await Model.SaveIncentivePaymentAsync(ListIncentivePayment)
                    AsyncLoader(False)
                    If Not incentivePaymentConfirm.StateResult Then
                        Using formulario As New FrmListErrors(incentivePaymentConfirm.MessageResult)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Join(" ", incentivePaymentConfirm.MessageResult)
                        Me.Close()
                    End If
                Else
                    ' No se generó liquidación
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(PrimeroNominaBorrador, Eform.LiquidacionNomina)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoGeneroPrimas, Eform.LiquidacionPrimas)
        End Try
    End Function

    Private Sub RepositoryItemPopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Click
        Dim FormulasToShow As IncentivePaymentDetail = GridView1.GetFocusedRow()
        AssignValues(FormulasToShow)
    End Sub
#End Region
End Class