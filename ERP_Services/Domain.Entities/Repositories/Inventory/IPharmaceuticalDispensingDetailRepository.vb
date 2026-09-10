'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPharmaceuticalDispensingDetailRepository
    Inherits IRepository(Of PharmaceuticalDispensingDetail)

    Function GetPharmaceuticalDispensingDetailById(id As Integer) As PharmaceuticalDispensingDetail

    Function GetPharmaceuticalDispensingDetailAndHeadById(PharmaceuticalDispensingDetailId As Integer, Optional tracking As Boolean = True) As PharmaceuticalDispensingDetail

    Function ListPharmaceuticalDispensingDetailsByIds(ByVal dispensingId As Int32, ByVal ids As List(Of Int32), Optional ByVal tracking As Boolean = True) As List(Of PharmaceuticalDispensingDetail)
End Interface
