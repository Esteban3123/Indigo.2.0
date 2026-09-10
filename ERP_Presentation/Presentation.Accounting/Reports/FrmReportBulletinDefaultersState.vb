#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Presentation.CloudAgent
Imports Presentation.Accounting.MVP
Imports System.Text
Imports System.IO

#End Region

Public Class FrmReportBulletinDefaultersState

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"

    ''' <summary>
    ''' Almacena el origen de datos para terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource

    ''' <summary>
    ''' Lsita de tuplas que contiene periodos de tiempo
    ''' </summary>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Almacena una lista de tuplas que representan diferentes periodos de tiempo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "Dias"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "Semanas"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(3, "Meses"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(4, "Años"))
            End If
            Return _FillingPeriod
        End Get
    End Property

    ''' <summary>
    ''' Lista de tuplas tipo de reporte
    ''' </summary>
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite acceder a una lista de tipos de reportes predefinidos (resumen y detallado) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumen"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

#End Region

#Region "Methods"

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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'validaciones controles de terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportBulletinDefaultersState()
            reporte.ParametrosReporte = New Object() {INDDeDateCourt.EditValue,
                                                        INDSleValueReport.EditValue,
                                                        INDTxtValue.EditValue,
                                                        INDGlePeriodType.EditValue,
                                                        INDSpnPeriodValue.EditValue,
                                                        INDSleThirdPartyStart.EditValue,
                                                        INDSleThirdPartyEnd.EditValue,
                                                        INDGleTypeReport.EditValue
                                                          }
            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeDateCourt.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGlePeriodType.Properties.DataSource = Me.FillingPeriod
        Me.INDGleTypeReport.Properties.DataSource = Me.FillingTypeReport

        Me.INDSleValueReport.EditValue = False
        Me.INDSleMoraPeriod.EditValue = False
        Me.INDGlePeriodType.EditValue = 3
        Me.INDSpnPeriodValue.EditValue = 6
        Me.INDGleTypeReport.EditValue = 1

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync

        INDSleThirdPartyStart.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyEnd.View.OptionsView.ShowGroupPanel = False

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el control "Valor a Reportar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleValueReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleValueReport.EditValueChanged
        If INDSleValueReport.EditValue = False Then
            INDTxtValue.EditValue = Nothing
            INDTxtValue.Enabled = False
        Else
            INDTxtValue.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el control "Periodo de Mora"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMoraPeriod_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMoraPeriod.EditValueChanged
        If INDSleMoraPeriod.EditValue = False Then
            INDGlePeriodType.EditValue = 3
            INDSpnPeriodValue.EditValue = 6
            INDGlePeriodType.Enabled = False
            INDSpnPeriodValue.Enabled = False
        Else
            INDGlePeriodType.Enabled = True
            INDSpnPeriodValue.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el botón para generar archivo de texto plano
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks
    Private Async Sub INDSbGenerateFlatFile_Click(sender As Object, e As EventArgs) Handles INDSbGenerateFlatFile.Click
        If Me.ValidateControlsReports = True Then
            Dim DataArchive As StringBuilder
            Using model As New MSettingsAccount(Me.Tag.ToString())
                DataArchive = Await model.GenerateArchiveBulletinDefaultersState(INDGleTypeReport.EditValue, Format(CDate(INDDeDateCourt.EditValue), "yyyy-MM-dd"), INDSleValueReport.EditValue, INDTxtValue.EditValue, INDGlePeriodType.EditValue, INDSpnPeriodValue.EditValue, INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue, INDTxtEntityCode.EditValue)
            End Using
            DialogGenerateFile(DataArchive)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Try
            Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            save.Filter = "Texto|*.txt"
            save.Title = "REPORTE SEMESTRAL " & INDTxtEntityCode.EditValue & " " & MonthName(Month(CDate(INDDeDateCourt.EditValue))) & " " & Year(CDate(INDDeDateCourt.EditValue))
            save.FileName = "REPORTE SEMESTRAL " & INDTxtEntityCode.EditValue & " " & MonthName(Month(CDate(INDDeDateCourt.EditValue))) & " " & Year(CDate(INDDeDateCourt.EditValue)) & ".txt"
            If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = save.OpenFile()
                Dim streamWrite As New StreamWriter(file)
                streamWrite.Write(content)
                streamWrite.Flush()
                streamWrite.Close()
                If MessageIndigo.Show("Desea Abrir el Archivo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Process.Start(save.FileName)
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando el Archivo"
        End Try
    End Sub

#End Region

End Class