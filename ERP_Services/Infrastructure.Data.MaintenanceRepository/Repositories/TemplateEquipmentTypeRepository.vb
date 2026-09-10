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
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Class TemplateEquipmentTypeRepository
    Inherits GenericRepository(Of TemplateEquipmentType)
    Implements ITemplateEquipmentTypeRepository


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

    Public Function GetTemplateEquipmentType(codeTemplateEquipmentType As String, Optional ByVal tracking As Boolean = True) As TemplateEquipmentType Implements ITemplateEquipmentTypeRepository.GetTemplateEquipmentType
        If tracking = True Then
            Dim Busqueda = From e In _context.TemplateEquipmentType
                Where e.Code = codeTemplateEquipmentType
                Select e
            If Busqueda.Count = 0 Then
                Return New TemplateEquipmentType
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.TemplateEquipmentType.AsNoTracking
                Where e.Code = codeTemplateEquipmentType
                Select e
            If Busqueda.Count = 0 Then
                Return New TemplateEquipmentType
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllTemplateEquipmentType() As List(Of TemplateEquipmentType) Implements ITemplateEquipmentTypeRepository.ListAllTemplateEquipmentType
        Dim Busqueda = From e In _context.TemplateEquipmentType
                      Where e.State = True
                      Select e
        Return Busqueda.ToList
    End Function

    Public Function SaveTemplateEquipmentType(TemplateEquipmentType As TemplateEquipmentType) As Boolean Implements ITemplateEquipmentTypeRepository.SaveTemplateEquipmentType
        _context.TemplateEquipmentType.ApplyChanges(TemplateEquipmentType)
        Return True
    End Function

    Public Function GetTemplateEquipmentTypeByEquipmentTypeId(IdEquipmentType As Integer) As TemplateEquipmentType Implements ITemplateEquipmentTypeRepository.GetTemplateEquipmentTypeByEquipmentTypeId

        Dim Busqueda = From e In _context.TemplateEquipmentType
            Where e.IdEquipmentType = IdEquipmentType
            Select e
        If Busqueda.Count = 0 Then
            Return New TemplateEquipmentType
        Else
            Return Busqueda.Single
        End If

    End Function
End Class
