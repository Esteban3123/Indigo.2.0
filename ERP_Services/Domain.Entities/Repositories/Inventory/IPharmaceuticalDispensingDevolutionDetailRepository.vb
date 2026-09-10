'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPharmaceuticalDispensingDevolutionDetailRepository
    Inherits IRepository(Of PharmaceuticalDispensingDevolutionDetail)

    ''' <summary>
    ''' lista los detalles de la devolucc
    ''' </summary>
    ''' <param name="idPharmaceuticalDispensingDevolutionDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(idPharmaceuticalDispensingDevolutionDetail As Integer) As List(Of PharmaceuticalDispensingDevolutionDetail)

End Interface
