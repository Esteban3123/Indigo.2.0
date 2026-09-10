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

#End Region

Public Class FrmPopupServices
    Implements IPopupServices

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IPopupServices.Status
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
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPopupServices.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _typeManual As Integer
    Public Property TypeRateManual As Integer
        Get
            Return _typeManual
        End Get
        Set(value As Integer)
            _typeManual = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el form se cierra con el boton de agregar o con la X de la ventana
    ''' </summary>
    ''' <remarks></remarks>
    Dim _banClose As Boolean
    Public Property BanClose As Boolean
        Get
            Return _banClose
        End Get
        Set(value As Boolean)
            _banClose = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _rateManualId As Integer
    Public Property RateManualId As Integer
        Get
            Return _rateManualId
        End Get
        Set(value As Integer)
            _rateManualId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _rateManualDescription As String
    Public Property RateManualDescription As String
        Get
            Return _rateManualDescription
        End Get
        Set(value As String)
            _rateManualDescription = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceId As Integer Implements IPopupServices.IPSServiceId
        Get
            Return INDsleIPSService.EditValue
        End Get
        Set(value As Integer)
            INDsleIPSService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los servicios ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupServices.IPSServiceXpo
        Get
            Return INDsleIPSService.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIPSService.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del salario minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractMinimumWageId As Integer Implements IPopupServices.ContractMinimumWageId
        Get
            Return INDsleContractMinimumWage.EditValue
        End Get
        Set(value As Integer)
            INDsleContractMinimumWage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del salario minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractMinimumWageXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupServices.ContractMinimumWageXpo
        Get
            Return INDsleContractMinimumWage.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractMinimumWage.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountPercentage As Decimal Implements IPopupServices.DiscountPercentage
        Get
            Return INDseDiscountPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseDiscountPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IPopupServices.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica la liquidacion de ingresos hospitalarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InPatientRecoveryFeeType As Integer Implements IPopupServices.InPatientRecoveryFeeType
        Get
            Return INDsleInPatientRecoveryFeeType.EditValue
        End Get
        Set(value As Integer)
            INDsleInPatientRecoveryFeeType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IPopupServices.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica la liquidacion de ingresos ambulatorios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutPatientRecoveryFeeType As Integer Implements IPopupServices.OutPatientRecoveryFeeType
        Get
            Return INDsleOutPatientRecoveryFeeType.EditValue
        End Get
        Set(value As Integer)
            INDsleOutPatientRecoveryFeeType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de procedimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ScoreProcedure As Integer Implements IPopupServices.ScoreProcedure
        Get
            Return INDtxtScoreProcedure.EditValue
        End Get
        Set(value As Integer)
            INDtxtScoreProcedure.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValue As Decimal Implements IPopupServices.SalesValue
        Get
            Return INDtxtSalesValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor con descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurcharge As Decimal Implements IPopupServices.SalesValueWithSurcharge
        Get
            Return INDtxtSalesValueWithSurcharge.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurcharge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo quirugico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupId As Integer Implements IPopupServices.SurgicalGroupId
        Get
            Return INDsleSurgicalGroup.EditValue
        End Get
        Set(value As Integer)
            INDsleSurgicalGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource para grupos quirurgicos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupServices.SurgicalGroupXpo
        Get
            Return INDsleSurgicalGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSurgicalGroup.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPopupServices

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Representa la entidad xpo de servicios ips
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ipsServicesXpo As Infrastructure.Data.Xpo.ContractRepository.ContractIPSServiceXPO

    ''' <summary>
    ''' Variable que identifica el cambio de valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSearch As Boolean = True

    ' ''' <summary>
    ' ''' Listado de rangos de valores para los servicios
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Dim ListServiceFees As List(Of ServiceFees)

    ' ''' <summary>
    ' ''' Listado de eliminados de rangos de valores para los servicios
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public ListDeleteServiceFees As List(Of ServiceFees)

    ' ''' <summary>
    ' ''' Variable que representa la entidad de rangos
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Dim _servicesFees As ServiceFees

    ''' <summary>
    ''' True = modificar y False = guardar en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListOutPatientRecoveryFeeType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListInPatientRecoveryFeeType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public rateManualDetail As RateManualDetail

    ''' <summary>
    ''' Listado para preguntar si ya existe el servicio en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public _listPivotIPSServiceId As List(Of Integer)

    ''' <summary>
    ''' True = Guardar y False = Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSaveModify As Boolean

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

