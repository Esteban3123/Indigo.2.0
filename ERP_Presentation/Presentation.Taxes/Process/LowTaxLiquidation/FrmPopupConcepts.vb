'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Jhossept kevin Garay
' Created          : 27-08-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class FrmPopupConcepts
#Region "BUILDER"
    Sub New()
        InitializeComponent()
        
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddLowTaxliquidationDetail(sender As Object, e As AddLowTaxLiquidationEventArgs)
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Taxes"
    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim lowTaxLiquidationDetail As LowTaxLiquidationDetail

    Public lowTaxLiquidationDetailEdit As LowTaxLiquidationDetail

    Dim _listDetails As List(Of LowTaxLiquidationDetail)

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    Dim _UVTValue As Decimal

    Dim Rate As Decimal
#End Region

#Region "PROPERTIES"

    Public Property UVTValue As Decimal
        Get
            Return _UVTValue
        End Get
        Set(value As Decimal)
            _UVTValue = value
        End Set
    End Property

    Dim _listConcepts As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListConcepts As List(Of Tuple(Of Byte, String))
        Get
            If _listConcepts Is Nothing Then
                _listConcepts = New List(Of Tuple(Of Byte, String))
                _listConcepts.Add(New Tuple(Of Byte, String)(1, "Pasacalles"))
                _listConcepts.Add(New Tuple(Of Byte, String)(2, "Avisos no adosados a la pared inferior a 8 mestros cuadrados"))
                _listConcepts.Add(New Tuple(Of Byte, String)(3, "Pendones y festones"))
                _listConcepts.Add(New Tuple(Of Byte, String)(4, "Afiches y Volantes"))
            End If
            Return _listConcepts
        End Get
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Property ListDetails As List(Of LowTaxLiquidationDetail)
        Get
            Return _listDetails
        End Get
        Set(value As List(Of LowTaxLiquidationDetail))
            _listDetails = value
        End Set
    End Property

#End Region

#Region "METHODS"



    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        lowTaxLiquidationDetail = Nothing
        INDgleConcept.Enabled = True
        INDgleConcept.Properties.DataSource = ListConcepts
        INDgleConcept.Properties.PopupFormSize = New Size(600, 300)
        INDgleConcept.EditValue = Nothing
        ActionsControls = False
        INDgleConcept.Focus()
        INDseTimeQuantity.EditValue = 0
        INDseQuantity.EditValue = 0
        INDteRate.EditValue = 0
        INDteTaxTotalValue.EditValue = 0

        'traer datos de UVT

    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String

    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            lowTaxLiquidationDetail = New LowTaxLiquidationDetail
            With lowTaxLiquidationDetail
                .Concept = INDgleConcept.EditValue
                If .Concept = 2 Then
                    .NumberOfDays = INDseTimeQuantity.EditValue * 30
                Else
                    .NumberOfDays = INDseTimeQuantity.EditValue
                End If
                .Quantity = INDseQuantity.EditValue
                '.TotalValueTax = Rate * INDseQuantity.EditValue * INDseTimeQuantity.EditValue
                .TotalValueTax = CDec(INDteTaxTotalValue.EditValue)
            End With
        Else
            With lowTaxLiquidationDetailEdit
                If .Concept = 2 Then
                    .NumberOfDays = INDseTimeQuantity.EditValue * 30
                Else
                    .NumberOfDays = INDseTimeQuantity.EditValue
                End If
                .Quantity = INDseQuantity.EditValue
                '.TotalValueTax = Rate * INDseQuantity.EditValue * INDseTimeQuantity.EditValue
                .TotalValueTax = CDec(INDteTaxTotalValue.EditValue)
            End With
        End If
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional detailTemp As LowTaxLiquidationDetail = Nothing)
        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If
        INDgleConcept.EditValue = lowTaxLiquidationDetailEdit.Concept
        If INDgleConcept.EditValue = 2 Then
            INDseTimeQuantity.EditValue = lowTaxLiquidationDetailEdit.NumberOfDays / 30
        Else
            INDseTimeQuantity.EditValue = lowTaxLiquidationDetailEdit.NumberOfDays
        End If
        INDseQuantity.EditValue = lowTaxLiquidationDetailEdit.Quantity

    End Sub

