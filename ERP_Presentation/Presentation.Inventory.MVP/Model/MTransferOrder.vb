'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 13/05/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
#End Region

Public Class MTransferOrder
    Implements IDisposable

#Region "Fields"

     ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una orden de traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTransferOrderById(id As Integer) As TransferOrder
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetTransferOrderById(id)
    End Function

    ''' <summary>
    ''' obtiene una oden de traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetTranferOrderByCode(code As String) As Task(Of TransferOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetTranferOrderByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' guarda, actualiza y confirma una orden traslado
    ''' </summary>
    ''' <param name="transferOrder"></param>
    ''' <returns></returns>
    Public Async Function SaveTrasnferOrder(transferOrder As TransferOrder) As Task(Of ActionResult(Of TransferOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveTrasnferOrderAsync(transferOrder, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' cambiar estado del item de la solicitud desde orden de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateInventoryRequestDetail(ListViewListRequestDetailImport As List(Of ViewListRequestDetailImport))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateInventoryRequestDetail(ListViewListRequestDetailImport)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
