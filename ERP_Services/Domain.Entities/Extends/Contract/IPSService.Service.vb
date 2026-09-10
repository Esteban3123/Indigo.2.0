Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

Partial Public Class IPSService

    <DataMember>
    Property CodeNameSurgicalGroup As String

    <DataMember()>
    Property AssociatedMaterialIPSServiceDescription As String

    <DataMember()>
    Property CodeNameBillingConcept As String

    ''' <summary>
    ''' Obtiene la lista de procedimientos quirurgicos eliminados
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property listSurgicalProcedureServiceDelete As List(Of SurgicalProcedureService)

    ''' <summary>
    ''' propiedad que obtiene la clase de servicio del servicio IPS
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ServiceClassName As String
        Get
            Return _dictionaryServiceClass(Me.ServiceClass)
        End Get
    End Property

    ''' <summary>
    ''' diccionario de las clases de servicio
    ''' </summary>
    Private _dictionaryServiceClass As Dictionary(Of EClassService, String) = New Dictionary(Of EClassService, String) From
                                                                            {{EClassService.None, ResourceManager.GetString("ServiceClass1", "Contract")},
                                                                            {EClassService.Surgeon, ResourceManager.GetString("ServiceClass2", "Contract")},
                                                                            {EClassService.Anesthesiologist, ResourceManager.GetString("ServiceClass3", "Contract")},
                                                                            {EClassService.Assistant, ResourceManager.GetString("ServiceClass4", "Contract")},
                                                                            {EClassService.RightRoom, ResourceManager.GetString("ServiceClass5", "Contract")},
                                                                            {EClassService.SutureMaterials, ResourceManager.GetString("ServiceClass6", "Contract")},
                                                                            {EClassService.SurgicalInstrumentation, ResourceManager.GetString("ServiceClass7", "Contract")}}

End Class
