Imports Application.Treasury
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class TreasuryService

    ''' <summary>
    ''' Función para Eliminar los convenios de redención de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteAgreementsRedemptionPoints(AgreementsRedemptionPoints As Domain.Entities.AgreementsRedemptionPoints, audit As AuditMessage) As ActionResult Implements IAgreementsRedemptionPoints.DeleteAgreementsRedemptionPoints
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.DeleteAgreementsRedemptionPoints(AgreementsRedemptionPoints, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene un convenios de redención de puntos por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>AgreementsRedemptionPoints</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsByCode(Code As String, session As SessionValues) As Domain.Entities.AgreementsRedemptionPoints Implements IAgreementsRedemptionPoints.GetAgreementsRedemptionPointsByCode
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.GetAgreementsRedemptionPointsByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Lista de Fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllAgreementsRedemptionPoints(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.AgreementsRedemptionPoints) Implements IAgreementsRedemptionPoints.ListAllAgreementsRedemptionPoints
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.ListAllAgreementsRedemptionPoints()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar un convenio de redención de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints As Domain.Entities.AgreementsRedemptionPoints, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AgreementsRedemptionPoints) Implements IAgreementsRedemptionPoints.SaveAgreementsRedemptionPoints
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Función para cambiar el estado a un convenio de redención de puntos
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function ChangeAgreementsRedemptionPointsStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AgreementsRedemptionPoints) Implements IAgreementsRedemptionPoints.ChangeAgreementsRedemptionPointsStatus
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.ChangeAgreementsRedemptionPointsStatus(Code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función para traer los detalles de los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail) Implements IAgreementsRedemptionPoints.GetAgreementsRedemptionPointsDetails
        Using service As IAgreementsRedemptionPointsAdminService = Container.Current.Resolve(Of IAgreementsRedemptionPointsAdminService)()
            Return service.GetAgreementsRedemptionPointsDetails(Id)
        End Using
    End Function

End Class