#End Region

#Region "Public Events"

    ''' <summary>
    ''' evento publico para agregar un servicio al form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRateManualToPrincipalForm(sender As Object, e As EventArgs)

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If rateManualDetail IsNot Nothing AndAlso rateManualDetail.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MRateManualDetail(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteRateManualDetail(rateManualDetail)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Dim args As AddRateManualToPrincipalFormEventArgs = New AddRateManualToPrincipalFormEventArgs
                        args.RateManualDetail = rateManualDetail
                        args.DeleteEntity = True
                        RaiseEvent AddRateManualToPrincipalForm(Nothing, args)
                        Deshacer()
                        HideButtons(1)
                        rateManualDetail = Nothing
                        Close()
                    Else
                        AsyncLoader(False)
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

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Dim errors As String = AddRateManualDetail()
        If errors <> String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            INDsleIPSService.Focus()
            Exit Sub
        End If
        AssigningValues()
        Using model As New MRateManualDetail(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveRateManualDetail(rateManualDetail)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If banSaveModify Then
                   Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                Else
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.rateManualDetail = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                Dim args As AddRateManualToPrincipalFormEventArgs = New AddRateManualToPrincipalFormEventArgs
                args.RateManualDetail = rateManualDetail
                args.DeleteEntity = False
                args.SaveModify = True
                RaiseEvent AddRateManualToPrincipalForm(Nothing, args)
                Deshacer()
                HideButtons(1)
                rateManualDetail = Nothing
                If banSaveModify = False Then
                    Close()
                End If
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

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        'FormSearchObjects = New FrmBusqueda
        'AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        'With FormSearchObjects
        '    .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
        '                      New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
        '    .ValorSolicitado = "Code"
        '    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMarketingUnit
        '    .FormParent = Me
        '    .ShowSearch()
        'End With
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), RateManualDescription, INDsleIPSService.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & rateManualDetail.RateManualId & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), INDsleIPSService.Text), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), RateManualDescription, INDsleIPSService.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), INDsleIPSService.Text)
            Return Me._doc
        End If
    End Function

#End Region

