Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteSupplier(Supplier As Domain.Entities.Supplier, listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), session As SessionValues) As ActionResult Implements ISupplierService.DeleteSupplier
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.DeleteSupplier(Supplier, listSupplierDistributionLines, listSupplierDetailType, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetSupplier(codeSupplier As String, session As SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplier
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplier(codeSupplier)
        End Using
    End Function

    Public Function GetSupplierById(id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplierById
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplierById(id)
        End Using
    End Function

    Public Function ListAllBSupplier(Empresa As String) As List(Of Domain.Entities.Supplier) Implements ISupplierService.ListAllBSupplier
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.ListAllSupplier
        End Using
    End Function

    Public Function SaveSupplier(Supplier As Domain.Entities.Supplier, listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), mode As Boolean, session As SessionValues) As ActionResult(Of Supplier) Implements ISupplierService.SaveSupplier
        Using SupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return SupplierAdmin.SaveSupplier(Supplier, listSupplierDistributionLines, listSupplierDetailType, mode, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    Public Function GetSupplierByIdThirdParty(Id As Integer, ByVal session As SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplierByIdThirdParty
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplierByIdThirdParty(Id)
        End Using
    End Function

    Public Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplierByIdThirdPartyWithThirdAdded
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplierByIdThirdPartyWithThirdAdded(Id)
        End Using
    End Function

    Public Function GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird As Integer, IdAccountAccounting As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplierByIdThirdPartyAndIdAccountAccounting
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird, IdAccountAccounting)
        End Using
    End Function

    Public Function GetThirdPartyById(id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.ThirdParty Implements ISupplierService.GetThirdPartyById
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetThirdPartyById(id)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Supplier) Implements ISupplierService.ChangeState
        Using SupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return SupplierAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el tercero por id proveedor
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByIdSupplier(Id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.ThirdParty Implements ISupplierService.GetThirdPartyByIdSupplier
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetThirdPartyByIdSupplier(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el Proveedor por Nit De Tercero
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierByNitThirdParty(Nit As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Supplier Implements ISupplierService.GetSupplierByNitThirdParty
        Using DeleteSupplierAdmin As ISupplierAdminService = Container.Current.Resolve(Of ISupplierAdminService)()
            Return DeleteSupplierAdmin.GetSupplierByNitThirdParty(Nit)
        End Using
    End Function

End Class
