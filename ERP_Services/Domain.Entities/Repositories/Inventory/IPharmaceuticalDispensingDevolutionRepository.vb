'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects

Public Interface IPharmaceuticalDispensingDevolutionRepository
    Inherits IRepository(Of PharmaceuticalDispensingDevolution)
    Inherits IRepositoryRollbackStrategy

    Function ListPharmaceuticalDispensingDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of PharmaceuticalDispensingDevolution)

    ''' <summary>
    ''' obtiene una devolucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingDevolutionById(id As Integer) As PharmaceuticalDispensingDevolution
    ''' <summary>
    ''' obtiene una devolucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingDevolutionByCode(code As String) As PharmaceuticalDispensingDevolution


    ''' <summary>
    ''' Metodo para crear devolucion de dispensacion
    ''' </summary>
    ''' <param name="ServiceOrderXml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GeneratePharmaceuticalDevolutionSP(PharmaceuticalDispensingDevolutionXml As String, AnnulationXml As String, UserCode As String) As ObjectResult(Of SP_GeneratePharmaceuticalDevolution_Result)

    Function GetPharmaceuticalDispensingDevolutionWithOutConfirmByAdmission(admissionNumber As String) As String

End Interface
