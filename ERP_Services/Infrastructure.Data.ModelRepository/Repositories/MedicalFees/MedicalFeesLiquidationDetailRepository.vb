'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MedicalFeesLiquidationDetailRepository
    Inherits GenericRepository(Of MedicalFeesLiquidationDetail)
    Implements IMedicalFeesLiquidationDetailRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
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
    ''' Obtiene un listado de detalles de liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListMedicalFeesLiquidationDetailByMedicalFeesCausationId(MedicalFeesCausationId As Integer, Optional tracking As Boolean = True) As List(Of MedicalFeesLiquidationDetail) Implements IMedicalFeesLiquidationDetailRepository.GetListMedicalFeesLiquidationDetailByMedicalFeesCausationId
        If MedicalFeesCausationId = 0 Then
            Throw New ArgumentNullException("MedicalFeesCausationId")
        End If
        Dim ListDetail As List(Of MedicalFeesLiquidationDetail)
        If tracking Then
            ListDetail = (From l In _context.MedicalFeesLiquidationDetail Where l.MedicalFeesCausationId = MedicalFeesCausationId Select l).ToList
        Else
            ListDetail = (From l In _context.MedicalFeesLiquidationDetail.AsNoTracking
                          Join c In _context.MedicalFeesLiquidation.AsNoTracking On l.MedicalFeesLiquidacionId Equals c.Id
                          Where l.MedicalFeesCausationId = MedicalFeesCausationId AndAlso (c.Status = 1 OrElse c.Status = 2)
                          Select l).ToList
        End If
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            Return ListDetail
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    '''  Valida que las causaciones esten o no en una liquidacion confirmada o registrada
    ''' </summary>
    ''' <param name="ListInfo">Item1=MedicalFeesCausationId, Item2=Position</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMedicalFeesCausationIdContainInMFLDConfirmedOrRegister(ListInfo As List(Of Tuple(Of Integer, Integer))) As List(Of MedicalFeesLiquidationDetail) Implements IMedicalFeesLiquidationDetailRepository.ValidateMedicalFeesCausationIdContainInMFLDConfirmedOrRegister
        If ListInfo Is Nothing OrElse ListInfo.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If
        Dim ListIds As New List(Of Integer)
        ListInfo.ForEach(Sub(item) ListIds.Add(item.Item1))
        Dim ListDetail = (From l In _context.MedicalFeesLiquidationDetail.AsNoTracking
                          Join c In _context.MedicalFeesLiquidation.AsNoTracking On l.MedicalFeesLiquidacionId Equals c.Id
                          Where ListIds.Contains(l.MedicalFeesCausationId) AndAlso (c.Status = 1 OrElse c.Status = 2)
                          Select l).ToList
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            Return ListDetail
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el listado de los detalles de liquidacion de honorarios siempre y cuando este en estado anulado
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListMedicalFeesLiquidationDetailAnnular(MedicalFeesCausationId As Integer) As List(Of MedicalFeesLiquidationDetail) Implements IMedicalFeesLiquidationDetailRepository.GetListMedicalFeesLiquidationDetailAnnular
        If MedicalFeesCausationId = 0 Then
            Throw New ArgumentNullException("MedicalFeesCausationId")
        End If
        Dim ListDetail As List(Of MedicalFeesLiquidationDetail) = (From l In _context.MedicalFeesLiquidationDetail.AsNoTracking
                          Join c In _context.MedicalFeesLiquidation.AsNoTracking On l.MedicalFeesLiquidacionId Equals c.Id
                          Where l.MedicalFeesCausationId = MedicalFeesCausationId AndAlso c.Status = 3
                          Select l).ToList
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            Return ListDetail
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    '''  Valida que las causaciones esten o no en una liquidacion anulada
    ''' </summary>
    ''' <param name="ListInfo">Item1=MedicalFeesCausationId, Item2=Position</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMedicalFeesCausationIdContainInMFLDAnnular(ListInfo As List(Of Tuple(Of Integer, Integer))) As List(Of MedicalFeesLiquidationDetail) Implements IMedicalFeesLiquidationDetailRepository.ValidateMedicalFeesCausationIdContainInMFLDAnnular
        If ListInfo Is Nothing OrElse ListInfo.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If
        Dim ListIds As New List(Of Integer)
        ListInfo.ForEach(Sub(item) ListIds.Add(item.Item1))
        Dim ListDetail = (From l In _context.MedicalFeesLiquidationDetail.AsNoTracking
                          Join c In _context.MedicalFeesLiquidation.AsNoTracking On l.MedicalFeesLiquidacionId Equals c.Id
                          Where ListIds.Contains(l.MedicalFeesCausationId) AndAlso c.Status = 3
                          Select l).ToList
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            Return ListDetail
        End If
        Return Nothing
    End Function

#End Region

End Class
