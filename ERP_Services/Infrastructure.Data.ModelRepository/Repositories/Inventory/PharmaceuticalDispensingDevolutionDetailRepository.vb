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

Public Class PharmaceuticalDispensingDevolutionDetailRepository
    Inherits GenericRepository(Of PharmaceuticalDispensingDevolutionDetail)
    Implements IPharmaceuticalDispensingDevolutionDetailRepository


    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub



    ''' <summary>
    ''' lista los detalles de la devolucc
    ''' </summary>
    ''' <param name="idPharmaceuticalDispensingDevolutionDetail"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(idPharmaceuticalDispensingDevolutionDetail As Integer) As List(Of PharmaceuticalDispensingDevolutionDetail) Implements IPharmaceuticalDispensingDevolutionDetailRepository.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution
        Dim res = (From pddd In _context.PharmaceuticalDispensingDevolutionDetail Where pddd.PharmaceuticalDispensingDevolutionId = idPharmaceuticalDispensingDevolutionDetail Select pddd).ToList()
        For Each itemDetail In res
            Dim pharmaceuticalDispensingDetailBatchSerial = (From pddbs In _context.PharmaceuticalDispensingDetailBatchSerial.AsNoTracking() Where pddbs.Id = itemDetail.PharmaceuticalDispensingDetailBatchSerialId Select pddbs).FirstOrDefault()
            Dim pharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking() Where pdd.Id = pharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = pharmaceuticalDispensingDetail.ProductId Select p).FirstOrDefault()
            Dim pharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where pd.Id = pharmaceuticalDispensingDetail.PharmaceuticalDispensingId Select pd).FirstOrDefault()

            itemDetail.CodePharmaceuticalDispensing = pharmaceuticalDispensing.Code
            itemDetail.ProductId = product.Id
            itemDetail.CodeNameProduct = String.Concat(product.Code, " - ", product.Name)
            itemDetail.PharmaceuticalDispensingDetailId = pharmaceuticalDispensingDetail.Id
        Next
        Return res
    End Function
End Class
