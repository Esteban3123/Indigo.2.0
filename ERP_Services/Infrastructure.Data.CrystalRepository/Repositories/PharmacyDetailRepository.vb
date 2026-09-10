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

Public Class PharmacyDetailRepository
    Inherits GenericRepository(Of HCFARMEPD)
    Implements IPharmacyDetailRepository


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

    Public Function GetPharmacyDetailByConsecutiveAndProductCode(consecutive As Decimal, productCode As String) As HCFARMEPD Implements IPharmacyDetailRepository.GetPharmacyDetailByConsecutiveAndProductCode
        Dim res = (From pd In _crystalContext.HCFARMEPD Where pd.CODCONCEC = consecutive And pd.CODPRODUC = productCode Select pd).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New HCFARMEPD
        End If
    End Function

    ''' <summary>
    ''' lista los detalles de farmacia por el consecutivo y que tengan cantidad por entregar
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Function ListPharmacyDetailByConsecutive(consecutive As Decimal) As List(Of HCFARMEPD) Implements IPharmacyDetailRepository.ListPharmacyDetailByConsecutive
        Dim quantity = 0
        Return (From pd In _crystalContext.HCFARMEPD Where pd.CODCONCEC = consecutive And pd.CANPENPRO > quantity Select pd).ToList()
    End Function
End Class
