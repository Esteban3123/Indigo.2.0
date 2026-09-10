'***********************************************************************
' Assembly         : Presentacion.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/08/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports Presentation.Taxes.MVP

#End Region

Public Class FrmTaxLiquidation
    Implements ITaxLiquidation

#Region "Properties"

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITaxLiquidation.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITaxLiquidation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Año de liquidación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Year As Integer Implements ITaxLiquidation.Year
        Get
            Return CtrDateNavigator1.GetYear
        End Get
    End Property

    ''' <summary>
    ''' Dirección
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Address As String Implements ITaxLiquidation.Address
        Get
            Return INDtxtAddress.EditValue
        End Get
        Set(value As String)
            INDtxtAddress.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Identificación catastral
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CadastralIdentification As String Implements ITaxLiquidation.CadastralIdentification
        Get
            Return INDtxtCadastralIdentification.EditValue
        End Get
        Set(value As String)
            INDtxtCadastralIdentification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OwnerId As Integer Implements ITaxLiquidation.OwnerId
        Get
            Return INDsleOwner.EditValue
        End Get
        Set(value As Integer)
            INDsleOwner.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OwnerXpo As XPInstantFeedbackSource Implements ITaxLiquidation.OwnerXpo
        Get
            Return INDsleOwner.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleOwner.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de predio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PropertyType As Integer Implements ITaxLiquidation.PropertyType
        Get
            Return INDslePropertyType.EditValue
        End Get
        Set(value As Integer)
            INDslePropertyType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Dirección 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Address2 As String Implements ITaxLiquidation.Address2
        Get
            Return INDtxtAddress2.EditValue
        End Get
        Set(value As String)
            INDtxtAddress2.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cédula catastral 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CadastralIdentification2 As String Implements ITaxLiquidation.CadastralIdentification2
        Get
            Return INDtxtCadastralIdentification2.EditValue
        End Get
        Set(value As String)
            INDtxtCadastralIdentification2.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Taxes"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PTaxLiquidation

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListPropertyType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Ids que se generan al guardar los detalles de la liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Dim Ids As String

    ''' <summary>
    ''' Variable para saber desde donde se esta abriendo el form de busqueda de la cedula catastral
    ''' </summary>
    ''' <remarks></remarks>
    Dim TextEditOpen As String

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Validar) = False
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Liquidar Impuestos")
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = False
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Dim varAddress As String = String.Empty
        Dim varCadastralIdentification As String = String.Empty
        Dim varAddress2 As String = String.Empty
        Dim varCadastralIdentification2 As String = String.Empty
        If Address IsNot Nothing Then
            varAddress = Address
        End If
        If CadastralIdentification IsNot Nothing Then
            varCadastralIdentification = CadastralIdentification
        End If
        If Address2 IsNot Nothing Then
            varAddress2 = Address2
        End If
        If CadastralIdentification2 IsNot Nothing Then
            varCadastralIdentification2 = CadastralIdentification2
        End If
        Try
            Using model As New MTaxesLiquidation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveTaxesLiquidation(Year, varCadastralIdentification.Trim, varCadastralIdentification2.Trim, varAddress, varAddress2, OwnerId, PropertyType)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    AsyncLoader(False)
                    INDgcTaxes.DataSource = Nothing
                    INDgcTaxes.DataSource = Presenter.ListTaxesLiquidationdetailByIds(Result.ObjectEmbbeded.Item2)
                    Ids = Result.ObjectEmbbeded.Item2
                Else
                    AsyncLoader(False)
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message

                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' METODO: Item confirmar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        If Ids = String.Empty OrElse Ids Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe liquidar primero para poder confirmar"
            Exit Sub
        End If
        If INDviewTaxes.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles de liquidación para poder confirmar"
            Exit Sub
        End If
        Try
            Using model As New MTaxesLiquidation(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.ConfirmTaxesLiquidation(Year, Ids)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    AsyncLoader(False)
                    INDgcTaxes.DataSource = Nothing
                    INDgcTaxes.DataSource = Presenter.ListTaxesLiquidationdetailByIds(Result.ObjectEmbbeded.Item1)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                Else
                    AsyncLoader(False)
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITaxLiquidation.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Propietario", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Dirección", .FieldName = "Addres", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Avalúo", .FieldName = "Appraisal", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTaxesProperty
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        If TextEditOpen = "INDtxtCadastralIdentification" Then
            CadastralIdentification = ReturnValue
        ElseIf TextEditOpen = "INDtxtCadastralIdentification2" Then
            CadastralIdentification2 = ReturnValue
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDgcTaxes.DataSource = Nothing
        CadastralIdentification = String.Empty
        CadastralIdentification2 = String.Empty
        Address = String.Empty
        Address2 = String.Empty
        OwnerId = Nothing
        PropertyType = Nothing
        Ids = String.Empty
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Sub LoadControls()

    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de predio
        ListPropertyType = New List(Of Tuple(Of Integer, String))
        ListPropertyType.Add(New Tuple(Of Integer, String)(1, "Urbano"))
        ListPropertyType.Add(New Tuple(Of Integer, String)(2, "Rural"))
        INDslePropertyType.Properties.DataSource = ListPropertyType.ToList
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListPropertyType = Nothing
        Ids = Nothing
        TextEditOpen = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTaxLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyTaxLiquidation, True)
        IndigoGridControl1.AcceptXPO = True
        Me.indigo = SessionValues.Instance
        Presenter = New PTaxLiquidation(Me)
        Deshacer()
        CtrDateNavigator1.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
        IndigoGridControl1.RefreshGrid(INDgcTaxes)
        InitializeTuple()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOwner_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleOwner.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtCadastralIdentification_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtCadastralIdentification.ButtonClick, INDtxtCadastralIdentification2.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            Dim control As DevExpress.XtraEditors.ButtonEdit = sender
            TextEditOpen = control.Name
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Despliega el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOwner_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOwner.QueryPopUp
        If OwnerXpo Is Nothing Then
            Presenter.InitializeThirdParty()
        End If
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
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Validar) = False
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Liquidar Impuestos")
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar

    End Sub

    ''' <summary>
    ''' Click barButton validar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_Validar() Handles BarraBotones.Click_Validar
        Guardar()
    End Sub

    ''' <summary>
    ''' Click varVutton confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

#End Region

End Class