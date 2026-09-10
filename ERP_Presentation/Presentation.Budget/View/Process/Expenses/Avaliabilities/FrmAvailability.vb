'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jeisson Herrera Peña
' Created          : 04-09-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmAvailability
    Implements IAvailability

#Region "Builder"

    Public ctrTmp As CtrInfoEntity

    ''' <summary>
    ''' Construct
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        InitializeComponent()
        ' This call is required by the designer.
        ctrTmp = New CtrInfoEntity()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(_budgetEntityId, _budgetEntityCodeName, _validityId, _validityCodeName, _validityStatusText)
    End Function

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

    Dim meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PAvailability

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Representa a la entidad de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _availability As Availability

    ''' <summary>
    ''' Objeto que contiene la dependencia
    ''' </summary>
    Dim _dependency As Dependency

    ''' <summary>
    ''' Lista los detalles de la disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAvailabilityDetail As List(Of AvailabilityDetail)

    ''' <summary>
    ''' Lista los detalles eliminados de la disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteAvailabilityDetail As List(Of AvailabilityDetail)

    ''' <summary>
    ''' Listado de nuevos de presupuesto inicial que sera enviado
    ''' al form popup que se despliega
    ''' </summary>
    ''' <remarks></remarks>
    Public _listNewBudget As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#Region "Validity"

    ''' <summary>
    ''' Establece el id de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _budgetEntityId As Integer

    ''' <summary>
    ''' Establece el codigo y nombre de la entidad presupuestal tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _budgetEntityCodeName As String

    ''' <summary>
    ''' Establece el id de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityId As Integer

    ''' <summary>
    ''' Establece el codigo y nombre de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityCodeName As String

    ''' <summary>
    ''' Representa al año
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityYear As Integer

    ''' <summary>
    ''' Representa al mes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityMonth As Integer

    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityStatus As String

    ''' <summary>
    ''' Texto Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityStatusText As String

    ''' <summary>
    ''' Controla el changed de los search
    ''' </summary>
    ''' <remarks></remarks>
    Dim _controlerChanged As Boolean

#End Region

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IAvailability.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAvailability.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As BudgetSequence Implements IAvailability.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As BudgetSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequense.BudgetSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IAvailability.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdPopup As Integer? Implements IAvailability.BudgetEntityIdPopup
        Get
            Return INDsleEntityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer Implements IAvailability.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdPopup As Integer? Implements IAvailability.ValidityIdPopup
        Get
            Return INDsleValidityPopUp.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidityPopUp.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IAvailability.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvailabilityDate As Date Implements IAvailability.AvailabilityDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvailabilityType As Integer Implements IAvailability.AvailabilityType
        Get
            Return INDGleAvailabilityType.EditValue
        End Get
        Set(value As Integer)
            INDGleAvailabilityType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los días de vencimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpirationDays As Integer Implements IAvailability.ExpirationDays
        Get
            Return INDSeTermDays.EditValue
        End Get
        Set(value As Integer)
            INDSeTermDays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la dependencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DependencyId As Integer Implements IAvailability.DependencyId
        Get
            Return INDSleDependency.EditValue
        End Get
        Set(value As Integer)
            INDSleDependency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IAvailability.Observations
        Get
            Return INDMemObservation.EditValue
        End Get
        Set(value As String)
            INDMemObservation.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpo As XPInstantFeedbackSource Implements IAvailability.BudgetEntityXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpoPopup As XPInstantFeedbackSource Implements IAvailability.BudgetEntityXpoPopup
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpo As XPCollection Implements IAvailability.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpoPopup As XPCollection Implements IAvailability.ValidityXpoPopup
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los tipos de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingAvailabilityType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAvailabilityType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAvailabilityType Is Nothing Then
                _FillingAvailabilityType = New List(Of Tuple(Of Integer, String))
                _FillingAvailabilityType.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
                _FillingAvailabilityType.Add(New Tuple(Of Integer, String)(2, "Disponibilidad"))
                _FillingAvailabilityType.Add(New Tuple(Of Integer, String)(3, "Vigencia Futura"))
            End If
            Return _FillingAvailabilityType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece las dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDependency As XPInstantFeedbackSource Implements IAvailability.ListDependency
        Get
            Return INDSleDependency.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDependency.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la lista de CPCCatalog activos, sin hijos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListCPCCatalog As XPCollection Implements IAvailability.ListCPCCatalog
        Get
            Return RepositoryItemSleCPC.DataSource
        End Get
        Set(value As XPCollection)
            RepositoryItemSleCPC.DataSource = value
        End Set
    End Property
