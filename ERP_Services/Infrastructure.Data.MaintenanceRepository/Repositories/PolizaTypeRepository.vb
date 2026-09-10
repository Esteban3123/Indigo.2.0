'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
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
''' clase para hacer todas las operaciones de persistencia para la entidad aseguradora
''' </summary>
Public Class PolizaTypeRepository
    Inherits GenericRepository(Of PolizaType)
    Implements IPolizaTypeRepository




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

   
    Public Function GetPolizaType(codepolizatype As String, Optional tracking As Boolean = True) As PolizaType Implements IPolizaTypeRepository.GetPolizaType
        If tracking = True Then
            Dim Busqueda = From e In _context.PolizaType
                       Where e.Code = codepolizatype
                       Select e

            If Busqueda.Count = 0 Then
                Return New PolizaType
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.PolizaType.AsNoTracking
                       Where e.Code = codepolizatype
                       Select e

            If Busqueda.Count = 0 Then
                Return New PolizaType
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllPolizaType() As List(Of PolizaType) Implements IPolizaTypeRepository.ListAllPolizaType
        Dim Busqueda = From e In _context.PolizaType
                       Select e

        Return Busqueda.ToList
    End Function

    Public Function SavePolizaType(PolizaType As PolizaType) As Boolean Implements IPolizaTypeRepository.SavePolizaType
        _context.PolizaType.ApplyChanges(PolizaType)
        Return True
    End Function
End Class
