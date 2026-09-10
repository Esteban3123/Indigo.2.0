'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 19-01-2015
'
' Last Modified By : Jhossept K. Garay Rodriguez
' Last Modified On : 04-08-2016
' Description      : Refactoring seleccion multimple rejilla, y opciones para devolver todo o devolver nada
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Inventory.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports System.Text
Imports Presentation.Glosas
Imports Presentation.Maintenance

#End Region

Public Class FrmReturnReferrals
    Implements IRemissionDevolution, ICustomizableForm


#Region "TUPLES"
    Dim _listDevolutionType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListDevolutionType As List(Of Tuple(Of Integer, String))
        Get
            If _listDevolutionType Is Nothing Then
                _listDevolutionType = New List(Of Tuple(Of Integer, String))
                _listDevolutionType.Add(New Tuple(Of Integer, String)(1, "Entrada"))
                _listDevolutionType.Add(New Tuple(Of Integer, String)(2, "Salida"))
                _listDevolutionType.Add(New Tuple(Of Integer, String)(3, "Inventario en Consignación"))
            End If
            Return _listDevolutionType
        End Get
    End Property
#End Region

#Region "GLOBLAS"
    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory
    ''' <summary>
    ''' presenter de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Private presenter As PRemissionDevolution
    ''' <summary>
    ''' entidad de devolucion de remision
    ''' </summary>
    ''' <remarks></remarks>
    Private remissionDevolution As RemissionDevolution
    ''' <summary>
    ''' listado del detalle de las remisiones de entrdad
    ''' </summary>
    ''' <remarks></remarks>
    Private listRemissionEntranceDetailBatchSerial As List(Of RemissionEntranceDetailBatchSerial)
    ''' <summary>
    ''' listado del detalle de las remisiones de salida
    ''' </summary>
    ''' <remarks></remarks>
    Private listRemissionOutputDetailPhysical As List(Of RemissionOutputDetailPhysical)
    ''' <summary>
    ''' listado del detalle de las remisiones de inventario en consignación
    ''' </summary>
    ''' <remarks></remarks>
    Private listConsignmentInventoryRemissionDetailBatchSerial As List(Of ConsignmentInventoryRemissionDetailBatchSerial)
    ''' <summary>
    ''' bandera para saber que se esta cargando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private flagLoadControls As Boolean
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
    ''' <summary>
    ''' Controlar si confirmar procede de guardar o actualizar
    ''' </summary>
    Private _action As Integer
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRemissionDevolution.ActionsOnControls
        Set(value As Boolean)
            INDLcRemissionDevolution.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDBteCode.Enabled = Not value
            INDGleRemissionType.Enabled = value
            INDDteDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleWarehouse.Enabled = value
            INDMeDetail.Enabled = value
            INDSleSupplier.Enabled = value
            INDDteRemissionDate.Enabled = value
            INDDteRemissionDate.Enabled = value
            INDSleWarehouseRemission.Enabled = value
            INDSleWarehouseRemission.Enabled = value
            INDGcProduct.Enabled = value
            INDGcProduct.Enabled = value
            INDLcRemissionDevolution.EndUpdate()
            If value Then
                INDGleRemissionType.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Public Property Code As String Implements IRemissionDevolution.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property RemissionDate As Date Implements IRemissionDevolution.RemissionDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' descripcion
    ''' </summary>
    Public Property Description As String Implements IRemissionDevolution.Description
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de la devolucion
    ''' </summary>
    Public Property DevolutionType As Integer? Implements IRemissionDevolution.DevolutionType
        Get
            Return INDGleRemissionType.EditValue
        End Get
        Set(value As Integer?)
            INDGleRemissionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRemissionDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IRemissionDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' id de la remision de entrada
    ''' </summary>
    Public Property RemissionEntranceId As Integer? Implements IRemissionDevolution.RemissionEntranceId
        Get
            Return INDSleRemissionNumberInput.EditValue
        End Get
        Set(value As Integer?)
            INDSleRemissionNumberInput.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id de la remision de salida
    ''' </summary>
    Public Property RemissionOutputId As Integer? Implements IRemissionDevolution.RemissionOutputId
        Get
            Return INDSleRemissionNumberOutput.EditValue
        End Get
        Set(value As Integer?)
            INDSleRemissionNumberOutput.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id de la remision de inventario en consignación
    ''' </summary>
    Public Property ConsignmentInventoryRemissionId As Integer? Implements IRemissionDevolution.ConsignmentInventoryRemissionId
        Get
            Return INDsleRemissionNumberConsignmentInventory.EditValue
        End Get
        Set(value As Integer?)
            INDsleRemissionNumberConsignmentInventory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseId As Integer? Implements IRemissionDevolution.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As InventorySequence Implements IRemissionDevolution.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Property RemissionEntranceXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleRemissionNumberInput.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRemissionNumberInput.Properties.DataSource = value
        End Set
    End Property

    Property RemissionOutputXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleRemissionNumberOutput.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRemissionNumberOutput.Properties.DataSource = value
        End Set
    End Property

    Property ConsignmentInventoryRemissionXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleRemissionNumberConsignmentInventory.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRemissionNumberConsignmentInventory.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de causas de devolución para el repositorio
    ''' </summary>
    Public Property DevolutionCauseXpo As XPInstantFeedbackSource Implements IRemissionDevolution.DevolutionCauseXpo
        Get
            Return CType(INDRptSleDevolutionCause.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRptSleDevolutionCause.DataSource = value
        End Set
    End Property

#End Region

#Region "CRUD"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If remissionDevolution IsNot Nothing AndAlso remissionDevolution.Status < 3 Then

            If ValidateControls() = True Then
                Dim errors As New StringBuilder
                If DevolutionType IsNot Nothing Then
                    If DevolutionType = 1 Then
                        Dim listDetailTmp = listRemissionEntranceDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                        If listDetailTmp.Count = 0 Then
                            errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                        End If
                    ElseIf DevolutionType = 2 Then
                        Dim listDetailTmp = listRemissionOutputDetailPhysical.FindAll(Function(x) x.QuantityDeliver > 0)
                        If listDetailTmp.Count = 0 Then
                            errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                        End If
                    Else
                        Dim listDetailTmp = listConsignmentInventoryRemissionDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                        If listDetailTmp.Count = 0 Then
                            errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                        End If
                    End If
                End If
                If INDSleWarehouseRemission.EditValue <> WarehouseId Then
                    errors.AppendLine("El almacen de la devolución no corresponde con el almacén de la remisión.")
                End If
                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If

        Try
            Using model As New MRemissionDevolution(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveRemissionDevolution(remissionDevolution, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If remissionDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If remissionDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    remissionDevolution = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, remissionDevolution.Id, 0, remissionDevolution.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, remissionDevolution.Id, 0, remissionDevolution.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, remissionDevolution.Id, 0, remissionDevolution.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If remissionDevolution.Id > 0 Then
                        remissionDevolution = Await model.GetRemissionDevolutionByCode(remissionDevolution.Code)
                    Else
                        remissionDevolution = New RemissionDevolution
                    End If

                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewRemissionDevolution()
        End If
    End Sub

    Private Async Sub SaveOrUpdateAndConfirm(actions As Integer, Optional controlCost As Boolean = True)
        If ValidateControls() = True Then
            Dim errors As New StringBuilder
            If DevolutionType IsNot Nothing Then
                If DevolutionType = 1 Then
                    Dim listDetailTmp = listRemissionEntranceDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                    If listDetailTmp.Count = 0 Then
                        errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                    End If
                ElseIf DevolutionType = 2 Then
                    Dim listDetailTmp = listRemissionOutputDetailPhysical.FindAll(Function(x) x.QuantityDeliver > 0)
                    If listDetailTmp.Count = 0 Then
                        errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                    End If
                Else
                    Dim listDetailTmp = listConsignmentInventoryRemissionDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                    If listDetailTmp.Count = 0 Then
                        errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
                    End If
                End If
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        Else

            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MRemissionDevolution(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmRemissionDevolution(remissionDevolution, _idCurrentSequence, actions, Me._sequence, controlCost)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    remissionDevolution = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, remissionDevolution.Id, 0, remissionDevolution.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    remissionDevolution = result.ObjectEmbbeded
                    If (result.ObjectEmbbeded IsNot Nothing And result.ObjectEmbbeded.Id > 0) Then
                        Code = result.ObjectEmbbeded.Code
                        remissionDevolution = Await model.GetRemissionDevolutionByCode(Code)

                        INDGcProduct.DataSource = Nothing
                        If remissionDevolution.DevolutionType = 1 Then
                            listRemissionEntranceDetailBatchSerial = Await model.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(remissionDevolution.RemissionEntranceId)
                            listRemissionEntranceDetailBatchSerial.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                            For Each item In remissionDevolution.RemissionDevolutionDetail
                                Dim entranceDetail = listRemissionEntranceDetailBatchSerial.Find(Function(x) x.Id = item.RemissionEntranceDetailBatchSerialId)
                                If entranceDetail IsNot Nothing Then
                                    entranceDetail.QuantityDeliver = item.Quantity
                                End If
                            Next
                            INDGcProduct.DataSource = listRemissionEntranceDetailBatchSerial
                        ElseIf remissionDevolution.DevolutionType = 2 Then
                            listRemissionOutputDetailPhysical = Await model.ListRemissionOutputDetailPhysicalByRemissionOutputId(remissionDevolution.RemissionOutputId)
                            listRemissionOutputDetailPhysical.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                            For Each item In remissionDevolution.RemissionDevolutionDetail
                                Dim outputDetail = listRemissionOutputDetailPhysical.Find(Function(x) x.Id = item.RemissionOutputDetailPhysicalId)
                                If outputDetail IsNot Nothing Then
                                    outputDetail.QuantityDeliver = item.Quantity
                                End If
                            Next
                            INDGcProduct.DataSource = listRemissionOutputDetailPhysical
                        ElseIf remissionDevolution.DevolutionType = 3 Then
                            listConsignmentInventoryRemissionDetailBatchSerial = Await model.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(remissionDevolution.ConsignmentInventoryRemissionId)
                            listConsignmentInventoryRemissionDetailBatchSerial.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                            For Each item In remissionDevolution.RemissionDevolutionDetail
                                Dim entranceDetail = listConsignmentInventoryRemissionDetailBatchSerial.Find(Function(x) x.Id = item.ConsignmentInventoryRemissionDetailBatchSerialId)
                                If entranceDetail IsNot Nothing Then
                                    entranceDetail.QuantityDeliver = item.Quantity
                                End If
                            Next
                            INDGcProduct.DataSource = listConsignmentInventoryRemissionDetailBatchSerial
                        End If
                    End If

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, remissionDevolution.Id, 0, remissionDevolution.Id)
                    AsyncLoader(False)

                    If result.Message IsNot Nothing AndAlso result.Message <> "" Then
                        If result.Message.Contains("porcentaje de variación") Then
                            _action = actions
                            Using formulario As New FrmNotificationItemDetailConfirm
                                AddHandler formulario.AcceptMessage, AddressOf FrmNotificationItemDetailConfirm_Accept
                                formulario.TxtMessage.Text = result.Message & vbCrLf & "¿Desea continuar confirmando este registro?"
                                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                Dim transparent = New Base.FrmTransparent(formulario, False)
                                transparent.ShowDialog(Me)
                            End Using
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            Me.Deshacer()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Me.Deshacer()
                    End If
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If remissionDevolution.Id > 0 Then
                        remissionDevolution = Await model.GetRemissionDevolutionByCode(Code)
                    Else
                        remissionDevolution = New RemissionDevolution
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDDteDate.Properties.MinValue = dateMin
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Tipo de Devolución", .FieldName = "DevolutionTypeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "RemissionDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllRemissionDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.remissionDevolution.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.remissionDevolution.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewRemissionDevolution() As Task
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        remissionDevolution = New RemissionDevolution()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"

        'Me.remissionDevolution = New RemissionDevolution()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = 0
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        '    BarraBotones.StatusRecordVisible = True
        '    BarraBotones.StatusRecord = "1"
        'End If
    End Function

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim remissionCode As String
        If DevolutionType = 1 Then
            remissionCode = If(INDSleRemissionNumberInput.Text, INDSleRemissionNumberInput.Properties.NullText, INDSleRemissionNumberInput.Text)
        ElseIf DevolutionType = 2 Then
            remissionCode = If(INDSleRemissionNumberOutput.Text, INDSleRemissionNumberOutput.Properties.NullText, INDSleRemissionNumberOutput.Text)
        Else
            remissionCode = If(INDsleRemissionNumberConsignmentInventory.Text, INDsleRemissionNumberConsignmentInventory.Properties.NullText, INDsleRemissionNumberConsignmentInventory.Text)
        End If
        Dim content = String.Format(ResourceManager.GetString("FrmReturnReferrals_IndexContent", MODULE_NAME), remissionDevolution.Code, INDDteDate.EditValue, If(INDSleWarehouse.Text Is String.Empty, INDSleWarehouse.Properties.NullText, INDSleWarehouse.Text), INDGleRemissionType.Text, remissionCode)
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.remissionDevolution.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.remissionDevolution.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.remissionDevolution.Code)
            Return Me._doc
        End If
    End Function

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

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MRemissionDevolution(CStr(Me.Tag))
                    AsyncLoader(True)
                    remissionDevolution = Await Model.GetRemissionDevolutionByCode(INDBteCode.Text.Trim)
                    INDLcRemissionDevolution.BeginUpdate()
                    If remissionDevolution IsNot Nothing AndAlso remissionDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(remissionDevolution.Id))
                            With remissionDevolution
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                If .Status <> 1 Then
                                    INDDteDate.Properties.MinValue = .RemissionDate
                                End If

                                flagLoadControls = True
                                Code = .Code
                                RemissionDate = .RemissionDate
                                DevolutionType = .DevolutionType
                                INDGleRemissionType.Properties.ReadOnly = True
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .CodeNameWarehouse
                                INDSleWarehouse.Properties.ReadOnly = True
                                RemissionEntranceId = .RemissionEntranceId
                                INDSleRemissionNumberInput.Properties.NullText = .CodeRemissionEntrace
                                INDSleRemissionNumberInput.Properties.ReadOnly = True
                                RemissionOutputId = .RemissionOutputId
                                INDSleRemissionNumberOutput.Properties.NullText = .CodeRemissionOutput
                                INDSleRemissionNumberOutput.Properties.ReadOnly = True
                                ConsignmentInventoryRemissionId = .ConsignmentInventoryRemissionId
                                INDsleRemissionNumberConsignmentInventory.Properties.NullText = .CodeConsignmentInventoryRemission
                                INDsleRemissionNumberConsignmentInventory.Properties.ReadOnly = True
                                Description = .Description
                                BarraBotones.StatusRecord = .Status.ToString()
                                If DevolutionType = 1 Then
                                    listRemissionEntranceDetailBatchSerial = Await Model.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId)
                                    listRemissionEntranceDetailBatchSerial.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                                    For Each item In .RemissionDevolutionDetail
                                        Dim entranceDetail = listRemissionEntranceDetailBatchSerial.Find(Function(x) x.Id = item.RemissionEntranceDetailBatchSerialId)
                                        If entranceDetail IsNot Nothing Then
                                            entranceDetail.QuantityDeliver = item.Quantity
                                            entranceDetail.DevolutionCauseId = item.DevolutionCauseId
                                        End If
                                    Next
                                    INDGcProduct.DataSource = Nothing
                                    INDGcProduct.DataSource = listRemissionEntranceDetailBatchSerial
                                    Using modelEntrance As New MReferralEntry(MyTag)
                                        Dim remissionEntrance = modelEntrance.GetRemissionEntranceById(.RemissionEntranceId)
                                        INDSleSupplier.Properties.NullText = remissionEntrance.CodeNameSupplier + " - " + remissionEntrance.CodeNameDistributionLine
                                        INDDteRemissionDate.EditValue = remissionEntrance.RemissionDate
                                        INDSleWarehouseRemission.EditValue = remissionEntrance.WarehouseId
                                        INDSleWarehouseRemission.Properties.NullText = remissionEntrance.CodeNameWareHouse
                                    End Using
                                ElseIf DevolutionType = 2 Then
                                    listRemissionOutputDetailPhysical = Await Model.ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId)
                                    listRemissionOutputDetailPhysical.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                                    For Each item In .RemissionDevolutionDetail
                                        Dim outputDetail = listRemissionOutputDetailPhysical.Find(Function(x) x.Id = item.RemissionOutputDetailPhysicalId)
                                        If outputDetail IsNot Nothing Then
                                            outputDetail.QuantityDeliver = item.Quantity
                                            outputDetail.DevolutionCauseId = item.DevolutionCauseId
                                        End If
                                    Next
                                    INDGcProduct.DataSource = Nothing
                                    INDGcProduct.DataSource = listRemissionOutputDetailPhysical
                                    Using modelOutput As New MRemissionOutput(MyTag)
                                        Dim remissionOutput = modelOutput.GetRemissionOutputById(.RemissionOutputId)
                                        INDSleCustomer.Properties.NullText = remissionOutput.CodeNameCustomer
                                        INDDteRemissionDate.EditValue = remissionOutput.RemissionDate
                                        INDSleWarehouseRemission.EditValue = remissionOutput.WarehouseId
                                        INDSleWarehouseRemission.Properties.NullText = remissionOutput.CodeNameWareHouse
                                    End Using
                                Else
                                    listConsignmentInventoryRemissionDetailBatchSerial = Await Model.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId)
                                    listConsignmentInventoryRemissionDetailBatchSerial.ToList().ForEach(Sub(d) d.QuantityDeliver = 0)
                                    For Each item In .RemissionDevolutionDetail
                                        Dim entranceDetail = listConsignmentInventoryRemissionDetailBatchSerial.Find(Function(x) x.Id = item.ConsignmentInventoryRemissionDetailBatchSerialId)
                                        If entranceDetail IsNot Nothing Then
                                            entranceDetail.QuantityDeliver = item.Quantity
                                            entranceDetail.DevolutionCauseId = item.DevolutionCauseId
                                        End If
                                    Next
                                    INDGcProduct.DataSource = Nothing
                                    INDGcProduct.DataSource = listConsignmentInventoryRemissionDetailBatchSerial
                                    Using modelEntrance As New MConsignmentInventoryRemission(MyTag)
                                        Dim consignmentInventoryRemission = modelEntrance.GetConsignmentInventoryRemissionById(.ConsignmentInventoryRemissionId)
                                        INDSleSupplier.Properties.NullText = consignmentInventoryRemission.CodeNameSupplier + " - " + consignmentInventoryRemission.CodeNameDistributionLine
                                        INDDteRemissionDate.EditValue = consignmentInventoryRemission.RemissionDate
                                        INDSleWarehouseRemission.EditValue = consignmentInventoryRemission.WarehouseId
                                        INDSleWarehouseRemission.Properties.NullText = consignmentInventoryRemission.CodeNameWareHouse
                                    End Using
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.remissionDevolution.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = remissionDevolution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(remissionDevolution.Id, Me.Tag.ToString(), Nothing, GetType(RemissionDevolution).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            'Dim reportDef As New Reporter.rptRemissionDevolution
                            'Me.BarraBotones.PrintReport(reportDef, remissionDevolution.Id, False, Me.Tag, Nothing, "FrmReturnReferrals")
                            Me.BarraBotones.PrintReport(PrintReportAction.None, remissionDevolution.Id, 0, remissionDevolution.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDDteDate.Focus()
                            flagLoadControls = False
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRemissionDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcRemissionDevolution.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function
    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcRemissionDevolution.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        Code = String.Empty
        RemissionDate = GetDateServer()
        DevolutionType = Nothing
        INDGleRemissionType.Properties.ReadOnly = False
        WarehouseId = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        INDSleWarehouse.Properties.ReadOnly = False
        RemissionEntranceId = Nothing
        Description = String.Empty
        INDSleRemissionNumberInput.Properties.NullText = String.Empty
        INDSleRemissionNumberInput.Properties.ReadOnly = False
        INDLciRemissionNumberInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciRemissionNumberInput.AllowHide = True
        INDLciSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        RemissionOutputId = Nothing
        INDSleRemissionNumberOutput.Properties.NullText = String.Empty
        INDSleRemissionNumberOutput.Properties.ReadOnly = False
        INDLciRemissionNumberOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciRemissionNumberOutput.AllowHide = True
        ConsignmentInventoryRemissionId = Nothing
        INDsleRemissionNumberConsignmentInventory.Properties.NullText = String.Empty
        INDsleRemissionNumberConsignmentInventory.Properties.ReadOnly = False
        INDLciRemissionNumberConsignmentInventory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciRemissionNumberConsignmentInventory.AllowHide = True
        INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleCustomer.Properties.ReadOnly = True
        INDSleCustomer.Properties.NullText = String.Empty
        INDSleSupplier.Properties.ReadOnly = True
        INDSleSupplier.Properties.NullText = String.Empty
        INDDteRemissionDate.EditValue = Nothing
        INDDteRemissionDate.Properties.ReadOnly = True
        INDSleWarehouseRemission.Properties.ReadOnly = True
        INDSleWarehouseRemission.EditValue = Nothing
        INDSleWarehouseRemission.Properties.NullText = String.Empty
        INDGcProduct.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProduct)
        ActionsOnControls = False
        INDLcRemissionDevolution.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' metodo para asignar los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With remissionDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .RemissionDate = RemissionDate
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .DevolutionType = DevolutionType
            .WarehouseId = WarehouseId
            .Prefix = Me._prefixSelected
            If DevolutionType = 1 Then
                .RemissionEntranceId = RemissionEntranceId
                If .Id = 0 Then
                    For Each item In listRemissionEntranceDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                        Dim devolutionDetail As New RemissionDevolutionDetail
                        devolutionDetail.ProductId = item.ProductId
                        devolutionDetail.Quantity = item.QuantityDeliver
                        devolutionDetail.RemissionEntranceDetailBatchSerialId = item.Id
                        devolutionDetail.CodeNameProduct = item.CodeNameProduct
                        devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                        .RemissionDevolutionDetail.Add(devolutionDetail)
                    Next
                Else
                    For Each item In listRemissionEntranceDetailBatchSerial
                        Dim detail = .RemissionDevolutionDetail.Where(Function(x) x.RemissionEntranceDetailBatchSerialId = item.Id).FirstOrDefault()
                        If detail IsNot Nothing Then
                            detail.CodeNameProduct = item.CodeNameProduct
                            detail.Quantity = item.QuantityDeliver
                            detail.DevolutionCauseId = item.DevolutionCauseId
                        Else
                            Dim devolutionDetail As New RemissionDevolutionDetail
                            devolutionDetail.ProductId = item.ProductId
                            devolutionDetail.Quantity = item.QuantityDeliver
                            devolutionDetail.RemissionEntranceDetailBatchSerialId = item.Id
                            devolutionDetail.CodeNameProduct = item.CodeNameProduct
                            devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                            .RemissionDevolutionDetail.Add(devolutionDetail)
                        End If
                    Next
                End If
            ElseIf DevolutionType = 2 Then
                .RemissionOutputId = RemissionOutputId
                If .Id = 0 Then
                    For Each item In listRemissionOutputDetailPhysical.FindAll(Function(x) x.QuantityDeliver > 0)
                        Dim devolutionDetail As New RemissionDevolutionDetail
                        devolutionDetail.ProductId = item.ProductId
                        devolutionDetail.Quantity = item.QuantityDeliver
                        devolutionDetail.RemissionOutputDetailPhysicalId = item.Id
                        devolutionDetail.CodeNameProduct = item.CodeNameProduct
                        devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                        .RemissionDevolutionDetail.Add(devolutionDetail)
                    Next
                Else
                    For Each item In listRemissionOutputDetailPhysical
                        Dim detail = .RemissionDevolutionDetail.Where(Function(x) x.RemissionOutputDetailPhysicalId = item.Id).FirstOrDefault()
                        If detail IsNot Nothing Then
                            detail.CodeNameProduct = item.CodeNameProduct
                            detail.Quantity = item.QuantityDeliver
                            detail.DevolutionCauseId = item.DevolutionCauseId
                        Else
                            Dim devolutionDetail As New RemissionDevolutionDetail
                            devolutionDetail.ProductId = item.ProductId
                            devolutionDetail.Quantity = item.QuantityDeliver
                            devolutionDetail.RemissionEntranceDetailBatchSerialId = item.Id
                            devolutionDetail.CodeNameProduct = item.CodeNameProduct
                            devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                            .RemissionDevolutionDetail.Add(devolutionDetail)
                        End If
                    Next
                End If
            Else
                .ConsignmentInventoryRemissionId = ConsignmentInventoryRemissionId
                If .Id = 0 Then
                    For Each item In listConsignmentInventoryRemissionDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                        Dim devolutionDetail As New RemissionDevolutionDetail
                        devolutionDetail.ProductId = item.ProductId
                        devolutionDetail.Quantity = item.QuantityDeliver
                        devolutionDetail.ConsignmentInventoryRemissionDetailBatchSerialId = item.Id
                        devolutionDetail.CodeNameProduct = item.CodeNameProduct
                        devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                        .RemissionDevolutionDetail.Add(devolutionDetail)
                    Next
                Else
                    For Each item In listConsignmentInventoryRemissionDetailBatchSerial
                        Dim detail = .RemissionDevolutionDetail.Where(Function(x) x.ConsignmentInventoryRemissionDetailBatchSerialId = item.Id).FirstOrDefault()
                        If detail IsNot Nothing Then
                            detail.CodeNameProduct = item.CodeNameProduct
                            detail.Quantity = item.QuantityDeliver
                            detail.DevolutionCauseId = item.DevolutionCauseId
                        Else
                            Dim devolutionDetail As New RemissionDevolutionDetail
                            devolutionDetail.ProductId = item.ProductId
                            devolutionDetail.Quantity = item.QuantityDeliver
                            devolutionDetail.ConsignmentInventoryRemissionDetailBatchSerialId = item.Id
                            devolutionDetail.CodeNameProduct = item.CodeNameProduct
                            devolutionDetail.DevolutionCauseId = item.DevolutionCauseId
                            .RemissionDevolutionDetail.Add(devolutionDetail)
                        End If
                    Next
                End If
            End If
            .Description = Description
            .Status = 1
        End With
    End Sub

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        indexEditRecord = Nothing
        _sequence = Nothing
        _prefixSelected = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        presenter = Nothing
        remissionDevolution = Nothing
        listRemissionEntranceDetailBatchSerial = Nothing
        listRemissionOutputDetailPhysical = Nothing
        listConsignmentInventoryRemissionDetailBatchSerial = Nothing
        flagLoadControls = Nothing
        varImp = Nothing
        _action = Nothing
    End Sub

    Private Async Sub FrmReturnReferrals_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRemissionDevolution, True)

        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        _idOperativeUnit = BarraBotones.OperatingUnitValue

        presenter = New PRemissionDevolution(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        presenter.LoadDevolutionCauses()
        Await Me.LoadParameters()

        INDGleRemissionType.Properties.DataSource = ListDevolutionType
        'menu contextual
        Dim _list = New List(Of eAcciones)
        _list.Add(eAcciones.ReturnAllQuantities)
        _list.Add(eAcciones.Set0Quantities)
        IndigoGridView1.SetListAcction(INDGvProduct, _list)
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmReturnReferrals_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmReturnReferrals_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopup"
    Private Sub INDSleRemissionNumberInput_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRemissionNumberInput.QueryPopUp
        If INDSleRemissionNumberInput.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If RemissionEntranceXPO Is Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                RemissionEntranceXPO = model.ListRemissionEntrance(WarehouseId)
            End Using
        End If
    End Sub

    Private Sub INDSleRemissionNumberOutput_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRemissionNumberOutput.QueryPopUp
        If INDSleRemissionNumberOutput.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If RemissionOutputXPO Is Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                RemissionOutputXPO = model.ListRemissionOutput(WarehouseId)
            End Using
        End If
    End Sub

    Private Sub INDSleRemissionNumberConsignmentInventory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleRemissionNumberConsignmentInventory.QueryPopUp
        If INDsleRemissionNumberConsignmentInventory.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ConsignmentInventoryRemissionXPO Is Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                ConsignmentInventoryRemissionXPO = model.ListConsignmentInventoryRemission(WarehouseId)
            End Using
        End If
    End Sub

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WarehouseXPO Is Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                WarehouseXPO = model.ListWarehouse(DevolutionType)
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDGleRemissionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRemissionType.EditValueChanged
        If DevolutionType IsNot Nothing Then
            INDLciRemissionNumberInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionNumberInput.AllowHide = True
            INDLciSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionNumberOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionNumberOutput.AllowHide = True
            INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionNumberConsignmentInventory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionNumberConsignmentInventory.AllowHide = True
            If DevolutionType = 1 Then 'entrada
                INDLciRemissionNumberInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRemissionNumberInput.AllowHide = False
                INDLciSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf DevolutionType = 2 Then 'salida
                INDLciRemissionNumberOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRemissionNumberOutput.AllowHide = False
                INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'Inventario en consignacion
                INDLciRemissionNumberConsignmentInventory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRemissionNumberConsignmentInventory.AllowHide = False
                INDLciSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            WarehouseXPO = Nothing
            listRemissionEntranceDetailBatchSerial = Nothing
            INDSleRemissionNumberInput.EditValue = Nothing
            listRemissionOutputDetailPhysical = Nothing
            INDSleRemissionNumberOutput.EditValue = Nothing
            listConsignmentInventoryRemissionDetailBatchSerial = Nothing
            INDsleRemissionNumberConsignmentInventory.EditValue = Nothing
            INDGcProduct.DataSource = Nothing
            IndigoGridControl1.RefreshGrid(INDGcProduct)
        End If
    End Sub

    Private Async Sub INDSleRemissionNumberInput_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRemissionNumberInput.EditValueChanged
        If flagLoadControls = True Then
            Exit Sub
        End If

        INDSleSupplier.Properties.NullText = String.Empty
        INDDteRemissionDate.EditValue = Nothing
        INDSleWarehouseRemission.EditValue = Nothing
        INDSleWarehouseRemission.Properties.NullText = String.Empty
        listRemissionEntranceDetailBatchSerial = Nothing
        INDGcProduct.DataSource = Nothing

        If RemissionEntranceId IsNot Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                listRemissionEntranceDetailBatchSerial = Await model.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId)
                INDGcProduct.DataSource = listRemissionEntranceDetailBatchSerial
                Using modelEntrance As New MReferralEntry(MyTag)
                    Dim remissionEntrance = modelEntrance.GetRemissionEntranceById(INDSleRemissionNumberInput.EditValue)
                    INDSleSupplier.Properties.NullText = remissionEntrance.CodeNameSupplier + " - " + remissionEntrance.CodeNameDistributionLine
                    INDDteRemissionDate.EditValue = remissionEntrance.RemissionDate
                    INDSleWarehouseRemission.EditValue = remissionEntrance.WarehouseId
                    INDSleWarehouseRemission.Properties.NullText = remissionEntrance.CodeNameWareHouse
                End Using
            End Using
        End If
    End Sub

    Private Async Sub INDSleRemissionNumberOutput_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRemissionNumberOutput.EditValueChanged
        If flagLoadControls = True Then
            Exit Sub
        End If

        INDSleCustomer.Properties.NullText = String.Empty
        INDDteRemissionDate.EditValue = Nothing
        INDSleWarehouseRemission.EditValue = Nothing
        INDSleWarehouseRemission.Properties.NullText = String.Empty
        listRemissionOutputDetailPhysical = Nothing
        INDGcProduct.DataSource = Nothing

        If RemissionOutputId IsNot Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                listRemissionOutputDetailPhysical = Await model.ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId)
                INDGcProduct.DataSource = Nothing
                INDGcProduct.DataSource = listRemissionOutputDetailPhysical
                Using modelOutput As New MRemissionOutput(MyTag)
                    Dim remissionOutput = modelOutput.GetRemissionOutputById(INDSleRemissionNumberOutput.EditValue)
                    INDSleCustomer.Properties.NullText = remissionOutput.CodeNameCustomer
                    INDDteRemissionDate.EditValue = remissionOutput.RemissionDate
                    INDSleWarehouseRemission.EditValue = remissionOutput.WarehouseId
                    INDSleWarehouseRemission.Properties.NullText = remissionOutput.CodeNameWareHouse
                End Using
            End Using
        End If
    End Sub

    Private Async Sub INDSleRemissionNumberConsignmentInventory_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRemissionNumberConsignmentInventory.EditValueChanged
        If flagLoadControls = True Then
            Exit Sub
        End If

        INDSleSupplier.Properties.NullText = String.Empty
        INDDteRemissionDate.EditValue = Nothing
        INDSleWarehouseRemission.EditValue = Nothing
        INDSleWarehouseRemission.Properties.NullText = String.Empty
        listConsignmentInventoryRemissionDetailBatchSerial = Nothing
        INDGcProduct.DataSource = Nothing

        If ConsignmentInventoryRemissionId IsNot Nothing Then
            Using model As New MRemissionDevolution(MyTag)
                listConsignmentInventoryRemissionDetailBatchSerial = Await model.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId)
                INDGcProduct.DataSource = Nothing
                INDGcProduct.DataSource = listConsignmentInventoryRemissionDetailBatchSerial
                Using modelEntrance As New MConsignmentInventoryRemission(MyTag)
                    Dim consignmentInventoryRemission = modelEntrance.GetConsignmentInventoryRemissionById(INDsleRemissionNumberConsignmentInventory.EditValue)
                    INDSleSupplier.Properties.NullText = consignmentInventoryRemission.CodeNameSupplier + " - " + consignmentInventoryRemission.CodeNameDistributionLine
                    INDDteRemissionDate.EditValue = consignmentInventoryRemission.RemissionDate
                    INDSleWarehouseRemission.EditValue = consignmentInventoryRemission.WarehouseId
                    INDSleWarehouseRemission.Properties.NullText = consignmentInventoryRemission.CodeNameWareHouse
                End Using
            End Using
        End If
    End Sub

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        RemissionEntranceId = Nothing
        RemissionOutputId = Nothing
        ConsignmentInventoryRemissionId = Nothing

        RemissionEntranceXPO = Nothing
        RemissionOutputXPO = Nothing
        ConsignmentInventoryRemissionXPO = Nothing

        If Me.INDSleWarehouse.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvWarehouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvWarehouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.remissionDevolution.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.remissionDevolution.Prefix)
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"
    Private Sub INDRptSeQuantityDeliver_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantityDeliver.EditValueChanging
        Dim value As Integer
        Try
            value = Convert.ToInt32(e.NewValue)
        Catch ex As Exception
            Exit Sub
        End Try
        If DevolutionType = 1 Then
            Dim entranceDetail = DirectCast(INDGvProduct.GetFocusedRow, RemissionEntranceDetailBatchSerial)
            If entranceDetail.OutstandingQuantity < value Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("MaxQuantity", MODULE_NAME), entranceDetail.OutstandingQuantity.ToString())
            End If
        ElseIf DevolutionType = 2 Then
            Dim outputDetail = DirectCast(INDGvProduct.GetFocusedRow, RemissionOutputDetailPhysical)
            If outputDetail.OutstandingQuantity < value Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("MaxQuantity", MODULE_NAME), outputDetail.OutstandingQuantity.ToString())
            End If
        Else
            Dim entranceDetail = DirectCast(INDGvProduct.GetFocusedRow, ConsignmentInventoryRemissionDetailBatchSerial)
            If entranceDetail.OutstandingQuantity < value Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("MaxQuantity", MODULE_NAME), entranceDetail.OutstandingQuantity.ToString())
            End If
        End If
    End Sub

    Private Sub INDGleRemissionType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleRemissionType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If e.NewValue = e.OldValue Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewRemissionDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If DevolutionType IsNot Nothing Then
                If DevolutionType = 1 Then
                    INDSleRemissionNumberInput.Focus()
                ElseIf DevolutionType = 2 Then
                    INDSleRemissionNumberOutput.Focus()
                Else
                    INDsleRemissionNumberConsignmentInventory.Focus()
                End If
            End If
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmSupplier
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores
                OpenFormDialog(form)
                WarehouseXPO = Nothing
                INDSleWarehouse.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleWarehouseRemission_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouseRemission.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSleRemissionNumberInput_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleRemissionNumberInput.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmReferralEntry
                OpenFormDialog(form)
                RemissionEntranceXPO = Nothing
                INDSleRemissionNumberInput.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleRemissionNumberOutput_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleRemissionNumberOutput.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmReferralOut
                OpenFormDialog(form)
                RemissionOutputXPO = Nothing
                INDSleRemissionNumberOutput.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleRemissionNumberConsignmentInventory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRemissionNumberConsignmentInventory.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmConsignmentInventoryRemission
                OpenFormDialog(form)
                ConsignmentInventoryRemissionXPO = Nothing
                INDsleRemissionNumberConsignmentInventory.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCustomers
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento al aceptar dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmNotificationItemDetailConfirm_Accept(sender As Object, e As EventArgs)
        SaveOrUpdateAndConfirm(_action, False)
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.remissionDevolution IsNot Nothing AndAlso Me.remissionDevolution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

    ''' <summary>
    ''' Evento que se dispara cuando clickeao un item del control de menu de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If Object.Equals(sender.Tag.ToString(), "ReturnAllQuantities") Then
            For Each _index As Integer In INDGvProduct.GetSelectedRows()
                Select Case Me.INDGleRemissionType.EditValue
                    Case 1
                        Dim _Item As RemissionEntranceDetailBatchSerial = INDGvProduct.GetRow(_index)
                        If _Item IsNot Nothing Then _Item.QuantityDeliver = _Item.OutstandingQuantity
                    Case 2
                        Dim _Item As RemissionOutputDetailPhysical = INDGvProduct.GetRow(_index)
                        If _Item IsNot Nothing Then _Item.QuantityDeliver = _Item.OutstandingQuantity
                    Case 3
                        Dim _Item As ConsignmentInventoryRemissionDetailBatchSerial = INDGvProduct.GetRow(_index)
                        If _Item IsNot Nothing Then _Item.QuantityDeliver = _Item.OutstandingQuantity
                End Select
            Next
        Else
            For Each _index As Integer In INDGvProduct.GetSelectedRows()
                Select Case Me.INDGleRemissionType.EditValue
                    Case 1
                        Dim _item As RemissionEntranceDetailBatchSerial = INDGvProduct.GetRow(_index)
                        If _item IsNot Nothing Then
                            _item.QuantityDeliver = 0
                        End If
                    Case 2
                        Dim _item As RemissionOutputDetailPhysical = INDGvProduct.GetRow(_index)
                        If _item IsNot Nothing Then
                            _item.QuantityDeliver = 0
                        End If
                    Case 3
                        Dim _item As ConsignmentInventoryRemissionDetailBatchSerial = INDGvProduct.GetRow(_index)
                        If _item IsNot Nothing Then
                            _item.QuantityDeliver = 0
                        End If
                End Select
            Next
        End If
        INDGcProduct.RefreshDataSource()
    End Sub
#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
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
        BarraBotones.Focus()
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Dim reportDef As New Reporter.rptRemissionDevolution
        'Me.BarraBotones.PrintReport(reportDef, remissionDevolution.Id, True, Me.Tag, Nothing, "FrmReturnReferrals")
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, remissionDevolution.Id, 0, remissionDevolution.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.remissionDevolution.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub

#End Region

End Class