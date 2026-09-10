Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports DevExpress.XtraLayout
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.FixedAsset.MVP

Public Class CtrLastCost

#Region "Delegate"

    ''' <summary>
    ''' limpia los controles previamente creados
    ''' </summary>
    Private Sub DeleteControlsLabel()
        For Each item As LayoutControlItem In LayoutControl1.Items.ToList().FindAll(Function(x) x.GetType.Name = "LayoutControlItem")
            ' Verificar si el item es un LayoutControlItem que deseas eliminar
            If item?.Control IsNot Nothing AndAlso String.IsNullOrEmpty(item.Control.Name) AndAlso item.Control.GetType.Name = "LabelControl" Then
                ' Eliminar el LayoutControlItem
                Me.LayoutControl1.Controls.Remove(item.Control)
                LayoutControl1.Remove(item)
            End If
        Next
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets the title.
    ''' </summary>
    ''' <value>
    ''' The title.
    ''' </value>
    Public Property Title As String
        Get
            Return INDlblLastCostTitle.Text
        End Get
        Set(value As String)
            INDlblLastCostTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de escritura que contiene el ultimo costo y lo muestra.
    ''' </summary>
    WriteOnly Property LastCostItemValue As String
        Set(value As String)
            INDpceLastCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que almacena el formato de la moneda del modulo
    ''' </summary>
    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo

    Private _codeISO4217 As String
    ''' <summary>
    ''' Codigo ISO4217
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
        End Set
    End Property
#End Region

#Region "Functions"

    ''' <summary>
    ''' Muestra el valor del avance en el control de usuario
    ''' </summary>
    Public Sub PrintLastCost()
        LastCostItemValue = Nothing
        INDgvLastCost.ExpandAllGroups()
        IndigoGridControl1.RefreshGrid(INDgcLastCost)
        AddHandler INDlcgLastCost.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler LayoutControl1.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler INDliLastCostTitle.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler INDlblLastCostTitle.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler MyBase.Click, AddressOf CtrAdvanceTreasury_Click
    End Sub

    ''' <summary>
    ''' Imprime el valor del ultimo costo cuando existe una sola moneda activa.
    ''' </summary>
    ''' <param name="lastCost"></param>
    Public Sub PrintSingleCost(lastCost As Decimal)
        _codeISO4217 = If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)
        CurrencyNumbertFormat = If(CurrencyNumbertFormat, _codeISO4217?.GetNumberFormat)
        INDLcLastCostValue.Appearance.Options.UseFont = True
        INDLcLastCostValue.Appearance.Options.UseForeColor = True
        INDLcLastCostValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        LastCostItemValue = _codeISO4217 + " " + Utils.GetMoneyWithISO4217(lastCost, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
        LayoutControlItem1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center
        INDLcLastCostValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub

    ''' <summary>
    ''' Obtiene el ultimo costo de un artículo por cada moneda parametrizada.
    ''' Si no existe ingreso de activo en la consulta, utiliza el LastCost del artículo.
    ''' </summary>
    ''' <param name="itemId"></param>
    ''' <returns></returns>
    Public Async Function PrintCostsPerCurrencies(itemId As Integer) As Task
        Try
            Using Model As New MFixedAssetEquipment
                Dim result = Await Model.GetEquipmentLastCostPerCurrency(itemId)
                If result?.ObjectEmbbeded Is Nothing OrElse Not result.StateResult OrElse Not result.ObjectEmbbeded.Any() Then
                    Me.PrintSingleCost(0)
                    Return
                End If

                Dim dataSource = result.ObjectEmbbeded

                If dataSource.Count > 1 Then
                    INDgcLastCost.DataSource = dataSource
                    PrintLastCost()
                    Return
                End If

                PrintSingleCost(dataSource.FirstOrDefault.Item1)
                Return
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Click event of the CtrAdvanceTreasury control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrAdvanceTreasury_Click(sender As Object, e As EventArgs)
        If INDgcLastCost.DataSource IsNot Nothing Then
            INDpceLastCost.ShowPopup()
        End If
    End Sub
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.INDlblLastCostTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliLastCostTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblLastCostTitle)
    End Sub

End Class