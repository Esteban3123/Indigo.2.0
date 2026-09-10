'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/06/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports Domain.Entities.Service
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance.MVP
Imports DevExpress.XtraEditors
Imports Presentation.Common.MVP
Imports DevExpress.Xpo
Imports System.Text
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Public Class FrmValidateSelectConcepts

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ListConceptEntity As List(Of AccountPayableDetailConcept), ListConceptXpo As List(Of Object))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeDatasource(ListConceptEntity, ListConceptXpo)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que me devuelve si aceptaron o cancelaron
    ''' </summary>
    ''' <remarks></remarks>
    Dim _acept As Boolean
    Public Property Acept As Boolean
        Get
            Return _acept
        End Get
        Set(value As Boolean)
            _acept = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' obtiene o establece los conceptos que se van a aplicar
    ''' </summary>
    Private DataSelected As List(Of ConceptsAccountPayableXpo)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDatasource(ListConceptEntity As List(Of AccountPayableDetailConcept), ListConceptXpo As List(Of Object))
        Dim ListObject As New List(Of Object)
        For Each itemConcept In ListConceptXpo
            Dim cont = ListConceptEntity.FindAll(Function(item) item.IdConceptAccountPayable = itemConcept.Id).Count
            If cont = 0 Then
                ListObject.Add(itemConcept)
            End If
        Next
        INDgcConcepts.DataSource = Nothing
        INDgcConcepts.DataSource = ListObject
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConcepts(sender As Object, e As List(Of ConceptsAccountPayableXpo))

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar si
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAcept_Click(sender As Object, e As EventArgs) Handles INDbtnAcept.Click
        If ValidateChecked() Then
            ''obtenes los datos seleccionados
            GetDataSelected()
            ''los enviamos al frmpopupbills
            RaiseEvent AddConcepts(Nothing, DataSelected)
            ''se deja en falso para que el ususario pueda ver los conceptos que se agregaron 
            Acept = False
            Me.Close()
        Else
            MessageIndigo.Show("Debe seleccionar un concepto para aplicar", MessageType.Information, Me.Text)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        ''si se oprime el boton no aplica significa que no se van a agregar conceptos y se debe guardar la factura tal y como esta
        Acept = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' function para validar si se ha selccionado algun concepto y se oprime el boton aplicar
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateChecked()
        Dim selectedRowHandles As Int32() = GridView1.GetSelectedRows()
        If selectedRowHandles.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Shown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _acept = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmValidateSelectConcepts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnAcept.Focus()
    End Sub

#End Region

#Region "Checked"
    ''' <summary>
    ''' obtiene la informacion de los conceptos seleccionados para agregarlos a la factura
    ''' </summary>
    ''' <returns></returns>
    Private Sub GetDataSelected()
        DataSelected = New List(Of ConceptsAccountPayableXpo)
        Dim selectedRowHandles As Int32() = GridView1.GetSelectedRows()
        Dim I As Integer
        For I = 0 To selectedRowHandles.Length - 1
            Dim selectedRowHandle As Int32 = selectedRowHandles(I)
            If (selectedRowHandle >= 0) Then
                Dim rowData = TryCast(GridView1.GetRow(selectedRowHandle), Infrastructure.Data.Xpo.CommonRepository.ConceptsAccountPayableXpo)

                DataSelected.Add(rowData)
            End If
        Next
    End Sub

#End Region
#End Region

End Class