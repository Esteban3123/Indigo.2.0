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
    Implements ITreasuryServiceCrossingAccountDetailCxC

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    Public Function GetCrossingAccountDetailCxCById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxC Implements ITreasuryServiceCrossingAccountDetailCxC.GetCrossingAccountDetailCxCById
        Using service As ICrossingAccountDetailCxCAdminService = Container.Current.Resolve(Of ICrossingAccountDetailCxCAdminService)()
            Return service.GetCrossingAccountDetailCxCById(Id, tracking)
        End Using
        'Return Me._crossingAccountDetailCxCAdminService.GetCrossingAccountDetailCxCById(Id, tracking)
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    Public Function ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxC) Implements ITreasuryServiceCrossingAccountDetailCxC.ListCrossingAccountDetailCxCByCrossingAccountId
        Using service As ICrossingAccountDetailCxCAdminService = Container.Current.Resolve(Of ICrossingAccountDetailCxCAdminService)()
            Return service.ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId, tracking)
        End Using
        'Return Me._crossingAccountDetailCxCAdminService.ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId, tracking)
    End Function

End Class
