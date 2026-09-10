'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Luis Felipe Pantoja Cerquera
' Created          : 17-08-2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************




Imports DevExpress.Xpo
Imports DevExpress.XtraLayout.Utils
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls

Public Class FrmPatientDeparture
    Implements IPatientDeparture


#Region "Properties"
    Public Sub New()
        InitializeComponent()
        IndigoGridControl1.SetHideNoRecords(INDGcPatientDeparture, True)
    End Sub

    ''' <summary>
    ''' Variable que se utiliza para instanciar el presentador
    ''' </summary>
    Dim Presenter As PPatientDeparture
    ''' <summary>
    ''' Variable que se utiliza para instanciar el presentador
    ''' </summary>
    Dim ListUnitFunctional As List(Of String)

    Public Property InContainer As Boolean = False

    ''' <summary>
    ''' ActionsOnControls
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPatientDeparture.ActionsOnControls
        Set(value As Boolean)
            INDSleCareCenter.Enabled = value
            INDpceFunctionalUnit.Enabled = Not value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Unidades Funcionales
    ''' </summary>
    Public Property DataSourceUnitFunctional As XPCollection(Of ViewUnitFunctionalHis) Implements IPatientDeparture.DataSourceUnitFunctional
        Get
            Return INDgcFunctionalUnit.DataSource
        End Get
        Set(value As XPCollection(Of ViewUnitFunctionalHis))
            INDgcFunctionalUnit.DataSource = value
            Me.INDgcFunctionalUnit.RefreshDataSource()
        End Set
    End Property

    Public Property DataSourceUnitFunctionalList As List(Of SP_ListFunctionalUnitHis_Result) Implements IPatientDeparture.DataSourceUnitFunctionalList
        Get
            Return INDgcFunctionalUnit.DataSource
        End Get
        Set(value As List(Of SP_ListFunctionalUnitHis_Result))
            INDgcFunctionalUnit.DataSource = value
            INDgcFunctionalUnit.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de pacientes egresados
    ''' </summary>
    Public Property PatientDepartureXPO As XPInstantFeedbackSource Implements IPatientDeparture.PatientDepartureXPO
        Get
            Return CType(INDGcPatientDeparture.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDGcPatientDeparture.DataSource = value
            ' Me.INDGcPatientDeparture.RefreshDataSource()
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que modifica el texto el popup de unidades funcionales
    ''' </summary>
    Public Property UnitFunctional As String Implements IPatientDeparture.UnitFunctional
        Get
            Return INDpceFunctionalUnit.Text
        End Get
        Set(value As String)
            INDpceFunctionalUnit.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que contiene el codigo del centro de atencion
    ''' </summary>
    Public Property CareCenterCode As String Implements IPatientDeparture.CareCenterCode
        Get
            Return INDSleCareCenter.EditValue
        End Get
        Set(value As String)
            INDSleCareCenter.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el datasource de los centros de atencion
    ''' </summary>
    Public Property CareCenterXPO As XPInstantFeedbackSource Implements IPatientDeparture.CareCenterXPO
        Get
            Return CType(INDSleCareCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property CareCenterList As List(Of SP_ListCareCenterHis_Result) Implements IPatientDeparture.CareCenterList
        Get
            Return INDSleCareCenter.Properties.DataSource
        End Get
        Set(value As List(Of SP_ListCareCenterHis_Result))
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que controla los mensajes
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListUnitFunctional = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se lanza cuando se modifica el centro de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareCenter.EditValueChanged
        If INDSleCareCenter.EditValue IsNot Nothing AndAlso Not InContainer Then
            ActionsOnControls = False
            INDpceFunctionalUnit.ShowPopup()
        End If

    End Sub

    ''' <summary>
    ''' Evento que se lanza al dar click en el control de centro de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleCareCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareCenter.QueryPopUp
        'If CareCenterXPO Is Nothing Then
        '    Using model As New MPatientDeparture(MyTag)
        '        CareCenterXPO = model.ListCentersHIS()
        '    End Using
        'End If
        If CareCenterList Is Nothing Then
            INDviewSlCareCenter.ShowLoadingPanel()
            Using model As New MPatientDeparture(MyTag)
                Dim result As ActionResult(Of List(Of SP_ListCareCenterHis_Result)) = Await model.SP_ListCareCenterHis()
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                INDviewSlCareCenter.HideLoadingPanel()
                CareCenterList = result.ObjectEmbbeded
            End Using
        End If
    End Sub

    ''' <summary>
    ''' abrir busqueda de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Async Sub INDpceFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceFunctionalUnit.QueryPopUp
        'If DataSourceUnitFunctional Is Nothing Then
        '    Using model As New MPatientDeparture(MyTag)
        '        DataSourceUnitFunctional = model.ListUnitFunctionalHIS(CareCenterCode)
        '    End Using
        '    LayoutControlItem5.Visibility = If(DataSourceUnitFunctional.Count = 0, LayoutVisibility.Never, LayoutVisibility.Always)
        'End If
        If DataSourceUnitFunctionalList Is Nothing Then
            INDgvUnitFunctional.ShowLoadingPanel()
            Using model As New MPatientDeparture(MyTag)
                Dim result As ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)) = Await model.SP_ListFunctionalUnitHis(CareCenterCode)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                INDgvUnitFunctional.HideLoadingPanel()
                DataSourceUnitFunctionalList = result.ObjectEmbbeded
                LayoutControlItem5.Visibility = If(DataSourceUnitFunctionalList.Count = 0, LayoutVisibility.Never, LayoutVisibility.Always)
            End Using
        End If
    End Sub
    Private Sub INDchkSelectAll_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkSelectAll.EditValueChanged
        If DataSourceUnitFunctionalList IsNot Nothing AndAlso DataSourceUnitFunctionalList.Count > 0 Then
            If INDchkSelectAll.EditValue = True Then
                For Each item As SP_ListFunctionalUnitHis_Result In DataSourceUnitFunctionalList
                    item.State = True
                Next
            Else
                For Each item As SP_ListFunctionalUnitHis_Result In DataSourceUnitFunctionalList
                    item.State = False
                Next
            End If
            INDgcFunctionalUnit.RefreshDataSource()
        End If
    End Sub

#Region "Load"
    ''' <summary>
    ''' Clean Controls
    ''' </summary>
    Private Sub FrmPatientDeparture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDGcPatientDeparture)
        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Cuentas)
        IndigoGridView1.SetListAcction(INDGvPatientDeparture, listAction)

        Me.indigo = SessionValues.Instance
        Presenter = New PPatientDeparture(Me)

        Deshacer()

        If InContainer Then
            INDLciCentroAtencion.Visibility = LayoutVisibility.Never
            INDLciClear.Visibility = LayoutVisibility.Always
            INDpceFunctionalUnit.Enabled = True
            INDSleCareCenter_EditValueChanged(Nothing, Nothing)
        End If
    End Sub


