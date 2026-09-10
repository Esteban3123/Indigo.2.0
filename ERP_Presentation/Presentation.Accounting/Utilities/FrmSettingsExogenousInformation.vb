'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports System.Text
'Imports System.Resources
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting
Imports System.Drawing
Imports Domain.Base.Entities
Imports DevExpress.Xpo

#End Region

Public Class FrmSettingsExogenousInformation
    Implements ISettingsExogenousInformation


#Region "Properties"

    ''' <summary>
    ''' Permite obtener el datasource del formato 1001
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1001 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1001
        Get
            Return CType(INDSlMainAccountFormat1001.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1001.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1007
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1007 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1007
        Get
            Return CType(INDSlMainAccountFormat1007.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1007.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1003
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1003 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1003
        Get
            Return CType(INDSlMainAccountFormat1003.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1003.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1004
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1004 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1004
        Get
            Return CType(INDSlMainAccountFormat1004.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1004.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1008
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1008 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1008
        Get
            Return CType(INDSlMainAccountFormat1008.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1008.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1009
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1009 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1009
        Get
            Return CType(INDSlMainAccountFormat1009.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1009.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite obtener el datasource del formato 1647
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingAccountFormat1647 As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsExogenousInformation.AccountingAccountFormat1647
        Get
            Return CType(INDSlMainAccountFormat1647.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlMainAccountFormat1647.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements ISettingsExogenousInformation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property


#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MSettingsExogenousInformation(Me.MyTag)

    'Dim Presenter As PSettingFixedAsset


    ''' <summary>
    ''' Variables que contienen la lista de tipos de datos
    ''' </summary>
    Dim ListConceptFormat1001 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1007 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1003 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1004 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1008 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1009 As New List(Of Tuple(Of String, String))

    Dim ListConceptFormat1647 As New List(Of Tuple(Of String, String))

    Dim ListSettingsExogenousInformation1001 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1007 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1003 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1004 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1008 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1009 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation1647 As List(Of SettingsExogenousInformation)

    Dim ListSettingsExogenousInformation As List(Of SettingsExogenousInformation)

    Dim SettingsExogenousInformation1001 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1007 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1003 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1004 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1008 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1009 As SettingsExogenousInformation

    Dim SettingsExogenousInformation1647 As SettingsExogenousInformation

    Dim ListDeleteSettingsExogenousInformation1001 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1007 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1003 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1004 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1008 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1009 As List(Of SettingsExogenousInformation)

    Dim ListDeleteSettingsExogenousInformation1647 As List(Of SettingsExogenousInformation)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Método: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        'CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        AssigningValues()

        If ValidarControles() = False Then
            Exit Sub
        End If

        Using model As New MSettingsExogenousInformation(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveParameters(ListSettingsExogenousInformation)
            AsyncLoader(False)
            If Result.StateResult = True Then

                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")

                Me.ListSettingsExogenousInformation = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()

                LoadControls()

                Me.BarraBotones.CleanAuditBasic()

                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region


#Region "Load"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        Model = Nothing
        ListConceptFormat1001 = Nothing
        ListConceptFormat1007 = Nothing
        ListConceptFormat1003 = Nothing
        ListConceptFormat1004 = Nothing
        ListConceptFormat1008 = Nothing
        ListConceptFormat1009 = Nothing
        ListConceptFormat1647 = Nothing
        ListSettingsExogenousInformation1001 = Nothing
        ListSettingsExogenousInformation1007 = Nothing
        ListSettingsExogenousInformation1003 = Nothing
        ListSettingsExogenousInformation1004 = Nothing
        ListSettingsExogenousInformation1008 = Nothing
        ListSettingsExogenousInformation1009 = Nothing
        ListSettingsExogenousInformation1647 = Nothing
        ListSettingsExogenousInformation = Nothing
        SettingsExogenousInformation1001 = Nothing
        SettingsExogenousInformation1007 = Nothing
        SettingsExogenousInformation1003 = Nothing
        SettingsExogenousInformation1004 = Nothing
        SettingsExogenousInformation1008 = Nothing
        SettingsExogenousInformation1009 = Nothing
        SettingsExogenousInformation1647 = Nothing
        ListDeleteSettingsExogenousInformation1001 = Nothing
        ListDeleteSettingsExogenousInformation1007 = Nothing
        ListDeleteSettingsExogenousInformation1003 = Nothing
        ListDeleteSettingsExogenousInformation1004 = Nothing
        ListDeleteSettingsExogenousInformation1008 = Nothing
        ListDeleteSettingsExogenousInformation1009 = Nothing
        ListDeleteSettingsExogenousInformation1647 = Nothing
    End Sub


    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLySettingFixedAsset, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        InitializeTuple()
        Deshacer()
        LoadControls()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvFormat1001, ListActions)
        IndigoGridView2.SetListAcction(INDGvFormat1007, ListActions)
        IndigoGridView3.SetListAcction(INDGvFormat1003, ListActions)
        IndigoGridView4.SetListAcction(INDGvFormat1004, ListActions)
        IndigoGridView5.SetListAcction(INDGvFormat1008, ListActions)
        IndigoGridView6.SetListAcction(INDGvFormat1009, ListActions)
        IndigoGridView7.SetListAcction(INDGvFormat1647, ListActions)

    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingFixedAsset_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se ejecuta cuando se da clic en el botón "Plus" en el control INDSlMainAccountFormat1001, se abre un formulario, y después se actualiza el origen de datos del control INDSlMainAccountFormat1001
    ''' con una lista de cuentas obtenida del método ListAccounts() en la clase MSettingsExogenousInformation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1001_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlMainAccountFormat1001.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1001 = model.ListAccounts()
            End Using
        End If
    End Sub



#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1001, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1001_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1001.Click

        If ValidateControl("1001") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1001 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1001 Is Nothing Then
            ListSettingsExogenousInformation1001 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1001.FileTemplate = 1
        SettingsExogenousInformation1001.MainAccountId = INDSlMainAccountFormat1001.EditValue
        SettingsExogenousInformation1001.ConceptCode = INDSlConceptCodeFormat1001.EditValue
        SettingsExogenousInformation1001.MainAccountCodeName = INDSlMainAccountFormat1001.Text

        If ListSettingsExogenousInformation1001.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1001.EditValue And x.MainAccountId = INDSlMainAccountFormat1001.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If


        ListSettingsExogenousInformation1001.Add(SettingsExogenousInformation1001)

        INDGc1001Format.DataSource = Nothing
        INDGc1001Format.DataSource = ListSettingsExogenousInformation1001

        CleanControls("1001")

    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1007, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1007_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1007.Click

        If ValidateControl("1007") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1007 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1007 Is Nothing Then
            ListSettingsExogenousInformation1007 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1007.FileTemplate = 2
        SettingsExogenousInformation1007.MainAccountId = INDSlMainAccountFormat1007.EditValue <
        SettingsExogenousInformation1007.ConceptCode = INDSlConceptCodeFormat1007.EditValue
        SettingsExogenousInformation1007.MainAccountCodeName = INDSlMainAccountFormat1007.Text

        If ListSettingsExogenousInformation1007.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1007.EditValue And x.MainAccountId = INDSlMainAccountFormat1007.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1007.Add(SettingsExogenousInformation1007)

        INDGc1007Format.DataSource = Nothing
        INDGc1007Format.DataSource = ListSettingsExogenousInformation1007

        CleanControls("1007")
    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1003, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1003_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1003.Click
        If ValidateControl("1003") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1003 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1003 Is Nothing Then
            ListSettingsExogenousInformation1003 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1003.FileTemplate = 3
        SettingsExogenousInformation1003.MainAccountId = INDSlMainAccountFormat1003.EditValue
        SettingsExogenousInformation1003.ConceptCode = INDSlConceptCodeFormat1003.EditValue
        SettingsExogenousInformation1003.MainAccountCodeName = INDSlMainAccountFormat1003.Text

        If ListSettingsExogenousInformation1003.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1003.EditValue And x.MainAccountId = INDSlMainAccountFormat1003.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1003.Add(SettingsExogenousInformation1003)

        INDGc1003Format.DataSource = Nothing
        INDGc1003Format.DataSource = ListSettingsExogenousInformation1003

        CleanControls("1003")
    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1004, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1004_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1004.Click
        If ValidateControl("1004") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1004 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1004 Is Nothing Then
            ListSettingsExogenousInformation1004 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1004.FileTemplate = 4
        SettingsExogenousInformation1004.MainAccountId = INDSlMainAccountFormat1004.EditValue
        SettingsExogenousInformation1004.ConceptCode = INDSlConceptCodeFormat1004.EditValue
        SettingsExogenousInformation1004.MainAccountCodeName = INDSlMainAccountFormat1004.Text

        If ListSettingsExogenousInformation1004.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1004.EditValue And x.MainAccountId = INDSlMainAccountFormat1004.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1004.Add(SettingsExogenousInformation1004)

        INDGc1004Format.DataSource = Nothing
        INDGc1004Format.DataSource = ListSettingsExogenousInformation1004

        CleanControls("1004")
    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1008, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1008_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1008.Click
        If ValidateControl("1008") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1008 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1008 Is Nothing Then
            ListSettingsExogenousInformation1008 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1008.FileTemplate = 5
        SettingsExogenousInformation1008.MainAccountId = INDSlMainAccountFormat1008.EditValue
        SettingsExogenousInformation1008.ConceptCode = INDSlConceptCodeFormat1008.EditValue
        SettingsExogenousInformation1008.MainAccountCodeName = INDSlMainAccountFormat1008.Text

        If ListSettingsExogenousInformation1008.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1008.EditValue And x.MainAccountId = INDSlMainAccountFormat1008.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1008.Add(SettingsExogenousInformation1008)

        INDGc1008Format.DataSource = Nothing
        INDGc1008Format.DataSource = ListSettingsExogenousInformation1008

        CleanControls("1008")
    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1009, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1009_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1009.Click

        If ValidateControl("1009") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1009 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1009 Is Nothing Then
            ListSettingsExogenousInformation1009 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1009.FileTemplate = 6
        SettingsExogenousInformation1009.MainAccountId = INDSlMainAccountFormat1009.EditValue
        SettingsExogenousInformation1009.ConceptCode = INDSlConceptCodeFormat1009.EditValue
        SettingsExogenousInformation1009.MainAccountCodeName = INDSlMainAccountFormat1009.Text

        If ListSettingsExogenousInformation1009.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1009.EditValue And x.MainAccountId = INDSlMainAccountFormat1009.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1009.Add(SettingsExogenousInformation1009)

        INDGc1009Format.DataSource = Nothing
        INDGc1009Format.DataSource = ListSettingsExogenousInformation1009

        CleanControls("1008")

    End Sub

    ''' <summary>
    ''' Agrega un elemento a la lista ListSettingsExogenousInformation1647, siempre y cuando este no exista 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddFormat1647_Click(sender As Object, e As EventArgs) Handles INDBtnAddFormat1647.Click
        If ValidateControl("1647") = False Then
            Exit Sub
        End If

        SettingsExogenousInformation1647 = New SettingsExogenousInformation()

        If ListSettingsExogenousInformation1647 Is Nothing Then
            ListSettingsExogenousInformation1647 = New List(Of SettingsExogenousInformation)
        End If

        SettingsExogenousInformation1647.FileTemplate = 7
        SettingsExogenousInformation1647.MainAccountId = INDSlMainAccountFormat1647.EditValue
        SettingsExogenousInformation1647.ConceptCode = INDSlConceptCodeFormat1647.EditValue
        SettingsExogenousInformation1647.MainAccountCodeName = INDSlMainAccountFormat1647.Text

        If ListSettingsExogenousInformation1647.Where(Function(x) x.ConceptCode = INDSlConceptCodeFormat1647.EditValue And x.MainAccountId = INDSlMainAccountFormat1647.EditValue).Count > 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra Agregado este Código de Concepto con esta Cuenta Contable"
            Exit Sub
        End If

        ListSettingsExogenousInformation1647.Add(SettingsExogenousInformation1647)

        INDGc1647Format.DataSource = Nothing
        INDGc1647Format.DataSource = ListSettingsExogenousInformation1647

        CleanControls("1647")
    End Sub

#End Region


#Region "QueryPopUp"

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1001
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1001_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1001.QueryPopUp
        If INDSlMainAccountFormat1001.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1001.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1001 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1007
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1007_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1007.QueryPopUp
        If INDSlMainAccountFormat1007.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1007.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1007 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1003
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1003_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1003.QueryPopUp
        If INDSlMainAccountFormat1003.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1003.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1003 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1004
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1004_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1004.QueryPopUp
        If INDSlMainAccountFormat1004.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1004.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1004 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1008
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1008_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1008.QueryPopUp
        If INDSlMainAccountFormat1008.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1008.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1008 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1009
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1009_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1009.QueryPopUp
        If INDSlMainAccountFormat1009.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1009.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1009 = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de que hayan datos disponibles en el control INDSlMainAccountFormat1647
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlMainAccountFormat1647_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlMainAccountFormat1647.QueryPopUp
        If INDSlMainAccountFormat1647.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlMainAccountFormat1647.Properties.DataSource Is Nothing Then
            Using model As New MSettingsExogenousInformation(Me.MyTag)
                AccountingAccountFormat1647 = model.ListAccounts()
            End Using
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1001()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1001
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1001()
        End Select
    End Sub

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1007()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1007
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1007()
        End Select
    End Sub

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView3
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1003()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1003
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1003()
        End Select
    End Sub

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView4
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1004()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1004
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView4.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1004()
        End Select
    End Sub

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView5
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView5_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView5.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1008()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1008
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView5_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView5.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1008()
        End Select
    End Sub

    ''' <summary>
    ''' Controla las acciones que ocurren cuando se hace clic en los botones dentro del control IndigoGridView6
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView6_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView6.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                RemoveFormat1009()
        End Select
    End Sub

    ''' <summary>
    ''' Evalua el valor del tag, en caso de que sea igual al case, remueve el formato 1009
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView6_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView6.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveFormat1009()
        End Select
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListConceptFormat1001 = New List(Of Tuple(Of String, String))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5001", "Salarios, Prestaciones y demás Pagos Laborales"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5002", "Honorarios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5003", "Comisiones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5004", "Servicios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5005", "Arrendamientos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5006", "Intereses y Rendimientos Financieros"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5007", "Compra de Activos Movibles"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5008", "Compra de Activos Fijos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5010", "Aportes Parafiscales Sena, ICBF y Cajas de Compensación"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5011", "Aportes parafiscales de EPS e ISS o ARL"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5012", "Aportes obligatorios de pensiones al ISS y fondo de pensiones (Incluidos aportes del trabajador)"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5013", "Donaciones en Dinero"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5014", "Donaciones en Otros Activos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5015", "Impuestos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5016", "Demás Costos y Deducciones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5018", "Importe de primas de reaseguros pagados o abonados en cuenta"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5019", "Amortizaciones realizadas durante el año"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5020", "Compra de activos fijos sobre sobre los cuales se solicito deducción"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5022", "Pensiones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5023", "Cuenta al exterior por asistencia técnica"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5024", "Cuenta al exterior por marcas"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5025", "Cuenta al exterior por patentes"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5026", "Cuenta al exterior por regalías"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5026", "Cuenta al exterior por regalías"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5027", "Cuenta al exterior por servicios técnicos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5028", "El valor acumulado de la devolución de pagos o abonos en cuenta y retenciones en años ant"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5029", "Cargos diferidos y/o gastos pagados por anticipado por compras"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5030", "Cargos diferidos y/o gastos pagados por anticipado por honorarios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5031", "Cargos diferidos y/o gastos pagados por anticipado por comisiones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5032", "Cargos diferidos y/o gastos pagados por anticipado por servicios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5033", "Cargos diferidos y/o gastos pagados por anticipado por arrendamientos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5034", "Cargos diferidos y/o gastos pagados por anticipado por intereses y rendimientos financieros"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5035", "Cargos diferidos y/o gastos pagados por anticipado por otros conceptos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5036", "Inversiones en control y mejoramiento del medio ambiente por compras"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5037", "Inversiones en control y mejoramiento del medio ambiente por honorarios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5038", "Inversiones en control y mejoramiento del medio ambiente porcomisiones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5039", "Inversiones en control y mejoramiento del medio ambiente por servicios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5040", "Inversiones en control y mejoramiento del medio ambiente por arrendamientos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5041", "Inversiones en control y mejoramiento del medio ambiente por intereses y rendimientos financieros"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5042", "Inversiones en control y mejoramiento del medio ambiente por otros conceptos"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5043", "Participaciones o dividendos pagados o abonados en cuenta en calidad de exigibles"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5044", "El pago por loterías, rifas, apuestas y similares"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5045", "Retención sobre ingresos de tarjetas debito y crédito"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5046", "Enajenación de activos fijos de personas naturales ante oficinas de transito u otras entidades"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5047", "Importe siniestros por lucro cesante pagados o abonados en cuenta"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5048", "Importe siniestros por  daño emergente pagados o abonados en cuenta"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5049", "Autoretenciones por ventas"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5050", "Autoretenciones por servicios"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5051", "Autoretenciones por rendimientos financieros"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5052", "Otras autoretenciones"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5053", "Retenciones practicadas a titulo de timbre"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5054", "Devolucones de retenciones a titulo de impuesto de timbre"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5055", "Viaticos no considerados ingreso al trabajador"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5056", "Gastos de representación no considerados como ingresos del  trabajador"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5057", "Amortizaciones realizadas durante el año por cargos diferidos impuesto al patrimonio"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5058", "Aportes, tasas y contribuciones efectivamente pagados"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5059", "El pago o abono en cuenta a cada uno de  los cooperados del valor del fondo de protección"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5060", "Redención de inversiones en lo que corresponde a reembolso de capital"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5062", "Autoretenciones por CREE"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5063", "Intereses y rendimientos financieros pagados"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5064", "Devolución de saldos de aportes pensionales pagados"))
        ListConceptFormat1001.Add(New Tuple(Of String, String)("5065", "Excedentes pensionales de libre disponibilidad componente de capital pagado"))
        INDSlConceptCodeFormat1001.Properties.DataSource = ListConceptFormat1001.ToList

        ListConceptFormat1007 = New List(Of Tuple(Of String, String))
        ListConceptFormat1007.Add(New Tuple(Of String, String)("4001", "Ingresos brutos operacionales"))
        ListConceptFormat1007.Add(New Tuple(Of String, String)("4002", "Ingresos no operacionales diferentes de intereses y rendimientos financieros"))
        ListConceptFormat1007.Add(New Tuple(Of String, String)("4003", "Ingresos por intereses y rendimientos financieros"))
        ListConceptFormat1007.Add(New Tuple(Of String, String)("4004", "Ingresos por intereses correspondientes a créditos hipotecarios"))
        INDSlConceptCodeFormat1007.Properties.DataSource = ListConceptFormat1007.ToList()

        ListConceptFormat1003 = New List(Of Tuple(Of String, String))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1301", "Retenciones por salarios, prestaciones y demás pagos laborales"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1302", "Retenciones por ventas"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1303", "Retenciones por servicios"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1304", "Retenciones por honorarios"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1305", "Retenciones por comisiones"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1306", "Retenciones por intereses y rendimientos financieros"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1307", "Retenciones por arrendamientos"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1308", "Otras retenciones por otros conceptos"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1309", "Retención en la fuente en el impuesto a las ventas"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1310", "Retención por dividendos y participaciones"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1311", "Retenciones por enajenación de activos fijos de personas naturales ate oficinas de transito"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1312", "Retención por ingresos de tarjetas debito y crédito"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1313", "Retención por loterías, rifas, apuestas y similares"))
        ListConceptFormat1003.Add(New Tuple(Of String, String)("1314", "Retención por impuesto de timbre"))
        INDSlConceptCodeFormat1003.Properties.DataSource = ListConceptFormat1003.ToList()

        ListConceptFormat1004 = New List(Of Tuple(Of String, String))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8301", "Valor descuento por inversión en nuevos cultivos de arboles de las especies y en áreas de reforestación"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8302", "Impuestos a las ventas en la importación de maquinaria pesada"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8303", "Impuestos pagados en el exterior por nacionales"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8304", "Demas descuentos tributarios solicitados"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8305", "Valor solicitado por empresas de servicio publico domiciliario"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8306", "Valor solicitado por empresas colombianas de transporte internacional"))
        ListConceptFormat1004.Add(New Tuple(Of String, String)("8307", "Valor descuento por inversión en acciones de sociedades agropecuarias"))
        INDSlConceptCodeFormat1004.Properties.DataSource = ListConceptFormat1004.ToList()

        ListConceptFormat1008 = New List(Of Tuple(Of String, String))
        ListConceptFormat1008.Add(New Tuple(Of String, String)("1315", "Cuentas por cobrar - clientes"))
        ListConceptFormat1008.Add(New Tuple(Of String, String)("1316", "Cuentas por cobrar  compañías accionistas, socios y compañías vinculadas"))
        ListConceptFormat1008.Add(New Tuple(Of String, String)("1317", "Otras cuentas por cobrar"))
        ListConceptFormat1008.Add(New Tuple(Of String, String)("1318", "Saldo fiscal provision de cartera"))
        INDSlConceptCodeFormat1008.Properties.DataSource = ListConceptFormat1008.ToList()

        ListConceptFormat1009 = New List(Of Tuple(Of String, String))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2201", "Pasivo con proveedores"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2202", "Cuentas por pagar a casa matriz, compañías vinculadas, socios y accionistas"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2203", "Obligaciones con el sector financiero"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2204", "Pasivos por impuestos, gravámenes y tasas"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2205", "Pasivos laborales"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2206", "Otros Pasivos"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2207", "Saldo del pasivo por el calculo actuarial"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2208", "Pasivos respaldados en documento de fecha cierta"))
        ListConceptFormat1009.Add(New Tuple(Of String, String)("2209", "Pasivos exclusivos de las compañías de seguros"))
        INDSlConceptCodeFormat1009.Properties.DataSource = ListConceptFormat1009.ToList()

        ListConceptFormat1647 = New List(Of Tuple(Of String, String))
        ListConceptFormat1647.Add(New Tuple(Of String, String)("4070", "Ingresos recibidos para terceros"))
        INDSlConceptCodeFormat1647.Properties.DataSource = ListConceptFormat1647.ToList()

    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls(FormatType As String)

        If FormatType = "1001" Then
            INDSlConceptCodeFormat1001.EditValue = Nothing
            INDSlMainAccountFormat1001.EditValue = Nothing
        ElseIf FormatType = "1007" Then
            INDSlConceptCodeFormat1007.EditValue = Nothing
            INDSlMainAccountFormat1007.EditValue = Nothing
        ElseIf FormatType = "1003" Then
            INDSlConceptCodeFormat1007.EditValue = Nothing
            INDSlMainAccountFormat1007.EditValue = Nothing
        ElseIf FormatType = "1004" Then
            INDSlConceptCodeFormat1004.EditValue = Nothing
            INDSlMainAccountFormat1004.EditValue = Nothing
        ElseIf FormatType = "1008" Then
            INDSlConceptCodeFormat1008.EditValue = Nothing
            INDSlMainAccountFormat1008.EditValue = Nothing
        ElseIf FormatType = "1009" Then
            INDSlConceptCodeFormat1009.EditValue = Nothing
            INDSlMainAccountFormat1009.EditValue = Nothing
        ElseIf FormatType = "1647" Then
            INDSlConceptCodeFormat1647.EditValue = Nothing
            INDSlMainAccountFormat1647.EditValue = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MSettingsExogenousInformation(Me.Tag)
                Await Model.SaveBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MSettingsExogenousInformation(CStr(Me.Tag))
            AsyncLoader(True)
            ListSettingsExogenousInformation = Await Model.ListParameters()
            AsyncLoader(False)

            If Not ListSettingsExogenousInformation Is Nothing Then

                ListSettingsExogenousInformation1001 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 1).ToList()
                ListSettingsExogenousInformation1007 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 2).ToList()
                ListSettingsExogenousInformation1003 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 3).ToList()
                ListSettingsExogenousInformation1004 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 4).ToList()
                ListSettingsExogenousInformation1008 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 5).ToList()
                ListSettingsExogenousInformation1009 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 6).ToList()
                ListSettingsExogenousInformation1647 = ListSettingsExogenousInformation.Where(Function(x) x.FileTemplate = 7).ToList()

                INDGc1001Format.DataSource = ListSettingsExogenousInformation1001
                INDGc1007Format.DataSource = ListSettingsExogenousInformation1007
                INDGc1003Format.DataSource = ListSettingsExogenousInformation1003
                INDGc1004Format.DataSource = ListSettingsExogenousInformation1004
                INDGc1008Format.DataSource = ListSettingsExogenousInformation1008
                INDGc1009Format.DataSource = ListSettingsExogenousInformation1009
                INDGc1647Format.DataSource = ListSettingsExogenousInformation1647

                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()

        ListSettingsExogenousInformation.Clear()

        If ListSettingsExogenousInformation1001 IsNot Nothing AndAlso ListSettingsExogenousInformation1001.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1001
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1001 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1001.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1001
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1007 IsNot Nothing AndAlso ListSettingsExogenousInformation1007.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1007
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1007 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1007.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1007
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1003 IsNot Nothing AndAlso ListSettingsExogenousInformation1003.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1003
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1003 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1003.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1003
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1004 IsNot Nothing AndAlso ListSettingsExogenousInformation1004.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1004
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1004 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1004.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1004
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1008 IsNot Nothing AndAlso ListSettingsExogenousInformation1008.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1008
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1008 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1008.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1008
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1009 IsNot Nothing AndAlso ListSettingsExogenousInformation1009.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1009
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1009 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1009.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1009
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListSettingsExogenousInformation1647 IsNot Nothing AndAlso ListSettingsExogenousInformation1647.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListSettingsExogenousInformation1647
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

        If ListDeleteSettingsExogenousInformation1647 IsNot Nothing AndAlso ListDeleteSettingsExogenousInformation1647.Count > 0 Then
            For Each ObjSettingsExigenous As SettingsExogenousInformation In ListDeleteSettingsExogenousInformation1647
                ListSettingsExogenousInformation.Add(ObjSettingsExigenous)
            Next
        End If

    End Sub

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1001" 
    ''' </summary>
    Private Sub RemoveFormat1001()
        SettingsExogenousInformation1001 = CType(INDGvFormat1001.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1001.Remove(SettingsExogenousInformation1001)

        If SettingsExogenousInformation1001.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1001 Is Nothing Then
                ListDeleteSettingsExogenousInformation1001 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1001.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1001.Add(SettingsExogenousInformation1001)
        End If

        INDGc1001Format.DataSource = Nothing
        INDGc1001Format.DataSource = ListSettingsExogenousInformation1001
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1007"
    ''' </summary>
    Private Sub RemoveFormat1007()
        SettingsExogenousInformation1007 = CType(INDGvFormat1007.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1007.Remove(SettingsExogenousInformation1007)

        If SettingsExogenousInformation1007.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1007 Is Nothing Then
                ListDeleteSettingsExogenousInformation1007 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1007.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1007.Add(SettingsExogenousInformation1007)
        End If

        INDGc1007Format.DataSource = Nothing
        INDGc1007Format.DataSource = ListSettingsExogenousInformation1007
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1003"
    ''' </summary>
    Private Sub RemoveFormat1003()
        SettingsExogenousInformation1003 = CType(INDGvFormat1003.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1003.Remove(SettingsExogenousInformation1007)

        If SettingsExogenousInformation1003.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1003 Is Nothing Then
                ListDeleteSettingsExogenousInformation1003 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1003.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1003.Add(SettingsExogenousInformation1003)
        End If

        INDGc1003Format.DataSource = Nothing
        INDGc1003Format.DataSource = ListSettingsExogenousInformation1003
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1004"
    ''' </summary>
    Private Sub RemoveFormat1004()
        SettingsExogenousInformation1004 = CType(INDGvFormat1004.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1004.Remove(SettingsExogenousInformation1004)

        If SettingsExogenousInformation1004.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1004 Is Nothing Then
                ListDeleteSettingsExogenousInformation1004 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1004.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1004.Add(SettingsExogenousInformation1004)
        End If

        INDGc1004Format.DataSource = Nothing
        INDGc1004Format.DataSource = ListSettingsExogenousInformation1004
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1008"
    ''' </summary>
    Private Sub RemoveFormat1008()
        SettingsExogenousInformation1008 = CType(INDGvFormat1008.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1008.Remove(SettingsExogenousInformation1008)

        If SettingsExogenousInformation1008.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1008 Is Nothing Then
                ListDeleteSettingsExogenousInformation1008 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1008.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1008.Add(SettingsExogenousInformation1008)
        End If

        INDGc1008Format.DataSource = Nothing
        INDGc1008Format.DataSource = ListSettingsExogenousInformation1008
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1009"
    ''' </summary>
    Private Sub RemoveFormat1009()
        SettingsExogenousInformation1009 = CType(INDGvFormat1009.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1009.Remove(SettingsExogenousInformation1009)

        If SettingsExogenousInformation1009.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1009 Is Nothing Then
                ListDeleteSettingsExogenousInformation1009 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1009.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1009.Add(SettingsExogenousInformation1009)
        End If

        INDGc1009Format.DataSource = Nothing
        INDGc1003Format.DataSource = ListSettingsExogenousInformation1009
    End Sub

    ''' <summary>
    ''' Elimina elementos relacionados con el formato "1647"
    ''' </summary>
    Private Sub RemoveFormat1647()
        SettingsExogenousInformation1647 = CType(INDGvFormat1647.GetFocusedRow, SettingsExogenousInformation)
        ListSettingsExogenousInformation1647.Remove(SettingsExogenousInformation1647)

        If SettingsExogenousInformation1647.Id > 0 Then
            If ListDeleteSettingsExogenousInformation1647 Is Nothing Then
                ListDeleteSettingsExogenousInformation1647 = New List(Of SettingsExogenousInformation)
            End If
            SettingsExogenousInformation1647.MarkAsDeleted()
            ListDeleteSettingsExogenousInformation1647.Add(SettingsExogenousInformation1647)
        End If

        INDGc1647Format.DataSource = Nothing
        INDGc1647Format.DataSource = ListSettingsExogenousInformation1647
    End Sub

    ''' <summary>
    ''' Validar la selección de ciertos controles relacionados con diferentes tipos de formatos (1001, 1007, 1003, 1004, 1008, 1009, 1647) y muestra mensajes de advertencia si faltan selecciones
    ''' </summary>
    ''' <param name="FormatType"></param>
    ''' <returns></returns>
    Private Function ValidateControl(FormatType As String) As Boolean

        If FormatType = "1001" Then

            If INDSlConceptCodeFormat1001.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1001.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1001.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1001.Focus()
                Return False
            End If

        ElseIf FormatType = "1007" Then

            If INDSlConceptCodeFormat1007.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1007.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1007.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1007.Focus()
                Return False
            End If

        ElseIf FormatType = "1003" Then

            If INDSlConceptCodeFormat1003.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1003.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1003.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1003.Focus()
                Return False
            End If

        ElseIf FormatType = "1004" Then

            If INDSlConceptCodeFormat1004.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1004.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1004.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1004.Focus()
                Return False
            End If

        ElseIf FormatType = "1008" Then

            If INDSlConceptCodeFormat1008.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1008.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1008.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1008.Focus()
                Return False
            End If

        ElseIf FormatType = "1009" Then

            If INDSlConceptCodeFormat1009.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1009.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1009.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1009.Focus()
                Return False
            End If

        ElseIf FormatType = "1647" Then

            If INDSlConceptCodeFormat1647.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Concepto"
                INDSlConceptCodeFormat1647.Focus()
                Return False
            End If

            If INDSlMainAccountFormat1647.EditValue = Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Cuenta Contable"
                INDSlMainAccountFormat1647.Focus()
                Return False
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' Verifica si una lista de elementos tiene elementos o no
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidarControles() As Boolean

        If ListSettingsExogenousInformation Is Nothing AndAlso ListSettingsExogenousInformation.Count = 0 Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
    End Sub

    ''' <summary>
    ''' Barra botones: cambia la unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing Then
        '    BarraBotones.StatusRecordVisible = False
        '    DeleteBlockedRecord()
        '    CleanControls()
        '    LoadControls()
        '    If SettingFixedAsset IsNot Nothing AndAlso SettingFixedAsset.Id > 0 Then
        '        SettingFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region

End Class