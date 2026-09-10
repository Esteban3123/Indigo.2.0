Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class MaintenancePlanProgramatedRepository
    Inherits GenericRepository(Of MaintenancePlanProgramated)
    Implements IMaintenancePlanProgramatedRepository

    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetMaintenancePlanProgramatedById(id As Integer) As MaintenancePlanProgramated Implements IMaintenancePlanProgramatedRepository.GetMaintenancePlanProgramatedById
        Return (From e In _context.MaintenancePlanProgramated Where e.Id = id Select e).FirstOrDefault()
    End Function

End Class
