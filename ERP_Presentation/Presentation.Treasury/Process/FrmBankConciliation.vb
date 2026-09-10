#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Treasury.MVP

#End Region

Public Class FrmBankConciliation
    Implements IBankConciliation

#Region "Consts"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private _presenter As PBankConciliation

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As Domain.Entities.BlockRecordTreasury

    ''' <summary>
    ''' Variable que contiene la entidad de la conciliación bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bankReconciliation As BankReconciliation

    ''' <summary>
    ''' Listado del detalle de la conciliación bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBankReconciliationDetail As List(Of BankReconciliationDetail)

    ''' <summary>
    ''' representa la entidad del detalle del extracto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bankReconciliationExtractDetail As BankReconciliationExtractDetail

    ''' <summary>
    ''' Listado del detalle del extracto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBankReconciliationExtractDetail As List(Of BankReconciliationExtractDetail)

    ''' <summary>
    ''' Lista de detalles del extracto de eliminados de la conciliación bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteBankReconciliationExtractDetail As List(Of BankReconciliationExtractDetail)

    ''' <summary>
    ''' bandera para identificar cuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim _isLoad As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IBankConciliation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBankConciliation.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As TreasurySequence Implements IBankConciliation.Sequence
        Get
            Return Me._sequense
        End Get
        Set(value As TreasurySequence)
            _sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequense.TreasurySequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IBankConciliation.Code
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
    ''' Id de la cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountId As Integer? Implements IBankConciliation.EntityBankAccountId
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date? Implements IBankConciliation.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final libros
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountValue As Decimal Implements IBankConciliation.EntityBankAccountValue
        Get
            Return INDseEndEntityBankAccountValue.EditValue
        End Get
        Set(value As Decimal)
            INDseEndEntityBankAccountValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final extracto
    ''' </summary>
    ''' <returns></returns>
    Public Property ExtractValue As Decimal Implements IBankConciliation.ExtractValue
        Get
            Return INDseExtractValue.EditValue
        End Get
        Set(value As Decimal)
            INDseExtractValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Diferencia a conciliar
    ''' </summary>
    ''' <returns></returns>
    Public Property DifferenceReconcile As Decimal Implements IBankConciliation.DifferenceReconcile
        Get
            Return INDseDifferenceReconcile.EditValue
        End Get
        Set(value As Decimal)
            INDseDifferenceReconcile.EditValue = value
        End Set
    End Property

#Region "Extract Details"

    ''' <summary>
    ''' Tipo de Documento del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentType As Byte? Implements IBankConciliation.ExtractDocumentType
        Get
            Return INDSleExtractDocumentType.EditValue
        End Get
        Set(value As Byte?)
            INDSleExtractDocumentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentDate As Date? Implements IBankConciliation.ExtractDocumentDate
        Get
            Return INDdeExtractDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeExtractDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDescription As String Implements IBankConciliation.ExtractDescription
        Get
            Return INDtxtExtractDetail.EditValue
        End Get
        Set(value As String)
            INDtxtExtractDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentNumber As String Implements IBankConciliation.ExtractDocumentNumber
        Get
            Return INDtxtExtractDocument.EditValue
        End Get
        Set(value As String)
            INDtxtExtractDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Naturaleza del detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExtractNature As Byte? Implements IBankConciliation.ExtractNature
        Get
            Return INDSleExtractNature.EditValue
        End Get
        Set(value As Byte?)
            INDSleExtractNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del detalle
    ''' </summary>
    ''' <returns></returns>
    Property ExtractDocumentValue As Decimal Implements IBankConciliation.ExtractDocumentValue
        Get
            Return INDtxtExtractDocumentValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtExtractDocumentValue.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountXpo As XPInstantFeedbackSource Implements IBankConciliation.EntityBankAccountXpo
        Get
            Return INDsleEntityBankAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los tipos de documentos
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingDocumentType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingDocumentType As List(Of Tuple(Of Byte, String)) Implements IBankConciliation.FillingDocumentType
        Get
            If _FillingDocumentType Is Nothing Then
                _FillingDocumentType = New List(Of Tuple(Of Byte, String))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(1, "Recibo de caja"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(2, "Comprobante de egreso"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(3, "Nota"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(4, "Consignación"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(5, "Fondo de Caja Menor"))
            End If
            Return _FillingDocumentType
        End Get
    End Property

    ''' <summary>
    ''' Establece las naturalezas a manejar
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingNature As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingNature As List(Of Tuple(Of Byte, String)) Implements IBankConciliation.FillingNature
        Get
            If _FillingNature Is Nothing Then
                _FillingNature = New List(Of Tuple(Of Byte, String))
                _FillingNature.Add(New Tuple(Of Byte, String)(1, "Débito"))
                _FillingNature.Add(New Tuple(Of Byte, String)(2, "Crédito"))
            End If
            Return _FillingNature
        End Get
    End Property

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios que se hayan hecho al form
    ''' </summary>
    Public Async Sub Deshacer() Implements ICrudBase.Deshacer
        Await CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() OrElse Not ValidateDetail() Then
            Exit Sub
        End If
        Try
            AsyncLoader(True)
            Await AssigningValues()
            Using Model As New MBankConciliation(Me.Tag.ToString())
                Dim result As ActionResult(Of BankReconciliation) = Await Model.SaveBankReconciliation(Me._bankReconciliation, Me._idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If _bankReconciliation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If

                    Me._bankReconciliation = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Slide de mensajes
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Banco", .FieldName = "EntityBankAccountId.CodeBankAccount", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBankReconciliation
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = CByte(1), .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(2), .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(3), .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IBankConciliation.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleEntityBankAccount.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDseEndEntityBankAccountValue.Enabled = value
            INDseExtractValue.Enabled = value
            INDseDifferenceReconcile.Enabled = value
            INDgcBankReconciliationDetails.Enabled = value
            INDPceAddExtractDetail.Enabled = value
            INDgcBankReconciliationExtractDetails.Enabled = value
            INDlyRoot.EndUpdate()
            If value Then
                INDsleEntityBankAccount.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Calcula el valor del saldo anterior del banco
    ''' </summary>
    Private Sub CalculateLastBalance()
        If EntityBankAccountId IsNot Nothing AndAlso DocumentDate IsNot Nothing Then
            EntityBankAccountValue = _presenter.CalculateBalance(EntityBankAccountId, DocumentDate)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la operacion para saber la diferencia a conciliar
    ''' </summary>
    Private Sub CalculateDifference()
        Dim DetailUncheckedDebitValue As Decimal = 0
        Dim DetailUncheckedCreditValue As Decimal = 0
        Dim ExtractDetailDebitValue As Decimal = 0
        Dim ExtractDetailCreditValue As Decimal = 0

        If _listBankReconciliationDetail IsNot Nothing Then
            DetailUncheckedDebitValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
            DetailUncheckedCreditValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
        End If

        If _listBankReconciliationExtractDetail IsNot Nothing Then
            ExtractDetailDebitValue = _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
            ExtractDetailCreditValue = _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
        End If

        DifferenceReconcile = (EntityBankAccountValue - DetailUncheckedDebitValue + DetailUncheckedCreditValue) - (ExtractValue + ExtractDetailDebitValue - ExtractDetailCreditValue)
    End Sub

    ''' <summary>
    ''' Cambia el formato dde los componentes segun la abreviacion de la moneda de la cuenta bancaria
    ''' </summary>
    Private Sub SetFormatCurrencyUI(currencyAbbreviation As String)
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La Abreviación viene Vacia"
            currencyAbbreviation = SessionValues.Instance.CurrencyISO4217
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = currencyAbbreviation.GetNumberFormat
        Me.INDtxtExtractDocumentValue.Properties.Mask.Culture = _culture
        Me.INDseEndEntityBankAccountValue.Properties.Mask.Culture = _culture
        Me.INDseExtractValue.Properties.Mask.Culture = _culture
        Me.INDseDifferenceReconcile.Properties.Mask.Culture = _culture
        INDcolBankReconciliationDetail_Value = Window.Utils.FormatGrid(INDcolBankReconciliationDetail_Value, currencyAbbreviation)
        INDcolBankReconciliationExtractDetail_Value = Window.Utils.FormatGrid(INDcolBankReconciliationExtractDetail_Value, currencyAbbreviation)
    End Sub

#Region "Extract Details"

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        _bankReconciliationExtractDetail = Nothing

        ExtractDocumentType = Nothing
        ExtractDocumentDate = Nothing
        ExtractDescription = Nothing
        ExtractDocumentNumber = Nothing
        ExtractNature = Nothing
        ExtractDocumentValue = Nothing
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        Dim listErrors As New StringBuilder

        If ExtractDocumentType Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un tipo de documento")
        End If

        If ExtractDocumentDate Is Nothing Then
            listErrors.AppendLine("Debe indicar la fecha del registro")
        Else
            If ExtractDocumentDate.AsDate > DocumentDate.AsDate Then
                listErrors.AppendLine("La fecha del registro no puede ser mayor a la fecha del documento")
            End If
        End If

        If String.IsNullOrEmpty(ExtractDescription) Then
            listErrors.AppendLine("Debe indicar una descripción del registro")
        End If

        If ExtractNature Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una naturaleza")
        End If

        If Not (ExtractDocumentValue > 0) Then
            listErrors.AppendLine("Debe ingresar un valor válido del registro")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Agrega el detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddExtractDetail()
        If EntityBankAccountId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un formato."
            Exit Sub
        End If

        If ValidateControlsPopup() = True Then
            AssigningValuesDetail()

            If _listBankReconciliationExtractDetail Is Nothing Then
                _listBankReconciliationExtractDetail = New List(Of BankReconciliationExtractDetail)
            End If
            _listBankReconciliationExtractDetail.Add(_bankReconciliationExtractDetail)
            If _bankReconciliationExtractDetail.Id > 0 Then
                _bankReconciliationExtractDetail.MarkAsModified()
            End If
            INDgcBankReconciliationExtractDetails.DataSource = Nothing
            INDgcBankReconciliationExtractDetails.DataSource = _listBankReconciliationExtractDetail

            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado con éxito"
            CleanControlsPopup()
            CalculateDifference()

            INDSleExtractDocumentType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Crea el objeto para el listado de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValuesDetail()
        _bankReconciliationExtractDetail = New BankReconciliationExtractDetail
        With _bankReconciliationExtractDetail
            .DocumentType = ExtractDocumentType
            .DocumentDate = ExtractDocumentDate
            .Description = ExtractDescription
            .DocumentNumber = ExtractDocumentNumber
            .Nature = ExtractNature
            .Value = ExtractDocumentValue
        End With
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteExtractDetail()
        Dim dld As BankReconciliationExtractDetail = CType(INDgvBankReconciliationExtractDetail.GetFocusedRow, BankReconciliationExtractDetail)
        If dld.Id <> 0 Then
            If _listDeleteBankReconciliationExtractDetail Is Nothing Then
                _listDeleteBankReconciliationExtractDetail = New List(Of BankReconciliationExtractDetail)
            End If
            dld.MarkAsDeleted()
            _listDeleteBankReconciliationExtractDetail.Add(dld)
        End If
        _listBankReconciliationExtractDetail.Remove(dld)
        INDgcBankReconciliationExtractDetails.DataSource = Nothing
        INDgcBankReconciliationExtractDetails.DataSource = _listBankReconciliationExtractDetail
        Me.CalculateDifference()
    End Sub

#End Region

    Public Async Function CleanControls() As Task
        INDlyRoot.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDsleEntityBankAccount.EditValue = Nothing
        INDsleEntityBankAccount.Properties.NullText = String.Empty
        INDsleEntityBankAccount.Properties.ReadOnly = False
        INDdteDocumentDate.EditValue = Nothing
        INDseEndEntityBankAccountValue.EditValue = Nothing
        INDseExtractValue.EditValue = Nothing
        INDseDifferenceReconcile.EditValue = Nothing
        INDgcBankReconciliationDetails.DataSource = Nothing
        INDgcBankReconciliationExtractDetails.DataSource = Nothing

        CleanControlsPopup()

        _doc = Nothing
        _bankReconciliation = Nothing
        _listBankReconciliationDetail = Nothing
        _bankReconciliationExtractDetail = Nothing
        _listBankReconciliationExtractDetail = Nothing
        _listDeleteBankReconciliationExtractDetail = Nothing

        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = -1
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As Boolean
        Dim listErrors As New StringBuilder

        Me.CalculateDifference()
        If DifferenceReconcile <> 0 Then
            listErrors.AppendLine("No debe quedar ninguna diferencia en la conciliación")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Async Function AssigningValues() As Task
        Await Task.Factory.StartNew(Sub()
                                        With _bankReconciliation
                                            .CustomProperties = LayoutControls.GetCustomFieldsValue()
                                            .Code = Code
                                            .EntityBankAccountId = EntityBankAccountId
                                            .DocumentDate = DocumentDate
                                            .EntityBankAccountValue = EntityBankAccountValue
                                            .ExtractValue = ExtractValue

                                            If _listBankReconciliationDetail IsNot Nothing Then
                                                For Each itemDetail As BankReconciliationDetail In _listBankReconciliationDetail
													If itemDetail.Id > 0 Then
														itemDetail.MarkAsModified()
													End If
													itemDetail.Observations = String.Empty
													.BankReconciliationDetail.Add(itemDetail)
                                                Next
                                                If _listBankReconciliationDetail.Any(Function(d) d.Id > 0) Then
                                                    .MarkAsModified()
                                                End If
                                            End If

                                            If _listBankReconciliationExtractDetail IsNot Nothing Then
                                                For Each itemDetail As BankReconciliationExtractDetail In _listBankReconciliationExtractDetail
                                                    .BankReconciliationExtractDetail.Add(itemDetail)
                                                Next
                                                If _listBankReconciliationExtractDetail.Any(Function(d) d.Id > 0) Then
                                                    .MarkAsModified()
                                                End If
                                            End If
                                            If _listDeleteBankReconciliationExtractDetail IsNot Nothing Then
                                                For Each itemDetail As BankReconciliationExtractDetail In _listDeleteBankReconciliationExtractDetail
                                                    .BankReconciliationExtractDetail.Add(itemDetail)
                                                Next
                                                .MarkAsModified()
                                            End If
                                        End With

                                    End Sub)
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Sub NewEntity()
        _bankReconciliation = New BankReconciliation() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.TreasurySequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
    End Sub

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
                Using Model As New MBankConciliation(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlyRoot.BeginUpdate()
                    Dim resultOperation = Await Model.GetBankReconciliationByCode(INDbtnCode.Text.Trim)
                    If resultOperation.StateResult Then
                        _bankReconciliation = resultOperation.ObjectEmbbeded
                        INDsleEntityBankAccount.Properties.ReadOnly = True
                        If _bankReconciliation IsNot Nothing AndAlso _bankReconciliation.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_bankReconciliation.Id))
                                With _bankReconciliation
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                    Me._isLoad = True
                                    Code = .Code
                                    EntityBankAccountId = .EntityBankAccountId
                                    INDsleEntityBankAccount.Properties.NullText = .EntityBankAccountCodeName
                                    DocumentDate = .DocumentDate
                                    EntityBankAccountValue = .EntityBankAccountValue
                                    ExtractValue = .ExtractValue
                                    Dim result = Await Me._presenter.ListBankReconciliationDetail(_bankReconciliation.Id, EntityBankAccountId, DocumentDate)
                                    If result.StateResult Then
                                        _listBankReconciliationDetail = result.ObjectEmbbeded
                                        INDgcBankReconciliationDetails.DataSource = Nothing
                                        INDgcBankReconciliationDetails.DataSource = _listBankReconciliationDetail
                                    Else
                                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                                    End If
                                    _listBankReconciliationExtractDetail = .BankReconciliationExtractDetail.ToList()
                                    INDgcBankReconciliationExtractDetails.DataSource = _listBankReconciliationExtractDetail
                                    Me.CalculateDifference()
                                    Me._isLoad = False

                                    Me.BarraBotones.StatusRecord = If(.Status, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
                                End With
                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._bankReconciliation.Code)
                                If _blockRecord.Id = 0 Then
                                    _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                            New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _bankReconciliation.Id})
                                            ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                                End If
                                If _bankReconciliation.Status = 1 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                                End If

                                Me.BarraBotones.SetDocuments(_bankReconciliation.Id, Me.Tag.ToString(), Nothing, GetType(BankReconciliation).Name)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                Me.BarraBotones.PrintReport(PrintReportAction.None, _bankReconciliation.Id, 0, _bankReconciliation.Id)

                                AsyncLoader(False)
                                ActionsOnControls = True
                            End Using
                        Else
                            AsyncLoader(False)
                            If Me._sequense.IsManual Then
                                Me.NewEntity()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDbtnCode.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    End If
                    INDlyRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._bankReconciliation.Code, String.Format("{0} ({1})", Me._bankReconciliation.EntityBankAccountCodeName)),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._bankReconciliation.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._bankReconciliation.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._bankReconciliation.Code, String.Format("{0} ({1})", Me._bankReconciliation.EntityBankAccountCodeName))
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._bankReconciliation.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._bankReconciliation IsNot Nothing AndAlso Me._bankReconciliation.Id > 0 Then
            If (MessageIndigo.Show(BaseClass.obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, BaseClass.obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBankConciliation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PBankConciliation(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcBankReconciliationDetails)
        IndigoGridControl1.RefreshGrid(INDgcBankReconciliationExtractDetails)

        'Cargar las acciones a la rejilla
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvBankReconciliationExtractDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvBankReconciliationExtractDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                col.Visible = False
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next

        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _bankReconciliation = Nothing
        _listBankReconciliationDetail = Nothing
        _bankReconciliationExtractDetail = Nothing
        _listBankReconciliationExtractDetail = Nothing
        _listDeleteBankReconciliationExtractDetail = Nothing
        _isLoad = Nothing
        _varImp = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBankConciliation_Activated(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDSleExtractDocumentType.Properties.DataSource = FillingDocumentType
        Me.INDrepSleDetailDocumentType.DataSource = FillingDocumentType
        Me.INDrepSleExtractDetailDocumentType.DataSource = FillingDocumentType

        Me.INDSleExtractNature.Properties.DataSource = FillingNature
        Me.INDrepSleDetailNature.DataSource = FillingNature
        Me.INDrepSleExtractDetailNature.DataSource = FillingNature

        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Async Sub FrmBankConciliation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Me.NewEntity()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al control del valor mínimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseExtractValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDseExtractValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDgcBankReconciliationDetails.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceAddExtractDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceAddExtractDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDPceAddExtractDetail.ShowPopup()
            INDSleExtractDocumentType.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta bancaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityBankAccount.QueryPopUp
        If EntityBankAccountXpo Is Nothing Then
            _presenter.InitializeEntityBankAccount()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el mas del control de cuenta bancaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(628, Nothing, True)
            _presenter.InitializeEntityBankAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la cuenta bancaria o de la fecha del documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleEntityBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityBankAccount.EditValueChanged, INDdteDocumentDate.EditValueChanged
        If EntityBankAccountId IsNot Nothing Then
            If DocumentDate IsNot Nothing Then
                If Not _isLoad Then
                    Try
                        AsyncLoader(True)
                        Using model As New MCashRegister(MyTag)
                            AsyncLoader(True)
                            Dim bankAccount = Await model.GetEntityBankAccountById(EntityBankAccountId)
                            If bankAccount IsNot Nothing AndAlso Not String.IsNullOrEmpty(bankAccount?.CurrencyAbbreviation) Then
                                SetFormatCurrencyUI(bankAccount?.CurrencyAbbreviation)
                            End If

                        End Using

                        Dim result = Await Me._presenter.ListBankReconciliationDetail(_bankReconciliation.Id, EntityBankAccountId, DocumentDate)
                        If result.StateResult Then
                            _listBankReconciliationDetail = result.ObjectEmbbeded
                            INDgcBankReconciliationDetails.DataSource = Nothing
                            INDgcBankReconciliationDetails.DataSource = _listBankReconciliationDetail

                            CalculateLastBalance()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    Catch ex As Exception
                        Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                    Finally
                        AsyncLoader(False)
                    End Try
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del libro de bancos, o el valor de los extractos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseEndEntityBankAccountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDseEndEntityBankAccountValue.EditValueChanged, INDseExtractValue.EditValueChanged
        CalculateDifference()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el estado de un detalle de libro de bancos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim detail = INDgvBankReconciliationDetail.GetFocusedRow()
            If detail IsNot Nothing Then
                Dim DetailUncheckedDebitValue As Decimal = If(detail.Nature = 1, detail.Value, 0) * If(e.NewValue, -1, 1)
                Dim DetailUncheckedCreditValue As Decimal = If(detail.Nature = 1, 0, detail.Value) * If(e.NewValue, -1, 1)
                Dim ExtractDetailDebitValue As Decimal = 0
                Dim ExtractDetailCreditValue As Decimal = 0

                If _listBankReconciliationDetail IsNot Nothing Then
                    If detail.Reconciled = e.NewValue Then
                        DetailUncheckedDebitValue = 0
                        DetailUncheckedCreditValue = 0
                    End If

                    DetailUncheckedDebitValue = DetailUncheckedDebitValue + _listBankReconciliationDetail.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                    DetailUncheckedCreditValue = DetailUncheckedCreditValue + _listBankReconciliationDetail.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                End If

                If _listBankReconciliationExtractDetail IsNot Nothing Then
                    ExtractDetailDebitValue = _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                    ExtractDetailCreditValue = _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
                End If

                DifferenceReconcile = (EntityBankAccountValue - DetailUncheckedDebitValue + DetailUncheckedCreditValue) - (ExtractValue + ExtractDetailDebitValue - ExtractDetailCreditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar la naturaleza de un detalle del extracto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepSleExtractDetailNature_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrepSleExtractDetailNature.EditValueChanging
        If e IsNot Nothing Then
            Dim detail = INDgvBankReconciliationExtractDetail.GetFocusedRow()
            If detail IsNot Nothing Then
                Dim DetailUncheckedDebitValue As Decimal = 0
                Dim DetailUncheckedCreditValue As Decimal = 0
                Dim ExtractDetailDebitValue As Decimal = detail.Value * If(e.NewValue = 1, 1, -1)
                Dim ExtractDetailCreditValue As Decimal = detail.Value * If(e.NewValue = 1, -1, 1)

                If _listBankReconciliationDetail IsNot Nothing Then
                    DetailUncheckedDebitValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                    DetailUncheckedCreditValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                End If

                If _listBankReconciliationExtractDetail IsNot Nothing Then
                    If detail.Nature = e.NewValue Then
                        ExtractDetailDebitValue = 0
                        ExtractDetailCreditValue = 0
                    End If

                    ExtractDetailDebitValue = ExtractDetailDebitValue + _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                    ExtractDetailCreditValue = ExtractDetailCreditValue + _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
                End If

                DifferenceReconcile = (EntityBankAccountValue - DetailUncheckedDebitValue + DetailUncheckedCreditValue) - (ExtractValue + ExtractDetailDebitValue - ExtractDetailCreditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de un detalle del extracto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtExtractDetailValue_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrepTxtExtractDetailValue.EditValueChanging
        If e IsNot Nothing Then
            Dim value As Decimal = 0
            If Not Decimal.TryParse(e.NewValue.ToString().Replace(indigo.Culture.NumberFormat.CurrencyGroupSeparator, indigo.Culture.NumberFormat.NumberDecimalSeparator), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, value) Then
                e.Cancel = True
                Exit Sub
            End If

            If value <= 0 Then
                e.Cancel = True
                Exit Sub
            End If

            Dim detail = INDgvBankReconciliationExtractDetail.GetFocusedRow()
            If detail IsNot Nothing Then
                Dim DetailUncheckedDebitValue As Decimal = 0
                Dim DetailUncheckedCreditValue As Decimal = 0
                Dim ExtractDetailDebitValue As Decimal = If(detail.Nature = 1, value - e.OldValue, 0)
                Dim ExtractDetailCreditValue As Decimal = If(detail.Nature = 1, 0, value - e.OldValue)

                If _listBankReconciliationDetail IsNot Nothing Then
                    DetailUncheckedDebitValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                    DetailUncheckedCreditValue = _listBankReconciliationDetail.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                End If

                If _listBankReconciliationExtractDetail IsNot Nothing Then
                    If detail.Value = value Then
                        ExtractDetailDebitValue = 0
                        ExtractDetailCreditValue = 0
                    End If

                    ExtractDetailDebitValue = ExtractDetailDebitValue + _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                    ExtractDetailCreditValue = ExtractDetailCreditValue + _listBankReconciliationExtractDetail.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
                End If

                DifferenceReconcile = (EntityBankAccountValue - DetailUncheckedDebitValue + DetailUncheckedCreditValue) - (ExtractValue + ExtractDetailDebitValue - ExtractDetailCreditValue)
            End If
        End If
    End Sub

#End Region

#Region "ValidatingEditor"

    Private Sub INDgvBankReconciliationExtractDetail_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs) Handles INDgvBankReconciliationExtractDetail.ValidatingEditor
        Dim view As ColumnView = sender
        Dim column As GridColumn = If(TryCast(e, EditFormValidateEditorEventArgs)?.Column, view.FocusedColumn)
        If column.FieldName <> "DocumentDate" Then Exit Sub
        If CDate(e.Value).AsDate > DocumentDate.AsDate Then
            e.Valid = False
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddExtractDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddExtractDetail.Click
        AddExtractDetail()
    End Sub

#End Region

#Region "ContextMenuGridControl"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteExtractDetail()
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _bankReconciliation.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _bankReconciliation.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _bankReconciliation.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _bankReconciliation.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _bankReconciliation.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _bankReconciliation.Id, 0, _bankReconciliation.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequense.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class