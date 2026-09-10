'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-02-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IConsignmentCostListRepository
    Inherits IRepository(Of ConsignmentCostList)

    ''' <summary>
    ''' Obtiene un registro por proveedor
    ''' </summary>
    Function GetConsignmentCostListBySupplierId(SupplierId As Integer, OperatingUnitId As Integer) As ConsignmentCostList

    ''' <summary>
    '''  Obtiene un registro por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetConsignmentCostListById(id As Integer) As ConsignmentCostList

    ''' <summary>
    '''  Elimina detalle 
    ''' </summary>
    ''' <returns></returns>
    Function DeleteDetail(detail As ConsignmentCostListDetail) As ConsignmentCostList

    ''' <summary>
    ''' Obtiene un registro detalle por Id
    ''' </summary>
    Function GetDetailById(id As Integer) As ConsignmentCostListDetail


    ''' <summary>
    ''' Copiar y pegar del excel 
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetConsignmentCostListDetailFromFile(EntityXml As String) As List(Of SP_CopyAndPasteConsignmentCostListDetail_Result)

    ''' <summary>
    ''' Obtiene el resultado del  comprobante contable 
    ''' </summary>
    ''' <param name="XMLConsignmentCostList"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveJournalVoucherConsignmentCostList(XMLConsignmentCostList As String, CodeUser As String) As SP_GenerateJournalVoucherByConsignmentCostListDetail_Result
End Interface
