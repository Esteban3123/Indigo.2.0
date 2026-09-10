'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 23/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View

#End Region

Public Class MAccountControl
    Implements IDisposable


#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function GenerateServiceOrderMassive(args As Object, homologations As List(Of List(Of CupsHomologation))) As Task(Of ActionResult(Of List(Of List(Of CupsHomologation))))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateServiceOrderMassiveAsync(parameter, homologations, Indigo.AuditMessageWcf)
    End Function

    Public Async Function GenerateServiceOrderMassiveWithListDetail(args As Object, listServiceOrderDetail As List(Of ServiceOrderDetail)) As Task(Of ActionResult)
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateServiceOrderMassiveWithListDetailAsync(parameter, listServiceOrderDetail, Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetServiceOrderDetailHomologation(careGroupId As Integer, listHomologations As List(Of List(Of Domain.Entities.CupsHomologation)), args As Object) As Task(Of ActionResult(Of Domain.Entities.ServiceOrder))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderDetailHomologationAsync(careGroupId, listHomologations, parameter)
    End Function

    Public Async Function GetHCREGEGREByAdmissionCode(admissionCode As String) As Task(Of Domain.Crystal.Entities.HCREGEGRE)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetHCREGEGREByAdmissionCodeAsync(admissionCode)
    End Function

    ''' <summary>
    ''' Consume servicio para traer el valor de un paquete en base a sus productos
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="PackageIds"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="ListviewPharmaDoseMixingStations"></param>
    ''' <returns></returns>
    Public Async Function GetPackageValuePerProduct(CareGroupId As Integer, PackageIds As List(Of Integer), ServiceDate As DateTime, ListviewPharmaDoseMixingStations As List(Of ViewPharmaDoseMixingStation)) As Task(Of ActionResult(Of List(Of ProductRateDetailPackage)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPackageValuePerProductAsync(CareGroupId, PackageIds, ServiceDate, ListviewPharmaDoseMixingStations)
    End Function

    ''' <summary>
    ''' Obtiene un ingresos plano por su numero
    ''' </summary>
    ''' <param name="code">Número del ingreso</param>
    ''' <returns>Ingreso plano</returns>
    Public Async Function GetAdmissionPOCOByCodeAsync(ByVal code As String) As Task(Of ActionResult(Of Object))
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetAdmissionPOCOByAdmissionCodeAsync(code)
        If res.StateResult Then
            If res IsNot Nothing AndAlso Not res.ObjectEmbbeded.Trim().Equals(String.Empty) Then
                Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = Utils.DeserializeJsonToObject(res.ObjectEmbbeded)}
            Else
                Return New ActionResult(Of Object) With {.StateResult = False, .Message = "No se encontraron datos para la admisión"}
            End If
        Else
            Return New ActionResult(Of Object) With {.StateResult = res.StateResult, .Message = res.Message}
        End If
    End Function

    Function ListInvoiceByStatus(status As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListInvoiceByStatus(status)
    End Function

    Function ListInvoiceByStatusAndPatientCode(status As Integer, PatientCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListInvoiceByStatusAndPatientCode(status, PatientCode)
    End Function

    Public Function ListKardexMedicineSupplierByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewKardexMedicineSupplier)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListKardexMedicineSupplierByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function listManualMovementsAsync(patientCode As String, admissionNumber As String) As XPCollection(Of ViewManualMovementsXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).InventoryService.ListManualMovements(patientCode, admissionNumber)
    End Function

    Public Async Function GetProcessedMedicationItemsForBillingListAsync(admissionCode As String, Optional isChild As Boolean = False) As Task(Of List(Of Domain.Entities.SP_GetProcessedMedicationItemsForBilling_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetProcessedMedicationItemsForBillingListAsync(admissionCode, isChild)
    End Function

    Public Function GetBedRatebyCodeAsync(code As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAllPatients()
    End Function

    Public Function ListAdmissionsToLiquidation() As Object
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAdmissionsToLiquidation()
    End Function

    Public Function ListMedicineSupplierAggregatesByPatientCodeIngreso(patientCode As String, admissionNumber As String, Optional ItemProductionClass As Boolean = False) As XPCollection(Of ViewMedicinesSupplies)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListMedicineSupplierAggregatesByPatientCodeIngreso(patientCode, admissionNumber, ItemProductionClass)
    End Function

    Public Function ListLaboratoriesByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewLaboratories)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListLaboratories(patientCode, admissionNumber)
    End Function

    Public Function ListProceduresQxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresQx)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListProceduresQxByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function ListServicesProceduresLaboratoriesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresLaboratories)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresLaboratoriesByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresPathologies)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresImagesDx)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function ListServicesProceduresQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresQx)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Function ListServicesProceduresReportQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresReportQx)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresReportQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresNoQx)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresConsultation)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresTherapy)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListServicesHemocomponentByadmissionIngreso(admissionNumber As String) As XPCollection(Of ViewServicesHemocomponent)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesHemocomponentByadmissionIngreso(admissionNumber)
    End Function

    Public Function ListStaysByadmissionIngresoAsync(admissionNumber As String) As Task(Of ActionResult(Of List(Of StayInfoModel)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListOfStaysByAdmissionToModelAsync(admissionNumber, Indigo.AuditMessageWcf)
    End Function

    Function ListOxygenConsumptionByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewOxygenConsumption)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListOxygenConsumptionByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProceduresDetail)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
    End Function

    Function ListServicesNursingProceduresByadmissionCodeIngresoAtentionCenter(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListServicesNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
    End Function

    Function ListReviewsByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewReviews)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListReviewsByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListQxRealizadosByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgeriesPerformed)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListQxRealizadosByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListQxEquipeByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgicalEquipment)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListQxEquipeByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function ListQxInformByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgicalReport)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListQxInformByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    Function GetHCORHEMBOLByID(_HCORHEMBOLID As Integer) As HCORHEMBOLXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetHCORHEMBOLByID(_HCORHEMBOLID)
    End Function

    Public Function GetHCORHEMBOLByCode(_CODPROSAL As String) As HealthCareProfessionalXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetHCORHEMBOLByCode(_CODPROSAL)
    End Function

    ''' <summary>
    ''' Se obtiene el grupo de atención por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCareGroupById(careGroupId As Integer) As Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo
        Dim filter As String = "Id = " & careGroupId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los cups que necesiten una cotización
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUPSWithQuoted(args As Object, careGroupId As Integer) As Tuple(Of String, List(Of Integer))
        Dim messageReturn As String = String.Empty
        Dim listCupsIdsReturn As List(Of Integer) = Nothing
        Dim listProcedures As New List(Of Infrastructure.Data.Xpo.ContractRepository.ProcedureCupsXpo)

        Dim filterCareGroup = "Id = " & careGroupId
        Dim careGroup = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo)(Nothing, filterCareGroup).FirstOrDefault()

        For Each x In args.Details
            Dim filterProcedures As String = "Quoted = 1 and CupsId.Code = '" & x.CupsEntityCode.ToString().Trim() & "' and ProceduresTemplateId = " & careGroup.ProcedureTemplateId.Id

            Try
                Dim contractDescriptionId = x.ContractDescriptionId
                If contractDescriptionId IsNot Nothing Then
                    filterProcedures = String.Format("{0} AND ContractDescriptionId.Id = {1}", filterProcedures, contractDescriptionId)
                End If
            Catch ex As Exception
                Console.WriteLine("propiedad no existe")
            End Try

            Dim procedure = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ProcedureCupsXpo)(Nothing, filterProcedures).FirstOrDefault()
            If procedure IsNot Nothing Then
                listProcedures.Add(procedure)
            End If
        Next

        If listProcedures IsNot Nothing AndAlso listProcedures.Count > 0 Then
            messageReturn = "Los servicios " + String.Join(",", (From x In listProcedures Select x.CupsId.Code + " - " + x.CupsId.Description).ToArray()) + " requieren de una cotización, desea agregar una cotización?"
            listCupsIdsReturn = (From x In listProcedures Select x.CupsId.Id).ToList()
        End If

        Return New Tuple(Of String, List(Of Integer))(messageReturn, listCupsIdsReturn)
    End Function

    ''' <summary>
    ''' funcion para validar si el numero de ingreso tiene procesos pendientes en el control de cuentas hospitalario
    ''' </summary>
    ''' <param name="AdmissionCode"></param>
    ''' <returns></returns>
    Public Function ListAccountControlValidations(AdmissionCode As String) As List(Of ViewAdmissionLock_AccountControlValidations)
        Dim Filter As String
        If AdmissionCode IsNot Nothing Then
            Filter = "NUMINGRES = '" & AdmissionCode & "'"
        Else
            Filter = ""
        End If
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewAdmissionLock_AccountControlValidations)(Nothing, Filter)
    End Function

    ''' <summary>
    ''' funcion para guardar en la tabla de accountcontroljustification las justificaciones por detalle
    ''' </summary>
    ''' <param name="_listAccountControlJustification"></param>
    ''' <returns></returns>
    Public Async Function SaveAccountControlJustification(_listAccountControlJustification As List(Of AccountControlJustification)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveAccountControlJustificationAsync(_listAccountControlJustification, Indigo.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class