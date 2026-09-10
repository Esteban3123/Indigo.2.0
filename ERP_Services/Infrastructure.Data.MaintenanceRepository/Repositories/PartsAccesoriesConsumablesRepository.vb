'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class PartsAccesoriesConsumablesRepository
    Inherits GenericRepository(Of PartsAccesoriesConsumables)
    Implements IPartsAccesoriesConsumablesRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    Public Sub New(ByVal context As IMaintenanceModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene una Parte, Accesorio por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String, Optional desatach As Boolean = True) As PartsAccesoriesConsumables Implements IPartsAccesoriesConsumablesRepository.GetPartsAccesoriesConsumablesByCode
        Dim PartsAccesoriesConsumables = From e In _context.PartsAccesoriesConsumables
                       Where e.Code = Code
                       Select e
        If PartsAccesoriesConsumables.Count > 0 Then
            Dim objPartsAccesoriesConsumables = Nothing
            If desatach = False Then
                objPartsAccesoriesConsumables = (From e In _context.PartsAccesoriesConsumables.AsNoTracking
                                 Where e.Code = Code
                                 Select e).SingleOrDefault
            Else
                objPartsAccesoriesConsumables = PartsAccesoriesConsumables.FirstOrDefault()
            End If
            Return CType(objPartsAccesoriesConsumables, Domain.Maintenance.Entities.PartsAccesoriesConsumables)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function ListAllPartsAccesoriesConsumables() As List(Of PartsAccesoriesConsumables) Implements IPartsAccesoriesConsumablesRepository.ListAllPartsAccesoriesConsumables
        Dim ListPartsAccesoriesConsumables = From e In _context.PartsAccesoriesConsumables
                  Select e

        If ListPartsAccesoriesConsumables.Count() > 0 Then
            Return ListPartsAccesoriesConsumables.ToList()
        Else
            Return New List(Of PartsAccesoriesConsumables)
        End If
    End Function
End Class
