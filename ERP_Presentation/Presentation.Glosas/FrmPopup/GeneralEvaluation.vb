'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz
' Created          : 2013-07-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text


''' <summary>
''' Formulario modal para evaluación general
''' </summary>
Public Class Generalevaluation

#Region "Fields"

    ''' <summary>
    ''' Opciones
    ''' </summary>
    Private _option As Integer
    ''' <summary>
    ''' variable de control
    ''' </summary>
    ''' <remarks></remarks>
    Private _control As Integer
    ''' <summary>
    ''' Interfaz del modulo de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private _model As IGeneralEvaluation
    ''' <summary>
    ''' Bandera´para saber si es un tramite general a Nivel de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _GeneralGLosaInvoice As Boolean
    ''' <summary>
    ''' Lista de movimientos Seleccionados
    ''' </summary>
    Private _listMovementsSelected As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Lista de Facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrListInvoiceselected As List(Of String)
    ''' <summary>
    ''' tag del formulario padre
    ''' </summary>
    ''' <remarks></remarks>
    Private _tag As Integer
    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private _ModuleGlosa As String
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' comentario en formato HTML
    ''' </summary>
    ''' <remarks></remarks>
    Dim CommentHtml As String
    ''' <summary>
    ''' Comentario en formato text
    ''' </summary>
    ''' <remarks></remarks>
    Dim TextCommentHtml As String
    ''' <summary>
    ''' Id del concepto de Aceptación de Jerarquía
    ''' </summary>
    ''' <remarks></remarks>
    Dim IdResponseHierarchy As Integer?
    ''' <summary>
    ''' Propiedad para saber si el formulario va manejar numeros decimales.
    ''' </summary>
    ''' <returns></returns>
    Public Property ManageDecimals As Boolean
    ''' <summary>
    ''' Codigo evaluación glosa
    ''' </summary>
    Dim codeglosaEvaluationas As String

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#Region "Builders"

    ''' <summary>
    ''' Constructor para el formulario
    ''' </summary>
    Public Sub New(ModuleGlosa As String, ListMovementSelected As List(Of GlosaMovementGlosa), optionInvoice As Integer, ByVal model As IGeneralEvaluation, ByVal tag As String, ByVal idOperativeUnit As Integer, Optional ByVal ListObjDSelectedSTr As List(Of String) = Nothing, Optional ByVal GeneralGLosaInvoice As Boolean = False)
        InitializeComponent()

        _listMovementsSelected = ListMovementSelected
        _option = optionInvoice
        _model = model
        _tag = tag
        _GeneralGLosaInvoice = GeneralGLosaInvoice
        _StrListInvoiceselected = ListObjDSelectedSTr
        _ModuleGlosa = ModuleGlosa
        _idOperativeUnit = idOperativeUnit

        GridLookUpLoad()
        ManageDecimals = True
        If ManageDecimals = False Then
            INDValueTxt.Properties.Mask.EditMask = "c0"
            INDValueTxt.Properties.DisplayFormat.FormatString = "c0"
        Else
            INDValueTxt.Properties.Mask.EditMask = "c0"
            INDValueTxt.Properties.DisplayFormat.FormatString = "c0"
        End If
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub GeneralEvaluation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._control = 0
        Me._indigoSessionValues = SessionValues.Instance

        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
        If _StrListInvoiceselected IsNot Nothing AndAlso _StrListInvoiceselected.Count > 0 Then
            If _ModuleGlosa = "COORDINACION" Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
            End If
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listMovementsSelected = Nothing
        _option = Nothing
        _control = Nothing
        _model = Nothing
        _GeneralGLosaInvoice = Nothing
        _StrListInvoiceselected = Nothing
        _ModuleGlosa = Nothing
        _idOperativeUnit = Nothing
        CommentHtml = Nothing
        TextCommentHtml = Nothing
        IdResponseHierarchy = Nothing
    End Sub

    ''' <summary>
    ''' Evento al cambiar el gridLookUp de conceptos
    ''' </summary>
    Private Sub INDConceptGEGle_EditValueChanged(sender As Object, e As EventArgs) Handles INDConceptGEGle.EditValueChanged

        Me.INDValueTxt.Enabled = True
        Me.INDValueTxt.Text = ""

        codeglosaEvaluationas = Me.INDConceptGEGle.EditValue

        Dim conceptGlosa = TryCast(Me.INDConceptGEGle.GetSelectedDataRow, Domain.Entities.ConceptGlosas)

        _control = 0

        If CInt(conceptGlosa.HomologateTypeByCodeAndResponse()) = ConceptsGlosaEvaluationByType.GlosaODevolucionInjustificada Then
            Me.INDValueTxt.Text = 0
            Me.INDValueTxt.Enabled = False
        End If

        If CInt(conceptGlosa.HomologateTypeByCodeAndResponse()) = ConceptsGlosaEvaluationByType.NoSubsanada Then
            _control = 1
            Me.INDValueTxt.Enabled = False
        End If

        If CInt(conceptGlosa.HomologateTypeByCodeAndResponse()) = ConceptsGlosaEvaluationByType.SubsanadaParcial Then
            _control = 2
        End If

        If CInt(conceptGlosa.HomologateTypeByCodeAndResponse()) = ConceptsGlosaEvaluationByType.Subsanada Then
            Me.INDValueTxt.Text = 0
            Me.INDValueTxt.Enabled = False
        End If

        If {ConceptsGlosaEvaluationByType.DevolucionInjustificada, ConceptsGlosaEvaluationByType.DevolucionJustificada}.ToList().Contains(CInt(conceptGlosa.HomologateTypeByCodeAndResponse())) Then
            Me.INDValueTxt.Text = 0
            Me.INDValueTxt.Enabled = False
        End If

        'If codeglosaEvaluationas = "996" Then
        '    Me.INDValueTxt.Text = 0
        '    Me.INDValueTxt.Enabled = False
        'End If

        'If codeglosaEvaluationas = "997" Then
        '    _control = 1
        '    Me.INDValueTxt.Enabled = False
        'End If

        'If codeglosaEvaluationas = "998" Then
        '    _control = 2
        'End If

        'If codeglosaEvaluationas = "999" Then
        '    Me.INDValueTxt.Text = 0
        '    Me.INDValueTxt.Enabled = False
        'End If

        'If codeglosaEvaluationas = "995" Then
        '    Me.INDValueTxt.Text = 0
        '    Me.INDValueTxt.Enabled = False
        'End If

    End Sub

    Private Sub INDCommentPce_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDCommentPce.QueryPopUp
        If codeglosaEvaluationas IsNot Nothing Then
            Using _JustificationEvaluation As New JustificationEvaluation(codeglosaEvaluationas)
                _JustificationEvaluation.ShowDialog(Me)
                CommentHtml = _JustificationEvaluation.CommentHtml
                TextCommentHtml = _JustificationEvaluation.CommentOnlyText
                IdResponseHierarchy = _JustificationEvaluation.IdResponseHierarchy
            End Using
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Evento al guardar la evaluación
    ''' </summary>
    Private Async Sub SaveWithState(StateEvaluation As Byte)
        Try
            Dim strrMEnsajeGeneral As StringBuilder = New StringBuilder()
            If Me.INDConceptGEGle.EditValue Is Nothing Or Me.CommentHtml Is String.Empty Or (Me.INDValueTxt.Text Is String.Empty AndAlso Me.INDConceptGEGle.EditValue = "998") Then
                Return
            End If
            AsyncLoader(True)
            Dim evaluationConcept = CType(Me.INDConceptGEGle.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)
            Dim _valueGLosado As Decimal = Me.INDValueTxt.Text

            If _GeneralGLosaInvoice = False Then
                For Each itemMov As GlosaMovementGlosa In _listMovementsSelected
                    If _option = 1 Then  ' DETALLE DE FACTURA SIN QX
                        If itemMov.ListMovimientoAux IsNot Nothing AndAlso itemMov.ListMovimientoAux.Count = 0 Then
                            'itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                            'itemMov.IdGlosaEvaluation = evaluationConcept.Id
                            itemMov.OtherMovements.Clear()

                            'GLOSA POR PRIMERA VEZ 
                            If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                                itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                                itemMov.IdGlosaEvaluation = evaluationConcept.Id
                                itemMov.JustificationGlosa = Me.CommentHtml
                                itemMov.JustificationGlosaText = Me.TextCommentHtml
                                itemMov.IdResponseHierarchyGlosa = Me.IdResponseHierarchy
                                If _control = 1 Then
                                    If itemMov.MainGlosa = True Then
                                        itemMov.ValueAcceptedFirstInstance = itemMov.ValueGlosado     'itemMov.MaxValueAcceptedGeneral
                                        If itemMov.MaxValueAcceptedGeneral > 0 Then
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                   x.MaxValueAcceptedGeneral = 0
                                                                               End If
                                                                           End Sub)
                                        End If
                                    End If
                                Else
                                    If _control = 2 Then

                                        If itemMov.MainGlosa = True Then

                                            If itemMov.MaxValueAcceptedGeneral = 1 Then
                                                itemMov.ValueAcceptedFirstInstance = ""
                                            Else
                                                If _valueGLosado = 0 Then
                                                    itemMov.ValueAcceptedFirstInstance = 0
                                                Else
                                                    Dim maxvalueAcceptItem As Decimal = itemMov.ValueGlosado - IIf(itemMov.ValueAcceptedFirstInstance Is Nothing, 0, itemMov.ValueAcceptedFirstInstance)
                                                    If maxvalueAcceptItem > 0 Then
                                                        If _valueGLosado >= itemMov.ValueGlosado Then
                                                            itemMov.ValueAcceptedFirstInstance = maxvalueAcceptItem
                                                            _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                        ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                            itemMov.ValueAcceptedFirstInstance = _valueGLosado
                                                            _valueGLosado = 0
                                                        End If
                                                    End If
                                                    _listMovementsSelected.ForEach(Sub(x)
                                                                                       If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                           x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedFirstInstance
                                                                                       End If
                                                                                   End Sub)
                                                End If

                                            End If
                                        End If
                                    Else

                                        If Me.INDValueTxt.Text > itemMov.MaxValueAcceptedGeneral Then
                                            itemMov.ValueAcceptedFirstInstance = itemMov.MaxValueAcceptedGeneral
                                            If itemMov.MaxValueAcceptedGeneral > 0 Then
                                                _listMovementsSelected.ForEach(Sub(x)
                                                                                   If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                       x.MaxValueAcceptedGeneral = 0
                                                                                   End If
                                                                               End Sub)
                                            End If
                                        Else
                                            itemMov.ValueAcceptedFirstInstance = Me.INDValueTxt.Text
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                   x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedFirstInstance
                                                                               End If
                                                                           End Sub)
                                        End If
                                    End If
                                End If
                            End If

                            'REITERACIONES
                            If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                                itemMov.IdReiterationEvaluation = evaluationConcept.Id
                                itemMov.JustificationReiteration = Me.CommentHtml
                                itemMov.JustificationReiterationText = Me.TextCommentHtml
                                itemMov.IdResponseHierarchyReiteration = Me.IdResponseHierarchy
                                If _control = 1 Then
                                    If itemMov.MainGlosa = True Then
                                        itemMov.ValueAcceptedSecondInstance = itemMov.ValueReiterated - IIf(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance)    'itemMov.MaxValueAcceptedGeneral
                                        If itemMov.MaxValueAcceptedGeneral > 0 Then
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                   x.MaxValueAcceptedGeneral = 0
                                                                               End If
                                                                           End Sub)
                                        End If
                                    End If
                                Else
                                    If _control = 2 Then

                                        If itemMov.MainGlosa = True Then
                                            If itemMov.MaxValueAcceptedGeneral = 1 Then
                                                itemMov.ValueAcceptedSecondInstance = ""
                                            Else
                                                Dim maxvalueAcceptItem As Decimal = itemMov.ValueReiterated - IIf(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance)
                                                If maxvalueAcceptItem > 0 Then
                                                    If _valueGLosado >= itemMov.ValueReiterated Then
                                                        itemMov.ValueAcceptedSecondInstance = maxvalueAcceptItem
                                                        _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                    ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                        itemMov.ValueAcceptedSecondInstance = _valueGLosado
                                                        _valueGLosado = 0
                                                    End If
                                                End If
                                                itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                      If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                                          x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedSecondInstance
                                                                                      End If
                                                                                  End Sub)
                                            End If
                                        End If
                                    Else
                                        If Me.INDValueTxt.Text > itemMov.MaxValueAcceptedGeneral Then
                                            itemMov.ValueAcceptedSecondInstance = itemMov.MaxValueAcceptedGeneral
                                            If itemMov.MaxValueAcceptedGeneral > 0 Then
                                                _listMovementsSelected.ForEach(Sub(x)
                                                                                   If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                       x.MaxValueAcceptedGeneral = 0
                                                                                   End If
                                                                               End Sub)
                                            End If
                                        Else
                                            itemMov.ValueAcceptedSecondInstance = Me.INDValueTxt.Text
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                   x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedSecondInstance
                                                                               End If
                                                                           End Sub)
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            ' mas de un registro
                            For Each itemMovQX As GlosaMovementGlosa In itemMov.ListMovimientoAux
                                itemMovQX.OtherMovements.Clear()
                                'glosas
                                If itemMovQX.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or itemMovQX.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                                    itemMovQX.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                                    itemMovQX.IdGlosaEvaluation = evaluationConcept.Id
                                    itemMovQX.JustificationGlosa = Me.CommentHtml
                                    itemMovQX.JustificationGlosaText = Me.TextCommentHtml
                                    itemMovQX.IdResponseHierarchyGlosa = Me.IdResponseHierarchy
                                    If _control = 1 Then
                                        If itemMov.MainGlosa = True Then
                                            itemMov.ValueAcceptedFirstInstance = itemMov.ValueGlosado     'itemMov.MaxValueAcceptedGeneral
                                            If itemMov.MaxValueAcceptedGeneral > 0 Then
                                                _listMovementsSelected.ForEach(Sub(x)
                                                                                   If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                       x.MaxValueAcceptedGeneral = 0
                                                                                   End If
                                                                               End Sub)
                                            End If
                                        End If
                                    Else
                                        If _control = 2 Then
                                            If itemMov.MainGlosa = True Then
                                                If itemMov.MaxValueAcceptedGeneral = 1 Then
                                                    itemMov.ValueAcceptedFirstInstance = ""
                                                Else
                                                    If _valueGLosado = 0 Then
                                                        itemMov.ValueAcceptedFirstInstance = 0
                                                    Else
                                                        Dim maxvalueAcceptItem As Decimal = itemMov.ValueGlosado - IIf(itemMov.ValueAcceptedFirstInstance Is Nothing, 0, itemMov.ValueAcceptedFirstInstance)
                                                        If maxvalueAcceptItem > 0 Then
                                                            If _valueGLosado >= itemMov.ValueGlosado Then
                                                                itemMov.ValueAcceptedFirstInstance = maxvalueAcceptItem
                                                                _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                            ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                                itemMov.ValueAcceptedFirstInstance = _valueGLosado
                                                                _valueGLosado = 0
                                                            End If
                                                        End If
                                                        _listMovementsSelected.ForEach(Sub(x)
                                                                                           If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                               x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedFirstInstance
                                                                                           End If
                                                                                       End Sub)
                                                    End If

                                                End If
                                            End If
                                        Else

                                            If Me.INDValueTxt.Text > itemMovQX.MaxValueAcceptedGeneral Then
                                                itemMovQX.ValueAcceptedFirstInstance = itemMovQX.MaxValueAcceptedGeneral
                                                If itemMovQX.MaxValueAcceptedGeneral > 0 Then
                                                    itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                          If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                              x.MaxValueAcceptedGeneral = 0
                                                                                          End If
                                                                                      End Sub)
                                                End If
                                            Else
                                                itemMovQX.ValueAcceptedFirstInstance = Me.INDValueTxt.Text
                                                itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                      If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                          x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMovQX.ValueAcceptedFirstInstance
                                                                                      End If
                                                                                  End Sub)
                                            End If
                                        End If
                                    End If

                                End If

                                If itemMovQX.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or itemMovQX.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                                    itemMovQX.IdReiterationEvaluation = evaluationConcept.Id
                                    itemMovQX.JustificationReiteration = Me.CommentHtml
                                    itemMovQX.JustificationReiterationText = Me.TextCommentHtml
                                    itemMov.IdResponseHierarchyReiteration = Me.IdResponseHierarchy
                                    If _control = 1 Then
                                        If itemMov.MainGlosa = True Then
                                            itemMovQX.ValueAcceptedSecondInstance = itemMovQX.ValueReiterated - IIf(itemMovQX.ValueAcceptedSecondInstance Is Nothing, 0, itemMovQX.ValueAcceptedSecondInstance) 'itemMovQX.MaxValueAcceptedGeneral
                                            If itemMovQX.MaxValueAcceptedGeneral > 0 Then
                                                itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                      If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                          x.MaxValueAcceptedGeneral = 0
                                                                                      End If
                                                                                  End Sub)
                                            End If
                                        End If
                                    Else
                                        If _control = 2 Then
                                            If itemMov.MainGlosa = True Then
                                                If itemMovQX.MaxValueAcceptedGeneral = 1 Then
                                                    itemMovQX.ValueAcceptedSecondInstance = ""
                                                Else
                                                    Dim maxvalueAcceptItem As Decimal = itemMov.ValueReiterated - IIf(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance)
                                                    If maxvalueAcceptItem > 0 Then
                                                        If _valueGLosado >= itemMov.ValueReiterated Then
                                                            itemMov.ValueAcceptedSecondInstance = maxvalueAcceptItem
                                                            _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                        ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                            itemMov.ValueAcceptedSecondInstance = _valueGLosado
                                                            _valueGLosado = 0
                                                        End If
                                                    End If
                                                    itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                          If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                              x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMovQX.ValueAcceptedSecondInstance
                                                                                          End If
                                                                                      End Sub)
                                                End If
                                            End If
                                        Else

                                            If Me.INDValueTxt.Text > itemMovQX.MaxValueAcceptedGeneral Then
                                                itemMovQX.ValueAcceptedSecondInstance = itemMovQX.MaxValueAcceptedGeneral
                                                If itemMovQX.MaxValueAcceptedGeneral > 0 Then
                                                    itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                          If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                              x.MaxValueAcceptedGeneral = 0
                                                                                          End If
                                                                                      End Sub)
                                                End If
                                            Else
                                                itemMovQX.ValueAcceptedSecondInstance = Me.INDValueTxt.Text
                                                itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                      If x.InvoiceDetailId = itemMovQX.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMovQX.InvoiceDetailIdQX Then
                                                                                          x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMovQX.ValueAcceptedSecondInstance
                                                                                      End If
                                                                                  End Sub)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    ElseIf _option = 2 Then  'detalle de factura QX
                        'itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                        'itemMov.IdGlosaEvaluation = evaluationConcept.Id
                        itemMov.OtherMovements.Clear()

                        'GLOSA POR PRIMERA VEZ QX
                        If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                            itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                            itemMov.IdGlosaEvaluation = evaluationConcept.Id
                            itemMov.JustificationGlosa = Me.CommentHtml
                            itemMov.JustificationGlosaText = Me.TextCommentHtml
                            itemMov.IdResponseHierarchyGlosa = Me.IdResponseHierarchy
                            If _control = 1 Then
                                If itemMov.MainGlosa = True Then
                                    itemMov.ValueAcceptedFirstInstance = itemMov.ValueGlosado     'itemMov.MaxValueAcceptedGeneral
                                    If itemMov.MaxValueAcceptedGeneral > 0 Then
                                        _listMovementsSelected.ForEach(Sub(x)
                                                                           If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                               x.MaxValueAcceptedGeneral = 0
                                                                           End If
                                                                       End Sub)
                                    End If
                                End If
                            Else
                                If _control = 2 Then
                                    If itemMov.MainGlosa = True Then
                                        If itemMov.MaxValueAcceptedGeneral = 1 Then
                                            itemMov.ValueAcceptedFirstInstance = ""
                                        Else
                                            If _valueGLosado = 0 Then
                                                itemMov.ValueAcceptedFirstInstance = 0
                                            Else
                                                Dim maxvalueAcceptItem As Decimal = itemMov.ValueGlosado - IIf(itemMov.ValueAcceptedFirstInstance Is Nothing, 0, itemMov.ValueAcceptedFirstInstance)
                                                If maxvalueAcceptItem > 0 Then
                                                    If _valueGLosado >= itemMov.ValueGlosado Then
                                                        itemMov.ValueAcceptedFirstInstance = maxvalueAcceptItem
                                                        _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                    ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                        itemMov.ValueAcceptedFirstInstance = _valueGLosado
                                                        _valueGLosado = 0
                                                    End If
                                                End If
                                                _listMovementsSelected.ForEach(Sub(x)
                                                                                   If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX Is Nothing Then
                                                                                       x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedFirstInstance
                                                                                   End If
                                                                               End Sub)
                                            End If

                                        End If
                                    End If
                                Else
                                    If Me.INDValueTxt.Text > itemMov.MaxValueAcceptedGeneral Then
                                        itemMov.ValueAcceptedFirstInstance = itemMov.MaxValueAcceptedGeneral
                                        If itemMov.MaxValueAcceptedGeneral > 0 Then
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                                   x.MaxValueAcceptedGeneral = 0
                                                                               End If
                                                                           End Sub)
                                        End If
                                    Else
                                        itemMov.ValueAcceptedFirstInstance = Me.INDValueTxt.Text
                                        _listMovementsSelected.ForEach(Sub(x)
                                                                           If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                               x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedFirstInstance
                                                                           End If
                                                                       End Sub)
                                    End If
                                End If
                            End If
                        End If

                        'REITERACION QX
                        If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                            itemMov.IdReiterationEvaluation = evaluationConcept.Id
                            itemMov.JustificationReiteration = Me.CommentHtml
                            itemMov.JustificationReiterationText = Me.TextCommentHtml
                            itemMov.IdResponseHierarchyReiteration = Me.IdResponseHierarchy
                            If _control = 1 Then
                                If itemMov.MainGlosa = True Then
                                    itemMov.ValueAcceptedSecondInstance = itemMov.ValueReiterated - IIf(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance) 'itemMov.MaxValueAcceptedGeneral
                                    If itemMov.MaxValueAcceptedGeneral > 0 Then
                                        itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                              If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                                  x.MaxValueAcceptedGeneral = 0
                                                                              End If
                                                                          End Sub)
                                    End If
                                End If
                            Else
                                If _control = 2 Then
                                    If itemMov.MainGlosa = True Then
                                        If itemMov.MaxValueAcceptedGeneral = 1 Then
                                            itemMov.ValueAcceptedSecondInstance = ""
                                        Else
                                            Dim maxvalueAcceptItem As Decimal = itemMov.ValueReiterated - IIf(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance)
                                            If maxvalueAcceptItem > 0 Then
                                                If _valueGLosado >= itemMov.ValueReiterated Then
                                                    itemMov.ValueAcceptedSecondInstance = maxvalueAcceptItem
                                                    _valueGLosado = _valueGLosado - maxvalueAcceptItem
                                                ElseIf maxvalueAcceptItem > _valueGLosado Then
                                                    itemMov.ValueAcceptedSecondInstance = _valueGLosado
                                                    _valueGLosado = 0
                                                End If
                                            End If
                                            itemMov.ListMovimientoAux.ForEach(Sub(x)
                                                                                  If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                                      x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedSecondInstance
                                                                                  End If
                                                                              End Sub)
                                        End If
                                    End If
                                Else
                                    If Me.INDValueTxt.Text > itemMov.MaxValueAcceptedGeneral Then
                                        itemMov.ValueAcceptedSecondInstance = itemMov.MaxValueAcceptedGeneral
                                        If itemMov.MaxValueAcceptedGeneral > 0 Then
                                            _listMovementsSelected.ForEach(Sub(x)
                                                                               If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                                   x.MaxValueAcceptedGeneral = 0
                                                                               End If
                                                                           End Sub)
                                        End If
                                    Else
                                        itemMov.ValueAcceptedSecondInstance = Me.INDValueTxt.Text
                                        _listMovementsSelected.ForEach(Sub(x)
                                                                           If x.InvoiceDetailId = itemMov.InvoiceDetailId AndAlso x.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX Then
                                                                               x.MaxValueAcceptedGeneral = x.MaxValueAcceptedGeneral - itemMov.ValueAcceptedSecondInstance
                                                                           End If
                                                                       End Sub)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                For Each item In _listMovementsSelected
                    item.StateEvaluation = StateEvaluation
                Next

                Dim result = Await Me._model.saveMov(_listMovementsSelected, _idOperativeUnit)
                If result IsNot Nothing Then
                    If result.StateResult = True Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            For Each item As String In result.MessageResult
                                strrMEnsajeGeneral.AppendLine(item)
                            Next
                        Else
                            strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesActualizado))
                        End If
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            For Each item As String In result.MessageResult
                                strrMEnsajeGeneral.AppendLine(item)
                            Next
                        Else
                            strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesContacteAdministrador))
                        End If
                    End If
                Else
                    strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesContacteAdministrador))
                End If

                If result.StateResult = True Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Else
                    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                End If
            Else
                If _option = 3 And _StrListInvoiceselected IsNot Nothing Then  'evaluacion general a nivel de facturas
                    For Each ObjD In _StrListInvoiceselected
                        Dim ListMovementInoviece As List(Of GlosaMovementGlosa) = Await _model.ListAllMovementGlosabymultipleInvoice(New List(Of String) From {ObjD}, String.Empty)
                        For Each itemMov As GlosaMovementGlosa In ListMovementInoviece 'por los movimientos de la factura
                            If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.DocumentType = 1 Then
                                itemMov.State = 2 'glosa evaluadaa
                            Else
                                itemMov.State = 4 'reiteracion evaluadaa
                            End If
                            If itemMov IsNot Nothing Then
                                'itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                                'itemMov.IdGlosaEvaluation = evaluationConcept.Id
                                '  itemMov.OtherMovements.Clear()
                                'GLOSA POR PRIMERA VEZ 
                                If itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                                    itemMov.CodeGlosaEvaluation = Me.INDConceptGEGle.EditValue
                                    itemMov.IdGlosaEvaluation = evaluationConcept.Id
                                    itemMov.JustificationGlosa = Me.CommentHtml
                                    itemMov.JustificationGlosaText = Me.TextCommentHtml
                                    itemMov.IdResponseHierarchyGlosa = Me.IdResponseHierarchy
                                    If _control = 1 Then
                                        If itemMov.MainGlosa = True Then
                                            itemMov.ValueAcceptedFirstInstance = itemMov.ValueGlosado
                                        Else
                                            itemMov.ValueAcceptedFirstInstance = 0
                                        End If
                                    Else
                                        itemMov.ValueAcceptedFirstInstance = 0
                                    End If
                                    'REITERACIONES
                                ElseIf itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or itemMov.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                                    itemMov.IdReiterationEvaluation = evaluationConcept.Id
                                    itemMov.JustificationReiteration = Me.CommentHtml
                                    itemMov.JustificationReiterationText = Me.TextCommentHtml
                                    itemMov.IdResponseHierarchyReiteration = Me.IdResponseHierarchy
                                    If _control = 1 Then
                                        If itemMov.MainGlosa = True Then
                                            itemMov.ValueAcceptedSecondInstance = If(itemMov.ValueReiterated Is Nothing, 0, itemMov.ValueReiterated) - If(itemMov.ValueAcceptedSecondInstance Is Nothing, 0, itemMov.ValueAcceptedSecondInstance)
                                        Else
                                            itemMov.ValueAcceptedSecondInstance = 0
                                        End If
                                    Else
                                        itemMov.ValueAcceptedFirstInstance = 0
                                    End If
                                End If
                            End If
                        Next 'por los movimientos de la factura

                        For Each item In ListMovementInoviece
                            item.StateEvaluation = StateEvaluation
                        Next

                        Dim result = Await Me._model.saveMov(ListMovementInoviece, _idOperativeUnit)
                        If result IsNot Nothing Then
                            If result.StateResult = True Then
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    For Each item As String In result.MessageResult
                                        strrMEnsajeGeneral.AppendLine("(" & ObjD.ToString & ")" & item)
                                    Next
                                Else
                                    strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesActualizado))
                                End If
                            Else
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    For Each item As String In result.MessageResult
                                        strrMEnsajeGeneral.AppendLine("(" & ObjD.ToString & ")" & item)
                                    Next
                                Else
                                    strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesContacteAdministrador))
                                End If
                            End If
                        Else
                            strrMEnsajeGeneral.AppendLine(obtenerRecurso(ComunesContacteAdministrador))
                        End If
                    Next 'fin recorrido de facturas

                    If strrMEnsajeGeneral.ToString <> String.Empty Then
                        Mensaje(EeventViewerImages.Informacion) = strrMEnsajeGeneral.ToString
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    End If
                End If 'fin evalñuacion general por factura
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Async Sub GridLookUpLoad()
        Dim LIstConceptEvaluation As List(Of Domain.Entities.ConceptGlosas) = Await Me._model.ListConceptsGlosaByTypes(New List(Of String) From {"2", "5", "6", "7", "8"})
        If _GeneralGLosaInvoice = True Then
            Me.INDlyiValueGeneral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Dim ObjItemDelete = LIstConceptEvaluation.Where(Function(x As Domain.Entities.ConceptGlosas) x.Code = "998" OrElse x.Type = "6").ToList()
            If ObjItemDelete?.Any() Then
                For Each item In ObjItemDelete
                    LIstConceptEvaluation.Remove(item)
                Next
            End If
        Else
            Me.INDlyiValueGeneral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        Me.INDConceptGEGle.Properties.DataSource = LIstConceptEvaluation
    End Sub

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(Me._tag))
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.SaveWithState(1)
    End Sub

    ''' <summary>
    ''' Se ejecuta la ccion de confirmar dependiendo del contexto en que se encuentre situado el proceso
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveWithState(2)
        End If
    End Sub

#End Region

End Class