'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 30/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll

#End Region

Public Class FrmRequest
    Implements IInventoryRequest, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la solicitud de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryRequest.Code
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
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IInventoryRequest.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de movimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MovementType As Integer? Implements IInventoryRequest.MovementType
        Get
            Return INDsleMovementType.EditValue
        End Get
        Set(value As Integer?)
            INDsleMovementType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryRequest.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    '''  Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryRequest.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IInventoryRequest.Sequense
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
    ''' Obtiene o establece una observacion de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IInventoryRequest.Observation
        Get
            Return INDmeObservation.EditValue
        End Get
        Set(value As String)
            INDmeObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequestType As Byte? Implements IInventoryRequest.RequestType
        Get
            Return INDsleRequestType.EditValue
        End Get
        Set(value As Byte?)
            INDsleRequestType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del almacen de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceWarehouseId As Integer? Implements IInventoryRequest.SourceWarehouseId
        Get
            Return INDsleSourceWarehouseId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSourceWarehouseId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceWarehouseXpo As XPInstantFeedbackSource Implements IInventoryRequest.SourceWarehouseXpo
        Get
            Return INDsleSourceWarehouseId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSourceWarehouseId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IInventoryRequest.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetFunctionalUnitId As Integer? Implements IInventoryRequest.TargetFunctionalUnitId
        Get
            Return INDsleTargetFunctionalUnitId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetFunctionalUnitId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetFunctionalUnitXpo As XPInstantFeedbackSource Implements IInventoryRequest.TargetFunctionalUnitXpo
        Get
            Return INDsleTargetFunctionalUnitId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetFunctionalUnitId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del Almacen de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetWarehouseId As Integer? Implements IInventoryRequest.TargetWarehouseId
        Get
            Return INDsleTargetWarehouseId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetWarehouseId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetWarehouseXpo As XPInstantFeedbackSource Implements IInventoryRequest.TargetWarehouseXpo
        Get
            Return INDsleTargetWarehouseId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetWarehouseId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInventoryRequest

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MInventoryRequest

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim inventoryRequest As InventoryRequest

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemInventoryRequestDetail As InventoryRequestDetail

    ''' <summary>
    ''' Variable que contiene un item del segunfo detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemInventoryRequestDetailOther As InventoryRequestDetailOther

    ''' <summary>
    ''' Listado de eliminados de los detalles de solicitud de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteInventoryRequestDetail As List(Of InventoryRequestDetail)

    ''' <summary>
    ''' Listado de los detalles de solicitud de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Property ListInventoryRequestDetail As Domain.Entities.TrackableCollection(Of InventoryRequestDetail)
        Get
            If INDgcProducts Is Nothing Then
                Return New Domain.Entities.TrackableCollection(Of InventoryRequestDetail)
            End If
            Return INDgcProducts.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of InventoryRequestDetail))
            INDgcProducts.DataSource = value
            If inventoryRequest IsNot Nothing Then
                inventoryRequest.InventoryRequestDetail = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Variable para saber si confirma (True = Si confirma, False = No confirma)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si guardan y confirman, o si actualizan y confirman (True = GuardarConfirmar, False = ActualizarConfirmar)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSaveAndConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si anulan (True = Si anula, False = no confirma)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banAnular As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de solicitud
    ''' </summary>
    Dim ListRequestType As New List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de movimiento
    ''' </summary>
    Dim ListMovementType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Cantidad de registros que se envian a validar
    ''' </summary>
    ''' <remarks></remarks>
    Const QuantityRowsSend As Integer = 100

    ''' <summary>
    ''' Tabla de detalle de medicamentos, insumos u otros
    ''' </summary>
    Dim RequestOtherDetail As InventoryRequestDetailOther

    ''' <summary>
    ''' Listado de los detalles de solicitud de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Property ListRequestOtherDetail As Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)
        Get
            If INDgcProducts Is Nothing Then
                Return New Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)
            End If
            Return INDGcOtherProducts.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther))
            INDGcOtherProducts.DataSource = value
            If inventoryRequest IsNot Nothing Then
                inventoryRequest.InventoryRequestDetailOther = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Listado de medicamentos, insumos o otros
    ''' </summary>
    Dim ListDeleteRequestOtherDetail As List(Of InventoryRequestDetailOther)

    ''' <summary>
    ''' Listado de productos o Insumos por unidad funcional según parámetros de solicitudes
    ''' </summary>
    Dim ListViewRequestParamXpo As List(Of ViewRequestParamXpo)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If inventoryRequest IsNot Nothing AndAlso inventoryRequest.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MInventoryRequest(Me.Tag.ToString())
                    AsyncLoader(True)
                    inventoryRequest.MarkAsDeleted()
                    Dim result = Await Model.DeleteInventoryRequest(inventoryRequest)
                    If result.StateResult = True Then
                        Me.DeleteDocumentIndexed()
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    Private Function IsValidForRequestParams() As Boolean
        Dim sb As New StringBuilder()
        If ListViewRequestParamXpo.Any() Then
            If Not ListViewRequestParamXpo.Any(Function(x) x.Aproved) Then
                If MessageIndigo.Show($"La solicitud no pudo confirmarse. Se podrán hacer solicitudes {ValidMeasureUnitTimeAndFrecuency(ListViewRequestParamXpo.First)} ¿Desea guardarla?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    inventoryRequest.Status = 1
                    banConfirm = False
                    banSaveAndConfirm = False
                    banAnular = False
                Else
                    Return False
                End If
            End If

            For Each detail In inventoryRequest.InventoryRequestDetailOther.Where(Function(d) d.ComponentType <> 1).ToList
                Dim requestByType As New List(Of ViewRequestParamXpo)
                If detail.ComponentType = 2 Then 'Insumo
                    requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 1 AndAlso i.SupplieId.Value = detail.SupplieId).ToList()
                ElseIf detail.ComponentType = 3 Then 'Producto
                    requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 2 AndAlso i.ProductId.Value = detail.InventoryProductId).ToList()
                End If

                If Not requestByType.Any() Then
                    sb.AppendLine(String.Format("El componente {0} no se encuentra parametrizado para la unidad funcional", detail.SourceCodeName))
                    Continue For
                End If

                If requestByType.Count > 1 Then
                    sb.AppendLine(String.Format("El componente {0} no se encuentra parametrizado más de una vez para la unidad funcional", detail.SourceCodeName))
                    Continue For
                End If

                If detail.Quantity > requestByType.First().Quantity Then
                    sb.AppendLine(String.Format("Cantidad no autorizada para el producto {0}", detail.SourceCodeName))
                    Continue For
                End If
            Next

        End If

        If sb.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = sb.ToString
            Return False
        End If

        Return True
    End Function

    Function ValidMeasureUnitTimeAndFrecuency(requestParam As ViewRequestParamXpo) As String
        Dim days As New List(Of String)
        Dim result As New StringBuilder

        If {2, 3}.Contains(requestParam.MeasuryUnitTime) Then

            If requestParam.Monday Then
                days.Add("lunes")
            End If

            If requestParam.Tuesday Then
                days.Add("martes")
            End If

            If requestParam.Wednesday Then
                days.Add("miércoles")
            End If

            If requestParam.Thursday Then
                days.Add("jueves")
            End If

            If requestParam.Friday Then
                days.Add("viernes")
            End If

            If requestParam.Saturday Then
                days.Add("sábado")
            End If

            If requestParam.Sunday Then
                days.Add("domingo")
            End If

            days.Add($"cada {requestParam.Frecuency} {requestParam.UnitTime}")
        End If

        If requestParam.MeasuryUnitTime = 1 Then
            Return result.AppendLine($"cada {requestParam.Frecuency} {requestParam.UnitTime}").ToString
        End If

        If days.Any Then
            Return result.AppendLine(String.Concat("solamente los días", " ", String.Join(", ", days.ToArray()))).ToString
        End If

        Return ""
    End Function

    Function isValidDate(requestParam As ViewRequestParamXpo) As Boolean
        Dim dateServer = Me.GetDateServer()

        If Me.IsHoliday(dateServer) Then
            Return requestParam.Holiday
        End If

        Select Case dateServer.DayOfWeek
            Case 1
                Return requestParam.Monday
            Case 2
                Return requestParam.Tuesday
            Case 3
                Return requestParam.Wednesday
            Case 4
                Return requestParam.Thursday
            Case 5
                Return requestParam.Friday
            Case 6
                Return requestParam.Saturday
            Case 7
                Return requestParam.Sunday
        End Select

        Return True
    End Function

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        AssigningValues()
        Try
            Using model As New MInventoryRequest(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveInventoryRequest(inventoryRequest, _idCurrentSequence, Me._sequence)
                If Result.StateResult = True Then
                    If inventoryRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense.Remove(_idCurrentSequence)
                        End If
                        If Me._sequence.Sequential Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf inventoryRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If banConfirm Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                            End If
                        ElseIf banAnular Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If

                    End If
                    Me.inventoryRequest = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, inventoryRequest.Id, 0, inventoryRequest.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, inventoryRequest.Id, 0, inventoryRequest.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, inventoryRequest.Id, 0, inventoryRequest.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, inventoryRequest.Id, 0, inventoryRequest.Id)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                    If inventoryRequest.Id > 0 Then
                        inventoryRequest = model.GetInventoryRequestByCode(Code).Result.ObjectEmbbeded
                    Else
                        inventoryRequest = New InventoryRequest
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInventoryRequest()
        End If
    End Sub

#End Region

#Region "Methods"

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

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()

        ListRequestType = New List(Of Tuple(Of Byte, String))
        ListRequestType.Add(New Tuple(Of Byte, String)(1, "Unidad Funcional"))
        ListRequestType.Add(New Tuple(Of Byte, String)(2, "Almacen"))
        INDsleRequestType.Properties.DataSource = ListRequestType.ToList


        ListMovementType = New List(Of Tuple(Of Integer, String))
        ListMovementType.Add(New Tuple(Of Integer, String)(1, "Consumo"))
        ListMovementType.Add(New Tuple(Of Integer, String)(2, "Traslado"))
        INDsleMovementType.Properties.DataSource = ListMovementType.ToList


    End Sub

    ''' <summary>
    ''' Carga los estados de la solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryRequest.Code, Me.inventoryRequest.OperatingUnitId, Me.inventoryRequest.DocumentDate, Me.inventoryRequest.RequestType, Me.inventoryRequest.Observation),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.inventoryRequest.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryRequest.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryRequest.Code, Me.inventoryRequest.OperatingUnitId, Me.inventoryRequest.DocumentDate, Me.inventoryRequest.RequestType, Me.inventoryRequest.Observation)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryRequest.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Tipo Solicitud", .FieldName = "RequestTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Almacén Origen", .FieldName = "SourceWarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Almacén Destino", .FieldName = "TargetWarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Unidad Funcional", .FieldName = "TargetFunctionalUnitId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryRequest
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryRequest.ActionsOnControls
        Set(value As Boolean)
            INDlyRequest.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleRequestType.Enabled = value
            INDsleTargetFunctionalUnitId.Enabled = value
            INDsleMovementType.Enabled = value
            INDsleSourceWarehouseId.Enabled = value
            INDsleTargetWarehouseId.Enabled = value
            INDmeObservation.Enabled = value
            INDBtnAddProducts.Enabled = value
            INDBtnAddOtherProducts.Enabled = value
            INDgcProducts.Enabled = value
            INDGcOtherProducts.Enabled = value
            INDesbExport.Enabled = value
            INDEsbExportOtherProducts.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDlyRequest.EndUpdate()
            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        DeleteBlockedRecord()
        INDlyRequest.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        'Me.BarraBotones.StatusRecordVisible = False

        inventoryRequest = Nothing
        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        RequestType = 0
        TargetFunctionalUnitId = Nothing
        INDsleTargetFunctionalUnitId.Properties.NullText = String.Empty
        MovementType = 0
        SourceWarehouseId = Nothing
        INDsleSourceWarehouseId.Properties.NullText = String.Empty
        TargetWarehouseId = Nothing
        INDsleTargetWarehouseId.Properties.NullText = String.Empty
        Observation = Nothing
        Status = 0
        CleanLayouts()

        ReadOnlyControls(False)
        ItemInventoryRequestDetail = Nothing
        ListInventoryRequestDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetail)
        ListDeleteInventoryRequestDetail = Nothing
        INDgcProducts.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDgcProducts)
        INDGcOtherProducts.DataSource = Nothing
        IndigoGridControl2.RefreshGrid(INDGcOtherProducts)
        RequestOtherDetail = Nothing
        ListRequestOtherDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)
        ListDeleteRequestOtherDetail = Nothing
        ListViewRequestParamXpo = New List(Of ViewRequestParamXpo)

        INDlyRequest.EndUpdate()
        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Limpia ocultando los layouts
    ''' </summary>
    Private Sub CleanLayouts()
        INDlciTargetFunctionalUnitId.HideControl(True)
        INDlciMovementType.HideControl(True)
        INDlciTargetWarehouseId.HideControl(True)
        INDlciSourceWarehouseId.HideControl(True)
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInventoryRequest() As Task
        inventoryRequest = New InventoryRequest()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.SaveAndConfirmObligatory()
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
                Me.SaveAndConfirmObligatory()
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.SaveAndConfirmObligatory()
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
                            Me.SaveAndConfirmObligatory()
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
                    Me.SaveAndConfirmObligatory()
                End If
            End If
        End If
    End Function

    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With inventoryRequest
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .RequestType = RequestType
            .TargetFunctionalUnitId = TargetFunctionalUnitId
            .MovementType = MovementType
            .Prefix = Me._prefixSelected
            .SourceWarehouseId = SourceWarehouseId
            .TargetWarehouseId = TargetWarehouseId
            .Observation = Observation
            If ListInventoryRequestDetail IsNot Nothing AndAlso ListInventoryRequestDetail.Count > 0 Then
                For Each itemDetail As InventoryRequestDetail In ListInventoryRequestDetail
                    itemDetail.Status = inventoryRequest.Status
                    .InventoryRequestDetail.Add(itemDetail)
                Next
            End If

            If ListDeleteInventoryRequestDetail IsNot Nothing AndAlso ListDeleteInventoryRequestDetail.Count > 0 Then
                For Each itemDeleteDetail As InventoryRequestDetail In ListDeleteInventoryRequestDetail
                    .InventoryRequestDetail.Add(itemDeleteDetail)
                Next
            End If

            If ListRequestOtherDetail IsNot Nothing AndAlso ListRequestOtherDetail.Count > 0 Then
                For Each itemOtherDetail As InventoryRequestDetailOther In ListRequestOtherDetail
                    itemOtherDetail.Status = inventoryRequest.Status
                    .InventoryRequestDetailOther.Add(itemOtherDetail)
                Next
            End If

            If ListDeleteRequestOtherDetail IsNot Nothing AndAlso ListDeleteRequestOtherDetail.Count > 0 Then
                For Each itemOtherDetail As InventoryRequestDetailOther In ListDeleteRequestOtherDetail
                    .InventoryRequestDetailOther.Add(itemOtherDetail.MarkAsDeleted())
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la solicitud de inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MInventoryRequest(CStr(Me.Tag))
                    AsyncLoader(True)
                    inventoryRequest = (Await Model.GetInventoryRequestByCode(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyRequest.BeginUpdate()
                    If inventoryRequest IsNot Nothing AndAlso inventoryRequest.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(inventoryRequest.Id))
                            With inventoryRequest
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Me._idOperativeUnit = .OperatingUnitId
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .DocumentDate
                                RequestType = .RequestType
                                TargetFunctionalUnitId = .TargetFunctionalUnitId
                                INDsleTargetFunctionalUnitId.Properties.NullText = .DescriptionFunctionalUnit
                                MovementType = .MovementType
                                SourceWarehouseId = .SourceWarehouseId
                                INDsleSourceWarehouseId.Properties.NullText = .DescriptionSourceWarehouse
                                TargetWarehouseId = .TargetWarehouseId
                                INDsleTargetWarehouseId.Properties.NullText = .DescriptionTargetWarehouse
                                Observation = .Observation
                                Me.Status = .Status

                                ListInventoryRequestDetail = .InventoryRequestDetail

                                ListRequestOtherDetail = .InventoryRequestDetailOther

                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.inventoryRequest.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = inventoryRequest.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                                INDBtnAddProducts.Enabled = True
                                INDBtnAddOtherProducts.Enabled = True

                            Else
                                INDBtnAddProducts.Enabled = False
                                INDBtnAddOtherProducts.Enabled = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                            End If

                            Me.BarraBotones.SetDocuments(inventoryRequest.Id, Me.Tag.ToString(), Nothing, GetType(InventoryRequest).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, inventoryRequest.Id, 0, inventoryRequest.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInventoryRequest()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyRequest.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If

        If Status = 2 Then
            INDBtnAddProducts.Enabled = False
            INDBtnAddOtherProducts.Enabled = False
        End If
    End Function

    ''' <summary>
    ''' Método utilizado para adquirir losproductos retornados por el frontal de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProduct(sender As Object, e As AddProductInventoryRequestDetailEventArgs)
        If ListInventoryRequestDetail Is Nothing Then
            ListInventoryRequestDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetail)
        End If
        If e.EditMode = True Then
            ListInventoryRequestDetail.Remove(ItemInventoryRequestDetail)

            ListInventoryRequestDetail.Insert(indexEditRecord, e.ItemInventoryRequestDetail)
        Else
            ListInventoryRequestDetail.Add(e.ItemInventoryRequestDetail)
        End If
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulario de agregar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup()
        If ListInventoryRequestDetail Is Nothing Then
            ListInventoryRequestDetail = New Domain.Entities.TrackableCollection(Of Domain.Entities.InventoryRequestDetail)
        End If
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopUpProductsRequest()
            AddHandler formulario.AddInventoryRequestDetail, AddressOf ReturnPopupAddProduct

            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.ListinventoryRequestDetailValidation = ListInventoryRequestDetail.ToList()
            formulario.Size = New Size(800, 700)
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulario de agregar medicamentos, insumos u otros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RequestDetailOtherPopup(EditMode As Boolean)
        Using formulario As New FrmRequestDetailOther()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRequestDetailOtherArgs, AddressOf ReturnAddEventArgs
            formulario.EditModeDetail = EditMode
            If EditMode Then
                RequestOtherDetail = DirectCast(INDGvOtherProducts.GetFocusedRow(), InventoryRequestDetailOther)
                indexEditRecord = ListRequestOtherDetail.IndexOf(RequestOtherDetail)
            Else
                formulario.ListRequestOtherDetailCompare = ListRequestOtherDetail
            End If
            formulario.RequestDetailOther = RequestOtherDetail
            formulario.ListViewRequestParamXpo = ListViewRequestParamXpo
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(700, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        ItemInventoryRequestDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryRequestDetail)
        indexEditRecord = ListInventoryRequestDetail.IndexOf(ItemInventoryRequestDetail)
        Using formulario As New PopUpProductsRequest()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddInventoryRequestDetail, AddressOf ReturnPopupAddProduct
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.inventoryRequestDetailEdit = ItemInventoryRequestDetail
            formulario.EditMode = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Edita el detalle de medicamentos insumos y otros 
    ''' </summary>
    Private Sub EditDetailRequestDetailOther()
        RequestOtherDetail = DirectCast(INDGvOtherProducts.GetFocusedRow(), InventoryRequestDetailOther)
        indexEditRecord = ListRequestOtherDetail.IndexOf(RequestOtherDetail)
        RequestDetailOtherPopup(True)
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        ItemInventoryRequestDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryRequestDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If ItemInventoryRequestDetail.Id > 0 Then
                If ListDeleteInventoryRequestDetail Is Nothing Then
                    ListDeleteInventoryRequestDetail = New List(Of InventoryRequestDetail)
                End If
                ItemInventoryRequestDetail.MarkAsDeleted()
                ListDeleteInventoryRequestDetail.Add(ItemInventoryRequestDetail)
            End If
            inventoryRequest.InventoryRequestDetail.Remove(ItemInventoryRequestDetail)

        End If
    End Sub

    ''' <summary>
    '''  elimina el detalle de medicamentos insumos y otros 
    ''' </summary>
    Private Sub DeleteDetailRequestDetailOther()
        Dim entityDelete = DirectCast(INDGvOtherProducts.GetFocusedRow(), InventoryRequestDetailOther)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If entityDelete.Id > 0 Then
                If ListDeleteRequestOtherDetail Is Nothing Then
                    ListDeleteRequestOtherDetail = New List(Of InventoryRequestDetailOther)
                End If
                ItemInventoryRequestDetailOther.MarkAsAdded()
                ListDeleteRequestOtherDetail.Add(entityDelete.MarkAsDeleted())
            End If
            ListRequestOtherDetail.Remove(entityDelete)
            Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.inventoryRequest IsNot Nothing AndAlso Me.inventoryRequest.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
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

#End Region

#Region "Events"

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal del detalle de medicamento,insumos u otros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddRequestDetailOther)
        If e IsNot Nothing Then
            If e.EditMode = False Then 'Si se esta insertando
                If ListRequestOtherDetail Is Nothing Then
                    ListRequestOtherDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)
                End If
                ListRequestOtherDetail.Add(e.RequestDetailOther)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListRequestOtherDetail.Remove(RequestOtherDetail)
                ListRequestOtherDetail.Insert(indexEditRecord, e.RequestDetailOther)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If
        End If
    End Sub

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        Presenter = Nothing
        Model = Nothing
        inventoryRequest = Nothing
        _idOperativeUnit = Nothing
        _prefixSelected = Nothing
        _idCurrentSequence = Nothing
        ItemInventoryRequestDetail = Nothing
        ListDeleteInventoryRequestDetail = Nothing
        OnlyRead = Nothing
        indexEditRecord = Nothing
        banConfirm = Nothing
        banSaveAndConfirm = Nothing
        banAnular = Nothing
        ListRequestType = Nothing
        ListMovementType = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)
        ' Agrega a la rejilla la columna de Acciones
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)

        Dim ListActionsTwo As New List(Of eAcciones)
        ListActionsTwo.Add(eAcciones.Edit)
        ListActionsTwo.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDGvOtherProducts, ListActionsTwo)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRequest, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventoryRequest(Me)
        Presenter.GetSequense()
        InitializeTuples()
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcProducts)
        IndigoGridControl2.RefreshGrid(INDGcOtherProducts)

        INDesbExport.AddRangeColumns("Código Producto", "Cantidad", "Observación")
        INDEsbExportOtherProducts.AddExcelSheets(New ExcelSheet With {
                       .Columns = New List(Of ExcelColumn) From {
                           New ExcelColumn With {.Name = "TIPO", .Comment = "1 - Medicamento" & vbCrLf & "2 -  Insumo" & vbCrLf & "3 - Producto"},
                           New ExcelColumn With {.Name = "CODIGO"},
                           New ExcelColumn With {.Name = "CANTIDAD"},
                           New ExcelColumn With {.Name = "DESCRIPCIÓN"}
                       }
                   })
        'Verificar permisos

        If BarraBotones.PermissionsForm.ContainsKey(128) Then 'Si tiene permiso de ver la sección de productos
            INDlyCgProducts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDlyCgProducts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        If BarraBotones.PermissionsForm.ContainsKey(129) Then 'Si tiene permiso de ver la sección de medicamentos
            INDLyOtherProducts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDLyOtherProducts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se activa, direge el foco al control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequest_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se dispara al cambiar el valor del tipo de solicitud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRequestType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRequestType.EditValueChanged
        If RequestType = 1 Then
            INDlciTargetFunctionalUnitId.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDlciTargetFunctionalUnitId.ShowInCustomizationForm = False

            INDlciMovementType.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDlciMovementType.ShowInCustomizationForm = True
            MovementType = Nothing

            INDlciSourceWarehouseId.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDlciSourceWarehouseId.ShowInCustomizationForm = True
            SourceWarehouseId = Nothing

            INDlciTargetWarehouseId.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDlciTargetWarehouseId.ShowInCustomizationForm = True
            TargetWarehouseId = Nothing

        ElseIf RequestType = 2 Then

            INDlciTargetFunctionalUnitId.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDlciTargetFunctionalUnitId.ShowInCustomizationForm = True
            TargetFunctionalUnitId = Nothing

            INDlciMovementType.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDlciMovementType.ShowInCustomizationForm = False

            INDlciSourceWarehouseId.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDlciSourceWarehouseId.ShowInCustomizationForm = False

            INDlciTargetWarehouseId.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDlciTargetWarehouseId.ShowInCustomizationForm = False

        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del almacen de destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetWarehouseId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetWarehouseId.EditValueChanged
        If SourceWarehouseId IsNot Nothing Then
            If SourceWarehouseId = TargetWarehouseId Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("TargetWarehouse", NAME_MODULE))
                TargetWarehouseId = Nothing
                INDsleTargetWarehouseId.Properties.NullText = String.Empty
                INDsleTargetWarehouseId.Focus()

            End If

            If TargetWarehouseId IsNot Nothing And MovementType IsNot Nothing Then
                If MovementType = 1 Then
                    ListViewRequestParamXpo = New List(Of ViewRequestParamXpo)
                    ListViewRequestParamXpo = Presenter.GetListRequestParamByWarehouseId(TargetWarehouseId)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del almacen de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceWarehouseId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSourceWarehouseId.EditValueChanged
        If Me.INDsleSourceWarehouseId.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvSourceWarehouseId.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvSourceWarehouseId.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.inventoryRequest.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.inventoryRequest.Prefix)
            End If
        End If

        If TargetWarehouseId IsNot Nothing Then
            If SourceWarehouseId = TargetWarehouseId Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("TargetWarehouse", NAME_MODULE))
                SourceWarehouseId = Nothing
                INDsleSourceWarehouseId.Properties.NullText = String.Empty
                INDsleSourceWarehouseId.Focus()
            End If
        End If
    End Sub

    Private Sub INDsleTargetFunctionalUnitId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetFunctionalUnitId.EditValueChanged
        ListViewRequestParamXpo = New List(Of ViewRequestParamXpo)

        If INDsleTargetFunctionalUnitId.EditValue IsNot Nothing Then
            ListViewRequestParamXpo = Presenter.GetListRequestParamByFunctionalUnitId(INDsleTargetFunctionalUnitId.EditValue)
        End If

    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceWarehouseId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSourceWarehouseId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.SourceWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetWarehouseId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetWarehouseId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.TargetWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetFunctionalUnitId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetFunctionalUnitId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.TargetFunctionalUnit()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewInventoryRequest()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetFunctionalUnitId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetFunctionalUnitId.QueryPopUp
        If INDsleTargetFunctionalUnitId.Properties.DataSource Is Nothing Then
            Presenter.TargetFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceWarehouseId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSourceWarehouseId.QueryPopUp
        If INDsleSourceWarehouseId.Properties.DataSource Is Nothing Then
            Presenter.SourceWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetWarehouseId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetWarehouseId.QueryPopUp
        If INDsleTargetWarehouseId.Properties.DataSource Is Nothing Then
            Presenter.TargetWarehouse()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Abre el popup para agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If OnlyRead = False Then
            InstantiatePopup()
        End If
    End Sub

    ''' <summary>
    ''' Abre el popup para agregar medicamentos,insumos y otros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddOtherProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddOtherProducts.Click
        RequestOtherDetail = Nothing
        RequestDetailOtherPopup(False)
    End Sub
