'***********************************************************************
' Assembly         : Infrastructure.Data.CareGroupRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 18/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DefinitionRateDetailConditionRepository
    Inherits GenericRepository(Of DefinitionRateDetailCondition)
    Implements IDefinitionRateDetailConditionRepository

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
    ''' obtiene una condicion por unidad funcional
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="functionalUnitId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionByFunctionalUnit(definitionRateDetailId As Integer, functionalUnitId As Integer) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByFunctionalUnit
        Dim res = (From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And drdc.FunctionalUnitId = functionalUnitId Select drdc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New DefinitionRateDetailCondition
    End Function

    ''' <summary>
    ''' obtiene una condicion por especialidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="specialty"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionBySpecialty(definitionRateDetailId As Integer, specialty As String) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionBySpecialty
        Dim res = (From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And drdc.SpecialtyId = specialty Select drdc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New DefinitionRateDetailCondition
    End Function

    ''' <summary>
    ''' obtiene una condicion por tiempo
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionByTime(definitionRateDetailId As Integer, time As TimeSpan) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByTime
        Dim res = From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And time >= drdc.StartTime And time <= drdc.EndTime Select drdc
        If res.Count() > 0 Then
            Return res.SingleOrDefault()
        End If
        Return New DefinitionRateDetailCondition
    End Function

    ''' <summary>
    ''' obtiene una condicion por tipo de unidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="unitTypeId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionByUnitType(definitionRateDetailId As Integer, unitTypeId As Integer) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByUnitType
        Dim res = (From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And drdc.UnitTypeId = unitTypeId Select drdc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New DefinitionRateDetailCondition
    End Function


    ''' <summary>
    ''' obtiene un detalle de la definicion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionById(Id As Integer, Optional tracking As Boolean = True) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionById
        If tracking Then
            Dim res = (From drdt In _context.DefinitionRateDetailCondition Where drdt.Id = Id Select drdt).FirstOrDefault()
            If res IsNot Nothing Then
                Return res
            End If
            Return New DefinitionRateDetailCondition
        Else
            Dim res = (From drdt In _context.DefinitionRateDetailCondition.AsNoTracking() Where drdt.Id = Id Select drdt).FirstOrDefault()
            If res IsNot Nothing Then
                Return res
            End If
            Return New DefinitionRateDetailCondition
        End If
    End Function

    ''' <summary>
    ''' Metodo para buscar por dos condiciones las cuales son Horario y Especialidad
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRateDetailByDefinitionRateDetailId(definitionRateDetailId As Integer) As List(Of DefinitionRateDetailCondition) Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailByDefinitionRateDetailId
        Dim res = From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId Select drdc
        Return res.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una condición por rias
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="riasId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionByRiasId(definitionRateDetailId As Integer, riasId As Integer) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByRiasId
        Dim res = (From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And drdc.RIASId = riasId Select drdc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New DefinitionRateDetailCondition
    End Function

    ''' <summary>
    ''' Consulta la condición por descripcion
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <param name="descriptionId"></param>
    ''' <returns></returns>
    Public Function GetDefinitionRateDetailConditionByDescriptionId(definitionRateDetailId As Integer, descriptionId As Integer) As DefinitionRateDetailCondition Implements IDefinitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByDescriptionId
        Dim res = (From drdc In _context.DefinitionRateDetailCondition Where drdc.DefinitionRateDetailId = definitionRateDetailId And drdc.ContractDescriptionId = descriptionId Select drdc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New DefinitionRateDetailCondition
    End Function

End Class
