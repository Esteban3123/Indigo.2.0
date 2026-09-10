'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael E. Patiño
' Created          : 2013-09-10
'
' Last Modified By : Rafael E. Patiño
' Last Modified On : 2013-09-10
' Description      : frontal de parametros de interfaz de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports System.ComponentModel
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmInterfaceParametersGlosa
    Implements IInterfacesParameters

#Region "Fields"

    ''' <summary>
    ''' Consjuto de datos que contiene los campos personalizables
    ''' </summary>
    Private _customizableFields As DataTable
    ''' <summary>
    ''' Bandera usada para verificar si existe o no una definicion del funcional
    ''' </summary>
    Private _frontDefinicionExists As Boolean
    ''' <summary>
    ''' Ruta de las definiciones del layout
    ''' </summary>
    Private _pathFunctionalDefinitions As String
    ''' <summary>
    ''' Hilo para cargar las definiciones del funcional
    ''' </summary>
    Private WithEvents DefinitionsLoader As BackgroundWorker
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PInterfacesParameters

    ''' <summary>
    ''' Encapsula la empresa consultada
    ''' </summary> 
    Private _company As GlosasParametersInterface

    ''' <summary>
    ''' Objeto del conjunto de parametros para las interfaces
    ''' </summary>
    Private _ObjParametersInterface As Domain.Entities.GlosasParametersInterface

    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MInterfacesParameters


    ''' <summary>
    ''' variable de control de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues
#End Region

#Region "Propiedades"

    ''' <summary>
    ''' propiedad que contiene el estado del parametro interfaz
    ''' </summary>
    Public Property StatusInterface As Boolean Implements IInterfacesParameters.StatusInterface
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el objeto que encapsula los parametros para la configuracion de interfaces
    ''' </summary>
    ''' <value>Parametros</value>
    ''' <returns>Los parametros</returns>
    Public Property bjParametersInterface As GlosasParametersInterface Implements IInterfacesParameters.ObjParametersInterface
        Get
            Return Me._ObjParametersInterface
        End Get
        Set(value As GlosasParametersInterface)
            If value IsNot Nothing Then
                Me._ObjParametersInterface = value
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para asignar los Mensaje del frontal
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Obtiene la lista de cuentas contables
    ''' </summary>
    ''' <value></value>
    Public Property AccountsDataSourcePrivate As List(Of Domain.Entities.SP_AccountsList_Result) Implements IInterfacesParameters.AccountsDataSourcePrivate
        Get
            Return Me.INDgleAccountGeneralDefault.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.SP_AccountsList_Result))
            Me.INDgleAccountGeneralDefault.Properties.DataSource = value
            Me.INDglePreviousAccountdefault.Properties.DataSource = value
            Me.RepositoryItemGridLookUpEditCuenta.DataSource = value
            Me.INDgleAccoutDebit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de concepto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConceptList As List(Of Domain.Entities.SP_AccountingConceptList_Result) Implements IInterfacesParameters.ConceptList
        Set(value As List(Of Domain.Entities.SP_AccountingConceptList_Result))
            Me.RepositoryItemGridLookUpEditConcepto.DataSource = value
            Me.INDgleConceptAcceptIPS.Properties.DataSource = value
            Me.INDgleConceptAccetPreviousIPS.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Lista de tipo de documento comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property TypeDocumentList As List(Of Domain.Entities.SP_TypeDocumentList_Result) Implements IInterfacesParameters.TypeDocumentList
        Set(value As List(Of Domain.Entities.SP_TypeDocumentList_Result))
            Me.INDgleNoteAccountingRadicate.Properties.DataSource = value
            Me.INDgleNoteAccountingDevolution.Properties.DataSource = value
        End Set
    End Property

    Public Property ListPrivateMethodFox As List(Of AccountSettingsFOX_PrivateMethod)
        Get
            Return CType(Me.INDgcCuentas.DataSource, List(Of AccountSettingsFOX_PrivateMethod))
        End Get
        Set(value As List(Of AccountSettingsFOX_PrivateMethod))
            Me.INDgcCuentas.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de parametros de interfaces
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListTipoInterfaz As List(Of Tuple(Of Integer, String, String))
    Dim ListaCuentas As IList

    ''' <summary>
    ''' Lista parametros de opciones para el manejo contable de las devoluciones de tipo injustificadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListTypeInjustificationDevolution As List(Of Tuple(Of Integer, String))

