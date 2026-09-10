Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base

Public Class PositionUserRepository

    Inherits GenericRepository(Of PositionUser)
    Implements IPositionUserRepository

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetListPositionUserByUserId(UserID As Integer) As List(Of PositionUser) Implements IPositionUserRepository.GetListPositionUserByUserId
        Dim res = (From bfd In _context.PositionUser Where bfd.IdUser = UserID Select bfd).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then

            For Each ObjPositionUser As PositionUser In res
                ObjPositionUser.CodePosition = (From d In _context.Position Where d.Id = ObjPositionUser.IdPosition Select d.Code).FirstOrDefault()
                ObjPositionUser.NamePosition = (From d In _context.Position Where d.Id = ObjPositionUser.IdPosition Select d.Name).FirstOrDefault()
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function
End Class
