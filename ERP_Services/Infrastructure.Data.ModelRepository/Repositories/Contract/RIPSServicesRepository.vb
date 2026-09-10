'************************************************************
' Assembly         : Infrastructure.Data.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RIPSServicesRepository
    Inherits GenericRepository(Of RIPSServices)
    Implements IRIPSServicesRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos los servicios RIPS
    ''' </summary>
    ''' <returns>Lista de servicios RIPS</returns>
    Public Function ListAllRIPSServices() As List(Of RIPSServices) Implements IRIPSServicesRepository.ListAllRIPSServices
        Dim Busqueda = From e In _context.RIPSServices
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Obtiene un servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del servicio</param>
    ''' <returns>Servicio RIPS</returns>
    Public Function GetRIPSServiceById(Id As Integer, Optional tracking As Boolean = True) As RIPSServices Implements IRIPSServicesRepository.GetRIPSServiceById
        Dim Service = (From e In _context.RIPSServices
                       Where e.Id = Id
                       Select e).FirstOrDefault()
        Return Service
    End Function

    ''' <summary>
    ''' Obtiene un servicio RIPS por código.
    ''' </summary>
    ''' <param name="code">Código del servicio</param>
    ''' <returns>Servicio RIPS</returns>
    Public Function GetRIPSServiceByCode(code As String) As RIPSServices Implements IRIPSServicesRepository.GetRIPSServiceByCode
        Dim Service = (From e In _context.RIPSServices
                       Where e.Code = code
                       Select e).FirstOrDefault
        Return Service
    End Function
End Class
