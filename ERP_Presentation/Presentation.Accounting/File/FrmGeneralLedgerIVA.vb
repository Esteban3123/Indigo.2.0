'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Jeisson Herrera Peña
' Created          : 13-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls

#End Region

''' <summary>
''' Formulario de GeneralLedgerIVA
''' </summary>
Public Class FrmGeneralLedgerIVA
    Implements IGenerealLedgerIVA, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' variable que contiene una secuencia
    ''' </summary>
    Private _sequence As GeneralLedgerSequence

    ''' <summary>
    ''' Contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IGenerealLedgerIVA.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As GeneralLedgerSequence Implements IGenerealLedgerIVA.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Encapsula la entidad del tipo de documento
    ''' </summary>
    Private _generalLedgerIva As New GeneralLedgerIVA
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PGeneralLedgerIVA
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger

#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna un mensaje para mostrar en el Funcional
    ''' </summary>
    ''' <param name="Icono">Icono del Mensaje</param>
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
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGenerealLedgerIVA.ActionsOnControls
        Set(value As Boolean)
            INDLcBase.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDTxtPercentage.Enabled = value
            INDsleAccountPurchaseService.Enabled = value
            INDsleAccountSale.Enabled = value
            INDsleAccountDebitControlFiscal.Enabled = value
            INDsleAccountCreditControlFiscal.Enabled = value
            INDGlePaymentMethodTypes.Enabled = value
            INDRgApplyTaxDevolution.Enabled = value
            INDLcBase.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Código del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property Code As String Implements IGenerealLedgerIVA.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Nombre del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property GLedgerIVAName As String Implements IGenerealLedgerIVA.GLedgerIVAName
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Porcentaje del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property Percentage As Decimal Implements IGenerealLedgerIVA.Percentage
        Get
            Return CType(INDTxtPercentage.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDTxtPercentage.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Estado del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IGenerealLedgerIVA.Status
        Get
            Return CBool(Me.BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property
    ''' <summary>
    '''  Esta propiedad proporciona acceso a la fuente de datos de cuentas mediante XPInstantFeedbackSource. implementa la interfaz IGenerealLedgerIVA.AccountsXpo
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountsXpo As XPInstantFeedbackSource Implements IGenerealLedgerIVA.AccountsXpo
        Get
            'Devuelve la fuente de datos XPInstantFeedbackSource del control INDsleAccountPurchaseService.
            Return CType(INDsleAccountPurchaseService.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        'Establece la fuente de datos del control INDsleAccountPurchaseService(Cuenta IVA compra/servicio) con el valor proporcionado.
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountPurchaseService.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el valor del Id de IdAccountPurchaseService y lo establece en el control INDsleAccountPurchaseService(Cuenta IVA compra/servicio)
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAccountPurchaseService As Integer? Implements IGenerealLedgerIVA.IdAccountPurchaseService
        Get
            Return INDsleAccountPurchaseService.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPurchaseService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Proporciona acceso a las cuentas de ventas y las establece en el select INDsleAccountSale
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountSaleXpo As XPInstantFeedbackSource Implements IGenerealLedgerIVA.AccountSaleXpo
        Get
            Return CType(INDsleAccountSale.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountSale.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el valor del Id de cuentas de venta y lo establece en el control INDsleAccountSale
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAccountSale As Integer? Implements IGenerealLedgerIVA.IdAccountSale
        Get
            Return INDsleAccountSale.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountSale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Proporciona acceso a las cuentas de control fiscal débito y las establece en el select INDsleAccountDebitControlFiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountDebitControlFiscalXpo As XPInstantFeedbackSource Implements IGenerealLedgerIVA.AccountDebitControlFiscalXpo
        Get
            Return CType(INDsleAccountDebitControlFiscal.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountDebitControlFiscal.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el valor del Id de control fiscal débito y lo establece en el control INDsleAccountDebitControlFiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAccountDebitControlFiscal As Integer? Implements IGenerealLedgerIVA.IdAccountDebitControlFiscal
        Get
            Return INDsleAccountDebitControlFiscal.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountDebitControlFiscal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Proporciona acceso a las cuentas de control fiscal crédito y las establece en el select INDsleAccountCreditControlFiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountCreditControlFiscalXpo As XPInstantFeedbackSource Implements IGenerealLedgerIVA.AccountCreditControlFiscalXpo
        Get
            Return CType(INDsleAccountCreditControlFiscal.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountCreditControlFiscal.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene el valor del Id de control fiscal crédito y lo establece en el control INDsleAccountCreditControlFiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAccountCreditControlFiscal As Integer? Implements IGenerealLedgerIVA.IdAccountCreditControlFiscal
        Get
            Return INDsleAccountCreditControlFiscal.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountCreditControlFiscal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece u obtiene el valor si aplica devolucion del iva
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyTaxDevolution As Boolean Implements IGenerealLedgerIVA.ApplyTaxDevolution
        Get
            Return INDRgApplyTaxDevolution.EditValue
        End Get
        Set(value As Boolean)
            INDRgApplyTaxDevolution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de los metodos de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Property DatasourcePaymentMethodTypes As List(Of Tuple(Of Byte, String)) Implements IGenerealLedgerIVA.DatasourcePaymentMethodTypes
        Get
            Return INDGlePaymentMethodTypes.Properties.DataSource
        End Get
        Set(value As List(Of Tuple(Of Byte, String)))
            INDGlePaymentMethodTypes.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Icrud"

    ''' <summary>
    ''' Metodo: Item buscar del control de usuarios
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me._generalLedgerIva IsNot Nothing AndAlso Me._generalLedgerIva.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MGeneralLedgerIVA(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteGeneralLedgerIVA(Me._generalLedgerIva)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MGeneralLedgerIVA(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of GeneralLedgerIVA) = Await Model.SaveGeneralLedgerIVA(Me._generalLedgerIva, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _generalLedgerIva.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._generalLedgerIva = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metododo para consultar el tipo de documento 
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MGeneralLedgerIVA(CStr(Me.Tag))
                    AsyncLoader(True)
                    _generalLedgerIva = Await Model.GetGeneralLedgerIVAByCodeAsync(INDBteCode.Text.Trim)
                    INDLcBase.BeginUpdate()
                    If _generalLedgerIva IsNot Nothing AndAlso _generalLedgerIva.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        _record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(_generalLedgerIva.Id))
                        With _generalLedgerIva
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            GLedgerIVAName = .Name
                            Percentage = .Percentage
                            IdAccountPurchaseService = .IdAccountPurchaseService
                            IdAccountSale = .IdAccountSale
                            IdAccountDebitControlFiscal = .IdAccountDebitControlFiscal
                            IdAccountCreditControlFiscal = .IdAccountCreditControlFiscal
                            Status = .Status
                            Me.ApplyTaxDevolution = .ApplyTaxDevolution
                            If Not String.IsNullOrEmpty(.PaymentMethodTypes) Then
                                .PaymentMethodTypes.Split(",").
                                                ToList().
                                                ForEach(Sub(s)
                                                            Dim _row = Me.DatasourcePaymentMethodTypes.Find(Function(x) x.Item1 = s)
                                                            Me._selectorPaymentMethodTypes.SetValue(_row)
                                                        End Sub)
                            End If
                        End With
                        INDSleSelector_Closed(INDGlePaymentMethodTypes, Nothing)

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._generalLedgerIva.Code)
                        If _record.Id = 0 Then
                            _record = (Await Model.SaveBlockRecord(
                                New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _generalLedgerIva.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If

                        Me.BarraBotones.SetDocuments(_generalLedgerIva.Id, Me.Tag.ToString(), Nothing, GetType(GeneralLedgerIVA).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewGeneralLedgerIVA()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcBase.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Genera el documento indexado
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._generalLedgerIva.Code, Me._generalLedgerIva.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._generalLedgerIva.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._generalLedgerIva.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._generalLedgerIva.Code, Me._generalLedgerIva.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._generalLedgerIva.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Me._generalLedgerIva
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Me.GLedgerIVAName
            .Percentage = Me.Percentage
            .IdAccountPurchaseService = Me.IdAccountPurchaseService
            .IdAccountSale = Me.IdAccountSale
            .IdAccountDebitControlFiscal = Me.IdAccountDebitControlFiscal
            .IdAccountCreditControlFiscal = Me.IdAccountCreditControlFiscal
            .PaymentMethodTypes = Me._selectorPaymentMethodTypes.GetKeys()
            .ApplyTaxDevolution = Me.ApplyTaxDevolution
        End With
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDLcBase.BeginUpdate()
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        INDBteCode.Text = String.Empty
        Me.INDTxtName.Text = String.Empty
        Me.INDTxtPercentage.Text = 0
        Me._generalLedgerIva = Nothing
        Me.INDsleAccountPurchaseService.EditValue = Nothing
        Me.INDsleAccountPurchaseService.Properties.NullText = String.Empty
        Me.INDsleAccountSale.EditValue = Nothing
        Me.INDsleAccountSale.Properties.NullText = String.Empty
        Me.INDsleAccountDebitControlFiscal.EditValue = Nothing
        Me.INDsleAccountDebitControlFiscal.Properties.NullText = String.Empty
        Me.INDsleAccountCreditControlFiscal.EditValue = Nothing
        Me.INDsleAccountCreditControlFiscal.Properties.NullText = String.Empty
        Me.Status = True
        Me.ApplyTaxDevolution = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcBase.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MGeneralLedgerIVA(Me.Tag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 30},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Porcentaje", .FieldName = "Percentage", .ColumnWidth = 150}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListGeneralLedgerIva
            .ValorSolicitado = "Code"
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
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        'No se implementa
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewGeneralLedgerIVA()
        End If
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._generalLedgerIva.Code) Then
            Try

                Using Model As New MGeneralLedgerIVA(Me.Tag)
                    AsyncLoader(True)
                    Dim stateCard As Boolean = Not Me._generalLedgerIva.Status
                    Dim Result = Await Model.UpdateStateGeneralLedgerIVA(Me._generalLedgerIva.Code, stateCard)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._generalLedgerIva = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                INDBteCode.Enabled = False
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo concepto de retencion
    ''' </summary>
    Private Async Function NewGeneralLedgerIVA() As Task
        _generalLedgerIva = New GeneralLedgerIVA() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MGeneralLedgerIVA(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
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
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Overloads Function ValidateControls() As Boolean
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
            Return False
        End If
        If INDTxtName.Text = String.Empty Then
            INDTxtName.Focus()
            Return False
        End If
        If Me.ApplyTaxDevolution AndAlso String.IsNullOrEmpty(Me._selectorPaymentMethodTypes.GetKeys) Then
            INDGlePaymentMethodTypes.Focus()
            Return False
        End If
        Return True
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _generalLedgerIva = Nothing
        _presenter = Nothing
        _record = Nothing
        _selectorPaymentMethodTypes = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    Private Sub FrmGeneralLedgerIVA_Load(sender As Object, e As EventArgs) Handles Me.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me._presenter = New PGeneralLedgerIVA(Me)
        Me._presenter.GetSequence()
        Me.Funct = AddressOf GenerateDoc
        Me.DatasourcePaymentMethodTypes = Utils.PaymentMethodTypes
        Me.LoadStatus()
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    Private Sub FrmGeneralLedgerIVA_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewGeneralLedgerIVA()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer click en el boton del control y lanza el form de busqueda
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnCode_Click(sender As Object, e As EventArgs)
        OpenSearch()
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Text Is String.Empty Then
            INDBteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Aquí se hace la lógica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me._generalLedgerIva IsNot Nothing AndAlso Me._generalLedgerIva.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Events"

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta IVA Compra/Servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountPurchaseService_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountPurchaseService.QueryPopUp, INDsleAccountPurchaseService.QueryPopUp
        If INDsleAccountPurchaseService.Properties.DataSource Is Nothing Then
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta de ventas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountSale_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountSale.QueryPopUp
        If INDsleAccountSale.Properties.DataSource Is Nothing Then
            _presenter.InitializeSale()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta control fiscal débito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountDebitControlFiscal_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountDebitControlFiscal.QueryPopUp
        If INDsleAccountDebitControlFiscal.Properties.DataSource Is Nothing Then
            _presenter.InitializeDebit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta control fiscal crédito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountCreditControlFiscal_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountCreditControlFiscal.QueryPopUp
        If INDsleAccountCreditControlFiscal.Properties.DataSource Is Nothing Then
            _presenter.InitializeCredit()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se activa cuando cambia el valor seleccionado en el control cuenta IVA Compra/Servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountPurchaseService_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountPurchaseService.EditValueChanged, INDsleAccountPurchaseService.EditValueChanged
        If INDsleAccountPurchaseService.EditValue IsNot Nothing Then
            _presenter.Initialize()
        Else
            IdAccountPurchaseService = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa cuando cambia el valor seleccionado en el control cuenta IVA ventas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountSale_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountSale.EditValueChanged
        If INDsleAccountSale.EditValue IsNot Nothing Then
            _presenter.InitializeSale()
        Else
            IdAccountPurchaseService = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa cuando cambia el valor seleccionado en el control cuenta control fiscal débito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountDebitControlFiscal_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountDebitControlFiscal.EditValueChanged
        If INDsleAccountDebitControlFiscal.EditValue IsNot Nothing Then
            _presenter.InitializeDebit()
        Else
            IdAccountDebitControlFiscal = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa cuando cambia el valor seleccionado en el control cuenta control fiscal crédito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAccountCreditControlFiscal_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountCreditControlFiscal.EditValueChanged
        If INDsleAccountCreditControlFiscal.EditValue IsNot Nothing Then
            _presenter.InitializeCredit()
        Else
            IdAccountCreditControlFiscal = Nothing
        End If
    End Sub

    ''' <summary>
    ''' evento que oculta o muestra las formas de pago dependiendo si aplica devolucion de iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgApplyTaxDevolution_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgApplyTaxDevolution.EditValueChanged
        If INDRgApplyTaxDevolution.EditValue Is Nothing Then
            Exit Sub
        End If

        INDLciPaymentMethodTypes.HideControl(Not Me.ApplyTaxDevolution)
        If Not Me.ApplyTaxDevolution Then
            Me._selectorPaymentMethodTypes = New SelectorCache("Item1", "Item2")
            Me.INDGlePaymentMethodTypes.Properties.NullText = String.Empty
        End If
    End Sub
#End Region

#End Region

#Region "Selector"
    ' Se crea una instancia de SelectorCache que utiliza los nombres de las propiedades Item1 y Item2
    Private _selectorPaymentMethodTypes As SelectorCache = New SelectorCache("Item1", "Item2")

    ''' <summary>
    ''' Este evento se activa cuando se necesita proporcionar datos para una columna no enlazada personalizada (CustomUnboundColumn) en el control INDGvSelector
    ''' </summary>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGleViewPaymentsMethods.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGleViewPaymentsMethods" Then
                e.Value = _selectorPaymentMethodTypes.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se hace click en una celda de una fila en el control INDGvSelector.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGleViewPaymentsMethods.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGleViewPaymentsMethods" Then
                selector = _selectorPaymentMethodTypes
            End If
            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se cierra el cuadro de búsqueda y selección INDSleSelector (GridLookUpEdit) para PaymentMethodTypes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDGlePaymentMethodTypes.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDGlePaymentMethodTypes" Then
            searchLookupEdit.Properties.NullText = _selectorPaymentMethodTypes.ToString()
        End If
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Evento para cambiar el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub


    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
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
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region


End Class