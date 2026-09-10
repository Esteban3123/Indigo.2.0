'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Judy Andrea Díaz Reyes
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports Domain.Base

Public Class UnitDoseTypeRepository
    Inherits GenericRepository(Of UnitDoseType)
    Implements IUnitDoseTypeRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    ''' <remarks></remarks>
    Public Function ListAllUnitDoseType() As List(Of UnitDoseType) Implements IUnitDoseTypeRepository.ListAllUnitDoseType
        Dim unitDoseType = From e In _context.UnitDoseType
                           Select e
        Return unitDoseType.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria especifica
    ''' </summary>
    ''' <param name="code">Codigo del tipo de dosis unitaria</param>
    ''' <returns>Tipo de Dosis Unitaria</returns>
    ''' <remarks></remarks>
    Public Function GetUnitDoseType(code As String, Optional tracking As Boolean = True) As UnitDoseType Implements IUnitDoseTypeRepository.GetUnitDoseType
        Dim query = From e In _context.UnitDoseType
                    Where e.Code = code
                    Select e

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Dim unitDoseType = query.FirstOrDefault()

        If unitDoseType IsNot Nothing Then

            If unitDoseType.InventoryGroupId.HasValue Then
                Dim group = _context.ProductGroup.AsNoTracking() _
                    .Where(Function(m) m.Id = unitDoseType.InventoryGroupId.Value) _
                    .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
                unitDoseType.InventoryGroupCodeName = $"{group.Code} - {group.Name}"
            End If

            If unitDoseType.InventorySubGroupId.HasValue Then
                Dim subgroup = _context.ProductSubGroup.AsNoTracking() _
                    .Where(Function(m) m.Id = unitDoseType.InventorySubGroupId.Value) _
                    .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
                unitDoseType.InventorySubGroupCodeName = $"{subgroup.Code} - {subgroup.Name}"
            End If

            If unitDoseType.IVACodeId.HasValue Then
                Dim iva = _context.GeneralLedgerIVA.AsNoTracking() _
                    .Where(Function(m) m.Id = unitDoseType.IVACodeId.Value) _
                    .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
                unitDoseType.IVaCodeName = $"{iva.Code} - {iva.Name}"
            End If

            If unitDoseType.BillingGroupId.HasValue Then
                Dim bgroup = _context.BillingGroup.AsNoTracking() _
                    .Where(Function(m) m.Id = unitDoseType.BillingGroupId.Value) _
                    .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
                unitDoseType.BillingGroupCodeName = $"{bgroup.Code} - {bgroup.Name}"
            End If

        End If

        Return unitDoseType
    End Function
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por el identificador
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetUnitDoseTypeById(id As String, Optional tracking As Boolean = True) As UnitDoseType Implements IUnitDoseTypeRepository.GetUnitDoseTypeById
        Dim unitDoseType = From e In _context.UnitDoseType
                           Where e.Id = id
                           Select e
        If unitDoseType.Count > 0 Then
            Dim ObjUnitDoseType = Nothing
            If tracking = False Then
                ObjUnitDoseType = (From e In _context.UnitDoseType.AsNoTracking
                                   Where e.Id = id
                                   Select e).SingleOrDefault
            Else
                ObjUnitDoseType = unitDoseType.SingleOrDefault
            End If
            Return ObjUnitDoseType
        Else
            Return New UnitDoseType()
        End If
    End Function
End Class