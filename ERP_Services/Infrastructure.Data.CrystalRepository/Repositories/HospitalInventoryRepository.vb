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

Public Class HospitalInventoryRepository
    Inherits GenericRepository(Of IHLISTPRO)
    Implements IHospitalInventoryRepository


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
    ''' obtiene un inventario hospitalario por codigo del producto
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function GetHospitalInventoryByProductCode(productCode As String) As IHLISTPRO Implements IHospitalInventoryRepository.GetHospitalInventoryByProductCode
        Return (From hi In _crystalContext.IHLISTPRO Where hi.CODPRODUC = productCode Select hi).FirstOrDefault()
    End Function
End Class