#End Region
    ''' <summary>
    ''' Evento que se genera al dar click en el boton cargar
    ''' </summary>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        ListUnitFunctional = New List(Of String)
        For Each item As SP_ListFunctionalUnitHis_Result In DataSourceUnitFunctionalList
            If item.State Then
                ListUnitFunctional.Add("'" & item.Codigo & "'")
            End If
        Next
        INDpceFunctionalUnit.ClosePopup()
        If ListUnitFunctional.Count > 0 Then
            UnitFunctional = "Unidades Funcionales Seleccionadas [" & CStr(ListUnitFunctional.Count) & "]"
        Else
            UnitFunctional = String.Empty
            PatientDepartureXPO = Nothing
            INDGcPatientDeparture.RefreshDataSource()
        End If

        If Not ValidateControls() Then
            Exit Sub
        End If

        Dim UnitFunctionalCodes As String = String.Join(",", ListUnitFunctional)
        Using model As New MPatientDeparture(MyTag)
            AsyncLoader(True)
            PatientDepartureXPO = model.ListPatientDepartureHIS(CareCenterCode, UnitFunctionalCodes)
            AsyncLoader(False)
        End Using
        IndigoGridControl1.RefreshGrid(INDGcPatientDeparture)
    End Sub

    ''' <summary>
    ''' Evento que se lanza cuando se abre el formulario
    ''' </summary>

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDSleCareCenter.Text Is String.Empty Then
            INDSleCareCenter.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se lanza cuando se da click en la accion de control de cuentas
    ''' </summary>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim FunctionalDeparture = DirectCast(DirectCast(INDGvPatientDeparture.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CrystalRepository.ViewPatientDeparture)
        OpenForm("776", FunctionalDeparture.NumIng, True)
    End Sub

#End Region

#Region "Methods"
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPatientDeparture.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IPatientDeparture.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Clean Controls
    ''' </summary>
    Private Sub CleanControls()
        INDlayoutControlRoot.BeginUpdate()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing

        ListUnitFunctional = Nothing
        DataSourceUnitFunctional = Nothing
        DataSourceUnitFunctionalList = Nothing
        'CareCenterXPO = Nothing
        CareCenterList = Nothing
        PatientDepartureXPO = Nothing
        UnitFunctional = String.Empty
        ActionsOnControls = True
        If Not InContainer Then
            CareCenterCode = Nothing
        Else
            INDpceFunctionalUnit.Enabled = True
        End If
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        INDpceFunctionalUnit.ClosePopup()
        INDlayoutControlRoot.EndUpdate()
        INDSleCareCenter.Focus()
    End Sub


#End Region

#Region "Barra Botones"
    Dim FlagValidar As Boolean = False

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer, INDsbClear.Click
        Me.Deshacer()
    End Sub

#End Region

End Class