#End Region

#Region "LoadForm"
    ''' <summary>
    ''' Inicializa los campos del frontal
    ''' </summary>
    Private Sub FrmParameters_Load(sender As Object, e As EventArgs) Handles Me.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MInterfacesParameters(Me.Tag)
        Me.indigo = SessionValues.Instance


        Me.Funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        Me._pathFunctionalDefinitions = String.Concat(Me.indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        Me.DefinitionsLoader = New BackgroundWorker()
        If Me.DefinitionsLoader.IsBusy = False Then
            Me.DefinitionsLoader.RunWorkerAsync()
        End If

        ListTipoInterfaz = New List(Of Tuple(Of Integer, String, String))
        'Cargamos Tipo de Interface
        Dim a As Tuple(Of Integer, String, String) = New Tuple(Of Integer, String, String)(enumInterfaceType.AccountSettingsFOX_PrivateMethod, "Interfaz Fox - Metodo Privado", "AccountSettingsFOX_PrivateMethod")
        Dim b As Tuple(Of Integer, String, String) = New Tuple(Of Integer, String, String)(enumInterfaceType.AccountSettingsFOX_PublicMethod, "Interfaz Fox - Metodo Publico", "AccountSettingsFOX_PublicMethod")
        Dim c As Tuple(Of Integer, String, String) = New Tuple(Of Integer, String, String)(enumInterfaceType.AccountSettingsNET_PrivateMethod, "Interfaz NET - Metodo Privado", "AccountSettingsNET_PrivateMethod")
        Dim d As Tuple(Of Integer, String, String) = New Tuple(Of Integer, String, String)(enumInterfaceType.AccountSettingsNET_PublicMethod, "Interfaz NET - Metodo publico", "AccountSettingsNET_PublicMethod")
        ListTipoInterfaz.Add(a)
        ListTipoInterfaz.Add(b)
        ListTipoInterfaz.Add(c)
        ListTipoInterfaz.Add(d)

        INDgleInterfaceType.Properties.DataSource = ListTipoInterfaz

        'cargamos gridlookupedit de parametros contable para devoluciones
        ListTypeInjustificationDevolution = New List(Of Tuple(Of Integer, String))
        ListTypeInjustificationDevolution.Add(New Tuple(Of Integer, String)(1, "1 - Liberar factura del radicado"))
        ListTypeInjustificationDevolution.Add(New Tuple(Of Integer, String)(2, "2 - Mantener factura en el radicado"))
        ListTypeInjustificationDevolution.Add(New Tuple(Of Integer, String)(3, "3 - Definido por el usuario"))
        INDgleParameterDevolution.Properties.DataSource = ListTypeInjustificationDevolution


        Me._presenter = New PInterfacesParameters(Me)
        Deshacer()
        Me.LoadStatus()

        Me.BarraBotones.StatusRecordVisible = True
        INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'ocultamos cuentas de radicacion
        ' INDlyiAccountCredit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ActionsOnControls = False
    End Sub

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IInterfacesParameters.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub


    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo Contenedor", .FieldName = "ContainerName"}, New ColumnInfo With {.Caption = "Empresa", .FieldName = "CompanyName"}}.ToList()
            .ValorSolicitado = "ContainerName"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListparametersInterface
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbeContainerCode.Text = ReturnValue
        If INDbeContainerCode.Text <> String.Empty Then
            Await LoadData(INDbeContainerCode.Text)
            If INDbeContainerCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeContainerCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo Para Abrir Busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Eliminar Registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guardar Cambios
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            AssigningValues()
            AsyncLoader(True)
            Dim result = Await Model.SaveInterfaceParameter(_ObjParametersInterface)
            AsyncLoader(False)
            If result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                CleanControls()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    ''' <param name="existeDatos">Valor que indica si existen datos para actualizar</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Nuevo registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        Me.INDbeContainerCode.Text = String.Empty
        Me._ObjParametersInterface = Nothing
        Me.INDgleAffectsXServiceIPS.EditValue = False
        Me.INDtxtCompanyName.Text = String.Empty
        Me.INDgleAccountGeneralDefault.EditValue = Nothing
        Me.INDglePreviousAccountdefault.EditValue = Nothing
        Me.INDgleNoteAccountingRadicate.EditValue = Nothing
        Me.INDgleNoteAccountingDevolution.EditValue = Nothing

        Me.INDgleInterfaceType.EditValue = Nothing
        Me.INDgleParameterDevolution.EditValue = Nothing
        Me.INDgleAccoutDebit.EditValue = String.Empty
        Me.INDgcCuentas.DataSource = Nothing
        Me.INDgcCuentas.RefreshDataSource()
        Me.IndigoGridControl1.RefreshGrid(INDgcCuentas)

        Me.INDgleAccoutDebit.EditValue = Nothing
        Me.INDgleConceptAcceptIPS.EditValue = Nothing
        Me.INDgleConceptAccetPreviousIPS.EditValue = Nothing


        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub


    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInterfacesParameters.ActionsOnControls
        Set(value As Boolean)
            INDbeContainerCode.Enabled = Not value
            INDtxtCompanyName.Enabled = value

            'si bandera interfaz esta en si

            Me.INDgleInterfaceType.Enabled = value
            Me.INDgleParameterDevolution.Enabled = value
            Me.INDgleAffectsXServiceIPS.Enabled = value
            Me.INDgleAccountGeneralDefault.Enabled = value
            Me.INDglePreviousAccountdefault.Enabled = value
            '  Me.INDtxtAccountCredit.Enabled = value
            Me.INDgleAccoutDebit.Enabled = value
            Me.INDgcCuentas.Enabled = value
            Me.INDgleNoteAccountingRadicate.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value


            Me.INDgleNoteAccountingDevolution.Enabled = value
            Me.INDgleAccoutDebit.Enabled = value
            Me.INDgleConceptAcceptIPS.Enabled = value
            Me.INDgleConceptAccetPreviousIPS.Enabled = value

            'Me.EnableControl()
            If value = True Then
                INDtxtCompanyName.Focus()
            Else
                INDbeContainerCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para cargar rejilla de configuracion de cunetas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadTypeInterface()
        Dim valorInterface As Integer = Me.INDgleInterfaceType.EditValue
        Select Case valorInterface
            Case enumInterfaceType.AccountSettingsFOX_PrivateMethod
                ListaCuentas = New List(Of AccountSettingsFOX_PrivateMethod)
                ListaCuentas = Await Model.List_AccountSettingsFOX_PrivateMethod()
                INDclCodePlan.VisibleIndex = -1
                INDclDenomination.VisibleIndex = 0
                INDclInvoiceNotRadicate.VisibleIndex = 1
                INDclInvoiceRadicate.VisibleIndex = 2
                INDclRectifiableGlosa.VisibleIndex = 3
                INDclLegalProcess.VisibleIndex = 4
                INDclConciliation.VisibleIndex = 5
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'ocultamos cuentas de radicacion
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDgleNoteAccountingRadicate.Enabled = True
                Me.INDgleNoteAccountingDevolution.Enabled = True
                TypeDocumentList = Await Model.ListTypeDocument(Me.INDbeContainerCode.Text)

                'privado
                INDgleAccountGeneralDefault.Enabled = True
                INDglePreviousAccountdefault.Enabled = True
                INDlyiAccountAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiAffectsXServiceIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiGeneralAccountdefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiVigenciaAnterioresDefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'para publico
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAccept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAcceptPreviousIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


            Case enumInterfaceType.AccountSettingsFOX_PublicMethod
                ListaCuentas = New List(Of AccountSettingsFOX_PublicMethod)
                ListaCuentas = Await Model.List_AccountSettingsFOX_PublicMethod()
                INDclCodePlan.VisibleIndex = 0
                INDclDenomination.Width = 150
                INDclDenomination.MinWidth = 100
                INDclDenomination.MaxWidth = 250
                INDclDenomination.VisibleIndex = 1
                INDclInvoiceNotRadicate.VisibleIndex = -1
                INDclInvoiceRadicate.VisibleIndex = 2
                INDclRectifiableGlosa.VisibleIndex = -1 'para interfaz FOX metodo publico ocultamos la columna INDclRectifiableGlosa de glosa subsanable
                INDclLegalProcess.VisibleIndex = -1
                INDclConciliation.VisibleIndex = -1
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always  'visualizamos cuentas de radicacion
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDgleNoteAccountingRadicate.Enabled = True
                Me.INDgleNoteAccountingDevolution.Enabled = True
                TypeDocumentList = Await Model.ListTypeDocument(Me.INDbeContainerCode.Text)
                'privado
                INDgleAccountGeneralDefault.Enabled = False
                INDglePreviousAccountdefault.Enabled = False
                INDlyiAccountAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiAffectsXServiceIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiGeneralAccountdefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiVigenciaAnterioresDefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'mostramos controles para metodo publico
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAccept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAcceptPreviousIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Case enumInterfaceType.AccountSettingsNET_PrivateMethod
                ListaCuentas = New List(Of AccountSettingsNET_PrivateMethod)
                ListaCuentas = Await Model.List_AccountSettingsNET_PrivateMethod()
                INDclCodePlan.VisibleIndex = -1
                INDclDenomination.VisibleIndex = 0
                INDclInvoiceNotRadicate.VisibleIndex = 1 'para interfaz NET metodo privado ocultamos la columna INDclInvoiceNotRadicate de Factura Radicada
                INDclInvoiceRadicate.VisibleIndex = 2
                INDclRectifiableGlosa.VisibleIndex = 3
                INDclLegalProcess.VisibleIndex = 4
                INDclConciliation.VisibleIndex = 5
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'ocultamos cuentas de radicacion
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDgleNoteAccountingRadicate.Text = String.Empty
                Me.INDgleNoteAccountingRadicate.EditValue = Nothing
                Me.INDgleNoteAccountingRadicate.Enabled = False

                'Me.INDgleNoteAccountingDevolution.Text = String.Empty
                'Me.INDgleNoteAccountingDevolution.EditValue = Nothing
                'Me.INDgleNoteAccountingDevolution.Enabled = False

                Me.INDgleNoteAccountingDevolution.Enabled = True
                Me.TypeDocumentList = Await Model.ListTypeDocument(Me.INDbeContainerCode.Text)
                'privado
                INDgleAccountGeneralDefault.Enabled = True
                INDglePreviousAccountdefault.Enabled = True
                INDlyiAccountAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiAffectsXServiceIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiGeneralAccountdefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiVigenciaAnterioresDefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'ocultamos controles para metodo publico
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAccept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiConceptAcceptPreviousIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            Case enumInterfaceType.AccountSettingsNET_PublicMethod
                ListaCuentas = New List(Of AccountSettingsNET_PublicMethod)
                ListaCuentas = Await Model.List_AccountSettingsNET_PublicMethod()
                INDclCodePlan.VisibleIndex = 0
                INDclDenomination.Width = 150
                INDclDenomination.MinWidth = 100
                INDclDenomination.MaxWidth = 250
                INDclDenomination.VisibleIndex = 1
                INDclInvoiceNotRadicate.VisibleIndex = -1 'para interfaz NET metodo privado ocultamos la columna INDclInvoiceNotRadicate de Factura Radicada
                INDclInvoiceRadicate.VisibleIndex = 2
                INDclRectifiableGlosa.VisibleIndex = -1
                INDclLegalProcess.VisibleIndex = -1
                INDclConciliation.VisibleIndex = -1
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'ocultamos cuentas de radicacion
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDgleNoteAccountingRadicate.Text = String.Empty
                Me.INDgleNoteAccountingRadicate.EditValue = Nothing
                Me.INDgleNoteAccountingRadicate.Enabled = False

                'Me.INDgleNoteAccountingDevolution.Text = String.Empty
                'Me.INDgleNoteAccountingDevolution.EditValue = Nothing
                'Me.INDgleNoteAccountingDevolution.Enabled = False

                Me.INDgleNoteAccountingDevolution.Enabled = True
                TypeDocumentList = Await Model.ListTypeDocument(Me.INDbeContainerCode.Text)

                INDlyiAccountAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiAffectsXServiceIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiGeneralAccountdefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiVigenciaAnterioresDefault.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'privado
                INDgleAccountGeneralDefault.Enabled = False
                INDglePreviousAccountdefault.Enabled = False
                'mostramos controles para metodo publico
                INDlyiAccountRadicate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiAccoutDebit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAccept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAcceptIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiConceptAcceptPreviousIPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Select
        Me.INDgcCuentas.DataSource = ListaCuentas
        Me.INDgcCuentas.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' cargar cuentas y conceptos
    ''' </summary>
    ''' <param name="companyDGH"></param>
    ''' <remarks></remarks>
    Private Async Function LoadData(ByVal companyDGH As String) As Task
        AsyncLoader(True)

        Dim Result = Await Model.ValidateContainers(INDbeContainerCode.Text.Trim)
        'Si exister el contenedor
        If Result = True Then
            Me.AccountsDataSourcePrivate = Await Model.ListAccounts(companyDGH)
            'valido que exista datos contables en dicho contenedor
            If Me.AccountsDataSourcePrivate Is Nothing AndAlso AccountsDataSourcePrivate.Count = 0 Then
                Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(NoExisteDatosContables, Eform.ParametersInterfaz), Me.INDbeContainerCode.Text)
                AsyncLoader(False)
                Exit Function
            End If

            ConceptList = Await Model.ListAccountingConcept(companyDGH, 0) 'todos los conceptos

            LoadControls()
            AsyncLoader(False)
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(NoExisteContenedor, Eform.ParametersInterfaz), Me.INDbeContainerCode.Text)
            AsyncLoader(False)
            Me.ActionsOnControls = False
        End If

    End Function
    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If INDbeContainerCode.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiCompanyCode.Text)
            Return False
        End If
        If INDtxtCompanyName.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiCompanyName.Text)
            Return False
        End If
        If Me.INDgleAffectsXServiceIPS.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiAffectsXServiceIPS.Text)
            Return False
        End If


        'validacion adicional para metodo FOX
        If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Then
            If Me.INDgleNoteAccountingRadicate.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiNoteAccountingRadicate.Text)
                Return False
            End If
        End If

        If Me.INDgleNoteAccountingDevolution.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiNoteAccountingDevolution.Text)
            Return False
        End If

        'metodo privado
        If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PrivateMethod Then

            If Me.INDgleAffectsXServiceIPS.EditValue = False Then
                If Me.INDgleAccountGeneralDefault.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiGeneralAccountdefault.Text)
                    Return False
                End If
            End If

            If Me.INDglePreviousAccountdefault.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiVigenciaAnterioresDefault.Text)
                Return False
            End If
            'metodo publico
        ElseIf Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PublicMethod Then

            If Me.INDgleAccoutDebit.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiAccoutDebit.Text)
                Return False
            End If

            If Me.INDgleConceptAcceptIPS.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiConceptAcceptIPS.Text)
                Return False
            End If

            If Me.INDgleConceptAccetPreviousIPS.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiConceptAcceptPreviousIPS.Text)
                Return False
            End If

        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Me._ObjParametersInterface
            .ContainerName = Me.INDbeContainerCode.Text
            .CompanyName = Me.INDtxtCompanyName.Text
            '.Interface = Me.INDrgInterfaceERP.EditValue
            .AccountingMethod = Me.INDgleInterfaceType.EditValue
            .AffectsService = Me.INDgleAffectsXServiceIPS.EditValue
            .DevolutionInjustificate = Me.INDgleParameterDevolution.EditValue
            .DevolutionCodeNoteAccounting = Me.INDgleNoteAccountingDevolution.EditValue

            'If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PublicMethod Then
            '    .DevolutionCodeNoteAccounting = Me.INDgleNoteAccountingDevolution.EditValue
            'End If

            If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Then
                .RadicateCodeNoteAccounting = Me.INDgleNoteAccountingRadicate.EditValue
            Else
                .RadicateCodeNoteAccounting = String.Empty
            End If

            'metodo privado
            If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PrivateMethod Then
                .AccountantAccountGeneralAcceptanceC = Me.INDgleAccountGeneralDefault.EditValue
                .AccountantAccountPreviousAcceptanceC = Me.INDglePreviousAccountdefault.EditValue
                'metodo publico
            ElseIf Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Or Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PublicMethod Then
                .DebitAccount = Me.INDgleAccoutDebit.EditValue
                .AcceptanceObjectionsConcept = Me.INDgleConceptAcceptIPS.EditValue
                .AcceptanceObjectionsConceptPast = Me.INDgleConceptAccetPreviousIPS.EditValue
            End If

            .DateConfiguration = Date.Now
            .DebitAccount = INDgleAccoutDebit.EditValue
            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .Interface = True
                Case eActionsStatusRecords.Inactive
                    .Interface = False
            End Select
        End With

        Select Case Me.INDgleInterfaceType.EditValue
            Case enumInterfaceType.AccountSettingsFOX_PrivateMethod
                For Each item In Me.INDgcCuentas.DataSource
                    _ObjParametersInterface.AccountSettingsFOX_PrivateMethod.Add(item)
                Next
            Case enumInterfaceType.AccountSettingsFOX_PublicMethod
                For Each item In Me.INDgcCuentas.DataSource
                    _ObjParametersInterface.AccountSettingsFOX_PublicMethod.Add(item)
                Next
            Case enumInterfaceType.AccountSettingsNET_PrivateMethod
                For Each item In Me.INDgcCuentas.DataSource
                    _ObjParametersInterface.AccountSettingsNET_PrivateMethod.Add(item)
                Next
            Case enumInterfaceType.AccountSettingsNET_PublicMethod
                For Each item In Me.INDgcCuentas.DataSource
                    _ObjParametersInterface.AccountSettingsNET_PublicMethod.Add(item)
                Next
        End Select
    End Sub


    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Limpiar Controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.CleanControls()
    End Sub

#End Region

#Region "Metodos y funciones"

    ''' <summary>-
    ''' Abrir el formulario de personalizacion del frontal
    ''' </summary>
    Private Sub OpenCustomize()
        Me.INDlycRoot.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Restablecer las definiciones del formulario
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(Me.indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(Me.indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            Me.INDlycRoot.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesLayoutRestablecido)
        End If
    End Sub


    ''' <summary>
    ''' Carga los parametros configurados
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadControls()
        ActionsOnControls = True
        Dim result As New Domain.Entities.BlockRecord
        'AsyncLoader(True)
        Me._ObjParametersInterface = Await Model.GetInterfacesParameters(Me.INDbeContainerCode.Text)
        ' AsyncLoader(False)
        If Me._ObjParametersInterface IsNot Nothing Then

            If Me._ObjParametersInterface.Id <> 0 Then
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Me._ObjParametersInterface.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Me._ObjParametersInterface.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Me._ObjParametersInterface.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Me._ObjParametersInterface.ModificationDate)
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                result = Await Model.GetBlockRecord(Me.Tag, Me._ObjParametersInterface.Id)
                Me.INDbeContainerCode.Text = Me._ObjParametersInterface.ContainerName
                Me.INDtxtCompanyName.Text = Me._ObjParametersInterface.CompanyName
                Me.INDgleAffectsXServiceIPS.EditValue = Me._ObjParametersInterface.AffectsService
                Me.INDgleAccountGeneralDefault.EditValue = Me._ObjParametersInterface.AccountantAccountGeneralAcceptanceC
                Me.INDglePreviousAccountdefault.EditValue = Me._ObjParametersInterface.AccountantAccountPreviousAcceptanceC
                Me.INDgleNoteAccountingRadicate.EditValue = Me._ObjParametersInterface.RadicateCodeNoteAccounting
                Me.INDgleNoteAccountingDevolution.EditValue = Me._ObjParametersInterface.DevolutionCodeNoteAccounting

                Me.INDgleInterfaceType.EditValue = Me._ObjParametersInterface.AccountingMethod
                Me.INDgleParameterDevolution.EditValue = Me._ObjParametersInterface.DevolutionInjustificate
                Me.INDgleAccoutDebit.EditValue = Me._ObjParametersInterface.DebitAccount
                Me.StatusInterface = _ObjParametersInterface.Interface

                'Me.INDgleAccoutDebit.EditValue = Me._ObjParametersInterface.DebitAccount
                Me.INDgleConceptAcceptIPS.EditValue = Me._ObjParametersInterface.AcceptanceObjectionsConcept
                Me.INDgleConceptAccetPreviousIPS.EditValue = Me._ObjParametersInterface.AcceptanceObjectionsConceptPast


                'envio a bloquear el registro
                If result IsNot Nothing Then
                    If result.Id = 0 Then
                        ' LogicaBotonActualizar(True)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Me._ObjParametersInterface.Id}
                        Using modelBlock As New MInterfacesParameters("577")
                            Dim operation = Await modelBlock.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        End Using
                    Else
                        LogicaBotonActualizar(False)
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                End If
            Else
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            ' ActionsOnControls = False
            Me._ObjParametersInterface = New Domain.Entities.GlosasParametersInterface()
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            ' Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaData, Eform.InfoMetaData), Me.Responsible.Code, Me.Responsible.Name, Me.Responsible.CodeERP, Me.Responsible.Charge), .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .JournalVoucher = IndexedDocumentType.File, .IdEntity =  "$#" & Me.Tag & "_" & Me.Responsible.Code & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code), .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            ' Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            'Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaData, Eform.InfoMetaData), Me.Responsible.Code, Me.Responsible.Name, Me.Responsible.CodeERP, Me.Responsible.Charge)
            ' Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code)
            Return Me._doc
        End If
    End Function



