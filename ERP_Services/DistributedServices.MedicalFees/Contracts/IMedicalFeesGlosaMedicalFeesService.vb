#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IGlosaMedicalFeesService

    ''' <summary>
    ''' Obtiene todos los Honoriario medicos glosados
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllGlosaMedicalFees(audit As AuditMessage) As List(Of GlosaMedicalFees)

    ''' <summary>
    ''' Obtiene un honoriario medico glosado por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGlosaMedicalFeesByCode(Id As String) As GlosaMedicalFees

    ''' <summary>
    ''' Obtiene una cuenta por pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableById(Id As Integer) As AccountPayable

    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFees"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGlosaMedicalFees(GlosaMedicalFees As Domain.Entities.GlosaMedicalFees, idSequense As Int64, audit As AuditMessage) As ActionResult(Of GlosaMedicalFees)

End Interface