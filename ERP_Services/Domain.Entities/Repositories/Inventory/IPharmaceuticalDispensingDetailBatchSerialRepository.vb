'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities


Public Interface IPharmaceuticalDispensingDetailBatchSerialRepository
    Inherits IRepository(Of PharmaceuticalDispensingDetailBatchSerial)

    ''' <summary>
    ''' obtiene los detalle para hacer la devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber As String, productCode As String, batchCode As String, userId As Integer) As Base.Entities.ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial))

    ''' <summary>
    ''' obtiene los productos para la devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="productType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber As String, functionalUnitCode As String, productCode As String, productType As Integer, userId As Integer, Optional batchSerialCode As String = "") As List(Of PharmaceuticalDispensingDetailBatchSerial)


    ''' <summary>
    ''' lista los detalle de las dispensacion para hacer devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber As String) As List(Of PharmaceuticalDispensingDetailBatchSerial)

    ''' <summary>
    ''' lista un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingDetailBatchSerialById(id As Integer) As PharmaceuticalDispensingDetailBatchSerial

    Function GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(pharmaceuticalDispensingId As Integer) As List(Of PharmaceuticalDispensingDetailBatchSerial)
End Interface
