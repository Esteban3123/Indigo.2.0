'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/02/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities

#End Region

Public Class FrmInvoiceReversalCausation

#Region "Properties"

    ''' <summary>
    ''' Listado de detalles de liquidacion que tienen la bandera de invoiceReversal en True en la causacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _listMedicalFeesLiquidationDetail As List(Of MedicalFeesLiquidationDetail)
    Public Property ListMedicalFeesLiquidationDetail As List(Of MedicalFeesLiquidationDetail)
        Get
            Return _listMedicalFeesLiquidationDetail
        End Get
        Set(value As List(Of MedicalFeesLiquidationDetail))
            _listMedicalFeesLiquidationDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Bandera para saber si aplica o no aplica el recalculo True = Aplica , False = No Aplica
    ''' </summary>
    ''' <remarks></remarks>
    Private _banClose As Boolean
    Public Property BanClose As Boolean
        Get
            Return _banClose
        End Get
        Set(value As Boolean)
            _banClose = value
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listMedicalFeesLiquidationDetail = Nothing
        _banClose = Nothing
    End Sub
    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInvoiceReversalCausation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDgcCausations.DataSource = Nothing
        INDgcCausations.DataSource = ListMedicalFeesLiquidationDetail
        INDbtnYes.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar si
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnYes_Click(sender As Object, e As EventArgs) Handles INDbtnYes.Click
        BanClose = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnNo_Click(sender As Object, e As EventArgs) Handles INDbtnNo.Click
        BanClose = False
        Me.Close()
    End Sub

#End Region

#End Region

End Class