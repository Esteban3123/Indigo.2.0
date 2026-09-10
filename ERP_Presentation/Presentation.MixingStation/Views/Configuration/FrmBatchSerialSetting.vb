'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Diego A. Roldán Lozano
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

Public Class FrmBatchSerialSetting
    Implements IBatchSerialSetting

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Private _batchSerialSetting As BatchSerialSetting

    ''' <summary>
    ''' Presenter
    ''' </summary>
    Private _presenter As PBatchSerialSetting

    ''' <summary>
    ''' block record
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' datasource para el tipo de formato de fecha
    ''' </summary>
    Private _dataSourceDateType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' Activa el codigo de lote de central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivateMixingStation As Boolean Implements IBatchSerialSetting.ActivateMixingStation
        Get
            Return INDRgActivateCodeMS.EditValue
        End Get
        Set(value As Boolean)
            INDRgActivateCodeMS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece que tipo de codigo Numerico-Alfabetico
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeMSType As Boolean? Implements IBatchSerialSetting.CodeMSType
        Get
            Return INDRgCodeTypeMS.EditValue
        End Get
        Set(value As Boolean?)
            INDRgCodeTypeMS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Activa el codigo de lote de tipo de dosis unitaria
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivateUnitDoseType As Boolean Implements IBatchSerialSetting.ActivateUnitDoseType
        Get
            Return INDRgActivateCodeUDT.EditValue
        End Get
        Set(value As Boolean)
            INDRgActivateCodeUDT.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de empaque
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeUDTType As Boolean? Implements IBatchSerialSetting.CodeUDTType
        Get
            Return INDRgCodeTypeUDT.EditValue
        End Get
        Set(value As Boolean?)
            INDRgCodeTypeUDT.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' activa la fecha para la creacion del lote
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivateDateFormatType As Boolean Implements IBatchSerialSetting.ActivateDateFormatType
        Get
            Return INDRgActivateDate.EditValue
        End Get
        Set(value As Boolean)
            INDRgActivateDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el tipo de formato de fecha a usar en el lote
    ''' </summary>
    ''' <returns></returns>
    Public Property DateFormatType As Byte? Implements IBatchSerialSetting.DateFormatType
        Get
            Return IIf(INDGleTypeDate.EditValue Is Nothing, Nothing, CType(INDGleTypeDate.EditValue, Byte))
        End Get
        Set(value As Byte?)
            INDGleTypeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el id de la secuencia numerica a usar
    ''' </summary>
    ''' <returns></returns>
    Public Property SequenseId As Integer? Implements IBatchSerialSetting.SequenseId
        Get
            Return INDslePatternSequence.EditValue
        End Get
        Set(value As Integer?)
            INDslePatternSequence.EditValue = value
        End Set
    End Property

#End Region

#Region "DataSource"

    Public ReadOnly Property DatasourceDateType As List(Of Tuple(Of Integer, String))
        Get
            If _dataSourceDateType Is Nothing Then
                _dataSourceDateType = New List(Of Tuple(Of Integer, String))
                _dataSourceDateType.Add(New Tuple(Of Integer, String)(1, "ddmmaa"))
                _dataSourceDateType.Add(New Tuple(Of Integer, String)(2, "aammdd"))
                _dataSourceDateType.Add(New Tuple(Of Integer, String)(3, "Número de día"))
            End If
            Return _dataSourceDateType
        End Get
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmBatchSerialSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await InitForm()
    End Sub

    ''' <summary>
    ''' Evento cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBatchSerialSetting_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' query popup de la secuencia numerica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePatternSequence_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslePatternSequence.QueryPopUp
        If INDslePatternSequence.Properties.DataSource Is Nothing Then
            Using Model As New MBatchSerialSetting(Tag)
                INDslePatternSequence.Properties.DataSource = Model.ListSequencePatterns()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento para bloquear control cuando cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgActivateCodeMS_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgActivateCodeMS.EditValueChanged
        INDRgCodeTypeMS.Enabled = ActivateMixingStation
        If Not ActivateMixingStation Then
            CodeMSType = Nothing
        End If
    End Sub
    ''' <summary>
    ''' evento para bloquear control cuando cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgActivateCodeUDT_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgActivateCodeUDT.EditValueChanged
        INDRgCodeTypeUDT.Enabled = ActivateUnitDoseType
        If Not ActivateUnitDoseType Then
            CodeUDTType = Nothing
        End If
    End Sub

    ''' <summary>
    ''' evento para bloquear control cuando cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgActivateDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgActivateDate.EditValueChanged
        INDGleTypeDate.Enabled = ActivateDateFormatType
        If Not ActivateDateFormatType Then
            DateFormatType = Nothing
        End If
    End Sub

#End Region

