Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ReligiousBeliefsRepository
    Inherits GenericRepository(Of ReligiousBeliefs)
    Implements IReligiousBeliefsRepository

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetReligiousBeliefs(pCode As String, pTracking As Boolean) As ReligiousBeliefs Implements IReligiousBeliefsRepository.GetReligiousBeliefs
        If pCode Is Nothing OrElse pCode.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("pCode")
        End If
        Dim res As New ReligiousBeliefs()
        If pTracking Then
            res = (From d As ReligiousBeliefs In Me._context.ReligiousBeliefs
                   Where d.Code.Equals(pCode.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From g In _context.ReligiousBeliefs.AsNoTracking
                   Where g.Code.Equals(pCode.Trim())
                   Select g).FirstOrDefault
        End If
        Return res
    End Function

    Public Function GetReligiousBeliefsByID(pID As Integer, pTracking As Boolean) As ReligiousBeliefs Implements IReligiousBeliefsRepository.GetReligiousBeliefsByID
        If pID = 0 Then
            Throw New ArgumentNullException("pID")
        End If
        Dim res As New ReligiousBeliefs()
        If pTracking Then
            res = (From d In Me._context.ReligiousBeliefs Where d.Id = pID Select d).FirstOrDefault
        Else
            res = (From d As ReligiousBeliefs In Me._context.ReligiousBeliefs.AsNoTracking() Where d.Id = pID Select d).SingleOrDefault()
        End If
        Return res
    End Function
End Class
