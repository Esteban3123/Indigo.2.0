'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IPharmacyRepository
    Inherits IRepository(Of HCFARMEPC)

    Function GetPharmacyByConsecutiveWithDetail(consecutive As Decimal) As HCFARMEPC

    Function GetPatientEgress(admissionNumber As String) As Boolean

    ''' <summary>
    ''' obtiene la cabecera de farmacia por codigo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmacyByConsecutive(consecutive As Decimal) As HCFARMEPC

    ''' <summary>
    ''' Obtener una solicitud de paquete QX por consecutivo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SurgicalPackageByConsecutive(consecutive As Decimal) As ViewDashBoardPharmacy_SurgicalPackage

End Interface
