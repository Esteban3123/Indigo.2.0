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

Public Class DevolutionMedicationDetailRepository
    Inherits GenericRepository(Of HCDEVMEDD)
    Implements IDevolutionMedicationDetailRepository


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
    ''' obtieene un detalle de la devolucion por consecutivo y por codigo de producto
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function GetDevolutionMedicationDetailDetailByConsecutiveAndProductCode(consecutive As Integer, productCode As String) As HCDEVMEDD Implements IDevolutionMedicationDetailRepository.GetDevolutionMedicationDetailDetailByConsecutiveAndProductCode
        Dim res = (From pd In _crystalContext.HCDEVMEDD Where pd.CODCONCEC = consecutive And pd.CODPRODUC = productCode Select pd).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New HCDEVMEDD
        End If
    End Function

    ''' <summary>
    ''' lista los detalles de la devolucion por el consecutivo y que tengan cantidad por entregar
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Function ListDevolutionMedicationDetailDetailByConsecutive(consecutive As Integer) As List(Of HCDEVMEDD) Implements IDevolutionMedicationDetailRepository.ListDevolutionMedicationDetailDetailByConsecutive
        Dim quantity = 0
        Dim status = "1"
        Return (From pd In _crystalContext.HCDEVMEDD Where pd.CODCONCEC = consecutive And pd.CANPENDIE > quantity And pd.PROESTADO = status Select pd).ToList()
    End Function
End Class
