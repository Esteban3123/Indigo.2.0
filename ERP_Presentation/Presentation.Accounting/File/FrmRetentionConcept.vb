'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Diego Andrés Roldán
' Created          : 20-01-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
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
''' Formulario de Conceptos de retencion
''' </summary>
Public Class FrmRetentionConcept
    Implements IRetentionConcept, ICustomizableForm

#Region "Constant"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Variable and properties"

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' variable para instanciar el presentador
    ''' </summary>
    Dim Presenter As PRetentionConcept

    ''' <summary>
    ''' Entidad de concepto de retencion
    ''' </summary>
    Dim RetentionConcept As RetentionConcepts

    ''' <summary>
    ''' Controla si se debe abrir el menú emergente del PopupContainerEdit
    ''' </summary>
    Dim openPopup As Boolean

    ''' <summary>
    ''' Controla si se debe abrir el menú emergente del INDPceRetentionByCity
    ''' </summary>
    Dim openPopupRetentionCity As Boolean

    ''' <summary>
    ''' variable para registrar el bloqueo de los registros
    ''' </summary>
    Dim record As BlockRecordGeneralLedger

    ''' <summary>
    '''  Rangos de conceptos de retención
    ''' </summary>
    Dim retentionRange As RetentionConceptRanges

    ''' <summary>
    ''' Declara una lista de objetos de tipo RetentionConceptRanges para almacenar una colección de conceptos de retención
    ''' </summary>
    Dim listRetentionRange As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' Retenciones por ciudad
    ''' </summary>
    Dim retentionByCity As RetentionConceptByCity

    ''' <summary>
    ''' Lista de objetos que almacena las listas por ciudad
    ''' </summary>
    Dim listRetentionByCity As List(Of RetentionConceptByCity)

    ''' <summary>
    ''' Declara una lista de tuplas que contienen un entero y una cadena
    ''' </summary>
    Dim ListTypeRounding As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tuplas que contienen un byte y una cadena
    ''' </summary>
    Dim ListTypeRetention As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Lista de tuplas que contienen un byte y una cadena
    ''' </summary>
    Dim ListCalculationType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IRetentionConcept.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As GeneralLedgerSequence Implements IRetentionConcept.Sequense
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
    ''' Obtiene o establece el codigo del concepto
    ''' </summary>
    ''' <value>
    ''' The code concept.
    ''' </value>
    Public Property Code As String Implements IRetentionConcept.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la base minima
    ''' </summary>
    ''' <value>
    ''' The minimum base.
    ''' </value>
    Public Property MinBase As Decimal Implements IRetentionConcept.MinBase
        Get
            Return INDtxtMinimalBase.EditValue
        End Get
        Set(value As Decimal)
            INDtxtMinimalBase.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto
    ''' </summary>
    ''' <value>
    ''' The name concept.
    ''' </value>
    Public Property NameConcept As String Implements IRetentionConcept.NameConcept
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tasa de retencion
    ''' </summary>
    ''' <value>
    ''' The rate.
    ''' </value>
    Public Property Rate As Decimal Implements IRetentionConcept.Rate
        Get
            Return INDspnRate.EditValue
        End Get
        Set(value As Decimal)
            INDspnRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la retencion
    ''' </summary>
    ''' <value>
    ''' The retention.
    ''' </value>
    Public Property Retention As Byte Implements IRetentionConcept.Retention
        Get
            Return Iif(String.IsNullOrEmpty(INDGleRetentionType.EditValue), 0, INDGleRetentionType.EditValue)
        End Get
        Set(value As Byte)
            INDGleRetentionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de cálculo
    ''' </summary>
    ''' <value>
    ''' The retention.
    ''' </value>
    Public Property CalculationType As Byte Implements IRetentionConcept.CalculationType
        Get
            Return IIf(String.IsNullOrEmpty(INDGleRetentionType1.EditValue), 0, INDGleRetentionType1.EditValue)
        End Get
        Set(value As Byte)
            INDGleRetentionType1.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el estado del registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Public Property StateConcept As Boolean Implements IRetentionConcept.StateConcept
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
    ''' Obtiene las ciudades en el control de ciudad y lo establece
    ''' </summary>
    ''' <returns></returns>
    Public Property CityXpo As XPInstantFeedbackSource Implements IRetentionConcept.CityXpo
        Get
            Return CType(INDSleCity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRetentionConcept.ActionsOnControls
        Set(value As Boolean)
            INDlycRetentionConcept.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtMinimalBase.Enabled = value
            INDspnRate.Enabled = value
            INDGleRetentionType.Enabled = value
            INDGleRetentionType1.Enabled = Not value
            glTypeRounding.Enabled = value

            INDlycRetentionConcept.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RetentionConcept.Code, Me.RetentionConcept.Name, Me.RetentionConcept.Rate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.RetentionConcept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RetentionConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RetentionConcept.Code, Me.RetentionConcept.Name, Me.RetentionConcept.Rate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RetentionConcept.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlycRetentionConcept.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Me.RetentionConcept = Nothing

        Me.Code = String.Empty
        Me.NameConcept = String.Empty
        Me.Retention = Nothing
        Me.CalculationType = CByte(ResourceManager.GetString("CalculationType", "Payroll"))
        Me.MinBase = 0
        Me.Rate = 0
        INDGleRetentionType.EditValue = Nothing
        glTypeRounding.EditValue = Nothing
        StateConcept = True

        openPopup = False
        openPopupRetentionCity = False

        indtxValInitial.EditValue = 0
        indtxtValFinish.EditValue = 0
        indSpinPercentege.EditValue = 0
        indTxtValReducted.EditValue = 0
        indTxtValIncrement.EditValue = 0

        listRetentionRange = Nothing
        indGcRetentionRange.DataSource = Nothing

        INDSleCity.EditValue = Nothing
        INDSleCity.Properties.NullText = String.Empty
        INDtxtRate.EditValue = 0

        listRetentionByCity = Nothing
        INDGcRetentionByCity.DataSource = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRetentionConcept.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' desbloquea el registro
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MRetentionConcept(Me.Tag)
                Await Model.DeleteBlockRecordAccounting(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que llena la entidad con los valores de los controles
    ''' </summary>
    Private Sub AssigningValues()
        With RetentionConcept
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameConcept
            .Retention = CInt(INDGleRetentionType.EditValue)
            .CalculationType = CByte(ResourceManager.GetString("CalculationType", "Payroll"))
            .TypeRounding = CInt(glTypeRounding.EditValue)
            If INDGleRetentionType.EditValue = 2 Then
                .MinBase = 0
                .Rate = 0
            Else
                .MinBase = MinBase
                .Rate = Rate
            End If

            If listRetentionRange IsNot Nothing Then
                For Each item In listRetentionRange
                    If item.ChangeTracker.State = ObjectState.Added Then
                        RetentionConcept.RetentionConceptRanges.Add(item)
                    End If
                Next
            End If

            If listRetentionByCity IsNot Nothing Then
                For Each item In listRetentionByCity
                    If item.ChangeTracker.State = ObjectState.Added Then
                        RetentionConcept.RetentionConceptByCity.Add(item)
                    End If
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo concepto de retencion
    ''' </summary>
    Private Async Function NewRetentionConcept() As Task
        RetentionConcept = New RetentionConcepts() With {.Status = True}
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
                        Using model As New MRetentionConcept(CStr(Me.Tag))
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
            Retention = 1
        End If
    End Function

    ''' <summary>
    ''' metodo para limpiar los controles de los rangos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanRetentionRange()
        indtxValInitial.EditValue = 0
        indtxtValFinish.EditValue = 0
        indSpinPercentege.EditValue = 0
        indTxtValReducted.EditValue = 0
        indTxtValIncrement.EditValue = 0
        INDseUVTIncrement.EditValue = 0
        retentionRange = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles de la retencion por ciudad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanRetentionByCity()
        INDSleCity.EditValue = Nothing
        INDSleCity.Properties.NullText = String.Empty
        INDSleCity.Properties.ReadOnly = False
        INDtxtRate.EditValue = 0
        retentionByCity = Nothing
    End Sub

    ''' <summary>
    ''' metodo para validar que los campos de los rangos de retenciones no esten en 0
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRetentionRange() As Boolean
        If indtxtValFinish.EditValue = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Función que valida la retención según la ciudad y la tasa
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateRetentionByCity() As Boolean
        If INDSleCity.EditValue Is Nothing Then
            Return False
        End If
        If INDtxtRate.EditValue < 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Función que valida una lista de rangos de conceptos de retención
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateRetentionRangeList() As Boolean
        If listRetentionRange Is Nothing Then
            Return False
        End If
        If listRetentionRange.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida los conceptos de retención por ciudad en una lista
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateRetentionByCityList() As String
        Dim errors As New StringBuilder
        If listRetentionByCity Is Nothing OrElse ListRetentionByCity.Count = 0 Then
            errors.AppendLine("Los conceptos de retención por ciudad deben tener minimo definida una retención por ciudad")
        Else
            For Each cityId In listRetentionByCity.GroupBy(Function(d) d.CityId).Select(Function(d) d.Key)
                If listRetentionByCity.Where(Function(d) d.CityId = cityId).Count > 1 Then
                    Dim reteCity = listRetentionByCity.FirstOrDefault(Function(d) d.CityId = cityId)
                    errors.AppendLine("La ciudad " + reteCity.CityName + " se encuentra duplicada")
                End If
            Next
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Valida la base minima
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateBase() As String
        Dim errors As New StringBuilder
        If MinBase = 0 Then
            errors.AppendLine("- Debe ingresar un valor base mínimo.")
        End If
        If INDGleRetentionType.EditValue Is Nothing Then
            errors.AppendLine("- Debe definir un tipo de retencion")
        End If
        If glTypeRounding.EditValue Is Nothing Then
            errors.AppendLine("- Debe ingresar un tipo de redondeo.")
        End If
        If Rate < 0 Then
            errors.AppendLine("- El porcentaje de retención debe ser mayor o igual cero.")
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Metodo que inicializa los search que contengan tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListTypeRounding = New List(Of Tuple(Of Integer, String))
        ListTypeRounding = New List(Of Tuple(Of Integer, String))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(6, "0.01"))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(5, "0.1"))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(1, "1"))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(2, "10"))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(3, "100"))
        ListTypeRounding.Add(New Tuple(Of Integer, String)(4, "1000"))
        glTypeRounding.Properties.DataSource = ListTypeRounding

        ListTypeRetention = New List(Of Tuple(Of Byte, String))
        ListTypeRetention.Add(New Tuple(Of Byte, String)(1, "Base"))
        ListTypeRetention.Add(New Tuple(Of Byte, String)(2, "Rangos"))
        ListTypeRetention.Add(New Tuple(Of Byte, String)(3, "Variable"))
        ListTypeRetention.Add(New Tuple(Of Byte, String)(4, "Por Ciudad"))
        INDGleRetentionType.Properties.DataSource = ListTypeRetention

        ListCalculationType = New List(Of Tuple(Of Byte, String))
        ListCalculationType.Add(New Tuple(Of Byte, String)(1, "Progresivo"))
        ListCalculationType.Add(New Tuple(Of Byte, String)(2, "Lineal"))
        INDGleRetentionType1.Properties.DataSource = ListCalculationType
    End Sub

