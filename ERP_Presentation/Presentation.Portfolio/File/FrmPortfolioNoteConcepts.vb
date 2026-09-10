'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 08-04-2014
'
' Last Modified By :  
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports System.Text
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmPortfolioNoteConcepts
    Implements IPortfolioNoteConcept, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

#End Region

#Region "CRUD BASE"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Async Sub Deshacer() Implements ICrudBase.Deshacer
        Await CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.portfolioNoteConcept IsNot Nothing AndAlso Me.portfolioNoteConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MPortfolioNoteConcept(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePortfolioNoteConcept(Me.portfolioNoteConcept)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Await Me.CleanControls()
                        Else
                            AsyncLoader(False)
                            INDBtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MPortfolioNoteConcept(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of PortfolioNoteConcept) = Await Model.SavePortfolioNoteConcept(Me.portfolioNoteConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If portfolioNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.portfolioNoteConcept = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Await CleanControls()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Await Me.CleanControls()
        Else
            Await NewPortfolioNoteConcept()
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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}, New ColumnInfo() With {.Caption = "Cuenta Contable", .FieldName = "IdAccount.NumberName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.PortfolioNoteConcept
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
        'Code = ReturnValue
        'If Code <> String.Empty Then
        '    Await LoadControls()
        '    If INDBtnCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDBtnCode.Enabled = False
        'End If


        DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDBtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioNoteConcept.ActionsOnControls
        Set(value As Boolean)
            INDLycNoteConcepts.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDGleAccount.Enabled = value
            INDGleNoteType.Enabled = value
            INDTxtAlternateCode.Enabled = value
            INDRgHandleTax.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDLycNoteConcepts.EndUpdate()
            If value = True Then
                INDTxtName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property
#End Region

#Region "Variable Globales Propiedades Interfaz"
    ''' <summary>
    ''' datasour para el tipo de nota
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listNoteType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListNoteType As List(Of Tuple(Of Byte, String))
        Get
            If _listNoteType Is Nothing Then
                _listNoteType = New List(Of Tuple(Of Byte, String))
                _listNoteType.Add(New Tuple(Of Byte, String)(1, "General"))
                _listNoteType.Add(New Tuple(Of Byte, String)(2, "Especifico"))
            End If
            Return _listNoteType
        End Get
    End Property
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Obtiene o establece la cuenta contable del concepto
    ''' </summary>
    ''' <value>
    ''' account.
    ''' </value>
    Public Property Account As Integer? Implements IPortfolioNoteConcept.Account
        Get
            Return INDGleAccount.EditValue
        End Get
        Set(value As Integer?)
            INDGleAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto
    ''' </summary>
    ''' <value>
    ''' code.
    ''' </value>
    Public Property Code As String Implements IPortfolioNoteConcept.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Gets or sets the name1.
    ''' </summary>
    ''' <value>
    ''' The name1.
    ''' </value>
    Public Property Name1 As String Implements IPortfolioNoteConcept.Name
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para almacenar y editar el Código Alterno (COL) / Código Cabys (CR)
    ''' </summary>
    ''' <value>
    ''' AlternateCode
    ''' </value>
    Public Property AlternateCode As String Implements IPortfolioNoteConcept.AlternateCode
        Get
            Return INDTxtAlternateCode.EditValue
        End Get
        Set(value As String)
            INDTxtAlternateCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Maneja impuesto
    ''' </summary>
    ''' <returns></returns>
    Public Property HandleTax As Boolean Implements IPortfolioNoteConcept.HandleTax
        Get
            Return INDRgHandleTax.EditValue
        End Get
        Set(value As Boolean)
            INDRgHandleTax.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As Boolean Implements IPortfolioNoteConcept.Status
        Get
            Return Me.BarraBotones.StatusRecord
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
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IPortfolioNoteConcept.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PortfolioSequence Implements IPortfolioNoteConcept.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property AccountsXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPortfolioNoteConcept.AccountsXpo
        Get
            Return CType(INDGleAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDGleAccount.Properties.DataSource = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioNoteConcept.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' entidad de indicadores economicos
    ''' </summary>
    Dim portfolioNoteConcept As PortfolioNoteConcept
    ''' <summary>
    ''' modelo de indicaores economicos
    ''' </summary>
    Dim model As MPortfolioNoteConcept
    ''' <summary>
    ''' presentador de indicadores economicos
    ''' </summary>
    Dim presenter As PPortfolioNoteConcept
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Variable privada para almacenar en memoria los parametros de empresa
    ''' </summary>
    Private _companySetting As CompanySettings

    ''' <summary>
    ''' tipo de nota
    ''' </summary>
    Public Property NoteType As Integer Implements IPortfolioNoteConcept.NoteType
        Get
            Return INDGleNoteType.EditValue
        End Get
        Set(value As Integer)
            INDGleNoteType.EditValue = value
        End Set
    End Property
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Obtiene el valor del campo "Precios de venta incluye impuestos" de las configuraciones de la compañia
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetHandleTax() As Task(Of Boolean)

        If Me._companySetting IsNot Nothing Then
            Return Me._companySetting.SalePriceIncludeTax
        End If

        Using model As New MCompanySettings(MyTag)
            Me._companySetting = Await model.GetCompanySettings()
            Return If(Me._companySetting?.SalePriceIncludeTax, False)
        End Using
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewPortfolioNoteConcept() As Task

        portfolioNoteConcept = New PortfolioNoteConcept() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Me.Code = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Metodo para generar la indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString("FrmNoteConcepts_IndexContent", NAME_MODULE), portfolioNoteConcept.Code, portfolioNoteConcept.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & portfolioNoteConcept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString("FrmNoteConcepts_IndexTitle", NAME_MODULE), portfolioNoteConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString("FrmNoteConcepts_IndexContent", NAME_MODULE), portfolioNoteConcept.Code, portfolioNoteConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString("FrmNoteConcepts_IndexTitle", NAME_MODULE), portfolioNoteConcept.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Async Function CleanControls() As Task
        INDLycNoteConcepts.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        'Limpiar controles
        INDBtnCode.Text = String.Empty
        INDTxtName.Text = String.Empty
        INDGleNoteType.EditValue = 2
        INDGleAccount.EditValue = Nothing
        AlternateCode = Nothing
        Account = Nothing
        HandleTax = Await GetHandleTax()
        portfolioNoteConcept = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLycNoteConcepts.EndUpdate()
        DeleteBlockedRecord()
    End Function

    ''' <summary>
    ''' metodo para validar que los controles esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If INDGleNoteType.EditValue = 2 Then
            If INDGleAccount.EditValue Is Nothing Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With portfolioNoteConcept
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name1
            .AlternateCode = AlternateCode
            .NoteType = NoteType
            .IdAccount = Account
            .HandleTax = HandleTax
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        'If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
        '        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '        Await model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End Using
        'Else
        '    Me.BarraBotones.EnableBarItems()
        'End If


        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar los controles con el registro consultado
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPortfolioNoteConcept(CStr(Me.Tag))
                    AsyncLoader(True)
                    portfolioNoteConcept = Await Model.GetPortfolioNoteConcept(INDBtnCode.Text.Trim)
                    INDLycNoteConcepts.BeginUpdate()
                    If portfolioNoteConcept IsNot Nothing AndAlso portfolioNoteConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(portfolioNoteConcept.Id))
                            With portfolioNoteConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Name1 = .Name
                                NoteType = .NoteType
                                Account = .IdAccount
                                AlternateCode = .AlternateCode
                                HandleTax = .HandleTax
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.portfolioNoteConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = portfolioNoteConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(portfolioNoteConcept.Id, Me.Tag.ToString(), Nothing, GetType(PortfolioNoteConcept).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPortfolioNoteConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLycNoteConcepts.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
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
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        portfolioNoteConcept = Nothing
        model = Nothing
        presenter = Nothing
        record = Nothing
        _searchMode = Nothing
        Me._companySetting = Nothing
    End Sub

    ''' <summary>
    ''' Evento para poner el foco al codigo cuando el formulario este activo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmNoteConcepts_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub


    ''' <summary>
    ''' evento que se dispara al presionar enter en el control y realiza una busqueda por codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If String.IsNullOrEmpty(Code) Then
        '        Me.NewPortfolioNoteConcept()
        '    Else
        '        Await Me.LoadControls()
        '    End If
        'End If

        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(Code) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(Code) Then
        '            Me.NewPortfolioNoteConcept()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If





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
                    Await Me.NewPortfolioNoteConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton de busqueda del control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento load del funcional
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmNoteConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLycNoteConcepts, True)
        '****Inicializar variables*****'
        'Me._doc = Nothing
        ' _idOperativeUnit = BarraBotones.OperatingUnitValue
        'Me._indigoSession = SessionValues.Instance
        '******************************'
        'Me.Funct = AddressOf GenerateDoc
        'INDGleNoteType.Properties.DataSource = ListNoteType
        'presenter = New PPortfolioNoteConcept(Me)
        ' presenter.LoadDefinitionLayout()
        ' presenter.GetSequense()
        'presenter.Initialize()
        'Deshacer()
        'Me.BarraBotones.StatusRecordVisible = True
        ' Me.LoadStatus()
        '_searchMode = False
        If Me.indigo.Culture.Name = "es-CR" Then
            INDLCiAlternateCode.Text = "Código Cabys"
        End If


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        presenter = New PPortfolioNoteConcept(Me)
        presenter.GetSequense()
        INDGleNoteType.Properties.DataSource = ListNoteType
        presenter.Initialize()
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerra el formulario y elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmNoteConcepts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'If portfolioNoteConcept IsNot Nothing AndAlso portfolioNoteConcept.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        DeleteBlockedRecord()
        '        Code = Me.IdEntity.Trim()
        '        Await LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Code = Me.IdEntity.Trim()
        '    Await LoadControls()
        'End If
        'Me.IdEntity =  String.Empty
        'If INDBtnCode.Enabled = False Then
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        'End If



        Me.ViewModeEditHold = True
        If Me.portfolioNoteConcept IsNot Nothing AndAlso Me.portfolioNoteConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Sub INDGleNoteType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleNoteType.EditValueChanged
        If INDGleNoteType.EditValue = 1 Then
            INDLciMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Account = Nothing
        Else
            INDLciMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub



#End Region

#Region "Eventos Barra Botones"

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Async Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Await Me.CleanControls()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        'Try
        '    Using model As New MPortfolioNoteConcept(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of PortfolioNoteConcept)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            Case eActionsStatusRecords.Active
        '                Result = Await model.ChangeState(Code, True)
        '            Case eActionsStatusRecords.Inactive
        '                Result = Await model.ChangeState(Code, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            portfolioNoteConcept = Result.ObjectEmbbeded
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try




        If Not String.IsNullOrEmpty(Me.portfolioNoteConcept.Code) Then
            Try
                Using model As New MPortfolioNoteConcept(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not portfolioNoteConcept.Status
                    Dim result As ActionResult(Of PortfolioNoteConcept) = Await model.ChangeState(Me.portfolioNoteConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.portfolioNoteConcept = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PortfolioSequenceDetail IsNot Nothing Then
        '    If Me._sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class