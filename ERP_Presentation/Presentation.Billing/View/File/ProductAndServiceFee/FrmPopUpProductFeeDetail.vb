'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 18-05-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPopUpProductFeeDetail
    Implements IProductFeeDetail

#Region "Events"

    Public Event AddProductFeeDetail(sender As Object, e As AddProductFeeEventArgs)

#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa a la entidad de detalle de tarifa de productos
    ''' </summary>
    Public _productFeeDetail As ProductFeeDetail

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    Private _presenter As PProductFeeDetail

    ''' <summary>
    ''' Representa la lista de los detalles ya agregados
    ''' </summary>
    Public ListProductFeeDetails As List(Of ProductFeeDetail)
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el Id del producto 
    ''' </summary>
    Public Property ProductId As Integer Implements IProductFeeDetail.ProductId
        Get
            Return INDsleProduct.EditValue
        End Get
        Set(value As Integer)
            INDsleProduct.EditValue = value
        End Set
    End Property

#End Region

#Region "DataSource"

    Private _rateType As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property RateTypeList As List(Of Tuple(Of Boolean, String))
        Get
            If _rateType Is Nothing Then
                _rateType = New List(Of Tuple(Of Boolean, String))
                _rateType.Add(New Tuple(Of Boolean, String)(False, "Tarifa Fija"))
                _rateType.Add(New Tuple(Of Boolean, String)(True, "Porcentaje"))
            End If
            Return _rateType
        End Get
    End Property

    Private _percentageType As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property PercentageType As List(Of Tuple(Of Boolean, String))
        Get
            If _percentageType Is Nothing Then
                _percentageType = New List(Of Tuple(Of Boolean, String))
                _percentageType.Add(New Tuple(Of Boolean, String)(False, "Costo Promedio Ponderado"))
                _percentageType.Add(New Tuple(Of Boolean, String)(True, "Ultimo Costo"))
            End If
            Return _percentageType
        End Get
    End Property

    ''' <summary>
    ''' Inicializa los controles con valores quemados
    ''' </summary>
    Private Sub InitializeTuples()
        INDsleRateType.Properties.DataSource = RateTypeList
        INDslePercentageType.Properties.DataSource = PercentageType
    End Sub
#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' Handles the Load event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductRateDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.StatusRecordVisible = False
        _presenter = New PProductFeeDetail(Me)
        InitializeTuples()

        If EditMode Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AsyncLoader(True)

        If Not ValidateControls() Then
            AsyncLoader(False)
            Exit Sub
        End If

        Dim errors = ValidateControlsForms()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            AsyncLoader(False)
            Exit Sub
        End If

        AssigningValues()
        RaiseEvent AddProductFeeDetail(Nothing, New AddProductFeeEventArgs With
            {
                .EditMode = EditMode,
                .ProductFeeDetail = _productFeeDetail
            }
        )

        AsyncLoader(False)
        If EditMode Then
            Me.Close()
        End If
        Me.CleanControls()
    End Sub

    Private Sub AssigningValues()

        With _productFeeDetail

            .ProductId = ProductId
            .ProductCodeName = INDsleProduct.Text
            .RateType = INDsleRateType.EditValue
            .RateTypeName = INDsleRateType.Text
            .Observations = INDTextObservations.Text
            .InitialDate = INDsleInitialDate.EditValue
            .FinalDate = INDsleFinalDate.EditValue

            If INDsleRateType.EditValue = False Then
                .SalePrice = INDtxtSalesValue.EditValue
            End If

            If INDsleRateType.EditValue = True Then
                .PercentageType = INDslePercentageType.EditValue
                .PercentageTypeName = INDslePercentageType.Text
                .Percentage = INDsePercentage.EditValue
            End If
        End With
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Maneja la función cuando se le da click al boton (+) del label de productos
    ''' </summary>
    Private Sub INDsleProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
        '    Using Formulario As New FrmProducts
        '        Formulario.ViewModeEditHold = True
        '        Formulario.MinimizeBox = False
        '        Formulario.MaximizeBox = False
        '        Formulario.Size = New Size(780, 700)
        '        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        '        Dim transparent As New FrmTransparent(Formulario, False)
        '        transparent.ShowDialog()
        '    End Using
        'End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductRateDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing Then
            INDsleProduct.Properties.DataSource = _presenter.InitializeProducts()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInitialDate.EditValueChanged
        If INDsleInitialDate.EditValue IsNot Nothing Then
            INDsleFinalDate.Properties.MinValue = INDsleInitialDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Acciones para ocultar elemento o mostrar cuando se cambia el tipo de tarifa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRateType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateType.EditValueChanged
        If INDsleRateType.EditValue IsNot Nothing Then
            If INDsleRateType.EditValue = False Then
                INDlciSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPercentageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf INDsleRateType.EditValue = True Then
                INDLciPercentageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductRateDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleProduct.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        INDbtnAdd.Text = ResourceManager.GetString("Edit")
        With _productFeeDetail

            INDsleProduct.ReadOnly = True
            INDsleProduct.EditValue = .ProductId
            INDsleProduct.Properties.NullText = .ProductCodeName
            INDsleRateType.EditValue = .RateType
            INDslePercentageType.EditValue = .PercentageType
            INDslePercentageType.Properties.NullText = .PercentageTypeName
            INDsePercentage.EditValue = .Percentage
            INDsleInitialDate.EditValue = .InitialDate
            INDsleFinalDate.EditValue = .FinalDate
            INDtxtSalesValue.EditValue = .SalePrice
            INDTextObservations.EditValue = .Observations

        End With
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()

        _rateType = Nothing
        _percentageType = Nothing
        INDsleRateType.EditValue = Nothing
        INDsleProduct.EditValue = Nothing
        INDsleProduct.Properties.NullText = String.Empty
        INDsleInitialDate.EditValue = Nothing
        INDsePercentage.EditValue = Nothing
        INDslePercentageType.EditValue = Nothing
        INDslePercentageType.Properties.NullText = String.Empty
        INDsleFinalDate.EditValue = Nothing
        INDtxtSalesValue.EditValue = Nothing
        INDTextObservations.EditValue = Nothing
        _productFeeDetail = New ProductFeeDetail
        INDLciPercentageType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()

        If INDsleInitialDate.EditValue > INDsleFinalDate.EditValue Then
            errorList.AppendLine("La fecha inicial debe ser menor a la fecha final")
        End If

        If INDsleRateType.EditValue = True Then
            If INDslePercentageType.EditValue Is Nothing Then
                errorList.AppendLine("Debe seleccionar un tipo de porcentaje")
            End If

            If INDsePercentage.EditValue < 0 Or INDsePercentage.EditValue Is Nothing Then
                errorList.AppendLine("Debe indicar un porcentaje")
            End If
        End If

        If INDsleRateType.EditValue = False Then
            If INDtxtSalesValue.EditValue Is Nothing Then
                errorList.AppendLine("Debe colocar un precio de venta")
            End If
        End If

        If Not EditMode Then
            If ListProductFeeDetails.Any(Function(x) x.ProductId = ProductId) Then
                errorList.AppendLine("El Producto " + INDsleProduct.Text + " ya esta agregado")
            End If
        End If

        Return errorList.ToString()
    End Function

#End Region

End Class