#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la solicitud de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub


    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de medicamentos, insumos y otros 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetailRequestDetailOther()
            Case "Remove"
                DeleteDetailRequestDetailOther()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetailRequestDetailOther()
            Case "Remove"
                DeleteDetailRequestDetailOther()
        End Select
    End Sub

#End Region

#Region "PasteToGrid"

    ''' <summary>
    ''' Listado que se envia a procesar
    ''' </summary>
    Private listRows As List(Of List(Of String))

    ''' <summary>
    ''' Listado de errores
    ''' </summary>
    Private listErrors As List(Of String)

    ''' <summary>
    ''' Evento que se dispara al copiar y pegar en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        listErrors = New List(Of String)

        INDGvProducts.ShowLoadingPanel()
        Await PasteToGrid(e.Rows)
        INDGvProducts.HideLoadingPanel()

        If listErrors IsNot Nothing AndAlso listErrors.Count > 0 Then
            Using formulario As New FrmListErrors(listErrors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de CopyPaste/Import de solicitudes
    ''' </summary>
    ''' <returns></returns>
    Private Function PasteToGrid(data As List(Of List(Of String))) As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MInventoryRequest(Me.Tag)
                                             Dim indexInitial As Integer = 0

                                             While indexInitial < data.Count
                                                 If (indexInitial + QuantityRowsSend) >= data.Count Then
                                                     SetRow(indexInitial, data.Count, data)
                                                     indexInitial = data.Count
                                                 Else
                                                     SetRow(indexInitial, indexInitial + QuantityRowsSend, data)
                                                     indexInitial += QuantityRowsSend
                                                 End If

                                                 Dim result = model.SP_CopyPasteAndImportRequests(listRows)
                                                 If result.StateResult = False Then
                                                     listErrors = New List(Of String)({result.Message})
                                                     Exit While
                                                 End If

                                                 If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                                     listErrors.AddRange(result.MessageResult)
                                                 End If

                                                 INDgcProducts.SafeInvoke(Sub(x)
                                                                              If ListInventoryRequestDetail Is Nothing Then
                                                                                  ListInventoryRequestDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetail)
                                                                              End If
                                                                              If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                                                                                  For Each item In result.ObjectEmbbeded
                                                                                      If (From y In ListInventoryRequestDetail Where y.InventoryProductId = item.InventoryProductId Select y).Count > 0 Then
                                                                                          listErrors.Add("El producto " + item.DescriptionProduct + " ya existe en la rejilla")
                                                                                          Continue For
                                                                                      End If
                                                                                      ListInventoryRequestDetail.Add(item)
                                                                                  Next
                                                                              End If
                                                                          End Sub)
                                             End While
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' Metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRow(indexInitial As Integer, indexEnd As Integer, data As List(Of List(Of String)))
        listRows = New List(Of List(Of String))
        Dim objLock As New Object()
        Parallel.For(indexInitial, indexEnd, Sub(x)
                                                 SyncLock objLock
                                                     listRows.Add(data(x))
                                                 End SyncLock
                                             End Sub)
    End Sub

