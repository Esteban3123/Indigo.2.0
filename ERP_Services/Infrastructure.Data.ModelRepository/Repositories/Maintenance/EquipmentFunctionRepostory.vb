'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad funcion del equipo
''' </summary>
''' <remarks></remarks>

Public Class EquipmentFunctionRepostory
    Inherits GenericRepository(Of EquipmentFunction)
    Implements IEquipmentFunctionRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetEquipmentFunctionById(id As Integer, Optional tracking As Boolean = True) As EquipmentFunction Implements IEquipmentFunctionRepository.GetEquipmentFunctionById
        Dim res As EquipmentFunction
        If tracking Then
            res = (From ef In _context.EquipmentFunction.Include("EquipmentRegistration") Where ef.Id = id).FirstOrDefault
        Else
            res = (From ef In _context.EquipmentFunction.AsNoTracking Where ef.Id = id).FirstOrDefault
        End If

        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From ef In _context.EquipmentFunction Where ef.Id = id).FirstOrDefault
            Return res
        Else
            Return New EquipmentFunction
        End If
    End Function

    Public Function GetEquipmentFunctionByCode(code As String, Optional tracking As Boolean = True) As EquipmentFunction Implements IEquipmentFunctionRepository.GetEquipmentFunctionByCode
        Dim res As EquipmentFunction
        If tracking Then
            res = (From ef In _context.EquipmentFunction Where ef.Code = code).FirstOrDefault
        Else
            res = (From ef In _context.EquipmentFunction.AsNoTracking Where ef.Code = code).FirstOrDefault
        End If

        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From ef In _context.EquipmentFunction Where ef.Code = code).FirstOrDefault
            Return res
        Else
            Return New EquipmentFunction
        End If
    End Function

    Public Function ListAllEquipmentFunction() As List(Of EquipmentFunction) Implements IEquipmentFunctionRepository.ListAllEquipmentFunction
        Dim Busqueda = From e In _context.EquipmentFunction
                       Where e.State = True
                       Select e

        Return Busqueda.ToList
    End Function

End Class
