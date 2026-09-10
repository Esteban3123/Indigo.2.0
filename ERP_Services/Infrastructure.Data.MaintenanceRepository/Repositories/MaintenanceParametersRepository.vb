'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-03-2015
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

Public Class MaintenanceParametersRepository

    Inherits GenericRepository(Of MaintenanceParameter)

    Implements IMaintenanceParameterRepository

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

    ''' <summary>
    ''' Función que obtiene los Parámetrosde Mantenimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMaintenanceParameter(Optional tracking As Boolean = True) As MaintenanceParameter Implements IMaintenanceParameterRepository.ListMaintenanceParameter
        Dim MaintenanceParameter = From e In _context.MaintenanceParameter
                        Select e
        If MaintenanceParameter.Count > 0 Then
            Dim objMaintenanceParameter = Nothing
            If tracking = False Then
                objMaintenanceParameter = (From e In _context.MaintenanceParameter.AsNoTracking
                                 Select e).FirstOrDefault()
            Else
                objMaintenanceParameter = MaintenanceParameter.FirstOrDefault()
            End If
            Return CType(objMaintenanceParameter, Domain.Maintenance.Entities.MaintenanceParameter)
        Else
            Return New MaintenanceParameter()
        End If
    End Function
End Class
