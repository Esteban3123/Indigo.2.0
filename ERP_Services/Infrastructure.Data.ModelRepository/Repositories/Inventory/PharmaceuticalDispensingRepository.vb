'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

Public Class PharmaceuticalDispensingRepository
    Inherits GenericRepository(Of PharmaceuticalDispensing)
    Implements IPharmaceuticalDispensingRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPharmaceuticalDispensingMassiveConfirm(listDocuments As List(Of String)) As List(Of PharmaceuticalDispensing) Implements IPharmaceuticalDispensingRepository.ListPharmaceuticalDispensingMassiveConfirm
        Return (From re In _context.PharmaceuticalDispensing.Include("PharmaceuticalDispensingDetail").Include("PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial") Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    ''' <summary>
    ''' Obtiene una dispensacion farmaceutica por codigo
    ''' </summary>
    Public Function GetPharmaceuticalDispensing(code As String) As PharmaceuticalDispensing Implements IPharmaceuticalDispensingRepository.GetPharmaceuticalDispensing
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.PharmaceuticalDispensing.Include("PharmaceuticalDispensingDetail").Include("PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial").Include("PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory").Include("PharmaceuticalDispensingDetail.ProductServiceDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.PharmaceuticalDispensing.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()

            For Each item As PharmaceuticalDispensingDetail In query.PharmaceuticalDispensingDetail
                Dim prod As InventoryProduct = (From p In _context.InventoryProduct Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.CodeProduct = prod.Code
                item.NameProduct = String.Concat(prod.Code, " - ", prod.Name)
                item.CodeNameCareGroup = (From c In _context.CareGroup Where c.Id = item.CareGroupId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                item.CodeNameWareHouse = (From w In _context.Warehouse Where w.Id = item.WarehouseId Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()
                item.FullNameFunctionalUnit = (From u In _context.FunctionalUnit Where u.Id = item.FunctionalUnitId Select String.Concat(u.Code, " - ", u.Name)).FirstOrDefault()
                item.CodeNameCups = If(item.CupsEntityId IsNot Nothing, (From c In _context.CUPSEntity Where c.Id = item.CupsEntityId Select String.Concat(c.Code, " - ", c.Description)).FirstOrDefault(), String.Empty)
                item.Custody = (From w In _context.Warehouse Where w.Id = item.WarehouseId Select w.CustodyStore).FirstOrDefault()
            Next

            Return query
        Else
            Return New PharmaceuticalDispensing()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una dispensacion farmaceutica por id
    ''' </summary>
    Public Function GetPharmaceuticalDispensingById(id As Integer) As PharmaceuticalDispensing Implements IPharmaceuticalDispensingRepository.GetPharmaceuticalDispensingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.PharmaceuticalDispensing.Include("PharmaceuticalDispensingDetail") Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.PharmaceuticalDispensing.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New PharmaceuticalDispensing()
        End If
    End Function


    Public Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As List(Of SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm_Result) Implements IPharmaceuticalDispensingRepository.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm
        If String.IsNullOrEmpty(admissionNumber) Then
            Throw New ArgumentNullException("admissionNumber")
        End If
        Return _context.SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber).ToList()
    End Function

    Public Function GetPharmaceuticalDispensingWithOutConfirmByAdmission(admissionNumber As String) As String Implements IPharmaceuticalDispensingRepository.GetPharmaceuticalDispensingWithOutConfirmByAdmission
        If String.IsNullOrEmpty(admissionNumber) Then
            Throw New ArgumentNullException("admissionNumber")
        End If
        Dim query = (From i In _context.PharmaceuticalDispensing.AsNoTracking() Where i.AdmissionNumber.Equals(admissionNumber) AndAlso i.Status = 1 Select i).ToList()
        If query.Count > 0 Then
            Return String.Join("-", query.Select(Function(o) o.Code).ToArray)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Metodo para crear la dispensacion
    ''' </summary>
    Public Function GeneratePharmaceuticalDispensingSP(PharmaceuticalDispensingXml As String, AnnulationXml As String, UserCode As String) As List(Of SP_GeneratePharmaceuticalDispensing_Result) Implements IPharmaceuticalDispensingRepository.GeneratePharmaceuticalDispensingSP
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GeneratePharmaceuticalDispensing(PharmaceuticalDispensingXml, AnnulationXml, UserCode, "").ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de medicamentos mediante una formula medica
    ''' </summary>
    ''' <param name="CODCONCEC"></param>
    ''' <returns></returns>
    Public Function SP_ListHCPRESCRDByCODCONCEC(CODCONCEC As String) As List(Of SP_ListHCPRESCRDByCODCONCEC_Result) Implements IPharmaceuticalDispensingRepository.SP_ListHCPRESCRDByCODCONCEC
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ListHCPRESCRDByCODCONCEC(CODCONCEC).ToList()
    End Function

    ''' <summary>
    ''' Sp que se encarga de realizar el proceso de dispensación y guardar en las tablas de formula medica
    ''' </summary>
    ''' <param name="XmlPharmaceutical"></param>
    ''' <param name="XmlAnnulateDashboard"></param>
    ''' <param name="XmlPrescription"></param>
    ''' <param name="CodeUser"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Public Function SP_SaveDispensingByPatientMedilaser(XmlPharmaceutical As String, XmlAnnulateDashboard As String, XmlPrescription As String, CodeUser As String, OperatingUnitId As Integer) As SP_SaveDispensingByPatientMedilaser_Result Implements IPharmaceuticalDispensingRepository.SP_SaveDispensingByPatientMedilaser
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveDispensingByPatientMedilaser(XmlPharmaceutical, XmlAnnulateDashboard, XmlPrescription, CodeUser, OperatingUnitId).SingleOrDefault()
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "

            DELETE FROM Inventory.PharmaceuticalDispensingDetailBatchSerial
            WHERE EXISTS (
                SELECT 1
                FROM Inventory.PharmaceuticalDispensingDetail pdd
                INNER JOIN Inventory.PharmaceuticalDispensing pd ON pdd.PharmaceuticalDispensingId = pd.Id
                WHERE Inventory.PharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId = pdd.Id
                  AND pd.Code = @Code
	              AND pd.Status = 1
            );

            DELETE FROM Inventory.PharmaceuticalDispensingDetail
            WHERE EXISTS (
                SELECT 1
                FROM Inventory.PharmaceuticalDispensing pd
                WHERE PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id
                  AND pd.Code = @Code
	              AND pd.Status = 1
            );

            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

            DELETE FROM Inventory.PharmaceuticalDispensing
            WHERE Code = @Code
            AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class