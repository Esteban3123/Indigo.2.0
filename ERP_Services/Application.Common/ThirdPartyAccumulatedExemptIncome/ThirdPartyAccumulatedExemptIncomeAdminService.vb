Imports Application.Common
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class ThirdPartyAccumulatedExemptIncomeAdminService
    Implements IThirdPartyAccumulatedExemptIncomeAdminService, Inject

    Private ReadOnly _Repository As IThirdPartyAccumulatedExemptIncomeRepository

#Region "Builder"
    Public Sub New(ByVal Repository As IThirdPartyAccumulatedExemptIncomeRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _Repository = Repository
    End Sub
#End Region

    Public Function GetThirdPartyYear(idThirdParty As Integer, year As String, Optional tracking As Boolean = True) As ThirdpartyAccumulatedExemptIncome Implements IThirdPartyAccumulatedExemptIncomeAdminService.GetThirdPartyYear
        Return _Repository.GetThirdpartyYear(idThirdParty, year, False)
    End Function

    Public Function SaveThirdPartyAccumulatedExemptIncome(thirdParty As ThirdpartyAccumulatedExemptIncome) As ActionMessageResult(Of ThirdpartyAccumulatedExemptIncome) Implements IThirdPartyAccumulatedExemptIncomeAdminService.SaveThirdPartyAccumulatedExemptIncome
        Try

            _Repository.SaveEntity(thirdParty)
            _Repository.UnitWork.Commit()

            Return New ActionMessageResult(Of ThirdpartyAccumulatedExemptIncome) With {.StateResult = True, .ObjectEmbbeded = thirdParty}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of ThirdpartyAccumulatedExemptIncome) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function


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
