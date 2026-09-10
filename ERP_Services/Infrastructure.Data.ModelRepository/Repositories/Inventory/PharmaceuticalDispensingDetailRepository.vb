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
Imports Domain.Base

Public Class PharmaceuticalDispensingDetailRepository
    Inherits GenericRepository(Of PharmaceuticalDispensingDetail)
    Implements IPharmaceuticalDispensingDetailRepository, Inject

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetPharmaceuticalDispensingDetailById(id As Integer) As PharmaceuticalDispensingDetail Implements IPharmaceuticalDispensingDetailRepository.GetPharmaceuticalDispensingDetailById
        Return (From pdd In _context.PharmaceuticalDispensingDetail Where pdd.Id = id Select pdd).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Funcion para cargar un detalle de dispensacion farmaceutica por Id del detalle
    ''' </summary>
    ''' <param name="PharmaceuticalDispensingDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDetailAndHeadById(PharmaceuticalDispensingDetailId As Integer, Optional tracking As Boolean = True) As PharmaceuticalDispensingDetail Implements IPharmaceuticalDispensingDetailRepository.GetPharmaceuticalDispensingDetailAndHeadById
        If tracking Then
            Return (From pdd In _context.PharmaceuticalDispensingDetail.Include("PharmaceuticalDispensing") Where pdd.Id = PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
        Else
            Return (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking().Include("PharmaceuticalDispensing").AsNoTracking() Where pdd.Id = PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
        End If
    End Function

    Public Function ListPharmaceuticalDispensingDetailsByIds(ByVal dispensingId As Int32, ids As List(Of Integer), Optional ByVal tracking As Boolean = True) As List(Of PharmaceuticalDispensingDetail) Implements IPharmaceuticalDispensingDetailRepository.ListPharmaceuticalDispensingDetailsByIds
        If tracking Then
            Return (From pdd In _context.PharmaceuticalDispensingDetail.Include("PharmaceuticalDispensing").Include("PharmaceuticalDispensingDetailBatchSerial") Where pdd.PharmaceuticalDispensingId = dispensingId And Not ids.Contains(pdd.Id) Select pdd).ToList()
        Else
            Return (From pdd In _context.PharmaceuticalDispensingDetail.Include("PharmaceuticalDispensing").AsNoTracking().Include("PharmaceuticalDispensingDetailBatchSerial").AsNoTracking() Where pdd.PharmaceuticalDispensingId = dispensingId And Not ids.Contains(pdd.Id) Select pdd).ToList()
        End If
    End Function
End Class
