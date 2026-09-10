'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCrossingAccountDetailCxP

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    <OperationContract()>
    Function GetCrossingAccountDetailCxPById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxP

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    <OperationContract()>
    Function ListCrossingAccountDetailCxPByCrossingAccountId(ByVal crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxP)

End Interface