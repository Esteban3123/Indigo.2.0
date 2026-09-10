'***********************************************************************
' Assembly         : Infrastructure.Data.CareGroupRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 18/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CareGroupDefinitionRateRepository
    Inherits GenericRepository(Of CareGroupDefinitionRate)
    Implements ICareGroupDefinitionRateRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' metodo para retornar la una definicion de tarifa por id del grupo de atencion y fecha
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <param name="serviceDate"></param>
    ''' <returns></returns>
    Public Function GetCareGroupDefinitionRateByCareGroupIdAndDate(careGroupId As Integer, serviceDate As Date, Optional tracking As Boolean = True) As CareGroupDefinitionRate Implements ICareGroupDefinitionRateRepository.GetCareGroupDefinitionRateByCareGroupIdAndDate
        Dim careGroupDefinitionRate As CareGroupDefinitionRate
        If tracking Then
            Dim res = (From cgdr In _context.CareGroupDefinitionRate Where cgdr.CareGroupId = careGroupId Select cgdr).ToList()
            careGroupDefinitionRate = res.Find(Function(x) serviceDate.Date >= x.InitialDate And serviceDate.Date <= x.EndDate)
            If careGroupDefinitionRate Is Nothing Then
                Return New CareGroupDefinitionRate
            End If
        Else
            Dim res = (From cgdr In _context.CareGroupDefinitionRate.AsNoTracking() Where cgdr.CareGroupId = careGroupId Select cgdr).ToList()
            careGroupDefinitionRate = res.Find(Function(x) serviceDate.Date >= x.InitialDate And serviceDate.Date <= x.EndDate)
            If careGroupDefinitionRate Is Nothing Then
                Return New CareGroupDefinitionRate
            End If
        End If
        
        Return careGroupDefinitionRate
    End Function

    ''' <summary>
    ''' Obtiene la primera condición con RIAS
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <returns></returns>
    Public Function GetConditionRIAS(definitionRateId As Integer) As DefinitionRateDetailCondition Implements ICareGroupDefinitionRateRepository.GetConditionRIAS
        Return (From x In _context.DefinitionRateDetail.AsNoTracking
                Join y In _context.DefinitionRateDetailCondition.AsNoTracking On y.DefinitionRateDetailId Equals x.Id
                Where x.ConditionType = 6 AndAlso x.DefinitionRateId = definitionRateId
                Select y).FirstOrDefault()
    End Function

End Class