#End Region

#Region "Cuztomizacion"

    ''' <summary>
    ''' Aqui se verifica asincronamente si existe una definicion xml del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_DoWork(sender As Object, e As DoWorkEventArgs) Handles DefinitionsLoader.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(Me._pathFunctionalDefinitions) = True Then
            Me._frontDefinicionExists = True
        End If
    End Sub

    ''' <summary>
    ''' Carga asincronamente la definicion del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DefinitionsLoader.RunWorkerCompleted
        If Me._frontDefinicionExists = True Then
            Me.INDlycRoot.RestoreLayoutFromXml(Me._pathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Se guardan las modificaciones realizadas en la definicion del frontal
    ''' </summary>
    Private Sub INDlycRoot_HideCustomization(sender As Object, e As EventArgs) Handles INDlycRoot.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If Me.INDlycRoot.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Me.indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    Me.INDlycRoot.SaveLayoutToXml(Me._pathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Aqui se evalua si existe el permiso de personalizar el frontal para permitir mostrar el frontal de personalizacion o no
    ''' </summary>
    Private Async Sub INDlycRoot_ShowCustomization(sender As Object, e As EventArgs) Handles INDlycRoot.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Dim dsFields As DataSet = Await Model.GetFieldsNULL()
            If dsFields IsNot Nothing Then
                Me._customizableFields = dsFields.Tables(0)
                For i As Integer = 0 To Me._customizableFields.Rows.Count - 1
                    For j As Integer = 0 To Me.INDlycgRoot.Items.Count - 1
                        If Object.Equals(Me.INDlycgRoot.Items.Item(j).Tag, Nothing) = False Then
                            If Me._customizableFields.Rows(i).Item("NAME").ToString.Trim = Me.INDlycgRoot.Items.Item(j).Tag.ToString.Trim Then
                                Me.INDlycgRoot.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
        End Try
    End Sub


#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Clcik Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de personalizacion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de reestablecer la definicion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        Me.ResetLayout()
    End Sub

    ''' <summary>
    ''' Activar o desactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Me.Guardar()
    End Sub


#End Region

#Region "Handles"


    ''' <summary>
    ''' Control del control Codigo contenedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbeContainerCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbeContainerCode.KeyDown
        If Not String.IsNullOrEmpty(INDbeContainerCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadData(INDbeContainerCode.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para Abrir Busqueda 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDEntityBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmInterfaceParametersGlosa_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbeContainerCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbeContainerCode.ButtonClick
        AbrirBusqueda()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _customizableFields = Nothing
        _frontDefinicionExists = Nothing
        _pathFunctionalDefinitions = Nothing
        DefinitionsLoader = Nothing
        _presenter = Nothing
        _company = Nothing
        _ObjParametersInterface = Nothing
        Model = Nothing
        record = Nothing
    End Sub


    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._ObjParametersInterface IsNot Nothing AndAlso Me._ObjParametersInterface.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDbeContainerCode.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbeContainerCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbeContainerCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Manejador de Eventos cambio tipo de interfaz
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleInterfaceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleInterfaceType.EditValueChanged
        LoadTypeInterface()
    End Sub


    Private Sub INDgvCuentas_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles INDgvCuentas.ValidatingEditor


        Dim viewGrid As GridView = sender
        Me.INDgcCuentas.RefreshDataSource()

        Dim ValorclInvoiceNotRadicate = viewGrid.GetFocusedRowCellValue(Me.INDclInvoiceNotRadicate)
        Dim ValorclInvoiceRadicate = viewGrid.GetFocusedRowCellValue(Me.INDclInvoiceRadicate)
        Dim ValorclLegalProcess = viewGrid.GetFocusedRowCellValue(Me.INDclLegalProcess)

        Dim ValorclRectifiableGlosa = viewGrid.GetFocusedRowCellValue(Me.INDclRectifiableGlosa)
        Dim ValorclConciliation = viewGrid.GetFocusedRowCellValue(Me.INDclConciliation)

        Dim valor As String = Me.INDgvCuentas.GetFocusedValue()
        Dim tmpLIstaBusqueda As IList = Me.INDgcCuentas.DataSource
        Dim queryCount As Integer = 0
        'no tenenmos encuenta valore vacio
        If e.Value IsNot Nothing AndAlso e.Value.ToString <> String.Empty Then
            If Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PrivateMethod Then
                Dim tmpLIstaBusquedaX As List(Of AccountSettingsFOX_PrivateMethod) = Me.INDgcCuentas.DataSource
                queryCount = Aggregate g In tmpLIstaBusquedaX
                            Where g.InvoiceNotRadicate = e.Value.ToString Or g.InvoiceRadicate = e.Value.ToString Or g.LegalProcess = e.Value.ToString
                            Into Count()

                'ElseIf Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsFOX_PublicMethod Then
                '    tmpLIstaBusqueda = New List(Of AccountSettingsFOX_PublicMethod)
                '    Dim tmpLIstaBusquedaX As List(Of AccountSettingsFOX_PublicMethod) = Me.INDgcCuentas.DataSource
                '    queryCount = Aggregate g In tmpLIstaBusquedaX
                '                Where g.InvoiceRadicate = e.Value.ToString
                '                Into Count()

            ElseIf Me.INDgleInterfaceType.EditValue = enumInterfaceType.AccountSettingsNET_PrivateMethod Then
                tmpLIstaBusqueda = New List(Of AccountSettingsNET_PrivateMethod)
                Dim tmpLIstaBusquedaX As List(Of AccountSettingsNET_PrivateMethod) = Me.INDgcCuentas.DataSource
                queryCount = Aggregate g In tmpLIstaBusquedaX
                            Where g.InvoiceNotRadicate = e.Value.ToString Or g.InvoiceRadicate = e.Value.ToString Or g.LegalProcess = e.Value.ToString
                            Into Count()
            End If
        End If


        If queryCount > 0 Then
            e.Valid = False
            e.ErrorText = obtenerRecurso(ItemYaAgregado, Presentation.Base.Eform.Comunes)
        End If
    End Sub

    ''' <summary>
    ''' Si afecta servicio bloqueamos las cuenats de aceptacion general y vigencia anteriors
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAffectsXServiceIPS_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAffectsXServiceIPS.EditValueChanged
        If Me.INDgleAffectsXServiceIPS.EditValue = True Then
            INDgleAccountGeneralDefault.Enabled = False
            'INDglePreviousAccountdefault.Enabled = False
        Else
            INDgleAccountGeneralDefault.Enabled = True
            'INDglePreviousAccountdefault.Enabled = True
        End If
    End Sub

#End Region

#Region "Enumeraciones"
    ''' <summary>
    ''' Enumeraciones de los tipos de interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumInterfaceType
        AccountSettingsFOX_PrivateMethod = 1
        AccountSettingsFOX_PublicMethod = 2
        AccountSettingsNET_PrivateMethod = 3
        AccountSettingsNET_PublicMethod = 4
    End Enum
#End Region
    Private Sub RepositoryItemGridLookUpEditConcepto_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditConcepto.EditValueChanged
        Dim GridLookEdit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim objTmp As Object = Nothing
        Select Case Me.INDgleInterfaceType.EditValue
            Case enumInterfaceType.AccountSettingsFOX_PrivateMethod
                objTmp = CType(Me.INDgvCuentas.GetRow(Me.INDgvCuentas.FocusedRowHandle), AccountSettingsFOX_PrivateMethod)
            Case enumInterfaceType.AccountSettingsFOX_PublicMethod
                objTmp = CType(Me.INDgvCuentas.GetRow(Me.INDgvCuentas.FocusedRowHandle), AccountSettingsFOX_PublicMethod)
            Case enumInterfaceType.AccountSettingsNET_PrivateMethod
                objTmp = CType(Me.INDgvCuentas.GetRow(Me.INDgvCuentas.FocusedRowHandle), AccountSettingsNET_PrivateMethod)
            Case enumInterfaceType.AccountSettingsNET_PublicMethod
                objTmp = CType(Me.INDgvCuentas.GetRow(Me.INDgvCuentas.FocusedRowHandle), AccountSettingsNET_PublicMethod)
        End Select

        If objTmp IsNot Nothing Then
            objTmp.Conciliation = GridLookEdit.EditValue
        End If
        INDgcCuentas.RefreshDataSource()
    End Sub

    Private Sub FrmInterfaceParametersGlosa_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDbeContainerCode.Focus()
    End Sub
End Class






