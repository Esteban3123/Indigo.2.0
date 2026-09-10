Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base


Public Class FrmFixedAssetAddLocation

    ''' <summary>
    ''' Modelo del busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Dim modelBusqueda As MBusqueda

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
    WriteOnly Property LocationTypeDataSource As List(Of FixedAssetLocationType)
        Set(value As List(Of FixedAssetLocationType))
            INDCbeLocationType.Properties.DataSource = value
            INDCbeLocationType.Properties.PopupFormWidth = INDCbeLocationType.Width
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de unidad funcional
    ''' </summary>
    WriteOnly Property FunctionalUnitDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource 'Implements FunctionalUnitDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Dim _datasourceLocationClass As List(Of Tuple(Of Integer, String))
    ReadOnly Property DatasourceLocationClass As List(Of Tuple(Of Integer, String))
        Get
            If _datasourceLocationClass Is Nothing Then
                _datasourceLocationClass = New List(Of Tuple(Of Integer, String))
                _datasourceLocationClass.Add(New Tuple(Of Integer, String)(1, "Administrativa"))
                _datasourceLocationClass.Add(New Tuple(Of Integer, String)(2, "Operativa"))
                _datasourceLocationClass.Add(New Tuple(Of Integer, String)(3, "Mantenimiento"))
                _datasourceLocationClass.Add(New Tuple(Of Integer, String)(4, "Almacén y Bodegaje"))
            End If
            Return _datasourceLocationClass
        End Get
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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        modelBusqueda = Nothing
        INDAceptar = Nothing
        INDVarHide = Nothing
        _risklevel = Nothing
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
        modelBusqueda = New MBusqueda()
        INDgleRiskLevel.Properties.DataSource = RiskLevel
        INDSlClasification.Properties.DataSource = DatasourceLocationClass
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

    Private Sub INDSlFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlFunctionalUnit.QueryPopUp
        If INDSlFunctionalUnit.Properties.DataSource Is Nothing Then
            FunctionalUnitDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.ListFunctionalUnit, (True))
        End If
    End Sub
End Class