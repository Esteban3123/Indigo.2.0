'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IPhysicalInventoryCrystalRepository
    Inherits IRepository(Of HCFISIPRO)
    ''' <summary>
    ''' obtiene un item del inventario fisico de crystal
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="careCenterCode"></param>
    ''' <param name="functionalUnitCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventory(patientCode As String, admissionNumber As String, careCenterCode As String, functionalUnitCode As String, productCode As String) As HCFISIPRO
End Interface
