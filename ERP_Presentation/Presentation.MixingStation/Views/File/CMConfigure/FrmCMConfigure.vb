'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Judy Andrea D�az Reyes
' Created          : 20-05-2019
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Utils.Extensions
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmCMConfigure
    Implements ICMConfig, ICustomizableForm

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Representa la entidad del centro de mezclas
    ''' </summary>
    Dim _CMConfiguration As CMConfiguration

    '''' <summary>
    '''' Representa la entidad del centro de atencion
    '''' </summary>
    'Dim _CMCenterAttention As CMCenterAttention
    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PCMConfig

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' Variable que contiene la lista de tipos de central de mezclas
    ''' </summary>
    Dim ListCMType As New List(Of Tuple(Of Integer, String))

    'Dim idMixingStation

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim _CMCenterAttention As CMCenterAttention

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim _CMWareHouse As CMWarehouse

    ''' <summary>
    ''' Entiad de horario
    ''' </summary>
    Dim CMConfigurationSchedule As CMConfigurationSchedule

    ''' <summary>
    ''' Entiad de horario
    ''' </summary>
    Dim ListDeleteCMConfigurationSchedule As List(Of CMConfigurationSchedule)

    ''' <summary>
    ''' Listado de eliminados de los detalles de centros de atencion, linea de produccion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteCenterAttentionProduction As List(Of CMCenterAttention)

    ''' <summary>
    ''' Listado de eliminados de los detalles de centros de atencion, linea de produccion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteWareHouse As List(Of CMWarehouse)

    ''' <summary>
    '''
    ''' </summary>
    Dim ListCMCenterLineUnit As List(Of SP_CMCenterLineUnit_Result)

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo

    ''' <summary>
    ''' Listado de usuarios
    ''' </summary>
    Private ListCMConfigurationUsers As List(Of CMConfigurationUsers)

    ''' <summary>
    ''' Listado de usuarios
    ''' </summary>
    Private ListDeleteCMConfigurationUsers As List(Of CMConfigurationUsers)

    ''' <summary>
    ''' Tabla de detalle
    ''' </summary>
    Dim CMExternalCareCenter As CMExternalCareCenter

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListCMExternalCareCenter As List(Of CMExternalCareCenter)

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListDeleteCMExternalCareCenter As List(Of CMExternalCareCenter)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

#End Region

#Region "Propierties ICMConfigure"
    ''' <summary>
    ''' Obtiene o establece el codigo de la central de mezcla
    ''' </summary>
    Public Property Code As String Implements ICMConfig.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements ICMConfig.State
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
    ''' Obtiene o establece el nombre de la central de mezcla
    ''' </summary>
    ''' <value></value>
    Public Property CmName As String Implements ICMConfig.cmName
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los tipos de central de mezcla
    ''' </summary>
    ''' <returns></returns>
    Public Property MSType As Byte? Implements ICMConfig.msType
        Get
            Return CStr(INDsleCmType.EditValue)
        End Get
        Set(value As Byte?)
            INDsleCmType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    Public Property Sequence As MixingStationSequence Implements ICMConfig.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para habilitar o deshabilitar controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICMConfig.ActionsOnControls
        Set(value As Boolean)
            INDlycBase.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtPrefix.Enabled = value
            INDsleCmType.Enabled = value
            INDSleProductionLine.Enabled = value
            INDSbAddLine.Enabled = value
            INDGvLineDetail.OptionsBehavior.ReadOnly = Not value
            INDGvCenterAttention.OptionsBehavior.ReadOnly = Not value
            INDGvDeliveryTime.OptionsBehavior.ReadOnly = Not value
            INDGcExternalCenter.Enabled = value
            INDGcCenterAttention.Enabled = value
            INDGcDeliveryTime.Enabled = value
            INDGcLineDetail.Enabled = value
            INDGcWareHouse.Enabled = value
            INDGcWorkingAreas.Enabled = value
            INDsleUsers.Enabled = value
            INDbtnAddUser.Enabled = value
            INDgcUsers.Enabled = value
            INDsleDirectPr.Enabled = value
            INDsleDirectSp.Enabled = value

            INDSbAddWorkingAreas.Enabled = value
            INDSbAddWarehouse.Enabled = value
            INDSbAddExternalAttentionCenter.Enabled = value
            INDSbAddAttentionCenter.Enabled = value
            INDlycBase.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements ICMConfig.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el Layout del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICMConfig.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    '''
    ''' </summary>
    Private _ListCenterAttentionProductionLine As Domain.Entities.TrackableCollection(Of CMCenterAttention)

    ''' <summary>
    ''' 
    ''' </summary>
    Private _listaCMWareHouse As Domain.Entities.TrackableCollection(Of CMWarehouse)

    ''' <summary>
    ''' Listado de los detalles de centro de atencion y lineas de produccion
    ''' </summary>
    ''' <remarks></remarks>
    Property ListCenterAttentionProductionLine As Domain.Entities.TrackableCollection(Of CMCenterAttention)
        Get
            Return _ListCenterAttentionProductionLine
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of CMCenterAttention))
            _ListCenterAttentionProductionLine = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCMWareHouse As New Domain.Entities.TrackableCollection(Of CMWarehouse)

    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProductionLine As Integer? Implements ICMConfig.IdProductionLine
        Get
            Return INDSleProductionLine.EditValue
        End Get
        Set(value As Integer?)
            INDSleProductionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    Property ProductionLineDatasource As XPInstantFeedbackSource Implements ICMConfig.ProductionLineDatasource
        Get
            Return CType(INDSleProductionLine.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProductionLine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el prefijo de la central de mezcla
    ''' </summary>
    ''' <value></value>
    Public Property CmPrefix As String Implements ICMConfig.cmPrefix
        Get
            Return INDtxtPrefix.EditValue
        End Get
        Set(value As String)
            INDtxtPrefix.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDirector As Integer Implements ICMConfig.IdDirector
        Get
            Return INDsleDirectPr.EditValue
        End Get
        Set(value As Integer)
            INDsleDirectPr.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDirectorSp As Integer? Implements ICMConfig.IdDirectorSp
        Get
            Return INDsleDirectSp.EditValue
        End Get
        Set(value As Integer?)
            INDsleDirectSp.EditValue = value
        End Set
    End Property

#End Region

#Region "Propierties ICrudBase"

    ''' <summary>
    ''' Propiedad que establece los mensajes (Advertencias)
    ''' </summary>
    ''' <param name="icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento barra de botones Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina una central de mezcla
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'If Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using Model As New MCMConfig(Me.Tag.ToString())
        '                AsyncLoader(True)
        '                Dim result = Await Model.DeleteUnitDoseTypeAsync(Me._CMConfiguration)
        '                If result.StatusCode = eStatusResult.SUCCESS Then
        '                    Me.DeleteDocumentIndexed()
        '                    AsyncLoader(False)
        '                    Me.Deshacer()
        '                Else
        '                    AsyncLoader(False)
        '                    INDbtnCode.Enabled = False
        '                End If
        '                ShowMessage(result.StatusCode) = result.Message
        '            End Using
        '        Catch ex As Exception
        '            AsyncLoader(False)
        '            INDbtnCode.Enabled = False
        '            Throw ex
        '        End Try
        '    End If
        'End If

    End Sub

    ''' <summary>
    ''' Guarda central de mezclas
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If Not ValidateData() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCMConfig(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of CMConfiguration) = Await Model.SaveCMConfigAsync(Me._CMConfiguration, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _CMConfiguration.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    If _CMConfiguration.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._CMConfiguration = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Update, _CMConfiguration.Id, 0, _CMConfiguration.Id)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    Me.AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            'Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Limpia el formulario para iniciar
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence?.IsManual Is Nothing OrElse Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewMixingCenter()
        End If
        INDbtnCode.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Try
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "CreationDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                                  New ColumnInfo() With {.Caption = "Estado", .FieldName = "StateName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
                .ValorSolicitado = "Code"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMixingCenter
                .FormParent = Me
                .ShowSearch()
            End With
        Catch ex As Exception
            Dim hola As Boolean
            hola = True
        End Try
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Cambia el estado del centro de atención externo
    ''' </summary>
    Private Sub ChangeStateCMExternalCareCenter()
        Dim info = DirectCast(INDviewCMExternalCareCenter.GetFocusedRow(), CMExternalCareCenter)
        info.Status = Not info.Status
        INDGcExternalCenter.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina un centro atención externo
    ''' </summary>
    Private Sub DeleteCMExternalCareCenter()
        Dim entityDelete = DirectCast(INDviewCMExternalCareCenter.GetFocusedRow(), CMExternalCareCenter)
        If entityDelete.Id > 0 Then
            If ListDeleteCMExternalCareCenter Is Nothing Then
                ListDeleteCMExternalCareCenter = New List(Of CMExternalCareCenter)
            End If
            ListDeleteCMExternalCareCenter.Add(entityDelete.MarkAsDeleted())
        End If
        ListCMExternalCareCenter.Remove(entityDelete)
        INDGcExternalCenter.DataSource = Nothing
        INDGcExternalCenter.DataSource = ListCMExternalCareCenter
        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Edita un centro atención externo
    ''' </summary>
    Private Sub EditCMExternalCareCenter()
        CMExternalCareCenter = DirectCast(INDviewCMExternalCareCenter.GetFocusedRow(), CMExternalCareCenter)
        IndexEditRecord = ListCMExternalCareCenter.IndexOf(CMExternalCareCenter)
        OpenFormCMExternalCareCenter(True)
    End Sub

    ''' <summary>
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormCMExternalCareCenter(EditMode As Boolean)
        Using formulario As New FrmCMExternalCareCenter()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddCMExternalCareCenterArgs, AddressOf ReturnAddEventArgs
            formulario.EditModeDetail = EditMode
            If EditMode Then
                formulario.ListCMExternalCareCenterCompare = (From x In ListCMExternalCareCenter Where Not x.Equals(CMExternalCareCenter)).ToList()
            Else
                formulario.ListCMExternalCareCenterCompare = ListCMExternalCareCenter
            End If
            formulario.CMExternalCareCenter = CMExternalCareCenter
            formulario.IdMixingStation = _CMConfiguration?.Id

            If Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.CMMixingProducitonLine IsNot Nothing _
                AndAlso Me._CMConfiguration.CMMixingProducitonLine.Any(Function(pl) pl.StatePl AndAlso (Not pl.ChangeTracker.State = ObjectState.Deleted)) Then
                formulario.ProductionLinesIds = String.Join(",", Me._CMConfiguration.CMMixingProducitonLine.Where(Function(pl) pl.StatePl AndAlso
                                                    (Not pl.ChangeTracker.State = ObjectState.Deleted)).Select(Function(pl) pl.Id_ProductionLine))
            End If
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(700, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddCMExternalCareCenter)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListCMExternalCareCenter Is Nothing Then
                    ListCMExternalCareCenter = New List(Of CMExternalCareCenter)
                End If
                ListCMExternalCareCenter.Add(e.CMExternalCareCenter)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListCMExternalCareCenter.Remove(CMExternalCareCenter)
                ListCMExternalCareCenter.Insert(IndexEditRecord, e.CMExternalCareCenter)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDGcExternalCenter.DataSource = Nothing
            INDGcExternalCenter.DataSource = ListCMExternalCareCenter
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedrecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Valida controles obligatorios
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateData() As Boolean
        ValidateData = True
        If Me._CMConfiguration IsNot Nothing AndAlso (Me._CMConfiguration.CMMixingProducitonLine Is Nothing OrElse Me._CMConfiguration.CMMixingProducitonLine.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar una línea de producción"
            ValidateData = False
        End If

        If IdDirector = IdDirectorSp Then
            Mensaje(EeventViewerImages.Advertencia) = "El Director técnico principal no puede ser el mismo Director t�cnico suplente"
            ValidateData = False
        End If

        If IdDirector = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar un director Principal"
            ValidateData = False
        End If

        If Me.MSType = 1 AndAlso (Me.ListCenterAttentionProductionLine Is Nothing OrElse Me.ListCenterAttentionProductionLine.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar un centro de atención (propios)"
            ValidateData = False
        End If

        If ListCMCenterLineUnit IsNot Nothing AndAlso Me.ListCMCenterLineUnit.Any(Function(item) item.StatusCL AndAlso (item.EveryTimeDeliverId Is Nothing OrElse item.FirstDeliveryTime Is Nothing)) Then
            Mensaje(EeventViewerImages.Advertencia) = "Hay unidades funcionales sin horario de entrega a enfermeria"
            ValidateData = False
        End If

        If listCMWareHouse.Count > 0 Then

            If Not Me.listCMWareHouse.Any(Function(item) item.WarehouseType = 1 AndAlso item.StateWH = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe asignar almacén tipo : Materia prima stock"
                Return False
            End If

            If Not Me.listCMWareHouse.Any(Function(item) item.WarehouseType = 3 AndAlso item.StateWH = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe asignar almacén tipo : En proceso"
                Return False
            End If

            If Not Me.listCMWareHouse.Any(Function(item) item.WarehouseType = 4 AndAlso item.StateWH = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe asignar almacén tipo : Producto terminado"
                Return False
            End If

            If Not Me.listCMWareHouse.Any(Function(item) item.WarehouseType = 5 AndAlso item.StateWH = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe asignar almacén tipo : Inventario de control"
                Return False
            End If

            If Not Me.listCMWareHouse.Any(Function(item) item.WarehouseType = 6 AndAlso item.StateWH = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe asignar almacén tipo : Remanente"
                Return False
            End If

        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar los almacenes"
            ValidateData = False
        End If

        If _CMConfiguration.WorkingArea Is Nothing OrElse Not _CMConfiguration.WorkingArea.Any(Function(wa) wa.Status AndAlso wa.ChangeTracker.State <> ObjectState.Deleted) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar como mínimo un area de trabajo activa"
            Return False
        End If
        Return ValidateData
    End Function

    ''' <summary>
    ''' Metodo que elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedrecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._CMConfiguration.Code, _idOperativeUnit, Me._CMConfiguration.CreationDate, Me._CMConfiguration.MixingStationType),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._CMConfiguration.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._CMConfiguration.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._CMConfiguration.Code, _idOperativeUnit, Me._CMConfiguration.CreationDate, Me._CMConfiguration.MixingStationType)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._CMConfiguration.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCMConfig(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetCMConfigAsync(INDbtnCode.Text.Trim)
                    INDlycBase.BeginUpdate()
                    _CMConfiguration = resultOperation.ObjectEmbbeded
                    If _CMConfiguration IsNot Nothing AndAlso _CMConfiguration.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_CMConfiguration.Id))
                            With _CMConfiguration
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                CmName = .Name
                                CmPrefix = .Prefix
                                MSType = .MixingStationType
                                Status = .State
                                ListCenterAttentionProductionLine = Me._CMConfiguration.CMCenterAttention
                                listCMWareHouse = Me._CMConfiguration.CMWarehouse
                                RefrescarRejilla()
                                RefrescarRejillaLine()
                                RefrescarRejillaWareHouse()
                                LoadListCMCenterLineUnit()

                                ListCMConfigurationUsers = .CMConfigurationUsers.ToList
                                INDgcUsers.DataSource = Nothing
                                INDgcUsers.DataSource = ListCMConfigurationUsers
                                IdDirector = .IdDirector
                                IdDirectorSp = .IdDirectorSp
                                INDGcWorkingAreas.DataSource = .WorkingArea
                                INDGcWorkingAreas.RefreshDataSource()
                                ListCMExternalCareCenter = .CMExternalCareCenter.ToList()
                                INDGcExternalCenter.DataSource = Nothing
                                INDGcExternalCenter.DataSource = ListCMExternalCareCenter
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._CMConfiguration.Code)

                            INDsleDirectPr.Properties.NullText = _CMConfiguration.DirectorUserCodeName
                            INDsleDirectSp.Properties.NullText = _CMConfiguration.DirectorSpUserCodeName

                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _CMConfiguration.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_CMConfiguration.Id, Me.Tag.ToString(), Nothing, GetType(CMConfiguration).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewMixingCenter()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlycBase.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateUser()
        If ListCMConfigurationUsers Is Nothing Then
            ListCMConfigurationUsers = New List(Of CMConfigurationUsers)
        Else
            If ListCMConfigurationUsers.FindAll(Function(item) item.UserId = INDsleUsers.EditValue).ToList().Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", "Inventory")
                INDsleUsers.Focus()
                Exit Sub
            End If
        End If
        Dim CMConfigurationUsers As New CMConfigurationUsers
        With CMConfigurationUsers
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .CodeNameUser = $"{_usersXpo.UserCode} - {_usersXpo.IdPerson.Fullname}"
            .FullNameUser = _usersXpo.IdPerson.Fullname
            .PositionName = _usersXpo.Position
        End With
        ListCMConfigurationUsers.Add(CMConfigurationUsers)
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListCMConfigurationUsers
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", "Inventory")
        INDsleUsers.EditValue = Nothing
        INDsleUsers.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteUser()
        Dim CMConfigurationUsers As CMConfigurationUsers = CType(viewUsersGrid.GetFocusedRow, CMConfigurationUsers)
        If CMConfigurationUsers.Id <> 0 Then
            If ListDeleteCMConfigurationUsers Is Nothing Then
                ListDeleteCMConfigurationUsers = New List(Of CMConfigurationUsers)
            End If
            CMConfigurationUsers.MarkAsDeleted()
            ListDeleteCMConfigurationUsers.Add(CMConfigurationUsers)

            If IdDirector = CMConfigurationUsers.UserId Then
                IdDirector = Nothing
                INDsleDirectPr.Properties.NullText = String.Empty
            End If
            If IdDirectorSp = CMConfigurationUsers.UserId Then
                IdDirectorSp = Nothing
                INDsleDirectSp.Properties.NullText = String.Empty
            End If
        End If
        ListCMConfigurationUsers.Remove(CMConfigurationUsers)
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListCMConfigurationUsers
    End Sub

    ''' <summary>
    ''' Metodo que prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMixingCenter() As Task
        Status = True
        _CMConfiguration = New CMConfiguration() With {.State = True}
        Me._CMConfiguration.CMCenterAttention = New Domain.Entities.TrackableCollection(Of CMCenterAttention)
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task

        If Not String.IsNullOrEmpty(Me._CMConfiguration.Code) Then
            Try
                Using model As New MCMConfig(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._CMConfiguration.State
                    Dim result As ActionResult(Of CMConfiguration) = Await model.ChangeState(Me._CMConfiguration.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._CMConfiguration = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Deshacer()
                    Else
                        INDbtnCode.Enabled = False
                        If result.MessageResult Is Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        ElseIf result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If

                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Maneja el evento Load de la barra de botones.
    ''' </summary>
    ''' <param name="sender">Referencia al objeto que lanza el evento .</param>
    ''' <param name="e">Instancia que contiene los datos del evento.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Maneja el evento actualizar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento buscar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Maneja el evento deshacer de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Maneja el evento eliminar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Maneja el evento guardar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento nuevo de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Maneja el evento imprimir de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _CMConfiguration.Id, 0, _CMConfiguration.Id)
    End Sub

    ''' <summary>
    ''' Maneja el evento cambio de unidad operativa de la barra de botones
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "BeforePopup"

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRipceEditDeliveryTime_BeforePopup(sender As Object, e As EventArgs) Handles INDRipceEditDeliveryTime.BeforePopup
        Dim clu = DirectCast(INDGvDeliveryTime.GetFocusedRow(), SP_CMCenterLineUnit_Result)
        If clu IsNot Nothing Then
            INDGleEveryTimeDeliver.EditValue = clu.EveryTimeDeliverId
            VisualizarControles(clu.EveryTimeDeliverId)
            If clu.EveryTimeDeliverId IsNot Nothing Then
                Select Case clu.EveryTimeDeliverId
                    Case 1
                        INDTmeFirstDeliveryTime.EditValue = clu.FirstDeliveryTime
                        INDTmeSecondDeliveryTime.EditValue = clu.SecondDeliveryTime
                        INDTmeThirdDeliveryTime.EditValue = clu.ThirdDeliveryTime
                        INDTmeFourthDeliveryTime.EditValue = clu.FourthDeliveryTime
                    Case 2
                        INDTmeFirstDeliveryTime.EditValue = clu.FirstDeliveryTime
                        INDTmeSecondDeliveryTime.EditValue = clu.SecondDeliveryTime
                        INDTmeThirdDeliveryTime.EditValue = clu.ThirdDeliveryTime
                    Case 3
                        INDTmeFirstDeliveryTime.EditValue = clu.FirstDeliveryTime
                        INDTmeSecondDeliveryTime.EditValue = clu.SecondDeliveryTime
                    Case 4
                        INDTmeFirstDeliveryTime.EditValue = clu.FirstDeliveryTime
                End Select
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de central de mezcla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCmType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCmType.EditValueChanged
        If MSType IsNot Nothing Then
            If MSType = 1 Then
                INDLcgCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgExternalCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf MSType = 2 Then
                INDLcgCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgExternalCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf MSType = 3 Then
                INDLcgCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcgExternalCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entrega cada cierta horas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleEveryTimeDeliver_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleEveryTimeDeliver.EditValueChanged
        VisualizarControles(INDGleEveryTimeDeliver.EditValue)
        AjustarHorasEntrega()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control primera entrega
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTmeFirstDeliveryTime_EditValueChanged(sender As Object, e As EventArgs) Handles INDTmeFirstDeliveryTime.EditValueChanged
        If INDTmeFirstDeliveryTime.EditValue IsNot Nothing Then
            AjustarHorasEntrega()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDsleUsers_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleUsers.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim list = INDsleUsers.GetFocusedRow(Of Infrastructure.Data.Xpo.MixingStationRepository.ViewListUserByContainerCM)()
            Dim IndigoSessionValues As SessionValues = SessionValues.Instance
            _usersXpo = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "Id = '" & e.NewValue & "'").FirstOrDefault
            _usersXpo.Position = list.PositionName
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If INDsleUsers.Properties.DataSource Is Nothing Then
            INDsleUsers.Properties.DataSource = _presenter.ViewListUserByContainer()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios en Director tecnico principal
    ''' </summary>
    Private Sub INDsleDirectPr_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDirectPr.QueryPopUp
        If ListCMConfigurationUsers?.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encuentran usuarios autorizados para la central de mezclas"
            Exit Sub
        End If

        INDsleDirectPr.Properties.DataSource = ListCMConfigurationUsers
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios en director tecnico suplente
    ''' </summary>
    Private Sub INDsleDirectSp_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDirectSp.QueryPopUp
        If ListCMConfigurationUsers?.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encuentran usuarios autorizados para la central de mezclas"
            Exit Sub
        End If

        INDsleDirectSp.Properties.DataSource = ListCMConfigurationUsers
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de lineas de producción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductionLine.QueryPopUp
        If INDSleProductionLine.Properties.DataSource Is Nothing Then
            _presenter.InitializeProductionLine()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de horas de entrega
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleEveryTimeDeliver_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleEveryTimeDeliver.QueryPopUp
        If INDGleEveryTimeDeliver.Properties.DataSource Is Nothing Then
            Dim _items As New List(Of Tuple(Of Byte, String))
            _items.Add(New Tuple(Of Byte, String)(1, "6"))
            _items.Add(New Tuple(Of Byte, String)(2, "8"))
            _items.Add(New Tuple(Of Byte, String)(3, "12"))
            _items.Add(New Tuple(Of Byte, String)(4, "24"))
            INDGleEveryTimeDeliver.Properties.DataSource = _items
        End If
    End Sub

#End Region

#Region "ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de asociacion central de mezcla y lineas de produccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewCenterAttention.Click_ButtonAction
        Dim buttonTag As String = String.Empty
        Dim button = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If button IsNot Nothing Then
            buttonTag = button.Tag.ToString
        Else
            Dim _buttonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If _buttonEdit IsNot Nothing Then
                buttonTag = _buttonEdit.Text
            End If
        End If
        Select Case buttonTag
            Case "Eliminar", "Remove"
                DeleteDetail()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateRelacion()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de lineas de produccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewLineDetail.Click_ButtonAction
        Dim buttonTag As String = String.Empty
        Dim button = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If button IsNot Nothing Then
            buttonTag = button.Tag.ToString
        Else
            Dim _buttonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If _buttonEdit IsNot Nothing Then
                buttonTag = _buttonEdit.Text
            End If
        End If
        Select Case buttonTag
            Case "Eliminar", "Remove"
                DeleteDetailLine()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateLine()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de lineas de produccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView11_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewWareHouse.Click_ButtonAction
        Dim buttonTag As String = String.Empty
        Dim button = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If button IsNot Nothing Then
            buttonTag = button.Tag.ToString
        Else
            Dim _buttonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If _buttonEdit IsNot Nothing Then
                buttonTag = _buttonEdit.Text
            End If
        End If
        Select Case buttonTag
            Case "Eliminar", "Remove"
                DeleteDetailWareHouse()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateWareHouse()
        End Select
    End Sub

#End Region

#Region "ContexMenuActions"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewCenterAttention.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Eliminar", "Remove"
                DeleteDetail()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateRelacion()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView11_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewWareHouse.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Eliminar", "Remove"
                DeleteDetailWareHouse()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateWareHouse()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewLineDetail.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Eliminar", "Remove"
                DeleteDetailLine()
            Case "Cambiar Estado", "ChangeState"
                ChangeStateLine()
        End Select
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewUsers.Click_ButtonAction
        DeleteUser()
    End Sub

    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewUsers.ContexMenuActions
        DeleteUser()
    End Sub

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView7_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView7.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditCMExternalCareCenter()
            Case "Remove"
                DeleteCMExternalCareCenter()
            Case "ChangeState"
                ChangeStateCMExternalCareCenter()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView7_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView7.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditCMExternalCareCenter()
            Case "Remove"
                DeleteCMExternalCareCenter()
            Case "ChangeState"
                ChangeStateCMExternalCareCenter()
        End Select
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCMConfigure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Agrega a la rejilla la columna de Acciones
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.ChangeState)

        IndigoGridViewCenterAttention.SetListAcction(INDGvCenterAttention, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCenterAttention.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        AddActionsColumns()
        IndigoGridViewCenterAttention.MoreInfoColunmns(INDGvCenterAttention)
        IndigoGridViewLineDetail.SetListAcction(INDGvLineDetail, ListActions)
        IndigoGridViewWareHouse.SetListAcction(INDGvWareHouse, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvWareHouse.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                'col.MaxWidth = 100
                'col.MinWidth = 100
            End If
        Next

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Remove)
        IndigoGridViewUsers.SetListAcction(viewUsersGrid, ListActions2)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewUsersGrid.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvLineDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                'col.MaxWidth = 100
                'col.MinWidth = 100
            End If
        Next

        IndigoGridViewWorkingArea.SetListAcction(INDGvWorkingAreas, {eAcciones.Edit, eAcciones.Remove}.ToList())
        Dim columnAction = INDGvWorkingAreas.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        columnAction.Width = 100

        Me.LayoutControls.SetIsCustomizable(Me.INDlycBase, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCMConfig(Me)
        _presenter.GetSequence()

        IndigoGridControl1.RefreshGrid(INDGcCenterAttention)
        IndigoGridControl1.RefreshGrid(INDGcLineDetail)
        IndigoGridControl1.RefreshGrid(INDGcDeliveryTime)
        IndigoGridControl1.RefreshGrid(INDGcWareHouse)

        InitializeTuples()
        LoadStatus()
        Deshacer()

    End Sub

#End Region

#Region "Disposed"

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        _presenter = Nothing
        _model = Nothing
        _CMConfiguration = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _CMCenterAttention = Nothing
        ListDeleteCenterAttentionProduction = Nothing
        ListCMType = Nothing
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmCMConfigure_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedrecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar una central de mezcla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewMixingCenter()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCMConfigure_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Método para agregar acciones a la rejilla de productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)

        ListActions.Add(eAcciones.ChangeState)
        IndigoGridView7.SetListAcction(INDviewCMExternalCareCenter, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewCMExternalCareCenter.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Evento click del boton agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", "Inventory")
            INDsleUsers.Focus()
            Exit Sub
        End If

        CreateUser()
    End Sub

    ''' <summary>
    ''' Agrega lineas de produccion a la central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAddLine_Click(sender As Object, e As EventArgs) Handles INDSbAddLine.Click
        If Me._CMConfiguration Is Nothing Then
            Me._CMConfiguration = New CMConfiguration
        End If
        If Me._CMConfiguration.CMMixingProducitonLine Is Nothing Then
            Me._CMConfiguration.CMMixingProducitonLine = New Domain.Entities.TrackableCollection(Of CMMixingProducitonLine)
        End If
        If Me.IdProductionLine Is Nothing OrElse Me.IdProductionLine = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una línea de producción"
            Exit Sub
        ElseIf Me._CMConfiguration.CMMixingProducitonLine.Any(Function(d) Not d.ChangeTracker.State = ObjectState.Deleted) _
            AndAlso Me._CMConfiguration.CMMixingProducitonLine.Any(Function(d) (Not d.ChangeTracker.State = ObjectState.Deleted) AndAlso d.Id_ProductionLine = Me.IdProductionLine) Then
            Mensaje(EeventViewerImages.Advertencia) = "La línea de producción ya se encuentra agregada"
            Exit Sub
        End If
        Dim _d As New CMMixingProducitonLine
        Dim pl As MixingStationProductionLineXpo
        _d.Id_ProductionLine = Me.IdProductionLine
        pl = _presenter.GetProductionLineById(Me.IdProductionLine)
        If pl IsNot Nothing Then
            _d.ProductionLineCode = pl.Code
            _d.ProductionLineName = pl.Name
        End If
        _d.StatePl = True
        _d.StatusName = "Activo"
        Me._CMConfiguration.CMMixingProducitonLine.Add(_d)
        RefrescarRejillaLine()
        INDSleProductionLine.Text = String.Empty
        INDSleProductionLine.EditValue = 0
    End Sub

    Private Sub OpenPopupWarehouse()
        Using formulario As New FrmPopupWareHouse
            Me.Cursor = ChangeCursorIndigo()
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            If Me._CMConfiguration IsNot Nothing Then
                formulario.MixingStationId = Me._CMConfiguration.Id
            End If

            Dim transparent = New Base.FrmTransparent(formulario, False)

            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            For Each caa In formulario.CMWarehouse
                Dim terminado As Boolean = False
                Dim Stock As Boolean = False
                If listCMWareHouse IsNot Nothing Then
                    'Valido Codigo tipo Almacén no sea Igual
                    If listCMWareHouse.Where(Function(x) x.WareHouseCode = caa.WareHouseCode AndAlso caa.WarehouseType = 2).Count > 0 Then 'almacen
                        Mensaje(EeventViewerImages.Advertencia) = "El Almacén ya se encuentra agregado"
                        Exit Sub
                    End If

                    If listCMWareHouse.Where(Function(x) x.WareHouseCode = caa.WareHouseCode AndAlso caa.WarehouseType = 3).Count > 0 Then 'proceso
                        Mensaje(EeventViewerImages.Advertencia) = "El almacén ya se encuentra agregado"
                        Exit Sub
                    End If

                    If listCMWareHouse.Where(Function(x) x.WareHouseCode = caa.WareHouseCode).Count > 0 Then
                        If caa.WarehouseType = 4 Then
                            If listCMWareHouse.Where(Function(x) x.WarehouseType = 1).Count > 0 Then
                                terminado = True
                            End If
                        End If
                        If caa.WarehouseType = 1 Then
                            If listCMWareHouse.Where(Function(x) x.WarehouseType = 4).Count > 0 Then
                                Stock = True
                            End If
                        End If
                        If terminado = False AndAlso Stock = False Then
                            If listCMWareHouse.Where(Function(x) x.WarehouseType = 2).Count > 0 Then
                                Select Case caa.WarehouseType
                                    Case 1, 2, 3, 4
                                        Mensaje(EeventViewerImages.Advertencia) = "El almacén ya se encuentra agregado"
                                        Exit Sub
                                End Select
                            End If
                        End If
                    End If
                    If listCMWareHouse.Where(Function(x) x.WarehouseType = caa.WarehouseType).Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("{0} {1} {2}", "Ya existe un almacén tipo ", caa.WarehouseTypeName, " asociado")
                        Exit Sub
                    End If
                End If
                listCMWareHouse.Add(caa)
            Next
            RefrescarRejillaWareHouse()

        End Using

    End Sub

#End Region

#End Region

#Region "Procesos"

    ''' <summary>
    '''
    ''' </summary>
    Private Sub AjustarHorasEntrega()
        If INDGleEveryTimeDeliver.EditValue IsNot Nothing Then
            Select Case INDGleEveryTimeDeliver.EditValue
                Case 1
                    INDTmeSecondDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 6, INDTmeFirstDeliveryTime.EditValue)
                    INDTmeThirdDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 6, INDTmeSecondDeliveryTime.EditValue)
                    INDTmeFourthDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 6, INDTmeThirdDeliveryTime.EditValue)
                Case 2
                    INDTmeSecondDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 8, INDTmeFirstDeliveryTime.EditValue)
                    INDTmeThirdDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 8, INDTmeSecondDeliveryTime.EditValue)
                Case 3
                    INDTmeSecondDeliveryTime.EditValue = DateAdd(DateInterval.Hour, 12, INDTmeFirstDeliveryTime.EditValue)
            End Select
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Async Sub LoadListCMCenterLineUnit()
        Dim listTemp As List(Of CMCenterAttention) = Nothing
        If _CMConfiguration.CMCenterAttention IsNot Nothing And _CMConfiguration.CMCenterAttention.Count > 0 Then
            listTemp = _CMConfiguration.CMCenterAttention.ToList()
        ElseIf ListCenterAttentionProductionLine IsNot Nothing AndAlso ListCenterAttentionProductionLine.Count > 0 Then
            listTemp = ListCenterAttentionProductionLine.ToList()
        End If

        If listTemp IsNot Nothing AndAlso listTemp.Count > 0 Then
            Dim _listCenterLine As New List(Of Tuple(Of String, Integer, Boolean))
            For Each ca In listTemp
                _listCenterLine.Add(New Tuple(Of String, Integer, Boolean)(ca.CodeCenterAttention, ca.IdProductionLine, ca.StateCA))
            Next
            Using model As New MCMConfig(Me.Tag)
                Dim resultado = Await model.ListCMCenterLineUnit(_CMConfiguration.Id, _listCenterLine)
                If resultado IsNot Nothing And resultado.StateResult Then
                    Me.ListCMCenterLineUnit = resultado.ObjectEmbbeded
                    RefrescarRejillaCenterLineUnit()
                Else
                    Me.ListCMCenterLineUnit = New List(Of SP_CMCenterLineUnit_Result)
                    Me.Mensaje(EeventViewerImages.MensajeError) = resultado.MessageResult.ToString
                    RefrescarRejillaCenterLineUnit()
                End If
            End Using

        End If
    End Sub

    Private Sub RefrescarRejilla()
        INDGcCenterAttention.DataSource = Me.ListCenterAttentionProductionLine
        INDGvCenterAttention.RefreshData()
        INDGcCenterAttention.RefreshDataSource()

    End Sub

    Private Sub RefrescarRejillaWareHouse()
        INDGcWareHouse.DataSource = Me.listCMWareHouse.ToList()
        INDGvWareHouse.RefreshData()
        INDGcWareHouse.RefreshDataSource()
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub RefrescarRejillaLine()
        If Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.CMMixingProducitonLine IsNot Nothing Then
            INDGcLineDetail.DataSource = Me._CMConfiguration.CMMixingProducitonLine.Where(Function(d) Not d.ChangeTracker.State = ObjectState.Deleted)
        Else
            INDGcLineDetail.DataSource = Nothing
        End If
        INDGvLineDetail.RefreshData()
        INDGcLineDetail.RefreshDataSource()
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub RefrescarRejillaCenterLineUnit()
        INDGcDeliveryTime.DataSource = Me.ListCMCenterLineUnit.Where(Function(clu) clu.StatusCL.GetValueOrDefault)
        INDGvDeliveryTime.RefreshData()
        INDGcDeliveryTime.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina el Centro de atencion y el Horario de Enfermeria. 
    ''' </summary>
    Private Sub DeleteDetail()
        _CMCenterAttention = DirectCast(INDGvCenterAttention.GetFocusedRow(), CMCenterAttention)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If _CMCenterAttention.Id > 0 Then
                If ListDeleteCenterAttentionProduction Is Nothing Then
                    ListDeleteCenterAttentionProduction = New List(Of CMCenterAttention)
                End If
                _CMCenterAttention.MarkAsDeleted()
                ListDeleteCenterAttentionProduction.Add(_CMCenterAttention)
            End If

            If Me.ListCMCenterLineUnit.Any(Function(clu) clu.CodeCenterAttention = _CMCenterAttention.CodeCenterAttention AndAlso clu.ProductionLineId = _CMCenterAttention.IdProductionLine) Then
                For Each o In Me.ListCMCenterLineUnit.Where(Function(clu) clu.CodeCenterAttention = _CMCenterAttention.CodeCenterAttention AndAlso clu.ProductionLineId = _CMCenterAttention.IdProductionLine).ToList
                    Me.ListCMCenterLineUnit.Remove(o)
                Next
                RefrescarRejillaCenterLineUnit()
            End If
            ListCenterAttentionProductionLine.Remove(_CMCenterAttention)
            RefrescarRejilla()
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub DeleteDetailWareHouse()
        _CMWareHouse = DirectCast(INDGvWareHouse.GetFocusedRow(), CMWarehouse)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If _CMWareHouse.Id > 0 Then
                If ListDeleteWareHouse Is Nothing Then
                    ListDeleteWareHouse = New List(Of CMWarehouse)
                End If
                _CMWareHouse.MarkAsDeleted()
                ListDeleteWareHouse.Add(_CMWareHouse)

            End If
            listCMWareHouse.Remove(_CMWareHouse)
            RefrescarRejillaWareHouse()
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub DeleteDetailLine()
        Dim pl = DirectCast(INDGvLineDetail.GetFocusedRow(), CMMixingProducitonLine)
        If pl IsNot Nothing Then
            If Me.ListCenterAttentionProductionLine Is Nothing OrElse Not Me.ListCenterAttentionProductionLine.Any(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine) Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If pl.Id > 0 Then
                        pl.MarkAsDeleted()
                        Me._CMConfiguration.CMMixingProducitonLine.Add(pl)
                    Else
                        Me._CMConfiguration.CMMixingProducitonLine.Remove(pl)
                    End If
                    RefrescarRejillaLine()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar, esta relacionada con centros de atención"
            End If
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub ChangeStateLine()
        Dim pl = DirectCast(INDGvLineDetail.GetFocusedRow(), CMMixingProducitonLine)
        If pl IsNot Nothing Then
            Dim listCenterAttention As New List(Of String)
            If (Not pl.StatePl) AndAlso Me.ListCenterAttentionProductionLine IsNot Nothing AndAlso
                Me.ListCenterAttentionProductionLine.Any(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine) Then

                For Each o In Me.ListCenterAttentionProductionLine.Where(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine)
                    If _presenter.GetCMCAPLByCenterAttention(o.IdMixingStation, o.CodeCenterAttention) > 0 Then
                        listCenterAttention.Add(o.CodeCenterAttention)
                    End If
                Next
                If Me.ListCenterAttentionProductionLine.Where(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine).Count =
                    listCenterAttention.Count Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar el estado, exite una relacion de los centros de atención de la línea de producción en otra Central de Mezclas"
                    Exit Sub
                End If

            End If
            If MessageIndigo.Show(ResourceManager.GetString(Me.GetType().Name & "_ChangeStateCAPLRecord", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                pl.StatePl = Not pl.StatePl
                pl.StatusName = IIf(pl.StatePl, "Activo", "Inactivo")
                If pl.Id > 0 Then
                    pl.MarkAsModified
                End If
                If Me.ListCenterAttentionProductionLine IsNot Nothing AndAlso Me.ListCenterAttentionProductionLine.Any(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine) Then
                    For Each o In Me.ListCenterAttentionProductionLine.Where(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine AndAlso Not listCenterAttention.Contains(capl.CodeCenterAttention))
                        o.StateCA = pl.StatePl
                        o.StatusName = IIf(o.StateCA, "Activo", "Inactivo")
                        If o.Id > 0 Then
                            o.MarkAsModified
                        End If
                    Next
                    For Each o In Me.ListCenterAttentionProductionLine.Where(Function(capl) capl.IdProductionLine = pl.Id_ProductionLine AndAlso listCenterAttention.Contains(capl.CodeCenterAttention))
                        o.StateCA = False
                        o.StatusName = "Inactivo"
                        If o.Id > 0 Then
                            o.MarkAsModified
                        End If
                    Next
                    Dim linq = From cl In Me.ListCenterAttentionProductionLine
                               Join clu In Me.ListCMCenterLineUnit On clu.CodeCenterAttention Equals cl.CodeCenterAttention And clu.ProductionLineId Equals cl.IdProductionLine
                               Where cl.StateCA <> clu.StatusCL
                               Select clu, cl.StateCA
                    If linq.Any Then
                        For Each o In linq
                            o.clu.StatusCL = o.StateCA
                        Next
                        RefrescarRejillaCenterLineUnit()
                    End If

                    Dim linq1 = From cl In Me.ListCMCenterLineUnit
                                Join clu In _CMConfiguration.CMCenterLineUnit On clu.CodeCenterAttention Equals cl.CodeCenterAttention And clu.ProductionLineId Equals cl.ProductionLineId _
                                    And clu.CodeFunctionalUnit Equals cl.CodeFunctionalUnit
                                Where clu.StatusCLU <> cl.StatusCL
                                Select clu, cl.StatusCL
                    If linq1.Any Then
                        For Each o In linq1
                            o.clu.StatusCLU = o.StatusCL
                            o.clu.MarkAsModified
                        Next
                    End If

                End If
            End If
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub ChangeStateRelacion()
        Dim pl = DirectCast(INDGvCenterAttention.GetFocusedRow(), CMCenterAttention)
        If pl IsNot Nothing Then
            If (Not pl.StateCA) AndAlso Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.CMMixingProducitonLine IsNot Nothing AndAlso
                Me._CMConfiguration.CMMixingProducitonLine.Any(Function(o) o.Id_ProductionLine = pl.IdProductionLine AndAlso Not o.ChangeTracker.State = ObjectState.Deleted) AndAlso
                Not Me._CMConfiguration.CMMixingProducitonLine.Where(Function(o) o.Id_ProductionLine = pl.IdProductionLine AndAlso Not o.ChangeTracker.State = ObjectState.Deleted).FirstOrDefault().StatePl Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar el estado, la línea de producción de este registro esta inactivo en la central de mezcla"
                Exit Sub
            End If

            If (Not pl.StateCA) AndAlso _presenter.GetCMCAPLByCenterAttention(pl.IdMixingStation, pl.CodeCenterAttention) > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Existe una relación del centro de atención con una línea de producción en otra Central de Mezclas"
                Exit Sub
            End If
            If MessageIndigo.Show(ResourceManager.GetString("ChangeStateRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                pl.StateCA = Not pl.StateCA
                pl.StatusName = IIf(pl.StateCA, "Activo", "Inactivo")
                If pl.Id > 0 Then
                    pl.MarkAsModified
                End If
                Dim linq = From clu In Me.ListCMCenterLineUnit Where clu.CodeCenterAttention = pl.CodeCenterAttention And clu.ProductionLineId = pl.IdProductionLine _
                           And clu.StatusCL <> pl.StateCA
                           Select clu
                If linq.Any Then
                    For Each clu In linq
                        clu.StatusCL = pl.StateCA
                    Next
                    RefrescarRejillaCenterLineUnit()
                End If

                Dim linq1 = From clu In _CMConfiguration.CMCenterLineUnit Where clu.CodeCenterAttention = pl.CodeCenterAttention AndAlso clu.ProductionLineId = pl.IdProductionLine _
                                                                          AndAlso clu.StatusCLU <> pl.StateCA
                            Select clu
                If linq1.Any Then
                    For Each o In linq1
                        o.StatusCLU = pl.StateCA
                        o.MarkAsModified
                    Next
                End If
            End If

        End If

    End Sub

    Private Sub ChangeStateWareHouse()
        Dim pl = DirectCast(INDGvWareHouse.GetFocusedRow(), CMWarehouse)
        If pl IsNot Nothing Then

            If MessageIndigo.Show(ResourceManager.GetString("ChangeStateRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                pl.StateWH = Not pl.StateWH
                pl.StatusName = IIf(pl.StateWH, "Activo", "Inactivo")
                If pl.Id > 0 Then
                    pl.MarkAsModified
                End If
                Dim linq = From clu In Me.listCMWareHouse Where clu.IdWarehouse = pl.IdWarehouse And clu.Id = pl.Id
                           Select clu
                'And clu.StateWH <> pl.StateWH
                If linq.Any Then
                    For Each clu In linq
                        clu.StateWH = pl.StateWH
                    Next
                    RefrescarRejillaWareHouse()
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Metodo para cargar los tipos de central de mezclas
    ''' </summary>
    Private Sub InitializeTuples()
        ListCMType = New List(Of Tuple(Of Integer, String))
        ListCMType.Add(New Tuple(Of Integer, String)(1, "CMP Propia"))
        ListCMType.Add(New Tuple(Of Integer, String)(2, "CMP Propia y Atención IPS Externas"))
        ListCMType.Add(New Tuple(Of Integer, String)(3, "CMP Atención IPS Externas"))
        INDsleCmType.Properties.DataSource = ListCMType.ToList
    End Sub

    ''' <summary>
    ''' Metodo que retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedrecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDbtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()
        With _CMConfiguration
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = CmName
            .Prefix = CmPrefix
            .MixingStationType = MSType
            .IdDirectorSp = IdDirectorSp
            .IdDirector = IdDirector

            Select Case Me.BarraBotones.StatusRecord
                Case eActionsStatusRecords.Active
                    .State = True
                Case eActionsStatusRecords.Inactive
                    .State = False
            End Select

            If ListCenterAttentionProductionLine IsNot Nothing AndAlso ListCenterAttentionProductionLine.Count > 0 Then
                For Each itemDetail As CMCenterAttention In ListCenterAttentionProductionLine
                    .CMCenterAttention.Add(itemDetail)
                Next
            End If

            If listCMWareHouse IsNot Nothing AndAlso listCMWareHouse.Count > 0 Then
                For Each itemDetail As CMWarehouse In listCMWareHouse
                    .CMWarehouse.Add(itemDetail)
                Next
            End If

            If ListDeleteCenterAttentionProduction IsNot Nothing AndAlso ListDeleteCenterAttentionProduction.Count > 0 Then
                For Each itemDeleteDetail As CMCenterAttention In ListDeleteCenterAttentionProduction
                    .CMCenterAttention.Add(itemDeleteDetail)
                Next
            End If

            If ListDeleteCMConfigurationSchedule IsNot Nothing AndAlso ListDeleteCMConfigurationSchedule.Count > 0 Then
                ListDeleteCMConfigurationSchedule.ForEach(Sub(item) .CMConfigurationSchedule.Add(item.MarkAsDeleted()))
            End If

            If ListCMExternalCareCenter IsNot Nothing AndAlso ListCMExternalCareCenter.Count > 0 Then
                ListCMExternalCareCenter.ForEach(Sub(item) .CMExternalCareCenter.Add(item))
            End If

            If ListDeleteCMExternalCareCenter IsNot Nothing AndAlso ListDeleteCMExternalCareCenter.Count > 0 Then
                ListDeleteCMExternalCareCenter.ForEach(Sub(item) .CMExternalCareCenter.Add(item.MarkAsDeleted()))
            End If

            If ListCMConfigurationUsers IsNot Nothing AndAlso ListCMConfigurationUsers.Count > 0 Then
                ListCMConfigurationUsers.ForEach(Sub(item) .CMConfigurationUsers.Add(item))
            End If

            If ListDeleteCMConfigurationUsers IsNot Nothing AndAlso ListDeleteCMConfigurationUsers.Count > 0 Then
                ListDeleteCMConfigurationUsers.ForEach(Sub(item) .CMConfigurationUsers.Add(item))
            End If

            If ListCMCenterLineUnit IsNot Nothing AndAlso ListCMCenterLineUnit.Count > 0 Then
                Dim linq = From clu In .CMCenterLineUnit
                           Group Join o In ListCMCenterLineUnit On o.CMCenterLineUnitId Equals clu.Id Into Group
                           From o In Group.DefaultIfEmpty()
                           Where o Is Nothing
                           Select clu

                If linq.Any Then
                    While linq.Any()
                        linq(0).MarkAsDeleted
                    End While
                End If

                If ListCMCenterLineUnit.Any(Function(clu) clu.CMCenterLineUnitId = 0 AndAlso clu.EveryTimeDeliverId IsNot Nothing) Then
                    Dim _CMCenterLineUnit As CMCenterLineUnit = Nothing
                    For Each o In ListCMCenterLineUnit.Where(Function(clu) clu.CMCenterLineUnitId = 0 AndAlso clu.EveryTimeDeliverId IsNot Nothing)
                        _CMCenterLineUnit = New CMCenterLineUnit
                        With _CMCenterLineUnit
                            .CodeCenterAttention = o.CodeCenterAttention
                            .CodeFunctionalUnit = o.CodeFunctionalUnit
                            .EveryTimeDeliverId = o.EveryTimeDeliverId
                            .FirstDeliveryTime = o.FirstDeliveryTime
                            .SecondDeliveryTime = o.SecondDeliveryTime
                            .ThirdDeliveryTime = o.ThirdDeliveryTime
                            .FourthDeliveryTime = o.FourthDeliveryTime
                            .MixingStationId = _CMConfiguration.Id
                            .ProductionLineId = o.ProductionLineId
                            .StatusCLU = o.StatusCLU
                        End With
                        .CMCenterLineUnit.Add(_CMCenterLineUnit)
                    Next
                End If

                If .Id > 0 Then
                    .MarkAsModified()
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()

        Me.SuspendLayout()
        Me.ActiveControl = Nothing
        Me.AutoScroll = False
        INDlycBase.BeginUpdate()

        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()

        _usersXpo = Nothing
        Me._CMConfiguration = Nothing
        CMConfigurationSchedule = Nothing
        ListDeleteCMConfigurationSchedule = Nothing
        Code = String.Empty
        CmName = String.Empty
        CmPrefix = String.Empty
        MSType = Nothing
        INDsleCmType.Properties.NullText = String.Empty
        Status = True
        ListCenterAttentionProductionLine = New Domain.Entities.TrackableCollection(Of CMCenterAttention)
        ListDeleteCenterAttentionProduction = Nothing
        INDGcCenterAttention.DataSource = Nothing
        IdProductionLine = Nothing
        INDSleProductionLine.Properties.NullText = String.Empty
        INDGcLineDetail.DataSource = Nothing
        INDGcDeliveryTime.DataSource = Nothing
        INDGcWareHouse.DataSource = Nothing
        INDGvWareHouse.RefreshData()
        INDGcWareHouse.RefreshDataSource()
        listCMWareHouse = New Domain.Entities.TrackableCollection(Of CMWarehouse)
        IndigoGridControl1.RefreshGrid(INDGcCenterAttention)
        IndigoGridControl1.RefreshGrid(INDGcLineDetail)
        IndigoGridControl1.RefreshGrid(INDGcDeliveryTime)
        BarraBotones.ReassignOperatingUnit()

        ListCMExternalCareCenter = Nothing
        ListDeleteCMExternalCareCenter = Nothing
        CMExternalCareCenter = Nothing
        INDGcExternalCenter.DataSource = Nothing

        INDsleUsers.EditValue = Nothing
        INDgcUsers.DataSource = Nothing
        ListCMConfigurationUsers = Nothing
        ListDeleteCMConfigurationUsers = Nothing
        INDGcWorkingAreas.DataSource = Nothing
        INDsleDirectPr.Properties.NullText = String.Empty
        INDsleDirectPr.Properties.DataSource = Nothing
        INDsleDirectSp.Properties.NullText = String.Empty
        INDsleDirectSp.Properties.DataSource = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        ActionsOnControls = False

        Await DeleteBlockedrecord()
        Me._CMConfiguration = New CMConfiguration
        Me._CMConfiguration.CMCenterAttention = New Domain.Entities.TrackableCollection(Of CMCenterAttention)
        Me._CMConfiguration.CMMixingProducitonLine = New Domain.Entities.TrackableCollection(Of CMMixingProducitonLine)
        Me._CMConfiguration.CMCenterLineUnit = New Domain.Entities.TrackableCollection(Of CMCenterLineUnit)

        INDlycBase.EndUpdate()
        Me.AutoScroll = True
        Me.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="_EveryTimeDeliverId"></param>
    Private Sub VisualizarControles(ByVal _EveryTimeDeliverId As Byte?)
        If _EveryTimeDeliverId Is Nothing Then
            INDLciFirstDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSecondDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciThirdDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciFourthDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Select Case _EveryTimeDeliverId
                Case 1
                    INDLciFirstDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciSecondDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciThirdDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciFourthDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case 2
                    INDLciFirstDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciSecondDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciThirdDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciFourthDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTmeFourthDeliveryTime.EditValue = Nothing
                Case 3
                    INDLciFirstDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciSecondDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciThirdDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciFourthDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTmeThirdDeliveryTime.EditValue = Nothing
                    INDTmeFourthDeliveryTime.EditValue = Nothing
                Case 4
                    INDLciFirstDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciSecondDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciThirdDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciFourthDeliveryTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTmeSecondDeliveryTime.EditValue = Nothing
                    INDTmeThirdDeliveryTime.EditValue = Nothing
                    INDTmeFourthDeliveryTime.EditValue = Nothing
            End Select
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim _mensaje As String = String.Empty
        If INDGleEveryTimeDeliver.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione 'Entrega cada'"
            Exit Sub
        End If
        Select Case INDGleEveryTimeDeliver.EditValue
            Case 1
                If INDTmeFirstDeliveryTime.EditValue Is Nothing Then
                    _mensaje = "Primera Entrega"
                End If
                If INDTmeSecondDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Segunda Entrega", _mensaje, Environment.NewLine)
                End If
                If INDTmeThirdDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Tercera Entrega", _mensaje, Environment.NewLine)
                End If
                If INDTmeFourthDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Cuarta Entrega", _mensaje, Environment.NewLine)
                End If
            Case 2
                If INDTmeFirstDeliveryTime.EditValue Is Nothing Then
                    _mensaje = "Primera Entrega"
                End If
                If INDTmeSecondDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Segunda Entrega", _mensaje, Environment.NewLine)
                End If
                If INDTmeThirdDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Tercera Entrega", _mensaje, Environment.NewLine)
                End If
            Case 3
                If INDTmeFirstDeliveryTime.EditValue Is Nothing Then
                    _mensaje = "Primera Entrega"
                End If
                If INDTmeSecondDeliveryTime.EditValue Is Nothing Then
                    _mensaje = String.Format("{0}{1}Segunda Entrega", _mensaje, Environment.NewLine)
                End If
            Case 4
                If INDTmeFirstDeliveryTime.EditValue Is Nothing Then
                    _mensaje = "Primera Entrega"
                End If
        End Select
        If Not String.IsNullOrEmpty(_mensaje) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Ingrese un valor para los siguientes campos:{0}{1}", Environment.NewLine, _mensaje)
            Exit Sub
        End If
        Dim clu = DirectCast(INDGvDeliveryTime.GetFocusedRow(), SP_CMCenterLineUnit_Result)
        If clu IsNot Nothing Then
            clu.EveryTimeDeliverId = INDGleEveryTimeDeliver.EditValue
            clu.FirstDeliveryTime = INDTmeFirstDeliveryTime.EditValue
            clu.SecondDeliveryTime = INDTmeSecondDeliveryTime.EditValue
            clu.ThirdDeliveryTime = INDTmeThirdDeliveryTime.EditValue
            clu.FourthDeliveryTime = INDTmeFourthDeliveryTime.EditValue
            Dim _name As String = String.Empty
            Select Case clu.EveryTimeDeliverId
                Case 1
                    _name = "6 horas"
                Case 2
                    _name = "8 horas"
                Case 3
                    _name = "12 horas"
                Case 4
                    _name = "24 horas"
            End Select
            clu.EveryTimeDeliverName = _name
            Dim _clu As CMCenterLineUnit = Nothing
            _clu = _CMConfiguration.CMCenterLineUnit.FirstOrDefault(Function(d) d.CodeCenterAttention = clu.CodeCenterAttention AndAlso d.ProductionLineId = clu.ProductionLineId _
                                                                        AndAlso d.CodeFunctionalUnit = clu.CodeFunctionalUnit)
            If _clu IsNot Nothing Then
                _clu.EveryTimeDeliverId = clu.EveryTimeDeliverId
                _clu.FirstDeliveryTime = clu.FirstDeliveryTime
                _clu.SecondDeliveryTime = clu.SecondDeliveryTime
                _clu.ThirdDeliveryTime = clu.ThirdDeliveryTime
                _clu.FourthDeliveryTime = clu.FourthDeliveryTime
                _clu.MarkAsModified
            End If
        End If
        INDPccDeliveryTime.OwnerEdit.ClosePopup()
    End Sub

    Private Sub INDLcgWorkingAreas_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles INDLcgWorkingAreas.CustomButtonClick
        If INDLcgWorkingAreas.CustomHeaderButtons(0).Properties.Enabled Then
            OpenFormWorkingAreas(Nothing)
        End If
    End Sub

    Private Sub OpenFormWorkingAreas(workingArea As WorkingArea)
        Using frm As New FrmPopupWorkingArea()
            frm.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            frm.CMConfigurationId = _CMConfiguration.Id
            frm.WorkingArea = workingArea
            frm.AllWorkingAreas = _CMConfiguration.WorkingArea.ToList()
            AddHandler frm.OnWorkingAreaAdded, Sub(sender As Object, wa As WorkingArea, edit As Boolean)
                                                   If edit Then
                                                       If workingArea.Id > 0 Then
                                                           workingArea.MarkAsModified
                                                       End If
                                                       With workingArea
                                                           .Code = wa.Code
                                                           .Description = wa.Description
                                                           .Observation = wa.Observation
                                                           .Status = wa.Status
                                                       End With
                                                   Else
                                                       _CMConfiguration.WorkingArea.Add(wa)
                                                       INDGcWorkingAreas.DataSource = _CMConfiguration.WorkingArea
                                                   End If
                                                   INDGcWorkingAreas.RefreshDataSource()
                                                   CType(sender, FrmPopupWorkingArea).Close()
                                               End Sub
            Dim t As New FrmTransparent(frm, False)
            t.ShowDialog(Me)
        End Using
    End Sub

    Private Sub IndigoGridViewWorkingArea_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewWorkingArea.Click_ButtonAction, IndigoGridViewWorkingArea.ContexMenuActions
        If sender.Tag = "Edit" Then
            OpenFormWorkingAreas(INDGvWorkingAreas.GetFocusedObject(Of WorkingArea))
        ElseIf sender.Tag = "Remove" Then
            RemoveWorkingArea(INDGvWorkingAreas.GetFocusedObject(Of WorkingArea))
        End If
    End Sub

    Private Sub RemoveWorkingArea(workingArea As WorkingArea)
        If workingArea Is Nothing Then Return

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If workingArea.Id > 0 Then
                workingArea.MarkAsDeleted()
            Else
                _CMConfiguration.WorkingArea.Remove(workingArea)
            End If
            INDGcWorkingAreas.DataSource = _CMConfiguration.WorkingArea
            INDGcWorkingAreas.RefreshDataSource()
        End If
    End Sub

    Private Sub INDGvWorkingAreas_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWorkingAreas.CustomUnboundColumnData
        If e.IsGetData Then
            Dim wa = CType(e.Row, WorkingArea)
            If e.Column.Name = INDColState.Name Then
                e.Value = IIf(wa.Status, "Activo", "Inactivo")
            End If
        End If
    End Sub

    Private Sub INDLcgWareHouse_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles INDLcgWareHouse.CustomButtonClick
        If INDLcgWareHouse.CustomHeaderButtons(0).Properties.Enabled Then
            OpenPopupWarehouse()
        End If
    End Sub

    Private Sub INDLcgExternalCenter_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles INDLcgExternalCenter.CustomButtonClick
        If INDLcgExternalCenter.CustomHeaderButtons(0).Properties.Enabled Then
            CMExternalCareCenter = Nothing
            OpenFormCMExternalCareCenter(False)
        End If
    End Sub

    Private Sub INDLcgCenter_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles INDLcgCenter.CustomButtonClick
        If INDLcgCenter.CustomHeaderButtons(0).Properties.Enabled Then
            OpenCenterAttention()
        End If
    End Sub

    Private Sub OpenCenterAttention()
        Using formulario As New FrmPopupAttentionCenterDetail
            Me.Cursor = ChangeCursorIndigo()
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario._listCMCenterAttentionDetail = ListCenterAttentionProductionLine
            If Me._CMConfiguration IsNot Nothing Then
                formulario.MixingStationId = Me._CMConfiguration.Id
            End If
            If Me._CMConfiguration IsNot Nothing AndAlso Me._CMConfiguration.CMMixingProducitonLine IsNot Nothing _
                AndAlso Me._CMConfiguration.CMMixingProducitonLine.Any(Function(pl) pl.StatePl AndAlso (Not pl.ChangeTracker.State = ObjectState.Deleted)) Then
                formulario.ProductionLinesIds = String.Join(",", Me._CMConfiguration.CMMixingProducitonLine.Where(Function(pl) pl.StatePl AndAlso
                                                    (Not pl.ChangeTracker.State = ObjectState.Deleted)).Select(Function(pl) pl.Id_ProductionLine))
            End If
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            For Each caa In formulario._listCMCenterAttentionDetail.Where(Function(ca) ca.Agregado)
                caa.Agregado = False
                ListCenterAttentionProductionLine.Add(caa)
            Next
            RefrescarRejilla()
            LoadListCMCenterLineUnit()
        End Using
    End Sub

    Private Sub IndigoGridControl1_ClickAdd(sender As Object, e As EventArgs) Handles IndigoGridControl1.ClickAdd
        Dim grid = CType(sender, GridControl)

        Select Case grid.Name
            Case INDGcCenterAttention.Name
                OpenCenterAttention()
            Case INDGcExternalCenter.Name
                CMExternalCareCenter = Nothing
                OpenFormCMExternalCareCenter(False)
        End Select
    End Sub

    Private Sub INDSbAddAttentionCenter_Click(sender As Object, e As EventArgs) Handles INDSbAddAttentionCenter.Click
        OpenCenterAttention()
    End Sub

    Private Sub INDSbAddExternalAttentionCenter_Click(sender As Object, e As EventArgs) Handles INDSbAddExternalAttentionCenter.Click
        CMExternalCareCenter = Nothing
        OpenFormCMExternalCareCenter(False)
    End Sub

    Private Sub INDSbAddWarehouse_Click(sender As Object, e As EventArgs) Handles INDSbAddWarehouse.Click
        OpenPopupWarehouse()
    End Sub

    Private Sub INDSbAddWorkingAreas_Click(sender As Object, e As EventArgs) Handles INDSbAddWorkingAreas.Click
        OpenFormWorkingAreas(Nothing)
    End Sub


#End Region

End Class