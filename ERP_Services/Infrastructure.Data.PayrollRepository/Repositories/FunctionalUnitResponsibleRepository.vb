Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base

Public Class FunctionalUnitResponsibleRepository

    Inherits GenericRepository(Of FunctionalUnitResponsible)
    Implements IFunctionalUnitResponsibleRepository
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

    Public Function GetListFunctionalUnitResponibleByUserId(UserID As Integer) As List(Of FunctionalUnitResponsible) Implements IFunctionalUnitResponsibleRepository.GetListFunctionalUnitResponibleByUserId
        Dim res = (From bfd In _context.FunctionalUnitResponsible Where bfd.UserId = UserID Select bfd).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then

            For Each ObjFunctionalUnitResponsible As FunctionalUnitResponsible In res
                ObjFunctionalUnitResponsible.CodeFunctionalUnit = (From d In _context.FunctionalUnit Where d.Id = ObjFunctionalUnitResponsible.FunctionalUnitId Select d.Code).FirstOrDefault()
                ObjFunctionalUnitResponsible.NameFunctionalUnit = (From d In _context.FunctionalUnit Where d.Id = ObjFunctionalUnitResponsible.FunctionalUnitId Select d.Name).FirstOrDefault()
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function
End Class
