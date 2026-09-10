
Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class EthnicGroupsRepository
    Inherits GenericRepository(Of EthnicGroups)
    Implements IEthnicGroupsRepository

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetEthnicGroupsByCode(code As String, tracking As Boolean) As EthnicGroups Implements IEthnicGroupsRepository.GetEthnicGroups
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New EthnicGroups()
        If tracking Then
            res = (From d As EthnicGroups In Me._context.EthnicGroups
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From g In _context.EthnicGroups.AsNoTracking
                   Where g.Code.Equals(code.Trim())
                   Select g).FirstOrDefault
        End If
        Return res
    End Function

    Public Function GetEthnicGroupsById(id As Integer, tracking As Boolean) As EthnicGroups Implements IEthnicGroupsRepository.GetEthnicGroupsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As New EthnicGroups()
        If tracking Then
            res = (From d In Me._context.EthnicGroups Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d As EthnicGroups In Me._context.EthnicGroups.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
        End If
        Return res
    End Function

End Class
