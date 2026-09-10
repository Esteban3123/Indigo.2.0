'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPharmaceuticalDispensingRepository
    Inherits IRepository(Of PharmaceuticalDispensing)
    Inherits IRepositoryRollbackStrategy

    Function GeneratePharmaceuticalDispensingSP(PharmaceuticalDispensingXml As String, AnnulationXml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_GeneratePharmaceuticalDispensing_Result)

    Function ListPharmaceuticalDispensingMassiveConfirm(listDocuments As List(Of String)) As List(Of PharmaceuticalDispensing)

    ''' <summary>
    ''' Obtiene una dispensacion farmaceutica por codigo
    ''' </summary>
    Function GetPharmaceuticalDispensing(ByVal code As String) As PharmaceuticalDispensing

    ''' <summary>
    ''' Obtiene una dispensacion farmaceutica por id
    ''' </summary>
    Function GetPharmaceuticalDispensingById(id As Integer) As PharmaceuticalDispensing

    Function GetPharmaceuticalDispensingWithOutConfirmByAdmission(admissionNumber As String) As String
    Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As List(Of SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm_Result)

    ''' <summary>
    ''' Obtiene el listado de medicamentos mediante una formula medica
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListHCPRESCRDByCODCONCEC(CODCONCEC As String) As List(Of SP_ListHCPRESCRDByCODCONCEC_Result)

    ''' <summary>
    ''' Sp que se encarga de ejecutar el proceso de dispensación y guardar la formula medica
    ''' </summary>
    ''' <returns></returns>
    Function SP_SaveDispensingByPatientMedilaser(XmlPharmaceutical As String, XmlAnnulateDashboard As String, XmlPrescription As String, CodeUser As String, OperatingUnitId As Integer) As SP_SaveDispensingByPatientMedilaser_Result

End Interface