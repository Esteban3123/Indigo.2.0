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
''' clase para hacer todas las operaciones de persistencia para la fabricante
''' </summary>
''' <remarks></remarks>
Public Class PolizaRepository
    Inherits GenericRepository(Of Poliza)
    Implements IPolizaRepository







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

 
    Public Function GetPoliza(codePoliza As String, Optional ByVal tracking As Boolean = True) As Poliza Implements IPolizaRepository.GetPoliza
        If tracking = True Then
            Dim Busqueda = From e In _context.Poliza
                       Where e.Code = codePoliza
                       Select e

            If Busqueda.Count = 0 Then
                Return New Poliza
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Poliza.AsNoTracking()
                       Where e.Code = codePoliza
                       Select e

            If Busqueda.Count = 0 Then
                Return New Poliza
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllPoliza() As List(Of Poliza) Implements IPolizaRepository.ListAllPoliza
        Dim Busqueda = From e In _context.Poliza
                       Select e

        Return Busqueda.ToList
    End Function

    Public Function SavePoliza(Poliza As Poliza) As Boolean Implements IPolizaRepository.SavePoliza
        _context.Poliza.ApplyChanges(Poliza)
        Return True
    End Function
End Class