#End Region

#Region "Crud Base"

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.8}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConcept
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
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Async Sub Buscar() Implements IcrudBase.Buscar
        Await LoadControls()
    End Sub

    ''' <summary>
    ''' Carga los controles relacionados con el concepto de retención. 
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MRetentionConcept(CStr(Me.Tag))
                    AsyncLoader(True)
                    RetentionConcept = Await Model.GetRetentionConcept(INDbteCode.Text.Trim)
                    INDlycRetentionConcept.BeginUpdate()
                    If RetentionConcept IsNot Nothing AndAlso RetentionConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        record = Await Model.GetBlockRecordAccounting(CStr(Me.Tag), CStr(RetentionConcept.Id))
                        With RetentionConcept
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            NameConcept = .Name
                            MinBase = .MinBase
                            Rate = .Rate
                            StateConcept = .Status
                            Retention = .Retention
                            glTypeRounding.EditValue = .TypeRounding
                            CalculationType = CByte(ResourceManager.GetString("CalculationType", "Payroll"))

                            listRetentionRange = New List(Of RetentionConceptRanges)
                            For Each item In RetentionConcept.RetentionConceptRanges
                                listRetentionRange.Add(item)
                            Next
                            indGcRetentionRange.DataSource = Nothing
                            indGcRetentionRange.DataSource = listRetentionRange

                            listRetentionByCity = New List(Of RetentionConceptByCity)
                            For Each item In RetentionConcept.RetentionConceptByCity
                                item.CityName = item.City.Name
                                listRetentionByCity.Add(item)
                            Next
                            INDGcRetentionByCity.DataSource = Nothing
                            INDGcRetentionByCity.DataSource = listRetentionByCity
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.RetentionConcept.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecordAccounting(
                                New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = RetentionConcept.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(RetentionConcept.Id, Me.Tag.ToString(), Nothing, GetType(RetentionConcepts).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRetentionConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRetentionConcept.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
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

        If Me.RetentionConcept IsNot Nothing AndAlso Me.RetentionConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MRetentionConcept(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteRetentionConcept(Me.RetentionConcept)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = IIf(result.StatusCode = eStatusResult.SUCCESS, ResourceManager.GetString("RecordDeleted"), result.Message)
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = True Then
            If Retention = 2 Then
                If ValidateRetentionRangeList() = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("RetentionRangeList", NAME_MODULE)
                    Exit Sub
                End If
            Else
                Dim errors As String = ValidateBase()

                If Retention = 4 AndAlso errors.Length = 0 Then
                    errors = ValidateRetentionByCityList()
                End If

                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors
                    Exit Sub
                End If
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MRetentionConcept(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of RetentionConcepts) = Await Model.SaveRetentionConcept(Me.RetentionConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If RetentionConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.RetentionConcept = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewRetentionConcept()
        End If
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento load donde hacemos las configuraciones iniciales del formulario
    ''' </summary>
    Private Async Sub FrmRetentionConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRetentionConcept, True)
        Await Me.LayoutControls.LoadDefinitionAsync()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PRetentionConcept(Me)
        Presenter.GetSequense()

        Me.LoadStatus()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGwRetentionRange, ListActions)
        IndigoGridControl1.RefreshGrid(indGcRetentionRange)

        ListActions = New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(INDGvRetentionByCity, ListActions)
        IndigoGridControl1.RefreshGrid(INDGcRetentionByCity)

        InitializeTuples()
    End Sub

    ''' <summary>
    ''' Evento que desbloquea el registro antes de cerrar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmRetentionConcept_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ExistDefinitionFront = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        RetentionConcept = Nothing
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing

        retentionRange = Nothing
        listRetentionRange = Nothing

        retentionByCity = Nothing
        listRetentionByCity = Nothing
        
        ListTypeRounding = Nothing
        ListTypeRetention = Nothing

        CityXpo = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.RetentionConcept IsNot Nothing AndAlso Me.RetentionConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que consulta El concepto de retencion por codigo
    ''' </summary>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewRetentionConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDGleRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGleRetentionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRetentionType.EditValueChanged
        If Retention = 2 Then
            retentionGroupRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgRetentionByCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRetentionType1.ShowLayout

            INDlyItemRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlycItemMinimalBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            'Validación para ocultar o mostrar columnas de acuerdo al tipo de cálculo
            If CalculationType = 2 Then
                GridColumn4.HideColumn
                colDeduction.HideColumn
                colIncrement.HideColumn
                lyitemIncrement.HideControl
                lyItemDeduction.HideControl
                INDlyItemUVTIncrement.HideControl
            Else
                GridColumn4.ShowColumn
                colDeduction.ShowColumn
                colIncrement.ShowColumn
            End If
        ElseIf Retention = 4 Then
            retentionGroupRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgRetentionByCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRetentionType1.HideControl

            INDlyItemRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlycItemMinimalBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            retentionGroupRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgRetentionByCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRetentionType1.HideControl

            INDlyItemRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlycItemMinimalBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el usuario intenta abrir el menú desplegable del control de Ciudad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCity.QueryPopUp
        If CityXpo Is Nothing Then
            Me.presenter.InitializeCityXPO()
        End If
    End Sub

    ''' <summary>
    ''' Se activa cuando el usuario intenta abrir el menú emergente del PopupContainerEdit1
    ''' </summary>
    Private Sub PopupContainerEdit1_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PopupContainerEdit1.QueryPopUp
        If openPopup = False Then
            CleanRetentionRange()
        Else
            openPopup = False
        End If
    End Sub

    ''' <summary>
    '''  Se activa cuando el usuario intenta abrir el menú emergente del INDPceRetentionByCity
    ''' </summary>
    Private Sub INDPceRetentionByCity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDPceRetentionByCity.QueryPopUp
        If openPopupRetentionCity = False Then
            CleanRetentionByCity()
        Else
            openPopupRetentionCity = False
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar clic en agregar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AddRangeRetention()
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar clic en agregar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnRetentionByCity_Click(sender As Object, e As EventArgs) Handles INDBtnRetentionByCity.Click
        AddRangeRetentionByCity()
    End Sub

    ''' <summary>
    ''' evento qie se dispara al presionar clic en un boton de la columna de acciones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        retentionRange = DirectCast(INDGwRetentionRange.GetFocusedRow(), RetentionConceptRanges)
        Dim selectedItem = listRetentionRange.IndexOf(retentionRange)
        Select Case btn.Tag
            Case "Remove"
                If retentionRange.RetentionId > 0 Then
                    RetentionConcept.RetentionConceptRanges.Item(selectedItem).MarkAsDeleted()
                End If
                listRetentionRange.Remove(retentionRange)
                indGcRetentionRange.DataSource = Nothing
                indGcRetentionRange.DataSource = listRetentionRange
                If RetentionConcept.ChangeTracker.State = ObjectState.Unchanged Then
                    RetentionConcept.MarkAsModified()
                End If
                CleanRetentionRange()
            Case "Edit"
                PopupContainerEdit1.Focus()
                indtxValInitial.EditValue = retentionRange.ValueInitial
                indtxtValFinish.EditValue = retentionRange.ValueFinish

                'If retentionRange.Percentage IsNot Nothing Then
                indSpinPercentege.EditValue = retentionRange.Percentage
                'End If

                'If retentionRange.ValueDeducted IsNot Nothing Then
                indTxtValReducted.EditValue = retentionRange.ValueDeducted
                'End If

                'If retentionRange.ValueIncrement IsNot Nothing Then
                indTxtValIncrement.EditValue = retentionRange.ValueIncrement
                ' End If
                INDseUVTIncrement.EditValue = retentionRange.UVTIncrement
                indtxValInitial.Focus()
                openPopup = True
                PopupContainerEdit1.ShowPopup()
        End Select
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar click en un botón de la columna de acciones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        retentionByCity = DirectCast(INDGvRetentionByCity.GetFocusedRow(), RetentionConceptByCity)
        Dim selectedItem = listRetentionByCity.IndexOf(retentionByCity)
        Select Case sender.Tag
            Case "Remove"
                If retentionByCity.Id > 0 Then
                    RetentionConcept.RetentionConceptByCity.Item(selectedItem).MarkAsDeleted()
                End If

                listRetentionByCity.Remove(retentionByCity)
                indGcretentionByCity.DataSource = Nothing
                indGcretentionByCity.DataSource = listRetentionByCity
                If RetentionConcept.ChangeTracker.State = ObjectState.Unchanged Then
                    RetentionConcept.MarkAsModified()
                End If

                CleanRetentionByCity()
            Case "Edit"
                INDPccRetentionByCity.Focus()

                INDSleCity.EditValue = retentionByCity.CityId
                INDSleCity.Properties.NullText = retentionByCity.CityName
                INDSleCity.Properties.ReadOnly = True
                INDtxtRate.EditValue = retentionByCity.Rate
                INDtxtRate.Focus()

                openPopupRetentionCity = True
                INDPceRetentionByCity.ShowPopup()
        End Select
    End Sub

    ''' <summary>
    ''' Método que agrega un rango de retención a la lista de rangos de retención.
    ''' </summary>
    Private Sub AddRangeRetention()
        If ValidateRetentionRange() = False Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If listRetentionRange Is Nothing Then
            listRetentionRange = New List(Of RetentionConceptRanges)
        End If
        If retentionRange Is Nothing Then
            retentionRange = New RetentionConceptRanges
        End If
        With retentionRange
            retentionRange.ValueInitial = indtxValInitial.EditValue
            retentionRange.ValueFinish = indtxtValFinish.EditValue

            retentionRange.Percentage = indSpinPercentege.EditValue
            retentionRange.ValueDeducted = indTxtValReducted.EditValue
            retentionRange.ValueIncrement = indTxtValIncrement.EditValue
            retentionRange.UVTIncrement = INDseUVTIncrement.EditValue
        End With
        If retentionRange.ValueInitial >= retentionRange.ValueFinish Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueInitial", NAME_MODULE)
            Exit Sub
        End If

        For Each item In listRetentionRange
            If (retentionRange.ValueInitial > item.ValueInitial And retentionRange.ValueInitial < item.ValueFinish) Or (retentionRange.ValueFinish > item.ValueInitial And retentionRange.ValueFinish < item.ValueFinish) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UsedRange", NAME_MODULE)
                Exit Sub
            End If
        Next
        If RetentionConcept.ChangeTracker.State = ObjectState.Unchanged Then
            RetentionConcept.MarkAsModified()
        End If
        If retentionRange.Id > 0 Then
            For Each item In RetentionConcept.RetentionConceptRanges
                If item.Equals(retentionRange) Then
                    item.ValueInitial = retentionRange.ValueInitial
                    item.ValueFinish = retentionRange.ValueFinish
                    item.Percentage = retentionRange.Percentage
                    item.ValueDeducted = retentionRange.ValueDeducted
                    item.ValueIncrement = retentionRange.ValueIncrement
                    item.UVTIncrement = retentionRange.UVTIncrement
                    item.MarkAsModified()
                End If
            Next
            retentionRange.MarkAsModified()
        Else
            retentionRange.MarkAsAdded()
        End If
        If listRetentionRange.Count > 0 Then
            listRetentionRange.Remove(retentionRange)
        End If
        listRetentionRange.Add(retentionRange)
        indGcRetentionRange.DataSource = Nothing
        indGcRetentionRange.DataSource = listRetentionRange
        CleanRetentionRange()
        indtxValInitial.Focus()
        PopupContainerEdit1.ClosePopup()
    End Sub

    ''' <summary>
    '''  Esta función agrega un concepto de retención por ciudad a la lista de conceptos por ciudad.
    ''' </summary>
    Private Sub AddRangeRetentionByCity()
        If ValidateRetentionByCity() = False Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        If listRetentionByCity Is Nothing Then
            listRetentionByCity = New List(Of RetentionConceptByCity)
        End If

        If retentionByCity Is Nothing Then
            If listRetentionByCity.Any(Function(d) d.CityId = INDSleCity.EditValue) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "La ciudad " + INDSleCity.Text + " ya se encuentra agregada"
                Exit Sub
            End If

            retentionByCity = New RetentionConceptByCity
            retentionByCity.CityId = INDSleCity.EditValue
            retentionByCity.CityName = INDSleCity.Text
        End If

        retentionByCity.Rate = INDtxtRate.EditValue

        If retentionByCity.Id = 0 Then
            retentionByCity.MarkAsAdded()
        Else
            retentionByCity.MarkAsModified()
        End If

        If listRetentionByCity.Count > 0 Then
            listRetentionByCity.Remove(retentionByCity)
        End If

        listRetentionByCity.Add(retentionByCity)
        INDGcRetentionByCity.DataSource = Nothing
        INDGcRetentionByCity.DataSource = listRetentionByCity

        CleanRetentionRange()
        INDSleCity.Focus()
        INDPceRetentionByCity.ClosePopup()
    End Sub

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Evento del boton nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Try
            Using model As New MRetentionConcept(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state = Not RetentionConcept.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    RetentionConcept = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' Este constructor se ejecuta cuando se crea una nueva instancia del formulario
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

End Class