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

Public Interface ICrossingAccountDetailOtherConceptsRepository
    Inherits IRepository(Of CrossingAccountDetailOtherConcept)

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetCrossingAccountDetailOtherConceptById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailOtherConcept

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    ''' <param name="crossingAccountId">The crossing account identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function ListCrossingAccountDetailOtherConceptByCrossingAccountId(ByVal crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailOtherConcept)

End Interface
