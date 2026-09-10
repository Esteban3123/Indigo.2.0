'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Security.Entities

Public Interface ICrossingAccountDetailCxCRepository
    Inherits IRepository(Of CrossingAccountDetailCxC)

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    Function GetCrossingAccountDetailCxCById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxC

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    Function ListCrossingAccountDetailCxCByCrossingAccountId(ByVal crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxC)

End Interface