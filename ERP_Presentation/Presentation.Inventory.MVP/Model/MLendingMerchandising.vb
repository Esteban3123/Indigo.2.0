'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 20/04/2015
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
#End Region

Public Class MLendingMerchandising
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Tago del formulario
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
        _indigoSessionValues = SessionValues.Instance
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.InventorySequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' obtiene una solicitud de prestamo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLoanMerchadiseByCode(code As String) As Task(Of LoanMerchandise)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetLoanMerchadiseByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una solicitud de prestamo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchadiseById(id As Integer) As LoanMerchandise
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetLoanMerchadiseById(id)
    End Function
    ''' <summary>
    ''' guardar una solicitud de prestamo
    ''' </summary>
    ''' <param name="LoanMerchandise"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveLoanMerchadise(LoanMerchandise As LoanMerchandise, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of LoanMerchandise))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveLoanMerchadiseAsync(LoanMerchandise, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una solicitud de prestamo
    ''' </summary>
    ''' <param name="LoanMerchandise"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmLoadMerchadise(LoanMerchandise As LoanMerchandise, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.LoanMerchandise))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmLoadMerchadiseAsync(LoanMerchandise, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC, action)
    End Function
    ''' <summary>
    ''' lista los detalles de una solicitud de prestamo
    ''' </summary>
    ''' <param name="IdLoanMerchandise"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise As Integer, isdevolution As Boolean) As List(Of LoanMerchandiseDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise, isdevolution)
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
