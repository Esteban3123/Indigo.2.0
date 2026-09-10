#Region "Imports"
Imports Domain.Payroll.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Payroll.MVP
Imports System.Windows.Forms
Imports Domain.Base.Entities

#End Region
Public Class FrmUnemployementDetail

    Public ListUnemployement As List(Of UnemployedLiquidation)
    Public PaidType As String
    Dim UnemployedLiquidation As UnemployedLiquidation
    Public FlagSearch As Boolean = False
    Public ListDetailLiquidation As New List(Of LiquidationDetail)
    Public ListEmployeeDetailLiquidation As New List(Of LiquidationDetail)
    Dim Year As Integer
    Public FlagParcial As Integer = False
    Public SanctionDays As Boolean = False
    Public UnemployedLiquidationDate As Date
    Public Employee As Employee
    Public AutorizationDatte As Date = Nothing
    Public RetirementReason As String = Nothing
    Public ResolutionNumber As String = Nothing
    Public listGroupsChecks As List(Of Group)


    Private Async Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        Dim InitialContractNumber As Integer = CType(Record, UnemployedLiquidation).ContractNumber
        Dim CalculationType As Integer = CType(Record, UnemployedLiquidation).CalculationType
        Year = CType(Record, UnemployedLiquidation).Year
        UnemployedLiquidation = (From e In ListUnemployement Where e.ContractId = CType(Record, UnemployedLiquidation).ContractId).FirstOrDefault()
        Await LoadLiquidationDetail(InitialContractNumber, CalculationType, Year)
        AssignValuesLiquidation(UnemployedLiquidation)
    End Sub

    Private Async Function LoadLiquidationDetail(InitialContractNumber As Integer, CalculationType As Integer, Year As Integer) As Task(Of LiquidationDetail)

        If ListDetailLiquidation.Any(Function(x) x.Liquidation.InitialContractNumber = InitialContractNumber) = False Then
            Using model = New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                AsyncLoader(True)

                If CalculationType = 1 Then
                    ListDetailLiquidation.AddRange(Await model.GetLiquidationDetailByContractIdConceptAffectUnemployement(Year, InitialContractNumber, True))
                Else
                    ListDetailLiquidation.AddRange(Await model.GetLiquidationDetailByContractIdConceptClass(Year, InitialContractNumber, "008"))
                End If

                AsyncLoader(False)
            End Using

        End If

        ListEmployeeDetailLiquidation = ListDetailLiquidation.Where(Function(x) x.Liquidation.InitialContractNumber = InitialContractNumber).ToList()
    End Function

    ''' <summary>
    ''' Función para Asignar los Valores de la Liquidación
    ''' </summary>
    ''' <param name="LiquidationToShow"></param>
    ''' <remarks></remarks>
    Sub AssignValuesLiquidation(ByVal UnemployedLiquidation As UnemployedLiquidation)

        If UnemployedLiquidation IsNot Nothing Then
            With UnemployedLiquidation
                INDtxtGroupName.EditValue = .GroupName
                INDTxtNameEmployee.EditValue = .FullNameEmployee
                INDTxtFundName.EditValue = .FundName
                INDTxtNumberContract.EditValue = .ContractNumber
                INDTxtPosition.EditValue = .PositionName
                INDTxtWorkedDays.EditValue = .WorkedTotalDays
                INDTxtSanctionDays.EditValue = .SanctionTotalDays
                INDTxtBasicSalary.EditValue = .BasicSalary
                INDTxtTotalPaid.EditValue = .TotalUnemployed
                INDtxtGroupName.EditValue = .GroupName
                INDTxtContractDate.EditValue = .JobBondingDate

                INDGcLiquidationDetail.DataSource = UnemployedLiquidation.UnemployedConcept.ToList()
                INDGcAccumulatedDetail.DataSource = ListEmployeeDetailLiquidation

            End With
        End If

    End Sub

    Private Sub FormatGridColums()
        Dim currencyFS = "c"
        GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn2.DisplayFormat.FormatString = currencyFS
        GridColumn3.DisplayFormat.FormatString = currencyFS
    End Sub

    Private Async Sub FrmUnemployementDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ListUnemployement IsNot Nothing AndAlso ListUnemployement.Count > 0 Then

            If FlagSearch = False Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If

            Me.BarraBotones.FilterDataSource = ListUnemployement
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Identificación -  Nombre", .FieldName = "FullNameEmployee"}}.ToList()

            If ListUnemployement.Count = 1 Then
                'Dim InitialContractNumber As Integer = CType(Record, UnemployedLiquidation).ContractNumber
                'Dim CalculationType As Integer = CType(Record, UnemployedLiquidation).CalculationType
                'Year = CType(Record, UnemployedLiquidation).Year
                Await LoadLiquidationDetail(ListUnemployement.Item(0).ContractNumber, ListUnemployement.Item(0).CalculationType, ListUnemployement.Item(0).Year)
                AssignValuesLiquidation(ListUnemployement.Item(0))
            End If
        End If
        FormatGridColums()
    End Sub

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
#End Region

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr("591"))
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
        If MessageIndigo.Show(String.Format(obtenerRecurso(ConfirmarCesantias, Eform.LiquidacionCesantias)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Await UnemployeeLiquidate()
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

#Region "Funciones"

    Async Function UnemployeeLiquidate() As Task
        ListUnemployedLiquidation = New List(Of UnemployedLiquidation)
        Try
            ListActionResultUnemployedLiquidation = Nothing
            If FlagParcial = 0 Then ' liq anual
                Using model = New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                    AsyncLoader(True)
                    Dim ObjIncentivePayment = ListUnemployement.FirstOrDefault()

                    Dim ListUnemployementToSend = New List(Of UnemployedLiquidation)
                    ListUnemployementToSend.Add(New UnemployedLiquidation With {
                            .GroupId = ObjIncentivePayment.GroupId,
                            .UnemployedInitialDate = ObjIncentivePayment.UnemployedInitialDate,
                            .UnemployedEndingDate = ObjIncentivePayment.UnemployedEndingDate
                                                   })

                    Dim result = Await model.ConfirmUnemployment(ListUnemployementToSend)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        Me.Close()
                    Else
                        '    'mensaje de que no se pudo confirmar las cesantias
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Dim NewListMessageResult As New List(Of MessageResult)
                            For Each message In result.MessageResult
                                NewListMessageResult.Add(New MessageResult(999, message))
                            Next
                            TmpListEmployeeMessage = MessageLiquidation(NewListMessageResult)
                            Dim frmMessage As FrmLiquidationMessage = New FrmLiquidationMessage()
                            frmMessage.StartPosition = FormStartPosition.CenterScreen
                            frmMessage.INDGcMessage.DataSource = TmpListEmployeeMessage
                            Dim frmTransparent As New FrmTransparent(frmMessage, False)
                            frmTransparent.ShowDialog()
                        End If

                    End If
                End Using

            ElseIf FlagParcial = 1 Then ' liq parcial

                Using model = New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                    AsyncLoader(True)
                    Dim result = Await model.UnemployedLiquidationAsync(Employee, Nothing, New Date(DateTime.Now().Year, 1, 1), UnemployedLiquidationDate, True, SanctionDays, AutorizationDatte, RetirementReason, ResolutionNumber)
                    AsyncLoader(False)

                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(CesantiasConfirmadasCorrectamente, Eform.LiquidacionCesantias)
                        Me.Close()
                    Else
                        'mensaje de que no se pudo confirmar las cesantias
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(CesantiasNoSePudieronConfirmar, Eform.LiquidacionCesantias)

                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            TmpListEmployeeMessage = MessageLiquidation(result.MessageResult)
                            Dim frmMessage As FrmLiquidationMessage = New FrmLiquidationMessage()
                            frmMessage.StartPosition = FormStartPosition.CenterScreen
                            frmMessage.INDGcMessage.DataSource = TmpListEmployeeMessage
                            Dim frmTransparent As New FrmTransparent(frmMessage, False)
                            frmTransparent.ShowDialog()
                        End If

                    End If
                End Using
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Function

    Private Sub RepositoryItemPopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Click
        Dim FormulasToShow As UnemployedConcept = GridView1.GetFocusedRow()
        AssignValues(FormulasToShow)
    End Sub

    Private Sub AssignValues(ByVal FormulasToShow As UnemployedConcept)
        INDteUsedFormula.Text = String.Empty
        INDteReplaceFormula.Text = String.Empty
        INDteResultFormula.Text = String.Empty
        INDteUsedFormula.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormula.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormula.Text = IIf(FormulasToShow.AccruedValue > 0, FormulasToShow.AccruedValue, FormulasToShow.DeductedValue)
    End Sub

    Private Sub FrmUnemployementDetail_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        ListUnemployement = Nothing
        UnemployedLiquidation = Nothing
        ListDetailLiquidation = Nothing
    End Sub

    Private Function MessageLiquidation(ByVal ListMessageResult As List(Of MessageResult)) As List(Of EmployeeMessage)

        Dim ListEmployeeMessage As New List(Of EmployeeMessage)

        For Each ObjMessage As MessageResult In ListMessageResult
            Dim ObjEmployeeMessage As New EmployeeMessage

            ObjEmployeeMessage.CodeError = ObjMessage.CodeMessage

            If ObjMessage.CodeMessage = "001: Empleado" Or ObjMessage.CodeMessage = "999: Parámetros de Nómina" Or ObjMessage.CodeMessage = "-999: Error" Then
                ObjEmployeeMessage.TypeMessage = "1"
            ElseIf ObjMessage.CodeMessage = "002: Cesantias" Or ObjMessage.CodeMessage = "003: Nomina" Then
                ObjEmployeeMessage.TypeMessage = "3"
            Else
                ObjEmployeeMessage.TypeMessage = "2"
            End If

            ObjEmployeeMessage.ErrorMessage = ObjMessage.Parameters(0)

            ListEmployeeMessage.Add(ObjEmployeeMessage)
        Next

        Return ListEmployeeMessage.GroupBy(Function(x) x.ErrorMessage).Select(Function(x) x.First).ToList()
    End Function
#End Region
End Class