'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Common
Imports Presentation.Common
Imports Presentation.Accounting
Imports System.Text

#End Region

Public Class FrmConceptsAccountsPayable
    Implements IConceptsAccountsPayable, ICustomizableForm

#Region "Properties and variables"

    ''' <summary>
    ''' Obtiene o establece si el concepto es de tipo empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeCategoryRetention As Boolean Implements IConceptsAccountsPayable.EmployeeCategoryRetention
        Get
            Return INDsleEmployeeCategoryRetention.EditValue
        End Get
        Set(value As Boolean)
            INDsleEmployeeCategoryRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Concepto de retencion 384
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThreeEightFourRetentionConceptId As Integer? Implements IConceptsAccountsPayable.ThreeEightFourRetentionConceptId
        Get
            Return INDsleThreeEightFourRetentionConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleThreeEightFourRetentionConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de retencion 384
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThreeEightFourRetentionConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.ThreeEightFourRetentionConceptIdXpo
        Get
            Return INDsleThreeEightFourRetentionConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThreeEightFourRetentionConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Concepto de retencion 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThreeEightThreeRetentionConceptId As Integer? Implements IConceptsAccountsPayable.ThreeEightThreeRetentionConceptId
        Get
            Return INDsleThreeEightThreeRetentionConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleThreeEightThreeRetentionConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de retencion 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThreeEightThreeRetentionConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.ThreeEightThreeRetentionConceptIdXpo
        Get
            Return INDsleThreeEightThreeRetentionConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThreeEightThreeRetentionConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConceptId As Integer? Implements IConceptsAccountsPayable.RetentionConceptId
        Get
            Return INDsleRetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleRetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.RetentionConceptXpo
        Get
            Return INDsleRetentionConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptType As Integer? Implements IConceptsAccountsPayable.ConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si acumula para calculo presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccumulateBudgetCalculated As Boolean Implements IConceptsAccountsPayable.AccumulateBudgetCalculated
        Get
            Return INDrgAccumulateCP.EditValue
        End Get
        Set(value As Boolean)
            INDrgAccumulateCP.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si libera recursos no ejecutados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreeResourcesUnexecuted As Boolean Implements IConceptsAccountsPayable.FreeResourcesUnexecuted
        Get
            Return INDrgFreeResources.EditValue
        End Get
        Set(value As Boolean)
            INDrgFreeResources.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConceptsAccountsPayable.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IConceptsAccountsPayable.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequence As PaymentsSecuence Implements IConceptsAccountsPayable.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PaymentsSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequence.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de un concepto de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeConceptsAccountsPayable As String Implements IConceptsAccountsPayable.CodeConceptsAccountsPayable
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el concepto de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandlesRetention As Boolean Implements IConceptsAccountsPayable.HandlesRetention
        Get
            Return CBool(INDrbgHandlesRetention.EditValue)
        End Get
        Set(value As Boolean)
            INDrbgHandlesRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el concepto de maneja impuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandleTaxes As Boolean Implements IConceptsAccountsPayable.HandleTaxes
        Get
            Return CBool(INDrbgHandleTaxes.EditValue)
        End Get
        Set(value As Boolean)
            INDrbgHandleTaxes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el concepto maneja causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeferredCausation As Boolean Implements IConceptsAccountsPayable.DeferredCausation
        Get
            Return INDrbgDeferredCausation.EditValue
        End Get
        Set(value As Boolean)
            INDrbgDeferredCausation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de un concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameConceptsAccountsPayable As String Implements IConceptsAccountsPayable.NameConceptsAccountsPayable
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IConceptsAccountsPayable.Status
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
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Contiene el listado de las cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountsXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.AccountsXpo
        Get
            Return CType(INDgleAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleAccount.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' contiene listado de cuentas contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Account383Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.Account383Xpo
        Get
            Return CType(INDgleAccount383.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleAccount383.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' contiene listado de cuentas contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Account384Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsAccountsPayable.Account384Xpo
        Get
            Return CType(INDgleAccount384.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleAccount384.Properties.DataSource = value
        End Set
    End Property



    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer? Implements IConceptsAccountsPayable.IdAccount
        Get
            Return INDgleAccount.EditValue
        End Get
        Set(value As Integer?)
            INDgleAccount.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount383 As Integer? Implements IConceptsAccountsPayable.IdAccount383
        Get
            Return INDgleAccount383.EditValue
        End Get
        Set(value As Integer?)
            INDgleAccount383.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount384 As Integer? Implements IConceptsAccountsPayable.IdAccount384
        Get
            Return INDgleAccount384.EditValue
        End Get
        Set(value As Integer?)
            INDgleAccount384.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Presentador de ConceptsAccountsPayable
    ''' </summary>
    Dim Presenter As PConceptsAccountsPayable

    ''' <summary>
    ''' Variable que contiene la entidad de conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim paymentConcept As AccountPayableConcepts

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListConceptType As New List(Of Tuple(Of Integer, String))

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
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.paymentConcept IsNot Nothing AndAlso Me.paymentConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MConceptsAccountsPayable(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePaymentConceptAsync(Me.paymentConcept)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
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
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MConceptsAccountsPayable(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of AccountPayableConcepts) = Await Model.SavePaymentConceptAsync(Me.paymentConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If paymentConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.paymentConcept = result.ObjectEmbbeded
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
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPaymentsConcept()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.paymentConcept IsNot Nothing AndAlso Me.paymentConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que inicializa los search que maneja tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeSearch()
        ListConceptType = New List(Of Tuple(Of Integer, String))
        ListConceptType.Add(New Tuple(Of Integer, String)(1, "General"))
        ListConceptType.Add(New Tuple(Of Integer, String)(2, "Específico"))
        INDsleConceptType.Properties.DataSource = ListConceptType.ToList
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConceptsAccountsPayable.ActionsOnControls
        Set(value As Boolean)
            INDlycConceptsAccountsPayable.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleConceptType.Enabled = value
            INDgleAccount.Enabled = value
            INDrbgHandlesRetention.Enabled = value
            INDrbgHandleTaxes.Enabled = value
            INDsleRetentionConcept.Enabled = value
            INDrbgDeferredCausation.Enabled = value
            INDrgAccumulateCP.Enabled = value
            INDrgFreeResources.Enabled = value
            INDlycConceptsAccountsPayable.EndUpdate()
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
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ConceptTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Cuenta", .FieldName = "AccountNumberName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Maneja Retención", .FieldName = "HandlesRetentionName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                             New ColumnInfo() With {.Caption = "Concepto Retención", .FieldName = "RetentionConceptCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}
                             }.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListConceptsAccountsPayable
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
        DeleteBlockedRecord()
        CodeConceptsAccountsPayable = ReturnValue
        If CodeConceptsAccountsPayable IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentConcept.Code, Me.paymentConcept.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.paymentConcept.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentConcept.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentConcept.Code, Me.paymentConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentConcept.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlycConceptsAccountsPayable.BeginUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        'Limpiar controles

        CodeConceptsAccountsPayable = String.Empty
        NameConceptsAccountsPayable = String.Empty
        ConceptType = Nothing
        IdAccount = Nothing
        INDgleAccount.Properties.NullText = String.Empty
        IdAccount383 = Nothing
        INDgleAccount383.Properties.NullText = String.Empty
        IdAccount384 = Nothing
        INDgleAccount384.Properties.NullText = String.Empty
        HandlesRetention = Nothing
        HandleTaxes = Nothing
        RetentionConceptId = Nothing
        INDsleRetentionConcept.Properties.NullText = String.Empty

        EmployeeCategoryRetention = Nothing
        ThreeEightThreeRetentionConceptId = Nothing
        INDsleThreeEightThreeRetentionConceptId.Properties.NullText = String.Empty
        ThreeEightFourRetentionConceptId = Nothing
        INDsleThreeEightFourRetentionConceptId.Properties.NullText = String.Empty

        HideControlsRetention()

        DeferredCausation = Nothing
        AccumulateBudgetCalculated = Nothing
        FreeResourcesUnexecuted = Nothing

        BarraBotones.CleanAuditBasic()
        'INDlycConceptsAccountsPayable.EndUpdate()

        INDlyItemRetentionConcept.AllowHide = True
        INDlyItemThreeEightThreeRetentionConceptId.AllowHide = True
        INDlyItemThreeEightThreeRetentionAccountId.AllowHide = True
        INDlyItemAccount.AllowHide = True
        paymentConcept = Nothing
        ActionsOnControls = False


        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycConceptsAccountsPayable.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub HideControlsRetention()
        INDlyItemRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRetentionConcept.AllowHide = True
        INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemEmployeeCategoryRetention.AllowHide = True
        INDlyItemThreeEightThreeRetentionConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemThreeEightThreeRetentionConceptId.AllowHide = True
        INDlyItemThreeEightThreeRetentionAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemThreeEightThreeRetentionAccountId.AllowHide = True
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With paymentConcept
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeConceptsAccountsPayable
            .Name = NameConceptsAccountsPayable
            .ConceptType = ConceptType
            If INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdAccount = IdAccount
            Else
                .IdAccount = Nothing
            End If

            If INDlyItemThreeEightThreeRetentionAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ThreeEightThreeAccountId = IdAccount383
            Else
                .ThreeEightThreeAccountId = Nothing
            End If

            .ThreeEightFourAccountId = Nothing

            .HandlesRetention = HandlesRetention
            If INDlyItemRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RetentionConceptId = RetentionConceptId
            Else
                .RetentionConceptId = Nothing
            End If

            .HandleTaxes = HandleTaxes

            If INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EmployeeCategoryRetention = EmployeeCategoryRetention
            Else
                .EmployeeCategoryRetention = False
            End If
            If INDlyItemThreeEightThreeRetentionConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ThreeEightThreeRetentionConceptId = ThreeEightThreeRetentionConceptId
            Else
                .ThreeEightThreeRetentionConceptId = Nothing
            End If
            .ThreeEightFourRetentionConceptId = Nothing
            .DeferredCausation = DeferredCausation
            .AccumulatedBudgetCalculation = AccumulateBudgetCalculated
            .FreeResourcesUnexecuted = FreeResourcesUnexecuted
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        'Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
        '    If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '        Await Model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End If
        'End Using

        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeConceptsAccountsPayable) AndAlso Not String.IsNullOrWhiteSpace(CodeConceptsAccountsPayable) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MConceptsAccountsPayable(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetPaymentConceptAsync(INDbteCode.Text.Trim)
                    INDlycConceptsAccountsPayable.BeginUpdate()
                    paymentConcept = resultOperation.ObjectEmbbeded
                    If paymentConcept IsNot Nothing AndAlso paymentConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(paymentConcept.Id))
                            With paymentConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                CodeConceptsAccountsPayable = .Code
                                NameConceptsAccountsPayable = .Name
                                ConceptType = .ConceptType
                                INDgleAccount.Properties.NullText = .NumberNameAccount
                                INDgleAccount383.Properties.NullText = .NumberNameAccountThreeEightThree
                                IdAccount = .IdAccount
                                IdAccount383 = .ThreeEightThreeAccountId
                                IdAccount384 = Nothing
                                HandleTaxes = .HandleTaxes
                                HandlesRetention = .HandlesRetention
                                EmployeeCategoryRetention = .EmployeeCategoryRetention
                                RetentionConceptId = .RetentionConceptId
                                ThreeEightThreeRetentionConceptId = .ThreeEightThreeRetentionConceptId
                                INDsleThreeEightThreeRetentionConceptId.Properties.NullText = .RetentionConceptThreeDescription
                                ThreeEightFourRetentionConceptId = Nothing
                                INDsleRetentionConcept.Properties.NullText = .RetentionConceptDescription
                                DeferredCausation = .DeferredCausation
                                AccumulateBudgetCalculated = .AccumulatedBudgetCalculation
                                FreeResourcesUnexecuted = .FreeResourcesUnexecuted
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.paymentConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = paymentConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(paymentConcept.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConcepts).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            EnableorDisableHandleTaxes(Not Me.HandlesRetention)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPaymentsConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeConceptsAccountsPayable = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycConceptsAccountsPayable.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPaymentsConcept() As Task
        Me.paymentConcept = New AccountPayableConcepts() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeConceptsAccountsPayable = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeConceptsAccountsPayable = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeConceptsAccountsPayable = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeConceptsAccountsPayable = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.paymentConcept.Code) Then
            Try
                Using model As New MConceptsAccountsPayable(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not paymentConcept.Status
                    Dim result As ActionResult(Of AccountPayableConcepts) = Await model.ChangeState(Me.paymentConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.paymentConcept = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Evento que valida para activar o desactivar el campo maneja impuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnableorDisableHandleTaxes(Value As Boolean)
        INDrbgHandleTaxes.Enabled = Value
        If Not Value Then
            Me.HandleTaxes = Value
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        paymentConcept = Nothing
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        ListConceptType = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmConceptsAccountsPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.LayoutControls.SetIsCustomizable(Me.INDlycConceptsAccountsPayable, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PConceptsAccountsPayable(Me)
        AsyncLoader(True)
        Await Presenter.GetSequense()
        AsyncLoader(False)
        InitializeSearch()
        LoadStatus()
        Deshacer()
        ' SearchMode = False

    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsAccountsPayable_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
        '            Me.NewPaymentsConcept()
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
                If Not String.IsNullOrEmpty(CodeConceptsAccountsPayable.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeConceptsAccountsPayable) Then
                    Await Me.NewPaymentsConcept()
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
    Private Sub FrmConceptsAccountsPayable_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount383_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleAccount383.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccountRetencion383()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount384_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleAccount384.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccountRetencion384()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRetentionConcept.ButtonClick, INDsleThreeEightFourRetentionConceptId.ButtonClick, INDsleThreeEightThreeRetentionConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRetentionConcept With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAllRetentions()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleAccount.QueryPopUp
        If INDgleAccount.Properties.DataSource Is Nothing Then
            Presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount383_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleAccount383.QueryPopUp
        If INDgleAccount383.Properties.DataSource Is Nothing Then
            Presenter.InitializeAccountRetencion383()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount384_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleAccount384.QueryPopUp
        If INDgleAccount384.Properties.DataSource Is Nothing Then
            Presenter.InitializeAccountRetencion384()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRetentionConcept.QueryPopUp
        If RetentionConceptXpo Is Nothing Then
            Presenter.InitializeRetentionConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThreeEightThreeRetentionConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThreeEightThreeRetentionConceptId.QueryPopUp
        If ThreeEightThreeRetentionConceptIdXpo Is Nothing Then
            Presenter.InitializeRetentionConceptRangeThree()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThreeEightFourRetentionConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThreeEightFourRetentionConceptId.QueryPopUp
        If ThreeEightFourRetentionConceptIdXpo Is Nothing Then
            Presenter.InitializeRetentionConceptRangeFour()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptType.EditValueChanged
        If ConceptType IsNot Nothing Then
            If ConceptType = 1 Then
                INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAccount.AllowHide = True
                If HandlesRetention Then
                    INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemEmployeeCategoryRetention.AllowHide = False
                Else
                    INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemEmployeeCategoryRetention.AllowHide = True
                End If
            Else
                INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAccount.AllowHide = False
                INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemEmployeeCategoryRetention.AllowHide = True
                HideControlsRetention()
            End If
        Else
            INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAccount.AllowHide = True
        End If
        HandlesRetention = False
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si maneja concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrbgHandlesRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDrbgHandlesRetention.EditValueChanged
        If INDrbgHandlesRetention.EditValue IsNot Nothing Then
            EnableorDisableHandleTaxes(Not HandlesRetention)
            If HandlesRetention Then
                INDsleEmployeeCategoryRetention.Enabled = True
                If INDsleConceptType.EditValue = 2 Then
                    INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemEmployeeCategoryRetention.AllowHide = False
                Else
                    INDlyItemEmployeeCategoryRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemEmployeeCategoryRetention.AllowHide = False
                End If
                EmployeeCategoryRetention = False
                INDlyItemRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemRetentionConcept.AllowHide = False
                INDlyItemThreeEightThreeRetentionConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemThreeEightThreeRetentionConceptId.AllowHide = True
            Else
                HideControlsRetention()

            End If
        Else
            HideControlsRetention()
        End If
    End Sub



    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEmployeeCategoryRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEmployeeCategoryRetention.EditValueChanged
        If EmployeeCategoryRetention Then
            INDsleThreeEightThreeRetentionConceptId.Enabled = True
            INDgleAccount383.Enabled = True
            INDlyItemRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRetentionConcept.AllowHide = True
            INDlyItemThreeEightThreeRetentionConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemThreeEightThreeRetentionConceptId.AllowHide = False
            INDlyItemThreeEightThreeRetentionAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemThreeEightThreeRetentionAccountId.AllowHide = False
        Else
            INDlyItemRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemRetentionConcept.AllowHide = False
            INDlyItemThreeEightThreeRetentionConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemThreeEightThreeRetentionConceptId.AllowHide = True
            INDlyItemThreeEightThreeRetentionAccountId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemThreeEightThreeRetentionAccountId.AllowHide = True
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento de la barra de botones
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
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
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PaymentsSecuenceDetail IsNot Nothing Then
        '    If Me._sequense.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If

        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class