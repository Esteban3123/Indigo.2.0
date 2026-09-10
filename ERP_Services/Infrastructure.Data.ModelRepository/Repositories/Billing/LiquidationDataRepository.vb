Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class LiquidationDataRepository
    Inherits GenericRepository(Of LiquidationData)
    Implements ILiquidationDataRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' obtiene los datos de liquidacion de la aseguradora por numero de ingreso
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <returns></returns>
    Function GetLiquidationDataByAdmissionNumber(_admissionNumber As String) As LiquidationData Implements ILiquidationDataRepository.GetLiquidationDataByAdmissionNumber


        Dim LiquidationData = (From e In _context.LiquidationData.Include("LiquidationDataDetail")
                               Where e.AdmissionNumber = _admissionNumber
                               Select e).FirstOrDefault()

        If LiquidationData IsNot Nothing Then
            Return LiquidationData
        Else
            Return New LiquidationData()
        End If
    End Function

#End Region

End Class