#Region "Functions"
    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord() Implements IBatchSerialSetting.DeleteBlockedRecord
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IBatchSerialSetting.CleanControls
        INDLcRoot.BeginUpdate()
        ActivateMixingStation = False
        CodeMSType = Nothing
        ActivateUnitDoseType = False
        CodeUDTType = Nothing
        ActivateDateFormatType = False
        DateFormatType = Nothing
        SequenseId = Nothing
        Me._batchSerialSetting = Nothing
        INDslePatternSequence.Properties.DataSource = Nothing
        _dataSourceDateType = Nothing
        INDslePatternSequence.Properties.NullText = Nothing
        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Estados iniciales del formulario
    ''' </summary>
    Private Async Function InitForm() As Task
        CleanControls()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PBatchSerialSetting(Me)

        DataSourceGridView()
        Await LoadControls()
    End Function

    ''' <summary>
    ''' carga las centrales de mezclas y los tipos de dosis unitarias
    ''' </summary>
    Private Sub DataSourceGridView()
        Task.Factory.StartNew(Sub()
                                  Me.SafeInvoke(Sub()
                                                    INDGcMixingStation.DataSource = _presenter.ListMixingStations()
                                                    INDGcMixingStation.RefreshDataSource()
                                                    INDGcUnitDoseType.DataSource = _presenter.ListUnitDoseType()
                                                    INDGcUnitDoseType.RefreshDataSource()
                                                    INDGleTypeDate.Properties.DataSource = DatasourceDateType
                                                End Sub)
                              End Sub)

    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Validatecontrols() Then Return
        AssigningValues()

        Try
            Using model As New MBatchSerialSetting(Tag)
                AsyncLoader(True)
                Dim result = Await model.SaveBatchSerialSetting(Me._batchSerialSetting)
                AsyncLoader(False)
                ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                If result.StateResult Then
                    Await LoadControls()
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With _batchSerialSetting
            .ActivateMixingStation = ActivateMixingStation
            .CodeMSType = CodeMSType
            .CodeUDTType = CodeUDTType
            .ActivateUnitDoseType = ActivateUnitDoseType
            .DateFormatType = DateFormatType
            .SequenseId = SequenseId
        End With
    End Sub

    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Formulario de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' funcion para validar los controles cuando se mandan a guardar o actualizar
    ''' </summary>
    ''' <returns></returns>
    Public Function Validatecontrols() As Boolean
        Dim Errors = New Text.StringBuilder
        If ActivateMixingStation AndAlso CodeMSType Is Nothing Then
            Errors.AppendLine("Si activa el Codigo por central de mezclas debe escoger el tipo de codigo")
        End If
        If ActivateUnitDoseType AndAlso CodeUDTType Is Nothing Then
            Errors.AppendLine("Si activa el código para tipo de dosis unitaria, debe escoger el tipo de código")
        End If
        If ActivateDateFormatType AndAlso (DateFormatType Is Nothing OrElse DateFormatType = 0) Then
            Errors.AppendLine("Si activa el Codigo por fecha debe escoger el tipo de formato de fecha")
        End If
        If SequenseId Is Nothing Then
            Errors.AppendLine("El patrón sequencial es obligatorio")
        End If
        If Errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = Errors.ToString()
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Carga los controles
    ''' </summary>
    Public Async Function LoadControls() As Task Implements IBatchSerialSetting.LoadControls
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Try
            Using model As New MBatchSerialSetting(Tag)
                AsyncLoader(True)
                Dim result = Await model.BatchSerialSettings()
                AsyncLoader(False)
                INDLcRoot.BeginUpdate()

                If result IsNot Nothing AndAlso result.StateResult AndAlso result.ObjectEmbbeded?.Id > 0 Then
                    Me._batchSerialSetting = result.ObjectEmbbeded
                    Using modelRecord As New MBlockRecordAndSequenceMixingStation(Tag)
                        _record = Await modelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_batchSerialSetting.Id))

                        With _batchSerialSetting
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            ActivateMixingStation = .ActivateMixingStation
                            CodeMSType = .CodeMSType
                            ActivateUnitDoseType = .ActivateUnitDoseType
                            CodeUDTType = .CodeUDTType
                            ActivateDateFormatType = IIf(.DateFormatType IsNot Nothing, True, False)
                            DateFormatType = .DateFormatType
                            SequenseId = .SequenseId
                            INDslePatternSequence.Properties.NullText = .PatternName
                        End With
                        If _record.Id = 0 Then
                            _record = (Await modelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {
                                        .BlockDate = Date.Now,
                                        .ChangeTracker = New ObjectChangeTracker With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName,
                                        .IdForm = Me.Tag,
                                        .CodUser = Me.indigo.UserIndigo,
                                        .IdRecord = _batchSerialSetting.Id
                                    })
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    Deshacer()
                    _batchSerialSetting = New BatchSerialSetting()
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
                INDLcRoot.EndUpdate()
            End Using
        Catch ex As Exception
            INDLcRoot.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' LogicaBotonActualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "Bar Button"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Tag)
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
    End Sub
#End Region
End Class