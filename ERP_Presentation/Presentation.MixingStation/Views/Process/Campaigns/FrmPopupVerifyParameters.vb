'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Andres Alarcon
' Created          : 2023-09-11
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Drawing
Imports System.Dynamic
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

Public Class FrmPopupVerifyParameters

#Region "Events"

    ''' <summary>
    ''' evento para aprovar los parametros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AproveParameters(sender As Object, e As AddAproveParametersEventArgs)

#End Region

#Region "Builder"
    ''' <summary>
    ''' Builder
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ResizePopup()
    End Sub
#End Region

#Region "Variables"

    ''' <summary>
    ''' Codigo unico de la solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupingCodeDose As String

#End Region

#Region "Functions and methods"
    ''' <summary>
    ''' Muestra los botones solicitados
    ''' </summary>
    Private Sub PrepareToolbar()
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cerrar) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Aprobar")
    End Sub

    ''' <summary>
    ''' Redimensiona el modal para que tome el 80% del ancho de la pantalla
    ''' </summary>
    Private Sub ResizePopup()
        SuspendLayout()
        Size = New System.Drawing.Size(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6, Me.Height)
        ResumeLayout()
    End Sub

    ''' <summary>
    ''' Carga los datos
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadData() As Task
        Try
            AsyncLoader(True)
            Dim data = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetCollection(Of ViewDetailPharmaceuticalParametersNPTXpo)(Nothing, $"GroupingCodeDose = '{GroupingCodeDose}'"))
            BuildColumns(data)
            INDGcNPT.DataSource = RebuildData(data)
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
    Private Function RebuildData(data As List(Of ViewDetailPharmaceuticalParametersNPTXpo)) As List(Of Object)
        Dim PharmaceuticalParameters = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
            .SelectMany(Function(m) m.Select(Function(o) New PharmaceuticalParameters With {.Name = o.ParameterName, .Color = o.Color}).Distinct().ToList()).Distinct().ToList()

        Dim refactorData = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
            .Select(Function(gitem)
                        Dim first = gitem.FirstOrDefault()
                        Dim newobject As Object = New ExpandoObject()
                        With newobject
                            .PatientCode = gitem.Key.PatientCode
                            .GroupingCodeDose = gitem.Key.GroupingCodeDose
                            .PatientName = first.PatientName
                            .Bed = first.Bed
                            .FunctionalUnitCodeName = first.FunctionalUnitCodeName
                            .PatientWeight = first.PatientWeight
                            .AdministrationRouteDescription = first.AdministrationRouteDescription
                            .InfusionTime = first.InfusionTime
                            .TotalVolume = first.TotalVolume
                            .InfusionVelocity = first.InfusionVelocity
                        End With

                        Dim dictionary As IDictionary(Of String, Object) = TryCast(newobject, IDictionary(Of String, Object))

                        For Each pp In PharmaceuticalParameters
                            Dim ParameterName = pp.Name
                            Dim ColorParameter = pp.Color
                            If dictionary.ContainsKey($"NUT_{ParameterName}_NAME") Then Continue For

                            dictionary.Add($"NUT_{ParameterName}_NAME", ParameterName)
                            dictionary.Add($"NUT_{ParameterName}_COLOR", ParameterName)
                        Next

                        For Each nut In gitem
                            Dim ParameterName = nut.ParameterName
                            Dim ColorParameter = nut.Color

                            dictionary($"NUT_{ParameterName}_NAME") = nut.ParameterName
                            dictionary($"NUT_{ParameterName}_COLOR") = nut.Color
                            dictionary($"NUT_{ParameterName}_RESULT") = String.Format("{0:N2}", nut.Result)
                        Next

                        Return newobject
                    End Function).ToList()

        Return refactorData
    End Function

    ''' <summary>
    ''' Construcción de las columnas por cada parametro
    ''' </summary>
    ''' <param name="data"></param>
    Private Sub BuildColumns(data As List(Of ViewDetailPharmaceuticalParametersNPTXpo))
        Dim PharmaceuticalParameters = data.GroupBy(Function(m) New With {Key m.PatientCode, Key m.GroupingCodeDose}) _
            .SelectMany(Function(m) m.Select(Function(o) New PharmaceuticalParameters With {.Name = o.ParameterName, .Color = o.Color})).Distinct().ToList()

        INDBgvNPT.Appearance.Row.BackColor = Color.Transparent
        INDBgvNPT.Appearance.FocusedRow.BackColor = Color.Transparent
        INDBgvNPT.Appearance.FocusedRow.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold)

        Dim gridBand As New GridBand()
        INDBgvNPT.Bands.Add(gridBand)
        gridBand.Caption = "Parámetros farmacéuticos"
        PharmaceuticalParameters.ForEach(Sub(pn)
                                             Dim ParameterName = pn.Name
                                             If INDBgvNPT.Bands.Any(Function(m) m.Name = $"gridBand{ParameterName}") Then Exit Sub
                                             Dim rnd As New Random(ParameterName.GetHashCode())

                                             Dim color As Color = HexToRgb(pn.Color)
                                             Dim col = GetNewColumn(ParameterName, $"NUT_{ParameterName}_RESULT", $"INDCol{ParameterName}_NAME", 140, color)

                                             gridBand.AppearanceHeader.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold)
                                             gridBand.AppearanceHeader.Options.UseFont = True
                                             gridBand.AppearanceHeader.Options.UseTextOptions = True
                                             gridBand.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None
                                             gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                                             gridBand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                                             gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                                             col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                             gridBand.Columns.Add(col)
                                             gridBand.Name = $"gridBand{ParameterName}"
                                             gridBand.Width = 264
                                         End Sub)
    End Sub

    ''' <summary>
    ''' Convierte el color en formato hexadecimal al RGB
    ''' </summary>
    Function HexToRgb(ByVal hex As String) As Color

        If String.IsNullOrEmpty(hex) Then
            Return Color.FromArgb(255, 255, 255)
        End If

        Dim r As Integer = Convert.ToInt32(hex.Substring(0, 2), 16)
        Dim g As Integer = Convert.ToInt32(hex.Substring(2, 2), 16)
        Dim b As Integer = Convert.ToInt32(hex.Substring(4, 2), 16)

        Return Color.FromArgb(r, g, b)
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
        col.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, fieldName, "Total={0:0.##}")})

        Return col
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmPopupVerifyParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrepareToolbar()
        Await LoadData()
    End Sub

    ''' <summary>
    ''' Cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupVerifyParameters_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            RaiseEvent AproveParameters(Nothing, New AddAproveParametersEventArgs With
            {
                .Aprove = False
            }
        )
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
    ''' Aprobar parametros
    ''' </summary>
    Private Sub BarraBotones_ClickValidar() Handles BarraBotones.ClickConfirmar
        RaiseEvent AproveParameters(Nothing, New AddAproveParametersEventArgs With
            {
                .Aprove = True
            }
        )
        Me.Close()
    End Sub

#End Region

End Class

Class PharmaceuticalParameters
    Property Name As String

    Property Color As String
End Class