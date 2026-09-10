#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene un CCPET por código
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Function GetBudgetId(Id As Integer, audit As AuditMessage) As ActionResult(Of CCPET) Implements IBudgetServiceCCPET.GetCCPETById
        Using service As ICCPETAdminService = Container.Current.Resolve(Of ICCPETAdminService)()
            Return service.GetCCPETById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un CCPET por código
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetBudgetCode(Code As String, audit As AuditMessage) As ActionResult(Of CCPET) Implements IBudgetServiceCCPET.GetCCPETByCode
        Using service As ICCPETAdminService = Container.Current.Resolve(Of ICCPETAdminService)()
            Return service.GetCCPETByCode(Code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="CCPET">la entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult(Of CCPET) Implements IBudgetServiceCCPET.SaveCCPET
        Using service As ICCPETAdminService = Container.Current.Resolve(Of ICCPETAdminService)()
            Return service.SaveCCPET(CCPET, audit)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="CCPET">The CCPET.</param>
    ''' <returns></returns>
    Public Function ChangeStatusCCPET(CCPET As CCPET, status As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CCPET) Implements IBudgetServiceCCPET.ChangeStatusCCPET
        Using service As ICCPETAdminService = Container.Current.Resolve(Of ICCPETAdminService)()
            Return service.ChangeStatusCCPET(CCPET, status, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CCPET">La entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult Implements IBudgetServiceCCPET.DeleteCCPET
        Using service As ICCPETAdminService = Container.Current.Resolve(Of ICCPETAdminService)()
            Return service.DeleteCCPET(CCPET, audit)
        End Using
    End Function
End Class