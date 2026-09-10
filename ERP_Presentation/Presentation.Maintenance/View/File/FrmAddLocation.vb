Imports Presentation.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP


Public Class FrmAddLocation


    Property INDAceptar As Boolean

    Public INDVarHide As String

    Dim _risklevel As List(Of Tuple(Of String, String))

    ReadOnly Property RiskLevel As List(Of Tuple(Of String, String))
        Get
            If _risklevel Is Nothing Then
                _risklevel = New List(Of Tuple(Of String, String))
                _risklevel.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("RiskLevelLow", "Maintenance")))
                _risklevel.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("RiskLevelMedium", "Maintenance")))
                _risklevel.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("RiskLevelHigh", "Maintenance")))
            End If
            Return _risklevel
        End Get
    End Property
    WriteOnly Property LocationTypeDataSource As List(Of LocationType)
        Set(value As List(Of LocationType))
            INDCbeLocationType.Properties.DataSource = value
            INDCbeLocationType.Properties.PopupFormWidth = INDCbeLocationType.Width
        End Set
    End Property

    Private Sub INDbtnAddRoot_Click(sender As Object, e As EventArgs) Handles INDbtnAddRoot.Click
        If INDVarHide <> "Ciudad" Then
            If INDTxtName.Text = String.Empty Then
                MessageIndigo.Show("Especifique la descripcion de la ubicacion", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
                Exit Sub
            End If
            If Object.Equals(INDCbeLocationType.EditValue, Nothing) = True Then
                MessageIndigo.Show("Especifique el tipo de ubicacion", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
                Exit Sub
            End If
        End If
        If INDBteCode.Text = String.Empty Then
            MessageIndigo.Show("Especifique el codigo de la ubicacion", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If
        If Object.Equals(INDgleRiskLevel.EditValue, Nothing) = True Then
            MessageIndigo.Show("Especifique el nivel de riesgo", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If
        INDAceptar = True
        Me.Close()
    End Sub

    Private Sub FrmAddLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If INDVarHide = "Ciudad" Then
            INDLyItemNameLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLyItemLocationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLyItemNameLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyItemLocationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If
        INDgleRiskLevel.Properties.DataSource = RiskLevel
    End Sub

    Private Sub INDBteCode_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.Properties.ButtonClick
        OpenSearch()
    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch()
        Me.BarraBotones.PermiteConsultar = True
        Dim FormSearchObjects = New FrmBusqueda
        AddHandler CType(FormSearchObjects.GridViewBusquedas, DevExpress.XtraGrid.Views.Grid.GridView).CustomColumnDisplayText, AddressOf CustomColumGrid
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = 400}}.ToList ', New ColumnInfo() With {.Caption = "Tipo De Ubicación", .FieldName = "IdLocationType", .ColumnWidth = 400}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLocationMaintenance
            '.ValorSolicitado = "Codigo"
            .FormParent = Me
            .ShowSearch()
        End With

    End Sub


    Private Sub CustomColumGrid(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs)
        If e.Value IsNot Nothing AndAlso e.Value.GetType.ToString <> "DevExpress.Data.NotLoadedObject" AndAlso CStr(e.Value) <> "" Then
            If e.Column.FieldName = "IdLocationType" Then
                Select Case e.Value
                    Case Is = 1
                        e.DisplayText = "Sede o Sucursal"
                    Case Is = 2
                        e.DisplayText = "Torre"
                    Case Is = 3
                        e.DisplayText = "Piso"
                    Case Is = 4
                        e.DisplayText = "Area"
                    Case Is = 5
                        e.DisplayText = "Habitación"
                End Select
            End If
        End If
    End Sub
End Class