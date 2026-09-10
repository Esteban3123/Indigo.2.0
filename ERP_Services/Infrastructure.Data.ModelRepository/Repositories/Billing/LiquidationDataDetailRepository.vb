Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class LiquidationDataDetailRepository
    Inherits GenericRepository(Of LiquidationDataDetail)
    Implements ILiquidationDataDetailRepository

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
    Public Function GetLiquidationDataDetailByLiquidationDataId(_liquidationDataId As List(Of Integer)) As List(Of LiquidationDataDetail) Implements ILiquidationDataDetailRepository.GetLiquidationDataDetailByLiquidationDataId

        If _liquidationDataId Is Nothing Then
            Throw New ArgumentNullException("listIds")
        End If

        Dim res = (From e In _context.LiquidationDataDetail
                   Where (_liquidationDataId.Contains(e.Id))
                   Select e).ToList

        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of LiquidationDataDetail)()
        End If
    End Function

#End Region

End Class
