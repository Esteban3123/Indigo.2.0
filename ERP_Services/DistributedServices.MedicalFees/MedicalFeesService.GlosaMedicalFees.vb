Imports Application.MedicalFees
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MedicalFeesService

    ''' <summary>
    ''' Obtiene todos los registros de la tabla
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllGlosaMedicalFees(audit As AuditMessage) As List(Of Domain.Entities.GlosaMedicalFees) Implements IGlosaMedicalFeesService.ListAllGlosaMedicalFees
        Using service As IGlosaMedicalFeesAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesAdminService)()
            Return service.ListAllGlosaMedicalFees()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetGlosaMedicalFeesByCode(Code As String) As GlosaMedicalFees Implements IGlosaMedicalFeesService.GetGlosaMedicalFeesByCode
        Using service As IGlosaMedicalFeesAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesAdminService)()
            Return service.GetGlosaMedicalFeesByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(Id As Integer) As AccountPayable Implements IGlosaMedicalFeesService.GetAccountPayableById
        Using service As IGlosaMedicalFeesAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesAdminService)()
            Return service.GetAccountPayableById(Id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFees"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveGlosaMedicalFees(GlosaMedicalFees As Domain.Entities.GlosaMedicalFees, idSequense As Int64, audit As AuditMessage) As ActionResult(Of GlosaMedicalFees) Implements IGlosaMedicalFeesService.SaveGlosaMedicalFees
        Using service As IGlosaMedicalFeesAdminService = Container.Current.Resolve(Of IGlosaMedicalFeesAdminService)()
            Return service.SaveGlosaMedicalFees(GlosaMedicalFees, audit, idSequense)
        End Using
    End Function

End Class