#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' Búsqueda de registros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Undo(False)
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Undo(Optional withBudgetEntity As Boolean = True)
        Await CleanControls(withBudgetEntity)
        If withBudgetEntity Then
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Else
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina la entidad(No se esta usando)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Metodo Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If _availability.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim errors As String = ValidateDetail()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MAvailability(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAvailability(_availability, _idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult Then
                    If result.ObjectEmbbeded.Status = 1 Then 'Guardar o Actualizar
                        If _availability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                                Me.DicSequense(Me._sequense.BudgetSequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequense.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _availability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    ElseIf result.ObjectEmbbeded.Status = 2 Then 'Confirmar
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), result.ObjectEmbbeded.Code)
                    ElseIf result.ObjectEmbbeded.Status = 3 Then 'Anular
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    End If
                    Me._availability = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _availability.Id, 0, _availability.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _availability.Id, 0, _availability.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _availability.Id, 0, _availability.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _availability.Id, 0, _availability.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If _availability.Id > 0 Then
                        Dim resullt2 = Await model.GetAvailability(Code, 2, _validityId)
                        _availability = resullt2.ObjectEmbbeded
                    Else
                        _availability = New Availability
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Metodo no utilizado
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad que establece los mensajes 
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
    ''' Metodo Cuando se da click en boton nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Me.Deshacer()
        Else
            NewAvaibility()
        End If
    End Sub

    ''' <summary>
    ''' Método para abrir búsqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo With {.Caption = "Tipo Disponibilidad", .FieldName = "AvailabilityTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo With {.Caption = "Valor Inicial", .FieldName = "InitialValue", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2), .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Valor Total", .FieldName = "TotalAvailability", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2), .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {_validityId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAvailability
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de búsqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDBtnCode.Text = ReturnValue
        If INDBtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Activa o Inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAvailability.ActionsOnControls
        Set(value As Boolean)
            INDLcAvailability.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDGleAvailabilityType.Enabled = value
            INDSeTermDays.Enabled = value
            INDSleDependency.Enabled = value
            INDMemObservation.Enabled = value
            INDTxeValue.Enabled = value
            INDSbAdd.Enabled = value
            INDGcCategory.Enabled = value
            INDLcAvailability.EndUpdate()
            If value Then
                INDDteDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function CleanControls(Optional withBudgetaryEntity As Boolean = True) As Task
        INDLcAvailability.BeginUpdate()
        Await DeleteBlockedRecord()

        INDBtnCode.Text = String.Empty
        INDDteDocumentDate.EditValue = GetDateServer()
        INDSeTermDays.EditValue = 0
        INDGleAvailabilityType.EditValue = 2
        INDTxeTermDate.Text = String.Empty
        INDSleDependency.EditValue = Nothing
        INDSleDependency.DisplayNullText = String.Empty
        INDMemObservation.EditValue = String.Empty
        INDTxeValue.Text = String.Empty
        INDGcCategory.DataSource = Nothing

        _doc = Nothing
        _availability = Nothing
        _listAvailabilityDetail = Nothing
        _listDeleteAvailabilityDetail = Nothing
        _listNewBudget = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.ControlHideStatus = False
        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If withBudgetaryEntity = True Then
            BudgetaryValidityId = Nothing
            ValidityIdPopup = Nothing
            _validityId = Nothing
            _validityYear = Nothing
            _validityMonth = Nothing
            _validityStatus = Nothing
            _validityStatusText = Nothing
            INDsleEntityPopUp.EditValue = Nothing
            INDsleValidityPopUp.EditValue = Nothing

            LoadLayoutGroup(False)
        End If

        INDLcAvailability.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="codes">The code.</param>
    Private Sub PrepareToolbar(codes As String)
        Code = codes
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = "1"
        Me.BarraBotones.ControlHideStatus = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup(ByVal Bandera As Boolean)
        If Bandera Then
            INDLcgPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If BudgetEntityXpoPopup Is Nothing Then
                _presenter.InitializeBudgetEntityPopup()
            End If
            INDsleEntityPopUp.EditValue = BudgetEntityId
            INDsleValidityPopUp.EditValue = BudgetaryValidityId

            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.ControlHideStatus = False
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Else
            INDLcgPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.BarraBotones.StatusRecordVisible = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 Then
            Dim item = (From l In ValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Buscar dependencia por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Private Async Function SearchDependency(ByVal code As String) As Task(Of Domain.Entities.Dependency)
        Using ModelDepedency As New MBudgetDependency(MBudgetDependency.TAG)
            _dependency = Await ModelDepedency.GetBudgetDependencyAsync(code, _validityId)
            If _dependency IsNot Nothing AndAlso _dependency.Id > 0 Then
                INDMemObservation.Focus()
            Else
                Me.INDSleDependency.DisplayNullText = String.Empty
                Me.INDSleDependency.EditValue = Nothing
                Me.INDSleDependency.DisplayMember = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "La Dependencia No Existe"
                INDSleDependency.Focus()
            End If
            Return _dependency
        End Using
    End Function

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatus()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 AndAlso BudgetaryValidityId <> 0 Then
            Dim item = (From l In ValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
            If item IsNot Nothing Then
                _validityStatus = item.Status
                Select Case item.Status
                    Case 1
                        _validityStatusText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        _validityStatusText = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        _validityStatusText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        _validityStatusText = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatusPopup()
        If ValidityXpoPopup IsNot Nothing AndAlso ValidityXpoPopup.Count > 0 AndAlso ValidityIdPopup IsNot Nothing Then
            Dim item = (From l In ValidityXpoPopup Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            If item IsNot Nothing Then
                _validityStatus = item.Status
                Select Case item.Status
                    Case 1
                        _validityStatusText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        _validityStatusText = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        _validityStatusText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        _validityStatusText = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega un rubro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCategory()
        Dim frmDetail As New FrmPopupAvailability()
        frmDetail.Size = New System.Drawing.Size(800, 730)
        frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        frmDetail.ValidatyId = _validityId
        Dim frmTransparent As New FrmTransparent(frmDetail, False)
        If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            If Not (_listAvailabilityDetail IsNot Nothing AndAlso _listAvailabilityDetail.Count > 0) Then
                _listAvailabilityDetail = New List(Of AvailabilityDetail)
            Else
                Dim listErrors As New StringBuilder
                For Each item As Domain.Entities.AvailabilityDetail In frmDetail.ListAvailabilityDetail
                    Dim cont = (From l In _listAvailabilityDetail Where l.CategoryId = item.CategoryId And l.RevenueTypeId = item.RevenueTypeId AndAlso l.CPCCodeId.GetValueOrDefault() = item.CategoryCPCCodeId.GetValueOrDefault()).Count
                    If cont > 0 Then
                        listErrors.AppendLine("No se puede agregar el rubro " + item.CodeNameCategory + " con el tipo de ingreso " + item.CodeNameRevenueType + " porque ya existe en la lista del formulario principal.")
                    End If
                Next
                If listErrors.ToString.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                    Exit Sub
                End If
            End If
            _listAvailabilityDetail.AddRange(frmDetail.ListAvailabilityDetail)
            INDGcCategory.DataSource = _listAvailabilityDetail
            INDGcCategory.RefreshDataSource()
            viewBudget.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MAvailability(MyTag)
            AsyncLoader(True)
            INDLcAvailability.BeginUpdate()
            Dim resultOperation = Await Model.GetAvailability(Code, 2, _validityId)
            _availability = resultOperation.ObjectEmbbeded
            If Not _availability Is Nothing AndAlso _availability.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(MyTag, _availability.Id)
                INDsleEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With _availability
                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                    Code = .Code
                    AvailabilityDate = .DocumentDate
                    AvailabilityType = .AvailabilityType
                    ExpirationDays = .ExpirationDays
                    INDTxeTermDate.Text = .ExpirationDate.Day.ToString & " de " & meses(.ExpirationDate.Month - 1) & " de " & .ExpirationDate.Year.ToString
                    DependencyId = .DependencyId
                    INDSleDependency.DisplayNullText = .NameDependency
                    Observations = .Observations
                    _listAvailabilityDetail = .AvailabilityDetail.ToList

                    INDGcCategory.DataSource = Nothing
                    INDGcCategory.DataSource = _listAvailabilityDetail

                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._availability.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _availability.Id}
                    Dim operation = Await Model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    _blockRecord = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If _availability.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(_availability.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _availability.Id, 0, _availability.Id)

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewAvaibility()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Me.Deshacer()
                    INDBtnCode.Focus()
                End If
            End If
        End Using
        INDLcAvailability.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva disponibilidad
    ''' </summary>
    Private Async Sub NewAvaibility()
        If INDsleBudgetEntity.EditValue IsNot Nothing AndAlso CType(INDsleBudgetEntity.EditValue, String) <> "" Then
            _availability = New Availability With {.Status = 1}
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.StatusRecord = "1"
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Sub
                    End If
                End If
                If Not Me._sequense.Sequential Then
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                        Else
                            Using model As New ModelBaseBudget(Me.Tag)
                                Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
                            End Using
                            If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                            Else
                                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                    End If
                Else
                    PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                End If
            End If
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SeleccioneEntidadPresupuestal", NAME_MODULE)
            INDsleBudgetEntity.Focus()
        End If
    End Sub

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _availability.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & _availability.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _availability.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _availability.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _availability.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MAvailability(MyTag)
                Await Model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._availability IsNot Nothing AndAlso Me._availability.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If _availability.Status > 1 Then
            If _availability.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta anulado"
            End If
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim detail As AvailabilityDetail = viewBudget.GetFocusedRow
        If detail.Id > 0 Then
            If _listDeleteAvailabilityDetail Is Nothing Then
                _listDeleteAvailabilityDetail = New List(Of AvailabilityDetail)
            End If
            detail.MarkAsDeleted()
            _listDeleteAvailabilityDetail.Add(detail)
        End If
        _listAvailabilityDetail.Remove(detail)
        INDGcCategory.DataSource = Nothing
        INDGcCategory.DataSource = _listAvailabilityDetail
    End Sub

    ''' <summary>
    ''' Valida el detalle del reconocimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As String
        Dim listErrors As New StringBuilder
        If Not (_listAvailabilityDetail IsNot Nothing AndAlso _listAvailabilityDetail.Count > 0) Then
            listErrors.AppendLine("No hay detalles de Disponibilidad para poder guardar.")
        End If

        If _validityId = 0 Then
            listErrors.AppendLine("Debe elegir una vigencia.")
        ElseIf Not {1, 2}.Contains(_validityStatus) Then
            listErrors.AppendLine(String.Format("La vigencia se encuentra en estado {0}.", _validityStatusText))
        End If

        'Se valida que el mes y el año de la fecha del documento sea igual al mes y año de la vigencia
        If Year(AvailabilityDate) <> CInt(_validityYear) Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectYear", NAME_MODULE), _validityYear.ToString))
        End If
        If Month(AvailabilityDate) <> Me._validityMonth Then
            listErrors.AppendLine(String.Format(ResourceManager.GetString("SelectMonth", NAME_MODULE), MonthName(Me._validityMonth, False)))
        End If

        If _listAvailabilityDetail IsNot Nothing AndAlso _listAvailabilityDetail.Count > 0 Then
            For Each item In _listAvailabilityDetail
                If item.InitialValue = 0 Then
                    listErrors.AppendLine("El valor de cada uno de los detalles de la Disponibilidad no puede ser cero (0).")
                    viewBudget.Focus()
                    Exit For
                End If
                If item.ValueBalance < item.InitialValue Then
                    listErrors.AppendLine("El valor no puede ser menor al saldo de presupuesto en ninguno de los detalles de la Disponibilidad.")
                    viewBudget.Focus()
                    Exit For
                End If
                If item.InitialValue < 0 Then
                    viewBudget.Focus()
                    listErrors.AppendLine("El valor de ninguno de los detalles de la Disponibilidad puede ser inferior a cero (0).")
                End If
                If item.CCPETLinkAccount AndAlso item.CPCCodeId Is Nothing Then
                    viewBudget.Focus()
                    listErrors.AppendLine(String.Format("Debe seleccionar CPC para el rubro {0}", item.CodeNameCategory))
                End If
            Next
        End If

        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Metodos para asignar valores a la entidad Availability
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _availability
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .BudgetaryValidityId = _validityId
            .Code = Code
            .DocumentDate = AvailabilityDate
            .AvailabilityType = AvailabilityType
            .ExpirationDays = ExpirationDays
            .ExpirationDate = DateAdd(DateInterval.Day, ExpirationDays, AvailabilityDate)
            .DependencyId = DependencyId
            .Observations = Observations
            If _listAvailabilityDetail IsNot Nothing AndAlso _listAvailabilityDetail.Count > 0 Then
                _listAvailabilityDetail.ForEach(Sub(item)
                                                    item.TotalAvailability = item.InitialValue - item.DebitModificationValue + item.CreditModificationValue
                                                    item.Balance = item.TotalAvailability - item.ExecutedValue
                                                    .AvailabilityDetail.Add(item)
                                                End Sub)
            End If
            If _listDeleteAvailabilityDetail IsNot Nothing AndAlso _listDeleteAvailabilityDetail.Count > 0 Then
                _listDeleteAvailabilityDetail.ForEach(Sub(item)
                                                          .AvailabilityDetail.Add(item)
                                                      End Sub)
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAvailabilitiy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcAvailability, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PAvailability(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        _presenter.ListCPCCatalogActiveWithoutChildren()
        '****************************** 
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.INDSleDependency.FuncQueryOnKeyEnterPressed = AddressOf Me.SearchDependency
        Me.INDSleDependency.View.OptionsView.ShowGroupPanel = False
        IndigoGridControl1.RefreshGrid(INDGcCategory)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewBudget, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewBudget.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        Me.Undo()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing

        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _availability = Nothing
        _dependency = Nothing
        _listAvailabilityDetail = Nothing
        _listDeleteAvailabilityDetail = Nothing
        _listNewBudget = Nothing
        varImp = Nothing

        _budgetEntityId = Nothing
        _budgetEntityCodeName = Nothing
        _validityId = Nothing
        _validityCodeName = Nothing
        _validityYear = Nothing
        _validityMonth = Nothing
        _validityStatus = Nothing
        _validityStatusText = Nothing
        _controlerChanged = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAvailabilitiy_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleAvailabilityType.Properties.DataSource = FillingAvailabilityType
        Me.INDGleAvailabilityType.EditValue = 2
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "FrmClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAvailabilitiy_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _validityId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDBtnCode.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDBtnCode.Text.Trim()) Then
                    Me.NewAvaibility()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al asignar los días de plazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSeTermDays_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSeTermDays.KeyDown
        If e.KeyCode = 13 OrElse e.KeyCode = 9 Then
            Dim termDate As Date = DateAdd(DateInterval.Day, ExpirationDays, AvailabilityDate)
            INDTxeTermDate.Text = termDate.Day.ToString & " de " & meses(termDate.Month - 1) & " de " & termDate.Year.ToString
            INDSleDependency.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntityXpo Is Nothing Then
            _presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal del popup del control de info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntityPopUp.QueryPopUp
        If BudgetEntityXpoPopup Is Nothing Then
            _presenter.InitializeBudgetEntityPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de dependenciasS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDependency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDependency.QueryPopUp
        If INDSleDependency.Datasource Is Nothing Then
            _presenter.LoadListDependency(_validityId)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemSleCPC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemSleCPC.QueryPopUp
        Dim _availabilityDetail = TryCast(viewBudget.GetFocusedRow(), AvailabilityDetail)
        If _availabilityDetail IsNot Nothing Then
            If Not _availabilityDetail.CCPETLinkAccount Then
                e.Cancel = True
            ElseIf _availabilityDetail.CategoryCPCCodeId.GetValueOrDefault() > 0 Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        AddCategory()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            _presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de entidades presupuestales del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            _presenter.InitializeBudgetEntity()
        End If
    End Sub

    Private Sub INDSleDependency_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSleDependency.OpenFormButtonClick
        OpenForm(204, Nothing, True)
        _presenter.LoadListDependency(_validityId)
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntityId <> 0 Then
            _budgetEntityId = BudgetEntityId
            _budgetEntityCodeName = INDsleBudgetEntity.Text

            BudgetaryValidityId = Nothing
            ValidityXpo = Nothing
            _presenter.InitializeValidity(BudgetEntityId)
            SetFirstOrDefaultValidity()

            _presenter.InitializeBudgetEntityPopup()
            _controlerChanged = True
            BudgetEntityIdPopup = BudgetEntityId
            _controlerChanged = False
            _presenter.InitializeValidityPopup(BudgetEntityIdPopup)

            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de entidad presupuestal del info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If BudgetEntityIdPopup IsNot Nothing AndAlso _controlerChanged = False Then
            _budgetEntityId = BudgetEntityIdPopup
            _budgetEntityCodeName = INDsleEntityPopUp.Text

            _validityId = 0
            _validityCodeName = String.Empty
            _validityStatus = 0
            _validityStatusText = String.Empty
            ValidityIdPopup = Nothing
            ValidityXpoPopup = Nothing
            _presenter.InitializeValidityPopup(BudgetEntityIdPopup)
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> 0 Then
            _validityId = BudgetaryValidityId
            _validityCodeName = INDsleValidity.Text

            _controlerChanged = True
            ValidityIdPopup = BudgetaryValidityId
            _controlerChanged = False

            SetStatus()
            ctrTmp.RefreshInfo()
            LoadLayoutGroup(True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de vigencia del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If ValidityIdPopup IsNot Nothing AndAlso _controlerChanged = False Then
            _validityId = ValidityIdPopup
            _validityCodeName = INDsleValidityPopUp.Text

            Dim item = (From l In ValidityXpoPopup Where l.Id = ValidityIdPopup Select l).FirstOrDefault
            _validityYear = item.Year
            _validityMonth = item.ExpenseMonth

            SetStatusPopup()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de dependencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleDependency_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDSleDependency.EditValueChanged
        If Me.INDSleDependency.EditValue IsNot Nothing AndAlso Me.INDSleDependency.EditValue > 0 Then
            Using ModelDependency As New MBudgetDependency(MBudgetDependency.TAG)
                Me._dependency = Await ModelDependency.GetBudgetDependencyById(Me.INDSleDependency.EditValue)
            End Using
        End If
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Suma de rubros para el valor total de la disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewBudget_DataSourceChanged(sender As Object, e As EventArgs) Handles viewBudget.DataSourceChanged
        INDTxeValue.Text = _listAvailabilityDetail.Sum(Function(x) x.InitialValue)
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que se dispara al desplegar con click derecho el menu sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "CellValueChanged"

    ''' <summary>
    ''' Suma de rubros para el valor total de la disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewBudget_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles viewBudget.CellValueChanged
        If e.Column.Name = "INDValue" Then
            INDTxeValue.Text = _listAvailabilityDetail.Sum(Function(x) x.InitialValue)
        End If
    End Sub

#End Region

#Region "QueyDown Grid"

    Private Sub viewBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles viewBudget.KeyDown
        Dim k As Integer = e.KeyCode
        Select Case k
            Case 9
                e.SuppressKeyPress = True
            Case 13
                If viewBudget.IsLastRow Then
                    viewBudget.MoveFirst()
                Else
                    viewBudget.MoveNext()
                End If
        End Select
    End Sub

#End Region

#Region "ValidatingEditor"

    Private Sub viewBudget_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles viewBudget.ValidatingEditor

        Dim view As GridView = sender
        Dim row As AvailabilityDetail = (viewBudget.GetRow(viewBudget.FocusedRowHandle))
        Dim saldo As Decimal

        If row IsNot Nothing Then
            saldo = row.ValueBalance
        End If

        If view.FocusedColumn.FieldName = "InitialValue" Then

            Dim valor As Decimal = Convert.ToDecimal(e.Value)

            Select Case valor
                Case Is < 0
                    e.Valid = False
                Case Is = 0
                    e.Valid = False
                Case Is > saldo
                    e.Valid = False
            End Select

        End If
    End Sub

#End Region

#Region "InvalidValueException"

    Private Sub viewBudget_InvalidValueException(sender As Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles viewBudget.InvalidValueException

        Dim view As GridView = sender
        Dim row As AvailabilityDetail = (viewBudget.GetRow(viewBudget.FocusedRowHandle))
        Dim saldo As Decimal

        If row IsNot Nothing Then
            saldo = row.ValueBalance
        End If

        If view.FocusedColumn.FieldName = "InitialValue" Then

            Dim valor As Decimal = Convert.ToDecimal(e.Value)

            Select Case valor

                Case Is < 0
                    e.ExceptionMode = ExceptionMode.DisplayError
                    e.WindowCaption = "Error en el valor ingresado"
                    e.ErrorText = "El valor no puede ser inferior al saldo del presupuesto."
                Case Is = 0
                    e.ExceptionMode = ExceptionMode.DisplayError
                    e.WindowCaption = "Error en el valor ingresado"
                    e.ErrorText = "El valor no puede ser igual a cero (0)."
                Case Is > saldo
                    e.ExceptionMode = ExceptionMode.DisplayError
                    e.WindowCaption = "Error en el valor ingresado"
                    e.ErrorText = "El valor no puede ser mayor al saldo del presupuesto."

            End Select

        End If
    End Sub

#End Region

#End Region

#Region "Buttons Bar"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        Me.Undo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _availability.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento guardarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _availability.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _availability.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento actualizarConfirmar de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _availability.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Evento anular de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _availability.Status = 3
            varImp = 4
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()
                PopUpAnnulmentReason.BudgetaryValidityId = _validityId

                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    _availability.AnnulmentConceptId = PopUpAnnulmentReason.ReversalReasonId
                    _availability.AnnulmentDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _availability.Id, 0, _availability.Id)
    End Sub

    ''' <summary>
    ''' Cambiar unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

#Region "CustomRowCellEdit"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBudget_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles viewBudget.CustomRowCellEdit
        If e.Column.FieldName = "CPCCodeId" Then
            Dim _availabilityDetail = TryCast(viewBudget.GetFocusedRow(), AvailabilityDetail)
            If _availabilityDetail IsNot Nothing Then
                If _availabilityDetail.CCPETLinkAccount Then
                    e.RepositoryItem = RepositoryItemSleCPC
                Else
                    e.RepositoryItem = Nothing
                End If
            End If
        End If
    End Sub
#End Region
End Class