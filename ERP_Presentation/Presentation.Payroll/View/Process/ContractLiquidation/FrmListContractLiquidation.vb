Imports Presentation.Payroll.MVP
Imports Presentation.Controls
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports Presentation.Payroll
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports Presentation.Controls.FormBase
Imports DevExpress.XtraPrinting
Imports Presentation.Controls.ReportPrintToolExt
Imports DevExpress.XtraReports.UI
Imports Presentation.CloudAgent

Public Class FrmListContractLiquidation

    Implements IContractLiquidation

    Public ListContractLiquidationEmploye As ContractLiquidation

    Dim indigo As SessionValues = SessionValues.Instance

    Public Property IsBusy As Boolean
        Get
            Return If(Me.LyciBusyIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always, True, False)
        End Get
        Set(value As Boolean)
            Me.LyciBusyIndicator.Visibility = If(value = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            If value Then
                INDBtnReporte.Enabled = False
                INDbtnView.Enabled = False
                INDgrdContractListLiquidation.Enabled = False
                CtrDateNavigator1.Enabled = False
                INDBtnBuscar.Enabled = False
                INDBtnCancelar.Enabled = False
            Else
                INDBtnReporte.Enabled = True
                INDbtnView.Enabled = True
                INDgrdContractListLiquidation.Enabled = True
                CtrDateNavigator1.Enabled = True
                INDBtnBuscar.Enabled = True
                INDBtnCancelar.Enabled = True
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Property HumanTalentList As LinqInstantFeedbackSource Implements IContractLiquidation.HumanTalentList
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As LinqInstantFeedbackSource)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property RetirementReasonList As List(Of RetirementReason) Implements IContractLiquidation.RetirementReasonList
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As List(Of RetirementReason))
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property EmployeeList As List(Of Employee) Implements IContractLiquidation.EmployeeList
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As List(Of Employee))
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Employee As Employee Implements IContractLiquidation.Employee
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Employee)
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractLiquidation.ActionsOnControls
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Buscar por Mes y Año las Liquidaciones de Contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnBuscar_Click(sender As Object, e As EventArgs) Handles INDBtnBuscar.Click

        Try
            Using Model As New MContractLiquidation(596)
                Me.IsBusy = True
                Dim ListContractLiquidation = Await Model.GetContractLiquidationByMonthAndYear(CtrDateNavigator1.GetMonth, CtrDateNavigator1.GetYear)
                INDgrdContractListLiquidation.DataSource = ListContractLiquidation
                Me.IsBusy = False
            End Using
        Catch ex As Exception
            Me.IsBusy = False
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try
    End Sub

    Private Sub INDBtnVisualizar_Click(sender As Object, e As EventArgs) Handles INDbtnView.Click
        Me.Close()
    End Sub

    Private Sub CtrDateNavigator1_OnChangeDate(sender As Object, e As EventArgs) Handles CtrDateNavigator1.OnChangeDate
        INDgrdContractListLiquidation.DataSource = Nothing
    End Sub

    Private Sub INDBtnCancelar_Click(sender As Object, e As EventArgs) Handles INDBtnCancelar.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' evento que se dispara al dar click en el boton de generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles INDBtnReporte.Click
        Try
            If INDviewInfo.RowCount() = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No existen datos en la rejilla"
                Exit Sub
            End If
            Dim List = DirectCast(INDgrdContractListLiquidation.DataSource, List(Of ContractLiquidation))

            If (From x In List Where x.SelectOption = True Select x).Count() = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay items seleccionados en la rejilla"
                Exit Sub
            End If

            If indigo.IndigoCompanyType = 1 Then

                Dim ObjOperatingUnit As Domain.Entities.OperatingUnit

                Using Model As New MContractLiquidation(596)
                    Me.IsBusy = True
                    Dim ListOperatingUnit = Await Model.ListAllOperatingUnit()
                    Me.IsBusy = False
                    If ListOperatingUnit IsNot Nothing AndAlso ListOperatingUnit.Count > 0 Then
                        ObjOperatingUnit = ListOperatingUnit.Where(Function(x) x.Id = indigo.IndigoOperatingUnitId).FirstOrDefault()
                    End If

                End Using

                Dim ObjListEmployee = (From x In List Where x.SelectOption = True Select x.ContractId).FirstOrDefault()

                Dim reporte As New rptContractLiquidation()

                reporte.ParametrosReporte = {ObjListEmployee, ObjOperatingUnit}
                INDDvReport.DocumentSource = reporte
                reporte.CargarDataSource()

                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then

                    reporte.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.Menu)
                    reporte.CreateDocument(True)

                End If
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDPcReport.Visible = True

                    Me.WindowState = FormWindowState.Maximized
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If

            Else

                ListEmployee = (From x In List Where x.SelectOption = True Select x.Id).ToList()

                Dim reporte As New rptPayrollContractLiquidation()

                INDDvReport.DocumentSource = reporte

                reporte.CargarDataSource1(ListEmployee)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then

                    reporte.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.Menu)
                    reporte.CreateDocument(True)

                End If
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDPcReport.Visible = True

                    Me.WindowState = FormWindowState.Maximized
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            End If

        Catch ex As Exception
            Me.IsBusy = False
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio del check
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim infoView As ContractLiquidation = INDviewInfo.GetFocusedRow()
            If infoView IsNot Nothing Then
                ListContractLiquidationEmploye = infoView
                infoView.SelectOption = e.NewValue
            End If
        End If
    End Sub
    ''' <summary>
    ''' Se dispara al dar clic en el boton retornar
    ''' </summary>
    Private Sub INDCnBackReport_ClickBack() Handles INDCnBackReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDPcReport.Visible = False
        Me.WindowState = FormWindowState.Normal
    End Sub
End Class