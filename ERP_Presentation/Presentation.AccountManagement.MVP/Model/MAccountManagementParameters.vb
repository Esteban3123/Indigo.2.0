'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Andres Felipe Quintero Garcia
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
#End Region

Public Class MAccountManagementParameters
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag">Tag del funcional</param>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Me._sessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    Public Async Function GetAccountManagementParameters(id As Integer) As Task(Of ActionResult(Of AccountManagementParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetAccountManagementParametersByIdAsync(id)
    End Function

    Public Async Function SaveAccountManagementParameters(accountManagementParameters As Domain.Entities.AccountManagementParameters, audit As AuditMessage) As Task(Of Domain.Base.Entities.ActionResult(Of AccountManagementParameters))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SaveAccountManagementParametersAsync(accountManagementParameters, audit)
    End Function

    Public Async Function GetUsersWithDistributedAccounts() As Task(Of ActionResult(Of List(Of UsersAssignment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetUsersWithDistributedAccountsAsync()
    End Function
#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

