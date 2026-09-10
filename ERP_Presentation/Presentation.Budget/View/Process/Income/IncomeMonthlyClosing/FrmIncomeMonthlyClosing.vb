'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 24-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Budget.MVP

#End Region

Public Class FrmIncomeMonthlyClosing
    Implements ICloseMonthIncome

#Region "GLOBALS"
    Dim presenter As PCloseMonthIncome

    Dim validity As BudgetaryValidity
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Propiedad para mostar los mensajes en el visor
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
    ''' <summary>
    ''' Propiedad que obtiene le tag del frm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements ICloseMonthIncome.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de vigencias
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection Implements ICloseMonthIncome.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource Implements ICloseMonthIncome.BudgetEntitiesXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property
#End Region

#Region "CRUD"
    ''' <summary>
    ''' Metodos crud sin usarse 
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        validity = Nothing
        INDsleBudgetEntity.EditValue = Nothing
        INDsleValidity.EditValue = Nothing
        INDTxtMonth.Text = String.Empty
        INDsleBudgetEntity.Focus()
    End Sub
    ''' <summary>
    ''' Funcion que obtiene los documentos no confrimados 
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDocumentsNotConfirmed() As List(Of Tuple(Of String, String, DateTime))
        Using model As New MConfirmationDocuments(MyTag)
            Dim listDocumentsNotConfirmd As New List(Of Tuple(Of String, String, DateTime))
            Dim listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.AnnualizedCashFlowModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("PAC Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If
            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.AnnualizedCashFlowTransfer, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("PAC Traslados", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.BudgetModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Presupuesto Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.BudgetTransfer, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Presupuesto Traslados", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.Collection, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Recaudos", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.CollectionModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Recaudos Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.Recognition, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Reconocimientos", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmed(EBudgetDocumentType.RecognitionModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Reconocimientos Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If
            Return listDocumentsNotConfirmd
        End Using
    End Function

#End Region

#Region "HANDLES"
    ''' <summary>
    ''' Libera la memoria del frm al cerrar lo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        validity = Nothing
    End Sub
    ''' <summary>
    ''' Carga el frm FrmIncomeMonthlyClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmIncomeMonthlyClosing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PCloseMonthIncome(Me)
    End Sub
    ''' <summary>
    ''' Enfoca el campo de INDsleBudgetEntity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmIncomeMonthlyClosing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub
    ''' <summary>
    ''' Abre el frm de entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleBudgetEntity.EditValue, True)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el campo de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar las vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            Using model As New MCloseMonthIncome(MyTag)
                validity = model.GetValidity(INDsleValidity.EditValue)
                If validity.IncomeMonth <= 12 Then
                    INDTxtMonth.Text = DateAndTime.MonthName(validity.IncomeMonth).ToUpper()
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Else
                    INDTxtMonth.Text = If(validity.IncomeMonth = 13, "PERIODOS CERRADOS", "VIGENCIA CERRADA")
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                End If
            End Using

        End If
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
    ''' <summary>
    ''' Click en confirmar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Try
            AsyncLoader(True)

            Dim list = GetDocumentsNotConfirmed()
            If list.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede cerrar el mes hasta que se confirmen los siguientes documentos"
                Using formDocuments As New FrmDocumentNotConfirmed
                    formDocuments.ListDocumet = list
                    formDocuments.Size = New System.Drawing.Size(800, 730)
                    formDocuments.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim frmTransparent As New FrmTransparent(formDocuments, False)
                    frmTransparent.ShowDialog(Me)
                End Using
                AsyncLoader(False)
                Exit Sub
            End If

            Using model As New MCloseMonthIncome(MyTag)
                validity.IncomeMonth += 1
                validity.MarkAsModified()
                Dim result = Await model.CloseMonth(validity)
                If result.StateResult = True Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = "Se cerro correctamente el mes de " + INDTxtMonth.Text
                    CleanControls()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub
    ''' <summary>
    ''' Click en deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
#End Region

End Class