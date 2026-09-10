Imports Presentation.Glosas.MVP
Imports Domain.Entities

'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 28-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Popup de Reasignación de Responsables
''' </summary>
Public Class FrmResponsibleTransferPopup

    ''' <summary>
    ''' Id del Responsable
    ''' </summary>
    Public _responsibleId As String
    ''' <summary>
    ''' Id del Concepto
    ''' </summary>
    Public _ConceptId As String
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MResponsibleTransfer
    ''' <summary>
    ''' Id del responsable al cual se le 
    ''' cambiará el movimiento
    ''' </summary>
    Private _idResponsibleAux As String
    ''' <summary>
    ''' Variable para indicar si es responsable
    ''' o concepto lo que se va a reasignar
    ''' </summary>
    Public _opt As Integer


    Sub New(TagForm As String, IdResponsibleAux As String, Opt As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Model = New MResponsibleTransfer(TagForm)
        Me._idResponsibleAux = IdResponsibleAux
        Me._opt = Opt
        If Me._opt = 1 Then
            Me.INDResponsibleLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.Text = "Reasignar responsables"
        ElseIf Me._opt = 2 Then
            Me.INDConceptLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.Text = "Reasignar conceptos"
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _responsibleId = Nothing
        _ConceptId = Nothing
        Model = Nothing
        _idResponsibleAux = Nothing
        _opt = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Async Sub FrmResponsibleTransferPopup_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me._opt = 1 Then
            Dim ListResponsibles As List(Of ResponsibleAll) = Await Me.Model.GetResponsibleALL
            Me.INDResponsibleTransferGle.Properties.DataSource = ListResponsibles.Where(Function(x) x.Id <> Me._idResponsibleAux).ToList
        ElseIf Me._opt = 2 Then
            Dim listTypes As New List(Of String)
            listTypes.Add("1")
            Dim ListConcepts As List(Of Domain.Entities.ConceptGlosas) = Await Me.Model.getConcepts(listTypes)
            Me.INDConceptsTransferGle.Properties.DataSource = ListConcepts
        End If

    End Sub

    ''' <summary>
    ''' Evento click sobre el boton reasignar
    ''' </summary>
    Private Sub INDResponsibleTransferSmb_Click(sender As Object, e As EventArgs) Handles INDResponsibleTransferSmb.Click
        If Me._opt = 1 Then
            If Me.INDResponsibleTransferGle.EditValue IsNot Nothing Then
                Me._responsibleId = Me.INDResponsibleTransferGle.EditValue
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            End If
        ElseIf Me._opt = 2 Then
            If Me.INDConceptsTransferGle.EditValue IsNot Nothing Then
                Me._ConceptId = Me.INDConceptsTransferGle.EditValue
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            End If
            End If
    End Sub

End Class