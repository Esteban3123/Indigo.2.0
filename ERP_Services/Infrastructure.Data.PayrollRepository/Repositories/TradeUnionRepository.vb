'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

#End Region

Public Class TradeUnionRepository
    Inherits GenericRepository(Of TradeUnion)
    Implements ITradeUnionRepository

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene una unidad de paquete por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionByCode(code As String, tracking As Boolean) As TradeUnion Implements ITradeUnionRepository.GetTradeUnionByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New TradeUnion()
        If tracking Then
            res = (From d As TradeUnion In Me._context.TradeUnion
                  Where d.Code.Equals(code.Trim())
                  Select d).FirstOrDefault
            If res IsNot Nothing Then
                Dim concept = (From c As Concept In Me._context.Concept.AsNoTracking() Where c.Id = res.PayrollConceptId Select c).FirstOrDefault
                Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.IdThirdParty Select c).FirstOrDefault
                res.DescriptionConcepPayroll = concept.Code + " - " + concept.Name

                If ThirdParty IsNot Nothing Then
                    res.DescriptionThirdParty = ThirdParty.Nit + " - " + ThirdParty.Name
                End If


            End If
        Else
            res = (From g In _context.TradeUnion.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault
            If res IsNot Nothing Then
                Dim concept = (From c As Concept In Me._context.Concept.AsNoTracking() Where c.Id = res.PayrollConceptId Select c).FirstOrDefault
                Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.IdThirdParty Select c).FirstOrDefault
                res.DescriptionConcepPayroll = concept.Code + " - " + concept.Name

                If ThirdParty IsNot Nothing Then
                    res.DescriptionThirdParty = ThirdParty.Nit + " - " + ThirdParty.Name
                End If

            End If
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtiene una unidad de paquete por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionById(id As Integer, tracking As Boolean) As TradeUnion Implements ITradeUnionRepository.GetTradeUnionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As New TradeUnion()
        If tracking Then
            res = (From d In Me._context.TradeUnion Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d As TradeUnion In Me._context.TradeUnion.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un Sindicato por el Id del Concepto
    ''' </summary>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <param name="tracking"></param>
    ''' <returns>TradeUnion</returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionByConceptId(ConceptId As Integer, tracking As Boolean) As TradeUnion Implements ITradeUnionRepository.GetTradeUnionByConceptId
        If ConceptId = 0 Then
            Throw New ArgumentNullException("ConceptId")
        End If
        Dim res As New TradeUnion()
        If tracking Then
            res = (From d In Me._context.TradeUnion.Include("ThirdParty") Where d.PayrollConceptId = ConceptId Select d).FirstOrDefault
        Else
            res = (From d As TradeUnion In Me._context.TradeUnion.AsNoTracking() Where d.PayrollConceptId = ConceptId Select d).SingleOrDefault()
        End If
        Return res
    End Function

End Class
