'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities

Public Class FixedAssetRepository
    Inherits GenericRepository(Of AFNACTIVO)
    Implements IFixedAssetRepository

    ' contexto del repositorio de ciudades
    Private _context As IInteropCostModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IInteropCostModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un activo fijo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetByCode(code As String) As AFNACTIVO Implements IFixedAssetRepository.GetFixedAssetByCode
        Dim query = From e In _context.AFNACTIVO.AsNoTracking()
                    Where e.AACCODACT = code
                    Select e
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New AFNACTIVO()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un activo fijo por id
    ''' </summary>
    ''' <param name="oid"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetById(oid As Integer) As AFNACTIVO Implements IFixedAssetRepository.GetFixedAssetById
        Dim query = From e In _context.AFNACTIVO.AsNoTracking().Include("AFNPRODUC1").AsNoTracking()
                   Where e.OID = oid
                   Select e
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New AFNACTIVO()
        End If
    End Function

End Class