#Region "Methods"

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
    ''' Metodo que carga la informacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadInformation() As Task
        If rateManualDetail IsNot Nothing Then
            With rateManualDetail
                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                banSearch = False
                IPSServiceId = .IPSServiceId
                banSearch = True
                INDsleIPSService.Properties.NullText = .IPSServiceDescription
                'ScoreProcedure = .ScoreProcedure
                'DiscountPercentage = .DiscountPercentage
                'If .SurgicalGroupId IsNot Nothing Then
                '    INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                '    SurgicalGroupId = .SurgicalGroupId
                '    INDsleSurgicalGroup.Properties.NullText = .SurgicalGroupDescription
                'Else
                '    INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                '    SurgicalGroupId = Nothing
                '    INDsleSurgicalGroup.Properties.NullText = String.Empty
                'End If
                'OutPatientRecoveryFeeType = .OutPatientRecoveryFeeType
                'InPatientRecoveryFeeType = .InPatientRecoveryFeeType
                BarraBotones.StatusRecordVisible = True
                'Status = .Status

                'ListServiceFees = .ServiceFees.ToList
            End With

            INDgcServicesFees.DataSource = Nothing
            'INDgcServicesFees.DataSource = ListServiceFees
            INDsleIPSService.Focus()

            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & rateManualDetail.RateManualId)
            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(rateManualDetail.Id))
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = rateManualDetail.Id}
                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            End Using

            HideButtons(2)
            BarraBotones.SetDocuments(rateManualDetail.Id)
        Else
            Status = True
            HideButtons(1)
        End If
    End Function

    ''' <summary>
    ''' Metodo que oculta los botones de la barra de usuarios: 1=Save, 2=Update
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideButtons(ByVal options As Integer)
        If options = 1 Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        End If

    End Sub

    ''' <summary>
    ''' Metodo que agrega el servicio al formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Function AddRateManualDetail() As String
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Return errors.ToString
        End If

        If _listPivotIPSServiceId IsNot Nothing AndAlso _listPivotIPSServiceId.Count > 0 Then
            Dim ban As Integer = _listPivotIPSServiceId.FindAll(Function(x) x = IPSServiceId).ToList.Count
            If ban > 0 Then
                errors = ResourceManager.GetString("ExistIPSService", NAME_MODULE)
                Return errors.ToString
            End If
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Metodo que asigna valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If rateManualDetail Is Nothing Then
            rateManualDetail = New RateManualDetail
        End If
        With rateManualDetail
            .RateManualId = RateManualId
            .IPSServiceId = IPSServiceId
            .IPSServiceDescription = INDsleIPSService.Text
            '.ScoreProcedure = ScoreProcedure
            '.DiscountPercentage = DiscountPercentage
            If INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                '.SurgicalGroupId = SurgicalGroupId
                .SurgicalGroupDescription = INDsleSurgicalGroup.Text
            Else
                '.SurgicalGroupId = Nothing
                .SurgicalGroupDescription = String.Empty
            End If
            '.OutPatientRecoveryFeeType = OutPatientRecoveryFeeType
            '.OutPatientRecoveryFeeTypeDescription = INDsleOutPatientRecoveryFeeType.Text
            '.InPatientRecoveryFeeType = InPatientRecoveryFeeType
            .InPatientRecoveryFeeTypeDescription = INDsleInPatientRecoveryFeeType.Text
            'Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
            '    Case eActionsStatusRecords.Active
            '        .Status = True
            '    Case eActionsStatusRecords.Inactive
            '        .Status = False
            'End Select

            'For Each itemServicesFees As ServiceFees In ListServiceFees
            '    .ServiceFees.Add(itemServicesFees)
            'Next

            'If ListDeleteServiceFees IsNot Nothing Then
            '    For Each itemDelete As ServiceFees In ListDeleteServiceFees
            '        .ServiceFees.Add(itemDelete)
            '    Next
            'End If

        End With
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim listErrors As New StringBuilder
        If IPSServiceId = Nothing Then
            listErrors.AppendLine("- Debe elegir un Servicio IPS.")
        End If
        If INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SurgicalGroupId = Nothing Then
                listErrors.AppendLine("- Debe elegir un Grupo Quirúrgico.")
            End If
        End If
        If OutPatientRecoveryFeeType = Nothing Then
            listErrors.AppendLine("- Debe elegir una Liquidación Ingresos Ambulatorios.")
        End If
        If InPatientRecoveryFeeType = Nothing Then
            listErrors.AppendLine("- Debe elegir una Liquidación Ingresos Hospitalarios.")
        End If
        'If ListServiceFees Is Nothing OrElse ListServiceFees.Count = 0 Then
        '    listErrors.AppendLine("- Debe ingresar al menos un rango de valores.")
        'End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Llena el control de tipo de entidad
    ''' </summary>
    Private Sub InitializeSearchTuples()
        ListOutPatientRecoveryFeeType = New List(Of Tuple(Of Integer, String))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(2, "Cuota Moderadora"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(3, "Copago"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(4, "Bono"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(5, "Franquicia"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(6, "Otra"))
        INDsleOutPatientRecoveryFeeType.Properties.DataSource = ListOutPatientRecoveryFeeType.ToList

        ListInPatientRecoveryFeeType = New List(Of Tuple(Of Integer, String))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(2, "Cuota Moderadora"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(3, "Copago"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(4, "Bono"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(5, "Franquicia"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(6, "Otra"))
        INDsleInPatientRecoveryFeeType.Properties.DataSource = ListInPatientRecoveryFeeType.ToList

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPopupServices.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyServices.BeginUpdate()
        banSearch = False
        IPSServiceId = Nothing
        banSearch = True
        INDsleIPSService.Properties.NullText = String.Empty
        ScoreProcedure = 0
        DiscountPercentage = 0
        SurgicalGroupId = Nothing
        INDsleSurgicalGroup.Properties.NullText = String.Empty
        OutPatientRecoveryFeeType = Nothing
        InPatientRecoveryFeeType = Nothing
        INDgcServicesFees.DataSource = Nothing
        'ListServiceFees = Nothing
        'ListDeleteServiceFees = Nothing
        CleanControlsPopup()
        DeleteBlockedRecord()
        BarraBotones.CleanAuditBasic()
        INDlyServices.EndUpdate()
        INDsleIPSService.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        ContractMinimumWageId = Nothing
        INDsleContractMinimumWage.Properties.NullText = String.Empty
        InitialDate = Nothing
        EndDate = Nothing
        SalesValue = 0
        SalesValueWithSurcharge = 0
    End Sub

    ''' <summary>
    ''' Metodo que oculta los controles dependiendo del tipo de manual tarifario que viene del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsRateManual()
        If TypeRateManual = 1 OrElse TypeRateManual = 2 Then
            INDlyItemScoreProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemScoreProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que oculta los controles dependiendo de la presentacion del servicio ips escogido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsIPSServices()
        If _ipsServicesXpo.Presentation = 2 OrElse _ipsServicesXpo.Presentation = 3 Then
            INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega los rangos de valores a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddServicesFees()
        'Dim errors = ValidateControlsPopup()
        'If errors.Length > 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = errors
        '    INDsleContractMinimumWage.Focus()
        '    Exit Sub
        'End If

        'If ListServiceFees Is Nothing Then
        '    ListServiceFees = New List(Of ServiceFees)
        'Else
        '    Dim banExist As Boolean = False
        '    Dim exist As ServiceFees = ListServiceFees.Find(Function(item) (InitialDate >= item.InitialDate AndAlso InitialDate <= item.EndDate) OrElse (EndDate >= item.InitialDate AndAlso EndDate <= item.EndDate) OrElse (InitialDate < item.InitialDate) AndAlso (EndDate > item.EndDate))
        '    If exist IsNot Nothing AndAlso modeModify = False Then
        '        banExist = True
        '    ElseIf exist IsNot Nothing AndAlso modeModify = True Then
        '        If exist.InitialDate = _servicesFees.InitialDate AndAlso exist.EndDate = _servicesFees.EndDate Then
        '            banExist = False
        '        Else
        '            banExist = True
        '        End If
        '    End If
        '    If banExist = True Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DateRangesExist", NAME_MODULE)
        '        INDsleContractMinimumWage.Focus()
        '        Exit Sub
        '    End If
        'End If

        If modeModify = False Then
            'Dim servicesFees As New ServiceFees
            'With servicesFees
            '    .InitialDate = InitialDate
            '    .EndDate = EndDate
            '    .SalesValue = SalesValue
            '    .SalesValueWithSurcharge = SalesValueWithSurcharge
            '    .ContractMinimumWageId = ContractMinimumWageId
            '    .ContractMinimumWageDescription = INDsleContractMinimumWage.Text
            'End With
            'ListServiceFees.Add(servicesFees)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DateRangeAgregatedSatisfactory", NAME_MODULE)
        Else
            'With _servicesFees
            '    .ContractMinimumWageId = ContractMinimumWageId
            '    .ContractMinimumWageDescription = INDsleContractMinimumWage.Text
            '    .InitialDate = InitialDate
            '    .EndDate = EndDate
            '    .SalesValue = SalesValue
            '    .SalesValueWithSurcharge = SalesValueWithSurcharge
            '    If .Id > 0 Then
            '        .MarkAsModified()
            '    End If
            'End With
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DateRangeModifySatisfactory", NAME_MODULE)
        End If

        If rateManualDetail IsNot Nothing Then
            If rateManualDetail.Id > 0 Then
                rateManualDetail.MarkAsModified()
            End If
        End If
        modeModify = False
        INDgcServicesFees.DataSource = Nothing
        'INDgcServicesFees.DataSource = ListServiceFees
        CleanControlsPopup()
        INDsleContractMinimumWage.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que edita los rangos de valores de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditServicesFees()
        modeModify = True
        '_servicesFees = CType(viewGridServicesfees.GetFocusedRow, ServiceFees)
        'With _servicesFees
        '    ContractMinimumWageId = .ContractMinimumWageId
        '    INDsleContractMinimumWage.Properties.NullText = .ContractMinimumWageDescription
        '    InitialDate = .InitialDate
        '    EndDate = .EndDate
        '    SalesValue = .SalesValue
        '    SalesValueWithSurcharge = .SalesValueWithSurcharge
        'End With
        INDpceServicesFees.ShowPopup()
        INDsleContractMinimumWage.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina los rangos de valores de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteServicesFees()
        '_servicesFees = CType(viewGridServicesfees.GetFocusedRow, ServiceFees)
        'ListServiceFees.Remove(_servicesFees)

        'If rateManualDetail IsNot Nothing Then
        '    If rateManualDetail.Id > 0 Then
        '        If ListDeleteServiceFees Is Nothing Then
        '            ListDeleteServiceFees = New List(Of ServiceFees)
        '        End If
        '        _servicesFees.MarkAsDeleted()
        '        ListDeleteServiceFees.Add(_servicesFees)
        '        rateManualDetail.MarkAsModified()
        '    End If
        'End If

        'INDgcServicesFees.DataSource = Nothing
        'INDgcServicesFees.DataSource = ListServiceFees
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If ContractMinimumWageId = Nothing Then
            listErrors.AppendLine(ResourceManager.GetString("SelectContractMinimumWage", NAME_MODULE))
        End If
        If InitialDate Is Nothing Then
            listErrors.AppendLine(ResourceManager.GetString("SelectInitialDate", NAME_MODULE))
        End If
        If EndDate Is Nothing Then
            listErrors.AppendLine(ResourceManager.GetString("SelectEndDate", NAME_MODULE))
        End If
        If SalesValue = 0 Then
            listErrors.AppendLine(ResourceManager.GetString("InputServiceValue", NAME_MODULE))
        End If
        If SalesValueWithSurcharge = 0 Then
            listErrors.AppendLine(ResourceManager.GetString("InputServiceValueWithSurcharge", NAME_MODULE))
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If rateManualDetail.Id > 0 Then
            Dim state As Boolean
            Select Case Status
                Case CBool(eActionsStatusRecords.Active)
                    state = True
                Case CBool(eActionsStatusRecords.Inactive)
                    state = False
            End Select
            Using model As New MRateManualDetail(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.ChangeState(rateManualDetail.Id, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Dim args As AddRateManualToPrincipalFormEventArgs = New AddRateManualToPrincipalFormEventArgs
                    'rateManualDetail.Status = state
                    rateManualDetail.MarkAsUnchanged()
                    args.RateManualDetail = rateManualDetail
                    args.DeleteEntity = False
                    args.SaveModify = False
                    RaiseEvent AddRateManualToPrincipalForm(Nothing, args)
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _ipsServicesXpo = Nothing
        banSearch = Nothing
        modeModify = Nothing
        ListOutPatientRecoveryFeeType = Nothing
        ListInPatientRecoveryFeeType = Nothing
        rateManualDetail = Nothing
        _listPivotIPSServiceId = Nothing
        banSaveModify = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPopupServices(Me)
        Deshacer()
        HideControlsRateManual()
        INDsleOutPatientRecoveryFeeType.Properties.Buttons.Item(1).Visible = False
        INDsleInPatientRecoveryFeeType.Properties.Buttons.Item(1).Visible = False
        IndigoGridControl1.RefreshGrid(INDgcServicesFees)
        LoadStatus()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewGridServicesfees, ListActions)
        InitializeSearchTuples()
        LoadInformation()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de servicio ips para abrir el form correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleService_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIPSService.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmIPSService With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeIPSServices()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de grupo quirurgico para abrir el form correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSurgicalGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSurgicalGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSurgicalGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de salario minimo para abrir el form correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractMinimumWage_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContractMinimumWage.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContractMinimumWage With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContractMinimumWage()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de servicios ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSService_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIPSService.QueryPopUp
        If INDsleIPSService.Properties.DataSource Is Nothing Then
            Presenter.InitializeIPSServices()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSurgicalGroup.QueryPopUp
        If INDsleSurgicalGroup.Properties.DataSource Is Nothing Then
            Presenter.InitializeSurgicalGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de salario minimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractMinimumWage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractMinimumWage.QueryPopUp
        If INDsleContractMinimumWage.Properties.DataSource Is Nothing Then
            Presenter.InitializeContractMinimumWage()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar F4 o enter en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceServicesFees_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceServicesFees.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceServicesFees.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el FormPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupServices_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupServices_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleIPSService.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSService_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIPSService.EditValueChanged
        If IPSServiceId <> Nothing AndAlso banSearch = True Then
            _ipsServicesXpo = DirectCast(DirectCast(viewSearchIPSServices.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.ContractRepository.ContractIPSServiceXPO)
            HideControlsIPSServices()
        Else
            INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If INDdteInitialDate.EditValue IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = CDate(INDdteInitialDate.EditValue)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddServicesFees_Click(sender As Object, e As EventArgs) Handles INDbtnAddServicesFees.Click
        AddServicesFees()
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString()
            Case "Remove"
                DeleteServicesFees()
            Case "Edit"
                EditServicesFees()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action As DXMenuItem = DirectCast(sender, DXMenuItem)
        Select Case (action.Tag.ToString)
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditServicesFees()
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteServicesFees()
        End Select
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupServices_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#End Region

#Region "Barra Botones Events"

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