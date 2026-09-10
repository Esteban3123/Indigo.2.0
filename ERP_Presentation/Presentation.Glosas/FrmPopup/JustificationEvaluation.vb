#Region "Imports"

Imports System.ComponentModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Glosas.MVP

#End Region

Public Class JustificationEvaluation

    ''' <summary>
    ''' Codigo concepto de evalucaion 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _codeConcept As String

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _model As MCoodination

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Tag formulario Evaluación
    ''' </summary>
    Private _tagEvaluation As String
    ''' <summary>
    ''' saber si ya tiene una jutificacion para no cargar la de por defecto
    ''' </summary>
    ''' <remarks></remarks>
    Private _haveTexto As Boolean


    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="codeCocept"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal codeCocept As String = "0")
        ' This call is required by the designer.
        InitializeComponent()
        _codeConcept = codeCocept
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    ''' Laod Frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub JustificationEvaluation_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._indigoSessionValues = SessionValues.Instance
        Me._tagEvaluation = "534" ' Formulario de Evaluaciones
        Me._model = New MCoodination(Me._tagEvaluation)
        Me.INDgleResponseHierarchy.View.OptionsView.ShowGroupPanel = False
        Await Me.DataLoad()
        Dim ConceptGlosa = Await Me._model.ConceptsGlosaByCode(_codeConcept)
        If ConceptGlosa IsNot Nothing AndAlso
            (_codeConcept = "0" OrElse
                CInt(ConceptGlosa.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.NoSubsanada OrElse
                CInt(ConceptGlosa.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.SubsanadaParcial) Then
            INDlyiResponseHierarchy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            ' Si concepto de aceptación tiene un valor previamente guardado
            If IdResponseHierarchy.HasValue Then
                Dim responseHierarchy = _model.GetResponseHierarchyById(IdResponseHierarchy.Value)
                If responseHierarchy IsNot Nothing Then
                    Me.INDgleResponseHierarchy.DisplayNullText = responseHierarchy.CodeName
                End If
            End If
        Else
            INDlyiResponseHierarchy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub


    ''' <summary>
    ''' Cargar plantillas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DataLoad() As Task
        Dim concepts = Await Me._model.listJustificationTemplate(_codeConcept)
        If concepts.Count > 0 Then
            Me.INDgleTemplates.Properties.NullText = "Seleccione un concepto"
            Me.INDgleTemplates.Properties.DataSource = concepts
            ' INDgleTemplates.Properties.DataSource = concepts
        Else
            Me.INDgleTemplates.Properties.DataSource = Nothing
            Me.INDgleTemplates.Properties.NullText = "No existen conceptos"
        End If
        Dim conceptDefault = concepts.Where(Function(x) x.IsDefault = True).FirstOrDefault
        If haveText = False Then 'si no tiene texto, cargo el por defecto de la plantilla
            If conceptDefault IsNot Nothing Then
                Me.INDgleTemplates.EditValue = conceptDefault.Id
                Me.INDrecComment.HtmlText = conceptDefault.Justification
            End If
        End If
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _codeConcept = Nothing
        _model = Nothing
        _tagEvaluation = Nothing
        _haveTexto = Nothing
    End Sub

    ''' <summary>
    ''' Click Boton Aplicar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If Me.INDgleTemplates.EditValue IsNot Nothing Then
            Dim data = CType(Me.INDgleTemplates.GetSelectedDataRow(), JustificationTemplate)
            If data IsNot Nothing AndAlso data.Justification IsNot Nothing Then
                Me.INDrecComment.HtmlText = data.Justification
                ' Me.INDTemplatesGle.EditValue = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtener o asignar comentario de evaluacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CommentHtml As String
        Get
            Return Me.INDrecComment.HtmlText
        End Get
        Set(value As String)
            Me.INDrecComment.HtmlText = value
        End Set
    End Property


    Public Property CommentOnlyText As String
        Get
            Return Me.INDrecComment.Text
        End Get
        Set(value As String)
            Me.INDrecComment.Text = value
        End Set
    End Property

    ''' <summary>
    ''' saber si ya tiene una jutificacion para no cargar la de por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property haveText As Boolean
        Get
            Return Me._haveTexto
        End Get
        Set(value As Boolean)
            Me._haveTexto = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna el ID del concepto de Aceptación Jerarquíco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdResponseHierarchy As Integer?
        Get
            Return INDgleResponseHierarchy.EditValue
        End Get
        Set(value As Integer?)
            Me.INDgleResponseHierarchy.EditValue = value
        End Set
    End Property

    Private Property DatasourceResponseHierarchy As Object
        Get
            Return INDgleResponseHierarchy.Datasource
        End Get
        Set(value As Object)
            Me.INDgleResponseHierarchy.Datasource = value
        End Set
    End Property


    Private Sub INDgleResponseHierarchy_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleResponseHierarchy.QueryPopUp
        If DatasourceResponseHierarchy Is Nothing Then
            DatasourceResponseHierarchy = _model.ListResponseHierarchy()
        End If
    End Sub
End Class