#End Region




#Region "PasteToGridRequestDetailOther"

    ''' <summary>
    ''' Listado que se envia a procesar
    ''' </summary>
    Private listRowsDetailOther As List(Of List(Of String))

    ''' <summary>
    ''' Listado de errores
    ''' </summary>
    Private listErrorsDetailOther As List(Of String)

    ''' <summary>
    ''' Evento que se dispara al copiar y pegar en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl2_PasteToGrid(sender As XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl2.PasteToGrid
        listErrorsDetailOther = New List(Of String)

        INDGvOtherProducts.ShowLoadingPanel()
        Await PasteToGridRequestDetailOther(e.Rows)
        INDGvOtherProducts.HideLoadingPanel()

        If listErrorsDetailOther IsNot Nothing AndAlso listErrorsDetailOther.Count > 0 Then
            Using formulario As New FrmListErrors(listErrorsDetailOther)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Metodo que se encarga de CopyPaste/Import de solicitudes
    ''' </summary>
    ''' <returns></returns>
    Private Function PasteToGridRequestDetailOther(data As List(Of List(Of String))) As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MInventoryRequest(Me.Tag)
                                             Dim indexInitial As Integer = 0

                                             While indexInitial < data.Count
                                                 If (indexInitial + QuantityRowsSend) >= data.Count Then
                                                     SetRowRequestDetailOther(indexInitial, data.Count, data)
                                                     indexInitial = data.Count
                                                 Else
                                                     SetRowRequestDetailOther(indexInitial, indexInitial + QuantityRowsSend, data)
                                                     indexInitial += QuantityRowsSend
                                                 End If

                                                 Dim result = model.SP_CopyPasteAndImportRequestsOtherDetail(listRowsDetailOther)
                                                 If result.StateResult = False Then
                                                     listErrorsDetailOther = New List(Of String)({result.Message})
                                                     Exit While
                                                 End If

                                                 If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                                     listErrorsDetailOther.AddRange(result.MessageResult)
                                                 End If

                                                 INDGcOtherProducts.SafeInvoke(Sub(x)
                                                                                   If ListRequestOtherDetail Is Nothing Then
                                                                                       ListRequestOtherDetail = New Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)
                                                                                   End If

                                                                                   If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                                                                                       For Each item In result.ObjectEmbbeded

                                                                                           'Medicines 
                                                                                           If item.ATCId IsNot Nothing Then
                                                                                               If (From y In ListRequestOtherDetail Where y.ATCId = item.ATCId Select y).Count > 0 Then
                                                                                                   listErrorsDetailOther.Add("El Medicamento " + item.SourceCodeName + " ya existe en la rejilla")
                                                                                                   Continue For
                                                                                               End If
                                                                                           End If

                                                                                           ''Supplies
                                                                                           If item.SupplieId IsNot Nothing Then
                                                                                               If (From y In ListRequestOtherDetail Where y.SupplieId = item.SupplieId Select y).Count > 0 Then
                                                                                                   listErrorsDetailOther.Add("El Insumo " + item.SourceCodeName + " ya existe en la rejilla")
                                                                                                   Continue For
                                                                                               End If
                                                                                           End If

                                                                                           ''Products
                                                                                           If item.InventoryProductId IsNot Nothing Then
                                                                                               If (From y In ListRequestOtherDetail Where y.InventoryProductId = item.InventoryProductId Select y).Count > 0 Then
                                                                                                   listErrorsDetailOther.Add("El Producto " + item.SourceCodeName + " ya existe en la rejilla")
                                                                                                   Continue For
                                                                                               End If
                                                                                           End If

                                                                                           'Se validan parámetros de las solicitudes
                                                                                           If ListViewRequestParamXpo.Any() AndAlso item.ComponentType <> 1 Then
                                                                                               Dim requestByType As New List(Of ViewRequestParamXpo)
                                                                                               If item.ComponentType = 2 Then 'Insumo
                                                                                                   requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 1 AndAlso i.SupplieId.Value = item.SupplieId).ToList()
                                                                                               ElseIf item.ComponentType = 3 Then 'Producto
                                                                                                   requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 2 AndAlso i.ProductId.Value = item.InventoryProductId).ToList()
                                                                                               End If

                                                                                               If Not requestByType.Any() Then
                                                                                                   Mensaje(EeventViewerImages.Advertencia) = String.Format("El componente {0} no se encuentra parametrizado para la unidad funcional", item.SourceCodeName)
                                                                                                   Continue For
                                                                                               End If

                                                                                               If requestByType.Count > 1 Then
                                                                                                   Mensaje(EeventViewerImages.Advertencia) = String.Format("El componente {0} no se encuentra parametrizado más de una vez para la unidad funcional", item.SourceCodeName)
                                                                                                   Continue For
                                                                                               End If

                                                                                               If item.Quantity > requestByType.First().Quantity Then
                                                                                                   Mensaje(EeventViewerImages.Advertencia) = String.Format("Cantidad no autorizada para el producto {0}", item.SourceCodeName)
                                                                                                   Continue For
                                                                                               End If
                                                                                           End If

                                                                                           ListRequestOtherDetail.Add(item)
                                                                                       Next
                                                                                   End If
                                                                               End Sub)
                                             End While
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' Metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRowRequestDetailOther(indexInitial As Integer, indexEnd As Integer, data As List(Of List(Of String)))
        listRowsDetailOther = New List(Of List(Of String))
        Dim objLock As New Object()
        Parallel.For(indexInitial, indexEnd, Sub(x)
                                                 SyncLock objLock
                                                     listRowsDetailOther.Add(data(x))
                                                 End SyncLock
                                             End Sub)
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

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
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = False
        inventoryRequest.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
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
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = False
        inventoryRequest.Status = 1
        If ListInventoryRequestDetail IsNot Nothing Then
            For Each item In ListInventoryRequestDetail
                item.Status = 1
            Next
        End If

        If ListRequestOtherDetail IsNot Nothing Then
            For Each item In ListRequestOtherDetail
                item.Status = 1
            Next
        End If
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, inventoryRequest.Id, 0, inventoryRequest.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        inventoryRequest.Status = 2
        banConfirm = True
        banSaveAndConfirm = False
        banAnular = False
        varImp = 3
        If Not IsValidForRequestParams() Then
            Exit Sub
        End If
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = True
        inventoryRequest.Status = 3
        varImp = 4
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        inventoryRequest.Status = 2
        banConfirm = True
        banSaveAndConfirm = True
        banAnular = False
        If Not IsValidForRequestParams() Then
            Exit Sub
        End If
        If ListInventoryRequestDetail IsNot Nothing Then
            For Each item In ListInventoryRequestDetail
                item.Status = 2
            Next
        End If

        If ListRequestOtherDetail IsNot Nothing Then
            For Each item In ListRequestOtherDetail
                item.Status = 2
            Next
        End If
        varImp = 3
        Guardar()
    End Sub

#End Region

End Class