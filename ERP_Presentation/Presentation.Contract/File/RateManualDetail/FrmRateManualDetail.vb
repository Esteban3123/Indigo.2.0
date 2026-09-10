'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class FrmRateManualDetail
    Implements IRateManualDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualId As Integer Implements IRateManualDetail.RateManualId
        Get
            Return INDsleRateManual.EditValue
        End Get
        Set(value As Integer)
            INDsleRateManual.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IRateManualDetail.RateManualXpo
        Get
            Return INDsleRateManual.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRateManual.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IRateManualDetail.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRateManualDetail.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IRateManualDetail.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador de manual de servicios
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PRateManualDetail

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Representa la entidad de grupo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim rateManualDetail As RateManualDetail

    ''' <summary>
    ''' Listado de manuales de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRateManualDetail As List(Of RateManualDetail)

    ''' <summary>
    ''' Listado de eliminados de manules de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateManulaDetail As List(Of RateManualDetail)

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Representa la entidad xpo de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _rateManualXpo As Infrastructure.Data.Xpo.ContractRepository.RateManualXpo

    ''' <summary>
    ''' Variable para controlar el editValueChanged
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSearch As Boolean = True

    ''' <summary>
    ''' True = Guardar - False = Modificar
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSaveModify As Boolean

    ''' <summary>
    ''' Listado de eliminados de rangos de valores para los servicios
    ''' </summary>
    ''' <remarks></remarks>
    'Dim ListDeleteServiceFees As List(Of ServiceFees)

    Dim codeNameRateManual As String

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
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If cupsGroup IsNot Nothing AndAlso cupsGroup.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using Model As New MCupsGroup(Me.Tag.ToString())
        '            AsyncLoader(True)
        '            cupsGroup.MarkAsDeleted()
        '            Dim result = Await Model.DeleteCupsGroup(cupsGroup)
        '            If result.StateResult = True Then
        '                Await Me.DeleteDocumentIndexed()
        '                AsyncLoader(False)
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                Me.Deshacer()
        '            Else
        '                AsyncLoader(False)
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                ElseIf result.MessageResult(0) = "-000" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                End If
        '            End If
        '        End Using
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub

        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MRateManualDetail(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveListRateManualDetail(ListRateManualDetail, ListDeleteRateManulaDetail)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If banSaveModify Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                Else
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                ListRateManualDetail = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        'Await NewCupsGroup()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCupsGroup
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que consulta si el manual tiene servicios asignados, options: True=Search, False=Vituel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultServices(ByVal options As Boolean, Optional id As Integer = 0)
        ActionsOnControls = True

        If options Then
            _rateManualXpo = DirectCast(DirectCast(viewSearchRateManual.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.ContractRepository.RateManualXpo)
            codeNameRateManual = _rateManualXpo.CodeName
        Else
            Using model As New MBusqueda
                Dim _rateManualXpInstant As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.RateManualById, id)
                For Each item As RateManualXpo In _rateManualXpInstant
                    _rateManualXpo = New RateManualXpo
                    _rateManualXpo.Id = item.Id
                    _rateManualXpo.Type = item.Type
                    codeNameRateManual = item.CodeName
                Next
            End Using
        End If

        ConvertXpoToEntity()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    ''' <summary>
    ''' Metodo que convierte la consulta de xpo a un listado de entity
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConvertXpoToEntity()
        Using model As New MBusqueda
            Dim filter() As Object = {_rateManualXpo.Id}
            Dim _listRateManualDetailXpo As XPCollection = model.ConsultarEntidades(eDataSource.ListRateManualDetailByRateManualId, filter)
            ListRateManualDetail = New List(Of RateManualDetail)
            If _listRateManualDetailXpo.Count > 0 Then
                For Each itemXpo As RateManualDetailXpo In _listRateManualDetailXpo
                    Dim _rateManualDetail As New RateManualDetail
                    With _rateManualDetail
                        .Id = itemXpo.Id
                        .RateManualId = itemXpo.RateManualId.Id

                        .IPSServiceId = itemXpo.IPSServiceId.Id
                        .IPSServiceDescription = itemXpo.IPSServiceId.CodeName

                        '.ScoreProcedure = itemXpo.ScoreProcedure
                        '.DiscountPercentage = itemXpo.DiscountPercentage
                        If itemXpo.SurgicalGroupId Is Nothing Then
                            '.SurgicalGroupId = Nothing
                            .SurgicalGroupDescription = String.Empty
                        Else
                            '.SurgicalGroupId = itemXpo.SurgicalGroupId.Id
                            .SurgicalGroupDescription = itemXpo.SurgicalGroupId.CodeName
                        End If

                        '.OutPatientRecoveryFeeType = itemXpo.OutPatientRecoveryFeeType
                        .OutPatientRecoveryFeeTypeDescription = itemXpo.OutPatientRecoveryFeeTypeName

                        '.InPatientRecoveryFeeType = itemXpo.InPatientRecoveryFeeType
                        .InPatientRecoveryFeeTypeDescription = itemXpo.InPatientRecoveryFeeTypeName

                        '.Status = itemXpo.Status
                        '.CreationUser = itemXpo.CreationUser
                        '.CreationDate = itemXpo.CreationDate
                        '.ModificationUser = itemXpo.ModificationUser
                        '.ModificationDate = itemXpo.ModificationDate

                        .MarkAsUnchanged()
                    End With

                    For Each itemServiceFeesXpo As ServiceFeesXpo In itemXpo.ServiceFeesXpo
                        'Dim _serviceFees As New ServiceFees
                        'With _serviceFees
                        '    .Id = itemServiceFeesXpo.Id
                        '    .RateManualDetailId = itemServiceFeesXpo.RateManualDetailId.Id
                        '    .InitialDate = itemServiceFeesXpo.InitialDate
                        '    .EndDate = itemServiceFeesXpo.EndDate
                        '    .SalesValue = itemServiceFeesXpo.SalesValue
                        '    .SalesValueWithSurcharge = itemServiceFeesXpo.SalesValueWithSurcharge
                        '    .ContractMinimumWageId = itemServiceFeesXpo.ContractMinimumWageId.Id
                        '    .ContractMinimumWageDescription = itemServiceFeesXpo.ContractMinimumWageId.CodeName

                        '    .MarkAsUnchanged()
                        'End With
                        '_rateManualDetail.ServiceFees.Add(_serviceFees)
                    Next

                    ListRateManualDetail.Add(_rateManualDetail)
                Next
                INDgcServices.DataSource = Nothing
                INDgcServices.DataSource = ListRateManualDetail
            Else
                ListRateManualDetail = Nothing
                INDgcServices.DataSource = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de servicios y agrega
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenAddService()
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmPopupServices
            AddHandler Formulario.AddRateManualToPrincipalForm, AddressOf ReturnAddRateManualDetail
            Formulario.TypeRateManual = _rateManualXpo.Type
            Formulario.RateManualDescription = codeNameRateManual
            Formulario.RateManualId = RateManualId
            Formulario.rateManualDetail = Nothing
            If ListRateManualDetail IsNot Nothing AndAlso ListRateManualDetail.Count > 0 Then
                Formulario._listPivotIPSServiceId = (From e In ListRateManualDetail Select e.IPSServiceId).ToList()
            End If
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim size As System.Drawing.Size
            size.Width = 1150
            size.Height = 818
            Formulario.Size = size
            Dim transparent As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog()
        End Using
        INDbtnAddServices.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de servicios y modifica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenModifyService()
        Dim rmd = CType(viewGridRateManualDetail.GetFocusedRow, RateManualDetail)
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmPopupServices
            AddHandler Formulario.AddRateManualToPrincipalForm, AddressOf ReturnModifyRateManualDetail
            Formulario.TypeRateManual = _rateManualXpo.Type
            Formulario.RateManualId = RateManualId
            Formulario.RateManualDescription = codeNameRateManual
            Formulario.rateManualDetail = rmd
            Formulario._listPivotIPSServiceId = (From e In ListRateManualDetail Where e.IPSServiceId <> rmd.IPSServiceId Select e.IPSServiceId).ToList()
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim size As System.Drawing.Size
            size.Width = 1150
            size.Height = 818
            Formulario.Size = size
            Dim transparent As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog()
        End Using
        INDbtnAddServices.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que actualiza la rejilla con la informacion del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRateManualDetail(sender As Object, e As AddRateManualToPrincipalFormEventArgs)
        If e.RateManualDetail IsNot Nothing Then
            rateManualDetail = e.RateManualDetail
            If ListRateManualDetail Is Nothing Then
                ListRateManualDetail = New List(Of RateManualDetail)
            End If
            ListRateManualDetail.Add(rateManualDetail)
            INDgcServices.DataSource = Nothing
            INDgcServices.DataSource = ListRateManualDetail
        End If
    End Sub

    ''' <summary>
    ''' Metodo que actualiza la rejilla con la informacion del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnModifyRateManualDetail(sender As Object, e As AddRateManualToPrincipalFormEventArgs)
        If e.DeleteEntity = False Then
            If e.RateManualDetail IsNot Nothing Then
                Dim rmGrid As RateManualDetail = ListRateManualDetail.Find(Function(item) item.Id = e.RateManualDetail.Id)
                With rmGrid
                    .RateManualId = e.RateManualDetail.RateManualId
                    .IPSServiceId = e.RateManualDetail.IPSServiceId
                    .IPSServiceDescription = e.RateManualDetail.IPSServiceDescription
                    '.ScoreProcedure = e.RateManualDetail.ScoreProcedure
                    '.DiscountPercentage = e.RateManualDetail.DiscountPercentage
                    ''.SurgicalGroupId = e.RateManualDetail.SurgicalGroupId
                    '.SurgicalGroupDescription = e.RateManualDetail.SurgicalGroupDescription
                    '.OutPatientRecoveryFeeType = e.RateManualDetail.OutPatientRecoveryFeeType
                    '.OutPatientRecoveryFeeTypeDescription = e.RateManualDetail.OutPatientRecoveryFeeTypeDescription
                    '.InPatientRecoveryFeeType = e.RateManualDetail.InPatientRecoveryFeeType
                    .InPatientRecoveryFeeTypeDescription = e.RateManualDetail.InPatientRecoveryFeeTypeDescription
                    '.Status = e.RateManualDetail.Status
                    '.ModificationUser = e.RateManualDetail.ModificationUser
                    '.ModificationDate = e.RateManualDetail.ModificationDate

                    'If e.SaveModify Then
                    '    .ServiceFees.Clear()
                    '    While e.RateManualDetail.ServiceFees.Count > 0
                    '        .ServiceFees.Add(e.RateManualDetail.ServiceFees.Item(0))
                    '    End While
                    'Else
                    '    For Each itemService As ServiceFees In e.RateManualDetail.ServiceFees
                    '        .ServiceFees.Add(itemService)
                    '    Next
                    'End If

                End With
                INDgcServices.RefreshDataSource()
            End If
        Else
            ListRateManualDetail.Remove(e.RateManualDetail)
            INDgcServices.DataSource = Nothing
            INDgcServices.DataSource = ListRateManualDetail
        End If
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRateManualDetail.ActionsOnControls
        Set(value As Boolean)
            INDsleRateManual.Properties.ReadOnly = value
            INDbtnAddServices.Enabled = value
            INDgcServices.Enabled = value
            If value Then
                INDbtnAddServices.Focus()
            Else
                INDsleRateManual.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Presenter.InitializeRateManual()
        banSearch = False
        RateManualId = CInt(Me.IdEntity.Trim())
        banSearch = True
        If ListRateManualDetail IsNot Nothing AndAlso ListRateManualDetail.Count > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                ConsultServices(False, Me.IdEntity.Trim())
            End If
        Else 'Realiza la consulta normal
            ConsultServices(False, Me.IdEntity.Trim())
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity =  String.Empty
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        'DeleteBlockedRecord()
        'INDbtnCode.Text = ReturnValue
        'If INDbtnCode.Text <> String.Empty Then
        '    LoadControls()
        '    If INDbtnCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDbtnCode.Enabled = False
        'End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsGroup.Code, Me.cupsGroup.Name), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.cupsGroup.Code & "#$", .IdForm = CStr(Me.Tag), _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsGroup.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsGroup.Code, Me.cupsGroup.Name)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsGroup.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRateManualDetail.BeginUpdate()
        ActionsOnControls = False
        banSearch = False
        RateManualId = Nothing
        banSearch = True
        ListRateManualDetail = Nothing
        ListDeleteRateManulaDetail = Nothing
        'ListDeleteServiceFees = Nothing
        INDsleRateManual.Properties.NullText = String.Empty
        INDgcServices.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        INDlyRateManualDetail.EndUpdate()
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If ListRateManualDetail Is Nothing OrElse ListRateManualDetail.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        For Each itemRateManualDetail As RateManualDetail In ListRateManualDetail
            itemRateManualDetail.RateManualId = RateManualId

            'If ListDeleteServiceFees IsNot Nothing Then
            '    For Each itemServiceFees As ServiceFees In ListDeleteServiceFees
            '        If itemServiceFees.RateManualDetailId = itemRateManualDetail.Id Then
            '            itemRateManualDetail.ServiceFees.Add(itemServiceFees)
            '        End If
            '    Next
            'End If
        Next
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'Using Model As New MCupsGroup(CStr(Me.Tag))
        '    Dim resultOperation = Await RunAsyncOperation(Model.GetCupsGroup(INDbtnCode.Text.Trim))
        '    cupsGroup = resultOperation.ObjectEmbbeded
        '    If Not cupsGroup Is Nothing Then
        '        If cupsGroup.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(cupsGroup.Id))
        '                With cupsGroup
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Code = .Code
        '                    NameCG = .Name
        '                    Description = .Description
        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.cupsGroup.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = cupsGroup.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning)
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.SetDocuments(cupsGroup.Id)
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.Code = String.Empty
        '        End If
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        Me.Code = String.Empty
        '    End If
        'End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ChangeState() As Task
        'If Not String.IsNullOrEmpty(Code) Then
        '    Dim state As Boolean
        '    Select Case Status
        '        Case CBool(eActionsStatusRecords.Active)
        '            state = True
        '        Case CBool(eActionsStatusRecords.Inactive)
        '            state = False
        '    End Select
        '    Using model As New MCupsGroup(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await model.ChangeState(Code, state)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Else
        '    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        'End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        rateManualDetail = Nothing
        ListRateManualDetail = Nothing
        ListDeleteRateManulaDetail = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        _rateManualXpo = Nothing
        banSearch = Nothing
        banSaveModify = Nothing
        codeNameRateManual = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRateManualDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRateManualDetail, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PRateManualDetail(Me)
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcServices)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewGridRateManualDetail, ListActions)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRateManualDetail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRateManualDetail_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleRateManual.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton mas del control de manual tarifario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManual.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManual With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeRateManual()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de manual de tarifas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManual.QueryPopUp
        If INDsleRateManual.Properties.DataSource Is Nothing Then
            Presenter.InitializeRateManual()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddServices_Click(sender As Object, e As EventArgs) Handles INDbtnAddServices.Click
        OpenAddService()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de manual tarifario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateManual.EditValueChanged
        If RateManualId <> Nothing AndAlso banSearch = True Then
            ConsultServices(True)
        End If
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
       OpenModifyService()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        OpenModifyService()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        banSaveModify = False
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        banSaveModify = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.ContractSequenceDetail IsNot Nothing Then
        '    If Me._sequense.ContractSequenceDetail.Any(Function(S) S.OperatingUnitId = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.ContractSequenceDetail.Where(Function(s) s.OperatingUnitId = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If
    End Sub

#End Region

End Class