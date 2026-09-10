'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class MeasureUnitRepository
    Inherits GenericRepository(Of InventoryMeasurementUnit)
    Implements IMeasureUnitRepository

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
    ''' Obtiene una unidad de medida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMeasureUnit(code As String) As InventoryMeasurementUnit Implements IMeasureUnitRepository.GetMeasureUnit
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As InventoryMeasurementUnit In Me._context.InventoryMeasurementUnit
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.InventoryMeasurementUnit.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New InventoryMeasurementUnit()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una unidad de medida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMeasureUnitById(id As Integer) As InventoryMeasurementUnit Implements IMeasureUnitRepository.GetMeasureUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.InventoryMeasurementUnit Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As InventoryMeasurementUnit In Me._context.InventoryMeasurementUnit.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New InventoryMeasurementUnit()
        End If
    End Function

    ''' <summary>
    ''' Función que se utiliza para Almacenar o Actualizar una Unidad de Medida
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Abbreviation"></param>
    ''' <param name="UnitType"></param>
    ''' <param name="Status"></param>
    ''' <param name="AllowEditCostValue"></param>
    ''' <param name="CostValue"></param>
    ''' <param name="CodeUser"></param>
    ''' <param name="RequiresStandardCode"></param>
    ''' <param name="StandardCode"></param>
    ''' <returns></returns>
    Public Function SaveMeasurementUnit(Code As String, Description As String, Abbreviation As String, UnitType As Byte, Status As Boolean, AllowEditCostValue As Boolean, CostValue As Decimal, CodeUser As String, RequiresStandardCode As Boolean, StandardCode As String) As SP_SaveMeasureUnit_Result Implements IMeasureUnitRepository.SaveMeasurementUnit
        Return _context.SP_SaveMeasureUnit(Code, Description, Abbreviation, UnitType, Status, AllowEditCostValue, CostValue, CodeUser, RequiresStandardCode, StandardCode).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteMeasurementUnit(Id As Integer) As SP_DeleteMeasurementUnit_Result Implements IMeasureUnitRepository.SP_DeleteMeasurementUnit
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteMeasurementUnit(Id).SingleOrDefault
    End Function

End Class