#End Region

#Region "HANDLES"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        lowTaxLiquidationDetail = Nothing
        lowTaxLiquidationDetailEdit = Nothing
        _listDetails = Nothing
        _editMode = Nothing
        _UVTValue = Nothing
        Rate = Nothing
    End Sub

    Private Sub FrmPopupProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CleanControls()
        If _editMode = True Then
            'si esta editando un registro
            LoadControls()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupProduct_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If lowTaxLiquidationDetail IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Activated"

#End Region

#Region "SelectProduct"

#End Region

#Region "QueryPopUp"

#End Region

#Region "ButtonClick"

#End Region

#Region "ChangeQuantity"

#End Region

#Region "Closed"

#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        INDBtnAdd.Enabled = False

        If ValidateForm() = False Then
            INDBtnAdd.Enabled = True
            Exit Sub
        End If
        SetValues()
        If ListDetails Is Nothing Then ListDetails = New List(Of LowTaxLiquidationDetail)
        Dim args As New AddLowTaxLiquidationEventArgs
        If _editMode = True Then
            'ListDetails.Add(lowTaxLiquidationDetailEdit)
            args.EditMode = True
            args.entity = lowTaxLiquidationDetailEdit
        Else
            ListDetails.Add(lowTaxLiquidationDetail)
            args.entity = lowTaxLiquidationDetail
        End If
        RaiseEvent AddLowTaxliquidationDetail(Nothing, args)
        INDBtnAdd.Enabled = True
        CleanControls()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmPopupProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        LoadControls(Record)
    End Sub
#End Region

#Region "EditValueChanged"

#End Region

#Region "Popup"
#End Region
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub
#End Region

    Private Sub INDgleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleConcept.EditValueChanged
        If INDgleConcept.EditValue IsNot Nothing Then
            INDgleConcept.Enabled = False
            INDseTimeQuantity.Properties.MaxValue = 0
            INDlciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Select Case INDgleConcept.EditValue
                Case Is = 1
                    'pasacalles, se debe limitar la cantidad de días a 30
                    INDseTimeQuantity.Properties.MaxValue = 30
                    INDlciTimeQuantity.Text = "Cantidad de Tiempo (Días)"
                    'la tarifa es 4 UVT por mes o fracción de mes
                    Rate = (4 * _UVTValue) / 30
                    INDteRate.EditValue = Rate
                Case Is = 2
                    'avisos no adosados a la pared inferior a 8 mestros caudrados
                    INDlciTimeQuantity.Text = "Cantidad de Tiempo (Meses)"
                    'la tarifa es 20 UVT por año o fracción de año, tomamos los datos en meses
                    Rate = (20 * _UVTValue) / 12
                    INDteRate.EditValue = Rate
                Case Is = 3
                    INDlciTimeQuantity.Text = "Cantidad de Tiempo (Días)"
                    'la tarifa es 3 UVT por mes o fracción de mes
                    Rate = (4 * _UVTValue) / 30
                    INDteRate.EditValue = Rate
                Case Is = 4
                    INDlciTimeQuantity.Text = "Cantidad de Tiempo (Días)"
                    Rate = 0
                    INDseTimeQuantity.Properties.MaxValue = 30
                    INDlciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Select
        End If
    End Sub

    Private Sub CalculateTaxesTotalValue(sender As Object, e As EventArgs) Handles INDseTimeQuantity.EditValueChanged, INDseQuantity.EditValueChanged
        Dim totalValue As Decimal = 0
        totalValue = Rate * INDseQuantity.EditValue * INDseTimeQuantity.EditValue
        INDteTaxTotalValue.EditValue = Utils.RoundValue(CDec(totalValue), Utils.RoundLevel.Hundred)
    End Sub

    Function ValidateForm() As Boolean
        ValidateForm = True

        If _editMode = False AndAlso ListDetails IsNot Nothing AndAlso ListDetails.FindAll(Function(x) x.Concept = INDgleConcept.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se agregó detalle de impuesto por este concepto"
            ValidateForm = False
        End If

    End Function
End Class