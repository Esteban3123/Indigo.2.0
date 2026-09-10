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
''' clase para hacer todas las operaciones de persistencia para la entidad responsable
''' </summary>
''' <remarks></remarks>

Public Class ResponsibleRepository
    Inherits GenericRepository(Of Responsible)
    Implements IResponsibleRepository

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


    Public Function GetResponsible(codeResponsible As String, Optional tracking As Boolean = True) As Responsible Implements IResponsibleRepository.GetResponsible
        If tracking = True Then
            Dim Busqueda = From e In _context.Responsible
               Where e.Code = codeResponsible
               Select e
            If Busqueda.Count = 0 Then
                Return New Responsible
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Responsible.AsNoTracking
               Where e.Code = codeResponsible
               Select e
            If Busqueda.Count = 0 Then
                Return New Responsible
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllResponsible() As List(Of Responsible) Implements IResponsibleRepository.ListAllResponsible
        Dim Busqueda = From e In _context.Responsible
                 Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveResponsible(Responsible As Responsible) As Boolean Implements IResponsibleRepository.SaveResponsible
        _context.Responsible.ApplyChanges(Responsible)
        Return True
    End Function

    
End Class
