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
Imports System.Data.SqlClient

Public Class PharmaceuticalDispensingDevolutionRepository
    Inherits GenericRepository(Of PharmaceuticalDispensingDevolution)
    Implements IPharmaceuticalDispensingDevolutionRepository

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
    Public Function ListPharmaceuticalDispensingDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of PharmaceuticalDispensingDevolution) Implements IPharmaceuticalDispensingDevolutionRepository.ListPharmaceuticalDispensingDevolutionMassiveConfirm
        Return (From re In _context.PharmaceuticalDispensingDevolution.Include("PharmaceuticalDispensingDevolutionDetail") Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function
    ''' <summary>
    ''' obtiene una devolucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalDispensingDevolutionByCode(code As String) As PharmaceuticalDispensingDevolution Implements IPharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionByCode
        Dim res = (From pdd In _context.PharmaceuticalDispensingDevolution Where pdd.Code = code Select pdd).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From pdd In _context.PharmaceuticalDispensingDevolution.AsNoTracking() Where pdd.Code = code Select pdd).FirstOrDefault
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where res.WarehouseId = wh.Id Select wh).FirstOrDefault()
            res.CodeNameWarehouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            Return res
        Else
            Return New PharmaceuticalDispensingDevolution
        End If
    End Function

    ''' <summary>
    ''' obtiene una devolucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalDispensingDevolutionById(id As Integer) As PharmaceuticalDispensingDevolution Implements IPharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionById
        Dim res = (From pdd In _context.PharmaceuticalDispensingDevolution Where pdd.Id = id Select pdd).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New PharmaceuticalDispensingDevolution
        End If
    End Function

    ''' <summary>
    ''' Metodo para crear devolucion de dispensacion
    ''' </summary>
    ''' <param name="PharmaceuticalDispensingDevolutionXml"></param>
    ''' <param name="AnnulationXml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function GeneratePharmaceuticalDevolutionSP(PharmaceuticalDispensingDevolutionXml As String, AnnulationXml As String, UserCode As String) As ObjectResult(Of SP_GeneratePharmaceuticalDevolution_Result) Implements IPharmaceuticalDispensingDevolutionRepository.GeneratePharmaceuticalDevolutionSP
        Return _context.SP_GeneratePharmaceuticalDevolution(PharmaceuticalDispensingDevolutionXml, AnnulationXml, UserCode)
    End Function

    Public Function GetPharmaceuticalDispensingDevolutionWithOutConfirmByAdmission(admissionNumber As String) As String Implements IPharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionWithOutConfirmByAdmission
        If String.IsNullOrEmpty(admissionNumber) Then
            Throw New ArgumentNullException("admissionNumber")
        End If
        Dim query = (From i In _context.PharmaceuticalDispensingDevolution.AsNoTracking() Where i.AdmissionNumber.Equals(admissionNumber) AndAlso i.Status = 1 Select i).ToList()
        If query.Count > 0 Then
            Return String.Join("-", query.Select(Function(o) o.Code).ToArray)
        Else
            Return Nothing
        End If
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql =
            "DELETE FROM Inventory.PharmaceuticalDispensingDevolutionDetail
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.PharmaceuticalDispensingDevolution pdd
                    WHERE pdd.Id = PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDevolutionId
                      AND pdd.Code = @Code
                      AND pdd.Status = 1
                );

            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

            DELETE FROM Inventory.PharmaceuticalDispensingDevolution
                WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
