'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán
' Created          : 04-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IControlOutPatientServicesAdminService
    Inherits IDisposable

#Region "Methods"

    Function GenerateDocuments(controlOutPatientServices As ControlOutPatientServices, companyNit As String, audit As AuditMessage) As ActionResult(Of String)

    Function GetProductsByActmedicaAndCups(listActmedicaCups As List(Of ACTMEDCUPS), IPFECNACI As Date) As ActionResult(Of List(Of ProductCita))

    Function GenerateServiceOrderProducts(params As String, audit As AuditMessage) As ActionResult(Of String)

#End Region

End Interface