'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IADCONCOEXrepository
    Inherits IRepository(Of ADCONCOEX)

    Function GetADCONCOEXByConsecutive(consecutive As Decimal) As ADCONCOEX

    Function GetADCONCOEXByAdmissionAndIPCODPACIAndESTADO(admissionNumber As String, ipcodpaci As String, estado As Integer) As ADCONCOEX

End Interface