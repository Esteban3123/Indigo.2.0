'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad parte
''' </summary>
''' <remarks></remarks>
Public Class PartRepository
    Inherits GenericRepository(Of Part)
    Implements IPartRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

   
    Public Function GetPart(codePart As String, Optional ByVal tracking As Boolean = True) As Part Implements IPartRepository.GetPart
        If tracking = True Then
            Dim Busqueda = From e In _context.Part.Include("PartDetail.EquipmentType")
            Where e.Code = codePart
                  Select e
            If Busqueda.Count = 0 Then
                Return New Part
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Part.AsNoTracking.Include("PartDetail.EquipmentType")
            Where e.Code = codePart
                  Select e
            If Busqueda.Count = 0 Then
                Return New Part
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllPart() As List(Of Part) Implements IPartRepository.ListAllPart
        Dim Busqueda = From e In _context.Part
                   Select e

        Return Busqueda.ToList
    End Function

    Public Function SavePart(Part As Part) As Boolean Implements IPartRepository.SavePart
        _context.Part.ApplyChanges(Part)
        Return True
    End Function
End Class
