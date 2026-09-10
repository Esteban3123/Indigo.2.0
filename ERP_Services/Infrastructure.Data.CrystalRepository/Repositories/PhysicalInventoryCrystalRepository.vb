'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 2015-03-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class PhysicalInventoryCrystalRepository
    Inherits GenericRepository(Of HCFISIPRO)
    Implements IPhysicalInventoryCrystalRepository



    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    
    ''' <summary>
    ''' obtiene un listado del inventario fisico de crystal para hacer la devolucion
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="careCenterCode"></param>
    ''' <param name="functionalUnitCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function GetPhysicalInventory(patientCode As String, admissionNumber As String, careCenterCode As String, functionalUnitCode As String, productCode As String) As HCFISIPRO Implements IPhysicalInventoryCrystalRepository.GetPhysicalInventory
        Return (From pic In _crystalContext.HCFISIPRO Where pic.IPCODPACI = patientCode And pic.NUMINGRES = admissionNumber And pic.CODCENATE = careCenterCode And pic.UFUCODIGO = functionalUnitCode And pic.CODPRODUC = productCode Select pic).FirstOrDefault()
    End Function
End Class
