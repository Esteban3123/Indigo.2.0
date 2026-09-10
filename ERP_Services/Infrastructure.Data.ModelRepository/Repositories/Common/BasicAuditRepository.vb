'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Infrastructure.Data.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Auditoria Basica.
''' </summary>
Public Class BasicAuditRepository
    Inherits GenericRepository(Of BasicAudit)
    Implements IBasicAuditRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener un registro de auditoria basica.
    ''' </summary>
    ''' <param name="Id">Id de Auditoria Basica</param>
    ''' <returns>Objeto Auditoria Basica</returns>
    Public Function GetBasicAuditById(Id As String) As BasicAudit Implements IBasicAuditRepository.GetBasicAuditById
        Dim auditBasic = (From e In _context.BasicAudit
        Where e.Id = CInt(Id)
        Select e)
        If auditBasic.Count > 0 Then
            Return auditBasic.Single
        Else
            Dim objeto As New BasicAudit
            Return objeto
        End If
    End Function

    ''' <summary>
    ''' Función para obtener todos los registros de auditoria basica.
    ''' </summary>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListAllBasicAudit() As List(Of BasicAudit) Implements IBasicAuditRepository.ListAllBasicAudit
        Dim Busqueda = From e In _context.BasicAudit
                 Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Función para obtener registros especificos de auditoria basica.
    ''' </summary>
    ''' <param name="entity">Nombre de la entidad</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListBasicAuditByEntity(entity As String) As List(Of BasicAudit) Implements IBasicAuditRepository.ListBasicAuditByEntity
        Dim Busqueda = From e In _context.BasicAudit
                 Where e.Entity = entity
                 Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Función para obtener registros eliminados de auditoria basica según formulario.
    ''' </summary>
    ''' <param name="Tag">Tag del formulario</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListBasicAuditByTag(Tag As String) As List(Of BasicAudit) Implements IBasicAuditRepository.ListBasicAuditByTag
        Dim Busqueda = From e In _context.BasicAudit
                 Where e.Tag = Tag AndAlso e.Operation = 5
                 Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Función para obtener registros especificos de auditoria basica.
    ''' </summary>
    ''' <param name="Id">Id de Usuario</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListBasicAuditByIdUsuario(Id As String) As List(Of BasicAudit) Implements IBasicAuditRepository.ListBasicAuditByIdUsuario
        Dim Busqueda = From e In _context.BasicAudit
                 Where e.UserCode = CInt(Id)
                 Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Función para obtener registros especificos de auditoria basica según IdForm y IdEntity
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdEntity">Id del registro</param>
    ''' <returns>Lista Auditoria Basica</returns>
    Public Function ListBasicAuditByIdIdFormAndIdEntity(IdForm As String, IdEntity As String) As List(Of BasicAudit) Implements IBasicAuditRepository.ListBasicAuditByIdIdFormAndIdEntity
        Dim Busqueda = From e In _context.BasicAudit
                 Where e.Tag = IdForm AndAlso e.RegisterId = IdEntity
                 Select e

        Return Busqueda.ToList
    End Function

End Class
