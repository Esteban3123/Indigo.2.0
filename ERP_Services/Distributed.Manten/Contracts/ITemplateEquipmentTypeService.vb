
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface ITemplateEquipmentTypeService

    <OperationContract()> _
    Function ListAllTemplateEquipmentType(Empresa As String) As List(Of TemplateEquipmentType)

    <OperationContract()> _
    Function DeleteTemplateEquipmentType(Empresa As String, ByVal TemplateEquipmentType As TemplateEquipmentType, ByVal audit As AuditMessage) As ActionResult

    <OperationContract()> _
    Function SaveTemplateEquipmentType(Empresa As String, ByVal TemplateEquipmentType As TemplateEquipmentType, ByVal audit As AuditMessage) As ActionResult(Of TemplateEquipmentType)

    <OperationContract()> _
    Function GetTemplateEquipmentType(Empresa As String, ByVal codeTemplateEquipmentType As String, ByVal audit As AuditMessage) As TemplateEquipmentType
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStateTemplate(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TemplateEquipmentType)

    <OperationContract()> _
    Function GetTemplateEquipmentTypeEquipmentTypeId(Empresa As String, ByVal IdEquipmentType As Integer, ByVal audit As AuditMessage) As TemplateEquipmentType
End Interface
