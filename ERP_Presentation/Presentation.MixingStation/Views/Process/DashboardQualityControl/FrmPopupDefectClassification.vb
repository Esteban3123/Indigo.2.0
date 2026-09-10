Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

Public Class FrmPopupDefectClassification
    Public Enum eFormState
        Production
        Quality
    End Enum

    ''' <summary>
    ''' control de la parte superior 
    ''' </summary>
    Dim _CtrGeneric2Labels As CtrGeneric2Labels

    Public Sub New()
        InitializeComponent()
        _CtrGeneric2Labels = New CtrGeneric2Labels()
        _CtrGeneric2Labels.Dock = DockStyle.Top
        AdditionalControlPanel.Controls.Add(_CtrGeneric2Labels)
        AdditionalControlPanel.Parent.MinimumSize = New Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.Parent.MaximumSize = New Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.MinimumSize = New Drawing.Size(450, AdditionalControlPanel.Height)
        AdditionalControlPanel.MaximumSize = New Drawing.Size(450, AdditionalControlPanel.Height)
        INDGvDefectClassification.OptionsView.ShowAutoFilterRow = False
    End Sub

    ''' <summary>
    ''' Peso teorico
    ''' </summary>
    Public Property TheoreticalWeight As Decimal
        Get
            Return INDSeTheoreticalWeight.EditValue
        End Get
        Set(value As Decimal)
            INDSeTheoreticalWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Peso insumos
    ''' </summary>
    Public Property InputsWeight As Decimal
        Get
            Return INDSeInputsWeight.EditValue
        End Get
        Set(value As Decimal)
            INDSeInputsWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Peso teorico + Peso insumos
    ''' </summary>
    Public Property TheoreticalAndInputsWeight As Decimal
        Get
            Return INDSeSumInputsTheoreticalWeight.EditValue
        End Get
        Set(value As Decimal)
            INDSeSumInputsTheoreticalWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Peso minimo - 5%
    ''' </summary>
    Public Property MinimunWeight As Decimal
        Get
            Return INDSeMinimumWeight.EditValue
        End Get
        Set(value As Decimal)
            INDSeMinimumWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Peso maximo 5%
    ''' </summary>
    Public Property MaximunWeight As Decimal
        Get
            Return INDSeMaximumWeight.EditValue
        End Get
        Set(value As Decimal)
            INDSeMaximumWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Peso real
    ''' </summary>
    Public Property ActualWeight As Decimal?
        Get
            Return INDSeActualWeight.EditValue
        End Get
        Set(value As Decimal?)
            INDSeActualWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidades reempacas / reenvasadas
    ''' </summary>
    Public Property QuantityRequest As Integer
        Get
            Return INDSeQuantityRequest.EditValue
        End Get
        Set(value As Integer)
            INDSeQuantityRequest.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Defecto Critico
    ''' </summary>
    Public Property CriticalDefect As Integer
        Get
            Return INDSeCriticalDefect.EditValue
        End Get
        Set(value As Integer)
            INDSeCriticalDefect.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidades a liberar
    ''' </summary>
    Public Property ReleaseQuantity As Integer
        Get
            Return INDSeReleaseQuantity.EditValue
        End Get
        Set(value As Integer)
            INDSeReleaseQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Rendimiento
    ''' </summary>
    Public Property Performance As Decimal
        Get
            Return INDSePerformance.EditValue
        End Get
        Set(value As Decimal)
            INDSePerformance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño de la muestra
    ''' </summary>
    Public Property IndicationSize As Integer
        Get
            Return INDSeIndicationSize.EditValue
        End Get
        Set(value As Integer)
            INDSeIndicationSize.EditValue = value
        End Set
    End Property

    Private Property DataSource As List(Of DefectClassificationModel)
        Get
            Return INDGcDefectClassification.DataSource
        End Get
        Set(value As List(Of DefectClassificationModel))
            INDGcDefectClassification.DataSource = value
            INDGcDefectClassification.RefreshDataSource()
        End Set
    End Property

    Private Shadows _model As New MDashboardQualityControl(Tag)

    Private _formState As eFormState

    Public Property FormState As eFormState
        Get
            Return _formState
        End Get
        Set(value As eFormState)
            _formState = value
            INDGvDefectClassification.BeginUpdate()
            If _formState = eFormState.Production Then
                INDColQuality.HideColumn()
            Else
                INDColProduction.HideColumn()
            End If
            INDGvDefectClassification.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Public Property RequestPackageDetailStatusIds As List(Of Integer)

    Public WriteOnly Property UnitDoseTypeCodeName As String
        Set(value As String)
            INDTeUnitDoseType.Text = value
        End Set
    End Property

    ''' <summary>
    ''' lote
    ''' </summary>
    Private _batchCodes As List(Of String)
    Public WriteOnly Property BatchCodes As List(Of String)
        Set(value As List(Of String))
            _batchCodes = value
        End Set
    End Property

    Public Property UnitDoseClass As Integer

    Private Async Sub FrmPopupDefectClassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await InitForm()
    End Sub

    ''' <summary>
    ''' Inicia el formulario
    ''' </summary>
    ''' <returns></returns>
    Private Async Function InitForm() As Task
        Try
            AsyncLoader(True)
            _CtrGeneric2Labels.Label1.Text = IIf(_batchCodes.Count > 1, "Lotes:", "Lote:")

            Dim batchs As String = String.Join(", ", _batchCodes)

            If batchs.Length > 22 Then
                batchs = String.Concat(batchs.Substring(0, 22), "...")
            ElseIf _batchCodes.Count = 1 Then
                batchs = batchs.Replace(",", "")
            End If

            _CtrGeneric2Labels.Label2.Text = $"{batchs}"
            _CtrGeneric2Labels.BatchCodes = _batchCodes

            DataSource = Await _model.GetRequestPackageDetailStatusDefectClassification(RequestPackageDetailStatusIds, UnitDoseClass, If(_formState = eFormState.Production, 1, 2))

            If DataSource?.Any() Then
                Dim Observations As String = String.Empty
                For Each itemDetailStatusId In RequestPackageDetailStatusIds
                    Dim itemObservation = DataSource?.Where(Function(x) x.RequestPackageDetailStatusId.HasValue AndAlso x.RequestPackageDetailStatusId.Value = itemDetailStatusId).
                        Select(Function(i) If(String.IsNullOrEmpty(i.Observation), String.Empty, i.Observation)).
                        FirstOrDefault()

                    If String.IsNullOrEmpty(Observations) Then
                        Observations = itemObservation
                    Else
                        If Not String.IsNullOrEmpty(itemObservation) Then
                            Observations &= " - " & itemObservation
                        End If
                    End If
                Next
                INDMeObservation.EditValue = Observations
            End If

            Select Case UnitDoseClass
                Case EUnitDoseTypeClass.ParenteralNutrition
                    INDLcgParenteralNutrition.HideControl(False)
                    INDColQuantity.HideColumn()
                    Await ValidateWeightParenteralNutrition(RequestPackageDetailStatusIds)
                Case EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling
                    INDLcgQualityControl.HideControl(False)
                    INDColQuantity.ShowColumn(2)
                    QuantityRequest = RequestPackageDetailStatusIds.Count()
                    ReleaseQuantity = RequestPackageDetailStatusIds.Count()
                    RepositoryItemSpinEdit1.MaxValue = QuantityRequest
                    Performance = 100
            End Select

            BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Refleja el segmento 'Validación del peso de la nutrición parenteral' si el tipo
    ''' de dosis unitaria es nutricion parenteral
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateWeightParenteralNutrition(_RequestPackageDetailStatusIds As List(Of Integer)) As Task
        Try
            Dim Filter As String = $"RequestPackageDetailStatusId = {_RequestPackageDetailStatusIds(0)}"
            Dim data = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetXPOObject(Of ViewListValidationWeightNPTXpo)(Filter))

            If data IsNot Nothing Then
                TheoreticalWeight = data.TheoreticalWeight
                InputsWeight = data.InputsWeight
                TheoreticalAndInputsWeight = data.TheoreticalAndInputsWeight
                MinimunWeight = data.MinimunWeight
                MaximunWeight = data.MaximunWeight
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Async Sub Guardar(Optional FSave As Boolean = False)
        Try
            If Not FSave AndAlso MessageIndigo.Show("¿Está seguro que desea almacenar los datos seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Return
            End If

            Dim _DefectClassificationHeaderTmp As New DefectClassificationHeaderModel _
            With {.Observation = INDMeObservation.EditValue, .ValidateWeigthNPT = IIf(UnitDoseClass = EUnitDoseTypeClass.ParenteralNutrition,
                                                                                      CBool(INDLciFalseValidation.Visibility),
                                                                                      Nothing),
                    .ActualWeight = If(ActualWeight > 0, ActualWeight, Nothing), .IndicationSize = If(IndicationSize > 0, IndicationSize, Nothing)}

            AsyncLoader(True)
            Dim res = Await _model.SaveDefectClassification(IIf(_formState = eFormState.Quality, True, False), RequestPackageDetailStatusIds, _DefectClassificationHeaderTmp, DataSource, FSave)
            AsyncLoader(False)
            If res.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "Los datos se han almacenado correctamente"
                DialogResult = DialogResult.OK
            Else
                If res?.MessageResult?.Count > 0 Then
                    Using formulario As New FrmListErrors(res?.MessageResult.Select(Function(m) $"Lote {m}").ToList())
                        formulario.Title = "Lista de Mensajes"
                        formulario.StartPosition = FormStartPosition.CenterParent
                        formulario.INDLygQuestionControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        formulario.LabelQuestion = "Desea guardar su clasificación de defectos sin hallazgos?"
                        AddHandler formulario.ConfirmQuestionEvent, AddressOf ForceSave
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' evento para mandar a guardar forzadamente la clasificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ForceSave(sender As Object, e As Boolean)
        If Not e Then
            Exit Sub
        End If
        Guardar(e)
    End Sub

#Region "Events"
    Private Sub FrmPopupDefectClassification_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub INDGvDefectClassification_CustomDrawRowIndicator(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles INDGvDefectClassification.CustomDrawRowIndicator
        If e.RowHandle >= 0 Then
            e.Info.DisplayText = (e.RowHandle + 1).ToString()
        End If
    End Sub

    Private Sub INDSePerformance_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDSePerformance.CustomDisplayText
        If Not String.IsNullOrEmpty(e.DisplayText) Then
            If Not e.DisplayText.EndsWith("%") Then
                e.DisplayText &= "%"
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        _model.Dispose()
    End Sub

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor de campo 'Peso real'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeActualWeight_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeActualWeight.EditValueChanged

        If ActualWeight = 0 Then Exit Sub

        If ActualWeight >= MinimunWeight AndAlso ActualWeight <= MaximunWeight Then
            INDLciTrueValidation.ShowLayout()
            INDLciFalseValidation.HideLayout()
        Else
            INDLciFalseValidation.ShowLayout()
            INDLciTrueValidation.HideLayout()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor de campo 'Peso real'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeIndicationSize_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDSeIndicationSize.EditValueChanging
        If e.NewValue = 0 Then Exit Sub

        If e.NewValue > QuantityRequest Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = "No se puede superar las unidades reempacadas / reenvasadas"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta mientras se cambia el SpinEdit de la columna "Cantidad"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemSpinEdit1.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim row = INDGvDefectClassification.GetFocusedObject(Of DefectClassificationModel)()

            Dim QuantityDefects As Integer = e.NewValue + TryCast(INDGvDefectClassification.DataSource, List(Of DefectClassificationModel)).ToList() _
                    .Where(Function(x) x.DefectClassificationItemId <> row.DefectClassificationItemId) _
                    .Sum(Function(x) x.Quantity)

            If QuantityDefects > QuantityRequest Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "La suma total de las cantidades no puede ser mayor a las unidades reempacadas / reenvasadas"
                Exit Sub
            End If

            If e.NewValue > 0 Then
                If FormState = eFormState.Production Then
                    row.Production = True
                Else
                    row.Quality = True
                End If
            ElseIf e.NewValue = 0 Then
                row.Production = False
                row.Quality = False
            End If

            If row.Critical Then
                Dim Result As Integer = e.NewValue + TryCast(INDGvDefectClassification.DataSource, List(Of DefectClassificationModel)).ToList() _
                    .Where(Function(x) x.DefectClassificationItemId <> row.DefectClassificationItemId And x.Critical) _
                    .Sum(Function(x) x.Quantity)

                CriticalDefect = Result
                ReleaseQuantity = QuantityRequest - CriticalDefect
                Performance = (QuantityRequest - CriticalDefect) / QuantityRequest
            End If
        End If

        INDGvDefectClassification.RefreshData()
    End Sub

#End Region
#End Region
End Class