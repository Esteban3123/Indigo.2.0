'************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-12-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class ManagementAreasRepository
    Inherits GenericRepository(Of ManagementAreas)
    Implements IManagementAreasRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todas las areas de gestion por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListManagementAreasByUserCode(userCode As String) As List(Of ManagementAreas) Implements IManagementAreasRepository.ListManagementAreasByUserCode
        Return (From a As ManagementAreas In Me._context.ManagementAreas.Include("ManagementAreasUser")
                Where a.Status And a.ManagementAreasUser.Any(Function(u) u.Usercode.Equals(userCode))
                Select a).ToList()
    End Function

    ''' <summary>
    ''' Obtiene la area de gestion por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetManagementAreasById(Id As Integer) As ManagementAreas Implements IManagementAreasRepository.GetManagementAreasById
        Return (From a As ManagementAreas
                   In _context.ManagementAreas.Include("ManagementAreasUser")
                Where a.Id = Id
                Select a).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el area de gestion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManagementAreasByCode(code As String) As ManagementAreas Implements IManagementAreasRepository.GetManagementAreasByCode
        Return (From ba In _context.ManagementAreas.Include("ManagementAreasUser")
                Where ba.Code.Equals(code.Trim())
                Select ba).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene todas las areas de gestión con las usuarios de cada una incluidos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllWithUsers() As List(Of ManagementAreas) Implements IManagementAreasRepository.GetAllWithUsers
        Return _context.ManagementAreas.
        Include("ManagementAreasUser").
        Where(Function(ma) ma.Status = True).ToList()
    End Function


End Class

