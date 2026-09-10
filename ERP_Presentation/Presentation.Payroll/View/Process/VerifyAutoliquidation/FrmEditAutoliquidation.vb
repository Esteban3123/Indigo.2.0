Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP

Public Class FrmEditAutoliquidation


#Region "Globals"
    Public VerifyAutoliquidation As VerifyAutoliquidationFile

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
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

    Private Sub FrmEditAutoliquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        ' BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        If VerifyAutoliquidation IsNot Nothing Then
            HideControls()
            LoadControls()
        End If

        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)

    End Sub

    Private Sub HideControls()

        If VerifyAutoliquidation.IGE = "X" Then
            INDLcgInabilities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If VerifyAutoliquidation.VAC = "X" Then
            INDLcgVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If VerifyAutoliquidation.SLN = "X" Then
            INDLcgSanction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If VerifyAutoliquidation.IRL > 0 Then
            INDLcgIRL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If VerifyAutoliquidation.LMA = "X" Then
            INDLcgMaternity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

    End Sub
    Private Sub LoadControls()

        'Datos del Empleado
        INDTxtNitEmployee.EditValue = VerifyAutoliquidation.NitEmployee
        INDTxtNameEmployee.EditValue = VerifyAutoliquidation.NameEmployee
        INDMemoObservation.EditValue = VerifyAutoliquidation.Observations

        'Días
        INDSpHealthDays.EditValue = VerifyAutoliquidation.HealthDays
        INDSpPensionDays.EditValue = VerifyAutoliquidation.PensionDays
        INDSpProfessionalRiskDays.EditValue = VerifyAutoliquidation.ProfessionalRiskDays
        INDSpCompensationFundDays.EditValue = VerifyAutoliquidation.CompensationFundDays

        'Pensión
        INDTxtIBCPension.EditValue = VerifyAutoliquidation.IBCPension
        INDSpPensionPercentage.EditValue = VerifyAutoliquidation.RateContributionPension * 100
        INDTxtValuePension.EditValue = VerifyAutoliquidation.ValuePension

        'Salud
        INDTxtIBCHealth.EditValue = VerifyAutoliquidation.IBCHealth
        INDSpHealthPercentage.EditValue = VerifyAutoliquidation.RateContributionHealth * 100
        INDTxtValueHealth.EditValue = VerifyAutoliquidation.ValueHealth

        'Riesgos Profesionales
        INDTxtibcProfessionalRisk.EditValue = VerifyAutoliquidation.IBCProfessionalRisk
        INDSpProfessionalRiskPercentage.EditValue = VerifyAutoliquidation.RateContributionProfessionalRisk * 100
        INDTxtProfessionalValue.EditValue = VerifyAutoliquidation.ValueContributionProfessionalRisk

        'Caja de Compensación, SENA e ICBF
        INDTxtIBCCompensationFund.EditValue = VerifyAutoliquidation.IBCCompensationFund
        INDSpCompensationFundPercentage.EditValue = VerifyAutoliquidation.RateContributorCCF * 100
        INDTxtValueCompensation.EditValue = VerifyAutoliquidation.ValueContributionCCF

        INDSpSenaPercentage.EditValue = VerifyAutoliquidation.RateContributorSENA * 100
        INDTxtValueSena.EditValue = VerifyAutoliquidation.ValueSena

        INDSpICBFPercentage.EditValue = VerifyAutoliquidation.RateContributionICBF * 100
        INDTxtICBFValue.EditValue = VerifyAutoliquidation.ValueICBF

        'Fechas Incapacidad
        INDDeInabilityInitialDate.EditValue = VerifyAutoliquidation.AmbulatoryDisabilityInitialDate
        INDDeInabilityEndDate.EditValue = VerifyAutoliquidation.AmbulatoryDisabiltyEndDate

        'Fechas Licencias Maternidad
        INDDeMaternityInitialDate.EditValue = VerifyAutoliquidation.MaternityLeaveInitialDate
        INDDeMaternityEndDate.EditValue = VerifyAutoliquidation.MaternityLeaveEndDate

        'Fechas Vacaciones
        INDDeInitialDateVacation.EditValue = VerifyAutoliquidation.VacationInitialDate
        INDDeEndDateVacation.EditValue = VerifyAutoliquidation.VacationEndDate

        'Fechas Sanciones
        INDDeInitialDateSanction.EditValue = VerifyAutoliquidation.SanctionInitialDate
        INDDeEndDateSanction.EditValue = VerifyAutoliquidation.SanctionEndDate

        'Fechas IRL
        INDDeInitialIRLInability.EditValue = VerifyAutoliquidation.FechaInicioIRL
        INDDeEndDateIRL.EditValue = VerifyAutoliquidation.FechaFinIRL
    End Sub

    Private Sub CleanControls()
        INDTxtNitEmployee.EditValue = Nothing
        INDTxtNameEmployee.EditValue = Nothing
        INDMemoObservation.EditValue = Nothing

        'Días
        INDSpHealthDays.EditValue = 0
        INDSpPensionDays.EditValue = 0
        INDSpProfessionalRiskDays.EditValue = 0
        INDSpCompensationFundDays.EditValue = 0

        'Pensión
        INDTxtIBCPension.EditValue = 0
        INDSpPensionPercentage.EditValue = 0
        INDTxtValuePension.EditValue = 0

        'Salud
        INDTxtIBCHealth.EditValue = 0
        INDSpHealthPercentage.EditValue = 0
        INDTxtValueHealth.EditValue = 0

        'Riesgos Profesionales
        INDTxtibcProfessionalRisk.EditValue = Nothing
        INDSpProfessionalRiskPercentage.EditValue = 0
        INDTxtProfessionalValue.EditValue = 0

        'Caja de Compensación, SENA e ICBF
        INDTxtIBCCompensationFund.EditValue = 0
        INDSpCompensationFundPercentage.EditValue = 0
        INDTxtValueCompensation.EditValue = 0

        INDSpSenaPercentage.EditValue = 0
        INDTxtValueSena.EditValue = 0

        INDSpICBFPercentage.EditValue = 0
        INDTxtICBFValue.EditValue = 0

        'Fechas Incapacidad
        INDDeInabilityInitialDate.EditValue = Nothing
        INDDeInabilityEndDate.EditValue = Nothing

        'Fechas Licencias Maternidad
        INDDeMaternityInitialDate.EditValue = Nothing
        INDDeMaternityEndDate.EditValue = Nothing

        'Fechas Vacaciones
        INDDeInitialDateVacation.EditValue = Nothing
        INDDeEndDateVacation.EditValue = Nothing

        'Fechas Sanciones
        INDDeInitialDateSanction.EditValue = Nothing
        INDDeEndDateSanction.EditValue = Nothing

        'Fechas IRL
        INDDeInitialIRLInability.EditValue = Nothing
        INDDeEndDateIRL.EditValue = Nothing
    End Sub


