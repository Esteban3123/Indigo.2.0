'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Jhossept K. Garay
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region
<ServiceContract()> _
Public Interface ICrystalServiceHealthCareProfessional
    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetProfessionalByCode(Code As String) As ActionResult(Of INPROFSAL)

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSpecialityByCode(Code As String) As ActionResult(Of INESPECIA)

    ''' <summary>
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="professional">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveProfessional(professional As INPROFSAL, ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                              ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract), audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.INPROFSAL)

    ''' <summary>
    ''' Elimina profesionales
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteProfessional(Code As String) As ActionResult

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateStatusProfessional(Code As String, Status As Boolean) As ActionResult(Of INPROFSAL)

    ''' <summary>
    ''' Obtiene el listado de detalles de contratos que tiene asociado el médico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of Domain.Entities.HealthProfessionalContract))

    ''' <summary>
    ''' Obtener Usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetUserHIS(Code As String) As ActionResult(Of SEGusuaru)

    ''' <summary>
    ''' Obtener el profesional ext o int por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetHealthProfessionalByCode(Code As String) As ActionResult(Of HealthProfessionalModel)

    ''' <summary>
    ''' Guardar el profesional ext o int
    ''' </summary>
    ''' <param name="professional"></param>
    ''' <param name="ListHealthProfessionalContract"></param>
    ''' <param name="ListDeleteHealthProfessionalContract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveHealthProfessional(professional As HealthProfessionalModel,
                                            ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            audit As AuditMessage) As ActionResult(Of HealthProfessionalModel)
    ''' <summary>
    ''' Actualizar el estado idnt o ext
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStatusProfessionalHealth(Code As String, Status As Boolean) As ActionResult(Of HealthProfessionalModel)

End Interface

