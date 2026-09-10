'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 2025-08-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPopupThirdPartyTaxExemptions

#Region "Builder"
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Events"

    Public Event AddThirdPartyTaxExemptions(sender As Object, e As AddBThirdPartyTaxExemptionsEventArgs)
    ''' <summary>
    ''' Evento que se dispara para  que al checkear me muestre el campo detalle 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckEditDetail_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEditDetail.CheckedChanged

        Dim enableDetail As Boolean = CheckEditDetail.Checked
        If enableDetail Then
            INDLciDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Not enableDetail Then
            Detail = String.Empty
        End If

    End Sub
#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa al presentador del formulario de terceros
    ''' </summary>
    Private _presenter As PThirdParty

    ''' <summary>
    ''' Indica si se esta en modo edicion
    ''' </summary>
    Private _editMode As Boolean

    ''' <summary>
    ''' entidad de exoneracion tributaria
    ''' </summary>
    Private _thirdPartyTaxExemptions As ThirdPartyTaxExemptions

#End Region

#Region "Fields"

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Indica el listado de las exoneraciones ya agregadas
    ''' </summary>
    ''' <returns></returns>
    Public Property ListThirdPartyTaxExemptions As List(Of ThirdPartyTaxExemptions)

    ''' <summary>
    ''' Registro el cual se va a editar
    ''' </summary>
    Public WriteOnly Property ThirdPartyTaxExemptions As ThirdPartyTaxExemptions
        Set(value As ThirdPartyTaxExemptions)
            Me._thirdPartyTaxExemptions = value
        End Set
    End Property

    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            Me._editMode = value
        End Set
    End Property

    Public Property DocumentTypeId As Integer
        Get
            Return INDSleDocumentType.EditValue
        End Get
        Set(value As Integer)
            INDSleDocumentType.EditValue = value
        End Set
    End Property

    Public Property Detail As String
        Get
            Return INDTxtDetail.EditValue
        End Get
        Set(value As String)
            INDTxtDetail.EditValue = value
        End Set
    End Property

    Public Property DocumentIdentification As String
        Get
            Return INDTxtDocumentIdentification.EditValue
        End Get
        Set(value As String)
            INDTxtDocumentIdentification.EditValue = value
        End Set
    End Property

    Public Property ART_RESNumber As String
        Get
            Return INDTxtArt_ResNumber.EditValue
        End Get
        Set(value As String)
            INDTxtArt_ResNumber.EditValue = value
        End Set
    End Property

    Public Property InstitutionId As Integer
        Get
            Return INDSleInstitution.EditValue
        End Get
        Set(value As Integer)
            INDSleInstitution.EditValue = value
        End Set
    End Property

    Public Property DocumentDate As Date
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    Public Property ExemptFeeId As Integer
        Get
            Return INDSleExemptFee.EditValue
        End Get
        Set(value As Integer)
            INDSleExemptFee.EditValue = value
        End Set
    End Property

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.CleanControls()
    End Sub

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Sub FrmPopupThirdPartyTaxExemptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False

        Me._presenter = New PThirdParty()

        If Me._editMode Then
            LoadControls()
        Else
            Me.CleanControls()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._presenter = Nothing
        Me._editMode = Nothing
        Me._thirdPartyTaxExemptions = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopupThirdPartyTaxExemptions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleDocumentType.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmPopupThirdPartyTaxExemptions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleDocumentType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentType.QueryPopUp
        If INDSleDocumentType.Properties.DataSource Is Nothing Then
            INDSleDocumentType.Properties.DataSource = Me._presenter.ListTaxExemptionsByApplicationType(True)
        End If
    End Sub

    Private Sub INDSleInstitution_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInstitution.QueryPopUp
        If INDSleInstitution.Properties.DataSource Is Nothing Then
            INDSleInstitution.Properties.DataSource = Me._presenter.ListTaxExemptionsByApplicationType(False)
        End If
    End Sub

    Private Sub INDSleExemptFee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleExemptFee.QueryPopUp
        If INDSleExemptFee.Properties.DataSource Is Nothing Then
            INDSleExemptFee.Properties.DataSource = Me._presenter.ListGeneralLedgerIvaByState(True)
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AsyncLoader(True)

        If Not ValidateControls() Then
            AsyncLoader(False)
            Exit Sub
        End If

        If _editMode Then
            If ListThirdPartyTaxExemptions.Where(Function(x) x.Id <> _thirdPartyTaxExemptions.Id).Any(
                Function(x) (
                                (x.DocumentTypeId = _thirdPartyTaxExemptions.DocumentTypeId) OrElse
                                (x.DocumentIdentification = _thirdPartyTaxExemptions.DocumentIdentification) OrElse
                                (x.InstitutionId = _thirdPartyTaxExemptions.InstitutionId)
                            )) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con estas caracteristicas(Tipo de Documento, Identificación e Institución)"
                AsyncLoader(False)
                Exit Sub
            End If
        End If

        If CheckEditDetail.Checked Then
            Dim LengthDetailText = If(Detail, String.Empty).Trim()
            If LengthDetailText.Length < 5 Then
                Mensaje(EeventViewerImages.Advertencia) = "El campo detalle debe tener mínimo 5 caracteres."
                AsyncLoader(False)
                Exit Sub
            End If
        End If

        Dim LengthIdentification = If(DocumentIdentification, String.Empty).Trim()
        If LengthIdentification.Length < 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo identificación debe tener mínimo 3 caracteres."
            AsyncLoader(False)
            Exit Sub
        End If

        Dim LengthART_RESNumber = If(ART_RESNumber, String.Empty).Trim()
        If LengthART_RESNumber.Length < 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo Número del Art / Res debe tener mínimo 3 caracteres."
            AsyncLoader(False)
            Exit Sub
        End If

        Dim resultAssignValues = AssignValues()

        If Not resultAssignValues.StateResult Then
            Mensaje(EeventViewerImages.Advertencia) = resultAssignValues.Message
            AsyncLoader(False)
            Exit Sub
        End If

        RaiseEvent AddThirdPartyTaxExemptions(Nothing, New AddBThirdPartyTaxExemptionsEventArgs With
            {
                .EditMode = Me._editMode,
                .ThirdPartyTaxExemptions = Me._thirdPartyTaxExemptions
            }
        )

        AsyncLoader(False)
        If _editMode = True Then
            Me.Close()
        End If

        Me.CleanControls()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que se encarga de limpiar los campos
    ''' </summary>
    Private Sub CleanControls()
        INDSleDocumentType.EditValue = Nothing
        Detail = String.Empty
        DocumentIdentification = String.Empty
        ART_RESNumber = String.Empty
        INDSleInstitution.EditValue = Nothing
        INDSleExemptFee.EditValue = Nothing

        Me._editMode = False
        Me._thirdPartyTaxExemptions = New ThirdPartyTaxExemptions
        INDBtnAdd.Enabled = True
        INDBtnAdd.Text = ResourceManager.GetString("Add")

        INDSleDocumentType.Focus()
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de cargar los controles con la entidad a editar
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadControls()
        Try
            AsyncLoader(True)

            INDBtnAdd.Text = ResourceManager.GetString("Edit")

            With Me._thirdPartyTaxExemptions
                DocumentTypeId = .DocumentTypeId
                INDSleDocumentType.Properties.NullText = .DocumentTypeCodeName

                Detail = .Detail
                DocumentIdentification = .DocumentIdentification
                ART_RESNumber = .ART_RESNumber

                InstitutionId = .InstitutionId
                INDSleInstitution.Properties.NullText = .InstitutionCodeName

                DocumentDate = .DocumentDate
                ExemptFeeId = .ExemptFeeId
                INDSleExemptFee.Properties.NullText = .ExemptFeeCodeName
            End With
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Metodo que se encarga de asignar los valores a la entidad a agregar
    ''' </summary>
    ''' <returns></returns>
    Private Function AssignValues() As ActionResult
        Try
            With Me._thirdPartyTaxExemptions

                .DocumentTypeId = DocumentTypeId
                .DocumentTypeCodeName = INDSleDocumentType.Text

                .Detail = Detail
                .DocumentIdentification = DocumentIdentification
                .ART_RESNumber = ART_RESNumber

                .InstitutionId = InstitutionId
                .InstitutionCodeName = INDSleInstitution.Text

                .DocumentDate = DocumentDate
                .ExemptFeeId = ExemptFeeId
                .ExemptFeeCodeName = INDSleExemptFee.Text
            End With

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function


    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Me.changeNumericFormatByCurrency(_currencyAbbreviation.GetNumberFormat)
    End Sub

#End Region

End Class