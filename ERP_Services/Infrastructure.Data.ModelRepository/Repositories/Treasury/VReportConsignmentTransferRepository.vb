Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class VReportConsignmentTransferRepository
    Inherits GenericRepository(Of VReportConsignmentTransfer)
    Implements IVReportConsignmentTransferRepository

    'Contexto de treasury
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetVReportConsignmentTransferByCode(Code As String) As VReportConsignmentTransfer Implements IVReportConsignmentTransferRepository.GetVReportConsignmentTransferByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From rct As VReportConsignmentTransfer In Me._context.VReportConsignmentTransfer Where rct.Code.Equals(Code.Trim()) Select rct).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From rct As VReportConsignmentTransfer In Me._context.VReportConsignmentTransfer.AsNoTracking() Where rct.Code.Equals(Code.Trim()) Select rct).SingleOrDefault()
            Return res(0)
        Else
            Return New VReportConsignmentTransfer()
        End If
    End Function

End Class
