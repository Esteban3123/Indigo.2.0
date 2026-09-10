'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 23-06-2015
'
' Last Modified By : 
' Last Modified On :  
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports System.Text
Imports DevExpress.Xpo
Imports System.Threading
Imports System.IO
Imports Presentation.Controls.MVP

#End Region

Public Class FrmInvoiceValidate

#Region "Variables"
    ''' <summary>
    ''' Lista de Radicado de rejilla principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEntityRadicateD As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Lista de facturas sin validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListInvoiceWithoutValidate As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Lista de Facturas no Encontradas en la validación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListInvoiceNotFound As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MRadicateInvoice
    ''' <summary>
    ''' Variable que contiene los valores de session
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Nit de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _nit As String
    ''' <summary>
    ''' Contenedor de BD
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ObjCompany As GlosasParametersInterface
    
    ''' <summary>
    ''' Evento que se dispara para remover las facturas restantes del datasource de las facturas a radicar
    ''' </summary>
    Public Event RemoveFromRadicateList(sender As Object, e As RemoveListRadicateEventArgs)
#End Region

#Region "Propertys"


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

    Public WriteOnly Property DatasourceListRadicateD As List(Of RadicateInvoiceD)
        Set(value As List(Of RadicateInvoiceD))
            ListEntityRadicateD = value
            ListInvoiceWithoutValidate = New List(Of RadicateInvoiceD)
            ListInvoiceWithoutValidate.AddRange(value)
            DataSourceListInvoiceWithoutValidate = ListInvoiceWithoutValidate
            ListInvoiceNotFound = New List(Of RadicateInvoiceD)
        End Set
    End Property

    Private Property DataSourceListInvoiceWithoutValidate() As List(Of RadicateInvoiceD)
        Get
            Return Me.INDgcListInvoiceWithoutValidate.DataSource
        End Get
        Set(value As List(Of RadicateInvoiceD))
            Me.INDgcListInvoiceWithoutValidate.DataSource = value
            Me.INDgcListInvoiceWithoutValidate.RefreshDataSource()
        End Set
    End Property

    Private Property DataSourceListInvoiceNotFound As List(Of RadicateInvoiceD)
        Get
            Return Me.INDgcListInvoiceNotFound.DataSource
        End Get
        Set(value As List(Of RadicateInvoiceD))
            Me.INDgcListInvoiceNotFound.DataSource = value
            Me.INDgcListInvoiceNotFound.RefreshDataSource()
        End Set
    End Property

    Public WriteOnly Property Nit As String
        Set(value As String)
            _nit = value
        End Set
    End Property

    Public WriteOnly Property Containers As GlosasParametersInterface
        Set(value As GlosasParametersInterface)
            _ObjCompany = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Private Sub FrmInvoiceValidate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListEntityRadicateD = Nothing
        ListInvoiceWithoutValidate = Nothing
        ListInvoiceNotFound = Nothing
        Model = Nothing
        _nit = Nothing
        _ObjCompany = Nothing
    End Sub

    Private Sub FrmInvoiceValidate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolBar.Visible = False
        Model = New MRadicateInvoice("509")
        Me.Indigo = SessionValues.Instance
        TabbedControlGroup1.SelectedTabPageIndex = 0
        INDtxtInvoiceValidate.Focus()
    End Sub

    Private Sub INDtxtInvoiceValidate_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtInvoiceValidate.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDtxtInvoiceValidate.Text <> String.Empty Then
                Dim itemInvoice As RadicateInvoiceD = ListEntityRadicateD.Where(Function(x) x.InvoiceNumber = INDtxtInvoiceValidate.Text.Trim).SingleOrDefault()
                If itemInvoice IsNot Nothing AndAlso itemInvoice.Id > 0 Then
                    ListInvoiceWithoutValidate.Remove(itemInvoice)
                    DataSourceListInvoiceWithoutValidate = ListInvoiceWithoutValidate
                Else
                    AddInvoiceNotFound()
                End If
                INDtxtInvoiceValidate.Text = String.Empty
                INDtxtInvoiceValidate.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Debe Ingresa un número de factura"
            End If
        End If
    End Sub

    Private Sub BtnRemoteFromList_Click(sender As Object, e As EventArgs) Handles BtnRemoteFromList.Click
        If ListInvoiceWithoutValidate IsNot Nothing AndAlso ListInvoiceWithoutValidate.Count > 0 Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                RaiseEvent RemoveFromRadicateList(Me, New RemoveListRadicateEventArgs() With {.ListToRemoveInvoiceNumber = ListInvoiceWithoutValidate.Select(Function(x) x.InvoiceNumber).Distinct().ToList()})
                Me.Close()
            End If
        End If
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Agrega un item a la rejilla de facturas no encontradas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub AddInvoiceNotFound()
        Try
        Dim itemInvoice As SP_invoiceList_Result = Nothing
        If _nit IsNot Nothing Then
            If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                If _ObjCompany IsNot Nothing Then
                        Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(INDtxtInvoiceValidate.Text.Trim(), _ObjCompany.AccountingMethod)
                    AsyncLoader(True)
                        itemInvoice = Await Model.GetInvoice(_ObjCompany.ContainerName, _nit, invoiceNumberFixed, String.Empty, 1)
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                End If
            Else
                AsyncLoader(True)
                itemInvoice = Await Model.GetInvoice(String.Empty, _nit, INDtxtInvoiceValidate.Text.Trim(), String.Empty, 1)
                AsyncLoader(False)
            End If
            If itemInvoice IsNot Nothing AndAlso itemInvoice.InvoiceNumber IsNot Nothing Then
                Dim _TmpObjectionsReceptionD As New RadicateInvoiceD
                With _TmpObjectionsReceptionD
                    .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                    .GlosasParametersInterfaceId = Nothing
                    .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                    .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                    .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                    .InvoiceDate = itemInvoice.InvoiceDate
                    .IngressDate = itemInvoice.IngressDate
                    .IngressNumber = itemInvoice.IngressNumber.Trim
                    .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                    .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                    .PatientCode = If(itemInvoice.PatientCode Is Nothing, String.Empty, itemInvoice.PatientCode.Trim)
                    .ContractCode = itemInvoice.ContractCode.Trim
                    .PlanCode = itemInvoice.CodePlan
                    .ContractEntity = itemInvoice.ContractEntity
                    .PatientName = If(itemInvoice.PatientName Is Nothing, String.Empty, itemInvoice.PatientName.Trim)
                    .State = "1"
                    .CreditNoteValue = itemInvoice.CreditNoteValue
                    .DebitNoteValue = itemInvoice.DebitNoteValue
                    .Devolution = itemInvoice.Devolution
                    .ConceptDevolution = itemInvoice.ConceptDevolution
                End With
                'valido que la factura agregar no se encuentre ya en la lista
                    Dim List = From c In ListInvoiceNotFound Where c.InvoiceNumber = _TmpObjectionsReceptionD.InvoiceNumber
                If List.Count = 0 Then
                    ListInvoiceNotFound.Add(_TmpObjectionsReceptionD)
                End If
                    DataSourceListInvoiceNotFound = ListInvoiceNotFound
                    Dim Count As Integer = DataSourceListInvoiceNotFound.Count()
                    INDlygInvoiceRadicateNotFound.Text = String.Format(INDlygInvoiceRadicateNotFound.Text, Count.ToString)
            Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoseEncuentraFactura, RecepcionObjeciones)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
#End Region

#Region "constructor"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
#End Region
    
End Class