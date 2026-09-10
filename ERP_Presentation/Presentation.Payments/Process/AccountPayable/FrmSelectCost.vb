'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/12/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports Domain.Entities.Service
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance.MVP
Imports DevExpress.XtraEditors
Imports Presentation.Common.MVP
Imports DevExpress.Xpo
Imports System.Text
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Class FrmSelectCost

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPopupBills

    ''' <summary>
    ''' Listado de distribuciones de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpo As List(Of CostDistributionDirectCostXpo)

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddSelectCostEventArgs(sender As Object, e As AddSelectCost)

#End Region

#Region "Properties"

    Private _documentDate As DateTime
    ''' <summary>
    ''' Fecha del documento que viene desde la cabecera de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime
        Get
            Return _documentDate
        End Get
        Set(value As DateTime)
            _documentDate = value
        End Set
    End Property

    Private _thirdPartyId As Integer
    ''' <summary>
    ''' Tercero que viene desde la cabecera, se saca del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer
        Get
            Return _thirdPartyId
        End Get
        Set(value As Integer)
            _thirdPartyId = value
        End Set
    End Property

    Private _value As Decimal
    ''' <summary>
    ''' Valor que hace referencia al valor facturado de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal
        Get
            Return _value
        End Get
        Set(value As Decimal)
            _value = value
        End Set
    End Property

    Private _costDistributionDirectCostId As Integer?
    Public Property CostDistributionDirectCostId As Integer?
        Get
            Return _costDistributionDirectCostId
        End Get
        Set(value As Integer?)
            _costDistributionDirectCostId = value
        End Set
    End Property


    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    Private _accountPayableId As Integer
    ''' <summary>
    ''' Id de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableId As Integer
        Get
            Return _accountPayableId
        End Get
        Set(value As Integer)
            _accountPayableId = value
        End Set
    End Property

    Private _listAccountPayable As List(Of AccountPayable)
    ''' <summary>
    ''' Listado para validar que la cxp han sido agregadas tengan o no el id de la distribución de elementos del costo para no mostrar este item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListAccountPayable As List(Of AccountPayable)
        Get
            Return _listAccountPayable
        End Get
        Set(value As List(Of AccountPayable))
            _listAccountPayable = value
        End Set
    End Property

    Private _billNumber As String
    ''' <summary>
    ''' Permite saber el no. de factura para realizar la validación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String
        Get
            Return _billNumber
        End Get
        Set(value As String)
            _billNumber = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadData() As Task
        INDgcData.DataSource = Nothing
        Await ValidateInfo()
        INDgcData.DataSource = ListXpo
    End Function

    ''' <summary>
    ''' Valida la info del listado
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateInfo() As Task
        Return Task.Factory.StartNew(AddressOf CompareList)
    End Function

    ''' <summary>
    ''' Compara la lista para realizar las validaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CompareList()
        'Se consulta con xpo una lista de distribuciones de elementos del costo asociada a la cuenta por pagar
        ListXpo = Presenter.ListCostDistributionDirectCost(AccountPayableId)
    End Sub

    ''' <summary>
    ''' Método que devuelve la entidad xpo que se seleccionó en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOption()
        If Not (ListXpo IsNot Nothing AndAlso ListXpo.Count > 0) Then
            Exit Sub
        End If

        'Se valida que se haya escogido un item
        If (From x In ListXpo Where x.SelectOption = True Select x).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una opción"
            Exit Sub
        End If

        'Se retorna la entidad que se seleccionó
        Dim args As New AddSelectCost
        args.CostDistributionDirectCostXpo = (From x In ListXpo Where x.SelectOption = True Select x).FirstOrDefault
        RaiseEvent AddSelectCostEventArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListXpo = Nothing
        _documentDate = Nothing
        _thirdPartyId = Nothing
        _accountPayableId = Nothing
        _listAccountPayable = Nothing
        _billNumber = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectCost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PPopupBills()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim infoXpo As CostDistributionDirectCostXpo = INDviewData.GetFocusedRow
            If infoXpo IsNot Nothing Then
                If e.NewValue Then
                    ListXpo.ForEach(Sub(item) item.SelectOption = False)
                End If
                infoXpo.SelectOption = e.NewValue
                INDgcData.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectCost_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar el botón de seleccionar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnSelect_Click(sender As Object, e As EventArgs) Handles INDbtnSelect.Click
        SelectOption()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSelectCost_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await LoadData()
    End Sub

#End Region

#End Region

End Class