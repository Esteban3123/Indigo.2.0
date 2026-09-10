Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base

Public Class PositionRollRepository

    Inherits GenericRepository(Of PositionRoll)
    Implements IPositionRollRepository


#Region "Builder"

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

    Public Function GetListPositionRollByRole(RoleID As Integer) As List(Of PositionRoll) Implements IPositionRollRepository.GetListPositionRollByRole
        Dim res = (From bfd In _context.PositionRoll.AsNoTracking Where bfd.IdRol = RoleID Select bfd).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then

            For Each ObjPositionRol As PositionRoll In res
                ObjPositionRol.CodePosition = (From d In _context.Position Where d.Id = ObjPositionRol.IdPosition Select d.Code).FirstOrDefault()
                ObjPositionRol.NamePosition = (From d In _context.Position Where d.Id = ObjPositionRol.IdPosition Select d.Name).FirstOrDefault()
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function

    Public Function GetListPositionRollByRoleCode(RoleCode As String) As List(Of PositionRoll) Implements IPositionRollRepository.GetListPositionRollByRoleCode
        Dim res = (From bfd In _context.PositionRoll.AsNoTracking Where bfd.CodeRol = RoleCode Select bfd).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then

            For Each ObjPositionRol As PositionRoll In res
                ObjPositionRol.CodePosition = (From d In _context.Position Where d.Id = ObjPositionRol.IdPosition Select d.Code).FirstOrDefault()
                ObjPositionRol.NamePosition = (From d In _context.Position Where d.Id = ObjPositionRol.IdPosition Select d.Name).FirstOrDefault()
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function

#End Region



End Class
