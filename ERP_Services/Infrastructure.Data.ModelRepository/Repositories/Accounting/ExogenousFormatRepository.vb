Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ExogenousFormatRepository
    Inherits GenericRepository(Of ExogenousFormat)
    Implements IExogenousFormatRepository

#Region "Fields"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        Me._context = context
    End Sub

#End Region

    Public Function GetExogenousFormatById(id As Integer, Optional tracking As Boolean = True) As ExogenousFormat Implements IExogenousFormatRepository.GetExogenousFormatById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ExogenousFormat.Include("ExogenousFormatDetail") Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res(0).OriginalValue = (From d In Me._context.ExogenousFormat.AsNoTracking.Include("ExogenousFormatDetail").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New ExogenousFormat()
        End If
    End Function

    Public Function GetExogenousFormatByCode(code As String, Optional tracking As Boolean = True) As ExogenousFormat Implements IExogenousFormatRepository.GetExogenousFormatByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ExogenousFormat In Me._context.ExogenousFormat.AsNoTracking.Include("ExogenousFormatDetail").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            If res.ExogenousFormatDetail IsNot Nothing AndAlso res.ExogenousFormatDetail.Count > 0 Then
                For Each itemDetail As ExogenousFormatDetail In res.ExogenousFormatDetail
                    Dim account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.MainAccountId Select ma).FirstOrDefault
                    itemDetail.MainAccountNumberName = account.Number + " - " + account.Name
                Next
            End If
            Return res
        Else
            Return New ExogenousFormat()
        End If
    End Function
End Class
