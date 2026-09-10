Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base
Imports Domain.Payroll

Public Class GroupRepository

    Inherits GenericRepository(Of Group)
    Implements IGroupRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un Grupo
    ''' </summary>
    ''' <param name="code">Código del Grupo</param>
    ''' <returns>Grupo</returns>
    Public Function GetGroup(code As String, Optional tracking As Boolean = True) As Group Implements IGroupRepository.GetGroup
        Dim contextGroup = _context.Group
        Dim Group As IQueryable(Of Group)
        If tracking = False Then
            Group = From e In contextGroup.AsNoTracking.Include("PayrollParameter").AsNoTracking _
                    .Include("GroupEventConcept").AsNoTracking.Include("GroupEventConcept.Concept").AsNoTracking.Include("GroupAdjustConceptContractLiquidation").AsNoTracking.Include("GroupAdjustConceptContractLiquidation.Concept").AsNoTracking
                    Where e.Code = code
                    Select e
        Else
            Group = From e In contextGroup.Include("PayrollParameter").Include("GroupEventConcept").Include("GroupEventConcept.Concept").Include("GroupAdjustConceptContractLiquidation").Include("GroupAdjustConceptContractLiquidation.Concept")
                    Where e.Code = code
                    Select e
        End If
        If Group.Count > 0 Then
            Dim GroupData = Nothing
                GroupData = Group.SingleOrDefault
            Return GroupData
        Else
            Return New Group
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los Grupos
    ''' </summary>
    ''' <returns>Lista de Grupos</returns>
    ''' <remarks></remarks>
    Public Function ListAllGroups() As List(Of Group) Implements IGroupRepository.ListAllGroups
        Dim Group = From e In _context.Group.Include("PayrollParameter")
                              Select e
        Return Group.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un Grupo por ID
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Grupo por Id</returns>
    ''' <remarks></remarks>
    Public Function GetGroupById(groupId As String) As Group Implements IGroupRepository.GetGroupById
        Dim Group = From e In _context.Group.Include("PayrollParameter").Include("Company").Include("Company.ThirdParty").Include("GroupAdjustConceptContractLiquidation").Include("GroupAdjustConceptContractLiquidation.Concept")
                    Where e.Id = groupId
                    Select e

        If Group.Count > 0 Then
            Return Group.Single
        Else
            Return New Group
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Grupo por ID
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Grupo por Id</returns>
    ''' <remarks></remarks>
    Public Function GetGroupSimpleById(groupId As Integer) As Group Implements IGroupRepository.GetGroupSimpleById
        Dim query = (From e In _context.Group.AsNoTracking Where e.Id = groupId Select e).FirstOrDefault
        If query IsNot Nothing Then
            Return query
        Else
            Return New Group()
        End If
    End Function

    ''' <summary>
    ''' Obtiene unos grupos filtrado por empresa
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupsByCompanyId(ByVal companyId As String) As List(Of Group) Implements IGroupRepository.GetGroupsByCompanyId
        Dim groups = From e In _context.Group.Include("PayrollParameter")
            Where e.CompanyId = companyId
            Select e

        Return groups.ToList
    End Function

    ''' <summary>
    ''' Grupo y sus correspondientes liquidaciones
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Public Function GetGroupLiquidationById(ByVal GroupId As String) As List(Of Group) Implements IGroupRepository.GetGroupLiquidationById
        Dim groups = From e In _context.Group.Include("Liquidation")
           Where e.Id = GroupId
           Select e

        Return groups.ToList()
    End Function

    ''' <summary>
    ''' Lista Todos los Grupos
    ''' </summary>
    ''' <returns>Lista de Grupos</returns>
    ''' <remarks></remarks>
    Public Function ListGroupsByStatus(Status As Boolean) As List(Of Group) Implements IGroupRepository.ListGroupsByStatus
        Dim Group = From e In _context.Group.Include("PayrollParameter")
                    Where e.State = Status
                              Select e
        Return Group.ToList()
    End Function

End Class
