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
Public Interface ITreasuryServiceCrossingAccountDetailCxC

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    <OperationContract()>
    Function GetCrossingAccountDetailCxCById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxC

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    <OperationContract()>
    Function ListCrossingAccountDetailCxCByCrossingAccountId(ByVal crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxC)

End Interface