#Region "Eventos Barra Botones"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(""))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
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
        'Me.Deshacer()
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
        Guardar()
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

    End Sub
    ''' <summary>
    ''' Confirmar un convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar

    End Sub
    ''' <summary>
    ''' Anualr un convenio
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular

    End Sub
    ''' <summary>
    ''' Suspender un contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender

    End Sub
    ''' <summary>
    ''' Click boton reactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickReactivar() Handles BarraBotones.ClickReactivar

    End Sub

    Private Async Function Guardar() As Task
        Try

            AssignValues()

            Using model As New MAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim Confirm = Await model.SaveVerifyAutoliquidation(VerifyAutoliquidation)

                If Confirm.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Confirm.Message
                    CleanControls()
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Confirm.Message
                End If

                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Function

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

    Private Sub AssignValues()
        With VerifyAutoliquidation
            .Observations = INDMemoObservation.EditValue

            'Dias
            .PensionDays = INDSpPensionDays.EditValue
            .HealthDays = INDSpHealthDays.EditValue
            .ProfessionalRiskDays = INDSpProfessionalRiskDays.EditValue
            .CompensationFundDays = INDSpCompensationFundDays.EditValue

            'Pensión
            .IBCPension = INDTxtIBCPension.EditValue
            .RateContributionPension = CInt(INDSpPensionPercentage.EditValue) / 100
            .ValuePension = INDTxtValuePension.EditValue

            'Salud
            .IBCHealth = INDTxtIBCHealth.EditValue
            .RateContributionHealth = CInt(INDSpHealthPercentage.EditValue) / 100
            .ValueHealth = INDTxtValueHealth.EditValue

            'Riesgos Profesionales
            .IBCProfessionalRisk = INDTxtibcProfessionalRisk.EditValue
            .RateContributionProfessionalRisk = CInt(INDSpProfessionalRiskPercentage.EditValue) / 100
            .ValueContributionProfessionalRisk = INDTxtProfessionalValue.EditValue

            'Caja de Compensación, SENA e ICBF
            .IBCCompensationFund = INDTxtIBCCompensationFund.EditValue
            .RateContributorCCF = CInt(INDSpCompensationFundPercentage.EditValue) / 100
            .ValueContributionCCF = INDTxtValueCompensation.EditValue

            .RateContributorSENA = CInt(INDSpSenaPercentage.EditValue) / 100
            .ValueSena = INDTxtValueSena.EditValue

            .RateContributionICBF = CInt(INDSpICBFPercentage.EditValue) / 100
            .ValueICBF = INDTxtICBFValue.EditValue

            'Fechas Incapacidad
            .AmbulatoryDisabilityInitialDate = INDDeInabilityInitialDate.EditValue
            .AmbulatoryDisabiltyEndDate = INDDeInabilityEndDate.EditValue

            'Fechas Licencias Maternidad
            .MaternityLeaveInitialDate = INDDeMaternityInitialDate.EditValue
            .MaternityLeaveEndDate = INDDeMaternityEndDate.EditValue

            'Fechas Vacaciones
            .VacationInitialDate = INDDeInitialDateVacation.EditValue
            .VacationEndDate = INDDeEndDateVacation.EditValue

            'Fechas Sanciones
            .SanctionInitialDate = INDDeInitialDateSanction.EditValue
            .SanctionEndDate = INDDeEndDateSanction.EditValue

            'Fechas IRL
            .FechaInicioIRL = INDDeInitialIRLInability.EditValue
            .FechaFinIRL = INDDeEndDateIRL.EditValue

        End With


    End Sub

#End Region
End Class