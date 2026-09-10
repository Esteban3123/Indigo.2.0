'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
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
Imports System.Text
Imports Presentation.Contract.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.ContractRepository
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

Public Class FrmContractAccountingStructure
    Implements IContractAccountingStructure, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Cuenta nif
    ''' </summary>
    ''' <returns></returns>
    Public Property ServicesPendingBillingMainAccountId As Integer? Implements IContractAccountingStructure.ServicesPendingBillingMainAccountId
        Get
            Return INDsleServicesPendingBillingMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleServicesPendingBillingMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta nif
    ''' </summary>
    ''' <returns></returns>
    Public Property ServicesPendingBillingMainAccountXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.ServicesPendingBillingMainAccountXpo
        Get
            Return INDsleServicesPendingBillingMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleServicesPendingBillingMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreditProvisionAccountId As Integer? Implements IContractAccountingStructure.CreditProvisionAccountId
        Get
            Return INDsleCreditProvisionAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCreditProvisionAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreditProvisionAccountIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.CreditProvisionAccountIdXpo
        Get
            Return INDsleCreditProvisionAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCreditProvisionAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DebitProvisionAccountId As Integer? Implements IContractAccountingStructure.DebitProvisionAccountId
        Get
            Return INDsleDebitProvisionAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleDebitProvisionAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DebitProvisionAccountIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.DebitProvisionAccountIdXpo
        Get
            Return INDsleDebitProvisionAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDebitProvisionAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name1 As String Implements IContractAccountingStructure.Name
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountRecoveryFeeId As Integer? Implements IContractAccountingStructure.AccountRecoveryFeeId
        Get
            Return INDsleAccountRecoveryFeeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountRecoveryFeeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountRecoveryFeeIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountRecoveryFeeIdXpo
        Get
            Return INDsleAccountRecoveryFeeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountRecoveryFeeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountParticularId As Integer? Implements IContractAccountingStructure.AccountParticularId
        Get
            Return INDsleAccountParticularId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountParticularId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountParticularIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountParticularIdXpo
        Get
            Return INDsleAccountParticularId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountParticularId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountWithoutRadicateId As Integer? Implements IContractAccountingStructure.AccountWithoutRadicateId
        Get
            Return INDsleAccountWithoutRadicateId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountWithoutRadicateId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountWithoutRadicateIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountWithoutRadicateIdXpo
        Get
            Return INDsleAccountWithoutRadicateId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountWithoutRadicateId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountRadicateId As Integer? Implements IContractAccountingStructure.AccountRadicateId
        Get
            Return INDsleAccountRadicateId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountRadicateId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountRadicateIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountRadicateIdXpo
        Get
            Return INDsleAccountRadicateId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountRadicateId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountObjectionRemediedId As Integer? Implements IContractAccountingStructure.AccountObjectionRemediedId
        Get
            Return INDsleAccountObjectionRemediedId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountObjectionRemediedId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountObjectionRemediedIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountObjectionRemediedIdXpo
        Get
            Return INDsleAccountObjectionRemediedId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountObjectionRemediedId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de conciliacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountConciliationId As Integer? Implements IContractAccountingStructure.AccountConciliationId
        Get
            Return INDsleAccountConciliationId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountConciliationId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de las cuentas de conciliacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountConciliationIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountConciliationIdXpo
        Get
            Return INDsleAccountConciliationId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountConciliationId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountLegalCollectionId As Integer? Implements IContractAccountingStructure.AccountLegalCollectionId
        Get
            Return INDsleAccountLegalCollectionId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountLegalCollectionId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountLegalCollectionIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountLegalCollectionIdXpo
        Get
            Return INDsleAccountLegalCollectionId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountLegalCollectionId.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountHardCollectionId As Integer? Implements IContractAccountingStructure.AccountHardCollectionId
        Get
            Return INDsleAccountHardCollectionId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountHardCollectionId.EditValue = value
        End Set
    End Property

    Public Property AccountHardCollectionIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountHardCollectionIdXpo
        Get
            Return INDsleAccountHardCollectionId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountHardCollectionId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountDebitOrderId As Integer? Implements IContractAccountingStructure.AccountDebitOrderId
        Get
            Return INDsleAccountDebitOrderId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountDebitOrderId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountDebitOrderIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountDebitOrderIdXpo
        Get
            Return INDsleAccountDebitOrderId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountDebitOrderId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountCreditOrderId As Integer? Implements IContractAccountingStructure.AccountCreditOrderId
        Get
            Return INDsleAccountCreditOrderId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountCreditOrderId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountCreditOrderIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.AccountCreditOrderIdXpo
        Get
            Return INDsleAccountCreditOrderId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountCreditOrderId.Properties.DataSource = value
        End Set
    End Property

    Public Property CreditAccountDeteriorationId As Integer? Implements IContractAccountingStructure.CreditAccountDeteriorationId
        Get
            Return INDsleCreditAccountDeteriorationId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCreditAccountDeteriorationId.EditValue = value
        End Set
    End Property

    Public Property CreditAccountDeteriorationIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.CreditAccountDeteriorationIdXpo
        Get
            Return INDsleCreditAccountDeteriorationId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCreditAccountDeteriorationId.Properties.DataSource = value
        End Set
    End Property

    Public Property DebitAccountDeteriorationId As Integer? Implements IContractAccountingStructure.DebitAccountDeteriorationId
        Get
            Return INDsleDebitAccountDeteriorationId.EditValue
        End Get
        Set(value As Integer?)
            INDsleDebitAccountDeteriorationId.EditValue = value
        End Set
    End Property

    Public Property DebitAccountDeteriorationIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.DebitAccountDeteriorationIdXpo
        Get
            Return INDsleDebitAccountDeteriorationId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDebitAccountDeteriorationId.Properties.DataSource = value
        End Set
    End Property

    Public Property PreviousPeriodReversalAccountDeteriorationId As Integer? Implements IContractAccountingStructure.PreviousPeriodReversalAccountDeteriorationId
        Get
            Return INDslePreviousPeriodReversalAccountDeteriorationId.EditValue
        End Get
        Set(value As Integer?)
            INDslePreviousPeriodReversalAccountDeteriorationId.EditValue = value
        End Set
    End Property

    Public Property PreviousPeriodReversalAccountDeteriorationIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.PreviousPeriodReversalAccountDeteriorationIdXpo
        Get
            Return INDslePreviousPeriodReversalAccountDeteriorationId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePreviousPeriodReversalAccountDeteriorationId.Properties.DataSource = value
        End Set
    End Property

    Public Property ReversalAccountDeteriorationId As Integer? Implements IContractAccountingStructure.ReversalAccountDeteriorationId
        Get
            Return INDsleReversalAccountDeteriorationId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReversalAccountDeteriorationId.EditValue = value
        End Set
    End Property

    Public Property ReversalAccountDeteriorationIdXpo As XPInstantFeedbackSource Implements IContractAccountingStructure.ReversalAccountDeteriorationIdXpo
        Get
            Return INDsleReversalAccountDeteriorationId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReversalAccountDeteriorationId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupType As Integer? Implements IContractAccountingStructure.CareGroupType
        Get
            Return INDsleCareGroupType.EditValue
        End Get
        Set(value As Integer?)
            INDsleCareGroupType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IContractAccountingStructure.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IContractAccountingStructure.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As ContractSequence Implements IContractAccountingStructure.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
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
    Public Property Code As String Implements IContractAccountingStructure.Code
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
    ''' Obtiene o establece el estado de los registros de los conceptos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IContractAccountingStructure.Status
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

#End Region

#Region "Variables"
    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PContractAccountingStructure

    ''' <summary>
    ''' Variable que contiene la entidad de conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractAccountingStructure As ContractAccountingStructure

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListCareGroupType As New List(Of Tuple(Of Integer, String))

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
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If ContractAccountingStructure IsNot Nothing AndAlso ContractAccountingStructure.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MContractAccountingStructure(Me.Tag.ToString())
                        AsyncLoader(True)
                        ContractAccountingStructure.MarkAsDeleted()
                        Dim result = Await Model.DeleteContractAccountingStructure(ContractAccountingStructure)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
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
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = True Then
            AssigningValues()
            Try
                Using model As New MContractAccountingStructure(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await model.SaveContractAccountingStructure(ContractAccountingStructure, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If ContractAccountingStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf ContractAccountingStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                        Me.ContractAccountingStructure = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewContractAccountingStructure()
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
        If Me.ContractAccountingStructure IsNot Nothing AndAlso Me.ContractAccountingStructure.Id > 0 Then
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
    ''' Metodo que inicializa los datasources de los search que se cargan con tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListCareGroupType = New List(Of Tuple(Of Integer, String))
        ListCareGroupType.Add(New Tuple(Of Integer, String)(1, "EAPB Con contrato"))
        ListCareGroupType.Add(New Tuple(Of Integer, String)(2, "EAPB Sin Contrato"))
        ListCareGroupType.Add(New Tuple(Of Integer, String)(3, "Particulares"))
        ListCareGroupType.Add(New Tuple(Of Integer, String)(4, "Aseguradoras"))
        INDsleCareGroupType.Properties.DataSource = ListCareGroupType.ToList
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractAccountingStructure.ActionsOnControls
        Set(value As Boolean)
            INDlyContractAccountingStructure.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleCareGroupType.Enabled = value
            INDsleAccountRecoveryFeeId.Enabled = value
            INDsleAccountParticularId.Enabled = value
            INDsleAccountWithoutRadicateId.Enabled = value
            INDsleAccountRadicateId.Enabled = value
            INDsleAccountObjectionRemediedId.Enabled = value
            INDsleAccountConciliationId.Enabled = value
            INDsleAccountLegalCollectionId.Enabled = value
            INDsleAccountHardCollectionId.Enabled = value
            INDsleAccountDebitOrderId.Enabled = value
            INDsleAccountCreditOrderId.Enabled = value
            INDsleDebitAccountDeteriorationId.Enabled = value
            INDsleCreditAccountDeteriorationId.Enabled = value
            INDsleReversalAccountDeteriorationId.Enabled = value
            INDslePreviousPeriodReversalAccountDeteriorationId.Enabled = value
            INDsleCreditProvisionAccountId.Enabled = value
            INDsleDebitProvisionAccountId.Enabled = value
            INDsleServicesPendingBillingMainAccount.Enabled = value
            INDlyContractAccountingStructure.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo Grupo Atención", .FieldName = "CareGroupTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListContractAccountingStructure
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
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractAccountingStructure.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.ContractAccountingStructure.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractAccountingStructure.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractAccountingStructure.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractAccountingStructure.Code)
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
        INDlyContractAccountingStructure.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Name1 = String.Empty
        CareGroupType = Nothing

        AccountRecoveryFeeId = Nothing
        INDsleAccountRecoveryFeeId.Properties.NullText = String.Empty
        AccountParticularId = Nothing
        INDsleAccountParticularId.Properties.NullText = String.Empty
        AccountWithoutRadicateId = Nothing
        INDsleAccountWithoutRadicateId.Properties.NullText = String.Empty
        AccountRadicateId = Nothing
        INDsleAccountRadicateId.Properties.NullText = String.Empty
        AccountObjectionRemediedId = Nothing
        INDsleAccountObjectionRemediedId.Properties.NullText = String.Empty
        AccountConciliationId = Nothing
        INDsleAccountConciliationId.Properties.NullText = String.Empty
        AccountLegalCollectionId = Nothing
        INDsleAccountLegalCollectionId.Properties.NullText = String.Empty
        AccountHardCollectionId = Nothing
        INDsleAccountHardCollectionId.Properties.NullText = String.Empty
        AccountDebitOrderId = Nothing
        INDsleAccountDebitOrderId.Properties.NullText = String.Empty
        AccountCreditOrderId = Nothing
        INDsleAccountCreditOrderId.Properties.NullText = String.Empty
        DebitAccountDeteriorationId = Nothing
        INDsleDebitAccountDeteriorationId.Properties.NullText = String.Empty
        CreditAccountDeteriorationId = Nothing
        INDsleCreditAccountDeteriorationId.Properties.NullText = String.Empty
        ReversalAccountDeteriorationId = Nothing
        INDsleReversalAccountDeteriorationId.Properties.NullText = String.Empty
        PreviousPeriodReversalAccountDeteriorationId = Nothing
        INDslePreviousPeriodReversalAccountDeteriorationId.Properties.NullText = String.Empty
        CreditProvisionAccountId = Nothing
        INDsleCreditProvisionAccountId.Properties.NullText = String.Empty
        DebitProvisionAccountId = Nothing
        INDsleDebitProvisionAccountId.Properties.NullText = String.Empty

        INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAccountRecoveryFeeId.AllowHide = True
        INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAccountParticularId.AllowHide = True
        INDlyItemAccountWithoutRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAccountWithoutRadicateId.AllowHide = True
        INDlyItemAccountRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAccountRadicateId.AllowHide = True

        ServicesPendingBillingMainAccountId = Nothing
        INDsleServicesPendingBillingMainAccount.Properties.NullText = String.Empty

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyContractAccountingStructure.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With ContractAccountingStructure
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name1
            .CareGroupType = CareGroupType

            If INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountRecoveryFeeId = AccountRecoveryFeeId
            Else
                .AccountRecoveryFeeId = Nothing
            End If
            If INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountParticularId = AccountParticularId
            Else
                .AccountParticularId = Nothing
            End If
            .AccountWithoutRadicateId = AccountWithoutRadicateId
            .AccountRadicateId = AccountRadicateId
            If INDlyItemAccountObjectionRemediedId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountObjectionRemediedId = AccountObjectionRemediedId
            Else
                .AccountObjectionRemediedId = Nothing
            End If
            If INDlyItemAccountConciliationId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountConciliationId = AccountConciliationId
            Else
                .AccountConciliationId = Nothing
            End If
            If INDlyItemAccountLegalCollectionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountLegalCollectionId = AccountLegalCollectionId
            Else
                .AccountLegalCollectionId = Nothing
            End If
            If INDlyItemAccountHardCollectionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountHardCollectionId = AccountHardCollectionId
            Else
                .AccountHardCollectionId = Nothing
            End If
            If INDlyItemAccountDebitOrderId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountDebitOrderId = AccountDebitOrderId
            Else
                .AccountDebitOrderId = Nothing
            End If
            If INDlyItemAccountCreditOrderId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AccountCreditOrderId = AccountCreditOrderId
            Else
                .AccountCreditOrderId = Nothing
            End If
            .DebitAccountDeteriorationId = DebitAccountDeteriorationId
            .CreditAccountDeteriorationId = CreditAccountDeteriorationId
            .ReversalAccountDeteriorationId = ReversalAccountDeteriorationId
            .PreviousPeriodReversalAccountDeteriorationId = PreviousPeriodReversalAccountDeteriorationId
            .CreditProvisionAccountId = CreditProvisionAccountId
            .DebitProvisionAccountId = DebitProvisionAccountId
            .ServicesPendingBillingMainAccountId = ServicesPendingBillingMainAccountId

            If .Id > 0 Then
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
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
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
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MContractAccountingStructure(CStr(Me.Tag))
                    AsyncLoader(True)
                    ContractAccountingStructure = (Await Model.GetContractAccountingStructure(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyContractAccountingStructure.BeginUpdate()
                    If ContractAccountingStructure IsNot Nothing AndAlso ContractAccountingStructure.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ContractAccountingStructure.Id))
                            With ContractAccountingStructure
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Name1 = .Name
                                CareGroupType = .CareGroupType

                                AccountRecoveryFeeId = .AccountRecoveryFeeId
                                INDsleAccountRecoveryFeeId.Properties.NullText = .AccountRecoveryFeeDescription
                                AccountParticularId = .AccountParticularId
                                INDsleAccountParticularId.Properties.NullText = .AccountParticularDescription
                                AccountWithoutRadicateId = .AccountWithoutRadicateId
                                INDsleAccountWithoutRadicateId.Properties.NullText = .AccountWithoutRadicateDescription
                                AccountRadicateId = .AccountRadicateId
                                INDsleAccountRadicateId.Properties.NullText = .AccountRadicateDescription
                                AccountObjectionRemediedId = .AccountObjectionRemediedId
                                INDsleAccountObjectionRemediedId.Properties.NullText = .AccountObjectionRemediedDescription
                                AccountConciliationId = .AccountConciliationId
                                INDsleAccountConciliationId.Properties.NullText = .AccountConciliationDescription
                                AccountLegalCollectionId = .AccountLegalCollectionId
                                INDsleAccountLegalCollectionId.Properties.NullText = .AccountLegalCollectionDescription
                                AccountHardCollectionId = .AccountHardCollectionId
                                INDsleAccountHardCollectionId.Properties.NullText = .AccountHardCollectionDescription
                                AccountDebitOrderId = .AccountDebitOrderId
                                INDsleAccountDebitOrderId.Properties.NullText = .AccountDebitOrderDescription
                                AccountCreditOrderId = .AccountCreditOrderId
                                INDsleAccountCreditOrderId.Properties.NullText = .AccountCreditOrderDescription
                                DebitAccountDeteriorationId = .DebitAccountDeteriorationId
                                INDsleDebitAccountDeteriorationId.Properties.NullText = .DebitAccountDeteriorationDescription
                                CreditAccountDeteriorationId = .CreditAccountDeteriorationId
                                INDsleCreditAccountDeteriorationId.Properties.NullText = .CreditAccountDeteriorationDescription
                                ReversalAccountDeteriorationId = .ReversalAccountDeteriorationId
                                INDsleReversalAccountDeteriorationId.Properties.NullText = .ReversalAccountDeteriorationDescription
                                PreviousPeriodReversalAccountDeteriorationId = .PreviousPeriodReversalAccountDeteriorationId
                                INDslePreviousPeriodReversalAccountDeteriorationId.Properties.NullText = .PreviousPeriodReversalAccountDeteriorationDescription
                                CreditProvisionAccountId = .CreditProvisionAccountId
                                INDsleCreditProvisionAccountId.Properties.NullText = .CreditProvisionAccountDescription
                                DebitProvisionAccountId = .DebitProvisionAccountId
                                INDsleDebitProvisionAccountId.Properties.NullText = .DebitProvisionAccountDescription

                                ServicesPendingBillingMainAccountId = .ServicesPendingBillingMainAccountId
                                INDsleServicesPendingBillingMainAccount.Properties.NullText = .ServicesPendingBillingMainAccountDescription

                                Status = .Status
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ContractAccountingStructure.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = ContractAccountingStructure.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(ContractAccountingStructure.Id, Me.Tag.ToString(), Nothing, GetType(ContractAccountingStructure).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewContractAccountingStructure()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyContractAccountingStructure.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If



        'Me.BarraBotones.StatusRecordVisible = True
        'Using Model As New MContractAccountingStructure(CStr(Me.Tag))
        '    AsyncLoader(True)
        '    Dim resultOperation = Await Model.GetContractAccountingStructure(INDbtnCode.Text.Trim)
        '    INDlyContractAccountingStructure.BeginUpdate()
        '    ContractAccountingStructure = resultOperation.ObjectEmbbeded
        '    If Not ContractAccountingStructure Is Nothing Then
        '        If ContractAccountingStructure.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ContractAccountingStructure.Id))
        '                With ContractAccountingStructure
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Name1 = .Name
        '                    CareGroupType = .CareGroupType

        '                    AccountRecoveryFeeId = .AccountRecoveryFeeId
        '                    INDsleAccountRecoveryFeeId.Properties.NullText = .AccountRecoveryFeeDescription
        '                    AccountParticularId = .AccountParticularId
        '                    INDsleAccountParticularId.Properties.NullText = .AccountParticularDescription
        '                    AccountWithoutRadicateId = .AccountWithoutRadicateId
        '                    INDsleAccountWithoutRadicateId.Properties.NullText = .AccountWithoutRadicateDescription
        '                    AccountRadicateId = .AccountRadicateId
        '                    INDsleAccountRadicateId.Properties.NullText = .AccountRadicateDescription
        '                    AccountObjectionRemediedId = .AccountObjectionRemediedId
        '                    INDsleAccountObjectionRemediedId.Properties.NullText = .AccountObjectionRemediedDescription
        '                    AccountConciliationId = .AccountConciliationId
        '                    INDsleAccountConciliationId.Properties.NullText = .AccountConciliationDescription
        '                    AccountLegalCollectionId = .AccountLegalCollectionId
        '                    INDsleAccountLegalCollectionId.Properties.NullText = .AccountLegalCollectionDescription
        '                    AccountHardCollectionId = .AccountHardCollectionId
        '                    INDsleAccountHardCollectionId.Properties.NullText = .AccountHardCollectionDescription
        '                    AccountDebitOrderId = .AccountDebitOrderId
        '                    INDsleAccountDebitOrderId.Properties.NullText = .AccountDebitOrderDescription
        '                    AccountCreditOrderId = .AccountCreditOrderId
        '                    INDsleAccountCreditOrderId.Properties.NullText = .AccountCreditOrderDescription
        '                    DebitAccountDeteriorationId = .DebitAccountDeteriorationId
        '                    INDsleDebitAccountDeteriorationId.Properties.NullText = .DebitAccountDeteriorationDescription
        '                    CreditAccountDeteriorationId = .CreditAccountDeteriorationId
        '                    INDsleCreditAccountDeteriorationId.Properties.NullText = .CreditAccountDeteriorationDescription
        '                    ReversalAccountDeteriorationId = .ReversalAccountDeteriorationId
        '                    INDsleReversalAccountDeteriorationId.Properties.NullText = .ReversalAccountDeteriorationDescription
        '                    PreviousPeriodReversalAccountDeteriorationId = .PreviousPeriodReversalAccountDeteriorationId
        '                    INDslePreviousPeriodReversalAccountDeteriorationId.Properties.NullText = .PreviousPeriodReversalAccountDeteriorationDescription
        '                    CreditProvisionAccountId = .CreditProvisionAccountId
        '                    INDsleCreditProvisionAccountId.Properties.NullText = .CreditProvisionAccountDescription
        '                    DebitProvisionAccountId = .DebitProvisionAccountId
        '                    INDsleDebitProvisionAccountId.Properties.NullText = .DebitProvisionAccountDescription

        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ContractAccountingStructure.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = ContractAccountingStructure.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    record = result
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.SetDocuments(ContractAccountingStructure.Id)
        '                AsyncLoader(False)
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.CodeConceptsNotes = String.Empty
        '            AsyncLoader(False)
        '            If Me._sequence.IsManual Then
        '                Me.NewContractAccountingStructure()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Me.CodeConceptsNotes = String.Empty
        '        AsyncLoader(False)
        '        If Me._sequence.IsManual Then
        '            Me.NewContractAccountingStructure()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.Code = String.Empty
        '            Deshacer()
        '            INDbtnCode.Focus()
        '        End If
        '    End If
        'End Using
        'INDlyContractAccountingStructure.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewContractAccountingStructure() As Task
        Me.ContractAccountingStructure = New ContractAccountingStructure() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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



        'If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "No existe configuración de secuencia numérica para el formulario"
        '    Exit Sub
        'End If
        'Me.ContractAccountingStructure = New ContractAccountingStructure()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MContractAccountingStructure(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not ContractAccountingStructure.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.ContractAccountingStructure = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDbtnCode.Enabled = False
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
        ContractAccountingStructure = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListCareGroupType = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractAccountingStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContractAccountingStructure, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PContractAccountingStructure(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeTuples()
        If indigo.IndigoCompanyType = 1 Then 'Si es privada
            INDlyItemAccountObjectionRemediedId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAccountObjectionRemediedId.AllowHide = False

            INDlyItemAccountConciliationId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAccountConciliationId.AllowHide = False
        Else 'Si es publica
            INDlyItemAccountDebitOrderId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAccountDebitOrderId.AllowHide = False

            INDlyItemAccountCreditOrderId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAccountCreditOrderId.AllowHide = False
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractAccountingStructure_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewContractAccountingStructure()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmContractAccountingStructure_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleServicesPendingBillingMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleServicesPendingBillingMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeServicesPendingBilling()
        End If
    End Sub

    Private Sub INDsleAccountRecoveryFeeId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountRecoveryFeeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountRecoveryFee()
        End If
    End Sub

    Private Sub INDsleAccountParticularId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountParticularId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountParticular()
        End If
    End Sub

    Private Sub INDsleAccountWithoutRadicateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountWithoutRadicateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountWithoutRadicate()
        End If
    End Sub

    Private Sub INDsleAccountRadicateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountRadicateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountRadicate()
        End If
    End Sub

    Private Sub INDsleAccountObjectionRemediedId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountObjectionRemediedId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountObjectionRemedied()
        End If
    End Sub

    Private Sub INDsleAccountConciliationId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountConciliationId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountConciliation()
        End If
    End Sub

    Private Sub INDsleAccountLegalCollectionId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountLegalCollectionId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountLegalCollection()
        End If
    End Sub

    Private Sub INDsleAccountHardCollectionId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountHardCollectionId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountHardCollection()
        End If
    End Sub

    Private Sub INDsleAccountDebitOrderId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountDebitOrderId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountDebitOrder()
        End If
    End Sub

    Private Sub INDsleAccountCreditOrderId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountCreditOrderId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeAccountCreditOrder()
        End If
    End Sub

    Private Sub INDsleDebitAccountDeteriorationId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDebitAccountDeteriorationId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeDebitAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleCreditAccountDeteriorationId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCreditAccountDeteriorationId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeCreditAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleReversalAccountDeteriorationId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleReversalAccountDeteriorationId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeReversalAccountDeterioration()
        End If
    End Sub

    Private Sub INDslePreviousPeriodReversalAccountDeteriorationId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePreviousPeriodReversalAccountDeteriorationId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializePreviousPeriodReversalAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleCreditProvisionAccountId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCreditProvisionAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeCreditProvisionAccount()
        End If
    End Sub

    Private Sub INDsleDebitProvisionAccountId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDebitProvisionAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeDebitProvisionAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleCareGroupType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroupType.EditValueChanged
        If CareGroupType IsNot Nothing Then
            Select Case CareGroupType
                Case 1 'EAPB con contrato
                    INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountRecoveryFeeId.AllowHide = False

                    INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountParticularId.AllowHide = True

                    INDlyItemAccountWithoutRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountWithoutRadicateId.AllowHide = False

                    INDlyItemAccountRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountRadicateId.AllowHide = False
                Case 2 'EAPB sin contrato
                    INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountRecoveryFeeId.AllowHide = False

                    INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountParticularId.AllowHide = True

                    INDlyItemAccountWithoutRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountWithoutRadicateId.AllowHide = False

                    INDlyItemAccountRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountRadicateId.AllowHide = False
                Case 3 'Particulares
                    INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountRecoveryFeeId.AllowHide = True

                    INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountParticularId.AllowHide = False

                    INDlyItemAccountWithoutRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountWithoutRadicateId.AllowHide = True

                    INDlyItemAccountRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountRadicateId.AllowHide = True
                Case 4 'Aseguradoras
                    INDlyItemAccountRecoveryFeeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountRecoveryFeeId.AllowHide = True

                    INDlyItemAccountParticularId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAccountParticularId.AllowHide = True

                    INDlyItemAccountWithoutRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountWithoutRadicateId.AllowHide = False

                    INDlyItemAccountRadicateId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAccountRadicateId.AllowHide = False
            End Select
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleServicesPendingBillingMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleServicesPendingBillingMainAccount.QueryPopUp
        If ServicesPendingBillingMainAccountXpo Is Nothing Then
            Presenter.InitializeServicesPendingBilling()
        End If
    End Sub

    Private Sub INDsleAccountRecoveryFeeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountRecoveryFeeId.QueryPopUp
        If AccountRecoveryFeeIdXpo Is Nothing Then
            Presenter.InitializeAccountRecoveryFee()
        End If
    End Sub

    Private Sub INDsleAccountParticularId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountParticularId.QueryPopUp
        If AccountParticularIdXpo Is Nothing Then
            Presenter.InitializeAccountParticular()
        End If
    End Sub

    Private Sub INDsleAccountWithoutRadicateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountWithoutRadicateId.QueryPopUp
        If AccountWithoutRadicateIdXpo Is Nothing Then
            Presenter.InitializeAccountWithoutRadicate()
        End If
    End Sub

    Private Sub INDsleAccountRadicateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountRadicateId.QueryPopUp
        If AccountRadicateIdXpo Is Nothing Then
            Presenter.InitializeAccountRadicate()
        End If
    End Sub

    Private Sub INDsleAccountObjectionRemediedId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountObjectionRemediedId.QueryPopUp
        If AccountObjectionRemediedIdXpo Is Nothing Then
            Presenter.InitializeAccountObjectionRemedied()
        End If
    End Sub

    Private Sub INDsleAccountConciliationId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountConciliationId.QueryPopUp
        If AccountConciliationIdXpo Is Nothing Then
            Presenter.InitializeAccountConciliation()
        End If
    End Sub

    Private Sub INDsleAccountLegalCollectionId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountLegalCollectionId.QueryPopUp
        If AccountLegalCollectionIdXpo Is Nothing Then
            Presenter.InitializeAccountLegalCollection()
        End If
    End Sub

    Private Sub INDsleAccountHardCollectionId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountHardCollectionId.QueryPopUp
        If AccountHardCollectionIdXpo Is Nothing Then
            Presenter.InitializeAccountHardCollection()
        End If
    End Sub

    Private Sub INDsleAccountDebitOrderId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountDebitOrderId.QueryPopUp
        If AccountDebitOrderIdXpo Is Nothing Then
            Presenter.InitializeAccountDebitOrder()
        End If
    End Sub

    Private Sub INDsleAccountCreditOrderId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountCreditOrderId.QueryPopUp
        If AccountCreditOrderIdXpo Is Nothing Then
            Presenter.InitializeAccountCreditOrder()
        End If
    End Sub

    Private Sub INDsleDebitAccountDeteriorationId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDebitAccountDeteriorationId.QueryPopUp
        If DebitAccountDeteriorationIdXpo Is Nothing Then
            Presenter.InitializeDebitAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleCreditAccountDeteriorationId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCreditAccountDeteriorationId.QueryPopUp
        If CreditAccountDeteriorationIdXpo Is Nothing Then
            Presenter.InitializeCreditAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleReversalAccountDeteriorationId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReversalAccountDeteriorationId.QueryPopUp
        If ReversalAccountDeteriorationIdXpo Is Nothing Then
            Presenter.InitializeReversalAccountDeterioration()
        End If
    End Sub

    Private Sub INDslePreviousPeriodReversalAccountDeteriorationId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePreviousPeriodReversalAccountDeteriorationId.QueryPopUp
        If PreviousPeriodReversalAccountDeteriorationIdXpo Is Nothing Then
            Presenter.InitializePreviousPeriodReversalAccountDeterioration()
        End If
    End Sub

    Private Sub INDsleCreditProvisionAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCreditProvisionAccountId.QueryPopUp
        If CreditProvisionAccountIdXpo Is Nothing Then
            Presenter.InitializeCreditProvisionAccount()
        End If
    End Sub

    Private Sub INDsleDebitProvisionAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDebitProvisionAccountId.QueryPopUp
        If DebitProvisionAccountIdXpo Is Nothing Then
            Presenter.InitializeDebitProvisionAccount()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class