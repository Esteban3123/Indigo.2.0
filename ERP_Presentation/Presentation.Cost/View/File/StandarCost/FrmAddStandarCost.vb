'***********************************************************************
' Assembly         : Presentation.Cost
' Author           : Juan David Capera
' Created          : 15/12/2023
'
' Last Modified By : Andrés Steven Rojas
' Last Modified On : 31/10/2024
' Description      : Refactorización del formulario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports System.Text
Imports Infrastructure.Data.Xpo.CostRepository
Imports DevExpress.Xpo
#End Region

Public Class FrmAddStandarCost
    Implements ICrudBase

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#End Region

#Region "PublicEvents"

    ''' <summary>
    ''' Evento publico para agregar información a la rejilla del form principal
    ''' </summary>
    ''' <param name="flagEdit"></param>
    ''' <param name="costStandarDetails"></param>
    Public Event AddInfoToGridFormPrincipal(flagEdit As Boolean, costStandarDetails As List(Of StandarCostDetails))

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la actividad
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivityId As Integer
        Get
            Return INDSleActivity.EditValue
        End Get
        Set(value As Integer)
            INDSleActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del activo fijo
    ''' </summary>
    ''' <returns></returns>
    Public Property FixedAssetValue As Decimal
        Get
            Return INDtxtFixedAsset.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFixedAsset.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de nómina
    ''' </summary>
    ''' <returns></returns>
    Public Property PayrollValue As Decimal
        Get
            Return INDtxtPayroll.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPayroll.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de inventario
    ''' </summary>
    ''' <returns></returns>
    Public Property InventoryValue As Decimal
        Get
            Return INDtxtInventory.EditValue
        End Get
        Set(value As Decimal)
            INDtxtInventory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del costo adicional
    ''' </summary>
    ''' <returns></returns>
    Public Property AdditionalCost As Decimal
        Get
            Return INDtxtAdditionalCost.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAdditionalCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property AverageCost As Decimal
        Get
            Return INDtxtAverageCost.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAverageCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property Observations As String
        Get
            Return INDTxtObservation.EditValue
        End Get
        Set(value As String)
            INDTxtObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property ActivityXPO As XPInstantFeedbackSource
        Get
            Return INDSleActivity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece si el registro es para modificar o guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property flagEdit As Boolean

    ''' <summary>
    ''' Variable que contiene la entidad
    ''' </summary>
    ''' <returns></returns>
    Property stanCostDetail As StandarCostDetails

    ''' <summary>
    ''' Listado de la entidad - Se instancia cuando se agregan nuevos registros
    ''' </summary>
    Private ListOfStandarCostDetails As List(Of StandarCostDetails)

    ''' <summary>
    ''' Parametrizacion de la moneda 
    ''' </summary>
    Private _currencyAbbreviation As String
    Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el código y nombre de la actividad
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CodeNameActivity As String
        Get
            Return INDSleActivity.Text
        End Get
    End Property

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

#Region "NoImplemented"
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub


    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub
#End Region

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

#Region "Methods"

    ''' <summary>
    ''' Carga los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If Me.stanCostDetail IsNot Nothing Then
            With Me.stanCostDetail
                ActivityId = .CostActivityId
                FixedAssetValue = .FixedAssetValue
                PayrollValue = .PayrollValue
                InventoryValue = .InventoryValue
                AdditionalCost = .AdditionalCost
                AverageCost = .StandarCostValue
                Observations = .Observation
            End With
        End If
    End Sub

    ''' <summary>
    ''' Asigna valores a la entidad
    ''' </summary>
    Private Sub AssignValues()
        If Me.stanCostDetail Is Nothing Then Me.stanCostDetail = New StandarCostDetails
        With Me.stanCostDetail
            .CostActivityId = ActivityId
            .CodeNameActivity = CodeNameActivity
            .FixedAssetValue = FixedAssetValue
            .PayrollValue = PayrollValue
            .InventoryValue = InventoryValue
            .AdditionalCost = AdditionalCost
            .StandarCostValue = AverageCost
            .Observation = Observations
        End With
        If Me.ListOfStandarCostDetails Is Nothing Then Me.ListOfStandarCostDetails = New List(Of StandarCostDetails)
        Me.ListOfStandarCostDetails.Add(Me.stanCostDetail)
    End Sub

    ''' <summary>
    ''' Metodo que agrega la regla al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        ''Se valida que los controles esten llenos
        If Not ValidateControls() OrElse Not ValidateAdditionalControls() Then
            Exit Sub
        End If

        AsyncLoader(True)
        AssignValues()
        RaiseEvent AddInfoToGridFormPrincipal(Me.flagEdit, Me.ListOfStandarCostDetails)
        CleanControls()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Validar controles adicionales
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateAdditionalControls() As Boolean
        Dim errors As New StringBuilder()
        If String.IsNullOrEmpty(INDSleActivity.Text) Then
            errors.AppendLine("Debe seleccionar alguna Actividad")
        End If
        If AverageCost = 0 Then
            errors.AppendLine("El costo promedio no puede estar en 0")
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        Else
            Return True
        End If
    End Function


    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActivityId = Nothing
        FixedAssetValue = 0
        PayrollValue = 0
        InventoryValue = 0
        AdditionalCost = 0
        AverageCost = 0
        Observations = Nothing
    End Sub

    ''' <summary>
    ''' Carga el DataSource del campo de Actividades
    ''' </summary>
    Private Async Sub LoadActiveXpo()
        If ActivityXPO Is Nothing Then
            ActivityXPO = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListActiveCostActivities())
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddStandarCost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAddRule, True)
        BarraBotones.PrepareToolbar(eAction.OnlyHideAudit)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Customizar) = True
        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)
        LoadActiveXpo()
        If flagEdit Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que asigna el valor de Costo Estándar Promedio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SetAverageCost(sender As Object, e As EventArgs) Handles INDtxtFixedAsset.EditValueChanged, INDtxtPayroll.EditValueChanged, INDtxtInventory.EditValueChanged, INDtxtAdditionalCost.EditValueChanged
        'Convertir a valores positivos
        FixedAssetValue = Math.Abs(FixedAssetValue)
        PayrollValue = Math.Abs(PayrollValue)
        InventoryValue = Math.Abs(InventoryValue)
        AdditionalCost = Math.Abs(AdditionalCost)
        AverageCost = FixedAssetValue + PayrollValue + InventoryValue + AdditionalCost
    End Sub
#End Region

#Region "key"
    ''' <summary>
    ''' keydown event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddStandarCost_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRule_Click(sender As Object, e As EventArgs) Handles INDbtnAddRule.Click
        AddDetail()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Carga el dataSource de las actividades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleActivity.QueryPopUp
        LoadActiveXpo()
    End Sub
#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load

    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

End Class
