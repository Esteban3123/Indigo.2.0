'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class INPRODPATRepository
    Inherits GenericRepository(Of INPRODPAT)
    Implements IINPRODPATRepository

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


    Public Function GetINPRODPATByCodeProduct(productCode As String) As List(Of INPRODPAT) Implements IINPRODPATRepository.GetINPRODPATByCodeProduct
        Return (From e In _crystalContext.INPRODPAT.AsNoTracking() Where e.IPRCODIGO = productCode Select e).ToList()
    End Function

    Public Function GetListINPRODPATByCodesINPRODPAT(listCodes As List(Of String)) As List(Of INPRODPAT) Implements IINPRODPATRepository.GetListINPRODPATByCodesINPRODPAT
        Return (From e In _crystalContext.INPRODPAT Where listCodes.Contains(e.CODDIAGNO) Select e).ToList()
    End Function
End Class
