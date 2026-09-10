'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class PharmacologicalGroupRepository
    Inherits GenericRepository(Of PharmacologicalGroup)
    Implements IPharmacologicalGroupRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un grupo farmacologico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmacologicalGroup(code As String) As PharmacologicalGroup Implements IPharmacologicalGroupRepository.GetPharmacologicalGroup
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PharmacologicalGroup In Me._context.PharmacologicalGroup
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.PharmacologicalGroup.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New PharmacologicalGroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo farmacologico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmacologicalGroupById(id As Integer) As PharmacologicalGroup Implements IPharmacologicalGroupRepository.GetPharmacologicalGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PharmacologicalGroup Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As PharmacologicalGroup In Me._context.PharmacologicalGroup.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New PharmacologicalGroup()
        End If
    End Function

    ''' <summary>
    ''' Función que se utiliza para Almacenar o Actualizar una Unidad de Medida
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Status"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SavePharmacologicalGroup(Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SavePharmacologicalGroup_Result Implements IPharmacologicalGroupRepository.SavePharmacologicalGroup
        Return _context.SP_SavePharmacologicalGroup(Code, Description, Status, CodeUser).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeletePharmacologicalGroup(Id As Integer) As SP_DeletePharmacologicalGroup_Result Implements IPharmacologicalGroupRepository.SP_DeletePharmacologicalGroup
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeletePharmacologicalGroup(Id).SingleOrDefault
    End Function

End Class
