'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/08/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Payments.MVP

#End Region

Public Class FrmDistributionLines
    Implements IDistributionLines

#Region "Properties"

    ''' <summary>
    ''' Concepto de acreencia
    ''' </summary>
    ''' <returns></returns>
    Public Property AccusationConcept As Integer? Implements IDistributionLines.AccusationConcept
        Get
            Return INDsleAccusationConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccusationConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Medicion posterior del instrumento financiero
    ''' </summary>
    ''' <returns></returns>
    Public Property FinancialInstrument As Integer? Implements IDistributionLines.FinancialInstrument
        Get
            Return INDsleFinancialInstrument.EditValue
        End Get
        Set(value As Integer?)
            INDsleFinancialInstrument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptType As Integer? Implements IDistributionLines.ConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptId As Integer? Implements IDistributionLines.ConceptId
        Get
            Return INDsleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDistributionLines.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.ConceptXpo
        Get
            Return INDsleConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConcept.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingUnitId As Integer? Implements IDistributionLines.OperatingUnitId
        Get
            Return INDsleOperatingUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleOperatingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingUnitXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.OperatingUnitXpo
        Get
            Return INDsleOperatingUnit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOperatingUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IDistributionLines.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.AccountXpo
        Get
            Return CType(INDsleMainAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    '''<summary>
    ''' Establece el DataSource de cuenta contable provision
    '''</summary>
    Public Property AccountCostProvision As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.AccountCostProvision
        Get
            Return CType(INDsleMainAccountCostProvision.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccountCostProvision.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IDistributionLines.Code
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
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer? Implements IDistributionLines.IdAccount
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del combobox seleccionado de la cuenta contable provision
    ''' </summary>
    Public Property IdAccountCostProvisionId As Integer? Implements IDistributionLines.IdAccountCostProvisionId
        Get
            Return INDsleMainAccountCostProvision.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccountCostProvision.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdPaymentConcept As Integer? Implements IDistributionLines.IdPaymentConcept
        Get
            Return INDslePaymentConcept.EditValue
        End Get
        Set(value As Integer?)
            INDslePaymentConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptId As Integer? Implements IDistributionLines.AccountPayableConceptId
        Get
            Return INDsleAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPayableConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameDL As String Implements IDistributionLines.NameDL
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y establece el id de la cuenta contable del concepto de nota cxp
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountAccountPayableConceptNotesId As Integer Implements IDistributionLines.MainAccountAccountPayableConceptNotesId
        Get
            Return INDsleMainAccountResult.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccountResult.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaymentConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.PaymentConceptXpo
        Get
            Return CType(INDslePaymentConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePaymentConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.AccountPayableConceptXpo
        Get
            Return CType(INDsleAccountPayableConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As PaymentsSecuence Implements IDistributionLines.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PaymentsSecuence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequense.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Status As Boolean Implements IDistributionLines.Status
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


    Public Property AccountPayableConceptNote As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.AccountPayableConceptNote
        Get
            Return CType(INDsleConceptNotePPDiscount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConceptNotePPDiscount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtienbe o establece el xpo de las cuentas contables para el campo cuenta contable-desceunto pronto pago inventarios
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountsXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDistributionLines.MainAccountsXpo
        Get
            Return CType(INDsleMainAccountResult.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccountResult.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListConceptType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAccusationConcept As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListFinancialInstrument As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Presentador de ConceptsAccountsPayable
    ''' </summary>
    Dim Presenter As PDistributionLines

    ''' <summary>
    ''' Variable que contiene la entidad de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim distributionLines As DistributionLines

	''' <summary>
	''' Nombre del módulo al que pertenece el frontal
	''' </summary>
	Private Const NAME_MODULE As String = "Commons"

	''' <summary>
	''' Instancia de los valores de sesión
	''' </summary>
	Private _indigoSession As SessionValues

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la informacion sacada del control de conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim expenseConceptXpo As ExpenseConceptXpo

    ''' <summary>
    ''' Variable que contiene la informacion sacada del control de conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim conceptRetentionXpo As Infrastructure.Data.Xpo.AccountingRepository.RetentionConceptXpo

    ''' <summary>
    ''' Listado del detalle de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDistributionLinesDetail As List(Of DistributionLinesDetail)

    ''' <summary>
    ''' representa la entidad de detalle de lineas de distirbucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim distributionLineDetail As DistributionLinesDetail

    ''' <summary>
    ''' Respresenta la entidad de conceptos de tipo retencion para la linea de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim distributionLineICARetention As DistributionLinesICARetention

    ''' <summary>
    ''' Lista de eliminados de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteDistributionLinesDetail As List(Of DistributionLinesDetail)

    ''' <summary>
    ''' Lista de conceptos de cxp de tipo retencion para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDistributionLineICARetention As List(Of DistributionLinesICARetention)

    ''' <summary>
    ''' Lista de eliminados de conceptos de cxp de tipo retencion para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteDistributionLineICARetention As List(Of DistributionLinesICARetention)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
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

        If Me.distributionLines IsNot Nothing AndAlso Me.distributionLines.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MDistributionLines(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteDistributionLines(Me.distributionLines)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
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
            Using Model As New MDistributionLines(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of DistributionLines) = Await Model.SaveDistributionLines(Me.distributionLines, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If distributionLines.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.distributionLines = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewDistributionLines()
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Llena los datasource de los controles de modo de impresion y control terminacion contrato
    ''' </summary>
    Private Sub InitializeSearch()
        ListConceptType = New List(Of Tuple(Of Integer, String))
        ListConceptType.Add(New Tuple(Of Integer, String)(1, "Declarante"))
        ListConceptType.Add(New Tuple(Of Integer, String)(2, "No Declarante"))
        INDsleConceptType.Properties.DataSource = ListConceptType.ToList
        INDrepSleConceptType.DataSource = ListConceptType.ToList

        ListAccusationConcept = New List(Of Tuple(Of Integer, String))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(1, "Prestación de servicios de salud"))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(2, "Insumos y medicamentos"))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(3, "Dispositivo médico o equipo biomédico"))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(4, "Administrativo"))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(5, "Restitución de recursos"))
        ListAccusationConcept.Add(New Tuple(Of Integer, String)(6, "Otro"))
        INDsleAccusationConcept.Properties.DataSource = ListAccusationConcept.ToList

        ListFinancialInstrument = New List(Of Tuple(Of Integer, String))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(1, "Precio de la Transacción / Valor Nominal / Costo"))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(2, "Costo Amortizado"))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(3, "Valor Razonable"))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(4, "Valor Razonable con cambios en el ORI"))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(5, "Valor Presente Pagos Futuros"))
        ListFinancialInstrument.Add(New Tuple(Of Integer, String)(6, "No aplica"))
        INDsleFinancialInstrument.Properties.DataSource = ListFinancialInstrument.ToList
    End Sub

    ''' <summary>
    ''' Valida los listados que estan en las rejillas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As Boolean
        'If ListDistributionLineICARetention Is Nothing OrElse ListDistributionLineICARetention.Count = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "No hay detalles de conceptos de retención ICA."
        '    Return False
        'End If
        'If ListDistributionLinesDetail Is Nothing OrElse ListDistributionLinesDetail.Count = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "No hay detalles de conceptos de retención."
        '    Return False
        'End If
        Return True
    End Function

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim dld As DistributionLinesDetail = CType(viewDetail.GetFocusedRow, DistributionLinesDetail)
        If dld.Id <> 0 Then
            If ListDeleteDistributionLinesDetail Is Nothing Then
                ListDeleteDistributionLinesDetail = New List(Of DistributionLinesDetail)
            End If
            dld.MarkAsDeleted()
            ListDeleteDistributionLinesDetail.Add(dld)
        End If
        ListDistributionLinesDetail.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListDistributionLinesDetail
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteConcept()
        Dim dlICA As DistributionLinesICARetention = CType(viewConcepts.GetFocusedRow, DistributionLinesICARetention)
        If dlICA.Id <> 0 Then
            If ListDeleteDistributionLineICARetention Is Nothing Then
                ListDeleteDistributionLineICARetention = New List(Of DistributionLinesICARetention)
            End If
            dlICA.MarkAsDeleted()
            ListDeleteDistributionLineICARetention.Add(dlICA)
        End If
        ListDistributionLineICARetention.Remove(dlICA)
        INDgcRetention.DataSource = Nothing
        INDgcRetention.DataSource = ListDistributionLineICARetention
    End Sub

    ''' <summary>
    ''' Agrega el detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        If ValidateControlsPopup() = True Then
            If ListDistributionLinesDetail Is Nothing Then
                ListDistributionLinesDetail = New List(Of DistributionLinesDetail)
            End If
            If ListDistributionLinesDetail.Any(Function(item) item.ConceptType = ConceptType AndAlso item.AccountPayableConceptId = AccountPayableConceptId) Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributionLineDetailExist")
                INDsleAccountPayableConcept.Focus()
                Exit Sub
            End If

            CreateDistributionLineDetail()
            ListDistributionLinesDetail.Add(distributionLineDetail)
            If distributionLines.Id > 0 Then
                distributionLines.MarkAsModified()
            End If
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = ListDistributionLinesDetail
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLinesDetailAgregateSatisfactory")
            AccountPayableConceptId = Nothing
            ConceptType = Nothing
            INDsleAccountPayableConcept.Focus()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            INDsleAccountPayableConcept.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Agrega los conceptos de cxp y la unidad operativa en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddConcepts()
        If ValidateControlsPopupConcepts() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            INDsleOperatingUnit.Focus()
            Exit Sub
        End If

        If ListDistributionLineICARetention Is Nothing Then
            ListDistributionLineICARetention = New List(Of DistributionLinesICARetention)
        End If

        If ListDistributionLineICARetention.Any(Function(item) item.OperatingUnitId = OperatingUnitId) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitExist")
            INDsleOperatingUnit.Focus()
            Exit Sub
        End If

        CreateDistributionLineICARetention()
        ListDistributionLineICARetention.Add(distributionLineICARetention)
        If distributionLines.Id > 0 Then
            distributionLines.MarkAsModified()
        End If
        INDgcRetention.DataSource = Nothing
        INDgcRetention.DataSource = ListDistributionLineICARetention
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConceptAgregatedSatisfactory")
        CleanControlsPopup()
        INDsleOperatingUnit.Focus()
    End Sub

    ''' <summary>
    ''' Crea el objeto para el listado de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDistributionLineDetail()
        distributionLineDetail = New DistributionLinesDetail
        With distributionLineDetail
            .AccountPayableConceptId = AccountPayableConceptId
            .DescriptionAccountPayableConcept = INDsleAccountPayableConcept.Text
            .ConceptType = ConceptType
        End With
    End Sub

    ''' <summary>
    ''' Crea el objeto para el listado de Ica retention para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDistributionLineICARetention()
        distributionLineICARetention = New DistributionLinesICARetention
        With distributionLineICARetention
            .OperatingUnitId = OperatingUnitId
            .DescriptionOperatingUnit = INDsleOperatingUnit.Text
            .AccountPayableConceptId = ConceptId
            .DescriptionConcept = INDsleConcept.Text
        End With
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded

        If Me.distributionLines IsNot Nothing AndAlso Me.distributionLines.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDistributionLines.ActionsOnControls
        Set(value As Boolean)
            INDlyDistributionLines.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleMainAccount.Enabled = value
            INDsleMainAccountCostProvision.Enabled = value
            INDsleAccusationConcept.Enabled = value
            INDsleFinancialInstrument.Enabled = value
            INDslePaymentConcept.Enabled = value
            INDsleAccountPayableConcept.Enabled = value
            INDbtnAddDetail.Enabled = value
            INDpceCxpRetention.Enabled = value
            INDgcDetail.Enabled = value
            INDpceRetention.Enabled = value
            INDgcRetention.Enabled = value
            INDsleConceptNotePPDiscount.Enabled = value
            INDlyDistributionLines.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        If AccountPayableConceptId Is Nothing Then
            Return False
        End If
        If ConceptType Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Cuenta Contable", .FieldName = "IdMainAccount.NumberName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Concepto Egreso", .FieldName = "ExpensesConceptId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDistributionLine
            .FormParent = Me
            .ShowSearch()
        End With
        'SearchMode = True
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me.distributionLines.Code, Me.distributionLines.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.distributionLines.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me.distributionLines.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me.distributionLines.Code, Me.distributionLines.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me.distributionLines.Code)
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

        INDlyDistributionLines.BeginUpdate()

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
        INDbtnCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDsleMainAccount.EditValue = Nothing
        INDsleMainAccount.Properties.NullText = String.Empty
        INDsleMainAccountCostProvision.EditValue = Nothing
        INDsleMainAccountCostProvision.Properties.NullText = String.Empty
        IdPaymentConcept = Nothing
        INDslePaymentConcept.Properties.NullText = String.Empty
        AccountPayableConceptId = Nothing
        INDsleAccountPayableConcept.Properties.NullText = String.Empty
        ConceptType = Nothing
        AccusationConcept = Nothing
        FinancialInstrument = Nothing
        INDsleConceptType.Properties.NullText = String.Empty
        INDgcDetail.DataSource = Nothing
        ListDistributionLinesDetail = Nothing
        ListDeleteDistributionLinesDetail = Nothing
        distributionLines = Nothing
        INDgcRetention.DataSource = Nothing
        ListDistributionLineICARetention = Nothing
        ListDeleteDistributionLineICARetention = Nothing
        INDsleConceptNotePPDiscount.EditValue = Nothing
        INDsleConceptNotePPDiscount.Properties.NullText = Nothing
        INDsleMainAccountResult.EditValue = Nothing
        CleanControlsPopup()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyDistributionLines.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        OperatingUnitId = Nothing
        INDsleOperatingUnit.Properties.NullText = String.Empty
        ConceptId = Nothing
        INDsleConcept.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupConcepts() As Boolean
        If OperatingUnitId Is Nothing Then
            Return False
        End If
        If ConceptId Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With distributionLines
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameDL
            .Description = NameDL
            .IdMainAccount = IdAccount
            .ExpensesConceptId = IdPaymentConcept
            .AccusationConcept = AccusationConcept
            .FinancialInstrument = FinancialInstrument
            .AccountPayableConceptNotesId = INDsleConceptNotePPDiscount.EditValue
            .MainAccountAccountPayableConceptNotesId = MainAccountAccountPayableConceptNotesId
            .MainAccountCostProvisionId = IdAccountCostProvisionId

            If ListDistributionLinesDetail IsNot Nothing Then
                For Each itemDetail As DistributionLinesDetail In ListDistributionLinesDetail
                    .DistributionLinesDetail.Add(itemDetail)
                Next
            End If
            If ListDeleteDistributionLinesDetail IsNot Nothing Then
                For Each itemDetail As DistributionLinesDetail In ListDeleteDistributionLinesDetail
                    .DistributionLinesDetail.Add(itemDetail)
                Next
                .MarkAsModified()
            End If

            If ListDistributionLineICARetention IsNot Nothing Then
                For Each itemDetail As DistributionLinesICARetention In ListDistributionLineICARetention
                    .DistributionLinesICARetention.Add(itemDetail)
                Next
            End If
            If ListDeleteDistributionLineICARetention IsNot Nothing Then
                For Each itemDetail As DistributionLinesICARetention In ListDeleteDistributionLineICARetention
                    .DistributionLinesICARetention.Add(itemDetail)
                Next
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task

        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MDistributionLines(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If

    End Function

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
                Using Model As New MDistributionLines(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetDistributionLines(INDbtnCode.Text.Trim)
                    distributionLines = resultOperation.ObjectEmbbeded
                    INDlyDistributionLines.BeginUpdate()
                    If distributionLines IsNot Nothing AndAlso distributionLines.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(distributionLines.Id))
                        With distributionLines
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad
                            Code = .Code
                            NameDL = .Name
                            Presenter.InitializeMainAccount()
                            IdAccount = .IdMainAccount
                            Presenter.InitializePaymentConcept(IdAccount)
                            IdPaymentConcept = .ExpensesConceptId

                            AccusationConcept = .AccusationConcept
                            FinancialInstrument = .FinancialInstrument
                            Status = .Status
                            ListDistributionLinesDetail = .DistributionLinesDetail.ToList
                            ListDistributionLineICARetention = .DistributionLinesICARetention.ToList
                            INDsleConceptNotePPDiscount.EditValue = .AccountPayableConceptNotesId
                            INDsleConceptNotePPDiscount.Properties.NullText = .AccountPayableConceptNoteName

                            If .MainAccountCostProvisionId IsNot Nothing Then
                                Presenter.InitializeMainAccountCostProvision()
                                IdAccountCostProvisionId = .MainAccountCostProvisionId
                            End If

                            If .MainAccountAccountPayableConceptNotesId IsNot Nothing Then
                                If MainAccountsXpo Is Nothing Then
                                    MainAccountsXpo = Presenter.InitializeMainAccountsResult()
                                End If
                                MainAccountAccountPayableConceptNotesId = .MainAccountAccountPayableConceptNotesId
                            End If
                        End With
                        'Llenar NullText
                        INDgcDetail.DataSource = Nothing
                        INDgcDetail.DataSource = ListDistributionLinesDetail

                        INDgcRetention.DataSource = Nothing
                        INDgcRetention.DataSource = ListDistributionLineICARetention

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.distributionLines.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = distributionLines.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(distributionLines.Id, Me.Tag.ToString(), Nothing, GetType(DistributionLines).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequense.IsManual Then
                            Await Me.NewDistributionLines()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyDistributionLines.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewDistributionLines() As Task

        distributionLines = New DistributionLines() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
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
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
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
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task

        If Not String.IsNullOrEmpty(Me.distributionLines.Code) Then
            Try
                Using model As New MDistributionLines(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not distributionLines.Status
                    Dim result As ActionResult(Of DistributionLines) = Await model.ChangeState(Me.distributionLines.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.distributionLines = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListConceptType = Nothing
        ListAccusationConcept = Nothing
        ListFinancialInstrument = Nothing
        SearchMode = Nothing
        record = Nothing
        Presenter = Nothing
        distributionLines = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        expenseConceptXpo = Nothing
        conceptRetentionXpo = Nothing
        ListDistributionLinesDetail = Nothing
        distributionLineDetail = Nothing
        distributionLineICARetention = Nothing
        ListDeleteDistributionLinesDetail = Nothing
        ListDistributionLineICARetention = Nothing
        ListDeleteDistributionLineICARetention = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDistributionLines_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeSearch()

        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PDistributionLines(Me)
        Presenter.GetSequense()
        'Presenter.LoadDefinitionLayout()

        IndigoGridControl1.RefreshGrid(INDgcDetail)
        IndigoGridControl1.RefreshGrid(INDgcRetention)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewDetail, ListActions)
        IndigoGridView2.SetListAcction(viewConcepts, ListActions)
        INDsleConceptType.Properties.Buttons(1).Visible = False
        'SearchMode = False

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 70
            End If
        Next
        LoadStatus()
        Deshacer()

    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDistributionLines_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown


        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewDistributionLines()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar la teclas correspondientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceRetention_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceRetention.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceRetention.ShowPopup()
            INDsleOperatingUnit.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDpceRetention.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 en el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceCxpRetention_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceCxpRetention.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceCxpRetention.ShowPopup()
            INDsleAccountPayableConcept.Focus()
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
    Private Sub FrmDistributionLines_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
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
    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable provision
    ''' </summary>
    Private Sub INDsleMainAccountCostProvision_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountCostProvision.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeMainAccountCostProvision()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de conceptos de egreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePaymentConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePaymentConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmExpenseConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            If INDsleMainAccount.EditValue IsNot Nothing Then
                Presenter.InitializePaymentConcept(INDsleMainAccount.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountPayableConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("725", INDsleConcept.EditValue, True)
            INDsleConcept.Properties.DataSource = Nothing
            Presenter.InitializeAccountPayableConcept()
            INDsleAccountPayableConcept.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de unidad operativa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOperatingUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleOperatingUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1511", Nothing, True)
            INDsleOperatingUnit.Properties.DataSource = Nothing
            Presenter.InitializeOperatingUnit()
            INDpceRetention.ShowPopup()
            INDsleOperatingUnit.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("725", INDsleConcept.EditValue, True)
            INDsleConcept.Properties.DataSource = Nothing
            Presenter.InitializeConcept()
            INDpceRetention.ShowPopup()
            INDsleConcept.Focus()
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
    Private Sub INDsleMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccount.QueryPopUp
        If INDsleMainAccount.Properties.DataSource Is Nothing Then
            Presenter.InitializeMainAccount()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable provision
    ''' </summary>
    Private Sub INDsleMainAccountCostProvision_Popup_1(sender As Object, e As EventArgs) Handles INDsleMainAccountCostProvision.Popup
        If INDsleMainAccountCostProvision.Properties.DataSource Is Nothing Then
            Presenter.InitializeMainAccountCostProvision()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePaymentConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePaymentConcept.QueryPopUp
        'If INDslePaymentConcept.Properties.DataSource Is Nothing Then
        '    Presenter.InitializePaymentConcept()
        'End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountPayableConcept.QueryPopUp
        If INDsleAccountPayableConcept.Properties.DataSource Is Nothing Then
            Presenter.InitializeAccountPayableConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad operativa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOperatingUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOperatingUnit.QueryPopUp
        If OperatingUnitXpo Is Nothing Then
            Presenter.InitializeOperatingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConcept.QueryPopUp
        If ConceptXpo Is Nothing Then
            Presenter.InitializeConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que consulta los conceptos de nota debido de cuentas por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConceptNotePPDiscount_Popup(sender As Object, e As EventArgs) Handles INDsleConceptNotePPDiscount.Popup
        If AccountPayableConceptNote Is Nothing Then
            Presenter.InitializeAccountPayableConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' evento que consulta las cuentas contables de del concepto nota cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLedgerAccount_Popup(sender As Object, e As EventArgs) Handles INDsleMainAccountResult.Popup
        If MainAccountsXpo Is Nothing Then
            MainAccountsXpo = Presenter.InitializeMainAccountsResult()
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
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceConcepts_Click(sender As Object, e As EventArgs)
        INDslePaymentConcept.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRetention_Click(sender As Object, e As EventArgs) Handles INDbtnAddRetention.Click
        AddConcepts()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de conceptos de pagos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePaymentConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePaymentConcept.EditValueChanged
        'If INDslePaymentConcept.EditValue > 0 AndAlso INDslePaymentConcept.EditValue IsNot Nothing Then
        '    expenseConceptXpo = DirectCast(DirectCast(viewConceptPayments.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.ExpenseConceptXpo)
        'End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar de valor el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMainAccount.EditValueChanged
        If INDsleMainAccount.EditValue IsNot Nothing Then
            Presenter.InitializePaymentConcept(INDsleMainAccount.EditValue)
        Else
            IdPaymentConcept = Nothing
            INDslePaymentConcept.Properties.DataSource = Nothing
        End If
    End Sub




#End Region

#Region "ContextMenuGridControl"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        DeleteConcept()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        DeleteConcept()
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
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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

        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class