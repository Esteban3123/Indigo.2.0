Imports System.Threading.Tasks
Imports Domain.Base.Entities

Public Class PMassiveConfirm

#Region "Properties"

    Public view As IMassiveConfirm

#End Region

#Region "Builder"

    Public Sub New(view As IMassiveConfirm)
        Me.view = view
    End Sub

#End Region

#Region "Methods"

#Region "Individual"

    Public Async Function ConfirmGlosaDocument(processId As Integer, code As String, Optional operativeUnitId As Integer = 0) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmGlosaDocument(processId, code, operativeUnitId)
        End Using
    End Function

    Public Async Function ConfirmAccountingDocument(code As String) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmAccountingDocument(code)
        End Using
    End Function

    Public Async Function ConfirmPaymentDocument(processId As Integer, code As String) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmPaymentDocument(processId, code)
        End Using
    End Function

    Public Async Function ConfirmPortfolioDocument(processId As Integer, code As String) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmPortfolioDocument(processId, code)
        End Using
    End Function

    Public Async Function ConfirmInventoryDocument(processId As Integer, code As String) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmInventoryDocument(processId, code)
        End Using
    End Function

    Public Async Function ConfirmTreasuryDocument(processId As Integer, code As String) As Task(Of ActionResult(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return Await model.ConfirmTreasuryDocument(processId, code)
        End Using
    End Function

#End Region

#Region "Lists"

    Public Function ConfirmDocumentsGlosas(processId As Integer, listDocuments As List(Of String), Optional operativeUnitId As Integer = 0) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsGlosas(processId, listDocuments, operativeUnitId)
        End Using
    End Function

    Public Function ConfirmDocumentsAccounting(listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsAccounting(listDocuments)
        End Using
    End Function

    Public Function ConfirmDocumentsPayments(processId As Integer, listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsPayments(processId, listDocuments)
        End Using
    End Function

    Public Function ConfirmDocumentsPortfolio(processId As Integer, listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsPortfolio(processId, listDocuments)
        End Using
    End Function

    Public Function ConfirmDocumentsInventory(processId As Integer, listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsInventory(processId, listDocuments)
        End Using
    End Function

    Public Function ConfirmDocumentsTreasury(processId As Integer, listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Using model As New MMassiveConfirm(Me.view.MyTag)
            Return model.ConfirmDocumentsTreasury(processId, listDocuments)
        End Using
    End Function

#End Region

#End Region

End Class
