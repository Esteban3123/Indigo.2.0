#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingControlOutPatientServices

#Region "Methods"

    ''' <summary>
    ''' Genera los documentos necesarios
    ''' </summary>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function GenerateDocuments(controlOutPatientServices As ControlOutPatientServices, companyNit As String, audit As AuditMessage) As ActionResult(Of String)

    <OperationContract()> _
    Function GetProductsByActmedicaAndCups(listActmedicaCups As List(Of Domain.Crystal.Entities.ACTMEDCUPS), IPFECNACI As Date) As ActionResult(Of List(Of ProductCita))

    <OperationContract()> _
    Function GenerateServiceOrderProducts(params As String, audit As AuditMessage) As ActionResult(Of String)

#End Region

End Interface