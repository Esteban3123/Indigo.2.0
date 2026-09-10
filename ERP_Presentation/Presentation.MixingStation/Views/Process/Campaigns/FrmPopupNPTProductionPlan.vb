'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Diego A. Roldán Lozano
' Created          : 2023-03-16
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Drawing
Imports System.Dynamic
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

Public Class FrmPopupNPTProductionPlan

#Region "Builder"
    ''' <summary>
    ''' Builder
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ResizePopup()
        SetCtrJustification()
    End Sub
#End Region

#Region "Variables"

    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    ''' <returns></returns>
    Public Property CampaignDetailId As Integer

    ''' <summary>
    ''' Entidad seleccionada para procesar
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemsToProcess As ViewListDashboardConfirmationUnitDoseXpo

    ''' <summary>
    ''' Validar si es Plan de adecución 
    ''' </summary>
    ''' <returns></returns>
    Public Property AdequacyPlanNPT As Boolean = False

    ''' <summary>
    ''' Control para establecer la justificacion
    ''' </summary>
    Private _ctrJustification As CtrJustification

    ''' <summary>
    ''' Justificación de la preescripción
    ''' </summary>
    Private Justification As String

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
#End Region

#Region "Functions and methods"
    ''' <summary>
    ''' Muestra los botones solicitados
    ''' </summary>
    Private Sub PrepareToolbar()
        If AdequacyPlanNPT = True Then
            BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cerrar) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
            BarraBotones.OperatingUnitVisible = False
        Else
            BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cerrar) = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
            BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Verificar parámetros")
        End If
    End Sub

    ''' <summary>
    ''' Redimensiona el modal para que tome el 80% del ancho de la pantalla
    ''' </summary>
    Private Sub ResizePopup()
        SuspendLayout()
        Size = New System.Drawing.Size(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.8, Me.Height)
        ResumeLayout()
    End Sub

    ''' <summary>
    ''' Asigna el Ctr de justificacion al PopUp
    ''' </summary>
    Private Sub SetCtrJustification()
        Me._ctrJustification = New CtrJustification()
        Me._ctrJustification.SetInfoFunction(AddressOf Me.GetInfo)
        Me._ctrJustification.PrintInfo()
        Me._ctrJustification.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(Me._ctrJustification)
    End Sub

    ''' <summary>
    ''' Anula la solicitud
    ''' </summary>
    Public Async Sub Annulate()
        Try
            Using frm As New PopUpActionQualityControl()
                AsyncLoader(True)
                frm.StartPosition = FormStartPosition.CenterParent
                frm.TypeAction = 2
                frm._OpenPopUpProductionPlanNPT = True
                Dim trasparent As New FrmTransparent(frm, False)
                Me.Cursor = Cursors.Default

                If trasparent.ShowDialog(Me) = DialogResult.OK Then

                    Dim info = New With {.RejectCause = frm.INDGleCauseRejection.EditValue, .Observation = frm.INDMeObservations.Text.Trim()}
                    Dim dictionary As IDictionary(Of String, Object) = CType(INDBgvNPT.GetFocusedRow(), IDictionary(Of String, Object))
                    Dim RequestMixingStationDetailId As Integer = Convert.ToInt32(dictionary("RequestMixingStationDetailId"))

                    Using model As New MCampaign(Tag)
                        Dim data As Object = New ExpandoObject()
                        data.info = info
                        data.status = 6

                        Dim Result = Await model.CancellationadjustmentsNPT(RequestMixingStationDetailId, data)
                        If Result Is Nothing OrElse Not Result.StateResult Then
                            Mensaje(EeventViewerImages.MensajeError) = String.Format("No se pudo anular la adecuación. info #:{0}", Result.Message)
                            AsyncLoader(False)
                            Exit Sub
                        Else
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        End If
                        Await LoadData()
                    End Using
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la informacion para ser reflejada en el CTR
    ''' </summary>
    ''' <returns></returns>
    Private Function GetInfo() As Tuple(Of String)
        Return New Tuple(Of String)(Me.Justification)
    End Function

    ''' <summary>
    ''' Verifica las solicitudes seleccionadas
    ''' </summary>
    ''' <returns></returns>
    Private Async Function Save() As Task
        Try
            AsyncLoader(True)

            Dim Result As ConfirmationUnitDoseValidations = AssigningDetails()

            Using m As New MDashboardConfirmationUnitDose(Tag)
                Dim res = Await m.VerifyRequestNPT(Result)

                If res.StateResult Then
                    DialogResult = DialogResult.OK
                Else
                    ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = res.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Se crea la entidad ConfirmationUnitDoseValidations
    ''' </summary>
    ''' <returns></returns>
    Private Function AssigningDetails() As ConfirmationUnitDoseValidations
        Dim ConfirmationUnitDoseValidations As New ConfirmationUnitDoseValidations _
        With {
                .Id = ItemsToProcess.Id,
                .NPTVerified = True,
                .SafeStatus = 1
             }

        Return ConfirmationUnitDoseValidations
    End Function

    ''' <summary>
    ''' Verifica las solicitudes
    ''' </summary>
    Private Sub VerifiedParameters()
        Dim frm As New FrmPopupVerifyParameters()
        frm.GroupingCodeDose = ItemsToProcess.AGRUPAQUETE
        frm.StartPosition = FormStartPosition.CenterParent
        AddHandler frm.AproveParameters, AddressOf ReturnAddAproveParameters
        Dim transparent As New FrmTransparent(frm, False)
        Me.Cursor = Cursors.Default
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Retorna la informacion de aprobacion de parametros
    ''' </summary>
    Private Async Sub ReturnAddAproveParameters(sender As Object, e As AddAproveParametersEventArgs)
        If e.Aprove Then
            Await Save()
        End If
    End Sub


    Private Sub ShowHideColumn()
        If AdequacyPlanNPT Then
            INDGbSecondaryInfo.Visible = False

            With GridColumn16
                .Visible = True
                .VisibleIndex = 2
            End With

            With GridColumn17
                .Visible = True
                .VisibleIndex = 3
            End With

            With GridColumn18
                Visible = True
                .VisibleIndex = 4
            End With

            GridColumn3.Visible = False
            GridColumn4.Visible = False
            GridColumn5.Visible = False

        Else
            INDGbSecondaryInfo.Visible = True

            With GridColumn3
                Visible = True
                .VisibleIndex = 2
            End With

            With GridColumn4
                Visible = True
                .VisibleIndex = 3
            End With

            With GridColumn5
                Visible = True
                .VisibleIndex = 4
            End With

            GridColumn16.Visible = False
            GridColumn17.Visible = False
            GridColumn18.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadData() As Task
        Try
            AsyncLoader(True)
            Dim Filter As String = If(AdequacyPlanNPT, $"CampaignDetailId = {CampaignDetailId}", $"GroupingCodeDose= '{ItemsToProcess.AGRUPAQUETE}'")
            Dim data = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetCollection(Of ViewDetailRequestNPTXpo)(Nothing, Filter))

            If data.Any() Then
                Justification = data(0).JustificationPrescription
                ShowHideColumn()
                BuildColumns(data)
                INDGcNPT.DataSource = RebuildData(data)
                Me._ctrJustification.PrintInfo()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Generamos los datos a presentar en la rejilla convirtiendo a columnas los medicamentos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function RebuildData(data As List(Of ViewDetailRequestNPTXpo)) As List(Of Object)
        Dim productCodes = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
            .SelectMany(Function(m) m.Select(Function(o) New ProductNutritionDetails With {.ProductCode = o.ProductCode, .NutritionName = o.NutritionName}).Distinct().ToList()).Distinct().ToList()

        Dim random As New Random()

        Dim refactorData = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
        .Select(Function(gitem)
                    Dim first = gitem.FirstOrDefault()
                    Dim newobject As Object = New ExpandoObject()
                    With newobject
                        .PatientCode = gitem.Key.PatientCode
                        .GroupingCodeDose = gitem.Key.GroupingCodeDose
                        .PatientName = first.PatientName
                        .Bed = first.Bed
                        .PatientWeight = first.PatientWeight
                        .AdministrationRouteDescription = first.AdministrationRouteDescription
                        .InfusionTime = first.InfusionTime
                        .TotalVolume = first.TotalVolume
                        .InfusionVelocity = first.InfusionVelocity
                        .Water = first.Water
                        .WaterOsmolarity = first.WaterOsmolarity
                        .FunctionalUnitCodeName = first.FunctionalUnitCodeName
                        .RequestMixingStationDetailId = first.RequestMixingStationDetailId
                        .TypeNutrition = first.TypeNutrition
                        .NumberLotAdequacy = first.NumberLotAdequacy
                    End With

                    Dim dictionary As IDictionary(Of String, Object) = TryCast(newobject, IDictionary(Of String, Object))

                    If AdequacyPlanNPT Then
                        For Each pn In productCodes
                            Dim NutritionName = pn.NutritionName.Trim()
                            If dictionary.ContainsKey($"NUT_{NutritionName}_NAME") Then Continue For

                            dictionary.Add($"NUT_{NutritionName}_NAME", pn.NutritionName)
                            dictionary.Add($"NUT_{NutritionName}_VOLUME", 0D)

                        Next

                        For Each nut In gitem
                            Dim NutritionName = nut.NutritionName.Trim()
                            dictionary($"NUT_{NutritionName}_NAME") = nut.NutritionName
                            dictionary($"NUT_{NutritionName}_VOLUME") = String.Format("{0,10:N2}", nut.Volume)
                        Next
                    Else
                        For Each pn In productCodes
                            Dim productCode = pn.ProductCode.Trim()
                            If dictionary.ContainsKey($"NUT_{productCode}_DOSE") Then Continue For

                            dictionary.Add($"NUT_{productCode}_NAME", pn.NutritionName)
                            dictionary.Add($"NUT_{productCode}_DOSE", 0)
                            dictionary.Add($"NUT_{productCode}_VOLUME", 0D)
                            dictionary.Add($"NUT_{productCode}_OSMOLARITY", 0D)
                        Next

                        For Each nut In gitem
                            Dim prodcutCode = nut.ProductCode.Trim()
                            dictionary($"NUT_{prodcutCode}_NAME") = nut.NutritionName
                            dictionary($"NUT_{prodcutCode}_DOSE") = nut.Dose
                            dictionary($"NUT_{prodcutCode}_VOLUME") = String.Format("{0:N2}", nut.Volume)
                            dictionary($"NUT_{prodcutCode}_OSMOLARITY") = String.Format("{0:N2}", Math.Round((nut.Volume * nut.Osmolarity), 2))
                        Next
                    End If
                    Return newobject
                End Function).ToList()

        Return refactorData
    End Function


    ''' <summary>
    ''' Construcción de las columnas personalizadas
    ''' </summary>
    ''' <param name="caption"></param>
    ''' <param name="fieldName"></param>
    ''' <param name="name"></param>
    ''' <param name="width"></param>
    ''' <param name="color"></param>
    ''' <returns></returns>
    Private Function GetNewColumn(caption As String, fieldName As String, name As String, width As Integer, color As System.Drawing.Color) As BandedGridColumn
        Dim col As New BandedGridColumn
        col.Caption = caption
        col.FieldName = fieldName
        col.Name = name
        col.OptionsColumn.AllowEdit = False
        col.OptionsColumn.AllowFocus = False
        col.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        col.OptionsColumn.AllowMove = False
        col.OptionsColumn.AllowShowHide = False
        col.OptionsColumn.AllowSize = False
        col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        col.OptionsColumn.FixedWidth = True
        col.AppearanceCell.Options.UseBackColor = True
        col.AppearanceCell.BackColor = color
        col.Visible = True
        col.Width = width
        col.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, fieldName, "Total={0:N2}")})

        Return col
    End Function

    ''' <summary>
    ''' Construcción de las columnas por cada producto
    ''' </summary>
    ''' <param name="data"></param>
    Private Sub BuildColumns(data As List(Of ViewDetailRequestNPTXpo))
        Dim productCodes = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
            .SelectMany(Function(m) m.Select(Function(o) New ProductNutritionDetails _
            With {.ProductCode = Trim(o.ProductCode),
                    .NutritionName = Trim(o.NutritionName),
                    .VolumeNutrition = o.Volume,
                    .MaxValueNutrition = o.MaxValueNutrition,
                    .MinValueNutrition = o.MinValueNutrition,
                    .Request = o.Request})).Distinct().ToList()

        INDBgvNPT.Appearance.Row.BackColor = System.Drawing.Color.Transparent
        INDBgvNPT.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent
        INDBgvNPT.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)

        If AdequacyPlanNPT Then
            Dim gridBand As New GridBand()

            INDBgvNPT.Bands.Add(gridBand)
            gridBand.Caption = "Datos de administración"
            gridBand.Name = $"GridBandDatosAdmin"
            gridBand.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
            gridBand.AppearanceHeader.Options.UseFont = True
            gridBand.AppearanceHeader.Options.UseTextOptions = True
            gridBand.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None
            gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default
            gridBand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
            gridBand.Width = 200

            productCodes.ForEach(Sub(pn)
                                     Dim NutritionName = pn.NutritionName.Trim()
                                     If INDBgvNPT.Columns.Any(Function(m) m.Name = $"INDCol{NutritionName}_NAME") Then Exit Sub
                                     Dim rnd As New Random(NutritionName.GetHashCode())
                                     Dim color As System.Drawing.Color = System.Drawing.Color.FromArgb(100, rnd.Next(256), rnd.Next(256), rnd.Next(256))

                                     Dim col = GetNewColumn(String.Concat(NutritionName, " - Volumen (ml)"),
                                                            $"NUT_{NutritionName }_VOLUME",
                                                            $"INDCol{NutritionName}_NAME", 200, color)

                                     col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                     gridBand.Columns.Add(col)
                                 End Sub)
        Else

            Dim column As BandedGridColumn = INDBgvNPT.Columns.ColumnByName("GridColumn8") 'Volumen Total(ml)
            If data.Exists(Function(x) x.TotalVolume > If(x.MaxValueVolume, x.TotalVolume) OrElse x.TotalVolume < If(x.MinValueVolume, x.TotalVolume)) Then
                column.Image = My.Resources.Alerta
                column.ImageAlignment = StringAlignment.Far
            End If

            productCodes.ForEach(Sub(pn)
                                     Dim prodcutCode = pn.ProductCode.Trim()
                                     If INDBgvNPT.Bands.Any(Function(m) m.Name = $"gridBand{prodcutCode}") Then Exit Sub
                                     Dim rnd As New Random(prodcutCode.GetHashCode())
                                     Dim color As System.Drawing.Color = System.Drawing.Color.FromArgb(100, rnd.Next(256), rnd.Next(256), rnd.Next(256))
                                     Dim col = GetNewColumn("Dosis", $"NUT_{prodcutCode}_DOSE", $"INDCol{prodcutCode}_dose", 60, color)
                                     Dim col2 = GetNewColumn("Volumen (ml)", $"NUT_{prodcutCode}_VOLUME", $"INDCol{prodcutCode}_vol", 100, color)
                                     Dim col3 = GetNewColumn("Osmolaridad (mOsm/L)", $"NUT_{prodcutCode}_OSMOLARITY", $"INDCol{prodcutCode}_osm", 100, color)

                                     Dim gridBand As New GridBand()
                                     INDBgvNPT.Bands.Add(gridBand)
                                     INDBgvNPT.Columns.AddRange(New BandedGridColumn() {col, col2, col3})

                                     If pn.Request > pn.MaxValueNutrition OrElse pn.Request < pn.MinValueNutrition Then
                                         gridBand.ImageOptions.Image = My.Resources.Alerta
                                         gridBand.ImageOptions.Alignment = StringAlignment.Far
                                     End If

                                     gridBand.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
                                     gridBand.AppearanceHeader.Options.UseFont = True
                                     gridBand.AppearanceHeader.Options.UseTextOptions = True
                                     gridBand.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None
                                     gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default
                                     gridBand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                                     gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                                     gridBand.Caption = pn.NutritionName
                                     col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                     col2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                     col3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                     gridBand.Columns.Add(col)
                                     gridBand.Columns.Add(col2)
                                     gridBand.Columns.Add(col3)
                                     gridBand.Name = $"gridBand{prodcutCode}"
                                     gridBand.Width = 264

                                 End Sub)

        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"

            Case "RequestAnnulate"
                Annulate()
        End Select
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmPopupNPTProductionPlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrepareToolbar()
        If AdequacyPlanNPT = False Then
            IndigoGridView1.SetListAcction(INDBgvNPT, {eAcciones.RequestAnnulate, eAcciones.Edit}.ToList())
            INDGbMainInfo.Columns.Add(INDBgvNPT.Columns.FirstOrDefault(Function(m) m.Name = "colActions"))
        End If
        Await LoadData()
    End Sub

    ''' <summary>
    ''' Cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupNPTProductionPlan_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Se genera el número de la fila
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBgvNPT_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDBgvNPT.CustomUnboundColumnData
        Dim view As GridView = sender
        If e.IsGetData Then
            If e.Column.Name = INDColItem.Name Then
                e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Async Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        'Funcion que guardara las modificaciones que se le realicen a las solicitudes
    End Sub

    ''' <summary>
    ''' Verificar parametros
    ''' </summary>
    Private Sub BarraBotones_ClickValidar() Handles BarraBotones.Click_Validar
        VerifiedParameters()
    End Sub
#End Region

End Class

Class ProductNutritionDetails
    Property ProductCode As String
    Property NutritionName As String
    Property VolumeNutrition As Decimal
    Property MaxValueNutrition As Decimal?
    Property MinValueNutrition As Decimal?
    Property Request As Decimal?
End Class

Public Class AddAproveParametersEventArgs
    Inherits EventArgs

    Property Aprove As Boolean

End Class