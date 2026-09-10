'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceCrossingAccountDetailCxP

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetCrossingAccountDetailCxPById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxP Implements ITreasuryServiceCrossingAccountDetailCxP.GetCrossingAccountDetailCxPById
        Using service As ICrossingAccountDetailCxPAdminService = Container.Current.Resolve(Of ICrossingAccountDetailCxPAdminService)()
            Return service.GetCrossingAccountDetailCxPById(Id, tracking)
        End Using
        'Return Me._crossingAccountDetailCxPAdminService.GetCrossingAccountDetailCxPById(Id, tracking)
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    ''' <param name="crossingAccountId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxP) Implements ITreasuryServiceCrossingAccountDetailCxP.ListCrossingAccountDetailCxPByCrossingAccountId
        Using service As ICrossingAccountDetailCxPAdminService = Container.Current.Resolve(Of ICrossingAccountDetailCxPAdminService)()
            Return service.ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId, tracking)
        End Using
        'Return Me._crossingAccountDetailCxPAdminService.ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId, tracking)
    End Function

End Class
